using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public bool IsDestroyed => currentHealth <= 0;

    [SerializeField] private Damaged Damaged;
    [SerializeField] private Destroyed Destroyed;
    [SerializeField] private int maxHealth;
    [SerializeField] private HealthBar healthBar;
    
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        InvokeRepeating(nameof(ApplyDamage), 1f, 1f);
    }

    private void ApplyDamage()  // temporary for testing
    {
        TakeDamage(10);
    }

    public void TakeDamage(int amount)
    {
        if (IsDestroyed || amount <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - amount);

        healthBar.SetFillAmount((float)currentHealth / maxHealth);

        Damaged.RaiseEvent(currentHealth);

        if (IsDestroyed)
        {
            healthBar.gameObject.SetActive(false);

            Destroyed.RaiseEvent(new DestructionInfo());
        }
    }
}