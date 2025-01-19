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
    [Header("Object Array")]
    public Transform[] bullbleWater;
    private int length;

    [Header("Fixed Slot Data")]
    private TransformData[] targetDatas;

    [Header("Movement Control")]
    public float moveDuration = 1f;
    public bool isMoving = false;

    void Start()
    {
        length = bullbleWater.Length;
        targetDatas = new TransformData[length];

        // Record each object's initial position/rotation/scale as the "fixed slots"
        for (int i = 0; i < length; i++)
        {
            Transform trans = bullbleWater[i];
            targetDatas[i] = new TransformData
            {
                position = trans.position,
                rotation = trans.rotation,
                scale = trans.localScale
            };
        }
    }

    void Update()
    {
        // Press Q to start the rotation (if not currently moving)
        if (Input.GetKeyDown(KeyCode.Y) && !isMoving)
        {
            StartRotationCycle();
        }
    }

    // Rotate references: move each object in bullbleWater left by one,
    // then start the interpolation to the fixed slots (targetDatas).
    public void StartRotationCycle()
    {
        Transform firstObj = bullbleWater[0];

        for (int i = 0; i < length - 1; i++)
        {
            bullbleWater[i] = bullbleWater[i + 1];
        }
        bullbleWater[length - 1] = firstObj;

        StartCoroutine(SmoothMoveCoroutine());
    }

    /// <summary>
    /// Coroutine for smoothly moving each object to the corresponding slot (targetDatas[i]).
    /// </summary>
    IEnumerator SmoothMoveCoroutine()
    {
        isMoving = true;
        float moveTimer = 0f;

        while (moveTimer < moveDuration)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / moveDuration);

            for (int i = 0; i < length; i++)
            {
                Transform trans = bullbleWater[i];
                if (trans == null) continue;

                // Lerp position
                trans.position = Vector3.Lerp(
                    trans.position,
                    targetDatas[i].position,
                    t
                );

                // Slerp rotation
                trans.rotation = Quaternion.Slerp(
                    trans.rotation,
                    targetDatas[i].rotation,
                    t
                );

                // Lerp scale
                trans.localScale = Vector3.Lerp(
                    trans.localScale,
                    targetDatas[i].scale,
                    t
                );
            }

            yield return null; // Wait for the next frame
        }

        // Ensure all transforms are correctly set at the end
        for (int i = 0; i < length; i++)
        {
            Transform trans = bullbleWater[i];
            if (trans == null) continue;

            trans.position = targetDatas[i].position;
            trans.rotation = targetDatas[i].rotation;
            trans.localScale = targetDatas[i].scale;
        }

        isMoving = false;
    }

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
