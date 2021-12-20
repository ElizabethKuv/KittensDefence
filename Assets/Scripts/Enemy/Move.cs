using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    [HideInInspector] public GameObject[] roadPoints;
    public float speed = 1.0f;

    private int _currentPoint = 0;
    private float _lastSwitchTime;
    private float _roadPointLength;
    private float _totalTimeOnRoadPoint;
    private float _currentTimeOnRoadPoint;
    private Vector3 _directionVector;


    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");


    void Start()
    {
        _lastSwitchTime = Time.time;
    }

    private void Update()
    {
        Vector3 startPosition = roadPoints[_currentPoint].transform.position;
        Vector3 endPosition = roadPoints[_currentPoint + 1].transform.position;

        _roadPointLength = (endPosition - startPosition).magnitude;
        _totalTimeOnRoadPoint = _roadPointLength / speed;
        _currentTimeOnRoadPoint = Time.time - _lastSwitchTime;
        var fractionOfJourney = _currentTimeOnRoadPoint / _totalTimeOnRoadPoint;
        transform.position = Vector2.Lerp(startPosition, endPosition, fractionOfJourney);

        if (gameObject.transform.position.Equals(endPosition))
        {
            //if this is the last but one point or another not last  point, so that  go to the next one
            if (_currentPoint < roadPoints.Length - 2)
            {
                _currentPoint++;
                _lastSwitchTime = Time.time;
                ChangeDirection();
            }
            else
            {
                HealthComponent healthComponent =
                    GameObject.Find("LevelManager").GetComponent<HealthComponent>();
                healthComponent.Health -= 1;
                Destroy(gameObject);
            }
        }
    }

    private void ChangeDirection()
    {
        Vector3 startPosition = roadPoints[_currentPoint].transform.position;
        Vector3 endPosition = roadPoints[_currentPoint + 1].transform.position;


        if (startPosition.x > endPosition.x)
        {
            _directionVector = Vector3.left;
        }
        else if (startPosition.x < endPosition.x)
        {
            _directionVector = Vector3.right;
        }

        else if (startPosition.y > endPosition.y)
        {
            _directionVector = Vector3.down;
        }
        else if (startPosition.y < endPosition.y)


        {
            _directionVector = Vector3.up;
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        _animator.SetFloat(MoveX, _directionVector.x);
        _animator.SetFloat(MoveY, _directionVector.y);
    }

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            roadPoints[_currentPoint + 1].transform.position);
        for (int i = _currentPoint + 1; i < roadPoints.Length - 1; i++)
        {
            Vector3 startPosition = roadPoints[i].transform.position;
            Vector3 endPosition = roadPoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }

        return distance;
    }
}