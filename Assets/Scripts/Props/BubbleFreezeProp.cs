using UnityEngine;

[CreateAssetMenu(fileName = "BubbleFreezeProp", menuName = "Props/BubbleFreezeProp")]
public class BubbleFreezeProp : Prop
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
