using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public BubbleDoubleProp bubbleDoubleProp;
    public BubbleFreezeProp bubbleFreezeProp;
    public BubbleStealProp bubbleStealProp;

    public Player playerA; //need to change the class to PlayerA/B
    public Player playerB;
    
    public void PlayerAGetProp(Prop certainProp)
    {
        //playerA.playerProp = certainProp  NeedToChange!
    }
}
