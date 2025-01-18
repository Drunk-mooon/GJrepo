using UnityEngine;
using System.Collections;

public class GrowOnKeyPress : MonoBehaviour
{

    public float growthRate = 0.1f; // ��������
    private bool isGrowing = false; // �Ƿ���������
    private Vector3 originalScale; // �洢ԭʼ��С


    void Start()
    {
        // �������岢�洢ԭʼ��С
        gameObject.SetActive(false);
        originalScale = transform.localScale;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // ���� Q ��ʱ�������β���ʼ����
            gameObject.SetActive(true);
            isGrowing = true;
            transform.localScale = originalScale; // ���ô�С
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            // �ɿ� Q ��ʱ��������
            gameObject.SetActive(false);
            isGrowing = false;
        }


        if (isGrowing)
        {
            GrowObject();
        }
    }


    void GrowObject()
    {
        // ��ȡ��ǰ������
        Vector3 currentScale = transform.localScale;
        // �����µ�����
        Vector3 newScale = currentScale + new Vector3(growthRate, growthRate, 0) * Time.deltaTime;
        // ��������
        transform.localScale = newScale;
    }
}