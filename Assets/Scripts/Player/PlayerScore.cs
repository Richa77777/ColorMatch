using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    public int Score { get; private set; } = 0;

    public void AddScore(int value)
    {
        Score = Mathf.Clamp(Score + value, 0, int.MaxValue);

        string recordKey = "Record" + GameController.Instance.CurrentDifficulty.name;

        if (Score > PlayerPrefs.GetInt(recordKey))
        {
            PlayerPrefs.SetInt(recordKey, Score);
        }

        UpdateText();
    }

    private void UpdateText()
    {
        _scoreText.text = Score.ToString();
    }
}