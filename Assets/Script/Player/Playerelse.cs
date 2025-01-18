using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playerelse : MonoBehaviour
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

    //Ϊ�˵���ϵͳ����ı���
    Prop playerProp; //��������
    public bool isPlayerA = true; //player�Ƿ�Ϊ���A

    private List<KeyCode> pressedKeys = new List<KeyCode>();
    private int maxKeysToPress = 3;
    void Update()
    {
        // �������е� KeyCode
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                BlowBubbleelse();
                SpecialItemelse();
                BubbleWaterelse();
            }
            if (pressedKeys.Count == maxKeysToPress)
            {
                KillBubbleelse(BubblePoolA.Instance.Bubbles[count.ToString()]);
            }
        }
    }

    //������
    public void BlowBubbleelse()
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
                    creatBubbleelse();
                    isBlowingBubbles = true;
                    timer = 0f;
                }
                // ��ʼ��ʱ����������ˮ��������Q��
                timer += Time.deltaTime;
                BBWAmount -= Mathf.FloorToInt(BBWSpeed * Time.deltaTime);
                // ȷ������ˮ��������Ϊ������Q��
                BBWAmount = Mathf.Max(0, BBWAmount);
            }
            else
            {
                // �����ٰ�ѹ Code1 ��ʱ��ֹͣ�����ݣ�Q��
                isBlowingBubbles = false;
                GetTimerelse();
            }
        }

    }

    public float GetTimerelse()
    {//����ʱ��     �ǲ��ǻ�û��
        Debug.Log("is" + timer);
        return timer;

    }
    //WER������ϼ�
    public void KillBubbleelse(Bubble bubble)
    {
        KeyCode[] Kill = new KeyCode[] { };

        if (playerinput != null && playerinput.Length > 0)
        {
            for (int n = 0; n < 3; n++)
                if (Input.GetKey(playerinput[1].key2))
                {
                    Kill[n] = playerinput[1].key2;
                }
                else if (Input.GetKey(playerinput[2].key3))
                {
                    Kill[n] = playerinput[1].key3;
                }
                else if (Input.GetKey(playerinput[3].key4))
                {
                    Kill[n] = playerinput[1].key4;
                }
            for (int i = 0; i < Kill.Length; i++)
            {
                count[i] = (char)Kill[i];
            }
        }
        for (int k = 0; k < 3; k++)
        {
            if (count[k] == bubble.code[k]) { }
            else return;
        }
        Destroy(bubble);
        return;
    }
    //�ͷ��������
    public void SpecialItemelse()
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
    public void BubbleWaterelse()
    {
        if (playerinput != null && playerinput.Length > 0)
        {
            // ����Ƿ����� key6 ��
            if (Input.GetKey(playerinput[5].key6))
            {
                //����ˮÿ��+10
                BBWAmount += 10f * Time.deltaTime;
            }
        }
    }
    public void creatBubbleelse()
    {
        Debug.Log("��������");
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
    }

}
