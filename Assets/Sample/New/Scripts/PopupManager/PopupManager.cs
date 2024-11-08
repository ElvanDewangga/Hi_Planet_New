
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;
    [SerializeField] private Image popupBackground;
    [SerializeField] private TMP_Text popupTitle;
    [SerializeField] private TMP_Text popupMessage;
    [SerializeField] private Button popupButton;

    private void Awake()
    {
        instance = this;
        popupButton.onClick.AddListener (clickpopupButton);
        DontDestroyOnLoad (this.gameObject);
    }

    public void ShowPopup(string title, string message, string buttonText)
    {
        popupBackground.gameObject.SetActive (true);
        popupTitle.text = title; popupMessage.text = message; popupButton.GetComponentInChildren<TMP_Text> ().text = buttonText;
    }

    void HidePopup()
    {
        popupBackground.gameObject.SetActive (false);
    }

    public void clickpopupButton () {
        HidePopup ();
    }
}
