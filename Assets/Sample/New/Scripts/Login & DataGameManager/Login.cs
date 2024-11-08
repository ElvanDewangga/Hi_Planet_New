using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private Canvas loginCanvas;
    [SerializeField] private Image loginBackground;
    [SerializeField] private Image newAccountBackground;
    [SerializeField] private Image existingAccountBackground;
    [SerializeField] private Image noPreviousIdBackground;
    [SerializeField] private Image hasPreviousIdBackground;
    [SerializeField] private TMP_Text userIdText;
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private Button anyButton;
    [SerializeField] private Button oldDataButton, oldDataButton2;
    [SerializeField] private Button newDataButton;
    [SerializeField] private Button loginButton;

    // GameManager :
    public  void InitializeLogin() {
        lastId = PlayerPrefs.GetString("lastId", "");
        lastPassword = PlayerPrefs.GetString("lastPassword", "");
        anyButton.onClick.AddListener (LatestAccountLogin);
        oldDataButton.onClick.AddListener (OnExistingAccountLogin);
        oldDataButton2.onClick.AddListener (OnExistingAccountLogin);
        // newDataButton.onClick.AddListener ();
        loginButton.onClick.AddListener (ConfirmExistingAccountLogin);
        if (!string.IsNullOrEmpty(lastId))
        {
            ShowPreviousId();
        }
        else
        {
            ShowNoPreviousId();
        }
    }

    void OnLogin()
    {
        SaveLastId();
        DisableLoginMenu();
    }

    private void EnableLoginMenu() 
    {
        if (!PlayerPrefs.HasKey("FromScene")) // Only show if not from scene transition
        {
            loginCanvas.gameObject.SetActive(true);
            loginBackground.gameObject.SetActive(true);
        }
        else
        {
            LoadingManager.instance.ShowLoading("Loading_1");
        }
    }

    private void DisableLoginMenu()
    {
        loginCanvas.gameObject.SetActive(false);
        loginBackground.gameObject.SetActive(false);
        newAccountBackground.gameObject.SetActive(false);
        hasPreviousIdBackground.gameObject.SetActive(false);
        noPreviousIdBackground.gameObject.SetActive(false);
        existingAccountBackground.gameObject.SetActive(false);
    }

    void ShowPreviousId() 
    {
        userIdText.text = "ID: " + lastId;
        hasPreviousIdBackground.gameObject.SetActive(true);
        EnableLoginMenu();
    }

    void HidePreviousId() 
    {
        hasPreviousIdBackground.gameObject.SetActive(false);
    }

    void ShowNoPreviousId()
    {
        noPreviousIdBackground.gameObject.SetActive(true);
        EnableLoginMenu();
    }

    void HideNoPreviousId()
    {
        noPreviousIdBackground.gameObject.SetActive(false);
    }

    void OnNewAccountLogin()
    {
        noPreviousIdBackground.gameObject.SetActive(false);
        loginBackground.gameObject.SetActive(false);
        newAccountBackground.gameObject.SetActive(true);
    }

    void OnExistingAccountLogin()
    {
        HidePreviousId(); HideNoPreviousId ();
        existingAccountBackground.gameObject.SetActive(true);
    }

    void ConfirmExistingAccountLogin()
    {
        if (string.IsNullOrEmpty(inputId.text))
        {
            PopupManager.instance.ShowPopup("Error Message", "ID cannot be empty", "Confirm");
            LoadingManager.instance.HideLoading ("Loading_2");
        }
        else if (string.IsNullOrEmpty(inputPassword.text))
        {
            PopupManager.instance.ShowPopup("Error Message", "Password cannot be empty", "Confirm");
            LoadingManager.instance.HideLoading ("Loading_2");
        }
        else
        {
            LoadingManager.instance.ShowLoading("Loading_2");
            SetLastId(inputId.text, inputPassword.text);
           // OnLogin();
            string [] Host_Server_Field = new string [2]; Host_Server_Field[0] = "Id"; Host_Server_Field[1] = "Table";
            string [] Host_Server_Value = new string [2]; Host_Server_Value[0] = inputId.text; Host_Server_Value[1] = "Db_Player";
            HiGameManager.serverManager.SendToHostServer ("Load_Db_Deck", "Read_All_Table_1", Host_Server_Field, Host_Server_Value, GetResultLogin);
        }
    }

    void LatestAccountLogin () {
        LoadingManager.instance.ShowLoading(lastId);
           // OnLogin();
            string [] Host_Server_Field = new string [2]; Host_Server_Field[0] = "Id"; Host_Server_Field[1] = "Table";
            string [] Host_Server_Value = new string [2]; Host_Server_Value[0] = lastId; Host_Server_Value[1] = "Db_Player";
            HiGameManager.serverManager.SendToHostServer ("Load_Db_Deck", "Read_All_Table_1", Host_Server_Field, Host_Server_Value, GetResultLogin);
    }
    #region lastId
    public string lastId { get; private set; }
    public string lastPassword { get; private set; }


    public void SetLastId(string id, string password)
    {

        lastId = id;
        lastPassword = password;
        SaveLastId();
    }

    public void SaveLastId()
    {
        PlayerPrefs.SetString("lastId", lastId);
        PlayerPrefs.SetString("lastPassword", lastPassword);
    }
    #endregion
    
    #region ServerProcess
    // ServerProcess
   
    public void GetResultLogin (string [] rows) {
      if (rows.Length >1) {  
        if (rows[1] != "" && rows[2] != "" && rows[2] == lastPassword) {
            OnLogin ();
            HiGameManager.dataGameManager.LoadPlayerDataAsync (lastId,rows[1],rows[2]);
            
        } else {
            if (rows[1] == "") {
                PopupManager.instance.ShowPopup("Error Message", "Account not found", "Confirm");
                LoadingManager.instance.HideLoading ("Loading_2");
            } else if (rows[2] != "" && rows[2] != lastPassword) {
                PopupManager.instance.ShowPopup("Error Message", "Wrong Password", "Confirm");
                LoadingManager.instance.HideLoading ("Loading_2");
            }
        } 
      } else {
        PopupManager.instance.ShowPopup("Error Message", "Lost Connection", "Confirm");
        LoadingManager.instance.HideLoading ("Loading_2");
      }
      
    }  
    #endregion
    
}

