using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour
{
    public GameObject retryButton; 

    void Start()
    {
        retryButton.SetActive(false);
    }

    public void GameOver()
    {
        retryButton.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Level 1");
    }
}