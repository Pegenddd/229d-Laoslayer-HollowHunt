using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Tower Statistics")]
    public int maxTowerHealth = 10;
    private int currentTowerHealth;
    
    [Header("UI Component References")]
    public Image healthBarFill;
    public TextMeshProUGUI killText;

    [Header("Win/Loss UI Objects")]
    // Separate slots for different UI screens
    public GameObject winUI;   
    public GameObject loseUI;  

    private int killCount = 0;
    private bool isGameEnded = false;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        Time.timeScale = 1f; 
        currentTowerHealth = maxTowerHealth;
        
        // Ensure both UI objects are hidden at the start
        if (winUI != null) winUI.SetActive(false);
        if (loseUI != null) loseUI.SetActive(false);
        
        UpdateGameUI();
    }

    public void TakeTowerDamage(int damage)
    {
        if (isGameEnded) return;

        currentTowerHealth -= damage;
        if (currentTowerHealth <= 0)
        {
            currentTowerHealth = 0;
            // Activate Lose UI and load MainMenu
            StartCoroutine(EndGameSequence(loseUI, "MainMenu"));
        }
        UpdateGameUI();
    }

    public void AddKill(int amount)
    {
        if (isGameEnded) return;

        killCount += amount;
        UpdateGameUI();

        // Win Condition: Reach 5 kills
        if (killCount >= 5)
        {
            // Activate Win UI and load Credits
            StartCoroutine(EndGameSequence(winUI, "Credits"));
        }
    }

    // Handles pausing the game and switching scenes
    IEnumerator EndGameSequence(GameObject uiToActivate, string nextScene)
    {
        isGameEnded = true;

        // Show the specific UI (Win or Lose)
        if (uiToActivate != null)
        {
            uiToActivate.SetActive(true);
        }

        // Stop the game world (Physics and Movement)
        Time.timeScale = 0f; 

        // Wait for 3 seconds of real-world time
        yield return new WaitForSecondsRealtime(3f);

        // Reset time and change scene
        Time.timeScale = 1f; 
        SceneManager.LoadScene(nextScene);
    }

    void UpdateGameUI()
    {
        if (healthBarFill != null) 
            healthBarFill.fillAmount = (float)currentTowerHealth / maxTowerHealth;

        if (killText != null) 
            killText.text = "Kills: " + killCount;
    }
}