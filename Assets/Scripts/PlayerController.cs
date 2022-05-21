using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float lowerBound = -5.0f;

    public Animator playerAnim;
    public ParticleSystem deathParticle;
    public AudioClip chickenHurtClip;
    private AudioSource audioSource;

    private float horizontalInput;
    private float forwardInput;
    [SerializeField] private float speed = 1.0f;
    private Vector3 lookDirection = new Vector3(0, 0, 0);
    private new Rigidbody rigidbody;

    private int maxHealth = 10;
    private int currentHealth;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (MainManager.Instance == null) // May occur during editor mode if menu scene is skipped
        {
            Debug.Log("MainManger Instance could not be found. Please start game from Menu");
        }

        playerAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = MainManager.Instance.volumeLevel;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!gameManager.isGameOver && !gameManager.isGameComplete)
        {
            forwardInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            Vector3 move = new Vector3(horizontalInput, 0, forwardInput);

            // Check if player is moving
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.z, 0.0f))
            {
                playerAnim.SetBool("Walk", true);
                lookDirection.Set(move.x, move.y, move.z);
                lookDirection.Normalize();
            }

            // Player is not moving
            else
                playerAnim.SetBool("Walk", false);

            // Set player rotation to lookDirection
            transform.rotation = Quaternion.LookRotation(lookDirection);

            // Peck Attack
            if (Input.GetKeyDown(KeyCode.Space))
                Attack();

            // Fall off death
            if (transform.position.y < lowerBound)
                ChangeHealth(-maxHealth);
        }

        // Stop player from moving when game is complete
        else
            speed = 0;
    }

    private void FixedUpdate()
    {
        if (!gameManager.isGameOver)
        {
            Vector3 position = rigidbody.position;
            position.x = position.x + speed * horizontalInput * Time.deltaTime;
            position.z = position.z + speed * forwardInput * Time.deltaTime;

            rigidbody.MovePosition(position);
        }
    }

    private void Attack()
    {
        // Push Blocks away
        playerAnim.Play("Eat");
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 0.5f, LayerMask.GetMask("Block")))
        {
            Rigidbody enemyRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (hit.collider.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * 10f, ForceMode.Impulse);
            Debug.Log(hit.collider.name);
        }  
    }

    private void Die()
    {
        deathParticle.Play();
        gameManager.isGameOver = true;
        Destroy(gameObject, 0.8f); // Add a delay before destroying object
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
            audioSource.PlayOneShot(chickenHurtClip);

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.Instance.SetValue(currentHealth, maxHealth);
        Debug.Log(currentHealth + " / " + maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void CompleteLevel()
    {
        // Check to see if it is last level (no more new level)
        int totalLevels = SceneManager.sceneCountInBuildSettings - 1; // Exclude menu scene
        if (MainManager.Instance.currentLevel < totalLevels)
        {
            MainManager.Instance.currentLevel++;
            MainManager.Instance.LoadLevel();

            // Save progress only if beat highestLevel (since last load) --> Navigating to earlier level will load and update highestLevel
            if (MainManager.Instance.currentLevel > MainManager.Instance.highestLevel)
            {
                bool isLastLevel = false;
                MainManager.Instance.SaveLevel(isLastLevel);
            }  
        }
        // Finish last level
        else
        {
            gameManager.isGameComplete = true;
            bool isLastLevel = true;
            MainManager.Instance.SaveLevel(isLastLevel); // Saves a completionist badge. Level remains the same (no need to increment)
        }
    }
}
