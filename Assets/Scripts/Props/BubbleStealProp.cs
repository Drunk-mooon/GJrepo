using UnityEngine;

[CreateAssetMenu(fileName = "DoubleStealProp", menuName = "Props/DoubleStealProp")]
public class BubbleStealProp : Prop
{
    public int bubbleStealNum = 3;

    public BubblePoolA playerABubblePool; // Reference to Player A's BubblePool
    public BubblePoolA playerBBubblePool; // Reference to Player B's BubblePool

    public override void ApplyEffect(bool isApplyByPlayerA)
    {
        base.ApplyEffect(isApplyByPlayerA);
        // Add specific logic for stronger bubbles here
        /*
        if (isApplyByPlayerA)                   NeedToChange!
            playerBBubblePool.changeBubble(bubbleStealNum);
        else
            playerABubblePool.changeBubble(bubbleStealNum);
        */
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }
}
