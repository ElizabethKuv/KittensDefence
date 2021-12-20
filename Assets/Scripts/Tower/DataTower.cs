using UnityEngine;
using UnityEngine.UI;

public class DataTower : MonoBehaviour
{
    [SerializeField] private int _costPerTower;
    [SerializeField] private Text _costPerTowerLable;


    void Start()
    {
        TowerCost = _costPerTower;
    }

    public int TowerCost
    {
        get => _costPerTower;
        set
        {
            _costPerTower = value;
            _costPerTowerLable.text = "Cost " + _costPerTower;
        }
    }
}