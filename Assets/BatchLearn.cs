using UnityEngine;

public class BatchLearn : MonoBehaviour
{
    [SerializeField] private GameObject sessionPrefab;

    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    
    [SerializeField] private Vector2 spaceBetweenSessions = new Vector2(10f, 10f);

    private void Awake()
    {
        for (var i = 0; i < width; i++)
        for (var j = 0; j < height; j++)
        {
            var session = Instantiate(sessionPrefab, transform);
            session.transform.position = new Vector3(i * spaceBetweenSessions.x, j * spaceBetweenSessions.y);
        }
    }
}