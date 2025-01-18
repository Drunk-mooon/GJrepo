using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBW : MonoBehaviour
{
    public float BBWdata;
    public float maxLength = 100f; // 柱状图形的最大长度
    private RectTransform rectTransform;

    void Start()
    {
        // 获取 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("This GameObject must have a RectTransform component.");
        }
    }

    void Update()
    {
        // 确保 BBW 不会导致柱状图形超出最大长度
        float targetLength = Mathf.Clamp(BBWdata, 0, maxLength);
        // 调整 RectTransform 的大小
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, targetLength);
    }
}
