using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent onJump;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            onJump.Invoke();
        }
    }
}