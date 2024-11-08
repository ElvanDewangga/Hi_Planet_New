﻿#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define DEVELOPMENT
#endif
using FishNet.Connection;
using FishNet.Documenting;
using FishNet.Managing.Logging;
using FishNet.Managing.Object;
using FishNet.Managing.Server;
using FishNet.Managing.Utility;
using FishNet.Object;
using FishNet.Object.Helping;
using FishNet.Serializing;
using FishNet.Transporting;
using FishNet.Utility.Extension;
using FishNet.Utility.Performance;
using GameKit.Dependencies.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FishNet.Serializing.Helping;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FishNet.Managing.Client
{
    /// <summary>
    /// Handles objects and information about objects for the local client. See ManagedObjects for inherited options.
    /// </summary>
    public partial class ClientObjects : ManagedObjects
    {
        #region Internal.
        /// <summary>
        /// NetworkObjects which are currently active on the local client.
        /// </summary>
        internal List<NetworkObject> LocalClientSpawned = new();
        #endregion

        #region Private.
        /// <summary>
        /// NetworkObjects which are cached to be spawned or despawned.
        /// </summary>
        private ClientObjectCache _objectCache;
        #endregion

        internal ClientObjects(NetworkManager networkManager)
        {
            base.Initialize(networkManager);
            _objectCache = new(this, networkManager);
        }

        /// <summary>
        /// Called when a connection state changes for the local server.
        /// </summary>
        internal void OnServerConnectionState(ServerConnectionStateArgs args)
        {
            //Nothing needs to be done if started.
            if (args.ConnectionState == LocalConnectionState.Started)
                return;

            /* If not started and client is active then deinitialize
             * client objects first. This will let the deinit calls
             * perform before the server destroys them. Ideally this
             * would be done when the user shows intent to shutdown
             * the server, but realistically planning for server socket
             * drops is a much more universal solution.
             *
             * Calling StopConnection on the client will set it's local state
             * to Stopping which will result in a deinit. */

            /* Only perform this step if the transport being stopped
             * is the one which client is connected to. */
            if (NetworkManager.IsClientStarted && args.TransportIndex == base.NetworkManager.ClientManager.GetTransportIndex())
                base.NetworkManager.ClientManager.StopConnection();
        }

        /// <summary>
        /// Called when the connection state changes for the local client.
        /// </summary>
        /// <param name="args"></param>
        internal void OnClientConnectionState(ClientConnectionStateArgs args)
        {
            /* If new state is not started then reset
             * environment. */
            if (args.ConnectionState != LocalConnectionState.Started)
            {
                _objectCache.Reset();

                //If not server then deinitialize normally.
                if (!base.NetworkManager.IsServerStarted)
                {
                    base.DespawnWithoutSynchronization(false);
                }
                //Otherwise invoke stop callbacks only for client side.
                else
                {
                    foreach (NetworkObject n in Spawned.Values)
                    {
                        n.InvokeStopCallbacks(false, true);
                        n.SetInitializedStatus(false, false);
                    }
                }

                /* Clear spawned and scene objects as they will be rebuilt.
                 * Spawned would have already be cleared if DespawnSpawned
                 * was called but it won't hurt anything clearing an empty collection. */
                base.Spawned.Clear();
                base.SceneObjects_Internal.Clear();
                LocalClientSpawned.Clear();
            }
        }


        /// <summary>
        /// Called when a scene is loaded.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="arg1"></param>
        [APIExclude]
        protected internal override void SceneManager_sceneLoaded(Scene s, LoadSceneMode arg1)
        {
            base.SceneManager_sceneLoaded(s, arg1);

            if (!base.NetworkManager.IsClientStarted)
                return;
            /* When a scene first loads for a client it should disable
             * all network objects in that scene. The server will send
             * spawn messages once it's aware client has loaded the scene. */
            RegisterAndDespawnSceneObjects(s);
        }

        /// <summary>
        /// Adds a NetworkObject to Spawned.
        /// </summary>
        internal override void AddToSpawned(NetworkObject nob, bool asServer)
        {
            LocalClientSpawned.Add(nob);
            base.AddToSpawned(nob, asServer);
            //If being added as client and is also server.
            if (NetworkManager.IsServerStarted)
                nob.SetRenderersVisible(true);
        }

        /// <summary>
        /// Removes a NetworkedObject from spawned.
        /// </summary>
        protected override void RemoveFromSpawned(NetworkObject nob, bool unexpectedlyDestroyed, bool asServer)
        {
            //No need to check if !asServer.
            LocalClientSpawned.Remove(nob);
            base.RemoveFromSpawned(nob, unexpectedlyDestroyed, asServer);
        }

        /// <summary>
        /// Sends a predicted spawn to the server.
        /// </summary>
        internal void PredictedSpawn(NetworkObject networkObject, NetworkConnection ownerConnection)
        {
            //No more Ids to use.
            Queue<int> predictedObjectIds = NetworkManager.ClientManager.Connection.PredictedObjectIds;
            if (predictedObjectIds.Count == 0)
            {
                NetworkManager.LogError($"Predicted spawn for object {networkObject.name} failed because no more predicted ObjectIds remain. This usually occurs when the client is spawning excessively before the server can respond. Increasing ReservedObjectIds within the ServerManager component or reducing spawn rate could prevent this problem.");
                return;
            }

            networkObject.InitializePredictedObject_Client(base.NetworkManager, predictedObjectIds.Dequeue(), ownerConnection, base.NetworkManager.ClientManager.Connection);
            NetworkManager.ClientManager.Objects.AddToSpawned(networkObject, false);
            networkObject.Initialize(false, true);

            PooledWriter writer = WriterPool.Retrieve();
            if (WriteSpawn(networkObject, writer, connection: null))
            {
                base.NetworkManager.TransportManager.SendToServer((byte)Channel.Reliable, writer.GetArraySegment());
            }
            else
            {
                networkObject.Deinitialize(false);
                NetworkManager.StorePooledInstantiated(networkObject, false);
            }

            writer.Store();
        }

        /// <summary>
        /// Sends a predicted despawn to the server.
        /// </summary>
        internal void PredictedDespawn(NetworkObject networkObject)
        {
            PooledWriter writer = WriterPool.Retrieve();
            WriteDepawn(networkObject, writer);
            base.NetworkManager.TransportManager.SendToServer((byte)Channel.Reliable, writer.GetArraySegment());
            writer.Store();

            networkObject.Deinitialize(false);
            NetworkManager.StorePooledInstantiated(networkObject, false);
        }

        /// <summary>
        /// Writes a predicted despawn.
        /// </summary>
        public void WriteDepawn(NetworkObject nob, Writer writer)
        {
            writer.WritePacketIdUnpacked(PacketId.ObjectDespawn);
            writer.WriteNetworkObject(nob);
        }

        /// <summary>
        /// Registers NetworkObjects in all scenes and despawns them.
        /// </summary>
        internal void RegisterAndDespawnSceneObjects()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
                RegisterAndDespawnSceneObjects(SceneManager.GetSceneAt(i));
        }

        /// <summary>
        /// Adds NetworkObjects within s to SceneObjects, and despawns them.
        /// </summary>
        /// <param name="s"></param>
        private void RegisterAndDespawnSceneObjects(Scene s)
        {
            List<NetworkObject> nobs = CollectionCaches<NetworkObject>.RetrieveList();
            Scenes.GetSceneNetworkObjects(s, false, true, true, ref nobs);

            bool isServerStarted = base.NetworkManager.IsServerStarted;

            int nobsCount = nobs.Count;
            for (int i = 0; i < nobsCount; i++)
            {
                NetworkObject nob = nobs[i];
                if (!nob.IsSceneObject)
                    continue;

                //Only set initialized values if not server, as server would have already done so.
                if (!isServerStarted)
                    nob.SetInitializedValues(null);

                if (nob.IsNetworked)
                {
                    base.AddToSceneObjects(nob);
                    //Only run if not also server, as this already ran on server.
                    if (!base.NetworkManager.IsServerStarted)
                        nob.gameObject.SetActive(false);
                }
            }

            CollectionCaches<NetworkObject>.Store(nobs);
        }

        /// <summary>
        /// Called when a NetworkObject runs Deactivate.
        /// </summary>
        /// <param name="nob"></param>
        internal override void NetworkObjectUnexpectedlyDestroyed(NetworkObject nob, bool asServer)
        {
            nob.RemoveClientRpcLinkIndexes();
            base.NetworkObjectUnexpectedlyDestroyed(nob, asServer);
        }

        /// <summary>
        /// Parses an OwnershipChange packet.
        /// </summary>
        /// <param name="reader"></param>
        internal void ParseOwnershipChange(PooledReader reader)
        {
            NetworkObject nob = reader.ReadNetworkObject();
            NetworkConnection newOwner = reader.ReadNetworkConnection();
            if (nob != null && nob.IsSpawned)
                nob.GiveOwnership(newOwner, asServer: false, includeNested: false);
            else
                NetworkManager.LogWarning($"NetworkBehaviour could not be found when trying to parse OwnershipChange packet.");
        }

        /// <summary>
        /// Parses a received syncVar.
        /// </summary>
        /// <param name="reader"></param>
        internal void ParseSyncType(PooledReader reader, Channel channel)
        {
            //cleanup this is unique to synctypes where length comes first.
            //this will change once I tidy up synctypes.
            ushort packetId = (ushort)PacketId.SyncType;
            NetworkBehaviour nb = reader.ReadNetworkBehaviour();
            int length = (int)ReservedLengthWriter.ReadLength(reader, NetworkBehaviour.SYNCTYPE_RESERVE_BYTES);

            if (nb != null && nb.IsSpawned)
            {
                /* Length of data to be read for syncvars.
                 * This is important because syncvars are never
                 * a set length and data must be read through completion.
                 * The only way to know where completion of syncvar is, versus
                 * when another packet starts is by including the length. */
                if (length > 0)
                    nb.ReadSyncType(reader, length);
            }
            else
            {
                SkipDataLength(packetId, reader, length);
            }
        }

        /// <summary>
        /// Parses a 
        /// </summary>
        /// <param name="reader"></param>
        internal void ParsePredictedSpawnResult(PooledReader reader)
        {
            bool success = reader.ReadBoolean();
            int usedObjectId = reader.ReadNetworkObjectId();
            int nextObjectId = reader.ReadNetworkObjectId();
            if (nextObjectId != NetworkObject.UNSET_OBJECTID_VALUE)
                NetworkManager.ClientManager.Connection.PredictedObjectIds.Enqueue(nextObjectId);
            
            /* If successful then read and apply RPCLinks.
             * Otherwise, find and deinitialize/destroy failed predicted spawn. */
            if (success)
            {
                //Read RpcLinks.
                uint segmentSize = ReservedLengthWriter.ReadLength(reader, NetworkBehaviour.RPCLINK_RESERVED_BYTES);
                ArraySegment<byte> rpcLinks = reader.ReadArraySegment((int)segmentSize);

                /* If found as still spawned then apply RPCLinks. If
                 * not found it shouldn't be a problem, just have to make sure
                 * the remainder of the response is parsed. */
                if (Spawned.TryGetValueIL2CPP(usedObjectId, out NetworkObject nob))
                {
                    PooledReader rpcLinkReader = ReaderPool.Retrieve(rpcLinks, NetworkManager, Reader.DataSource.Server);
                    ApplyRpcLinks(nob, rpcLinkReader);
                    ReaderPool.Store(rpcLinkReader);
                }
            }
            else
            {
                if (Spawned.TryGetValueIL2CPP(usedObjectId, out NetworkObject nob))
                {
                    nob.Deinitialize(false);
                    NetworkManager.StorePooledInstantiated(nob, false);
                }
            }

        }

        /// <summary>
        /// Parses a ReconcileRpc.
        /// </summary>
        /// <param name="reader"></param>
        internal void ParseReconcileRpc(PooledReader reader, Channel channel)
        {
            NetworkBehaviour nb = reader.ReadNetworkBehaviour();
            int dataLength = Packets.GetPacketLength((ushort)PacketId.Reconcile, reader, channel);

            if (nb != null && nb.IsSpawned)
                nb.OnReconcileRpc(null, reader, channel);
            else
                SkipDataLength((ushort)PacketId.ObserversRpc, reader, dataLength);
        }

        /// <summary>
        /// Parses an ObserversRpc.
        /// </summary>
        /// <param name="reader"></param>
        internal void ParseObserversRpc(PooledReader reader, Channel channel)
        {
            NetworkBehaviour nb = reader.ReadNetworkBehaviour();
            int dataLength = Packets.GetPacketLength((ushort)PacketId.ObserversRpc, reader, channel);

            if (nb != null && nb.IsSpawned)
                nb.ReadObserversRpc(null, reader, channel);
            else
                SkipDataLength((ushort)PacketId.ObserversRpc, reader, dataLength);
        }

        /// <summary>
        /// Parses a TargetRpc.
        /// </summary>
        /// <param name="reader"></param>
        internal void ParseTargetRpc(PooledReader reader, Channel channel)
        {
            NetworkBehaviour nb = reader.ReadNetworkBehaviour();
            int dataLength = Packets.GetPacketLength((ushort)PacketId.TargetRpc, reader, channel);

            if (nb != null && nb.IsSpawned)
                nb.ReadTargetRpc(null, reader, channel);
            else
                SkipDataLength((ushort)PacketId.TargetRpc, reader, dataLength);
        }

        /// <summary>
        /// Caches a received spawn to be processed after all spawns and despawns are received for the tick.
        /// </summary>
        internal void ReadSpawn(PooledReader reader)
        {
            SpawnType st = (SpawnType)reader.ReadUInt8Unpacked();
            
            bool sceneObject = st.FastContains(SpawnType.Scene);

            ReadNestedSpawnIds(reader, st, out byte? nobComponentId, out int? parentObjectId, out byte? parentComponentId, _objectCache.ReadSpawningObjects);

            //NeworkObject and owner information.
            int objectId = reader.ReadSpawnedNetworkObject(out sbyte initializeOrder, out ushort collectionId);
            int ownerId = reader.ReadNetworkConnectionId();

            //Read transform values which differ from serialized values.
            Vector3? localPosition;
            Quaternion? localRotation;
            Vector3? localScale;
            base.ReadTransformProperties(reader, out localPosition, out localRotation, out localScale);

            int prefabId = 0;
            ulong sceneId = 0;
            string sceneName = string.Empty;
            string objectName = string.Empty;

            if (sceneObject)
            {
                base.ReadSceneObjectId(reader, out sceneId);
#if DEVELOPMENT
                if (NetworkManager.ClientManager.IsServerDevelopment)
                    base.CheckReadSceneObjectDetails(reader, ref sceneName, ref objectName);
#endif
            }
            else
            {
                prefabId = reader.ReadNetworkObjectId();
            }

            ArraySegment<byte> payload = base.ReadPayload(reader);

            //Read RpcLinks.
            uint segmentSize = ReservedLengthWriter.ReadLength(reader, NetworkBehaviour.RPCLINK_RESERVED_BYTES);
            ArraySegment<byte> rpcLinks = reader.ReadArraySegment((int)segmentSize);

            //Read SyncTypes.
            segmentSize = ReservedLengthWriter.ReadLength(reader, NetworkBehaviour.SYNCTYPE_RESERVE_BYTES);
            ArraySegment<byte> syncTypes = reader.ReadArraySegment((int)segmentSize);

            /* If the objectId can be found as already spawned then check if it's predicted.
             * Should the spawn be predicted then no need to continue. Later on however
             * we may want to apply synctypes.
             *
             * Only check if not server, since if server the client doesnt need
             * to predicted spawn. */
            if (!base.NetworkManager.IsServerStarted && base.Spawned.TryGetValue(objectId, out NetworkObject nob))
            {
                //If not predicted the nob should not be in spawned.
                if (!nob.PredictedSpawner.IsValid)
                {
                    NetworkManager.LogWarning($"Received a spawn objectId of {objectId} which was already found in spawned, and was not predicted. This sometimes may occur on clientHost when the server destroys an object unexpectedly before the clientHost gets the spawn message.");
                }
                //Everything is proper, apply RPC links.
                else
                {
                    //Only apply rpcLinks if there are links to apply.
                    if (rpcLinks.Count > 0)
                    {
                        PooledReader linkReader = ReaderPool.Retrieve(rpcLinks, NetworkManager);
                        ApplyRpcLinks(nob, linkReader);
                        linkReader.Store();
                    }
                }

                //No further initialization needed when predicting.
                return;
            }

            _objectCache.AddSpawn(base.NetworkManager, collectionId, objectId, initializeOrder, ownerId, st, nobComponentId, parentObjectId, parentComponentId, prefabId, localPosition, localRotation, localScale, sceneId, sceneName, objectName, payload, rpcLinks, syncTypes);
        }

        /// <summary>
        /// Caches a received despawn to be processed after all spawns and despawns are received for the tick.
        /// </summary>
        /// <param name="reader"></param>
        internal void CacheDespawn(PooledReader reader)
        {
            DespawnType despawnType;
            int objectId = reader.ReadNetworkObjectForDepawn(out despawnType);
            _objectCache.AddDespawn(objectId, despawnType);
        }

        /// <summary>
        /// Iterates object cache which contains spawn and despawn messages.
        /// Parses the packets within the cache and ensures objects are spawned and despawned before their sync values are applied.
        /// This ensures there is no chance a sync value is referencing a spawned object which does not exist yet due to it normally being spawned later in the cache.
        /// </summary>
        internal void IterateObjectCache()
        {
            _objectCache.Iterate();
        }

        /// <summary>
        /// Gets a nested NetworkObject within it's root.
        /// </summary>
        /// <param name="cnob"></param>
        /// <returns></returns>
        internal NetworkObject GetNestedNetworkObject(CachedNetworkObject cnob)
        {
            NetworkObject rootNob;
            int rootObjectId = cnob.ParentObjectId.Value;
            byte componentIndex = cnob.ComponentId.Value;

            /* Spawns are processed after all spawns come in,
             * this ensures no reference race conditions. Turns out because of this
             * the parentNob may be in cache and not actually spawned, if it was spawned the same packet
             * as this one. So when not found in the spawned collection try to
             * find it in Spawning before throwing. */
            rootNob = _objectCache.GetSpawnedObject(rootObjectId);
            //If still null, that's not good.
            if (rootNob == null)
            {
                NetworkManager.LogError($"Nested spawned object with componentIndex of {componentIndex} and a parentId of {rootObjectId} could not be spawned because parent was not found.");
                return null;
            }

            NetworkObject nob = null;
            List<NetworkObject> childNobs = rootNob.InitializedNestedNetworkObjects;

            //Find nob with component index.
            for (int i = 0; i < childNobs.Count; i++)
            {
                if (childNobs[i].ComponentIndex == componentIndex)
                {
                    nob = childNobs[i];
                    break;
                }
            }

            //If child nob was not found.
            if (nob == null)
            {
                NetworkManager.LogError($"Nested spawned object with componentIndex of {componentIndex} could not be found as a child NetworkObject of {rootNob.name}.");
                return null;
            }

            return nob;
        }

        /// <summary>
        /// Applies RPCLinks to a NetworkObject.
        /// </summary>
        internal void ApplyRpcLinks(NetworkObject nob, Reader reader)
        {
            List<ushort> rpcLinkIndexes = new();

            while (reader.Remaining > 0)
            {
                byte componentId = reader.ReadNetworkBehaviourId();
                ushort count = reader.ReadUInt16Unpacked();

                for (int i = 0; i < count; i++)
                {
                    //Index of RpcLink.
                    ushort linkIndex = reader.ReadUInt16Unpacked();
                    RpcLink link = new(nob.ObjectId, componentId,
                        //RpcHash.
                        reader.ReadUInt16Unpacked(),
                        //ObserverRpc.
                        (RpcType)reader.ReadUInt8Unpacked());
                    //Add to links.
                    SetRpcLink(linkIndex, link);
                    rpcLinkIndexes.Add(linkIndex);
                }
            }

            nob.SetRpcLinkIndexes(rpcLinkIndexes);
        }

        /// <summary>
        /// Instantiates a NetworkObject if required and sets transform values.
        /// </summary>
        internal NetworkObject GetInstantiatedNetworkObject(CachedNetworkObject cnob)
        {
            if (cnob.PrefabId == null)
            {
                NetworkManager.LogError($"PrefabId for {cnob.ObjectId} is null. Object will not spawn.");
                return null;
            }

            NetworkManager networkManager = base.NetworkManager;
            int prefabId = cnob.PrefabId.Value;
            NetworkObject result;

            if (prefabId == NetworkObject.UNSET_OBJECTID_VALUE)
            {
                NetworkManager.LogError($"Spawned object has an invalid prefabId. Make sure all objects which are being spawned over the network are within SpawnableObjects on the NetworkManager.");
                return null;
            }

            ushort collectionId = cnob.CollectionId;
            //PrefabObjects to get the prefab from.
            PrefabObjects prefabObjects = networkManager.GetPrefabObjects<PrefabObjects>(collectionId, false);
            //Not found for collectionId > 0. This means the user likely did not setup the collection on client.
            if (prefabObjects == null && collectionId > 0)
            {
                networkManager.LogError($"PrefabObjects collection is not found for CollectionId {collectionId}. Be sure to add your addressables NetworkObject prefabs to the collection on server and client before attempting to spawn them over the network.");
                return null;
            }

            //Only instantiate if not host.
            if (!networkManager.IsHostStarted)
            {
                Transform parentTransform = null;
                //Set parentTransform if there's a parent object.
                if (cnob.HasParent)
                {
                    int objectId = cnob.ParentObjectId.Value;
                    NetworkObject nob = _objectCache.GetSpawnedObject(objectId);

                    if (nob == null)
                    {
                        NetworkObject prefab = prefabObjects.GetObject(false, prefabId);
                        networkManager.LogError($"NetworkObject not found for ObjectId {objectId}. Prefab {prefab.name} will be instantiated without parent synchronization.");
                    }
                    else
                    {
                        byte componentIndex = cnob.ComponentId.Value;
                        NetworkBehaviour nb = nob.GetNetworkBehaviour(componentIndex, false);
                        if (nb != null)
                        {
                            parentTransform = nb.transform;
                        }
                        else
                        {
                            NetworkObject prefab = prefabObjects.GetObject(false, prefabId);
                            networkManager.LogError($"NetworkBehaviour on index {componentIndex} could not be found within NetworkObject {nob.name} with ObjectId {objectId}. Prefab {prefab.name} will be instantiated without parent synchronization.");
                        }
                    }
                }

                ObjectPoolRetrieveOption retrieveOptions = (ObjectPoolRetrieveOption.MakeActive | ObjectPoolRetrieveOption.LocalSpace);
                result = networkManager.GetPooledInstantiated(prefabId, collectionId, retrieveOptions, parentTransform, cnob.Position, cnob.Rotation, cnob.Scale, asServer: false);

                //Only need to set IsGlobal also if not host.
                bool isGlobal = cnob.SpawnType.FastContains(SpawnType.InstantiatedGlobal);
                result.SetIsGlobal(isGlobal);
            }
            //If host then find server instantiated object.
            else
            {
                ServerObjects so = networkManager.ServerManager.Objects;
                if (!so.Spawned.TryGetValueIL2CPP(cnob.ObjectId, out result))
                    result = so.GetFromPending(cnob.ObjectId);

                if (result == null)
                    networkManager.LogWarning($"ObjectId {cnob.ObjectId} could not be found in Server spawned, nor Server pending despawn. This may occur as clientHost when objects are destroyed before the client receives a despawn packet. In most cases this may be ignored.");
            }

            return result;
        }

        /// <summary>
        /// Gets a NetworkObject from Spawned, or object cache.
        /// </summary>
        /// <param name="cnob"></param>
        /// <returns></returns>
        internal NetworkObject GetSpawnedNetworkObject(CachedNetworkObject cnob)
        {
            NetworkObject nob;
            //Try checking already spawned objects first.
            if (base.Spawned.TryGetValueIL2CPP(cnob.ObjectId, out nob))
            {
                return nob;
            }
            /* If not found in already spawned objects see if
             * the networkObject is in the objectCache. It's possible the despawn
             * came immediately or shortly after the spawn message, before
             * the object has been initialized. */
            else
            {
                nob = _objectCache.GetInCached(cnob.ObjectId, ClientObjectCache.CacheSearchType.Any);
                /* Nob may be null if it's a child object being despawned, and the
                 * parent despawn already occurred. */
                return nob;
            }
        }
    }
}