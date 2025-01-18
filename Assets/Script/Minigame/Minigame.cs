using UnityEngine;
using TMPro;
using UnityEngine.UI;  // 如果要使用 UI 元素，比如 Text, Button

public class Minigame : MonoBehaviour
{
    [Header("UI 元素")]
    public TextMeshProUGUI  countdownText;      // 用于显示倒计时的文本
    public TextMeshProUGUI  playerAKeyText;     // 用于显示玩家 A 需要按的键
    public TextMeshProUGUI  playerBKeyText;     // 用于显示玩家 B 需要按的键
    public TextMeshProUGUI  resultText;         // 用于显示最终结果

    [Header("配置")]
    public float minigameTime = 5f;

    private bool minigameOngoing = false; // 是否正在进行比拼

    private KeyCode playerAKey;
    private KeyCode playerBKey;

    private int playerACount;
    private int playerBCount;

    // 用于把结果返回给调用方(比如外部脚本):
    private System.Action<bool> onMinigameEnd; 
    // 这个回调里，bool=true 表示 A 胜利，false 表示 B 胜利

    // 可选：存一个胜利方，方便后面查询
    private bool isAWin;

    // 所有可能随机到的 KeyCode，可以按照需求自定义
    private KeyCode[] possibleKeys = 
    {
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, 
        KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, 
        KeyCode.N, KeyCode.M
    };

    /// <summary>
    /// 对外暴露的方法：开始比拼并暂停游戏
    /// </summary>
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
        if (playerAKeyText) playerAKeyText.text = "玩家A按键: " + playerAKey.ToString();
        if (playerBKeyText) playerBKeyText.text = "玩家B按键: " + playerBKey.ToString();

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

        while (timer > 0f)
        {
            // 监听按键（用 unscaledTime，因为 timeScale=0）
            if (Input.GetKeyDown(playerAKey))
            {
                playerACount++;
            }
            if (Input.GetKeyDown(playerBKey))
            {
                playerBCount++;
            }

            // 更新倒计时文本
            if (countdownText)
            {
                countdownText.text = "countdown: " + Mathf.CeilToInt(timer).ToString() + "s";
            }

            // 减少计时（用 unscaledDeltaTime）
            timer -= Time.unscaledDeltaTime;

            // 等下一帧
            yield return null;
        }

        // 时间到，结束比拼
        minigameOngoing = false;

        // 判断胜负
        if (playerACount > playerBCount)
        {
            isAWin = true;  // A 胜利
            if (resultText) resultText.text = "A 胜利！";
        }
        else if (playerACount < playerBCount)
        {
            isAWin = false; // B 胜利
            if (resultText) resultText.text = "B 胜利！";
        }
        else
        {
            // 平局处理，这里随便处理一下，或者再加一轮判断
            isAWin = false; 
            if (resultText) resultText.text = "平局！";
        }

        // 调用回调，把结果通知外部
        if (onMinigameEnd != null)
        {
            onMinigameEnd(isAWin);
        }

        // 恢复游戏
        Time.timeScale = 1f;
    }
    
    // 工具方法：从 possibleKeys 中随机取一个 KeyCode
    private KeyCode GetRandomKeyCode()
    {
        int idx = Random.Range(0, possibleKeys.Length);
        return possibleKeys[idx];
    }
}
