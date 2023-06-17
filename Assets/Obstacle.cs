using UnityEngine;

public class Obstacle : MonoBehaviour, IInit
{
    private const float RandomYRange = 2f;

    public void Init()
    {
        var randomY = Random.Range(-RandomYRange, RandomYRange);
        transform.position = new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z);
    }
}