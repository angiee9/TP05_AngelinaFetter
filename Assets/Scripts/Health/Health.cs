using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberFlashes;
    private SpriteRenderer spriteRenderer;

    SoundManager soundManager;

    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    public void TakeDamage(float damage1)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage1, 0, startingHealth);
        if (currentHealth > 0)
        {
            soundManager.PlaySFX(soundManager.hurt);
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if(!dead)
            {
                
                soundManager.PlaySFX(soundManager.death);
                anim.SetTrigger("die");

                if (GetComponent<PlayerMovement>() != null)
                {
                    GameManager.Instance.ShowGameOverScreen();
                    GetComponent<PlayerMovement>().enabled = false;
                }

                if (GetComponent<Enemy>() != null)
                {
                    GetComponent<Enemy>().enabled = false;
                }

               
                dead = true;
            }
        }
    }

    public void AddHealth(float value1)
    {

        currentHealth = Mathf.Clamp(currentHealth + value1, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 5f);
            yield return new WaitForSeconds(iFramesDuration / (numberFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

}
