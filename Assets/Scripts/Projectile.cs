using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explode");
    }

    public void SetDirection(float direction1)
    {
        lifeTime = 0;
        direction = direction1;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        
        if(Mathf.Sign(localScaleX) != direction1)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
