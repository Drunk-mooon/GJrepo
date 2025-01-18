using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;


public class PlayerB : MonoBehaviour
{
    public Playerinput[] playerinput2;
    //泡泡水总量100
    public float BBWAmount2 = 100;
    // 用于计时的变量
    private float timer2 = 0f;
    // 吹泡泡的速度，单位为每秒消耗的泡泡水数量
    public float BBWSpeed2 = 10f;
    // 标记是否正在吹泡泡
    private bool isBlowingBubbles2 = false;
    //计分
    public float playerScore2 = 0.1f;
    char[] count2 = new char[3];
    //玩家输入
    public Playerinput[] playerinputin2;
    //输入队列
    private Queue<KeyCode> keyQueue2 = new Queue<KeyCode>();
    //为了道具系统加入的变量
    public Prop playerProp; //道具种类
    public bool isPlayerA = true; //player是否为玩家A

    private List<KeyCode> pressedKeys2 = new List<KeyCode>();
    private int maxKeysToPress2 = 3;
    char[] Kill2 = new char[3];
    Bubble bubble2 = new Bubble();
    void Update()
    {
        // 检查是否按下吹泡泡的键
        if (playerinput2 != null && playerinput2.Length > 0 && Input.GetKey(playerinput2[0].key1))
        {
            BlowBubble();
        }
        // 检查是否按下特殊道具键
        if (playerinput2 != null && playerinput2.Length > 0 && Input.GetKey(playerinput2[4].key5))
        {
            SpecialItem();
        }
        // 检查是否按下蘸泡泡水的键
        if (playerinput2 != null && playerinput2.Length > 0 && Input.GetKey(playerinput2[5].key6))
        {
            BubbleWater();
        }
        if (playerinput2 != null && playerinput2.Length > 0 && Input.GetKey(playerinput2[1].key2) || Input.GetKey(playerinput2[2].key3) || Input.GetKey(playerinput2[3].key4))
        {
            InputKey();
            if (pressedKeys2.Count == maxKeysToPress2)
            {
                if (BubblePoolA.Instance.Bubbles.ContainsKey(Kill2.ToString()))
                {
                    KillBubble(BubblePoolA.Instance.Bubbles[Kill2.ToString()]);
                }
            }
        }
    }

    public string InputKey()
    {
        keyQueue2.Clear();
        int index = 0;
        // 遍历所有的 KeyCode 来处理组合键
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (playerinput2 != null && playerinput2.Length > 0)
            {
                if (Input.GetKey(playerinput2[1].key2))
                {
                    keyQueue2.Enqueue(playerinput2[1].key2);
                }
                else if (Input.GetKey(playerinput2[2].key3))
                {
                    keyQueue2.Enqueue(playerinput2[2].key3);
                }
                else if (Input.GetKey(playerinput2[3].key4))
                {
                    keyQueue2.Enqueue(playerinput2[3].key4);
                }
            }
        }
        char[] kill = new char[3];
        while (keyQueue2.Count > 0 && index < 3)
        {
            Kill2[index++] = (char)keyQueue2.Dequeue();
        }
        //Kill转string
        string killString = new string(Kill2);
        return killString;
    }
    //吹泡泡
    public void BlowBubble()
    {
        // 首先确保 playerinput 数组不为空，并且至少有一个元素
        if (playerinput2 != null && playerinput2.Length > 0)
        {
            // 检查是否按下了 key1 键
            if (Input.GetKey(playerinput2[0].key1))
            {
                if (!isBlowingBubbles2)
                {
                    // 开始吹泡泡（Q）
                    CreateBubble();
                    isBlowingBubbles2 = true;
                    timer2 = 0f;
                }
                // 开始计时并减少泡泡水的数量（Q）
                timer2 += Time.deltaTime;
                BBWAmount2 -= Mathf.FloorToInt(BBWSpeed2 * Time.deltaTime);
                //等于零时泡泡生成失败
                if (BBWAmount2 <= 0)
                {
                    isBlowingBubbles2 = false;
                    timer2 = 0f;
                }
            }
            else
            {
                //一秒最小值
                // 当不再按压 Code1 键时，停止吹泡泡（Q）
                isBlowingBubbles2 = false;
                BubblePoolA.Instance.blowTime = timer2;
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
        if (playerinput2!= null && playerinput2.Length > 0)
        {
            // 检查是否按下了 key6 键
            if (Input.GetKey(playerinput2[5].key6))
            {
                //泡泡水每秒+10
                BBWAmount2 += 10f * Time.deltaTime;
            }
        }
    }
    public void CreateBubble()
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
