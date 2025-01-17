using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howchillTest : MonoBehaviour
{
    // Start is called before the first frame update
    Queue<Bubble> bubbles = new Queue<Bubble>();
    void Start()
    {
        BubblePoolA.Instance.blowTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {   
        
        if (Input.GetMouseButtonDown(0))
        {
            bubbles.Enqueue( BubblePoolA.Instance.GetObj());
            Debug.Log("s");
        }

        if (Input.GetMouseButtonDown(1))
        {

            Debug.Log("a");
        }
    }
}
