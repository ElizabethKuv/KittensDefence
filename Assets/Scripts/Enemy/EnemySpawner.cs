using System.Collections;
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

    private int _enemiesSpawned = 0;
    private int _currentLevel;
    private int _totalEnemiesInCurrentLevel;
    private int _enemiesInLevelLeft;


    public static int LevelsCount;
    public GameObject[] roadPoints;

    private void Start()
    {
        LevelsCount = _levels.Length - 1;
        //to avoid out of bounds, cuz StartNextLevel() on Start before 1st level
        _currentLevel = -1;

        StartNextLevel();
    }

    void StartNextLevel()
    {
        _currentLevel++;
        //if win
        if (_currentLevel > LevelsCount)
        {
            _gameOverComponent.gameIsOver = true;
            return;
        }

        _totalEnemiesInCurrentLevel = _levels[_currentLevel].maxEnemies;
        _enemiesInLevelLeft = 0;
        _enemiesSpawned = 0;
        _levelComponent.Level += 1;
        var moneyReward = _levels[_currentLevel].maxEnemies * _levels[_currentLevel].reward;
        if (_currentLevel > 0)
        {
            _moneyComponent.Money = _moneyComponent.AddMoney(moneyReward);
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (_enemiesSpawned < _totalEnemiesInCurrentLevel)
        {
            _enemiesSpawned++;
            _enemiesInLevelLeft++;

            GameObject newEnemy = Instantiate(_levels[_currentLevel].enemyPrefab);
            newEnemy.GetComponent<Move>().roadPoints = roadPoints;
            yield return new WaitForSeconds(_levels[_currentLevel].spawnInterval);
        }

        yield return null;
    }


    public void EnemyDefeated()
    {
        _enemiesInLevelLeft--;

        // Start the next level , when we have spawned and defeated all enemies
        if (_enemiesInLevelLeft == 0 && _enemiesSpawned == _totalEnemiesInCurrentLevel)
        {
            StartNextLevel();
            if (_levelComponent.Level > _levels.Length)
            {
                _gameOverComponent.gameIsOver = true;
            }
        }
    }

    private void Update()
    {
        if (_currentLevel < _levels.Length - 1)
        {
            if (_enemiesSpawned == _levels[_currentLevel].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                EnemyDefeated();
            }
        }
    }
}