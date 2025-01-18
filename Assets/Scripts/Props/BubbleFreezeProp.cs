using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BubbleFreezeProp", menuName = "Props/BubbleFreezeProp")]
public class BubbleFreezeProp : Prop
{
    public BubblePoolA playerABubblePool; // Reference to Player A's BubblePool
    public BubblePoolA playerBBubblePool; // Reference to Player B's BubblePool   NeedToChange! to certain pool class

    public float freezeSpeed = 0.7f; // The reduced speed during freeze
    public float freezeDuration = 5f; // Duration of the freeze effect

    public override void ApplyEffect(bool isApplyByPlayerA)
    {
        base.ApplyEffect(isApplyByPlayerA);

        // Start the freeze effect
        if (isApplyByPlayerA)
        {
            playerBBubblePool.StartCoroutine(FreezeEffectB(playerBBubblePool));
        }
        else if(!isApplyByPlayerA)
        {
            playerBBubblePool.StartCoroutine(FreezeEffectA(playerABubblePool));
        }
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        
    }

    private IEnumerator FreezeEffectA(BubblePoolA bubblePool)
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

    private IEnumerator FreezeEffectB(BubblePoolA bubblePool)
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
