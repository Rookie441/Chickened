using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float lowerBound = -5;

    public Animator playerAnim;
    AudioSource audioSource;
    public AudioClip chickenHurtClip;
    public ParticleSystem deathParticle;

    private float horizontalInput;
    private float forwardInput;
    [SerializeField] private float speed = 1;
    private Vector3 lookDirection = new Vector3(1, 0, 0);
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private Rigidbody rigidbody;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    private int maxHealth = 10;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            forwardInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            Vector3 move = new Vector3(horizontalInput, 0, forwardInput);

            // Check if player is moving
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.z, 0.0f))
            {
                lookDirection.Set(move.x, move.y, move.z);
                lookDirection.Normalize();
                playerAnim.SetBool("Walk", true);
            }
            // Player is not moving
            else
            {
                playerAnim.SetBool("Walk", false);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 40f);

            // Peck Attack
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }

            // Fall off death
            if (transform.position.y < lowerBound)
            {
                ChangeHealth(-maxHealth);
            }
        }

    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            Vector3 position = rigidbody.position;
            position.x = position.x + speed * horizontalInput * Time.deltaTime;
            position.z = position.z + speed * forwardInput * Time.deltaTime;

            rigidbody.MovePosition(position);
        }
        
    }

    void Attack()
    {
        playerAnim.Play("Eat");
    }

    void Die()
    {
        deathParticle.Play();
        gameOver = true;
        Destroy(gameObject, 0.8f);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            audioSource.PlayOneShot(chickenHurtClip);
            
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth, maxHealth);
        Debug.Log(currentHealth + " / " + maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void CompleteLevel()
    {
        if (MainManager.Instance != null) //may occur during editor mode if menu scene is skipped
        {
            //Check to see if it is last level (no more new level)
            int totalLevels = SceneManager.sceneCountInBuildSettings - 1; //exclude menu scene
            if (MainManager.Instance.currentLevel < totalLevels)
            {
                MainManager.Instance.currentLevel++;
                MainManager.Instance.LoadLevel();
                //save progress
                MainManager.Instance.SaveLevel();
            }
            else
            {
                Debug.Log("Congratulations you completed all levels");
                //to-do: show omega badge on menu
            }
            
            

            
        }

    }
}
