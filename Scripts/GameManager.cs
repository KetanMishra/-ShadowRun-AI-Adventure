using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages game state, objectives, win/lose, and UI.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int coinsCollected = 0;
    public bool relicCollected = false;
    public float gameTime = 300f; // 5 minutes
    public Text timerText;
    public Text coinText;
    public GameObject winScreen;
    public GameObject loseScreen;
    private float timer;
    private bool gameActive = true;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        timer = gameTime;
    }

    void Update()
    {
        if (!gameActive) return;
        timer -= Time.deltaTime;
        if (timerText) timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
        if (coinText) coinText.text = "Coins: " + coinsCollected;
        if (timer <= 0) LoseGame();
    }

    public void AddCoin()
    {
        coinsCollected++;
    }

    public void CollectRelic()
    {
        relicCollected = true;
        // Optionally show message: "Return to start!"
    }

    public void WinGame()
    {
        gameActive = false;
        if (winScreen) winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        gameActive = false;
        if (loseScreen) loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
