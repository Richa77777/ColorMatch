using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private int _difficultyIndex = 0;

    public void LoadGame()
    {
        PlayerPrefs.SetInt("Difficulty", _difficultyIndex);
        SceneManager.LoadScene("Game");
    }
}
