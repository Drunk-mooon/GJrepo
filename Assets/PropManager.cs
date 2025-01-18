using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public BubbleDoubleProp bubbleDoubleProp;
    public BubbleFreezeProp bubbleFreezeProp;
    public BubbleStealProp bubbleStealProp;

    public PlayerA playerA; //need to change the class to PlayerA/B
    public PlayerB playerB;
    
    public void PlayerAGetProp(Prop certainProp)
    {
        playerA.playerProp = certainProp;
    }

    public void PlayerBGetProp(Prop certainProp)
    {
        playerB.playerProp = certainProp;
    }
}
