using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Baloon_Dialog : MonoBehaviour {
    void On_Example () {
        Transform Baloon_Img_Position = null;
        // All_Scene_Go_Script.Ins._Baloon_Dialog.On_Call_Baloon_Dialog ("I will become magician hehehe", "Event_Setelah_Baloon_Selesai",Baloon_Img_Position);
        
    }
    Vector3 Selisih_Jarak;
    // World_Canvas :
    public void On_Selisih_Jarak (Vector3 Selisih_Jarak_V) {
        Selisih_Jarak = Selisih_Jarak_V;
    }
    #region Type_Call_Baloon_Dialog
    bool b_Ins = false;
    // Home_Child_Interact :
    public void On_Call_Baloon_Dialog (string Isi_Tx, string Event_V, Transform Layout_V) {
        GameObject Ins = GameObject.Instantiate (this.gameObject);
        Ins.transform.position = Layout_V.transform.position + Selisih_Jarak;
        Ins.transform.SetParent (Layout_V);
        Ins.SetActive (true);
        Ins.GetComponent <Baloon_Dialog> ().On_Input_Chat (Isi_Tx, Event_V, true);
    }

    // World_Canvas :
    public void On_Set_Active_Baloon_Dialog (string Isi_Tx, string Event_V, Transform Layout_V) {
        this.transform.position = Layout_V.transform.position + Selisih_Jarak;
        // this.transform.SetParent (Layout_V);
        this.gameObject.SetActive (true);
        this.GetComponent <Baloon_Dialog> ().On_Input_Chat (Isi_Tx, Event_V, false);  
    }
    #endregion
   [SerializeField]
   TMP_Text Isi_Text;
   [SerializeField]
   Animator _Animator;
    string Event;
    // Home_Child_Interact :
   public void On_Input_Chat (string Isi_Tx, string Event_V, bool b_Ins_V) {
    b_Ins = b_Ins_V;
    Isi_Text.text = Isi_Tx;
    // Isi_Text.text = All_Scene_Go_Script.Ins._Pengubah_String._Replace_String.On_Replace_String (Isi_Text.text, "Player_Child", All_Scene_Go_Script.Ins._Pengubah_String._TMP_Text_Pengubah_Text_Sumber.On_TMP_Text_Pengubah_Text ("Player_Child") );
    // Isi_Text.text = All_Scene_Go_Script.Ins._Pengubah_String._Replace_String.On_Replace_String (Isi_Text.text, "Parent_Call", All_Scene_Go_Script.Ins._Pengubah_String._TMP_Text_Pengubah_Text_Sumber.On_TMP_Text_Pengubah_Text ("Parent_Call") );

     Event = Event_V;
    this.gameObject.SetActive (true);
    if (b_Ins) {
    StartCoroutine (N_On_Input_Chat ());
    }
   }

   IEnumerator N_On_Input_Chat () {
    yield return new WaitForSeconds (2.5f);
    // Anim Out :
    _Animator.SetTrigger ("Out");
    yield return new WaitForSeconds (0.5f);
    if (Event == "Can_Child_Chat_Again") {

    }
    if (b_Ins) {
    Destroy (this.gameObject);
    } else {
        // this.gameObject.SetActive (false);
    }
   }

   #region SetActive
    // World_Canvas :
    public void On_Set_Active (bool f) {
        this.gameObject.SetActive (false);
    }
   #endregion
}
