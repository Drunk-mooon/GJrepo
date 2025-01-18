using UnityEngine;

public class DebugCapsule : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the cube moves
    public GameManager gameManager;
    void Update()
    {
        // Get input for movement (WASD or Arrow keys)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys
        float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow keys


        if (Input.GetKeyDown(KeyCode.J))
            gameManager.AddScore(1, 0.5f);
        if (Input.GetKeyDown(KeyCode.K))
            gameManager.AddScore(2, 0.7f);
        // Move the cube in the 2D space
        transform.Translate(new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime);
    }
}
