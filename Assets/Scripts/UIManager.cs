using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image playerHealthBar;
    public Image enemyHealthBar;
    public TextMeshProUGUI combatLog;

    public Player Player;
    public Enemy Enemy;
    public GameObject BattlePopup;
    public Button YesButton;
    public Button NoButton;

    private Queue<string> messageQueue = new Queue<string>();
    private bool isDisplayingMessage = false;
    public float messageDisplayDuration = 1f;

    public void UpdateHealthBar(Image healthBar, float healthPercentage)
    {
        healthBar.fillAmount = healthPercentage;
        healthBar.color = Color.Lerp(Color.red, Color.green, healthPercentage);
    }

    public void LogCombatEvent(string message)
    {
        messageQueue.Enqueue(message);
        if (!isDisplayingMessage)
        {
            StartCoroutine(DisplayMessages());
        }
    }

    private IEnumerator DisplayMessages()
    {
        isDisplayingMessage = true;
        while (messageQueue.Count > 0)
        {
            string message = messageQueue.Dequeue();
            combatLog.text += message + "\n";
            yield return new WaitForSeconds(messageDisplayDuration);
        }
        isDisplayingMessage = false;
    }

    public void StartCombat()
    {
        LogCombatEvent("Combat begins!");
    }

    public void ShowCardOptions(Player player)
    {
        LogCombatEvent("Choose a card to play.");
    }

    public void ShowBattlePopup()
    {
        BattlePopup.SetActive(true);
    }

    public void HideBattlePopup()
    {
        BattlePopup.SetActive(false);
    }

    private void OnYesButtonClicked()
    {
        HideBattlePopup();
        CombatManager.Instance.StartCombat();
        
    }

    private void OnNoButtonClicked()
    {
        HideBattlePopup();
    }

    public void ShowActionResult(string result)
    {
        LogCombatEvent(result);
    }

    public void EndCombat(bool playerWon)
    {
        if (playerWon)
        {
            LogCombatEvent("Player wins!");
        }
        else
        {
            LogCombatEvent("Enemy wins!");
        }
    }

    void Start()
    {
        // hide at the start
        BattlePopup.SetActive(false);

        //listeners for buttons
        YesButton.onClick.AddListener(OnYesButtonClicked);
        NoButton.onClick.AddListener(OnNoButtonClicked);
    }
        

}

