using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;


public class Players : MonoBehaviour
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
    public int point;


    void Update()
    {
        BlowBubble();
        KillBubble();
        SpecialItem();
        BubbleWater();
    }
    //吹泡泡
    public void BlowBubble()
    {   
        BBW = new BBW();
        // 首先确保 playerinput 数组不为空，并且至少有一个元素
        if (playerinput != null && playerinput.Length > 0)
        {
            // 检查是否按下了 key1 键
            if (Input.GetKey(playerinput[0].key1))
            {
                if (!isBlowingBubbles)
                {
                    // 开始吹泡泡（Q）
                    isBlowingBubbles = true;
                    timer = 0f;
                }
                // 开始计时并减少泡泡水的数量（Q）
                timer += Time.deltaTime;
                BBWAmount -= Mathf.FloorToInt(BBWSpeed * Time.deltaTime);
                // 确保泡泡水数量不会为负数（Q）
                BBWAmount = Mathf.Max(0, BBWAmount);
            }
            else
            {
                // 当不再按压 Code1 键时，停止吹泡泡（Q）
                isBlowingBubbles = false;
                GetTimer();
            }
        }

    }

    public float GetTimer()
    {//返回时间
        return timer;
    }
    //WER操作组合键
    public void KillBubble(bubble,String code)
    {   KeyCode[] Kill=new KeyCode[] { };
        char[] count =new char[code.length];
        if (playerinput != null && playerinput.Length > 0)
        {for (int n = 0; n< code.Length; n++)
            if (Input.GetKey(playerinput[1].key2))
            {
                Kill[n] = playerinput[1].key2;
            }
            else if(Input.GetKey(playerinput[2].key3))
            {
                Kill[n] = playerinput[1].key3;
            }
            else if(Input.GetKey(playerinput[3].key4))
            {
                Kill[n] = playerinput[1].key4;
            }
            for (int i = 0; i < Kill.Length; i++)
            {
                count[i] = (char)Kill[i];
            }
        }
        for(int k = 0;k < code.Length; k++)
        {
            if (count[k] == code[k]) { }
            else return;
        }
        Destroy bubble;
        point += bubble.point;
        return;
    }
    //释放特殊道具
    public void SpecialItem()
    {
        if (playerinput != null && playerinput.Length > 0)
        {
            SpeciailItem Item = new SpeciailItem();
            // 检查是否按下了 key5键
            if (Input.GetKey(playerinput[4].key5))
            {   //调用特殊道具脚本
                if (Item.exist = true)
                {
                    Item.use();
                    Item.exist = false;
                }

            }
        }

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
                PPWAmount += 10f*Time.deltaTime;
            }
        }
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
