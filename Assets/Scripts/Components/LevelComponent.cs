using UnityEngine;
using UnityEngine.UI;

public class LevelComponent : MonoBehaviour
{
    [SerializeField] private Text _levelCountLable;

    private int _level;

    void Awake()
    {
        Level = 0;
    }

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            _levelCountLable.text = "Level: " + _level;
        }
    }
}