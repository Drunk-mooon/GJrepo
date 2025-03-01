using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public float gameDuration = 60f;           // Duration of the game in seconds
    public GameObject tutorialPanel;          // Tutorial panel to display at the start
    public GameObject endGamePanel;           // End panel to display when time runs out

    private MainSceneUIManager uiManager;               // Reference to UIManager
    private float elapsedTime = 0f;           // Time since the game started
    private bool isGameActive = false;
    private bool isTuitionActive = false;
    private bool isEndActive = false;

    //private float playerAScore = 0.1f;       //variables for debug. 
    //private float playerBScore = 0.1f;

    public PlayerA playerA;
    public PlayerB playerB;

    private int miniGameTime = 0;
    public float miniGameInterval = 10f;
    private bool isInMiniGame = false;
    public Minigame minigame;

    public Sprite[] Tuitions;
    public Image tuitionImage;
    private int tuitionIndex;
    private bool isNearEndMusicPlayed = false;

    public TextMeshProUGUI EndPanelText;
    void Start()
    {
        SoundManager.Init();
        MusicManager.Init();
        SoundManager.AddSound("sound/游戏开始", 0, 1);
        MusicManager.AddMusic("ggjj", 0, 0.5f);
        // Find UIManager in the scene and assign it to uiManager
        uiManager = FindObjectOfType<MainSceneUIManager>();
        isInMiniGame = false;
        playerA.playerScore = 0.1f;
        playerB.playerScore2 = 0.1f;
        isNearEndMusicPlayed = false;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowTutorial();
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if(gameDuration - elapsedTime <= 10f && !isNearEndMusicPlayed)
            {
                SoundManager.AddSound("sound/最后十秒", 0, 1);
                isNearEndMusicPlayed=true;
            }
            UpdateTimerUI();
            uiManager.UpdateScoreBar(playerA.playerScore, playerB.playerScore2);
            //Debug.Log((elapsedTime - (miniGameTime + 1) * miniGameInterval));
            if (elapsedTime >= gameDuration)
            {
                EndGame();
            }
            else if ((elapsedTime - (miniGameTime + 1) * miniGameInterval >= 0f) && !isInMiniGame)
            {
                Debug.Log("minigame!");
                BeginMiniGame();
            }
            
        }
    }

    private void TuitionUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
        else if (Input.anyKeyDown)
        {
            tuitionIndex = (tuitionIndex + 1) % (Tuitions.Length);
            tuitionImage.sprite = Tuitions[tuitionIndex];
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
        SoundManager.AddSound("sound/游戏结束", 0, 1);
        Time.timeScale = 0f;

        if(playerA.playerScore > playerB.playerScore2)
        {
            EndPanelText.text = "Player RED WIN!";
        }
        else
        {
            EndPanelText.text = "Player BLUE WIN!";
        }
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
        SoundManager.Init();
        MusicManager.Init();
        isNearEndMusicPlayed = false;
        elapsedTime = 0f;
        isGameActive = false;
        isTuitionActive = true;
        isEndActive = false;
        tutorialPanel.SetActive(true);
        endGamePanel.SetActive(false);
        Time.timeScale = 0f;
        playerA.playerScore = 0.1f;
        playerB.playerScore2 = 0.1f;
        miniGameTime = 0;

        if (uiManager != null)
        {
            uiManager.timerText.gameObject.SetActive(false);
        }
    }

    public void BeginMiniGame()
    {
        isInMiniGame = true;
        uiManager.EnableMiniGamePanel();
        minigame.StartMinigame(OnMinigameEnd);
        SoundManager.AddSound("sound/小游戏开始", 0, 1);
    }
    public void EndMiniGame()
    {
        isInMiniGame = false;
        miniGameTime++;
        uiManager.DisableMiniGamePanel();
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
        tuitionIndex = (tuitionIndex + 1) % (Tuitions.Length);
        tuitionImage.sprite = Tuitions[tuitionIndex];
        isGameActive = false;
        isTuitionActive = true;
        isEndActive = false;

        tutorialPanel.SetActive(true);
        endGamePanel.SetActive(false);
        tuitionIndex = 0;
        if (uiManager != null)
        {
            uiManager.timerText.gameObject.SetActive(false); // Hide timer text
        }

        Time.timeScale = 0f;
    }
    #endregion

    #region DataManage

    void OnMinigameEnd(bool isAWin)
    {
        if (isAWin)
        {
            Debug.Log("玩家A 获胜！");
        }
        else
        {
            Debug.Log("玩家B 获胜！");
        }
    }
    #endregion


}
