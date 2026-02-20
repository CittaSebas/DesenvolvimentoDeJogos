using UnityEngine;
public class ia_adversario : MonoBehaviour
{
    public Transform bola;
    private Rigidbody2D rb2d;
    public float speed = 2f; 
    public float limiteMaximoY = 0.1f;
 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bola = GameObject.Find("puck_0").transform;
    }

    void Update()
    {
        Vector3 ballPos = bola.position;
        Vector3 playerPos = transform.position; 
        
        Vector3 dir = ballPos - playerPos; 
        
        if (playerPos.y < limiteMaximoY)
        {           
            transform.position = new Vector3(playerPos.x,0.2f,playerPos.z);
        }

        dir.Normalize(); 
        Vector3 speedVec = dir * speed; 
        //Debug.Log("Velocidad " + speedVec);
        var vel = rb2d.linearVelocity;
        vel.x = speedVec.x; 
        vel.y = speedVec.y; 
        rb2d.linearVelocity = vel; 
    }
}
