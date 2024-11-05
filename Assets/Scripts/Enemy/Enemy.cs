using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;

    private Vector3 initScale;
    private bool movingLeft;
    private float idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;

        animator.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private void Update()
    {
            if (movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    DirectionChange();
                }
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                {
                    MoveInDirection(1);
                }
                else
                {
                    DirectionChange();
                }
            }
        
        

    }

    private void DirectionChange()
    {
        animator.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

}
