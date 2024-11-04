using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform spellPoint;
    [SerializeField] private GameObject[] spellPoints;

    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        cooldownTimer = 0;

        spellPoints[FindSpell()].transform.position = spellPoint.position;
        spellPoints[FindSpell()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));  
    }

    private int FindSpell()
    {
        for (int i = 0; i < spellPoints.Length; i++)
        {
            if (!spellPoints[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
