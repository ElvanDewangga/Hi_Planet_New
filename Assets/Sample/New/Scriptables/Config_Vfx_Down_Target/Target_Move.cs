using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Move : MonoBehaviour
{
    [SerializeField]
    Vector3 V3_Target;
    void Update () {
        if (b_Move) {
            //this.transform.position = Vector3.Lerp (this.transform.position, V3_Target, Speed * Time.deltaTime);
            this.transform.position = Vector3.MoveTowards(this.transform.position, V3_Target, Speed );
            if (Vector3.Distance (this.transform.position, V3_Target) < 1.0f) {
                b_Move = false;
               // this.transform.position = V3_Target;
                On_Finish_Point ();
            }
        }
    }

    #region Char_Data_Variant_Attack
    bool b_Move = false;
    Char_Data_Variant_Attack _Char_Data_Variant_Attack;
    [HideInInspector]
    public Object_Variant_Config.Object_Delay Object_Config;
    [HideInInspector]
    public int Urutan;
    [HideInInspector]
    public GameObject GO_Peluru;
    float Speed = 0.0f;
    public void On_Set_Data (Char_Data_Variant_Attack Ca, Object_Variant_Config.Object_Delay Obj_Config, int Urutan_V, GameObject GO_Peluru_V, Vector3 V3_Start_V, Vector3 V3_Target_V, float Speed_V) {
        Object_Config = Obj_Config; Urutan = Urutan_V;
        this.transform.position =V3_Start_V;
        V3_Target = V3_Target_V;
        Speed = Speed_V;
        GO_Peluru = GO_Peluru_V;
        _Char_Data_Variant_Attack = Ca;
        b_Move = true;
    }

    void On_Finish_Point () {
        _Char_Data_Variant_Attack.On_Finish_Point_Vfx_Down_Target (this);
    }
    #endregion
}
