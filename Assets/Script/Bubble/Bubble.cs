using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;


 public enum E_bType
 {
    blue,
    green,
    pink
 }


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
    public E_bType bType;
    public float dis;
  
    
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
            bubble = Instantiate(AssetManager.LoadRes<GameObject>("bubble1"), transform);
            ani = bubble.GetComponentInChildren<Animator>();
        }
        else
        {
            bubble = Instantiate(AssetManager.LoadRes<GameObject>("bubble2"), transform);
            ani = bubble.GetComponentInChildren<Animator>();
        }
    }

    //判断角色

}