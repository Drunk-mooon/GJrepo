using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerA : MonoBehaviour
{
    private Coroutine blowBubbleCoroutine; // Store reference to the coroutine

    public Playerinput[] playerinput;
    //���ݰ����� 100,��ʼֵ0
    public float BBWAmount = 0;
    // ���ڼ�ʱ�ı���
    private float timer = 0f;
    // �����ݵ��ٶȣ���λΪÿ�����ĵ�����ˮ����
    public float BBWSpeed = 10f;
    // ����Ƿ����ڴ�����
    private bool isBlowingBubbles = false;
    //�Ʒ�
    public float playerScore = 0.1f;
    char[] count = new char[3];
    //�������
    public Playerinput[] playerinputin;
    //�������
    private Queue<KeyCode> keyQueue = new Queue<KeyCode>();
    //Ϊ�˵���ϵͳ����ı���
    public Prop playerProp; //��������
    public bool isPlayerA = true; //player�Ƿ�Ϊ��� A

    public bool isDoubleBlow=false; 
    // �洢��ǰ�� BBW ��������
    private int currentBBWTypeIndex = 0;
    E_bType BBWType = E_bType.white;
    char[] Kill = new char[3];

    private Coroutine waterCoroutine;
    private void Start()
    {
        Debug.Log(BBWAmount);
    }
    void Update()
    {
        //����Ƿ����������
        KeyCode[] allowedKeys = new KeyCode[] { KeyCode.W, KeyCode.E, KeyCode.R};
        foreach (KeyCode key in allowedKeys)
        {
            if (Input.GetKeyDown(key))
            {
                keyQueue.Enqueue(key);
                if (keyQueue.Count >= 3)
                {
                    string tempinput = InputKey();
                    if (BubblePoolA.Instance.Bubbles.ContainsKey(tempinput))
                        KillBubble(tempinput);
                }
               
            }
            
        }
            // ����Ƿ��´����ݵļ�
            if (Input.GetKeyDown(KeyCode.Q))
            {
                BlowBubble();
            }
            // ����Ƿ���������߼�
            if (Input.GetKey(KeyCode.A))
            {
                SpecialItem();
            }
            // ����Ƿ���ѡ����ˮ�ļ�
            if (Input.GetKey(KeyCode.S))
            {
                BubbleWater();
            }
            // ����Ƿ���պ����ˮ�ļ�
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
            tempKill[indexInKill] = (char)item;
            indexInKill++;
        }
        keyQueue.Dequeue();
        // Kill ת string
        string killString = new string(tempKill);
        Debug.Log(killString);
        return killString;
    }


    //������
    public void BlowBubble()
    {
        // Start the coroutine for growing the bubble when the key is pressed
        if (blowBubbleCoroutine == null)  // Prevent starting multiple coroutines
        {
            blowBubbleCoroutine = StartCoroutine(GrowBubble());
        }
    }
    private IEnumerator GrowBubble()
    {
        float growthRate = 0.1f; // Rate of bubble growth
        timer = 0f;

        // While the key is held down, continue growing the bubble
        while (Input.GetKey(KeyCode.Q)) // Continue while the key is pressed
        {
            // Grow the bubble
            Vector3 currentScale = transform.localScale;
            float newRadius = Mathf.Max(0, currentScale.x + growthRate * Time.deltaTime);
            transform.localScale = new Vector3(newRadius, newRadius, newRadius);

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

            // Wait for the next frame
            yield return null;
        }

        // When the key is released, stop growing and instantiate the bubble
        if (BBWAmount > 0)  // If there's enough BBWAmount, instantiate the bubble
        {
            BubblePoolA.Instance.blowTime = timer;
            BubblePoolA.Instance.bType = BBWType;
            CreateBubble(true, timer, BBWType);
        }

        // Reset the timer after the bubble is instantiated
        timer = 0f;

        // Reset the coroutine reference
        blowBubbleCoroutine = null;
    }


    public void KillBubble(string killString)
    {
        BubblePoolA.Instance.PutObj(BubblePoolA.Instance.Bubbles[killString]);
    }

    //�ͷ��������
    public void SpecialItem()
    {/*
        if (playerinput != null && playerinput.Length > 0)
        {
            // ����Ƿ����� key5 ��
            if (Input.GetKey(playerinput[4].key5))
            {
                //����������߽ű�
                if (this.playerProp != null)
                {
                    playerProp.ApplyEffect();
                    playerProp = null;
                }
            }
        }*/
    }


    //ѡ����ˮ
    public void BubbleWater()
    {
        if (playerinput != null && playerinput.Length > 0)
        {
            // ����Ƿ����� key6 ��
            if (Input.GetKeyDown(KeyCode.S))
            {
                // �л� BBW ����
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
                    BBWAmount += BBWSpeed * Time.deltaTime;
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

    // �л� BBW ���͵ķ���
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
                // ����δԤ�ڵ�ö��ֵ
                Debug.Log("Unexpected bubble water type.");
                return E_bType.white;

        }

    }
    [System.Serializable]
    // Ƕ���� Playerinput
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
    private void CreateBubble(bool isa,float blowTime,E_bType type)
    {
        Bubble newBubble = Instantiate(Resources.Load<Bubble>("BubblePrefab"));

        if (newBubble != null)
        {
            // ���´����� Bubble ������г�ʼ��
            newBubble.Set(isa);
            newBubble.Init(blowTime);
            // ���� fly ����
            newBubble.fly();
        }
        else
        {
            Debug.LogError("Failed to instantiate Bubble object.");
        }
    }

}