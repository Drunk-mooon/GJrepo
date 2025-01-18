using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;


public class PlayerA : MonoBehaviour
{
    public Playerinput[] playerinput;
    //泡泡水总量100
    public float BBWAmount = 100;
    // 用于计时的变量
    private float timer = 0f;
    // 吹泡泡的速度，单位为每秒消耗的泡泡水数量
    public float BBWSpeed = 10f;
    // 标记是否正在吹泡泡
    private bool isBlowingBubbles = false;
    //计分
    public float playerScore = 0.1f;
    char[] count = new char[3];
    //玩家输入
    public Playerinput[] playerinputin;
    //输入队列
    private Queue<KeyCode> keyQueue = new Queue<KeyCode>();
    //为了道具系统加入的变量
    public Prop playerProp; //道具种类
    public bool isPlayerA = true; //player是否为玩家A

    private List<KeyCode> pressedKeys = new List<KeyCode>();
    private int maxKeysToPress = 3;
    char[] Kill = new char[3];
    Bubble bubble = new Bubble();
    void Update()
    {
        // 检查是否按下吹泡泡的键
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[0].key1))
        {
            BlowBubble();
        }
        // 检查是否按下特殊道具键
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[4].key5))
        {
            SpecialItem();
        }
        // 检查是否按下蘸泡泡水的键
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[5].key6))
        {
            BubbleWater();
        }
        //检查是否输入操作键
        KeyCode[] allowedKeys = new KeyCode[] { KeyCode.W, KeyCode.E, KeyCode.R };
        foreach (KeyCode key in allowedKeys)
        {
            if (Input.GetKeyDown(key))
            {
                keyQueue.Enqueue(key);
                if (keyQueue.Count >= 3)
                    InputKey();
            }

            if (pressedKeys.Count == maxKeysToPress)
            {
                if (BubblePoolA.Instance.Bubbles.ContainsKey(Kill.ToString()))
                {
                    KillBubble(BubblePoolA.Instance.Bubbles[Kill.ToString()]);
                }
            }
        }
    }

    public string InputKey()
    {
        char[] tempKill = new char[3];
        int indexInKill = 0;
        while (indexInKill < 3)
        {
            tempKill[indexInKill++] = (char)keyQueue.Dequeue();
        }

        // Kill 转 string
        string killString = new string(tempKill);
        Debug.Log(killString);
        return killString;
    
}
    //吹泡泡
    public void BlowBubble()
    {   
        // 首先确保 playerinput 数组不为空，并且至少有一个元素
        if (playerinput != null && playerinput.Length > 0)
        {
            // 检查是否按下了 key1 键
            if (Input.GetKey(playerinput[0].key1))
            {
                if (!isBlowingBubbles)
                {
                    // 开始吹泡泡（Q）
                    creatBubble();
                    isBlowingBubbles = true;
                    timer = 0f;
                }
                // 开始计时并减少泡泡水的数量（Q）
                timer += Time.deltaTime;
                BBWAmount -= Mathf.FloorToInt(BBWSpeed * Time.deltaTime);
                //等于零时泡泡生成失败
                if(BBWAmount<=0)
                {
                    isBlowingBubbles=false;
                    timer = 0f;
                }
            }
            else
            {
                //一秒最小值
                // 当不再按压 Code1 键时，停止吹泡泡（Q）
                isBlowingBubbles = false;
                BubblePoolA.Instance.blowTime = timer;
                //销毁泡泡模型
                BubblePoolA.Instance.GetObj();
            }
        }

    }




    //WER操作组合键
    public void KillBubble(Bubble bubble)
    {/* 
        string number;
        bool match = true;
        for(int k = 0;k < Mathf.Min(3,index); k++)
         {
             if (k >= bubble.code.Length || Kill[k] != bubble.code[k])
             {
                 match = false;
                 break;
             }
         }
         if(match)
         { string nm="";
             foreach (char num in Kill){
                 if (num == 'W') nm += 1;
                    if(num=='E') nm += 2;
                    if(num=='R')nm += 3;
             }
             Kill = nm.ToCharArray();  
         }*/
        BubblePoolA.Instance.PutObj(bubble);
    }
    //释放特殊道具
    public void SpecialItem()
    {/*
        if (playerinput != null && playerinput.Length > 0)
        {
            SpeciailItem Item = new SpeciailItem();
            // 检查是否按下了 key5键
            if (Input.GetKey(playerinput[4].key5))
            {   //调用特殊道具脚本
                if (this.playerProp!=null)
                {
                    playerProp.ApplyEffect();
                    playerProp=null;
                }

            }
        }
        */
    }
    //蘸泡泡水
    public void BubbleWater()
    {
        if (playerinput != null && playerinput.Length > 0)
        {
            // 检查是否按下了 key6 键
            if (Input.GetKey(playerinput[5].key6))
            {
                //泡泡水每秒+10
                BBWAmount += 10f*Time.deltaTime;
            }
        }
    }
    public void creatBubble()
    {
    float growthRate = 0.1f;
    Vector3 currentScale = transform.localScale;
    // 计算新的半径
    float newRadius = Mathf.Max(0, currentScale.x + growthRate * Time.deltaTime);
    // 确保半径不为负数
    currentScale = new Vector3(newRadius, newRadius, newRadius);
    // 应用新的缩放
    transform.localScale = currentScale;
        //停
    }

    [System.Serializable]
    // 嵌套类 Playerinput
    public class Playerinput
    {
        public KeyCode key1;
        public KeyCode key2;
        public KeyCode key3;
        public KeyCode key4;
        public KeyCode key5;
        public KeyCode key6;
        public int point;

    }

}
