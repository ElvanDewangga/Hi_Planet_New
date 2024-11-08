using System;
using System.Reflection;
using UnityEngine;

public class Char_Equipment : MonoBehaviour
{
    [SerializeField] private Char_Utama _Char_Utama;

    [SerializeField] private Char_Status _Char_Status;

    public Data_Item_Input _Data_Item_Input_Helmet;
    public Data_Item_Input _Data_Item_Input_Armor;
    public Data_Item_Input _Data_Item_Input_Drone;
    public Data_Item_Input _Data_Item_Input_Wing;
    public Data_Item_Input _Data_Item_Input_Intelligence_Cube;

    #region Character Display Accesories

    private void On_Refresh_All_Equiment_Display_Accesories()
    {
        _Char_Utama._Char_Body_Component.On_Set_Char_Accesories(_Data_Item_Input_Helmet);
        _Char_Utama._Char_Body_Component.On_Set_Char_Accesories(_Data_Item_Input_Armor);
        _Char_Utama._Char_Body_Component.On_Set_Char_Accesories(_Data_Item_Input_Drone);
        _Char_Utama._Char_Body_Component.On_Set_Char_Accesories(_Data_Item_Input_Wing);
        _Char_Utama._Char_Body_Component.On_Set_Char_Accesories(_Data_Item_Input_Intelligence_Cube);
    }

    #endregion

    #region Char_Data_Equipment

    public Data_Item_Input On_Get_Equipment_Data_Item_Input(string v)
    {
        if (v == "Helmet")
            return _Data_Item_Input_Helmet;
        if (v == "Armor")
            return _Data_Item_Input_Armor;
        if (v == "Drone")
            return _Data_Item_Input_Drone;
        if (v == "Wing")
            return _Data_Item_Input_Wing;
        if (v == "Intelligence_Cube")
            return _Data_Item_Input_Intelligence_Cube;
        return null;
    }

    public int Helmet_Id, Armor_Id, Drone_Id, Wing_Id, Intelligence_Cube_Id;
    private int Target_Id;

    private void On_Set_Equipment_Id(string v, int value)
    {
        // MyClass obj = new MyClass();
        // Mengambil tipe dari objek
        var type = GetType();

        // Mengambil field dengan nama "myField"
        var fieldInfo = type.GetField(v, BindingFlags.Public | BindingFlags.Instance);

        if (fieldInfo != null)
            // Mengatur nilai baru untuk field
            fieldInfo.SetValue(this, value);
        // Verifikasi perubahan
        // object fieldValue = fieldInfo.GetValue(this);
        // Console.WriteLine("Nilai baru dari myField: " + fieldValue);
    }


    private void On_Get_Equpiment_Id(string v)
    {
        // MyClass obj = new MyClass();
        // Mengambil tipe dari objek
        var type = GetType();

        // Mengambil field dengan nama "myField"
        var fieldInfo = type.GetField(v, BindingFlags.Public | BindingFlags.Instance);

        if (fieldInfo != null)
        {
            // Verifikasi perubahan
            var fieldValue = fieldInfo.GetValue(this);
            Target_Id = Convert.ToInt32(fieldInfo.GetValue(fieldValue));
            // Console.WriteLine("Nilai baru dari myField: " + fieldValue);
        }
    }

    #endregion

    #region Char_Utama

