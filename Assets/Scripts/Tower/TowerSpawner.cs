using UnityEngine;
using Button = UnityEngine.UI.Button;
using UnityEngine.Tilemaps;


[System.Serializable]
public class Tower
{
    public GameObject towerPrefab;
    public Button towerButton;
    public int towerCost;
}

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Tower[] _towers;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _towersLayer;
    [SerializeField] private MoneyComponent _moneyComponent;
    [SerializeField] private Tilemap _tilemapGrass;

    private GameObject _towerPrefab;
    private Vector3 _positionToSpawn;
    private bool _isSpawning;
    private int _towerIndex;


    void Start()
    {
        MakeListenersForTowerButton(_towers.Length);
        
    }

    private void MakeListenersForTowerButton(int buttonsCount)
    {
        for (int i = 0; i < buttonsCount; i++)
        {
            var buttonHandler = i;
            _towers[i].towerButton.onClick.AddListener(delegate { SwitchButtonHandler(buttonHandler); });
        }
    }

    private void Update()
    {
        if (_isSpawning)
        {
            if (Input.GetMouseButton(0))
            {
                Spawn();
            }
        }
    }

    void SwitchButtonHandler(int towerIndex)
    {
        _towerIndex = towerIndex;
        _towerPrefab = _towers[towerIndex].towerPrefab;
        if (_towers[towerIndex].towerCost <= _moneyComponent.Money)
        {
            _isSpawning = true;
        }
        else
        {
            Debug.Log("Money?");
        }
    }


    void Spawn()
    {
        _positionToSpawn = GetPosition();
        //if touches the road with enemies
        if (_positionToSpawn == new Vector3(0, 0, -1200) && _isSpawning == true)
        {
            _isSpawning = false;
            return;
        }

        GameObject newTower = Instantiate(_towerPrefab, _positionToSpawn, Quaternion.identity);
        _moneyComponent.Money -= _towers[_towerIndex].towerCost;
        _isSpawning = false;
    }

    Vector3 GetPosition()
    {
        RaycastHit2D hit = Physics2D.CircleCast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), 0.1f, Vector2.zero,
            400f, _towersLayer);

        if (hit.collider != null)
        {
            Tilemap tilemap = _tilemapGrass.GetComponent<Tilemap>();
            Vector3Int cellPosition = tilemap.WorldToCell(hit.point);
            Vector3 positionToSpawn = tilemap.GetCellCenterWorld(cellPosition);
            return positionToSpawn;
        }

        // (if touches the road with enemies) returns the vector that definitely doesn't exist  
        Vector3 underplaneVector = new Vector3(0, 0, -1200);
        return underplaneVector;
    }
}