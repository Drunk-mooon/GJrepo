using UnityEngine;

[CreateAssetMenu(fileName = "DoubleBubbleProp", menuName = "Props/DoubleBubbleProp")]
public class BubbleDoubleProp : Prop
{
    public Player playerA;
    public Player playerB;
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
