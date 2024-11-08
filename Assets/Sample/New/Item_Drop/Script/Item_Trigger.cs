using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Trigger : MonoBehaviour
{
    [SerializeField]
    Item_Data _Item_Data;
    GameObject Baloon_Ins;
    void OnTriggerEnter (Collider Col) {
        if (Col.gameObject.tag == "Player") {
            Debug.Log ("Show List");
            Baloon_Ins =Char_Data.Ins.World_Baloon_Samp.GetComponent <World_Baloon> ().On_Create (this.gameObject, _Item_Data._Data_Item_Input._Item_Input.Name, _Item_Data._Data_Item_Input._Item_Input.Item_Detail);
            All_Scene_Go.Ins._Hud_Canvas.On_Got_Target (this.gameObject, _Item_Data._Data_Item_Input._Item_Input.Name, 1);
        }
    }

    void OnTriggerExit (Collider Col) {
        if (Col.gameObject.tag == "Player") {
            Debug.Log ("Delete Show");
            
            Baloon_Ins.GetComponent <World_Baloon> ().Off_Display ();
            Baloon_Ins = null;
            All_Scene_Go.Ins._Hud_Canvas.Off_Got_Target ();
        }
    }

    #region Hud_Canvas
    public void On_Destroy () {
        Baloon_Ins.GetComponent <World_Baloon> ().Off_Display ();
        Baloon_Ins = null;
        All_Scene_Go.Ins._Hud_Canvas.Off_Got_Target ();
        Destroy (this.gameObject);
    }
    #endregion
}
