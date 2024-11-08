using FishNet.Connection;
using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

public class ServerClientDisconnectHandler : MonoBehaviour
{
    // Untuk mendeteksi ketika client terputus dari server
   private NetworkManager _networkManager;

    private void Awake()
    {
        // Mendapatkan referensi ke NetworkManager
        _networkManager = FindObjectOfType<NetworkManager>();
    }

    private void OnEnable()
    {
        // Mendaftarkan event ketika status koneksi client berubah
        if (_networkManager != null)
        {
            _networkManager.ServerManager.OnRemoteConnectionState += HandleClientConnectionState;
        }
    }

    private void OnDisable()
    {
        // Menghapus event ketika objek ini dimatikan
        if (_networkManager != null)
        {
            _networkManager.ServerManager.OnRemoteConnectionState -= HandleClientConnectionState;
        }
    }

    private void HandleClientConnectionState(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        // Mengecek apakah client sudah terputus
        if (args.ConnectionState == RemoteConnectionState.Stopped)
        {
            Debug.Log($"Client with ID {conn.ClientId} has disconnected.");
            
            // Lakukan clean-up di sini jika diperlukan
            // Contoh: DespawnPlayer(conn);
        }
    }
}
