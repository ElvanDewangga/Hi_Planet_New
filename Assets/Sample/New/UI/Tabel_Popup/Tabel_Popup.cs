using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tabel_Popup : MonoBehaviour {
    #region Tabel_Popup_1
        #region Tabel_Popup_Canvas
            
            // Tabel_Popup_Clone_Close :
            public GameObject Tabel_Popup_Clone_Close_Samp;
            
            [SerializeField]
            Canvas Tabel_Popup_Canvas;
            
            // Test :
            public void On_Example_Tabel_Popup_Canvas () {
                On_Open_Tabel_Popup_Canvas ();
            }

            void On_Open_Tabel_Popup_Canvas () {
                Tabel_Popup_Canvas.gameObject.SetActive (true);
            }

            // Close_Button :
            public void Off_Open_Tabel_Popup_Canvas () {
                Tabel_Popup_Canvas.gameObject.SetActive (false);
            }
        #endregion
        
        #region Tabel_Popup_Source
        public Tabel_Popup_Source _Tabel_Popup_Source;
        [SerializeField]
        List <GameObject> L_Set_Tabel_Popup_Go = new List<GameObject> ();
        [SerializeField]
        List <string> L_Code_Popup = new List<string> ();
        List <Transform> L_Back_Go = new List<Transform> ();

    
            // Inventory_Source, Char_Data_Source :
        public void On_Set_Tabel_Popup (GameObject Popup_V, string Code_Popup_V, Transform Back_Go) {
            if (!L_Code_Popup.Contains (Code_Popup_V)) {
                L_Set_Tabel_Popup_Go.Add (Popup_V); L_Code_Popup.Add (Code_Popup_V); L_Back_Go.Add (Back_Go);
                _Tabel_Popup_Source.On_Code_Tabel_Popup (Code_Popup_V, Popup_V);
                On_Open_Tabel_Popup_Canvas ();
            }
        }

        void Off_Set_Tabel_Popup (int Id_Remove) {
                _Tabel_Popup_Source.Off_Code_Tabel_Popup (L_Code_Popup[Id_Remove], L_Set_Tabel_Popup_Go[Id_Remove], L_Back_Go[Id_Remove]);
        }

        int Id_Remove (string Code) {
                int result = 0;
                int x = 0;
                for (x = 0; x < L_Code_Popup.Count; x++) {
                    if (L_Code_Popup [x] == Code) {
                        result = x;
                    }
                } 
                return result;
        } 
        public void Off_All_Set_tabel_Popup () {
            Debug.Log ("CLOSE");
                foreach (string S in L_Code_Popup) {
                    Debug.Log ("CLOSE 2");
                    Off_Set_Tabel_Popup (Id_Remove (S));
                }

                L_Set_Tabel_Popup_Go = new List <GameObject> ();
                L_Code_Popup = new List<string> ();
                L_Back_Go = new List <Transform> ();
        }
        #endregion
        
        #region Close_Button or Back_Button
            // Tabel_Popup
            public void On_Close_Button () {
                Off_All_Set_tabel_Popup ();
                Off_Open_Tabel_Popup_Canvas ();
            }    
        #endregion
    #endregion
   #region Tabel_Popup_2
   [SerializeField]
   Canvas Tabel_Popup_2;
   [SerializeField]
   TMP_Text Tabel_Popup_2_Title, Tabel_Popup_2_Sub_Title;
   [SerializeField]
   Button [] A_Tabel_Popup_2_Button;
    string [] A_Tabel_Popup_2_Event, A_Tabel_Popup_2_Text;
    public void On_Tabel_Popup_2 (string Title, string Sub_Title, string [] Event_V, string [] Text_V) {
        foreach (Button x in A_Tabel_Popup_2_Button) {
            x.gameObject.SetActive (false);
        }
        A_Tabel_Popup_2_Event = new string [0]; A_Tabel_Popup_2_Text = new string [0];
        Tabel_Popup_2_Title.text = Title; Tabel_Popup_2_Sub_Title.text =Sub_Title;
        A_Tabel_Popup_2_Event = Event_V; A_Tabel_Popup_2_Text = Text_V;
        for (int y =0; y<A_Tabel_Popup_2_Event.Length; y++) {
            A_Tabel_Popup_2_Button[y].gameObject.SetActive (true);
            A_Tabel_Popup_2_Button[y].GetComponentInChildren<TMP_Text> ().text = A_Tabel_Popup_2_Text[y];
        }

        Tabel_Popup_2.gameObject.SetActive (true);
    }

    public void Tabel_Popup_2_Cli_Button (int x) {
        if (A_Tabel_Popup_2_Event[x] == "") {

        }
        Tabel_Popup_2.gameObject.SetActive (false);
    }
   #endregion
   
}
