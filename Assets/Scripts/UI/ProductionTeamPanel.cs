using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionTeamPanel : MonoBehaviour
{
    private bool isActive;

    private void Start()
    {
        gameObject.SetActive(false); // Directly use gameObject
        isActive = false;
    }

    private void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            DisableTeamPanel();
        }
    }

    public void EnableTeamPanel()
    {
        gameObject.SetActive(true); // Directly use gameObject
        isActive = true;
    }

    public void DisableTeamPanel()
    {
        gameObject.SetActive(false); // Directly use gameObject
        isActive = false;
    }
}
