using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TransformData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

public class WaterBullbleSelect : MonoBehaviour
{
    [Header("物体数组")]
    public Transform[] bullbleWater;
    private int length;

    [Header("目标属性")]
    private TransformData[] targetDatas;

    [Header("控制参数")]
    public float moveDuration = 1f;   // 移动时长
    private bool isMoving = false;    
    private float moveTimer = 0f;

    void Start()
    {
        length = bullbleWater.Length;
        targetDatas = new TransformData[length];

        // 把最初每个物体位置/旋转/缩放记录下来作为槽位信息
        for (int i = 0; i < length; i++)
        {
            Transform trans = bullbleWater[i];
            targetDatas[i] = new TransformData
            {
                position = trans.position,
                rotation = trans.rotation,
                scale    = trans.localScale
            };
        }
    }

    void Update()
    {
        // 按下 Q 并且当前不在移动时，开始轮换
        if (Input.GetKeyDown(KeyCode.Q) && !isMoving)
        {
            StartRotationCycle();
        }

        if (isMoving)
        {
            PerformSmoothMove();
        }
    }
    
    // 让物体轮换占用固定的槽位
    void StartRotationCycle()
    {
        Transform firstObj = bullbleWater[0];

        // 左移
        for (int i = 0; i < length - 1; i++)
        {
            bullbleWater[i] = bullbleWater[i + 1];
        }

        // 把最末位替换为原先的第一个
        bullbleWater[length - 1] = firstObj;

        moveTimer = 0f;
        isMoving = true;
    }
    
    /// 逐帧对各个属性进行插值，并且设置渲染顺序
    void PerformSmoothMove()
    {
        moveTimer += Time.deltaTime;
        float t = Mathf.Clamp01(moveTimer / moveDuration);

        for (int i = 0; i < length; i++)
        {
            Transform trans = bullbleWater[i];
            if (trans == null) continue;

            // 位置插值
            trans.position = Vector3.Lerp(
                trans.position,
                targetDatas[i].position,
                t
            );

            // 旋转插值
            trans.rotation = Quaternion.Slerp(
                trans.rotation,
                targetDatas[i].rotation,
                t
            );

            // 缩放插值
            trans.localScale = Vector3.Lerp(
                trans.localScale,
                targetDatas[i].scale,
                t
            );
            
            Renderer rd = trans.GetComponent<Renderer>();
            if (rd != null)
            {
                if (i == 1)
                {
                    rd.sortingOrder = 1;
                }
                else if (i == 3)
                {
                    rd.sortingOrder = 2;
                }
                else
                {
                    rd.sortingOrder = 0;
                }
            }
        }
        if (t >= 1f)
        {
            isMoving = false;
        }
    }
    
    public Transform GetSelectedElement()
    {
        if (bullbleWater != null && bullbleWater.Length > 1)
        {
            return bullbleWater[3];
        }
        else
        {
            return null;
        }
    }
}
