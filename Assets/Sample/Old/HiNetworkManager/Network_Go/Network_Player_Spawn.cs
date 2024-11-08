using FishNet.Object;
using UnityEngine;

public class Network_Player_Spawn : NetworkBehaviour
{
    [SerializeField]
    private GameObject playerPrefab; // Prefab player dengan NetworkObject

    public override void OnStartServer()
    {
        base.OnStartServer();

        // Misal spawn player ketika server mulai
       // SpawnPlayer();
    }

    /*
    private void SpawnPlayer()
    {
        // Pastikan hanya server yang bisa melakukan spawning
        if (IsServerInitialized)
        {
            // Instantiate objek player secara lokal di server
            GameObject playerInstance = Instantiate(playerPrefab, GetRandomSpawnPoint(), Quaternion.identity);

            // Pastikan objek di-spawn ke semua client
            Spawn(playerInstance);
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        // Bisa disesuaikan dengan spawn point yang kamu inginkan
        return new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
    }




    public override void OnClientConnectionState(ClientConnectionStateArgs args)
    {
        // Ketika client baru terhubung ke server
        if (args.ConnectionState == LocalConnectionState.Started)
        {
            // Spawn player untuk client tersebut di server
            SpawnPlayerForClient(args.ClientId);
        }
    }

    private void SpawnPlayerForClient(ulong clientId)
    {
        if (IsServer)
        {
            GameObject playerInstance = Instantiate(playerPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            Spawn(playerInstance, clientId); // Ini akan melakukan spawning objek ke client yang terhubung
        }
    }
    */
    #region Source
        #region Char_Spawn
        public void On_Char_Spawn (Char_Spawn Cs) {
             OnStartServer ();
            // Pastikan hanya server yang bisa melakukan spawning
            Debug.Log ("Spawning");
            if (IsServerInitialized)
            {
                // Instantiate objek player secara lokal di server
                GameObject Char_Instantitate = Instantiate(playerPrefab);

                // Pastikan objek di-spawn ke semua client
                Spawn(Char_Instantitate);
                Cs.On_Get_Spawn_Object (Char_Instantitate);
            } else {
                Debug.LogError ("Server tidak terdeteksi !");
            }
        }

        #endregion
    #endregion
}