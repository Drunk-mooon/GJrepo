using UnityEngine;

public abstract class Prop : ScriptableObject
{
    public string propName;            //Name
    public Sprite propIcon;            //Icon
    public float duration = 5f;        //Time of the prop effect
    public PlayerA playerA;
    public PlayerB playerB;
    public virtual void ApplyEffect(bool isApplyByPlayerA)
    {
        Debug.Log($"{propName} applied to player.");
    }

    public virtual void RemoveEffect()
    {
        Debug.Log($"{propName} effect removed from player.");
    }

    public void InitializePlayers()
    {
        // Find the unique PlayerA and PlayerB in the scene
        playerA = GameObject.FindObjectOfType<PlayerA>();
        playerB = GameObject.FindObjectOfType<PlayerB>();

        // Check if the players were found
        /*
        if (playerA == null)
        {
            Debug.LogError("player PlayerA not found in the scene.");
        }
        else if (playerB == null)
        {
            Debug.LogError("player PlayerB not found in the scene.");
        }
        else
        {
            Debug.Log("player success.");
        }*/
    }
}

