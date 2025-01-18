using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerA : MonoBehaviour
{
    public Playerinput[] playerinput;
    //����ˮ���� 100
    public float BBWAmount = 100;
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
    private Bubble bubble = new Bubble();
    void Update()
    {
        //����Ƿ����������
        KeyCode[] allowedKeys = new KeyCode[] { KeyCode.W, KeyCode.E, KeyCode.R };
        foreach (KeyCode key in allowedKeys)
        {
            if (Input.GetKeyDown(key))
            {
                keyQueue.Enqueue(key);
                if (keyQueue.Count >= 3)
                    InputKey();
                KillBubble(InputKey(),bubble);
            }
            
        }
        // ����Ƿ��´����ݵļ�
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[0].key1))
        {
            BlowBubble();
        }
        // ����Ƿ���������߼�
        if (playerinput != null && playerinput.Length > 4 && Input.GetKey(playerinput[4].key5))
        {
            SpecialItem();
        }
        // ����Ƿ���պ����ˮ�ļ�
        if (playerinput != null && playerinput.Length > 5 && Input.GetKey(playerinput[5].key6))
        {
            BubbleWater();
        }
    }


    public char[] InputKey()
    {
        char[] tempKill = new char[3];
        int indexInKill = 0;
        while (indexInKill < 3)
        {
            tempKill[indexInKill++] = (char)keyQueue.Dequeue();
        }

        // Kill ת string
        return tempKill;
    }


    //������
    public void BlowBubble()
    {
        // ����ȷ�� playerinput ���鲻Ϊ�գ�����������һ��Ԫ��
        if (playerinput != null && playerinput.Length > 0)
        {
            // ����Ƿ����� key1 ��
            if (Input.GetKey(playerinput[0].key1))
            {
                if (!isBlowingBubbles)
                {
                    // ��ʼ�����ݣ�Q��
                    GrowBubble();
                    isBlowingBubbles = true;
                    timer = 0f;
                }
                // ��ʼ��ʱ����������ˮ��������Q��
                timer += Time.deltaTime;
                BBWAmount -= Mathf.FloorToInt(BBWSpeed * Time.deltaTime);
                //������ʱ��������ʧ��
                if (BBWAmount <= 0)
                {
                    isBlowingBubbles = false;
                    timer = 0f;
                }
            }
            else
            {
                //һ����Сֵ
                // �����ٰ�ѹ Code1 ��ʱ��ֹͣ�����ݣ�Q��
                isBlowingBubbles = false;
                BubblePoolA.Instance.blowTime = timer;
                BubblePoolA.Instance.bType = BBWType;
                CreateBubble(true,timer,BBWType);
                timer = 0f;
                // �˴�����Ӹ���ֹͣ�����ݵ��߼�
            }
        }
    }

    public void KillBubble(char[] killString, Bubble bubble)
    {
        for (int i = bubble.code.Length - 1; i >= 0; i--)
        {
            char[] c = killString;
            if (c[i] != bubble.code[i])
            {
                return;
            }
        }
        BubblePoolA.Instance.PutObj(bubble);
    }
    private bool CompareStrings(string str1, string str2)
    {
        // ���бȽ�
        return str1.ToLower() == str2.ToLower();
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


    //պ����ˮ
    public void BubbleWater()
    {
        if (playerinput != null && playerinput.Length > 0)
        {
            // ����Ƿ����� key6 ��
            if (Input.GetKeyDown(playerinput[5].key6))
            {
                // �л� BBW ����
                BBWType=SwitchBBWType();
                BubblePoolA.Instance.bType = BBWType;
            }
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


    public void GrowBubble()
    {
        float growthRate = 0.1f;
        Vector3 currentScale = transform.localScale;
        // �����µİ뾶
        float newRadius = Mathf.Max(0, currentScale.x + growthRate * Time.deltaTime);
        // ȷ���뾶��Ϊ����
        currentScale = new Vector3(newRadius, newRadius, newRadius);
        // Ӧ���µ�����
        transform.localScale = currentScale;
        //ͣ
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