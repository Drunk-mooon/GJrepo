using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed; //速度
    public float score; //得分
    public bool isA; //来源
    public GameObject bubble; //模型
    public TextMeshPro label; //文本对应的obj是模型子对象
    public Animator ani; //动画
    public string code; //当前组合键
    public int index = 2; //泡泡生命

    public void Set(bool isa)
    {
        this.isA = isa;
        GetBubble(isA);
        this.code = "";
        this.index = 2;
        label = bubble.GetComponentInChildren<TextMeshPro>();
    } //初始化 主要是绑定和确认哪个玩家

    public void Init(float blowTime)
    {
        GetScore(blowTime);
        GetSpeed(blowTime);
    }

    private void GetSpeed(float blowTime)
    {
        speed = blowTime / 3;
    }

    private void GetScore(float blowTime)
    {
        score = math.pow(blowTime, 2);
    }

    //获取两个值
    private void GetBubble(bool isa)
    {
        if (isa)
        {
            bubble = Instantiate(AssetManager.LoadRes<GameObject>("BubbleA"), transform);
            ani = bubble.GetComponent<Animator>();
        }
        else
        {
            bubble = Instantiate(AssetManager.LoadRes<GameObject>("BubbleB"), transform);
            ani = bubble.GetComponent<Animator>();
        }
    }

    //判断角色
    public void fly()
    {
        bubble.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    } //移动
}