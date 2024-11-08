using UnityEngine;

public class Data_Game_Equipment : DataGameBase {
    public override void StartSave(string[] hostServerFields, string[] hostServerValues) {
        base.StartSave(hostServerFields, hostServerValues);
        HiGameManager.serverManager.SendToHostServer("Save_Status", "Write_All_Table_Value_Fix", hostServerFields, hostServerValues, HandleLoadStatus); 
    }

    public override void StartLoad(string id) {
        base.StartLoad(id);
        string[] hostServerFields = { "Id", "Table" };
        string[] hostServerValues = { id, "Db_Equipment" };
        HiGameManager.serverManager.SendToHostServer("Load_Db_Deck", "Read_All_Table_1", hostServerFields, hostServerValues, HandleLoadStatus);
    }

    public override void FinishLoad() {
        base.FinishLoad();
        SendDataToEquipment(LoadStatusResult);
    }

    #region Equipment Data Handling
        public string [] equipmentResult;
        // Data_Game_Equipment
        public void SendDataToEquipment(string[] result) 
        {
            equipmentResult = result;
        }
    #endregion
}

