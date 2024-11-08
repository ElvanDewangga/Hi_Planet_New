using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;
    void Awake () {
        Instance = this;
    }
    [SerializeField] private Canvas Char_Data_Equipment_Canvas;

    [SerializeField] private Image Bg_Char_Data_Equipment;

    [SerializeField] private Image Layout_Equipment_Setup;

    // button, InventoryManager :
    public void On_Char_Data_Equipment()
    {
        Char_Data_Equipment_Canvas.gameObject.SetActive(true);

        Bg_Char_Data_Equipment.gameObject.SetActive(true);
        HiGameManager.tablePopup.On_Set_Tabel_Popup(Bg_Char_Data_Equipment.gameObject, "Equipment_Setup", Layout_Equipment_Setup.transform);
        On_Refresh_Tab_Status();
        On_Refresh_Equipment_Setup(AccountManager.instance.player);
    }

    // Tabel_Popup :
    public void Off_Char_Data_Equipment()
    {
        Char_Data_Equipment_Canvas.gameObject.SetActive(false);
        Off_Perubahan_Status();
        Bg_Char_Data_Equipment.gameObject.SetActive(false);
       // Char_Data.Ins.Off_Char_Data_Equipment();
    }

    #region Tab_Status

    [SerializeField] private TMP_Text Life_Tx, Attack_Tx, Defense_Tx, Speed_Tx, Intelligence_Tx;

    [SerializeField] private TMP_Text Player_Tx;

    private void On_Refresh_Tab_Status()
    {
        if (Perubahan_Life == 0)
            Life_Tx.text = "Life : " + AccountManager.instance.player.life;
        else if (Perubahan_Life > 0)
            Life_Tx.text = "Life : " + AccountManager.instance.player.life + "<color=green> + (" +
                           Perubahan_Life + ") </color>";
        else if (Perubahan_Life < 0)
            Life_Tx.text = "Life : " + AccountManager.instance.player.life + "<color=red> - (" +
                           Perubahan_Life + ") </color>";
        Attack_Tx.text = "Attack : " + AccountManager.instance.player.attack;
        Defense_Tx.text = "Defense : " + AccountManager.instance.player.defense;
        Speed_Tx.text = "Speed : " + AccountManager.instance.player.speed;
        Intelligence_Tx.text = "Intelligence : " + AccountManager.instance.player.intelligence;
        Player_Tx.text = AccountManager.instance.player.Username;
    }

    #endregion

    #region Item_Detail_Source

    private int Perubahan_Life, Perubahan_Attack, Perubahan_Defense, Perubahan_Speed, Perubahan_Intelligence;
    private DataItemInput Select_Data_Item_Input;

    public void On_Perubahan_Status(DataItemInput Div_Input)
    {
        Off_Perubahan_Status();
        Select_Data_Item_Input = Div_Input;

        foreach (var Ie in Div_Input.L_Data_Item_Effect)
            if (Ie.Code_Effect[0] == "Attack")
                Perubahan_Attack += Ie.Code_Value[0];
            else if (Ie.Code_Effect[0] == "Defense")
                Perubahan_Defense += Ie.Code_Value[0];
            else if (Ie.Code_Effect[0] == "Speed")
                Perubahan_Speed += Ie.Code_Value[0];
            else if (Ie.Code_Effect[0] == "Life")
                Perubahan_Life += Ie.Code_Value[0];
            else if (Ie.Code_Effect[0] == "Intelligence") Perubahan_Intelligence += Ie.Code_Value[0];

        var Di =
            AccountManager.instance.player.GetEquipmentDataItem(Div_Input._Item_Input
                .Type);
        foreach (var Ie in Di.L_Data_Item_Effect)
            if (Ie.Code_Effect[0] == "Attack")
                Perubahan_Attack -= Ie.Code_Value[0];
            else if (Ie.Code_Effect[0] == "Defense")
                Perubahan_Defense -= Ie.Code_Value[0];
            else if (Ie.Code_Effect[0] == "Speed")
                Perubahan_Speed -= Ie.Code_Value[0];
            else if (Ie.Code_Effect[0] == "Life")
                Perubahan_Life -= Ie.Code_Value[0];
            else if (Ie.Code_Effect[0] == "Intelligence") Perubahan_Intelligence -= Ie.Code_Value[0];
        On_Refresh_Tab_Status();
    }

    // Item_Detail_Source:
    public void Off_Perubahan_Status()
    {
        Perubahan_Life = 0;
        Perubahan_Attack = 0;
        Perubahan_Defense = 0;
        Perubahan_Speed = 0;
        Perubahan_Intelligence = 0;
        On_Refresh_Tab_Status();
    }

    #endregion

    #region Equip

    // Item_Detail_Source
    public void On_Equip(int Inventory_Item_Id, string Slot_Equipment_Type)
    {
        // Slot_Equipment_Type : "Drone", "Helmet", dll.
        var Host_Server_Value = new string [4]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
        var Host_Server_Field = new string [4];
        Host_Server_Field[0] = "Id";
        Host_Server_Value[0] = Data_Game_Utama.Ins._Data_Game_Account.Id;
        Host_Server_Field[1] = "table_1";
        Host_Server_Value[1] = "Db_Equipment";
        Host_Server_Field[2] = "title_1";
        Host_Server_Value[2] = Slot_Equipment_Type;
        Host_Server_Field[3] = "value_1";
        Host_Server_Value[3] = Inventory_Item_Id.ToString();
        Data_Game_Utama.Ins._Data_Game_Equipment.StartSave(Host_Server_Field, Host_Server_Value);

        var Di = AccountManager.instance.player.GetEquipmentDataItem(Select_Data_Item_Input._Item_Input.Type);
        if (Di.Id == "")
        {
            // Slot Kosong
            AccountManager.instance.player.EquipItem(Inventory_Item_Id, Slot_Equipment_Type);
        }
        else
        {
            // Slot Isi
            AccountManager.instance.player.UnequipItem(Slot_Equipment_Type);
            AccountManager.instance.player.EquipItem(Inventory_Item_Id, Slot_Equipment_Type);
        }

        On_Refresh_Tab_Status();
        On_Refresh_Equipment_Setup(AccountManager.instance.player);
    }

    public void On_Unequip(string Slot_Equipment_Type)
    {
        // Slot_Equipment_Type : "Drone", "Helmet", dll.
        var Host_Server_Value = new string [4]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
        var Host_Server_Field = new string [4];
        Host_Server_Field[0] = "Id";
        Host_Server_Value[0] = Data_Game_Utama.Ins._Data_Game_Account.Id;
        Host_Server_Field[1] = "table_1";
        Host_Server_Value[1] = "Db_Equipment";
        Host_Server_Field[2] = "title_1";
        Host_Server_Value[2] = Slot_Equipment_Type;
        Host_Server_Field[3] = "value_1";
        Host_Server_Value[3] = "-1";
        Data_Game_Utama.Ins._Data_Game_Equipment.StartSave(Host_Server_Field, Host_Server_Value);

        AccountManager.instance.player.UnequipItem(Slot_Equipment_Type);

        On_Refresh_Tab_Status();
        On_Refresh_Equipment_Setup(AccountManager.instance.player);
    }

    #endregion

    #region Equipment_Setup

    [SerializeField] private GI_V2_Button[] A_Equipment_Setup_GI_Button = new GI_V2_Button[0];

    private void On_Refresh_Equipment_Setup(PlayerCharBase playerCharBase)
    {
        A_Equipment_Setup_GI_Button[0]
            .On_Input_Data("Equipment_Setup", "Equipment_Setup", playerCharBase.dataItemHelmet.gameObject);
    }

    #endregion
}