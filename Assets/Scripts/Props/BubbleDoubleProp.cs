using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "DoubleBubbleProp", menuName = "Props/DoubleBubbleProp")]
public class BubbleDoubleProp : Prop
{
    private void OnEnable()
    {
        InitializePlayers();
    }
    public override void ApplyEffect(bool isApplyByPlayerA)
    {
        base.ApplyEffect(isApplyByPlayerA);

        if (isApplyByPlayerA)
        {
            // Start the coroutine to change isDoubleBlow for 5 seconds
            playerA.StartCoroutine(ActivateDoubleBlowForTimeA(duration));
            playerA.playerProp = null;
        }
        else
        {
            playerB.StartCoroutine(ActivateDoubleBlowForTimeB(duration));
            playerB.playerProp = null;
        }
    }

    private IEnumerator ActivateDoubleBlowForTimeA(float t_duration)
    {
        // Activate double blow
        playerA.DoubleStatus = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(t_duration);

        // Deactivate double blow after the duration
        playerA.DoubleStatus = false;
    }

    private IEnumerator ActivateDoubleBlowForTimeB(float t_duration)
    {
        // Activate double blow
        playerB.DoubleStatus = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(t_duration);

        // Deactivate double blow after the duration
        playerB.DoubleStatus = false;
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }
}
