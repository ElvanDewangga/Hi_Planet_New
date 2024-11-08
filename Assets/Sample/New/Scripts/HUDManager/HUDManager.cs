using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    void Awake () {
        instance = this;
    }

    #region Player_Status_Mini
    [Header("Player Info")]
    [SerializeField] private TMP_Text nicknameText;
    
    [Header("Health")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;

    [Header("Level & Exp")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider levelSlider;
    private Slider_Follow_Value levelSliderValueUpdater;
    [SerializeField] private TMP_Text expText;
    private Text_Follow_Value expTextValueUpdater;

    #endregion

    #region Claim_Button
    [Header("Claim Button")]
    [SerializeField] private Button claimButton;

    private string itemName = "";
    private int itemQuantity = 0;
    private GameObject itemSource;

    #endregion

    #region Player Status Updates
    // PlayerCharBase
    public void UpdateUsername(string username)
    {
        nicknameText.text = username;
    }
    // PlayerCharBase
    public void UpdateCurrentHp(int newHp)
    {
        currentHp = newHp;
        RefreshHpText();
    }
    // PlayerCharBase
    public void UpdateMaxHp(int newMaxHp)
    {
        maxHp = newMaxHp;
        RefreshHpText();
    }
    private void RefreshHpText()
    {
        hpText.text = $"Hp: {currentHp} / {maxHp}";
    }
    // PlayerCharBase
    public void UpdateLevel(int level)
    {
        Debug.Log ("Level up " +level);
        levelText.text = $"Lvl. {level}";
    }
    // PlayerCharBase
    public void UpdateExp(int currentExp, int maxExp)
    {
        // Lazy load components if they haven't been set
        if (expTextValueUpdater == null)
            expTextValueUpdater = expText.GetComponent<Text_Follow_Value>();

        expTextValueUpdater.On_Active(currentExp, maxExp);

        levelSlider.maxValue = maxExp;
        if (levelSliderValueUpdater == null)
            levelSliderValueUpdater = levelSlider.GetComponent<Slider_Follow_Value>();

        levelSliderValueUpdater.On_Active(currentExp);
    }

    #endregion

    #region Claim Item Functionality

    public void ClaimButtonPressed()
    {
        if (string.IsNullOrEmpty(itemName) || itemQuantity <= 0) return;

        List<string> itemNames = new() { itemName };
        List<int> itemQuantities = new() { itemQuantity };
        All_Scene_Go.Ins._Inventory.On_Add_Item(itemNames, itemQuantities);

        ResetClaim();
        itemSource.GetComponent<Item_Trigger>().On_Destroy();
    }

    public void SetClaimTarget(GameObject itemObject, string itemName, int quantity)
    {
        this.itemSource = itemObject;
        this.itemName = itemName;
        this.itemQuantity = quantity;

        claimButton.gameObject.SetActive(true);
    }

    public void ResetClaim()
    {
        itemName = string.Empty;
        itemQuantity = 0;
        claimButton.gameObject.SetActive(false);
    }

    #endregion
}
