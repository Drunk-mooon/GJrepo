using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using TMPro;
public class PlayerA : MonoBehaviour
{
    private Coroutine blowBubbleCoroutine; // Store reference to the coroutine
    public GameObject gameObject;
    public Playerinput[] playerinput;
    //���ݰ����� 100,��ʼֵ0
    public float BBWAmount = 100;
    // ���ڼ�ʱ�ı���
    private float timer = 0f;
    // �����ݵ��ٶȣ���λΪÿ�����ĵ�����ˮ����
    public float BBWSpeed = 10f;
    // ����Ƿ����ڴ�����
    private bool isBlowingBubbles = false;
    private bool isGettingBubbleWater = false;
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
    //����������
    public Vector2 maxBubbleSize = new Vector2(100f,100f);
    // �洢��ǰ�� BBW ��������
    private int currentBBWTypeIndex = 0;
    E_bType BBWType = E_bType.blue;
    char[] Kill = new char[3];

    private Coroutine waterCoroutine;
    //˫��״̬
    public bool DoubleStatus = false;
    //WaterBubbleSelect
    public WaterBullbleSelect waterBubbleSelect;
    //պ����ˮ���
    
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
                        KillBubble(tempinput);
                }
               
            }
            
        }
            // ����Ƿ��´����ݵļ�
            if (Input.GetKeyDown(KeyCode.Q) && !isGettingBubbleWater)
            {
                
                BlowBubble();
            }
            // ����Ƿ���������߼�
            if (Input.GetKeyDown(KeyCode.A))
            {
                SpecialItem();
            }
            // ����Ƿ���ѡ����ˮ�ļ�
            if (Input.GetKeyDown(KeyCode.S) && !isGettingBubbleWater && !isBlowingBubbles)
            {
                BubbleWater();
            }
            // ����Ƿ���պ����ˮ�ļ�
            if (Input.GetKeyDown(KeyCode.D) && !isBlowingBubbles)
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
        // Kill ת string
        string killString = new string(tempKill);
        BubblePoolA.Instance.attackCode = killString;
        BubblePoolB.Instance.attackCode = killString;
        return killString;
    }


    //������
    public void BlowBubble()
    {
        isBlowingBubbles = true;
        // Start the coroutine for growing the bubble when the key is pressed
        if (blowBubbleCoroutine == null)  // Prevent starting multiple coroutines
        {
            blowBubbleCoroutine = StartCoroutine(GrowBubble());
        }
    }
    public GameObject bubblePrefab;
    public float offsetX = -3f; // Small offset along the X axis
    public float offsetY = 0f; // Small offset along the Y axis
    public Transform generatingTransform;
    public float leastPressTime = 0.2f;
    private IEnumerator GrowBubble()
    {
        float growthRate = 0.1f; // Rate of bubble growth
        timer = 0f;
        bool keyHeldDown = false;
        float maxGrowTime = 100 / (BBWSpeed * 2f);

        // Instantiate a bubble sprite (initially small)
        GameObject bubbleSprite = Instantiate(bubblePrefab);

        // Start at the player's position with offsets in both the X and Y directions
        bubbleSprite.GetComponentInChildren<TextMeshPro>().text = "";
        bubbleSprite.transform.position = generatingTransform.position + new Vector3(offsetX, offsetY, 0f); // Add both offsetX and offsetY
        bubbleSprite.transform.localScale = Vector3.zero; // Start with no size

        // While the key is held down, continue growing the bubble
        while (Input.GetKey(KeyCode.Q)) // Continue while the key is pressed
        {
            keyHeldDown = true;

            // Deduct BBWAmount while the bubble grows
            BBWAmount -= BBWSpeed * Time.deltaTime * 2;
            if (transform.localScale.x < maxBubbleSize.x)
            {
                // Deduct BBWAmount while the bubble grows
                BBWAmount -= Mathf.FloorToInt(BBWSpeed * Time.deltaTime);
                BBWAmount = Mathf.Max(0, BBWAmount); // Ensure BBWAmount doesn't go below 0

                // If BBWAmount is 0 or less, stop growing the bubble and exit the loop
                if (BBWAmount <= 0)
                {
                    isBlowingBubbles = false;
                    break; // Exit the coroutine and stop bubble growth
                }

                // Increment the timer as the bubble grows
                timer += Time.deltaTime;

                // Update the bubble sprite size based on the timer
                float sizeFactor = Mathf.Lerp(0f, maxBubbleSize.x, timer / maxGrowTime);
                bubbleSprite.transform.localScale = new Vector3(sizeFactor, sizeFactor, 1f);

                yield return null; // Wait for the next frame
            }
            else
            {
                isBlowingBubbles = false;
                break;
            }

            // Wait for the next frame
            yield return null;
        }

        keyHeldDown = false;

        if (!keyHeldDown && timer < leastPressTime)
        {
            // Reset the timer if the key was released early
            timer = 0f;
            Destroy(bubbleSprite); // Destroy the bubble sprite if the key was released early
            isBlowingBubbles = false;
            blowBubbleCoroutine = null;
            Debug.Log("timer <= leastPressTime");
        }
        else if (!keyHeldDown)
        {
            Debug.Log("timer > 1");
            // When the key is released and the bubble was successfully grown
            if (BBWAmount > 0) // If there's enough BBWAmount, instantiate the bubble
            {
                Debug.Log("generating bb");
                BubblePoolA.Instance.blowTime = timer;
                BubblePoolA.Instance.bType = BBWType;
                BubblePoolA.Instance.trans = bubbleSprite.transform; // Set the transform of the bubble
                BubblePoolA.Instance.GetObj(); // Generate the bubble from the pool
            }

            // Double the bubble (if DoubleStatus is true)
            if (DoubleStatus == true)
            {
                BubblePoolA.Instance.GetObj();
            }

            // Destroy the bubble sprite after using it
            Destroy(bubbleSprite);

            // Reset the timer after the bubble is instantiated
            timer = 0f;
            isBlowingBubbles = false;
            blowBubbleCoroutine = null;
        }
    }

    public void KillBubble(string killString)
    {
        if (BubblePoolA.Instance.Bubbles.ContainsKey(killString) || BubblePoolB.Instance.Bubbles.ContainsKey(killString)) 
        {
            keyQueue.Dequeue();
            keyQueue.Dequeue();
        }
        if (BubblePoolA.Instance.Bubbles.ContainsKey(killString))
            BubblePoolA.Instance.PutObj(BubblePoolA.Instance.Bubbles[killString]);
        else
            BubblePoolB.Instance.PutObj(BubblePoolB.Instance.Bubbles[killString]);
        /*foreach (var item in BubblePoolA.Instance.Bubbles)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }
        foreach (var item in BubblePoolB.Instance.Bubbles)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }*/

    }





    //�ͷ��������
    public void SpecialItem()
    {   
            if (playerProp != null)
            {
                playerProp.ApplyEffect(isPlayerA);
            }   
    }


    //ѡ����ˮ
    public void BubbleWater()
    {
        bool isMoving = waterBubbleSelect.isMoving;
        // �л� BBW ����
        if (waterBubbleSelect != null && !isMoving)
            {
                // �л� BBW ����
                BBWType = SwitchBBWType();
                // ��ʼ��ת����
                if (!isMoving)
                {
                    waterBubbleSelect.StartRotationCycle();
                    isMoving = true;
                }
                BubblePoolA.Instance.bType = BBWType;
            }
    }
    
    public void UseWater()
    {
        isGettingBubbleWater = true;
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
                isGettingBubbleWater = false;
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
            case E_bType.pink:
                Debug.Log("The bubble water type is pink.");
                return E_bType.pink;
            default:
                // ����δԤ�ڵ�ö��ֵ
                Debug.LogError("Unexpected bubble water type.");
                return E_bType.blue;

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
   
}