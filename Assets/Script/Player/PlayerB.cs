using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;


public class PlayerB : MonoBehaviour
{
    public Playerinput[] playerinput2;
    //����ˮ����100
    public float BBWAmount2 = 100;
    // ���ڼ�ʱ�ı���
    private float timer2 = 0f;
    // �����ݵ��ٶȣ���λΪÿ�����ĵ�����ˮ����
    public float BBWSpeed2 = 10f;
    // ����Ƿ����ڴ�����
    private bool isBlowingBubbles2 = false;
    //�Ʒ�
    public float playerScore2 = 0.1f;
    char[] count2 = new char[3];
    //�������
    public Playerinput[] playerinputin2;
    //�������
    private Queue<KeyCode> keyQueue2 = new Queue<KeyCode>();
    //Ϊ�˵���ϵͳ����ı���
    public Prop playerProp; //��������
    public bool isPlayerA = true; //player�Ƿ�Ϊ���A

    private List<KeyCode> pressedKeys2 = new List<KeyCode>();
    private int maxKeysToPress2 = 3;
    char[] Kill2 = new char[3];
    Bubble bubble2 = new Bubble();
    void Update()
    {
        // ����Ƿ��´����ݵļ�
        if (playerinput2 != null && playerinput2.Length > 0 && Input.GetKey(playerinput2[0].key1))
        {
            BlowBubble();
        }
        // ����Ƿ���������߼�
        if (playerinput2 != null && playerinput2.Length > 0 && Input.GetKey(playerinput2[4].key5))
        {
            SpecialItem();
        }
        // ����Ƿ���պ����ˮ�ļ�
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
        // �������е� KeyCode ��������ϼ�
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
        //Killתstring
        string killString = new string(Kill2);
        return killString;
    }
    //������
    public void BlowBubble()
    {
        // ����ȷ�� playerinput ���鲻Ϊ�գ�����������һ��Ԫ��
        if (playerinput2 != null && playerinput2.Length > 0)
        {
            // ����Ƿ����� key1 ��
            if (Input.GetKey(playerinput2[0].key1))
            {
                if (!isBlowingBubbles2)
                {
                    // ��ʼ�����ݣ�Q��
                    CreateBubble();
                    isBlowingBubbles2 = true;
                    timer2 = 0f;
                }
                // ��ʼ��ʱ����������ˮ��������Q��
                timer2 += Time.deltaTime;
                BBWAmount2 -= Mathf.FloorToInt(BBWSpeed2 * Time.deltaTime);
                //������ʱ��������ʧ��
                if (BBWAmount2 <= 0)
                {
                    isBlowingBubbles2 = false;
                    timer2 = 0f;
                }
            }
            else
            {
                //һ����Сֵ
                // �����ٰ�ѹ Code1 ��ʱ��ֹͣ�����ݣ�Q��
                isBlowingBubbles2 = false;
                BubblePoolA.Instance.blowTime = timer2;
                //��������ģ��
                BubblePoolA.Instance.GetObj();
            }
        }

    }




    //WER������ϼ�
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
        if (playerinput2!= null && playerinput2.Length > 0)
        {
            // ����Ƿ����� key6 ��
            if (Input.GetKey(playerinput2[5].key6))
            {
                //����ˮÿ��+10
                BBWAmount2 += 10f * Time.deltaTime;
            }
        }
    }
    public void CreateBubble()
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

}
