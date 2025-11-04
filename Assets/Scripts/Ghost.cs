using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private GameObject King;
    private bool hasTarget = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Check for required components
        if (GetComponent<SpriteRenderer>() == null)
        {
            Debug.LogError("Ghost is missing SpriteRenderer! Please add a sprite to the Ghost prefab.");
        }

        // Find the player
        King = GameObject.FindGameObjectWithTag("Player");
        if (King == null)
        {
            Debug.LogWarning("Ghost cannot find Player object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (King != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, King.transform.position, speed * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, King.transform.position) < 1f)
            {
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.OnPlayerCaught();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.OnPlayerCaught();
            }
        }
    }
}
