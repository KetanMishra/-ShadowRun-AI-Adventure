using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI updates for health, stamina, timer, and score.
/// </summary>
public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;
    public Text timerText;
    public Text coinText;

    public void UpdateHealth(int value, int max)
    {
        if (healthBar) healthBar.value = value;
        if (healthBar) healthBar.maxValue = max;
    }

    public void UpdateStamina(float value, float max)
    {
        if (staminaBar) staminaBar.value = value;
        if (staminaBar) staminaBar.maxValue = max;
    }

    public void UpdateTimer(float time)
    {
        if (timerText) timerText.text = "Time: " + Mathf.CeilToInt(time).ToString();
    }

    public void UpdateCoins(int coins)
    {
        if (coinText) coinText.text = "Coins: " + coins;
    }
}
