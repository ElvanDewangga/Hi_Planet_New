using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

using System.Collections.Generic;
using Item_Ability;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.Serialization;

#if UNITY_EDITOR
[CustomEditor(typeof(Item_Input))]
#endif
public class ItemInputManager : MonoBehaviour {
    [FormerlySerializedAs("_A_Item_Input")] [SerializeField]
    Transform itemInputManager;

    void Awake () {
        instance = this;
    }

    public static ItemInputManager instance;

    #region Data_Item_Input
    public Item_Input On_Get_Item_Input (string Id_V) {
        int.TryParse (Id_V, out int Id);
        // -1 karena tidak ada Id = 0.
        return itemInputManager.GetChild (Id).GetComponent <Data_Item_Input> ()._Item_Input; 
        
    }
    // Inventory - Inventory_Source :
    public Data_Item_Input On_Get_Data_Item_Input_From_Name (string Name_V) {
        Data_Item_Input Di = null;
        foreach (Transform S in itemInputManager.transform) {
            Data_Item_Input Ip = S.GetComponent <Data_Item_Input> ();
            if (Ip._Item_Input.Name == Name_V) {
                Di = Ip;
                break;
            }
        }
        return Di;
    }
    #endregion
    #region Item_Setup
    public List<Item_Setup> Items = new List<Item_Setup>();

    void Start () {
        LoadItems();
    }
    

    private void LoadItems()
    {
         Items.Clear();
        Addressables.LoadAssetsAsync<Item_Setup>("ItemSetup", item => {
            if (item != null) {
                Items.Add(item);
            }
        }).Completed += handle => {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                On_Create_Object();
            }
        };
    }
    [SerializeField]
    GameObject GO_Item;
    void On_Create_Object () {
        foreach (Item_Setup I in Items) {
            GameObject Ins = GameObject.Instantiate (GO_Item);
            Ins.transform.SetParent (itemInputManager);
            Ins.GetComponent<Data_Item_Input> ().On_Set_Item_Setup (I);
            Ins.name = I._Item_Input.Name + " (Clone)";
            int.TryParse (I.Id, out int Ix);
            Ins.transform.SetSiblingIndex(Ix);
        }
    }

    /*
    private void LoadSprites()
    {
        Sprites.Clear();
        // Mendapatkan semua asset sprite di folder "Assets/Assets/Assets/Item_Setup/Item"
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Assets/Assets/Item_Setup/Item" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            // Mengambil semua representasi asset dalam path tersebut
            Object[] assets = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);
            foreach (Object asset in assets)
            {
                if (asset is Sprite sprite)
                {
                    Sprites.Add(sprite);
                }
            }
        }
    }
    */
    #endregion
}

