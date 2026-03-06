using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxLives = 3;
    private int currentLives;
    private GameObject blocksParent;  

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        currentLives = maxLives;
        FindBlocksParent();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindBlocksParent(); 
    }

    void FindBlocksParent()
    {
        blocksParent = GameObject.Find("Blocks"); 
        if (blocksParent == null)
        {
            Debug.LogWarning("Objeto 'Blocks' não encontrado na cena!");
        }
    }
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)
        {
            LoadNextLevel();
        }
    }

    public void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public int GetLives()
    {
        return currentLives;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 3.0f, 200.0f, 200.0f), "Vidas: " + currentLives);
    }

    void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}