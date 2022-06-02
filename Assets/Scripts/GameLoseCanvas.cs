using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoseCanvas : MonoBehaviour
{
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
