using UnityEngine;

public class StorageManager : InventoryManager
{
    private InventoryManager mainInventoryManager; // Referensi ke inventory utama jika diperlukan

    void Awake () {
        inventoryType = InventoryType.Storage;
    }
    
    public override void ShowInventory() 
    {
        if (!mainInventoryManager) {mainInventoryManager = HiGameManager.inventoryManager;}
        base.ShowInventory();
        mainInventoryManager?.ShowInventory(); // Pastikan inventory utama ditampilkan jika ada
    }

    public override void HideInventory() 
    {
        base.HideInventory();
    }

    public override void SaveInventory()
    {
        base.SaveInventory(); 
        // Tambahkan logika tambahan jika penyimpanan khusus untuk Storage dibutuhkan
    }
}