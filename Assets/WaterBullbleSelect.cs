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
    [Header("物体数组 (准备轮换的物体们)")]
    public Transform[] bullbleWater;
    private int length;

    [Header("每个槽位的目标属性")]
    private TransformData[] targetDatas;

    [Header("移动控制参数")]
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
    
    /// <summary>
    /// 左移 bullbleWater 的引用，让物体轮换占用固定的槽位
    /// </summary>
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
    
    /// <summary>
    /// 逐帧对位置、旋转、缩放进行插值，并且在此期间/结束后设置渲染顺序
    /// </summary>
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
            
            // 让 2 号槽位（i=1）永远在 4 号槽位（i=3）前面
            Renderer rd = trans.GetComponent<Renderer>();
            if (rd != null)
            {
                // 可以直接在插值过程或插值结束后设置
                if (i == 1)
                {
                    // 2 号位置：更高 sortingOrder
                    rd.sortingOrder = 1;
                }
                else if (i == 3)
                {
                    // 4 号位置：更低 sortingOrder
                    rd.sortingOrder = 2;
                }
                else
                {
                    // 其他槽位你可以自定义
                    rd.sortingOrder = 0;
                }
            }
        }

        // 插值完成
        if (t >= 1f)
        {
            isMoving = false;
        }
    }

    /// <summary>
    /// 获取当前选中的物体，即2号槽位(下标=1)
    /// </summary>
    public Transform GetSelectedElement()
    {
        if (bullbleWater != null && bullbleWater.Length > 1)
        {
            return bullbleWater[1];
        }
        else
        {
            return null;
        }
    }
}
