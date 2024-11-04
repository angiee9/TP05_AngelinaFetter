using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage1)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage1, 0, startingHealth);
        if (currentHealth > 0)
        {

        }
    }
}
