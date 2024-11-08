using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Item_Ability;
public class Inventory : MonoBehaviour {
    [SerializeField]
    public string Code_Inventory = ""; // "Inventory" / "Storage"
    [SerializeField]
    public Canvas Inventory_Canvas;
    public GameObject Tabel_Inventory;
    public Data_Item_Input Data_Item_Input_Sample;
    [SerializeField]
    public Transform A_Data_Item_Input;

    public Inventory_Source _Inventory_Source;
    public Item_Detail _Item_Detail;

    public virtual void On_Inventory () {
      //  Inventory_Canvas.gameObject.SetActive (true);
      
        Tabel_Inventory.gameObject.SetActive (true);
        On_Check_Perubahan_Item_Inventory ();
        
        _Inventory_Source.On_Display ();
        _Inventory_Source.On_Set_Back_System ();
    }

    // Tabel_Popup_Source :
    public virtual void Off_Inventory () {
      //  Inventory_Canvas.gameObject.SetActive (false);
        Tabel_Inventory.gameObject.SetActive (false);
        _Inventory_Source.Off_Display ();
        _Inventory_Source.On_Save_Inventory (Code_Inventory);
    }
    
    #region Item_Detail
    public void On_Inventory_Canvas () {
      Inventory_Canvas.gameObject.SetActive (true);
    }
    public void Off_Inventory_Canvas () {
      Inventory_Canvas.gameObject.SetActive (false);
    }

    public void On_Refresh_Page () {
        On_Check_Perubahan_Item_Inventory ();
         _Inventory_Source.Off_Display ();
          _Inventory_Source.On_Display ();
    }
    #endregion
    #region Pengaturan
    int Max_Slot = 10;
    #endregion
    #region Data_Game_Source
    [SerializeField]
    public string [] Item_Id = new string [0];
    [SerializeField]
    public int [] Item_Own = new int [0];
    [SerializeField]
    public string [] Item_Status = new string [0];
    public virtual void On_Get_Data(int Max_Slot_V, string[] Item_Id_V, string[] Item_Own_V) {
        Max_Slot = Max_Slot_V;
        Item_Id = Item_Id_V; 
        Item_Own = Fungsi_Umum.Ins._A_String_Umum.On_A_String_To_A_Int (Item_Own_V);
        Item_Status = Item_Own_V;
        Inventory_Data_Item_Input = new Data_Item_Input[Max_Slot];
        for (int x = 0; x < Max_Slot; x++) {
            if (x < Item_Id.Length) { // Slot Isi
              
              // int.TryParse (Item_Own[x], out int Quantity_V);
              GameObject Ins = GameObject.Instantiate (Data_Item_Input_Sample.gameObject);
              Ins.transform.SetParent (A_Data_Item_Input);
              Data_Item_Input Di = Ins.GetComponent <Data_Item_Input> ();
              Inventory_Data_Item_Input[x] = Di;
              if (Item_Status[x].Contains("(")) {
                 Di.On_Get_Equipment_Status (Item_Id[x], Item_Status[x]);
                
              } else {
                Di.On_Get_Data (Item_Id[x], Item_Own[x]);
                
              }
              Di.On_Slot_Panel (x);
              Ins.SetActive (true);
            } else { // Slot kosong :
              GameObject Ins = GameObject.Instantiate (Data_Item_Input_Sample.gameObject);
              Ins.transform.SetParent (A_Data_Item_Input);
              Data_Item_Input Di = Ins.GetComponent <Data_Item_Input> ();
              Inventory_Data_Item_Input[x] = null;
            //  Di.On_Get_Data ("0", 0);
            
              Ins.SetActive (true);
            }
            
        }
        b_Perubahan_Inventory = false;
    }
    #endregion
    #region Panel_Popup_Circle_V1
    // Char_Equipment :
    public Data_Item_Input [] Inventory_Data_Item_Input;
    void On_Refresh_Data_Item_Input () {
      foreach (Data_Item_Input s in Inventory_Data_Item_Input) {
        if (s != null) {
          Destroy (s.gameObject);
        }
      }
      Inventory_Data_Item_Input = new Data_Item_Input [0];
    }

    void On_Check_Perubahan_Item_Inventory () {
      if (b_Perubahan_Inventory) {
          On_Refresh_Data_Item_Input ();
          // menggunakan Item_Id terbaru yang sudah di Add atau di Remove
          On_Get_Data (Max_Slot, Item_Id, Fungsi_Umum.Ins._A_Int_Umum.On_A_Int_To_A_String(Item_Own));
      }
    }
    #endregion
    #region Add_And_Remove_Item
    bool b_Perubahan_Inventory = false;
    
