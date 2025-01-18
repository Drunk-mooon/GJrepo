using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBW : MonoBehaviour
{
    public float BBWdata;
    public float maxLength = 100f; // ��״ͼ�ε���󳤶�
    private RectTransform rectTransform;

    void Start()
    {
        // ��ȡ RectTransform ���
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("This GameObject must have a RectTransform component.");
        }
    }

    void Update()
    {
        // ȷ�� BBW ���ᵼ����״ͼ�γ�����󳤶�
        float targetLength = Mathf.Clamp(BBWdata, 0, maxLength);
        // ���� RectTransform �Ĵ�С
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, targetLength);
    }
}
