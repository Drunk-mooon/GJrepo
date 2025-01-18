using UnityEngine;

[CreateAssetMenu(fileName = "DoubleStealProp", menuName = "Props/DoubleStealProp")]
public class BubbleStealProp : Prop
{
    public override void ApplyEffect(bool isApplyByPlayerA)
    {
        base.ApplyEffect(isApplyByPlayerA);
        // Add specific logic for stronger bubbles here
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }
}
