using UnityEngine;

public class DogControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the bird movement

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.Alpha7)) // Move Up
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.Alpha8)) // Move Down
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(KeyCode.Alpha9)) // Move Left
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.Alpha0)) // Move Right
        {
            moveDirection += Vector3.right;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
