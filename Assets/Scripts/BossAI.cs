using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float speed = 1.8f;
    private float force = 10.0f;
    private float lowerBound = -15.0f;
    public Animator enemyAnim;
    private GameObject player;
    private bool isAlive = true;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (isAlive && player!=null)
        {
            // Chase Player
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Destroy Out of Bounds
            if (transform.position.y < lowerBound)
            {
                Destroy(gameObject);
                isAlive = false;
                PlayerController controller = player.GetComponent<PlayerController>();
                if (controller != null)
                {
                    controller.CompleteLevel();
                }
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        // Duplicated enemy's box collider, but checked isTrigger to allow both collisions and triggers

        // Damage Player
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.ChangeHealth(-10);
            enemyAnim.Play("Attack 01");
        }

        // Push Blocks away
        if (other.gameObject.CompareTag("Block"))
        {
            enemyAnim.Play("Attack 02");
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * force, ForceMode.Impulse);
        }
    }
}
