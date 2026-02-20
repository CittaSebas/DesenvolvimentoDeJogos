using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    
    // Adicionamos uma variável pública para você poder controlar a velocidade no Unity
    public float speed = 10f; 
    public float limiteMaximoY = -0.1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector3 playerPos = transform.position; 
        
        Vector3 dir = mousePos - playerPos; 

        //Debug.Log(playerPos);
        //Debug.Log(mousePos);

        if (playerPos.y > limiteMaximoY)
        {           
            transform.position = new Vector3(playerPos.x,0f,playerPos.z);
        }

        dir.Normalize(); 
        Vector3 speedVec = dir * speed; 
        //Debug.Log("Velocidad nuestra " + speedVec);
        var vel = rb2d.linearVelocity;
        vel.x = speedVec.x; 
        vel.y = speedVec.y; 
        rb2d.linearVelocity = vel; 

        

    }
}