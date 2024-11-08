using FishNet.Connection;
using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

public class ServerConnectionHandler : MonoBehaviour
{
    // Di sisi server, kamu bisa menggunakan OnServerConnectionState untuk mendeteksi ketika server mulai menerima koneksi dari client.

    private NetworkManager _networkManager;

    private void Awake()
    {
        // Mendapatkan referensi ke NetworkManager
        _networkManager = FindObjectOfType<NetworkManager>();
    }

    private void OnEnable()
    {
        // Mendaftarkan event ketika server connection state berubah
        if (_networkManager != null)
        {
            _networkManager.ServerManager.OnServerConnectionState += HandleServerConnectionState;
        }
    }

    private void OnDisable()
    {
        // Menghapus event ketika objek ini dimatikan
        if (_networkManager != null)
        {
            _networkManager.ServerManager.OnServerConnectionState -= HandleServerConnectionState;
        }
    }

    private void HandleServerConnectionState(ServerConnectionStateArgs args)
    {
        // Mengecek apakah server sudah mulai
        if (args.ConnectionState == LocalConnectionState.Started)
        {
            Debug.Log("Server is now running.");
          //  On_Test_Sample ();
        }
        else if (args.ConnectionState == LocalConnectionState.Stopped)
        {
            Debug.Log("Server has stopped.");
        }
    }

    #region Test_Sample
        [SerializeField]
        GameObject [] A_Char_Spawn;
        
        void On_Test_Sample () {
            foreach (GameObject s in A_Char_Spawn) {
                s.gameObject.SetActive (true);
            }
        }
    #endregion
}

