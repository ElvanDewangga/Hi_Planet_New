using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Type : MonoBehaviour {
    
    #region Head
    [SerializeField]
    GameObject [] A_Tab;
    GameObject Isi_Children;

    public Notification_Script _Notification;
    public Close_Button _Close_Button;
    void On_Refresh_Head () {
        Isi_Children = null;
    }
    #endregion
    #region Set_Popup_Tab (2 Component)
    // Sample_Scene :
    // int,bool,string
    public void Set_Popup_Tab (int tab_v, bool v, string val) { //v = true (Active), false (Diactive)
        On_Refresh_Head ();

        // Fungsi_Umum Fs = new Fungsi_Umum ();
        // Fs.On_Set_Active (A_Tab[tab_v], v);

        //Find_Children Find = A_Tab[tab_v].GetComponent <Find_Children> ();
       // Isi_Children = Find.On_Find_Children_GO (val);
     //   Fs.On_Set_Active (Isi_Children, v);
        
    }
    #endregion
    // Close_Button
    public Transform Popup_Panel;
    // GameObject_And_Destroy
    public GameObject Yellow_Arrow;
}
