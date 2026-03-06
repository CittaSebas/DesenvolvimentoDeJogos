using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScrollObjectGO : MonoBehaviour
{
    public RectTransform textRect;    
    public float scrollSpeed = 80f;   
    public float endPositionY = 700f; 
    public string nextScene = "Level 1"; 

    void Update()
    {
        textRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (textRect.anchoredPosition.y >= endPositionY)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}