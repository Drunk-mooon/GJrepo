using UnityEngine;

[CreateAssetMenu(fileName = "BubbleStealProp", menuName = "Props/BubbleStealProp")]
public class BubbleStealProp : Prop
{
    public int bubbleStealNum = 3;

    public BubblePoolA playerABubblePool; // Reference to Player A's BubblePool
    public BubblePoolB playerBBubblePool; // Reference to Player B's BubblePool 
    private void OnEnable()
    {
        InitializeBulletPool();
        InitializePlayers();
    }
    public override void ApplyEffect(bool isApplyByPlayerA)
    {
        base.ApplyEffect(isApplyByPlayerA);
        SoundManager.AddSound("sound/м╣ещещ", 0, 1);
        InitializeBulletPool();
        InitializePlayers();
        // Add specific logic for stronger bubbles here
        if (isApplyByPlayerA)
        {
            playerBBubblePool.changeBubble(bubbleStealNum);
            playerA.playerProp = null;
        }
        else
        {
            playerABubblePool.changeBubble(bubbleStealNum);
            playerB.playerProp = null;
        }

    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }

    public void InitializeBulletPool()
    {
        // Find the unique PlayerA and PlayerB in the scene
        playerABubblePool = GameObject.FindObjectOfType<BubblePoolA>();
        playerBBubblePool = GameObject.FindObjectOfType<BubblePoolB>();

        // Check if the players were found
        /*
        if (playerABubblePool == null)
        {
            Debug.LogError("steal playerABubblePool not found in the scene.");
        }
        else if (playerBBubblePool == null)
        {
            Debug.LogError("steal playerBBubblePool not found in the scene.");
        }
        else
        {
            Debug.Log("Steal initial success");
        }*/
    }
}
