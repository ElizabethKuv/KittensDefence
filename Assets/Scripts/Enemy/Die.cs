using UnityEngine;

public class Die : MonoBehaviour
{
    public void DestroyEnemy()
    {
        Destroy(transform.gameObject);
    }
}