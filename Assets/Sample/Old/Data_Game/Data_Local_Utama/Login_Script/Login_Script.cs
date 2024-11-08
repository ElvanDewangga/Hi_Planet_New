using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Login_Script : MonoBehaviour {
    public Data_Local_Utama _Data_Local_Utama;

    public virtual void On_Login () {
        _Data_Local_Utama._Data_Local.On_Save_Last_Id ();
        Off_Login_Menu ();
    }

    void On_Login_Menu () {
        if(!PlayerPrefs.HasKey("FromScene")) //jangan munculkan page setelah perpindahan scene
        {
            Data_Local_Utama_Canvas.gameObject.SetActive (true);
            Bg_Login.gameObject.SetActive (true);
        }else{
            Loading.Ins.On_Loading("Loading_1");
        }
    }

    void Off_Login_Menu () {
        Bg_Login.gameObject.SetActive (false);
        Bg_New_Account.gameObject.SetActive (false);
        Bg_On_Have_Last_Id.gameObject.SetActive (false);
        Bg_Off_Have_Last_Id.gameObject.SetActive (false);
        Bg_Old_Account.gameObject.SetActive (false);
        Data_Local_Utama_Canvas.gameObject.SetActive (false);
    }
    #region Data_Local
        [SerializeField]
        Canvas Data_Local_Utama_Canvas;
        [SerializeField]
        Image Bg_Login;
        [SerializeField]
        Image Bg_On_Have_Last_Id;
        [SerializeField]
        TMP_Text Id_Text;
        public virtual void On_Have_Last_Id () {
            Id_Text.text = "ID: " + _Data_Local_Utama._Data_Local.Last_Id;
            Bg_On_Have_Last_Id.gameObject.SetActive (true);
            On_Login_Menu ();
        }

        public virtual void Click_Old_Data () {
            Bg_On_Have_Last_Id.gameObject.SetActive (false);
            Debug.Log ("False");
        }
        [SerializeField]
        Image Bg_Off_Have_Last_Id;
        public virtual void Off_Have_Last_Id () {
            Bg_Off_Have_Last_Id.gameObject.SetActive (true);
            On_Login_Menu ();
        }

            #region Off_Have_Last_Id
            [SerializeField]
            Image Bg_New_Account;
            // Button :
            public void On_Login_New_Account () {
                Bg_Off_Have_Last_Id.gameObject.SetActive (false);
                Bg_Login.gameObject.SetActive (false);
                Bg_New_Account.gameObject.SetActive (true);
            }

            [SerializeField]
            Image Bg_Old_Account;
            // Button :
            public void On_Login_Old_Account () {
                Click_Old_Data ();
                Bg_Off_Have_Last_Id.gameObject.SetActive (false);
                Bg_Old_Account.gameObject.SetActive (true);
            }

            [SerializeField]
            public TMP_InputField IF_Id;
            public TMP_InputField IF_Password;
            public virtual void On_Login_Confirm_Old_Account () {
                _Data_Local_Utama._Data_Local.On_Set_Last_Id (IF_Id.text, IF_Password.text);
               // Bg_Login.gameObject.SetActive (false);
               // On_Login ();
            }
            #endregion
    #endregion
    
}
