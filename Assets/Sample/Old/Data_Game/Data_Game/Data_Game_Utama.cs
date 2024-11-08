using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Game_Utama : MonoBehaviour {
    public static Data_Game_Utama Ins;
    void Awake () {
        Ins = this;
    }
    public Data_Game_Account _Data_Game_Account;
    public Data_Game_Player _Data_Game_Player;
    public Data_Game_Source _Data_Game_Source;
    public Data_Game_Inventory _Data_Game_Inventory;
    public Data_Game_Equipment _Data_Game_Equipment;
    public Data_Game_Storage _Data_Game_Storage;
}
