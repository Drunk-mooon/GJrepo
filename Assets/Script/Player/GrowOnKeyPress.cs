using UnityEngine;
using System.Collections;

public class GrowOnKeyPress : MonoBehaviour
{

    public float growthRate = 0.1f; // 生长速率
    private bool isGrowing = false; // 是否正在生长
    private Vector3 originalScale; // 存储原始大小


    void Start()
    {
        // 隐藏物体并存储原始大小
        gameObject.SetActive(false);
        originalScale = transform.localScale;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 按下 Q 键时物体现形并开始生长
            gameObject.SetActive(true);
            isGrowing = true;
            transform.localScale = originalScale; // 重置大小
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            // 松开 Q 键时物体隐藏
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
        // 获取当前的缩放
        Vector3 currentScale = transform.localScale;
        // 计算新的缩放
        Vector3 newScale = currentScale + new Vector3(growthRate, growthRate, 0) * Time.deltaTime;
        // 更新缩放
        transform.localScale = newScale;
    }
}