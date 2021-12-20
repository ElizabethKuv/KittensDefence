using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    private float _originalScale;
    private static readonly int Death = Animator.StringToHash("Death");

    void Start()
    {
        _originalScale = gameObject.transform.localScale.x;
    }

    void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * _originalScale;
        gameObject.transform.localScale = tmpScale;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            GetComponentInParent<Animator>().SetTrigger(Death);
        }
    }
}