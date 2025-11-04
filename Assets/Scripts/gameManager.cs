using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private const int WIN_SCORE = 200;
    public bool isGameOver = false;

    void Start()
    {
        score = 0;
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
            return;

        // Check for left mouse click or touch
        if (Input.GetMouseButtonDown(0)) // 0 is left click/touch
        {
            // Convert screen position to world position
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            // Check if we hit a ghost
            if (hit.collider != null && hit.collider.CompareTag("Ghost"))
            {
                // Make sure the game isn't already over before destroying the ghost
                if (!isGameOver)
                {
                    DestroyGhost(hit.collider.gameObject);
                }
            }
        }
    }

    void DestroyGhost(GameObject ghost)
    {
        Destroy(ghost);
        score++;
        
        if (score >= WIN_SCORE)
        {
            Win();
        }
    }

    void Win()
    {
        isGameOver = true;
        SceneManager.LoadSceneAsync(4); // Load win scene
    }

    void GameOver()
    {
        isGameOver = true;
        SceneManager.LoadSceneAsync(3); // Load game over scene
    }

    // This will be called by the Player when they collide with a ghost
    public void OnPlayerCaught()
    {
        if (!isGameOver)
        {
            GameOver();
        }
    }
}
