using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTabFunctions : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }   
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
