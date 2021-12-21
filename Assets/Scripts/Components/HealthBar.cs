using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    private float _originalScale;
    private static readonly int Death = Animator.StringToHash("Death");

    private EnemySpawner _enemySpawner;
    private GameObject _road;

    void Start()
    {
        _originalScale = gameObject.transform.localScale.x;

        _road = GameObject.Find("Road");
        _enemySpawner = _road.GetComponent<EnemySpawner>();
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
            _enemySpawner.EnemyDefeated();
        }
    }
}