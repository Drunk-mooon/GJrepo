using UnityEngine;

[CreateAssetMenu(fileName = "DoubleBubbleProp", menuName = "Props/DoubleBubbleProp")]
public class DoubleBubbleProp : Prop
{
    public override void ApplyEffect(GameObject player)
    {
        base.ApplyEffect(player);
        // Add specific logic for stronger bubbles here
    }

    public override void RemoveEffect(GameObject player)
    {
        base.RemoveEffect(player);
    }
}
