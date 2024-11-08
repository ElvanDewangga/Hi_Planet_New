using System.Collections.Generic;
using UnityEngine;

public class SkillClickPanel : MonoBehaviour 
{
    public static SkillClickPanel Instance { get; private set; }

    [SerializeField] private SkillClickButton[] skillButtons;

    private SkillClickButton currentSkillButton;

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Skill Button Handling

    /// <summary>
    /// Sets the clicked skill button as the current skill and updates the skill source.
    /// </summary>
    /// <param name="skillButton">The clicked skill button.</param>
    public void SelectSkillButton(SkillClickButton skillButton) 
    {
        currentSkillButton = skillButton;
      //  skillSource.AssignSkill(currentSkillButton);
        
        if (AccountManager.instance.player != null)
        {
          //  CharData.Instance.Character.AttackHandler.AssignSkillPanelSource(skillButton.ButtonCode, skillButton.ButtonNumber);
            AccountManager.instance.player.PlaySkill (skillButton, skillButton.ButtonNumber);
        }
    }

    #endregion  

    #region Skill Countdown Handling

    /// <summary>
    /// Updates the cooldown timer on the current skill button.
    /// </summary>
    /// <param name="cooldownValue">The remaining cooldown time.</param>
    public void UpdateSkillCooldown(float cooldownValue) 
    {
        if (currentSkillButton != null)
        {
            currentSkillButton.StartCooldown(cooldownValue);
        }
    }

    #endregion
}