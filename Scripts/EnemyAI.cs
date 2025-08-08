using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemy AI with patrol, chase, attack, and search states. Uses NavMesh.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack, Search }
    public State currentState = State.Patrol;

    public Transform[] patrolPoints;
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public float attackRange = 1.5f;
    public int attackDamage = 15;
    public float searchTime = 3f;
    public int maxHealth = 50;
    public float adaptiveSpeed = 3.5f;

    private int currentPatrol = 0;
    private float searchTimer = 0f;
    private int currentHealth;
    private NavMeshAgent agent;
    private Transform player;
    private Animator anim;
    private bool playerVisible = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        agent.speed = adaptiveSpeed;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: Chase(); break;
            case State.Attack: Attack(); break;
            case State.Search: Search(); break;
        }
        AdaptiveDifficulty();
    }

    void Patrol()
    {
        anim.SetFloat("Speed", 1f);
        if (patrolPoints.Length == 0) return;
        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentPatrol].position);
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrol].position) < 1f)
            currentPatrol = (currentPatrol + 1) % patrolPoints.Length;
        if (CanSeePlayer()) currentState = State.Chase;
    }

    void Chase()
    {
        anim.SetFloat("Speed", 2f);
        agent.isStopped = false;
        agent.SetDestination(player.position);
        if (!CanSeePlayer()) { currentState = State.Search; searchTimer = searchTime; }
        else if (Vector3.Distance(transform.position, player.position) < attackRange)
            currentState = State.Attack;
    }

    void Attack()
    {
        agent.isStopped = true;
        anim.SetTrigger("Attack");
        if (Vector3.Distance(transform.position, player.position) > attackRange)
            currentState = State.Chase;
        else
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    void Search()
    {
        anim.SetFloat("Speed", 0f);
        agent.isStopped = true;
        searchTimer -= Time.deltaTime;
        if (searchTimer <= 0) currentState = State.Patrol;
        if (CanSeePlayer()) currentState = State.Chase;
    }

    bool CanSeePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, player.position) < visionRange)
        {
            if (Vector3.Angle(transform.forward, dir) < visionAngle / 2)
            {
                if (!Physics.Linecast(transform.position + Vector3.up, player.position + Vector3.up, out RaycastHit hit) || hit.transform == player)
                    return true;
            }
        }
        return false;
    }

    void AdaptiveDifficulty()
    {
        int coins = GameManager.Instance.coinsCollected;
        agent.speed = adaptiveSpeed + coins * 0.1f;
        visionRange = 10f + coins * 0.5f;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        anim.SetTrigger("Die");
        agent.isStopped = true;
        Destroy(gameObject, 2f);
    }
}
