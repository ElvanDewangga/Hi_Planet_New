using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiGameManager : MonoBehaviour
{
   public static HiGameManager instance;
   public Login login;
   public DataGameManager _dataGameManager;
   public static DataGameManager dataGameManager => instance._dataGameManager;
   public ServerManager _serverManager;
   public static ServerManager serverManager => instance._serverManager;
   public Tabel_Popup _tablePopup;
   public static Tabel_Popup tablePopup => instance._tablePopup;
   public InventoryManager _inventoryManager;
   public static InventoryManager inventoryManager => instance._inventoryManager;
   public StorageManager _storageManager;
   public static StorageManager storageManager => instance._storageManager;
   public CameraMove _cameraMove;
   public static CameraMove cameraMove => instance._cameraMove;
   void Awake()
   {
      instance = this;
   }

   void Start() {
      login.InitializeLogin();
      DontDestroyOnLoad (this.gameObject);
   }


}
