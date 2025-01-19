using UnityEngine;
using TMPro;
using UnityEngine.UI;  // 如果要使用 UI 元素，比如 Text, Button

public class Minigame : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI  countdownText;      // 显示倒计时的文本
    public TextMeshProUGUI  playerAKeyText;     // 显示玩家 A 需要按的键
    public TextMeshProUGUI  playerBKeyText;     // 显示玩家 B 需要按的键
    public TextMeshProUGUI  resultText;         // 显示最终结果

    [Header("Settings")]
    public float minigameTime = 5f;
    public int fontAmplifyTime = 2;
    public float showResultTime = 1f;

    [Header("Reference")]
    public GameManager gameManager;
    public PropManager propManager;

    private bool minigameOngoing = false; // 是否正在进行游戏

    private KeyCode playerAKey;
    private KeyCode playerBKey;

    private int playerACount;
    private int playerBCount;

    // 用于把结果返回给调用方:
    private System.Action<bool> onMinigameEnd; 
    // 这个回调里，bool=true 表示 A 胜利，false 表示 B 胜利
    
    private bool isAWin;

    // 所有可能随机到的 KeyCode，可以按照需求自定义
    private KeyCode[] possibleKeys = 
    {
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, 
        KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, 
        KeyCode.N, KeyCode.M
    };
    
    //对外暴露的方法：开始比拼并暂停游戏
    public void StartMinigame(System.Action<bool> callback)
    {
        // 暂停游戏
        Time.timeScale = 0f;

        // 清空之前的数据
        playerACount = 0;
        playerBCount = 0;

        onMinigameEnd = callback;
        isAWin = false;

        // 随机生成玩家 A、B 的按键
        playerAKey = GetRandomKeyCode();
        playerBKey = GetRandomKeyCode();

        // 更新 UI 显示
        if (playerAKeyText) playerAKeyText.text = "Left Player Key: " + playerAKey.ToString();
        if (playerBKeyText) playerBKeyText.text = "Right Player Key: " + playerBKey.ToString();

        // 重置结果文本
        if (resultText) resultText.text = "";

        // 启动协程来计时 & 监听按键
        minigameOngoing = true;
        StartCoroutine(MinigameRoutine());
    }

    // 协程：进行 10 秒倒计时，每帧监听键盘按下次数
    private System.Collections.IEnumerator MinigameRoutine()
    {
        float timer = minigameTime;

        // Define the base font size
        int baseFontSize = 36;

        while (timer > 0f)
        {
            // Listen for key presses (use unscaledTime, since timeScale=0)
            if (Input.GetKeyDown(playerAKey))
            {
                playerACount++;
            }
            if (Input.GetKeyDown(playerBKey))
            {
                playerBCount++;
            }

            // Update the font size based on the counts
            if (playerAKeyText)
            {
                playerAKeyText.fontSize = baseFontSize + playerACount * fontAmplifyTime; // Increase by 2 for each key press
            }

            if (playerBKeyText)
            {
                playerBKeyText.fontSize = baseFontSize + playerBCount * fontAmplifyTime; // Increase by 2 for each key press
            }

            // Update countdown text
            if (countdownText)
            {
                countdownText.text = "Countdown: " + Mathf.CeilToInt(timer).ToString() + "s";
            }

            // Decrease timer (use unscaledDeltaTime because timeScale=0)
            timer -= Time.unscaledDeltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Time's up, end the mini-game
        minigameOngoing = false;
        if (countdownText)
        {
            countdownText.text = "Game Will Continue After " + showResultTime.ToString() + " Seconds";
        }

        // Handle the result: if there's a tie, randomly pick a winner
        if (playerACount > playerBCount)
        {
            isAWin = true;  // A wins
            if (resultText) resultText.text = "A Wins!";
            propManager.PlayerAGetProp();
        }
        else if (playerACount < playerBCount)
        {
            isAWin = false; // B wins
            if (resultText) resultText.text = "B Wins!";
            propManager.PlayerBGetProp();
        }
        else
        {
            // Tie-breaker logic: Randomly select a winner
            Random.InitState(System.DateTime.Now.Millisecond);
            isAWin = Random.Range(0, 2) == 0; // 50% chance for A to win, 50% for B to win
            if (resultText)
            {
                resultText.text = isAWin ? "A Wins!" : "B Wins!";
            }
        }


        // Assign random props after a tie
        if (isAWin)
        {
            propManager.PlayerAGetProp();
        }
        else
        {
            propManager.PlayerBGetProp();
        }

        // Wait for a few seconds before triggering the end callback and ending the mini-game
        yield return new WaitForSecondsRealtime(showResultTime); // Use WaitForSecondsRealtime instead of WaitForSeconds

        // Notify the result to the caller after the delay
        if (onMinigameEnd != null)
        {
            onMinigameEnd(isAWin);
        }

        // End the mini-game
        gameManager.EndMiniGame();

        // Restore normal game time
        Time.timeScale = 1f;
    }





    // 工具方法：从 possibleKeys 中随机取一个 KeyCode
    private KeyCode GetRandomKeyCode()
    {
        int idx = Random.Range(0, possibleKeys.Length);
        return possibleKeys[idx];
    }
}
