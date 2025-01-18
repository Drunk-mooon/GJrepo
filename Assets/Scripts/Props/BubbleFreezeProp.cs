using UnityEngine;

[CreateAssetMenu(fileName = "BubbleFreezeProp", menuName = "Props/BubbleFreezeProp")]
public class BubbleFreezeProp : Prop//将敌方泡泡速度设为0.7倍的道具
{

    public override void ApplyEffect(GameObject player)
    {
        base.ApplyEffect(player);
        
    }

    public override void RemoveEffect(GameObject player)
    {
        base.RemoveEffect(player);
    }
}
