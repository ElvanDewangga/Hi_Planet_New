using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Char_Spawn : MonoBehaviour
{
    public string Code_Char = ""; // "Enemy"/ "Player" / "Client"
    public string Code_Char_Pack = ""; // "Claw_Bot"
    // Enemy_Test_Canvas :
    public void On_Set_Code_Char_Pack (string Code_Char_V, string Code_Char_Pack_V) {
        Code_Char = Code_Char_V; Code_Char_Pack = Code_Char_Pack_V;
        On_Spawn ();
    }
    
    // Start is called before the first frame update
    void Start() {
        if (Code_Char_Pack != "") {
            if (Code_Char == "Player") {
                if (!Char_Data.Ins.Your_Char_Utama_Script) {
                    On_Spawn ();
                }
            } else if (Code_Char == "Enemy") {
                
            }
        }
    }

    // this, Dungeon_Sub :
    public void On_Spawn () {
        /*
        GameObject Ins = GameObject.Instantiate (Char_Data.Ins.Char_Utama_Sample.gameObject);
        Ins.name = "Clone_Enemy";
        Ins.transform.position = this.transform.position;
        Ins.gameObject.SetActive (true);
        Char_Data.Ins.On_Set_Char_Pack (Ins.GetComponent <Char_Utama> (),"Claw_Bot");
        Ins.tag = "Enemy";
        Ins.GetComponent <Char_Utama> ()._Char_Status.On_Get_Data_From_Char_Utama (0,0,0,0,0);
        */
        if (Code_Char == "Player") {
            /*
            if (!Char_Data.Ins.Your_Char_Utama_Script) {
                Char_Data.Ins.On_Create_Char_Utama_With_Char_Spawn (this);
            } else {
                Char_Data.Ins.On_Spawn_Teleport (this, Char_Data.Ins.Your_Char_Utama);
            }
            */
            if (!Char_Data.Ins.Your_Char_Utama_Script) {
            } else {
                Char_Data.Ins.On_Spawn_Teleport (this, Char_Data.Ins.Your_Char_Utama);
            }
        } else {
             Char_Data.Ins.On_Create_Char_Utama_With_Char_Spawn (this);
        }
        
    }

    #region Source (Lobby_Scene), Char_Utama
        #region Char_Utama
        void On_Char_Spawn_Network () {
            Network_Go.Ins._Network_Player_Spawn.On_Char_Spawn (this);
        }

        // Char_Utama 
        public void On_Get_Spawn_Object (GameObject Ins_Go) {
            Char_Data.Ins.On_Set_Status (this,Ins_Go);
            Char_Data.Ins.On_Spawn_Teleport (this, Ins_Go);
        }
        #endregion
    #endregion
}
