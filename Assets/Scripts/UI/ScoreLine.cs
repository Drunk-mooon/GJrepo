using UnityEngine;

public class ScoreLine : MonoBehaviour
{
    // Reference to the players
    public PlayerA playerA;
    public PlayerB playerB;

    public float greenBubbleEffect = 0.1f;

    // Trigger event for scoring
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has a Bubble component
        Bubble bubble = collision.GetComponent<Bubble>();
        if (bubble != null)
        {
            // Determine which player gets the score
            if (bubble.isA)
            {
                // Bubble belongs to Player A
                /*
                if(bubble.btype = E_type.green)
                    playerA.playerScore*=(1+greenBubbleEffect); 
                else
                */
                    playerA.playerScore += bubble.score;
                
            }
            else
            {
                // Bubble belongs to Player B
                /*
                if(bubble.btype = E_type.green)
                    playerB.playerScore*=(1+greenBubbleEffect); 
                else
                */
                    playerB.playerScore2 += bubble.score;
            }
            // Optionally, destroy the bubble after scoring
            bubble.index = 0;
            if (bubble.isA)
            {
                BubblePoolA.Instance.PutObj(bubble);
            }
            else
                BubblePoolB.Instance.PutObj(bubble); //NeedToChange!
        }
    }
}
