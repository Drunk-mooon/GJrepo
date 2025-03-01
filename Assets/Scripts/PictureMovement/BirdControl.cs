using UnityEngine;
using System.Collections;

public class BirdControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the bird movement
    public GameObject lovePrefab; // Assign a love image prefab in the inspector
    public float loveDuration = 2f; // Time before the love disappears
    public float loveCooldown = 3f; // Cooldown before another love can appear
    public int loveCount = 3; // Number of loves to generate per touch
    public float loveSpacing = 0.5f; // Spacing between loves
    public float minRiseSpeed = 0.5f; // Minimum rise speed for loves
    public float maxRiseSpeed = 1.5f; // Maximum rise speed for loves
    public float shakeAmount = 0.1f;
    public float verticalLoveOffset = 0.1f;

    private bool canGenerateLove = true;

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.Alpha1)) // Move Up
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.Alpha2)) // Move Down
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(KeyCode.Alpha3)) // Move Left
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.Alpha4)) // Move Right
        {
            moveDirection += Vector3.right;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dog") && canGenerateLove)
        {
            StartCoroutine(GenerateLoves(other.transform.position));
        }
    }

    private IEnumerator GenerateLoves(Vector3 position)
    {
        canGenerateLove = false;

        for (int i = 0; i < loveCount; i++)
        {
            Vector3 spawnPos = position + new Vector3(0, i * loveSpacing + verticalLoveOffset, 0);
            GameObject love = Instantiate(lovePrefab, spawnPos, Quaternion.identity);
            float randomRiseSpeed = Random.Range(minRiseSpeed, maxRiseSpeed);
            StartCoroutine(RiseAndShakeLove(love, randomRiseSpeed));
        }

        yield return new WaitForSeconds(loveCooldown);
        canGenerateLove = true;
    }

    private IEnumerator RiseAndShakeLove(GameObject love, float riseSpeed)
    {
        float elapsedTime = 0f;

        while (elapsedTime < loveDuration)
        {
            love.transform.position += new Vector3(Mathf.Sin(Time.time * 5) * shakeAmount,riseSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(love);
    }
}
