using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private float _speed;
    public float score;
    public bool isA;
    public GameObject bubble;
    public TextMeshPro label;
    public Animator ani;
    public string code;
    public int index=2;
    
    public void Set( bool isA)
    {
        this.isA = isA;
        GetBubble(isA);
        this.code = "";
        this.index = 2;
        label=bubble.GetComponentInChildren<TextMeshPro>(); 
    }

    public void Init(float blowTime)
    {
        GetScore(blowTime);
        GetSpeed(blowTime);
    }

    private void GetSpeed(float blowTime)
    {
        _speed = blowTime / 3;
    }

    private void GetScore(float blowTime)
    {
        score = math.pow(blowTime, 2);
    }

    private void GetBubble(bool isA)
    {
        if (isA)
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

    public void fly()
    {
        bubble.transform.position += new Vector3(0, _speed*Time.deltaTime, 0);
    }
    

}
