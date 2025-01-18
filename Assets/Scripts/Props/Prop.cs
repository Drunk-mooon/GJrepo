using UnityEngine;

public abstract class Prop : ScriptableObject
{
    public string propName;            //Name
    public Sprite propIcon;            //Icon
    public float duration = 5f;        //Time of the prop effect

    public virtual void ApplyEffect(GameObject player)
    {
        Debug.Log($"{propName} applied to player.");
    }

    public virtual void RemoveEffect(GameObject player)
    {
        Debug.Log($"{propName} effect removed from player.");
    }
}

