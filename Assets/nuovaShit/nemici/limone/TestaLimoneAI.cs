using UnityEngine;
using UnityEngine.AI;

public class TestaLimoneAI : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 500;
    public int damage = 1;
    public float detectionRange = 15f;
    public float chargeDelay = 3f;
    
    [Header("Movement")]
    public float walkSpeed = 3.5f;
    public float runSpeed = 10f;

    [Header("Components")]
    public NavMeshAgent agent;

    private Transform player;
    private int currentHealth;
    private float timer = 0f;
    private bool isCharging = false;
    private bool isPreparing = false;

    void Start()
    {
        currentHealth = maxHealth;
        agent.speed = walkSpeed;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange || isPreparing)
        {
            if (!isCharging)
            {
                PrepareAttack();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    void PrepareAttack()
    {
        isPreparing = true;
        agent.isStopped = true;
        //animator.SetBool("IsRunning", false);

        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPos);

        timer += Time.deltaTime;

        if (timer >= chargeDelay)
        {
            isCharging = true;
            isPreparing = false;
            agent.isStopped = false;
            timer = 0f;
        }
    }

    void ChasePlayer()
    {
        agent.speed = runSpeed;
        agent.SetDestination(player.position);
        ////animator.SetBool("IsRunning", true);
    }

    void Patrol()
    {
        agent.speed = walkSpeed;
        //animator.SetBool("IsRunning", false);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // danno al player va messo quii
            collision.gameObject.GetComponent<HealthScript>()?.TakeDamage(damage);
            isCharging = false;
            isPreparing = true;
            timer = 0f;
            agent.isStopped = true;
            //animator.SetBool("IsRunning", false);
            
            
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}