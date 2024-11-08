using FishNet.Object;
using UnityEngine;

// [RequireComponent(typeof(NetworkTransform))]
public class Player_Sync_Position : NetworkBehaviour
{
    [SerializeField] private Vector3 _lastSentPosition;
    public bool b_Owner;

    private void Update()
    {
        b_Owner = IsOwner;
        // Jika objek ini milik lokal (pemain sendiri), kirimkan posisi ke server
        if (IsOwner)
        {
            // Kirim posisi jika jaraknya cukup berbeda dari posisi terakhir yang dikirim
            if (Vector3.Distance(transform.position, _lastSentPosition) > 0.1f)
            {
                _lastSentPosition = transform.position;
                SendPosition(transform.position);
            }
        }
        else
        {
            transform.position = _lastSentPosition;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendPosition(Vector3 position)
    {
        // Debug.Log($"Sending position {position} to observers.");
        // Kirim posisi ke semua observer
        SendPositionToObservers(position);
    }

    [ObserversRpc]
    private void SendPositionToObservers(Vector3 position)
    {
        // Sinkronisasi posisi di client lain
        if (!IsOwner)
            //   Debug.Log($"Received position {position} on observer.");
            _lastSentPosition = position;
    }
}