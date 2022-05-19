using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float speed = 1.5f;
    private float pushForce = 10.0f;
    public Animator enemyAnim;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        // Duplicated enemy's box collider, but checked isTrigger to allow both collisions and triggers
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.ChangeHealth(-3);
            controller.playerAnim.Play("Run In Place");
            enemyAnim.Play("Attack 01");
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * pushForce, ForceMode.Impulse);
      
        }

        // When reach patrol point, change direction
        if (other.gameObject.CompareTag("Patrol"))
        {
            transform.Rotate(new Vector3(0, 1, 0), 270);
        }
    }
}
