using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBall : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float limiteMaximoY = 8f;
    public float limiteMinimoY = -8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ballPos = transform.position;

         if (ballPos.y > limiteMaximoY || ballPos.y < limiteMinimoY)
        {           
            SceneManager.LoadSceneAsync(0);
        }

    }
 
}
