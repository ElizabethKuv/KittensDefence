using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [HideInInspector] public string enemyTag = "Enemy";

    public float range;
    public float fireRate;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float _fireCountdown = 0f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }


        if (_fireCountdown <= 0f)
        {
            Shoot();
            _fireCountdown = 1f / fireRate;
        }

        _fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject shootedBullet = (GameObject) Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = shootedBullet.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Hunt(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.21f, 0f);
        Gizmos.DrawWireSphere(transform.position, range);
    }
}