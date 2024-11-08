using System.Collections;
using UnityEngine;
using Item_Ability;

public class Data_Game_Inventory : DataGameBase
{
    public InventoryManager Save_Inventory;
    public InventoryManager Save_Storage;

    private enum DatabaseTables
    {
        Inventory,
        Storage
    }

    public override void StartSave()
    {
        base.StartSave();

        var hostServerFields = PrepareHostServerFields();
        var hostServerValues = PrepareHostServerValues();

        HiGameManager.serverManager.SendToHostServer("Save_Status", "Write_All_Table_Value_Fix", hostServerFields, hostServerValues, HandleLoadStatus);
    }

    private string[] PrepareHostServerFields()
    {
        return new[]
        {
            "Id", "table_1", "title_1", "value_1", "table_2", "title_2", "value_2",
            "table_3", "title_3", "value_3", "table_4", "title_4", "value_4"
        };
    }

    private string[] PrepareHostServerValues()
    {
        Convert_Item_Effect ci = new Convert_Item_Effect();
        var fungsiUmum = Fungsi_Umum.Ins._A_String_Umum;

        return new[]
        {
            HiGameManager.instance._dataGameManager.id,
            "Db_Inventory",
            "Slot_1",
            fungsiUmum.On_A_String_To_String(Save_Inventory.itemIds),
            "Db_Inventory",
            "Own_1",
            fungsiUmum.On_A_String_To_String(ci.Combine_A_Int_Own_And_A_String_Status(Save_Inventory.itemQuantities, Save_Inventory.itemStatuses)),
            "Db_Storage",
            "Slot_1",
            fungsiUmum.On_A_String_To_String(Save_Storage.itemIds),
            "Db_Storage",
            "Own_1",
            fungsiUmum.On_A_String_To_String(ci.Combine_A_Int_Own_And_A_String_Status(Save_Storage.itemQuantities, Save_Storage.itemStatuses))
        };
    }

    public override void FinishSave()
    {
        base.FinishSave();
    }

    public override void StartLoad(string id)
    {
        base.StartLoad(id);
         string[] hostServerFields = { "Id", "Table" };
        string[] hostServerValues = { id, "Db_Inventory" };
        HiGameManager.serverManager.SendToHostServer("Load_Db_Deck", "Read_All_Table_1", hostServerFields, hostServerValues, HandleLoadStatus);
    }

    public override void FinishLoad()
    {
        base.FinishLoad();
        SendDataToInventory(LoadStatusResult);
    }

    #region Inventory Data Handling
        // Data_Game_Inventory
        public void SendDataToInventory(string[] result) 
        {
            if (int.TryParse(result[2], out int maxSlot)) 
            {
                var inventoryData = HiGameManager.dataGameManager.ConvertResultToStringArrays(result, 3, 4);
                HiGameManager.inventoryManager.LoadInventoryData(maxSlot, inventoryData.Item1, inventoryData.Item2);
            }
        }
    #endregion

}