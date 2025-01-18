using UnityEngine;

public class MinigameTest : MonoBehaviour
{
    public Minigame minigame; // 在Inspector中把MinigameManager拖进来

    void Update()
    {
        // 启动小游戏
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 调用StartMinigame，并给它传一个回调函数
            minigame.StartMinigame(OnMinigameEnd);
        }
    }

    // 在小游戏结束时被调用
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
}