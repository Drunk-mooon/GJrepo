using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjPool<BulletPool, Bullet>
{
    public GameObject model; //对象模型
    public GameObject aim;//对象父类
    public float a = 0;
    
    public override void Init()
    { 
      IniPool(3);
    }

    protected override Bullet IniObj(Bullet obj)
    { 
        
        obj.Set(15,2);
        obj.theObj.SetActive(true);
        obj.theObj.transform.position = new Vector3(a, 0, 0);
        obj.theObj.transform.SetParent(aim.transform);
        return obj;
    }

    protected override Bullet CreateObj()
    {
        
        GameObject obj = Instantiate(model,this.transform);
        obj.AddComponent<Bullet>();
        
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.Set(15,1);
        bullet.theObj =obj;
        bullet.theObj.SetActive(false);
        return bullet;
    }

    protected override IEnumerator RecyleObj(Bullet obj)
    {
        yield return new WaitForSeconds(3f);
        obj.theObj.SetActive(false);
        obj.theObj.transform.SetParent(this.transform);
        Pool.Enqueue(obj);
       
    }
    
    protected override void DestroyObj(Bullet obj)
    {
       Destroy(obj.theObj);
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
        a += Time.deltaTime;

    }
    
    
    

}

