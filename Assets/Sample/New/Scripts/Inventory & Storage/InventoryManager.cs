using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Item_Ability;
public class InventoryManager : MonoBehaviour 
{
    [SerializeField]
    private Canvas inventoryCanvas;
    [SerializeField]
    private GameObject inventoryTable;
    [SerializeField]
    private DataItemInput itemInputSample;
    [SerializeField]
    private Transform itemInputContainer;
    public enum InventoryType 
    {
        Inventory,
        Storage
    }
    public InventoryType inventoryType = InventoryType.Inventory; // Tambahkan ini

    public ItemDetail itemDetail;
    
    private int maxSlot = 10;
    public string[] itemIds;
    public int[] itemQuantities;
    public string[] itemStatuses;
    public DataItemInput[] dataItemInputs;
    private bool isInventoryUpdated = false;
    public Sprite EmptySlot;
    // Tampilkan Inventory
    // Button
    public void ShowInventoryAndEquipmentSetup () {
         EquipmentManager.Instance.On_Char_Data_Equipment ();
         itemDetail.On_Direct_Code_Detail ("Equipment_Setup");
         ShowInventory ();
    }

    // StorageManager
    public virtual void ShowInventory() 
    {
        inventoryTable.SetActive(true);
        CheckInventoryChanges();
        Display();
        
    }

    // Sembunyikan Inventory
    public virtual void HideInventory() 
    {
        itemDetail.Off_Direct_Code_Detail ();
        inventoryTable.SetActive(false);
        Hide();
        SaveInventory();
    }

    // Mendapatkan Data Inventory
    public virtual void LoadInventoryData(int maxSlots, string[] ids, string[] quantities) 
    {
        maxSlot = maxSlots;
        itemIds = ids;
        itemQuantities = UtilityFunctions.ConvertStringArrayToIntArray(quantities);
        itemStatuses = quantities;
        
        dataItemInputs = new DataItemInput[maxSlot];
        for (int i = 0; i < maxSlot; i++) 
        {
            if (i < itemIds.Length) 
            {
                GameObject instance = Instantiate(itemInputSample.gameObject);
                instance.transform.SetParent(itemInputContainer);
                DataItemInput dataInput = instance.GetComponent<DataItemInput>();
                dataItemInputs[i] = dataInput;
                if (itemStatuses[i].Contains("(")) 
                {
                    dataInput.SetEquipmentStatus(itemIds[i], itemStatuses[i]);
                } 
                else 
                {
                    dataInput.SetData(itemIds[i], itemQuantities[i]);
                }
                dataInput.SetSlotPanel(i);
                instance.SetActive(true);
            } 
            else 
            {
                GameObject instance = Instantiate(itemInputSample.gameObject);
                instance.transform.SetParent(itemInputContainer);
                dataItemInputs[i] = null;
                instance.SetActive(true);
            }
        }
        isInventoryUpdated = false;
    }

    // Menambah Item ke Inventory
    public virtual void AddItem(List<string> itemNames, List<int> quantities) 
    {
        for (int i = 0; i < itemNames.Count; i++)
        {
            string itemId = GetItemIdFromName(itemNames[i]);
            int itemQuantity = quantities[i];
            int index = System.Array.IndexOf(itemIds, itemId);

            if (index >= 0)
            {
                itemQuantities[index] += itemQuantity;
            }
            else 
            {
                AddNewItem(itemId, itemQuantity);
            }
        }
        isInventoryUpdated = true;
        SaveInventory();
    }

    private void AddNewItem(string newItem, int newQuantity)
    {
        var updatedItemIds = new List<string>(itemIds) { newItem };
        var updatedQuantities = new List<int>(itemQuantities) { newQuantity };

        itemIds = updatedItemIds.ToArray();
        itemQuantities = updatedQuantities.ToArray();
    }

    // Menghapus Item dari Inventory
    public virtual void RemoveItem(List<string> itemNames, List<int> quantities) 
    {
        for (int i = 0; i < itemNames.Count; i++)
        {
            string itemId = GetItemIdFromName(itemNames[i]);
            int quantity = quantities[i];
            int index = System.Array.IndexOf(itemIds, itemId);

            if (index >= 0)
            {
                itemQuantities[index] += quantity;
                if (itemQuantities[index] <= 0)
                {
                    RemoveItemAt(index);
                }
            }
        }
        isInventoryUpdated = true;
        SaveInventory();
    }

    private void RemoveItemAt(int index)
    {
        var updatedItemIds = new List<string>(itemIds);
        var updatedQuantities = new List<int>(itemQuantities);
        
        updatedItemIds.RemoveAt(index);
        updatedQuantities.RemoveAt(index);

        itemIds = updatedItemIds.ToArray();
        itemQuantities = updatedQuantities.ToArray();
    }

    // Item_Detail_Source
    public void RefreshPage () {
        CheckInventoryChanges ();
        Hide ();
        Display ();
    }

    void CheckInventoryChanges() 
    {
        if (isInventoryUpdated) 
        {
            RefreshInventoryItems();
            Convert_Item_Effect ci = new Convert_Item_Effect();
            LoadInventoryData(maxSlot, itemIds, ci.Combine_A_Int_Own_And_A_String_Status(itemQuantities, itemStatuses));
        }
    }

    IEnumerator CheckInventoryChangesnumerator () {
        yield return new WaitForSeconds (0.1f);
        
    }

    // ItemDetail :
    public void RefreshInventoryItems() 
    {
        foreach (DataItemInput item in dataItemInputs) 
        {
            if (item != null) 
            {
                Destroy(item.gameObject);
            }
        }
        dataItemInputs = new DataItemInput[0];
    }

    #region Source
       
        [SerializeField]
        private Panel_Popup_Circle_V1 popupCirclePanelPrefab;
        [SerializeField]
        private Transform inventoryPanelTransform;
        [SerializeField]
        private Transform tabInventory;

        private GameObject activePopupCirclePanel;
        private bool isDisplayActive = false;

        // Menampilkan Popup Inventory
        public virtual void Display() 
        {

            
                HiGameManager.tablePopup.On_Set_Tabel_Popup(tabInventory.gameObject, inventoryType.ToString (), inventoryTable.transform);
                activePopupCirclePanel = Instantiate(popupCirclePanelPrefab.gameObject, inventoryPanelTransform);
                activePopupCirclePanel.SetActive(true);
                activePopupCirclePanel.GetComponent<Panel_Popup_Circle_V1>().On_Line_Popup_Circle_V1(inventoryType.ToString ());
                isDisplayActive = true;
            
        }

        // Mengatur kembali sistem sesuai inventori
        public void Hide() 
        {
            
            HideInventoryPanel ();
        }

        // Menyembunyikan Popup Inventory
        public virtual void HideInventoryPanel() 
        {
            if (activePopupCirclePanel != null) 
            {
                Destroy(activePopupCirclePanel);
                activePopupCirclePanel = null;
                itemDetail.Off_Display();
                isDisplayActive = false;
            }
        }

        // Mendapatkan ID Item berdasarkan Nama
        public string GetItemIdFromName(string itemName) 
        {
            return ItemInputManager.instance.On_Get_Data_Item_Input_From_Name(itemName).Id;
        }

        // Menyimpan Data Inventory Berdasarkan Kode
        public virtual void SaveInventory() 
        {
            HiGameManager.dataGameManager._DataGameInventory.StartSave ();
        }
    

    #endregion
}
