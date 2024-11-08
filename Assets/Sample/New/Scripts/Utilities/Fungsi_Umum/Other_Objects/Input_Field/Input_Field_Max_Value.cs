using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Input_Field_Max_Value : MonoBehaviour {
    /* Catatan
    Digunakan untuk memberikan batas input integer (Min_V, Max_V)
    */
   [SerializeField]
    TMP_InputField _Tmp_Inputfield;
    int Input_Field_Int;
    
    void Start () {
        _Tmp_Inputfield = GetComponent<TMP_InputField>();
        On_Active ();
    }

    void FixedUpdate () {
        int.TryParse (_Tmp_Inputfield.text, out Input_Field_Int);
        if (Input_Field_Int < Min_Value) {
            _Tmp_Inputfield.text = Min_Value.ToString ();
        } else if (Input_Field_Int > Max_Value) {
            _Tmp_Inputfield.text = Max_Value.ToString ();
        }
    }

    void Example_On_Setup () {
        // Memberikan value Min = 0 dan max = 50
        On_Setup (0,50);
    }

    int Min_Value, Max_Value;
    public void On_Setup (int Min_V, int Max_V) {
        Min_Value = Min_V;
        Max_Value = Max_V;
    }

    void On_Active () {
        
    } 
  
}
