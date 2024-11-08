using UnityEngine;

public class Data_Game_Storage : DataGameBase {
    public override void StartLoad(string id) {
        base.StartLoad(id);
        string[] hostServerFields = { "Id", "Table" };
        string[] hostServerValues = { id, "Db_Storage" };
        HiGameManager.serverManager.SendToHostServer("Load_Db_Deck", "Read_All_Table_1", hostServerFields, hostServerValues, HandleLoadStatus);
    }

    public override void FinishLoad() {
        base.FinishLoad();
        SendDataToStorage(LoadStatusResult);
    }

    #region Storage Data Handling
        // Data_Game_Storage
        public void SendDataToStorage(string[] result) 
        {
            if (int.TryParse(result[2], out int maxSlot)) 
            {
                var storageData = HiGameManager.dataGameManager.ConvertResultToStringArrays(result, 3, 4);
                HiGameManager.storageManager.LoadInventoryData(maxSlot, storageData.Item1, storageData.Item2);
            }
        }
    #endregion
}
