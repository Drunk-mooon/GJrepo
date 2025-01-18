using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;

public class Player : MonoBehaviour
{
    public Playerinput[] playerinput;
    //����ˮ����100
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
    Prop playerProp; //��������
    public bool isPlayerA = true; //player�Ƿ�Ϊ���A

    private List<KeyCode> pressedKeys = new List<KeyCode>();
    private int maxKeysToPress = 3;
    char[] Kill = new char[3];
    Bubble bubble = new Bubble();
    void Update()
    {
        // ����Ƿ��´����ݵļ�
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[0].key1))
        {
            BlowBubble();
        }
        // ����Ƿ���������߼�
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[4].key5))
        {
            SpecialItem();
        }
        // ����Ƿ���պ����ˮ�ļ�
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[5].key6))
        {
            BubbleWater();
        }
        if (playerinput != null && playerinput.Length > 0 && Input.GetKey(playerinput[1].key2) || Input.GetKey(playerinput[2].key3) || Input.GetKey(playerinput[3].key4))
            {
            InputKey();
            if (pressedKeys.Count == maxKeysToPress)
            {
                if (BubblePoolA.Instance.Bubbles.ContainsKey(Kill.ToString())) { 
                KillBubble(BubblePoolA.Instance.Bubbles[Kill.ToString()], ref Kill);
            }
            }
        }
    }

    public string InputKey()
    {
        keyQueue.Clear();
        int index = 0;
        // �������е� KeyCode ��������ϼ�
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (playerinput != null && playerinput.Length > 0)
            {
                if (Input.GetKey(playerinput[1].key2))
                {
                    keyQueue.Enqueue(playerinput[1].key2);
                }
                else if (Input.GetKey(playerinput[2].key3))
                {
                    keyQueue.Enqueue(playerinput[2].key3);
                }
                else if (Input.GetKey(playerinput[3].key4))
                {
                    keyQueue.Enqueue(playerinput[3].key4);
                }
            }
        }
        char[] kill=new char[3];
        while (keyQueue.Count > 0 && index < 3)
        {
            Kill[index++] = (char)keyQueue.Dequeue();
        }
        //Killתstring
        string killString = new string(Kill);
        return killString;
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
                    creatBubble();
                    isBlowingBubbles = true;
                    timer = 0f;
                }
                // ��ʼ��ʱ����������ˮ��������Q��
                timer += Time.deltaTime;
                BBWAmount -= Mathf.FloorToInt(BBWSpeed * Time.deltaTime);
                //������ʱ��������ʧ��
                if(BBWAmount<=0)
                {
                    isBlowingBubbles=false;
                    timer = 0f;
                }
            }
            else
            {
                // �����ٰ�ѹ Code1 ��ʱ��ֹͣ�����ݣ�Q��
                isBlowingBubbles = false;
                BubblePoolA.Instance.blowTime = timer;
                //��������ģ��
                BubblePoolA.Instance.GetObj();
            }
        }

    }




    //WER������ϼ�
    public void KillBubble(Bubble bubble,ref char[] Kill)
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
    //�ͷ��������
    public void SpecialItem()
    {/*
        if (playerinput != null && playerinput.Length > 0)
        {
            SpeciailItem Item = new SpeciailItem();
            // ����Ƿ����� key5��
            if (Input.GetKey(playerinput[4].key5))
            {   //����������߽ű�
                if (this.playerProp!=null)
                {
                    playerProp.ApplyEffect();
                    playerProp=null;
                }

            }
        }
        */
    }
    //պ����ˮ
    public void BubbleWater()
    {
        if (playerinput != null && playerinput.Length > 0)
        {
            // ����Ƿ����� key6 ��
            if (Input.GetKey(playerinput[5].key6))
            {
                //����ˮÿ��+10
                BBWAmount += 10f*Time.deltaTime;
            }
        }
    }
    public void creatBubble()
    {
    float growthRate = 0.1f;
    Vector3 currentScale = transform.localScale;
    // �����µİ뾶
    float newRadius = Mathf.Max(0, currentScale.x + growthRate * Time.deltaTime);
    // ȷ���뾶��Ϊ����
    currentScale = new Vector3(newRadius, newRadius, newRadius);
    // Ӧ���µ�����
    transform.localScale = currentScale;
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

}
