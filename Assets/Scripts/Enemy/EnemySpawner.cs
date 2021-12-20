using UnityEngine;


[System.Serializable]
public class Level
{
    public GameObject enemyPrefab;
    public float spawnInterval;
    public int maxEnemies;
    public int reward;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Level[] _levels;
    [SerializeField] private LevelComponent _levelComponent;
    [SerializeField] private MoneyComponent _moneyComponent;
    [SerializeField] private GameOverComponent _gameOverComponent;

    private float _lastSpawnTime;
    private int _enemiesSpawned = 0;
    private int _currentLevel;
    private float _currentTimeBetweenSpawn;
    private float _timeBetweenEnemies;
    private float _timeBetweenLevels;


    public static int LevelsCount;

    public GameObject[] roadPoints;

    private void Awake()
    {
        _lastSpawnTime = Time.time;
        _currentLevel = 0;
        _currentTimeBetweenSpawn = _levels[0].spawnInterval;
        LevelsCount = _levels.Length;
    }

    private void Update()
    {
        if (_currentLevel < _levels.Length - 1)
        {
            _timeBetweenEnemies = Time.time - _lastSpawnTime;
            if (((_enemiesSpawned == 0 && _currentTimeBetweenSpawn > _timeBetweenLevels) ||
                 _timeBetweenEnemies > _currentTimeBetweenSpawn) &&
                _enemiesSpawned < _levels[_currentLevel].maxEnemies)
            {
                _lastSpawnTime = Time.time;
                GameObject newEnemy = Instantiate(_levels[_currentLevel].enemyPrefab);
                newEnemy.GetComponent<Move>().roadPoints = roadPoints;

                _enemiesSpawned++;
            }

            if (_enemiesSpawned == _levels[_currentLevel].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                _levelComponent.Level += 1;
                _currentLevel += 1;
                //giving more money for the next level
                var moneyReward = _levels[_currentLevel].maxEnemies * _levels[_currentLevel].reward;
                _moneyComponent.Money = _moneyComponent.AddMoney(moneyReward);
                _enemiesSpawned = 0;
                _lastSpawnTime = Time.time;
            }
        }
        else
        {
            _gameOverComponent.gameIsOver = true;
        }
    }
}