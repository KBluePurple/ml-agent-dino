using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Session session;
    private Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
    }

    private void Start()
    {
        session.OnScoreChanged += UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = $"{score:F0}";
    }
}