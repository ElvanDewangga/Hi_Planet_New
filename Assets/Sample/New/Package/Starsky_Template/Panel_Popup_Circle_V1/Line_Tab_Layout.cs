using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Tab_Layout : MonoBehaviour {
    public Panel_Popup_Circle_V1 _Panel_Popup_Circle_V1;
    [Header ("Centang jika pakai lebih dari 1 layout :")]
    [SerializeField]
    bool b_Using_More_Than_1_Layout = false; 
    [Header ("b_Using_More_Than_1_Layout (TRUE) :")]
    // batas objeck instantiate di dalam satu layout sebelum pindah
   // [SerializeField] // ada yang diubah langsung dari inspector atau lewat script ini.
    public int Max_Ins_Per_Layout = 5;
    [SerializeField]
    int Start_Set_Sibling = 1;
    int Cur_Set_Sibling = 0;
    
    // GameObject Go_Layout_Samp;
    Transform Target_Par;
    string Event_Object ="";
    GameObject Layout_Go_Samp;
    
    public void On_Input_Layout_Samp (GameObject Sp) {
        On_Destroy_All_Go_Layout ();
        Cur_Set_Sibling = Start_Set_Sibling;
         On_Refresh_Status ();
        Go_Samp = Sp;Layout_Go_Samp = Sp;
        Event_Object = "Layout";
        On_Instantiate_Go_Samp ();
    }

    public void On_Create_Layout_Again () {
        Go_Samp = Layout_Go_Samp;
        Event_Object = "Layout";
        On_Instantiate_Go_Samp ();
    }

    GameObject Go_Samp;
     // My_Deck, Edit_Deck :
    public void On_Input_Go_Samp (GameObject Sp) {
        On_Refresh_Status ();
        Go_Samp = Sp;
        Event_Object = "";
        On_Instantiate_Go_Samp ();
    }

    int Target_Sibling_Object = -1;
    // My_Deck :
    public void On_Input_Go_Samp (GameObject Sp, int Target_Sibling_Object_V) {
        On_Refresh_Status ();
        Go_Samp = Sp; Target_Sibling_Object = Target_Sibling_Object_V;
        Event_Object = "";
        On_Instantiate_Go_Samp ();
    }
    // Harus public karena ada GetField () :
   // public Deck_Choose_Samp _Deck_Choose_Samp;
    [HideInInspector]
    public Partna_Select_Button _Partna_Select_Button;
    [HideInInspector]
    public GI_V2_Button _GI_V2_Button;
     // Panel_Popup_Circle_V1 :
    public void On_Input_Go_Samp (GameObject Go, string Code, object Sp) {
        On_Refresh_Status ();
        Go_Samp = Go;
        Event_Object = "";
        this.GetType().GetField(Code).SetValue(this, Sp);
        On_Instantiate_Go_Samp (); 
    }
    

    void On_Refresh_Status () {
        Target_Sibling_Object = -1;
        _Partna_Select_Button = null; _GI_V2_Button = null;
    }
    [SerializeField]
    List <Line_Tab_Samp> L_Ins_Line_Tab = new List<Line_Tab_Samp> ();
    Line_Tab_Samp On_Get_L_Ins_Line_Tab () {
        Line_Tab_Samp Res = null;
        foreach (Line_Tab_Samp Tb in L_Ins_Line_Tab) {
            if (Tb.L_Go_Tab.Count < Max_Ins_Per_Layout) {
                Res = Tb;
                break;
            }
        }
        return Res;
    }

    void On_Remove_Go_Tab_L_Ins_Line_Tab (GameObject s) {
        foreach (Line_Tab_Samp Tb in L_Ins_Line_Tab) {
            if (Tb.L_Go_Tab.Contains (s)) {
                Tb.On_Remove_Go_Tab (s);
                On_Destroy_Go_Layout_With_No_Object (Tb);
                break;
            }
        }
        On_Auto_Sort_Layout ();
    }
    [SerializeField]
    List <GameObject> L_Ins_Go = new List<GameObject> ();
    // My_Deck :
    public List <GameObject> Get_L_Ins_Go () {
        return L_Ins_Go;
    }
    GameObject Holding_Go;
    void On_Ins_Holding_Go () {
        Go_Samp = Holding_Go;
        Event_Object = ""; Holding_Go = null;
        On_Instantiate_Go_Samp (); 
    }

    void On_Instantiate_Go_Samp () {
        GameObject Ins = GameObject.Instantiate (Go_Samp);
      //  Debug.Log ("Instantiate " + Event_Object);
        if (Event_Object == "Layout") {
            Cur_Set_Sibling = Start_Set_Sibling +L_Ins_Line_Tab.Count;
            if (!b_Using_More_Than_1_Layout) {
                Target_Par = this.gameObject.transform;
            } else {
                Target_Par = Ins.transform;
            }
            Ins.GetComponent <Sample_Check> ().On_b_Sample(false);
            Ins.transform.SetParent (_Panel_Popup_Circle_V1.transform);
            Ins.transform.SetSiblingIndex(Cur_Set_Sibling);
            L_Ins_Line_Tab.Add (Ins.GetComponent <Line_Tab_Samp> ());

            if (Holding_Go != null) {
                On_Ins_Holding_Go ();
            }
            
            On_Set_Layout_Settings (Ins);
          
        } else {
            On_Send_Data_To_Object (Ins);
            // if (_Deck_Choose_Samp != null) {Ins.GetComponent<Deck_Choose_Samp> ().On_Refresh_Input_Data (_Data_Deck_Choose_Samp);}
          //  if (_Card_2d_Sample != null) {Ins.GetComponent<Card_2d_Sample> ().On_Input_Data (C_Card);}
            // All_Scene_Go_Script As = All_Scene_Go_Script.Instance;
            

            Line_Tab_Samp Tb = On_Get_L_Ins_Line_Tab ();
            if (Tb == null ) {
                Holding_Go = Ins;
            //    Holding_C_Card = C_Card;
          //      Holding_Card_2d_Sample = Card;
                On_Create_Layout_Again ();
                Destroy (Ins);
            } else {
                Tb.On_Add_Go_Tab (Ins);
                 Ins.transform.SetParent (Tb.gameObject.transform);
                 if (Target_Sibling_Object != -1) {
                    Ins. transform.SetSiblingIndex (Target_Sibling_Object);
                 }
                L_Ins_Go.Add (Ins);
                Debug.Log ("Add");
            }
           Ins.transform.localScale = new Vector3 (1,1,1);
            
        }
        
        Ins.SetActive (true);
        // Ins.transform.localScale = new Vector3 (1,1,1);
    }
    
    string Code_Remove = "My_Deck";
    // My_Deck
    public void On_Remove_Go_Samp (string Event_V) {
        On_Refresh_Status ();
        Code_Remove = Event_V;
        On_Removing_Go_Samp (); 
    }
    
    void On_Removing_Go_Samp () {
        bool b_Removing = false;
        Debug.LogError ("Remove");
        if (Code_Remove == "My_Deck") {
            /*
            int Num = -1;
            foreach (GameObject Go in L_Ins_Go) {
                Num++;
                
                Card_2d_Sample Cs = Go.GetComponent <Card_2d_Sample> ();
                if (Cs.On_Get_C_Card ().Number == C_Card.Number) {
                    b_Removing = true;
                    break;
                }
                
            }

            On_Remove_Go_Tab_L_Ins_Line_Tab (L_Ins_Go[Num]);
            if (b_Removing) {
                Destroy (L_Ins_Go[Num]);
                L_Ins_Go.RemoveAt (Num);
            }
            */
        } else if (Code_Remove == "My_Deck_Add_Button") {
            /*
            int Num = -1;
            foreach (GameObject Go in L_Ins_Go) {
                Num ++;
                if (!Go.GetComponent <Deck_Choose_Samp> ()) {
                    b_Removing = true;
                    break;
                }
            }

            On_Remove_Go_Tab_L_Ins_Line_Tab (L_Ins_Go[Num]);
            if (b_Removing) {
                Destroy (L_Ins_Go[Num]);
                L_Ins_Go.RemoveAt (Num);
            }
            */
        }
    }

    void On_Destroy_All_Go_Layout () {
        foreach (Transform si in this.transform) {
            Sample_Check Sc= si.GetComponent <Sample_Check> ();
            if (Sc.b_Sample == false) {
                Destroy (si.gameObject);
            }
        }
        L_Ins_Go = new List<GameObject> ();
        L_Ins_Line_Tab = new List<Line_Tab_Samp> ();
    }

    

    void On_Destroy_Go_Layout_With_No_Object (Line_Tab_Samp s) {
        if (s.L_Go_Tab.Count == 0) {
            L_Ins_Line_Tab.Remove (s);
            Destroy (s.gameObject);
        }
    }

    #region Auto_Sort_Layout
    void On_Auto_Sort_Layout () { 
        
        int x = 0;
        for (x = 0; x < L_Ins_Line_Tab.Count; x++) {
            if (x < L_Ins_Line_Tab.Count-1) {
                if (L_Ins_Line_Tab[x].L_Go_Tab.Count < Max_Ins_Per_Layout) {
                    On_Get_Object_From_Next_Layout (x);
                }
            } else { // tab terakhir.

            }
        }
    }

    void On_Get_Object_From_Next_Layout (int val) {
        // NOTE :
        // untuk sementara hanya dibuat mengambil satu object, jika suatu saat ingin mengambil object sesuai jumlah slot yang kosong, maka cukup mengubah variable menjadi jumlah yang dibutuhkan + loop.
        
        // Memindahkan object 3d :
        L_Ins_Line_Tab[val +1].L_Go_Tab[0].transform.SetParent (L_Ins_Line_Tab[val].gameObject.transform);
        // menambah komponen sesuai dengan layout next yang terdepan atau teratas
        L_Ins_Line_Tab[val].On_Add_Go_Tab (L_Ins_Line_Tab[val +1].L_Go_Tab[0]);
        // menghapus layout next yang sudah ditambahakan ke layout prev.
         L_Ins_Line_Tab[val+1].On_Remove_Go_Tab (L_Ins_Line_Tab[val +1].L_Go_Tab[0]);
         On_Destroy_Go_Layout_With_No_Object (L_Ins_Line_Tab[val+1]);
    }
    #endregion
    #region L_Ins_Go_Event
    // Business_Location_Canvas - Panel_Popup_Circle_V1 :
    /* memberikan suatu event/action utk seluruh object button yang diinstantiate dalam sebuah layout :
    */
    public void On_Doing_Event_All_L_Ins_Go (string Event_V) {
        foreach (GameObject si in L_Ins_Go) {
            Sample_Check Sc= si.GetComponent <Sample_Check> ();
            if (Sc.b_Sample == false) {
                if (Event_V == "Off_All_Button_Partna") {
                    Partna_Select_Button Ps = si.GetComponent <Partna_Select_Button> ();
                    Ps.Off_Select_Directly ();
                }
            }
        }
    }
    #endregion
    #region Other_Source

    public virtual void On_Set_Layout_Settings (GameObject Layout_Ins) {
        Debug.Log ("Set Layout " + _Panel_Popup_Circle_V1.Sub_Event_Line_Tab);
        if (_Panel_Popup_Circle_V1.Sub_Event_Line_Tab == "Inventory") {
            
            Layout_Ins.transform.localScale = new Vector3 (0.95f,0.95f,0.95f);
            Layout_Ins.GetComponent<Scale_Fixer>().On_Set_V3_Scale (new Vector3 (0.95f,0.95f,0.95f));
            Max_Ins_Per_Layout = 8;
        }
    }

    public virtual void On_Send_Data_To_Object (GameObject Object_Ins) {

    }
    #endregion
}
