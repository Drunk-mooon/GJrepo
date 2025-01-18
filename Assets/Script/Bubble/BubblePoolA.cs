using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class BubblePoolA : ObjPool<BubblePoolA, Bubble>
{
    public float blowTime; //吹的时间
    private Random _random;
    public Dictionary<string, Bubble> Bubbles = new Dictionary<string, Bubble>(); //组合键与泡泡的对应关系
    public string attackCode; //玩家输入的组合键
    public float speedChange=1; 
    public E_bType bType;

    public override void Init()
    {
        _random = new Random();
        IniPool(10);
    }

    protected override Bubble IniObj(Bubble obj) //每次调用前的初始化 
    {
        obj.Init(blowTime); //获取本次吹得到的速度和分数 
        obj.bubble.SetActive(true); //模型激活 
        obj.bubble.transform.position = new Vector3(0, 0, 0); //初始位置 
        obj.code = GetCode(obj); //获得code 
        obj.label.text = obj.code; //code可见 
        Bubbles.Add(obj.label.text, obj); //字典添加 code，泡泡 
        obj.bType = bType;
        TypeEffect(obj);
        return obj;
    }

    protected override Bubble CreateObj()
    {
        GameObject obj = new GameObject(); //建立泡泡的对象 
        obj.transform.SetParent(this.transform); //父亲是这个对象池 
        obj.AddComponent<Bubble>(); //添加脚本 
        Bubble bubble = obj.GetComponent<Bubble>(); //指明 
        bubble.Set(true); 
        bubble.bubble.SetActive(false); //初始出来先隐藏 
        return bubble; 
    }

    protected override IEnumerator RecyleObj(Bubble obj) 
    {  
         if (obj.index == 0) //如果没命了
         {
             obj.ani.SetTrigger("EndAni"); //动画状态机操作
             yield return new WaitForSeconds(3); //等等
             obj.bubble.SetActive(false); //模型消失
             Pool.Enqueue(obj);
        } 
        else
        { 
             attack(attackCode); //用code 攻击
            Debug.Log("WWW");
        } 
    }

    protected override void DestroyObj(Bubble obj)
    {
    }

    protected override void IniPool(int size) //初始化对象池
    {
        for (int i = 0; i < size; i++)
        {
            Pool.Enqueue(CreateObj());
        }
    }

    private void Update() // 飞起来
    {
        foreach (Bubble a in Bubbles.Values)
        {
           if(a.isA)  a.transform.position += new Vector3(0, a.speed * Time.deltaTime*speedChange, 0);
           else a.transform.position += new Vector3(0, a.speed * Time.deltaTime*speedChange, 0); //其实是 BubblePoolB.instance.speedChange
        } 
    }

    private string GetCode(Bubble obj) //随机获得code  
    {
        string result;
        do
        {
            result = "";
            for (int i = 0; i < 3; i++)
            {
                result += _random.Next(1, 4).ToString();
            }
        } while (Bubbles.ContainsKey(result)||Bubbles.ContainsKey(result));

        obj.index--; //  命数-1
        return result;
    }

    public void attack(string code) 
    {
        Bubbles[code].code = GetCode(Bubbles[code]); //这个attackcode对应的bubble获得新的code
        string secCod = Bubbles[code].code; //临时code=上个code
        Bubbles[code].label.text = secCod;
        Bubbles.Add(secCod, Bubbles[code]); //临时code，以及那个bubble进入字典
        Bubbles.Remove(code); //移除字典中之前那个code与bubble的键值对
    }

    public void changeBubble(int num)
    {
        int[] index =new int[num];
        int count = Bubbles.Count;
        index[0]=_random.Next(0, count);
        for (int i = 1; i < num; i++)
        {
            index[i] = (index[0] + i)%count;
        }

        for (int i = 0; i < num; i++)
        {
           Bubbles.ElementAt(index[i]).Value.isA = false;
        }

        
    }
    private void TypeEffect(Bubble obj)
    {
        switch (obj.bType)
        {
            case E_bType.blue:
                obj.index = 3;
                break;
            case E_bType.green:
                break;
            case E_bType.pink:
                obj.speed *= 2;
                break;
            case E_bType.white:
                break;
            default:
                break;

        }
    }
}