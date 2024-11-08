using FishNet.Connection;
using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

public class ServerClientConnectHandler : MonoBehaviour
{
    /*
    Jika kamu ingin mendeteksi ketika client baru terhubung ke server, gunakan event OnRemoteClientConnected di ServerManager. 
    Ini sangat berguna untuk melakukan tindakan tertentu (seperti spawn player) ketika client berhasil terhubung ke server.
    */
    private NetworkManager _networkManager;

    private void Awake()
    {
        // Mendapatkan referensi ke NetworkManager
        _networkManager = FindObjectOfType<NetworkManager>();
    }

    private void OnEnable()
    {
        // Mendaftarkan event ketika client baru terhubung
        if (_networkManager != null)
        {
            _networkManager.ServerManager.OnRemoteConnectionState += OnClientConnectionStateChange;
        }
    }

    private void OnDisable()
    {
        // Menghapus event ketika objek ini dimatikan
        if (_networkManager != null)
        {
            _networkManager.ServerManager.OnRemoteConnectionState -= OnClientConnectionStateChange;
        }
    }

    private void OnClientConnectionStateChange(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        // Mengecek apakah client baru terhubung
        if (args.ConnectionState == RemoteConnectionState.Started)
        {
            Debug.Log($"Client with ID {conn.ClientId} has connected.");
            
            // Kamu bisa melakukan spawn player di sini
            // Example: SpawnPlayer(conn);
        }
    }
}

