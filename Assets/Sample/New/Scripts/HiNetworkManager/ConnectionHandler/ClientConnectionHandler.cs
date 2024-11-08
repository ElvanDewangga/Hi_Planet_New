using FishNet.Connection;
using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

public class ClientConnectionHandler : MonoBehaviour
{
    /*
    Di sisi client, callback OnClientConnectionState digunakan untuk mendeteksi status koneksi pemain ke server.
    Kamu bisa menggunakannya untuk menangani logika ketika client berhasil terhubung atau terputus dari server.
    */
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
            _networkManager.ClientManager.OnClientConnectionState += HandleClientConnectionState;
        }
    }

    private void OnDisable()
    {
        // Menghapus event ketika objek ini dimatikan
        if (_networkManager != null)
        {
            _networkManager.ClientManager.OnClientConnectionState -= HandleClientConnectionState;
        }
    }

    private void HandleClientConnectionState(ClientConnectionStateArgs args)
    {
        // Mengecek apakah client sudah terhubung atau terputus
        if (args.ConnectionState == LocalConnectionState.Started)
        {
            
            Debug.Log("Client connected to the server.");
            HiNetworkManager.networkWorld.NetworkStart ();
        }
        else if (args.ConnectionState == LocalConnectionState.Stopped)
        {
            Debug.Log("Client disconnected from the server.");
        }
    }
}