    public void Example_Add_Item () {
      List <string> L_Name = new List<string> ();
      List <int> L_Quantity = new List<int> ();
      L_Name.Add ("Orange Juice"); L_Quantity.Add (3);
      On_Add_Item (L_Name, L_Quantity);
      
    }

    public virtual void On_Add_Item (List <string> L_Name_V, List <int> L_Quantity_V) {
        
          for (int i = 0; i < L_Name_V.Count; i++)
        {
          Debug.Log ("Add " + L_Name_V[i] + " x" + L_Quantity_V[i] + "(Connect display to Dami)");
            string currentItem = _Inventory_Source.On_Get_Id_From_Data_Item_Input_From_Name(L_Name_V[i]);
            int currentItemOwn = L_Quantity_V[i];

            int index = System.Array.IndexOf(Item_Id, currentItem); // Mencari index dari item yang sama di Item_Id
            Debug.Log (index);
            if (index >= 0) // Jika item ditemukan
            {
                Item_Own[index] += currentItemOwn; // Tambahkan jumlah kepemilikan
            }
            else // Jika item tidak ditemukan
            {
                AddNewItem(currentItem, currentItemOwn); // Tambahkan item baru ke array
            }
        }
        b_Perubahan_Inventory = true;
         _Inventory_Source.On_Save_Inventory ("Save_Items");
    }

    void AddNewItem(string newItem, int newItemOwn)
    {
        // Menambah panjang array untuk menambahkan item baru
        string[] newItem_IdArray = new string[Item_Id.Length + 1];
        int[] newItem_OwnArray = new int[Item_Own.Length + 1];

        // Menyalin data yang ada ke array baru
        for (int i = 0; i < Item_Id.Length; i++)
        {
            newItem_IdArray[i] = Item_Id[i];
            newItem_OwnArray[i] = Item_Own[i];
        }

        // Menambahkan item baru di akhir array
        newItem_IdArray[Item_Id.Length] = newItem;
        newItem_OwnArray[Item_Own.Length] = newItemOwn;

        // Menugaskan array yang telah diperbarui kembali ke variabel asli
        Item_Id = newItem_IdArray;
        Item_Own = newItem_OwnArray;
        Debug.Log ("Item baru");
    }

    public void Example_Remove_Item () {
      List <string> L_Name = new List<string> ();
      List <int> L_Quantity = new List<int> ();
      L_Name.Add ("Orange Juice"); L_Quantity.Add (-10);
      On_Remove_Item (L_Name, L_Quantity);
    }

    public virtual void On_Remove_Item (List <string> L_Name_V, List <int> L_Quantity_V) {
      for (int i = 0; i < L_Name_V.Count; i++)
        {
            string currentItem = _Inventory_Source.On_Get_Id_From_Data_Item_Input_From_Name(L_Name_V[i]);
            int currentItemOwn = L_Quantity_V[i];

            int index = System.Array.IndexOf(Item_Id, currentItem); // Mencari index dari item yang sama di Item_Id

            if (index >= 0) // Jika item ditemukan
            {
                Item_Own[index] += currentItemOwn; // Mengurangi jumlah kepemilikan

                // Jika jumlah item menjadi nol atau kurang, hapus item
                if (Item_Own[index] <= 0)
                {
                    RemoveItemAt(index);
                }
            }
        }

      b_Perubahan_Inventory = true;
       _Inventory_Source.On_Save_Inventory ("Save_Items");
    }

    void RemoveItemAt(int index)
    {
        // Buat array baru dengan ukuran yang lebih kecil
        string[] newItem_IdArray = new string[Item_Id.Length - 1];
        int[] newItem_OwnArray = new int[Item_Own.Length - 1];

        // Salin elemen sebelum index yang dihapus
        for (int i = 0; i < index; i++)
        {
            newItem_IdArray[i] = Item_Id[i];
            newItem_OwnArray[i] = Item_Own[i];
        }

        // Salin elemen setelah index yang dihapus
        for (int i = index + 1; i < Item_Id.Length; i++)
        {
            newItem_IdArray[i - 1] = Item_Id[i];
            newItem_OwnArray[i - 1] = Item_Own[i];
        }

        // Memperbarui array asli dengan array yang telah diperbarui
        Item_Id = newItem_IdArray;
        Item_Own = newItem_OwnArray;
    }
    #endregion
}
