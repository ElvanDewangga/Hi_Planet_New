using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillClickButton : MonoBehaviour
{
    [SerializeField] private SkillClickPanel skillClickPanel;
    [SerializeField] private Button buttonInteract;
    [SerializeField] private Image countdownBackground;
    [SerializeField] private TMP_Text countdownText;
    
    public string ButtonCode { get; private set; }  // Example: "Basic_Attack", "Skill_Attack"
    public int ButtonNumber;  // Example: 0,1,2

    private float countdownTime;
    private Coroutine countdownCoroutine;

    #region Button Click Handling

    /// <summary>
    /// Triggers when the skill button is clicked, sending this button's reference to the SkillClickPanel.
    /// </summary>
    public void OnButtonClick() 
    {
        skillClickPanel.SelectSkillButton(this);
    }

    #endregion

    #region Countdown Handling

    /// <summary>
    /// Initiates the skill cooldown display.
    /// </summary>
    /// <param name="cooldownDuration">Duration of the cooldown in seconds.</param>
    /// 
    public void StartCooldown(float cooldownDuration) 
    {
        buttonInteract.interactable = false;
        countdownTime = cooldownDuration;
        countdownBackground.gameObject.SetActive(true);

        if (countdownCoroutine != null) 
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(CooldownRoutine());
    }

    /// <summary>
    /// Coroutine that handles countdown display for the skill button.
    /// </summary>
    private IEnumerator CooldownRoutine() 
    {
        float remainingTime = countdownTime;

        while (remainingTime > 0) 
        {
            countdownText.text = remainingTime.ToString("F1");
            remainingTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        EndCooldown();
    }

    /// <summary>
    /// Stops the cooldown and resets the button to an active state.
    /// </summary>
    private void EndCooldown() 
    {
        countdownBackground.gameObject.SetActive(false);
        buttonInteract.interactable = true;
        countdownCoroutine = null;
    }

    /// <summary>
    /// Pauses the cooldown by stopping the countdown coroutine.
    /// </summary>
    public void PauseCooldown() 
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }

    #endregion
}
