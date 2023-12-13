using UnityEngine;

public class PlayerNavigation : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate moveDirection
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;


        // Move the cube
        transform.Translate(new Vector3(- moveDirection.x, 0f, - moveDirection.y) * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            // Handle game finish logic here (e.g., show game over screen, restart level, etc.)
            GameFinish();
        }
    }

    void GameFinish()
    {
        // Add code to finish the game (e.g., display game over UI, reset level, etc.)
        Debug.Log("Game Over");
        // You can add more game finish logic here.
    }

}
