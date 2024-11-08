using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Input_Field_Default : MonoBehaviour {
    /* Catatan
    Digunakan untuk mendefaultkan value di Inputfield
    */
    [SerializeField]
    TMP_InputField _Tmp_Inputfield;
    
    void Start () {
        _Tmp_Inputfield = GetComponent<TMP_InputField>();
        On_Active ();
    }

    void Example_On_Setup () {
        // value akan otomatis kosong.
        On_Setup ("");
    }

    string Str_Default = "";
    public void On_Setup (string Str_Default_V) {
        Str_Default = Str_Default_V;
    }

    void On_Active () {
        _Tmp_Inputfield.text = Str_Default;
    } 

    void OnEnable () {
        On_Active ();
    }
}
