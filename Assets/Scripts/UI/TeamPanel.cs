using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPanel : MonoBehaviour
{
    private bool isActive = false;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isActive = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void EnablePanel()
    {
        isActive = true;
        gameObject.SetActive(true);
    }
}
