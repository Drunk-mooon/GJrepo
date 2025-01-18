using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerA : MonoBehaviour
{
    public Playerinput[] playerinput;
    //泡泡水总量 100
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
    public bool isPlayerA = true; //player是否为玩家 A


    // 存储当前的 BBW 类型索引
    private int currentBBWTypeIndex = 0;


    void Update()
    {
        // 检查是否按下吹泡泡的键
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[0].key1))
        {
            BlowBubble();
        }
        // 检查是否按下特殊道具键
        if (playerinput != null && playerinput.Length > 4 && Input.GetKey(playerinput[4].key5))
        {
            SpecialItem();
        }
        // 检查是否按下蘸泡泡水的键
        if (playerinput != null && playerinput.Length > 5 && Input.GetKey(playerinput[5].key6))
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
        }
    }


    public string InputKey()
    {
        // 此处实现输入键的处理逻辑，可根据需求补充完整
        return "";
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
                if (BBWAmount <= 0)
                {
                    isBlowingBubbles = false;
                    timer = 0f;
                }
            }
            else
            {
                //一秒最小值
                // 当不再按压 Code1 键时，停止吹泡泡（Q）
                isBlowingBubbles = false;
                // 此处可添加更多停止吹泡泡的逻辑
            }
        }
    }


    //释放特殊道具
    public void SpecialItem()
    {/*
        if (playerinput != null && playerinput.Length > 0)
        {
            // 检查是否按下了 key5 键
            if (Input.GetKey(playerinput[4].key5))
            {
                //调用特殊道具脚本
                if (this.playerProp != null)
                {
                    playerProp.ApplyEffect();
                    playerProp = null;
                }
            }
        }*/
    }


    //蘸泡泡水
    public void BubbleWater()
    {
        if (playerinput != null && playerinput.Length > 0)
        {
            // 检查是否按下了 key6 键
            if (Input.GetKeyDown(playerinput[5].key6))
            {
                // 切换 BBW 类型
                SwitchBBWType();
                //泡泡水每秒 +10
                BBWAmount += 10f * Time.deltaTime;
            }
        }
    }


    // 切换 BBW 类型的方法
    private E_bType SwitchBBWType()
    {
        E_bType[] BBWTypes = (E_bType[])System.Enum.GetValues(typeof(E_bType));
        currentBBWTypeIndex = (currentBBWTypeIndex + 1) % BBWTypes.Length;
        E_bType currentBBWType = BBWTypes[currentBBWTypeIndex];


        switch (currentBBWType)
        {
            case E_bType.blue:
                Debug.Log("The bubble water type is blue.");
                return E_bType.blue;       
            case E_bType.green:
                Debug.Log("The bubble water type is green.");
                return E_bType.green;
            case E_bType.white:
                Debug.Log("The bubble water type is white.");
                return E_bType.white;
            case E_bType.pink:
                Debug.Log("The bubble water type is pink.");
                return E_bType.pink;
            default:
                // 处理未预期的枚举值
                Debug.Log("Unexpected bubble water type.");
                return E_bType.white;

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