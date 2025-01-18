using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BubbleFreezeProp", menuName = "Props/BubbleFreezeProp")]
public class BubbleFreezeProp : Prop
{
    public BubblePoolA playerABubblePool; // Reference to Player A's BubblePool
    public BubblePoolA playerBBubblePool; // Reference to Player B's BubblePool

    public float freezeSpeed = 0.7f; // The reduced speed during freeze
    public float freezeDuration = 5f; // Duration of the freeze effect

    public override void ApplyEffect(bool isApplyByPlayerA)
    {
        base.ApplyEffect(isApplyByPlayerA);

        // Choose the correct bubble pool based on the boolean
        BubblePoolA targetBubblePool = isApplyByPlayerA ? playerABubblePool : playerBBubblePool;

        // Start the freeze effect
        if (targetBubblePool != null)
        {
            targetBubblePool.StartCoroutine(FreezeEffect(targetBubblePool));
        }
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        
    }

    private IEnumerator FreezeEffect(BubblePoolA bubblePool)
    {
        // Store the original speed
        float originalSpeed = bubblePool.speedChange;

        // Apply the freeze speed
        bubblePool.speedChange = freezeSpeed;

        // Wait for the freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Reset the speed to its original value
        bubblePool.speedChange = originalSpeed;
    }
}
