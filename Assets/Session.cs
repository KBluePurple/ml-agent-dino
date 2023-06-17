using System;
using UnityEngine;
using UnityEngine.Events;

public class Session : MonoBehaviour, IInit
{
    [field:SerializeField] public ObstacleWorker ObstacleWorker { get; private set; }
    
    private int _score;

    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    public event Action<int> OnScoreChanged;
    public UnityEvent onInit;

    private void Update()
    {
        Score += (int)(Time.deltaTime * 1000f);
    }

    public void Init()
    {
        Score = 0;
        onInit?.Invoke();
    }
}