using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    public Image redBar;  // Image representing Player 2's score (right part, red)
    public Image blueBar; // Image representing Player 1's score (left part, blue)
    public float totalBarWidth = 1000f;

    public Prop playerAProp;  //调试变量，需直接引用player所拥有的道具
    public Prop playerBProp;

    public PlayerA playerA;
    public PlayerA playerB;

    public PropPanel playerAPropPanel; //!need to change to player!
    public PropPanel playerBPropPanel;
    public TextMeshProUGUI timerText;

    private void Update()
    {
        if (playerAProp != null)
            playerAPropPanel.prop = playerAProp;
        if (playerBProp != null)
            playerBPropPanel.prop = playerBProp;
    }

    public void UpdateTimerUI(float remainingTime)
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {remainingTime:F1}s"; // Update the text with the remaining time
        }
    }

    // Method to update the score bar
    public void UpdateScoreBar(float player1Score, float player2Score)
    {
        // Calculate the total score to find the ratio
        float totalScore = player1Score + player2Score;

        if (totalScore == 0f) return; // Avoid division by zero
        float player1Percentage = player1Score / totalScore; // Blue bar proportion
        float player2Percentage = player2Score / totalScore; // Red bar proportion

        // Update the width of each bar based on the proportions
        float blueBarWidth = totalBarWidth * player1Percentage;
        float redBarWidth = totalBarWidth * player2Percentage;

        // Ensure the total width of red and blue bars adds up to the container width
        blueBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, blueBarWidth);
        redBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, redBarWidth);

        float centerOffset = (totalBarWidth - (blueBarWidth + redBarWidth)) / 2f;

        blueBar.rectTransform.anchoredPosition = new Vector2(-totalBarWidth/2, blueBar.rectTransform.anchoredPosition.y);
        redBar.rectTransform.anchoredPosition = new Vector2(-totalBarWidth / 2 + blueBarWidth, redBar.rectTransform.anchoredPosition.y);
    }
}
