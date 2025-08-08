using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// Handles player movement, root motion, stamina, health, and combat.
/// Attach to the Player prefab with Animator and Rigidbody.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpForce = 5f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float staminaDrain = 20f;
    public float staminaRegen = 10f;
    public float attackStaminaCost = 15f;
    public float sprintStaminaCost = 10f;

    [Header("Combat")]
    public float attackRange = 1.5f;
    public int attackDamage = 25;
    public LayerMask enemyLayer;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public Slider healthBar;
    public Slider staminaBar;

    private Animator anim;
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isSprinting = false;
    private bool isAttacking = false;
    private float inputH, inputV;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        if (healthBar) healthBar.maxValue = maxHealth;
        if (staminaBar) staminaBar.maxValue = maxStamina;
    }

    void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0 && inputV > 0.1f;
        if (Input.GetButtonDown("Jump") && isGrounded) Jump();
        if (Input.GetButtonDown("Fire1") && stamina >= attackStaminaCost && !isAttacking) Attack();
        UpdateStamina();
        UpdateUI();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = new Vector3(inputH, 0, inputV).normalized;
        if (move.magnitude > 0.1f)
        {
            float speed = isSprinting ? runSpeed : walkSpeed;
            Vector3 moveDir = Camera.main.transform.TransformDirection(move);
            moveDir.y = 0;
            rb.MovePosition(transform.position + moveDir.normalized * speed * Time.fixedDeltaTime);
            transform.forward = moveDir.normalized;
        }
        anim.SetFloat("Speed", move.magnitude * (isSprinting ? 2f : 1f));
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        anim.SetTrigger("Jump");
        isGrounded = false;
    }

    void Attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        stamina -= attackStaminaCost;
        Invoke(nameof(DoAttack), 0.3f); // Sync with animation
        Invoke(nameof(ResetAttack), 0.7f);
    }

    void DoAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward, attackRange, enemyLayer);
        foreach (var hit in hits)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();
            if (enemy) enemy.TakeDamage(attackDamage);
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void UpdateStamina()
    {
        if (isSprinting && (inputH != 0 || inputV != 0))
            stamina = Mathf.Max(0, stamina - sprintStaminaCost * Time.deltaTime);
        else
            stamina = Mathf.Min(maxStamina, stamina + staminaRegen * Time.deltaTime);
    }

    void UpdateUI()
    {
        if (healthBar) healthBar.value = currentHealth;
        if (staminaBar) staminaBar.value = stamina;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) GameManager.Instance.LoseGame();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f) isGrounded = true;
    }
}
