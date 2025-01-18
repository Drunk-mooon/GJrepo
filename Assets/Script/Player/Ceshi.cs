using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceshi : MonoBehaviour
{
    private Queue<KeyCode> keyQueue = new Queue<KeyCode>();
    private int keysNeeded = 3;

    private void Update()
    {
        // 遍历所有的 KeyCode 来检查输入
        KeyCode[] allowedKeys = new KeyCode[] { KeyCode.W, KeyCode.E, KeyCode.R };
        foreach (KeyCode key in allowedKeys)
        {
            if (Input.GetKeyDown(key))
            {
                keyQueue.Enqueue(key);
                if (keyQueue.Count >= keysNeeded)
                {
                    InputKey();
                }
            }
        }
    }

    public string InputKey()
    {
        char[] tempKill = new char[keysNeeded];
        int indexInKill = 0;
        while (indexInKill < keysNeeded)
        {
            tempKill[indexInKill++] = (char)keyQueue.Dequeue();
        }

        // Kill 转 string
        string killString = new string(tempKill);
        Debug.Log(killString);
        return killString;
    }
}