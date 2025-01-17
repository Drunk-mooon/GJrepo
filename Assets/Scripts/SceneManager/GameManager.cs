using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameDuration = 60f;           // Duration of the game in seconds
    public GameObject tutorialPanel;          // Tutorial panel to display at the start
    public GameObject endGamePanel;           // End panel to display when time runs out

    private UIManager uiManager;               // Reference to UIManager
    private float elapsedTime = 0f;           // Time since the game started
    private bool isGameActive = false;
    private bool isTuitionActive = false;
    private bool isEndActive = false;

    private float player1Score = 0.1f;
    private float player2Score = 0.1f;

    void Start()
    {
        // Find UIManager in the scene and assign it to uiManager
        uiManager = FindObjectOfType<UIManager>();

        ShowTutorial();
    }

    void Update()
    {
        if (isGameActive)
        {
            GameUpdate();
        }

        if (isTuitionActive)
        {
            TuitionUpdate();
        }

        if (isEndActive)
        {
            EndUpdate();
        }
    }

    #region Update
    private void GameUpdate()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerUI();
        uiManager.UpdateScoreBar(player1Score, player2Score);
        if (elapsedTime >= gameDuration)
        {
            EndGame();
        }
    }

    private void TuitionUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    private void EndUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }
    #endregion

    #region GameGlobalOperation
    public void StartGame()
    {
        tutorialPanel.SetActive(false);
        elapsedTime = 0f;
        isGameActive = true;
        isTuitionActive = false;
        isEndActive = false;

        if (uiManager != null)
        {
            uiManager.timerText.gameObject.SetActive(true);
            UpdateTimerUI();
        }

        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        isGameActive = false;
        isTuitionActive = false;
        isEndActive = true;

        endGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ResetGame()
    {
        elapsedTime = 0f;
        isGameActive = false;
        isTuitionActive = true;
        isEndActive = false;
        tutorialPanel.SetActive(true);
        endGamePanel.SetActive(false);
        Time.timeScale = 0f;

        if (uiManager != null)
        {
            uiManager.timerText.gameObject.SetActive(false);
        }
    }

    #endregion

    #region UIControl
    private void UpdateTimerUI()
    {
        if (uiManager != null)
        {
            float remainingTime = Mathf.Max(0, gameDuration - elapsedTime);
            uiManager.UpdateTimerUI(remainingTime); // Call the UIManager to update the timer UI
        }
    }

    private void ShowTutorial()
    {
        isGameActive = false;
        isTuitionActive = true;
        isEndActive = false;

        tutorialPanel.SetActive(true);
        endGamePanel.SetActive(false);

        if (uiManager != null)
        {
            uiManager.timerText.gameObject.SetActive(false); // Hide timer text
        }

        Time.timeScale = 0f;
    }
    #endregion

    #region DataManage
    public void AddScore(int player,float score)
    {
        if (player == 1)
            player1Score += score;
        if (player==2)
            player2Score += score;
    } 
    #endregion
}
