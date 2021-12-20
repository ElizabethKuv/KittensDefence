using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transformTarget;
    public float speed = 30f;
    [SerializeField] private float damage;

    public void Seek(Transform target)
    {
        _transformTarget = target;
    }

    void Update()
    {
        if (_transformTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _transformTarget.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);


        void HitTarget()
        {
            Destroy(gameObject);

            var healthBar = _transformTarget.gameObject.GetComponentInChildren<HealthBar>();
            healthBar.Damage(damage);
        }
    }
}