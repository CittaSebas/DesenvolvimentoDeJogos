using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private bool ballIsActive;
    private Vector3 ballPosition;
	private Vector2 ballInitialForce;
    public GameObject playerObject;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ballInitialForce = new Vector2 (100.0f,300.0f);
		// set to inactive 
		ballIsActive = false;
		// ball position 
		ballPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetButtonDown("Jump") == true) {
            if (!ballIsActive) {
                rb2d.isKinematic = false;
                rb2d.AddForce(ballInitialForce);  
                ballIsActive = true;
            }
        }

        if (!ballIsActive && playerObject != null) {
            ballPosition.x = playerObject.transform.position.x;
            transform.position = ballPosition;
        }

        if (ballIsActive && transform.position.y < -4.5) {
            ballIsActive = false;
            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = -3.55f;
            transform.position = ballPosition;

            rb2d.linearVelocity = Vector2.zero;   
            rb2d.angularVelocity = 0f;
            rb2d.isKinematic = true;
            GameManager.Instance.LoseLife();
        }
    }
}
