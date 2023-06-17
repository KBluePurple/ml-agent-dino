using System.Collections.Generic;
using System.Linq;
using AchromaticDev.Util.Pooling;
using UnityEngine;

public class ObstacleWorker : MonoBehaviour, IInit
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float scrollSpeed = 1f;

    private readonly List<Obstacle> _obstacles = new();

    public float Speed => scrollSpeed;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 0f, 1f);
    }

    private void Update()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.transform.position += Vector3.left * (scrollSpeed * Time.deltaTime);

            if (!(obstacle.transform.position.x - transform.position.x < -10f)) continue;

            PoolManager.Destroy(obstacle.gameObject);
            _obstacles.Remove(obstacle);
            break;
        }
    }

    public void Init()
    {
        foreach (var obstacle in _obstacles) PoolManager.Destroy(obstacle.gameObject);
        _obstacles.Clear();
    }

    public void SpawnObstacle()
    {
        var obstacleObject = PoolManager.Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        var obstacle = obstacleObject.GetComponent<Obstacle>();

        const float screenWidthX = 10f;
        obstacleObject.transform.position = transform.position + Vector3.right * screenWidthX;

        obstacle.Init();
        _obstacles.Add(obstacle);
    }

    public Obstacle[] GetNearObstacle(float x)
    {
        var nearObstacle = _obstacles.Where(obstacle => obstacle.transform.position.x > x)
            .OrderBy(obstacle => obstacle.transform.position.x).ToArray();

        return nearObstacle.Length == 0 ? null : nearObstacle;
    }
}