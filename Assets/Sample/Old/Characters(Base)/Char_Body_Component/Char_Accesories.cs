using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item_Ability;
public class Char_Accesories : MonoBehaviour
{   
    #region Char_Body_Component
    [SerializeField]
    Char_Body_Component _Char_Body_Component;
    public string Code_Accesories;
    [SerializeField]
    Data_Item_Input _Data_Item_Input_Accesories;
    Transform [] A_Sample;
    [SerializeField]
    GameObject Accesories_Sample;
    public void On_Set_Char_Accesories (Data_Item_Input From) {
        A_Sample = _Char_Body_Component.On_Get_Char_Utama ()._Char_Animation._A_Aseprite.A_Accesories;
        On_Destroy_Clone_In_A_Sample ();
        foreach (Transform S in A_Sample) {
            

            Convert_Item_Effect Ci = new Convert_Item_Effect ();
            Ci.On_Transfer_Data_Item_Input (From, _Data_Item_Input_Accesories);
            
            GameObject Ins = GameObject.Instantiate (Accesories_Sample);
            Ins.transform.SetParent (S);
            Ins.GetComponent<Sample_Check> ().On_b_Sample (false);
            Ins.gameObject.SetActive (true);
            
            Item_Display_Logic Idl = new Item_Display_Logic ();
            if (Code_Accesories == "Helmet") {
                Idl.On_Set_Item_Display (Ins, From._Item_Input, _Char_Body_Component.On_Get_Char_Utama ()._Char_Pack._Char_Pack_Accesories.V3_Offset_Glass);
            }
        }
        
    }

    // Char_Body_Component
    public void On_Destroy_Clone_In_A_Sample () {
        foreach (Transform S in A_Sample) {
            foreach (Transform Trs in S.transform) {
                Sample_Check Sc = Trs.gameObject.GetComponent <Sample_Check> ();
                if (!Sc.b_Sample) {
                    Destroy (Trs.gameObject);
                }
            }
        }
    }
   #endregion
}
