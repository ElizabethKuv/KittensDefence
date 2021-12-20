using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private Text _healthCountLable;

    [SerializeField] private int _healthCount;

    private int _health;

    void Awake()
    {
        Health = _healthCount;
    }

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            _healthCountLable.text = "Lifes: " + _health;
        }
    }
}