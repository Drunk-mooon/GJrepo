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
        else if (AnyKeyExceptMouse())
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
        SceneManager.LoadScene("MainScene(UI)"); // Make sure "MainScene(UI)" matches your scene name
    }

    // Method to detect any key press except mouse clicks
    private bool AnyKeyExceptMouse()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode) && !IsMouseKey(keyCode))
            {
                return true;
            }
        }
        return false;
    }

    // Helper method to check if the KeyCode is a mouse key
    private bool IsMouseKey(KeyCode keyCode)
    {
        return keyCode >= KeyCode.Mouse0 && keyCode <= KeyCode.Mouse6;
    }
}
