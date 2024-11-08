using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon_Settings_Scene : MonoBehaviour
{
   [SerializeField]
   Dungeon_Sub [] A_Dungeon_Sub;
    void Awake () {
        Ins = this;
    }

    public static Dungeon_Settings_Scene Ins;
   #region Network_Go
   public Char_Spawn First_Char_Spawn;
   
    Dungeon_Sub On_Get_Dungeon_Sub (string Name_V) {
        Dungeon_Sub Res = null;
        foreach (Dungeon_Sub As in A_Dungeon_Sub) {
            if (As.DungeonSubName == Name_V) {
                Res = As;
            }
        } 
        return Res;
    }

    Dungeon_Sub _Dungeon_Sub;
    public void On_Start_Dungeon_Sub (string vl) {
        _Dungeon_Sub = On_Get_Dungeon_Sub (vl);
        
        _Dungeon_Sub.StartDungeon ();
    }
   #endregion


}
