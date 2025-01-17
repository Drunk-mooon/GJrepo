using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BubblePoolA : ObjPool<BubblePoolA, Bubble>
{
    public float blowTime;
    public int size;
    private Random _random;
    public Dictionary<string, Bubble> Bubbles = new Dictionary<string, Bubble>();
    public string attackCode;
    
    public override void Init()
    {
       _random = new Random();
        IniPool(10);
    }

    protected override Bubble IniObj(Bubble obj)
    {
        obj.Init(blowTime); 
        obj.bubble.SetActive(true);
        obj.bubble.transform.position = new Vector3(0, 0, 0);
        obj.code = GetCode(obj);
        obj.label.text = obj.code;
        obj.code=obj.code.Remove(0,3);
        
        Bubbles.Add(obj.label.text, obj);
        return obj;
    }

    protected override Bubble CreateObj()
    {
<<<<<<< Updated upstream
        GameObject obj = new GameObject();
        obj.transform.SetParent(this.transform);
        obj.AddComponent<Bubble>();
        Bubble bubble = obj.GetComponent<Bubble>();
        bubble.isA = true;
        bubble.Set(true);
        bubble.bubble.SetActive(false);
=======
        GameObject obj = new GameObject(); //建立泡泡的对象
        obj.transform.SetParent(this.transform); //父亲是这个对象池
        obj.AddComponent<Bubble>(); //添加脚本 
        Bubble bubble = obj.GetComponent<Bubble>(); //指明
        bubble.Set(true); 
        bubble.bubble.SetActive(false); //初始出来先隐藏
>>>>>>> Stashed changes
        return bubble;
    }
    
    protected override IEnumerator RecyleObj(Bubble obj)
    {
        if (obj.index==0)
        {
            obj.ani.SetTrigger("EndAni");
            yield return new WaitForSeconds(3);
            obj.bubble.SetActive(false);
            Bubbles.Remove(obj.label.text);
            Pool.Enqueue(obj);
        }
        else
        {
          // attack(attackCode);
          obj.index -= 1;
        }
    }

    protected override void DestroyObj(Bubble obj)
    {
    }

    protected override void IniPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            Pool.Enqueue(CreateObj());
        }
    }

    private void Update()
    {
        foreach (Bubble a in Bubbles.Values)
        {
            a.fly();
        }
    }
    
    private string GetCode(Bubble obj)
    {
        string result = "";
        do
        {
            for (int i = 0; i < 3; i++)
            { 
                result += _random.Next(1,4).ToString();
            }    

        } while (Bubbles.ContainsKey(result));
        
        obj.index--;
        return result;
    }

    public void attack(string code)
    {   
        Bubbles[code].code = GetCode(Bubbles[code]);;
        string secCod = Bubbles[code].code;
        Bubbles.Add(secCod,Bubbles[code]);
        Bubbles.Remove(code);
    }
    
    
}