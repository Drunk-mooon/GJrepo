using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerA : MonoBehaviour
{
    private Coroutine blowBubbleCoroutine; // Store reference to the coroutine

    public Playerinput[] playerinput;
    //泡泡棒总量 100,初始值0
    public float BBWAmount = 0;
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
    //泡泡最大体积
    public Vector2 maxBubbleSize = new Vector2(100f,100f);
    //是否为绿色泡泡双倍效果
    public bool isDoubleBlow=false; 
    // 存储当前的 BBW 类型索引
    private int currentBBWTypeIndex = 0;
    E_bType BBWType = E_bType.white;
    char[] Kill = new char[3];

    private Coroutine waterCoroutine;
    private void Start()
    {

    }
    void Update()
    {
        //检查是否输入操作键
        KeyCode[] allowedKeys = new KeyCode[] { KeyCode.W, KeyCode.E, KeyCode.R};
        foreach (KeyCode key in allowedKeys)
        {
            if (Input.GetKeyDown(key))
            {
                keyQueue.Enqueue(key);
                if (keyQueue.Count >= 3)
                {
                    string tempinput = InputKey();
                  //  if (BubblePoolA.Instance.Bubbles.ContainsKey(tempinput))
                        KillBubble(tempinput);
                }
               
            }
            
        }
            // 检查是否按下吹泡泡的键
            if (Input.GetKeyDown(KeyCode.Q))
            {
                
                BlowBubble();
            }
            // 检查是否按下特殊道具键
            if (Input.GetKey(KeyCode.A))
            {
                SpecialItem();
            }
            // 检查是否按下选泡泡水的键
            if (Input.GetKey(KeyCode.S))
            {
                BubbleWater();
            }
            // 检查是否按下蘸泡泡水的键
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log(BBWAmount);
                UseWater();
                Debug.Log(BBWAmount);
            }
        
    }


    public string InputKey()
    {
        char[] tempKill = new char[3];
        int indexInKill = 0;
        foreach (var item in keyQueue)
        {
            switch ((char)item)
            {
                case 'w':
                    tempKill[indexInKill] = '1';
                    break;
                case 'e':
                    tempKill[indexInKill] = '2';
                    break;
                case 'r':
                    tempKill[indexInKill] = '3';
                    break;
                default:
                    tempKill[indexInKill] = (char)item;
                    break;
            }
            indexInKill++;

        }
        keyQueue.Dequeue();
        // Kill 转 string
        string killString = new string(tempKill);
        BubblePoolA.Instance.attackCode = killString;
        return killString;
    }


    //吹泡泡
    public void BlowBubble()
    {
        // Start the coroutine for growing the bubble when the key is pressed
        if (blowBubbleCoroutine == null)  // Prevent starting multiple coroutines
        {
            blowBubbleCoroutine = StartCoroutine(GrowBubble());
        }
    }
    private IEnumerator GrowBubble()
    {    //白色

        float growthRate = 0.1f; // Rate of bubble growth
        timer = 0f;
        while (timer < 1f)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        // While the key is held down, continue growing the bubble
        while (Input.GetKey(KeyCode.Q)) // Continue while the key is pressed
        {
            if (transform.localScale.x < maxBubbleSize.x)
            {
                // Grow the bubble
                Vector3 currentScale = transform.localScale;
                float newRadius = Mathf.Max(0, currentScale.x + growthRate * Time.deltaTime);
                transform.localScale = new Vector2(newRadius, newRadius);

                // Deduct BBWAmount while the bubble grows
                BBWAmount -= Mathf.FloorToInt(BBWSpeed * Time.deltaTime);
                BBWAmount = Mathf.Max(0, BBWAmount); // Ensure BBWAmount doesn't go below 0

                // If BBWAmount is 0, stop growing the bubble and exit the loop
                if (BBWAmount <= 0)
                {
                    break; // Exit the coroutine and stop bubble growth
                }
                // Increment the timer as the bubble grows
                timer += Time.deltaTime;
            }
            if (timer < 1f)
            {
                break;
            }
            // Wait for the next frame
            yield return null;
        }
        Debug.Log(BBWAmount);
        // When the key is released, stop growing and instantiate the bubble
        if (BBWAmount > 0)  // If there's enough BBWAmount, instantiate the bubble
        {
            BubblePoolA.Instance.blowTime = timer;
            BubblePoolA.Instance.bType = BBWType;
            BubblePoolA.Instance.GetObj();
        }

        // Reset the timer after the bubble is instantiated
        timer = 0f;

        // Reset the coroutine reference
        blowBubbleCoroutine = null;
    }


    public void KillBubble(string killString)
    {
        BubblePoolA.Instance.PutObj(BubblePoolA.Instance.Bubbles[killString]);
        /*foreach (var item in BubblePoolA.Instance.Bubbles)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }*/
        keyQueue.Dequeue();
        keyQueue.Dequeue();
    }





    //释放特殊道具
    public void SpecialItem()
    {
        while (Input.GetKey(KeyCode.Q))
        {
            if (playerinput != null && playerinput.Length > 0)
            {
                // 检查是否按下了 key6 键
                if (Input.GetKeyDown(KeyCode.S))
                {
                   
                }
            }
        }
       
    }


    //选泡泡水
    public void BubbleWater()
    {
        if (playerinput != null && playerinput.Length > 0)
        {
            // 检查是否按下了S 键
            if (Input.GetKeyDown(KeyCode.S))
            {
                // 切换 BBW 类型
                BBWType=SwitchBBWType();
                BubblePoolA.Instance.bType = BBWType;
            }
        }
    }
    public void UseWater()
    {
        // Start the coroutine only if it's not already running
        if (waterCoroutine == null)
        {
            waterCoroutine = StartCoroutine(ManageWaterCoroutine());
        }
    }

    private IEnumerator ManageWaterCoroutine()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                // Increase BBWAmount while the key is held down
                if (BBWAmount < 100)
                {
                    if (BubblePoolA.Instance.bType == E_bType.white)
                    {
                        BBWAmount += BBWSpeed * Time.deltaTime * 5;
                    }
                    else
                    {
                        BBWAmount += BBWSpeed * Time.deltaTime;
                    }
                    BBWAmount = Mathf.Min(BBWAmount, 100); // Clamp to max value
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                // Stop the coroutine when the key is released
                StopCoroutine(waterCoroutine);
                waterCoroutine = null;
                yield break;
            }
            yield return null; // Wait for the next frame
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
        public KeyCode key7;
        public int point;
    }
   
}