using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_AI : MonoBehaviour {
    public Char_Utama _Char_Utama;

    public Char_AI_Zone_Target _Char_AI_Zone_Target;

    [SerializeField]
    List <Char_AI_Event> L_Char_AI_Event = new List<Char_AI_Event> ();
     public Char_AI_Follow _Char_AI_Follow;
     public Char_AI_Idle _Char_AI_Idle;
    public Char_AI_Attack _Char_AI_Attack;
    
    public Char_AI_Event On_Get_Char_AI_Event (string Name_V) {
        Char_AI_Event Res = null;
        switch (Name_V) {
            case "Char_AI_Follow" :
                Res = _Char_AI_Follow;
            break;
            case "Char_AI_Idle" :
                Res = _Char_AI_Idle;
            break;
            case "Char_AI_Attack" :
                Res = _Char_AI_Attack;
            break;
            default :

            break;
        }
        return Res;
    }

    // Enemy_Test_Canvas :
    public void On_Set_AI (string v) {
        if (Cur_Char_AI_Event == null) {
          //  Debug.Log ("Null");
            L_Char_AI_Event.Add (On_Get_Char_AI_Event(v));
            On_Play_AI ();
        } else {
         //   Debug.Log ("Not Null");
            Cur_Char_AI_Event.Stop ();
            L_Char_AI_Event.RemoveAt (0);
            L_Char_AI_Event.Add (On_Get_Char_AI_Event(v));
            On_Play_AI ();
        }
    }

    public void On_Add_AI (string v) {
        L_Char_AI_Event.Add (On_Get_Char_AI_Event(v));
    }

    public void On_Remove_AI (string v) {
        L_Char_AI_Event.Add (On_Get_Char_AI_Event(v));
    }

    public void On_Refresh_AI () {
        L_Char_AI_Event = new List<Char_AI_Event> ();
    }

    Char_AI_Event Cur_Char_AI_Event;
    void On_Play_AI () {
        Cur_Char_AI_Event = L_Char_AI_Event [0];
        Cur_Char_AI_Event.Play ();
    }

    void Start () {
       // On_Load_Dicitionary ();
       
    }

    #region Enemy_Test_Canvas
        #region Char_AI_Attack
        public void On_Set_Attack_Code (int x) {
            _Char_AI_Attack.On_Set_Attack_Code (x);
            Debug.Log ("Set Attack Code " + x);
        }
        #endregion
    #endregion
    /*
    #region Dictionary
    [SerializeField]
    Transform A_Char_AI_Event;
    Dictionary <string, Char_AI_Event> Dict_Char_AI_Event = new Dictionary<string, Char_AI_Event> ();

    void On_Load_Dicitionary () {
        Dict_Char_AI_Event = new Dictionary<string, Char_AI_Event> ();
        foreach (Transform Ts in A_Char_AI_Event) {
            Ts.GetComponent <Char_Data_Hit_Settings> ().On_Add_Asset ();
        }
    }

    public void On_Add_Dict_Char_Data_Hit (string s, Char_AI_Event cd) {
        Dict_Char_AI_Event.Add (s,cd);
    }

   #endregion
   */
}
