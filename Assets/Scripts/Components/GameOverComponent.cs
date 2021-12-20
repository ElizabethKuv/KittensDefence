using UnityEngine;
using UnityEngine.UI;

public class GameOverComponent : MonoBehaviour
{
    [SerializeField] Canvas _gameOverCanvas;
    [SerializeField] private Text _gameOverText;
    [SerializeField] HealthComponent _healthComponent;
    [SerializeField] LevelComponent _levelComponent;
    public static Canvas gameOverCanvas;
    public bool gameIsOver;

    private void Start()
    {
        gameOverCanvas = _gameOverCanvas;
        gameIsOver = false;
    }

    private void Update()
    {
        if (gameIsOver == true)
        {
            GameOver();
        }

        if (_healthComponent.Health <= 0)
        {
            gameIsOver = true;
        }

        if (_levelComponent.Level >= EnemySpawner.LevelsCount)
        {
            gameIsOver = true;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        if (_healthComponent.Health <= 0)
        {
            _gameOverText.text = "Maybe again?";
        }
        else
        {
            _gameOverText.text = "Congrats! You won:)";
        }

        gameOverCanvas.gameObject.SetActive(true);
    }
}