    // Pertama kali load.
    public void On_Get_Equipment_Id(int Helmet_V, int Armor_V, int Drone_V, int Wing_V, int Intelligence_Cube_V,
        int Fire_V, int Water_V, int Wood_V, int Metal_V, int Stone_V)
    {
        var Cs = _Char_Utama._Char_Utama_Source;
        if (Helmet_V >= 0) Cs.On_Get_Data_Item_Input_Inventory(Helmet_V, _Data_Item_Input_Helmet);
        // if (Armor_V >=0) {_Data_Item_Input_Armor = Cs.On_Get_Data_Item_Input_Inventory (Armor_V);}
        //  if (Drone_V >=0) {_Data_Item_Input_Drone = Cs.On_Get_Data_Item_Input_Inventory (Drone_V);}
        //  if (Wing_V >=0) {_Data_Item_Input_Wing = Cs.On_Get_Data_Item_Input_Inventory (Wing_V);}
        //  if (Intelligence_Cube_V >=0) {_Data_Item_Input_Intelligence_Cube = Cs.On_Get_Data_Item_Input_Inventory (Intelligence_Cube_V);}
        On_Set_Equipment_Id("Helmet", Helmet_V);
        On_Set_Equipment_Id("Armor", Armor_V);
        On_Set_Equipment_Id("Drone", Drone_V);
        On_Set_Equipment_Id("Wing", Wing_V);
        On_Set_Equipment_Id("Intelligence_Cube", Intelligence_Cube_V);
        /*
        if (Helmet_V >=0) {_Data_Item_Input_Helmet = Cs.On_Get_Data_Item_Input_Inventory (Helmet_V);}
        if (Helmet_V >=0) {_Data_Item_Input_Helmet = Cs.On_Get_Data_Item_Input_Inventory (Helmet_V);}
        if (Helmet_V >=0) {_Data_Item_Input_Helmet = Cs.On_Get_Data_Item_Input_Inventory (Helmet_V);}
        if (Helmet_V >=0) {_Data_Item_Input_Helmet = Cs.On_Get_Data_Item_Input_Inventory (Helmet_V);}
        if (Helmet_V >=0) {_Data_Item_Input_Helmet = Cs.On_Get_Data_Item_Input_Inventory (Helmet_V);}
        */

        On_Refresh_Status_Equipment(_Data_Item_Input_Helmet);
        On_Refresh_Status_Equipment(_Data_Item_Input_Armor);
        On_Refresh_Status_Equipment(_Data_Item_Input_Drone);
        On_Refresh_Status_Equipment(_Data_Item_Input_Wing);
        On_Refresh_Status_Equipment(_Data_Item_Input_Intelligence_Cube);

        On_Refresh_All_Equiment_Display_Accesories();
    }

    private void On_Refresh_Status_Equipment(Data_Item_Input Ds)
    {
        foreach (var Ie in Ds.L_Data_Item_Effect)
            _Char_Status.On_Char_Equipment_Value("Equip", Ie.Code_Effect[0], Ie.Code_Value[0]);
        _Char_Status.On_Restore();
    }

    #endregion

    #region Char_Data_Equipment

    [SerializeField] private Data_Item_Input Item_Inventory;

    public void On_Equip(int Inventory_Item_Id, string Slot_Equipment_Type)
    {
        Target_Id = Inventory_Item_Id;
        var Di = On_Get_Equipment_Data_Item_Input(Slot_Equipment_Type);
        var Cs = _Char_Utama._Char_Utama_Source;
        // Di = Cs.On_Get_Data_Item_Input_Inventory (Inventory_Item_Id);

        Cs.On_Get_Data_Item_Input_Inventory(Inventory_Item_Id, Item_Inventory);
        Cs.On_Transfer_Data_Item_Input(Item_Inventory, Di);
        foreach (var ie in Di.L_Data_Item_Effect)
        {
            Debug.Log(ie);
            _Char_Utama._Char_Status.On_Char_Equipment_Value("Equip", ie.Code_Effect[0], ie.Code_Value[0]);
        }

        On_Refresh_All_Equiment_Display_Accesories();
    }

    public void On_Unequip(string Slot_Equipment_Type)
    {
        var Di = On_Get_Equipment_Data_Item_Input(Slot_Equipment_Type);
        var Cs = _Char_Utama._Char_Utama_Source;

        On_Get_Equpiment_Id(Slot_Equipment_Type);
        // Di.On_Transfer_Data_Item_Input (Di, Cs.On_Get_Data_Item_Input_Inventory (Target_Id));
        // Cs.On_Get_Data_Item_Input_Inventory (Inventory_Item_Id, Item_Inventory);

        foreach (var ie in Di.L_Data_Item_Effect)
            _Char_Utama._Char_Status.On_Char_Equipment_Value("Unequip", ie.Code_Effect[0], ie.Code_Value[0]);

        Cs.On_Refresh_Data_Item_Input(Item_Inventory);
        Cs.On_Transfer_Data_Item_Input(Item_Inventory, Di);

        _Char_Utama._Char_Body_Component.On_Unequip_Char_Accesories(Slot_Equipment_Type);
        // On_Refresh_All_Equiment_Display_Accesories ();
    }

    #endregion
}