using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IInit
{
    public UnityEvent onPlayerDeath;
    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Kill()
    {
        onPlayerDeath?.Invoke();
    }

    public void Init()
    {
        transform.position = new Vector3(-5f, 0f, 0f) + transform.parent.position;
        Rigidbody2D.velocity = Vector2.zero;
    }
}