using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    public float Score { get; private set; } = 0f;

    public void AddScore(float value)
    {
        Score = Mathf.Clamp(Score + value, 0, float.MaxValue);
        UpdateText();
    }

    private void UpdateText()
    {
        _scoreText.text = Score.ToString();
    }
}
