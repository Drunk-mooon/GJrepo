using UnityEngine;
public class PropManager : MonoBehaviour
{
    public BubbleDoubleProp bubbleDoubleProp;
    public BubbleFreezeProp bubbleFreezeProp;
    public BubbleStealProp bubbleStealProp;

    public PlayerA playerA; //need to change the class to PlayerA/B
    public PlayerB playerB;
    
    public void PlayerAGetProp()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomValue;
        if (playerB.playerProp = bubbleStealProp)
        {
            randomValue = Random.Range(0, 2);
        }
        else
            randomValue = Random.Range(0, 3);
        if (randomValue == 0)
        {
            playerA.playerProp = bubbleDoubleProp;
            //Debug.Log(randomValue);
        }
        else if (randomValue == 1)
        {
            playerA.playerProp = bubbleFreezeProp;
            //Debug.Log(randomValue);
        }
        else if (randomValue == 2)
        {
            playerA.playerProp = bubbleStealProp;
            //Debug.Log(randomValue);
        }
    }

    public void PlayerBGetProp()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomValue;
        if (playerA.playerProp = bubbleStealProp)
        {
            randomValue = Random.Range(0, 2);
        }
        else
            randomValue = Random.Range(0, 3);
        if (randomValue == 0)
        {
            playerB.playerProp = bubbleDoubleProp;
            //Debug.Log(randomValue);
        }
        else if (randomValue == 1)
        {
            playerB.playerProp = bubbleFreezeProp;
            //Debug.Log(randomValue);
        }
        else if (randomValue == 2)
        {
            playerB.playerProp = bubbleStealProp;
            //Debug.Log(randomValue);
        }
    }
}
