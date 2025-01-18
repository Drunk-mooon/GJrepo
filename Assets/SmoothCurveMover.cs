using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCurveMover : MonoBehaviour
{
    public Transform[] bullbleWater; // 长方体数组
    private Vector3[] targetPositions; // 每次轮换的目标位置
    private Quaternion[] targetRotations; // 每次轮换的目标旋转

    public float moveDuration = 1f; // 每次移动的持续时间
    private bool isMoving = false; // 是否正在移动
    private float moveTimer = 0f; // 移动计时器

    void Start()
    {
        // 初始化目标位置和旋转数组
        targetPositions = new Vector3[bullbleWater.Length];
        targetRotations = new Quaternion[bullbleWater.Length];

        // 初始设置为当前位置和旋转
        for (int i = 0; i < bullbleWater.Length; i++)
        {
            targetPositions[i] = bullbleWater[i].position;
            targetRotations[i] = bullbleWater[i].rotation;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isMoving)
        {
            StartRotationCycle(); // 按下 Q 时开始轮换

        }

        if (isMoving)
        {
            PerformSmoothMove(); // 如果正在移动，逐帧更新位置和旋转
        }
    }

    // 开始轮换：计算目标位置和旋转
    void StartRotationCycle()
    {
        // 临时保存第一个物体的位置和旋转
        Vector3 firstPosition = targetPositions[0];
        Quaternion firstRotation = targetRotations[0];

        // 所有目标位置和旋转向左轮换
        for (int i = 0; i < bullbleWater.Length - 1; i++)
        {
            targetPositions[i] = targetPositions[i + 1];
            targetRotations[i] = targetRotations[i + 1];
        }

        // 最后一个位置和旋转赋值为原第一个
        targetPositions[bullbleWater.Length - 1] = firstPosition;
        targetRotations[bullbleWater.Length - 1] = firstRotation;

        // 启动移动
        moveTimer = 0f;
        isMoving = true;
    }

    // 平滑更新物体的位置和旋转
    void PerformSmoothMove()
    {
        moveTimer += Time.deltaTime;
        float t = Mathf.Clamp01(moveTimer / moveDuration); // 归一化时间参数 t，范围 [0, 1]

        for (int i = 0; i < bullbleWater.Length; i++)
        {
            // 使用 Lerp 插值位置
            bullbleWater[i].position = Vector3.Lerp(bullbleWater[i].position, targetPositions[i], t);

            // 使用 Slerp 插值旋转（更平滑）
            bullbleWater[i].rotation = Quaternion.Slerp(bullbleWater[i].rotation, targetRotations[i], t);
        }

        // 如果插值完成
        if (t >= 1f)
        {
            isMoving = false; // 停止移动
        }
    }
}
