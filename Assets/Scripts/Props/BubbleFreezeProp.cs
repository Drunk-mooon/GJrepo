using UnityEngine;
using System.Collections;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "BubbleFreezeProp", menuName = "Props/BubbleFreezeProp")]
public class BubbleFreezeProp : Prop
{
    public BubblePoolA playerABubblePool; // Reference to Player A's BubblePool
    public BubblePoolB playerBBubblePool; // Reference to Player B's BubblePool

    public float freezeSpeed = 0.1f; // The reduced speed during freeze
    public float freezeDuration = 5f; // Duration of the freeze effect

    private void OnEnable()
    {
        InitializeBulletPool();
        InitializePlayers();
    }
    public override void ApplyEffect(bool isApplyByPlayerA)
    {
        base.ApplyEffect(isApplyByPlayerA);
        InitializeBulletPool();
        InitializePlayers();
        SoundManager.AddSound("sound/¼õËÙÄ§·¨", 0, 1);
        // Start the freeze effect
        if (isApplyByPlayerA)
        {
            playerBBubblePool.StartCoroutine(FreezeEffectB(playerBBubblePool));
            playerA.playerProp = null;
        }
        else if(!isApplyByPlayerA)
        {
            playerBBubblePool.StartCoroutine(FreezeEffectA(playerABubblePool));
            playerB.playerProp = null;
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

    private IEnumerator FreezeEffectB(BubblePoolB bubblePool)
    {
        Debug.Log("FreezeB!");
        // Store the original speed
        float originalSpeed = bubblePool.speedChange;

        // Apply the freeze speed
        bubblePool.speedChange = freezeSpeed;

        // Wait for the freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Reset the speed to its original value
        bubblePool.speedChange = originalSpeed;
    }

    public void InitializeBulletPool()
    {
        // Find the unique PlayerA and PlayerB in the scene
        playerABubblePool = GameObject.FindObjectOfType<BubblePoolA>();
        playerBBubblePool = GameObject.FindObjectOfType<BubblePoolB>();
        /*
        // Check if the players were found
        if (playerABubblePool == null)
        {
            Debug.LogError("freeze playerABubblePool not found in the scene.");
        }
        else if (playerBBubblePool == null)
        {
            Debug.LogError("freeze playerBBubblePool not found in the scene.");
        }
        else
        {
            Debug.Log("freeze initial success");
        }*/
    }
}
