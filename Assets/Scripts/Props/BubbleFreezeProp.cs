using UnityEngine;

[CreateAssetMenu(fileName = "BubbleFreezeProp", menuName = "Props/BubbleFreezeProp")]
public class BubbleFreezeProp : Prop//���з������ٶ���Ϊ0.7���ĵ���
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
