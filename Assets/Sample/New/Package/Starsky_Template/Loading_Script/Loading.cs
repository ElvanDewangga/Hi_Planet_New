using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Loading : MonoBehaviour {
    public static Loading Ins;
    void Awake () {
        Ins = this;
    }
    
    string Code_Active;
    // Visual_Novel_System :
   public void On_Loading (string Code_Active_V) {
        Code_Active = Code_Active_V;
        if (Code_Active == "Loading_1") {
            On_Loading_1 ();
        } else if (Code_Active == "Loading_2") {
            On_Loading_2 ();
        }
   }

    // Visual_Novel_System :
   public void Off_Loading (string x_V) {
        Code_Active = x_V;
        if (Code_Active == "Loading_1") {
            Off_Loading_1 ();
        } else if (Code_Active == "Loading_2") {
            Off_Loading_2 ();
        }
   }
   #region Loading_1
   
   [SerializeField]
   Image Loading_1; 
   void On_Loading_1 () {
    Loading_1.gameObject.SetActive (true);
   }

   void Off_Loading_1 () {
    Loading_1.gameObject.SetActive (false);
   }
   
   #endregion
   #region Loading_2
    [SerializeField]
    Image Loading_2;

    void On_Loading_2 () {
    Loading_2.gameObject.SetActive (true);
   }

   void Off_Loading_2 () {
    Loading_2.gameObject.SetActive (false);
   }
   #endregion

    #region Loading_Animation_Event
    UnityAction Loading_Unity_Event;
    void On_Example_Loading_Animation_Event () {
        // On_Loading_Animation_Event (On_Example_Loading_Animation_Event () Event setelah loading, 1.5f lama animasi);
    }
    //  :
    public void On_Loading_Animation_Event (UnityAction UnityAction, float Time_Animation) {
        Loading_Unity_Event = UnityAction;
        StartCoroutine (N_On_Loading_Animation_Event (Time_Animation));
    }

    IEnumerator N_On_Loading_Animation_Event (float Time_Animation) {
        yield return new WaitForSeconds (Time_Animation);
        Loading_Unity_Event ();
    }
        #region Loading_Animation_Event_A_Object
        UnityAction<object[]> Loading_Unity_Event_A_Object;
        object [] Unity_Event_A_Object;
        public void On_Loading_Animation_Event_A_Object (UnityAction<object[]> UnityAction, object[] parameters, float Time_Animation) {
            Loading_Unity_Event_A_Object = UnityAction;
            Unity_Event_A_Object = parameters;
            StartCoroutine (N_On_Loading_Animation_Event_A_Object (Time_Animation));
        }

        IEnumerator N_On_Loading_Animation_Event_A_Object (float Time_Animation) {
            yield return new WaitForSeconds (Time_Animation);
            Loading_Unity_Event_A_Object (Unity_Event_A_Object);
        }
        #endregion
    
    #endregion
    #region Loading_Time
    void On_Example_On_Loading_Time () {
        // Ingin membuat white fade effect : Code = Loading Code, Cd = Jeda waktu utk Off.
        On_Loading_Time ("Black_Fade", 1.0f);
    }

    public void On_Loading_Time (string Code, float Cd) {
        Loading_Time_Code = Code;
        On_Process_Black_Fade (0);
        StartCoroutine (N_On_Loading_Time (Cd));
    }
    
    IEnumerator N_On_Loading_Time (float Cd) {
        yield return new WaitForSeconds (Cd);
        On_Process_Black_Fade (1);
        yield return new WaitForSeconds (Loading_Time_Fade_Out); // Jeda waktu Animasi fade out
        On_Process_Black_Fade (2);
    }

    float Loading_Time_Fade_Out = 0.0f;
    string Loading_Time_Code = "";
    Animator Loading_Time_Animator;
    [SerializeField]
    Animator Black_Fade;
    void On_Process_Black_Fade (int Prc) {

        if (Prc == 0) {
            switch (Loading_Time_Code) {
                case "Black_Fade":
                    Loading_Time_Fade_Out = 0.5f;
                    Loading_Time_Animator = Black_Fade;
                break;

                default :
                break;
            }
            Loading_Time_Animator.gameObject.SetActive (true);
            Loading_Time_Animator.SetTrigger ("On");
        } else if (Prc == 1) {
            Loading_Time_Animator.SetTrigger ("Off");
        } else if (Prc == 2) {
            Loading_Time_Animator.gameObject.SetActive (false);
        }
    }
    #endregion
    /*
    #region White_Fade

    void On_Example_On_White_Fade () {
        // Ingin membuat white fade effect : Cd = Jeda waktu utk Off.
       
        On_White_Fade (0.5f);
    }
    public void On_White_Fade (float Cd) {
        White_Fade.gameObject.SetActive (true);
        White_Fade.SetTrigger ("On");
        
        StartCoroutine (N_On_White_Fade (Cd));
    }

    IEnumerator N_On_White_Fade (float Cd) {
        yield return new WaitForSeconds (Cd);
        White_Fade.SetTrigger ("Off");
        yield return new WaitForSeconds (0.5f); // Jeda waktu Animasi fade out
        White_Fade.gameObject.SetActive (false);

    }
    // Button_Event_Server : 
    public Animator White_Fade;
    #endregion
    #region Dinding_Fade
    public Animator Dinding_Fade;
    void On_Example_On_Dinding_Fade () {
        // Ingin membuat white fade effect : Cd = Jeda waktu utk Off.
        
        On_Dinding_Fade (0.5f);
    }
    public void On_Dinding_Fade (float Cd) {
        Dinding_Fade.gameObject.SetActive (true);
        Dinding_Fade.SetTrigger ("On");
        
        StartCoroutine (N_On_Dinding_Fade (Cd));
    }

    IEnumerator N_On_Dinding_Fade (float Cd) {
        yield return new WaitForSeconds (Cd);
        Dinding_Fade.SetTrigger ("Off");
        yield return new WaitForSeconds (0.5f); // Jeda waktu Animasi fade out
        Dinding_Fade.gameObject.SetActive (false);

    }
    #endregion
    */
    #region Loading_Invisible_List
    // Player_Duel_Room_Field :
    public bool b_Loading_Invisible = false;
    UnityAction Event_Loading_Invisible;
    [SerializeField]
    List <string> L_Loading_Invisible;
    int Cur_Progress;
    string Type_Loading = "";
    // Data_Game_Account :
    void Example_On_b_Loading_Invisible () {
        /*
            Type_Loading_V = Tipe Loading / Jenis loading
            Event_Loading_Invisible = Event_Loading_Invisible_V
            Cur_Progress = Tulis jumlah loading, jika tidak menggunakan jumlah loading bisa tulis 0 saja.
            On_b_Loading_Invisible ("Loading_1", On_Finish_Event_Loading (),0);
        */
    }
    public void On_b_Loading_Invisible (string Type_Loading_V, UnityAction Event_Loading_Invisible_V, int Cur_Progress_V) {
        if (!b_Loading_Invisible) {
            b_Loading_Invisible = true;
            Cur_Progress = Cur_Progress_V;
            Event_Loading_Invisible = Event_Loading_Invisible_V;
            Type_Loading = Type_Loading_V;

            if (Type_Loading == "Loading_1" || Type_Loading == "Loading_2") {
                Debug.Log (Type_Loading);
                On_Loading (Type_Loading);
            }
        } else {
            Debug.LogError ("Ada Loading Lewat ! Cek struktur loading");
        }
    }

    // Data_Game_Account :
    public void On_Add_Loading_Invisible (string s) {
        if (L_Late_Rpc_Loading_Invisible.Contains (s)) {
            On_Remove_L_Late_Rpc_Loading_Invisible (s);
        } else {
            L_Loading_Invisible.Add (s);
        }
        
    }

    // Data_Game_Account :
    public void On_Remove_Loading_Invisible (string s) {
        if (L_Loading_Invisible.Contains (s)) {
            L_Loading_Invisible.Remove (s);
        } else {
            On_Add_L_Late_Rpc_Loading_Invisible (s);
        }
        
        if (b_Loading_Invisible) {
            On_Check_Finish_Loading ();
        }

    }

    void On_Check_Finish_Loading () {
        // Debug.LogError ("Loading Removed");
            if (Cur_Progress >0) {
                Cur_Progress --;
            }

            if (L_Loading_Invisible.Count == 0 && Cur_Progress == 0) {                
                On_Finish_Event_Loading_Invisible ();   
            }
    }

    string Target_Loading_Invisible = ""; // supaya variable bisa lanjut loading dari Loading_Input_Deck ke Loading_Client_Pemisah_Duel_Room
    void On_Finish_Event_Loading_Invisible () {
        b_Loading_Invisible = false;

        if (Type_Loading == "Loading_1") {
            Off_Loading (Type_Loading);
        }
        Event_Loading_Invisible ();
    }

    [SerializeField]
    List <string> L_Late_Rpc_Loading_Invisible;
    void On_Add_L_Late_Rpc_Loading_Invisible (string s) {
        L_Late_Rpc_Loading_Invisible.Add (s);
    }

    void On_Remove_L_Late_Rpc_Loading_Invisible (string s) {
        L_Late_Rpc_Loading_Invisible.Remove (s);
    }
    #endregion
}
