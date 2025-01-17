using UnityEngine;

[CreateAssetMenu(fileName = "New StrongBubbleProp", menuName = "Props/StrongBubbleProp")]
public class StrongBubbleProp : Prop
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
