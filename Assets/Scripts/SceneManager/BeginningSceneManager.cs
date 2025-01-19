using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginningSceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ;
        }
        else if (Input.anyKeyDown)
        {
            GoToMainScene();
        }
    }

    // Method to exit the game
    private void ExitGame()
    {
        Application.Quit();
    }

    // Method to load the main scene
    private void GoToMainScene()
    {
        SoundManager.AddSound("sound/ÓÎÏ·¿ªÊ¼", 0, 1);
        SceneManager.LoadScene("FinalScene");  // Make sure "MainScene" is the name of your main scene
    }
}

