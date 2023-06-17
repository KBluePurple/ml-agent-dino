using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void Jump()
    {
        _player.Rigidbody2D.velocity = Vector2.up * jumpForce;
    }
}