using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int hitsToKill;
    public int points;
    private int numberOfHits;

    void Start()
    {
        numberOfHits = 0;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            numberOfHits++;
            if (numberOfHits >= hitsToKill)
            {
                Destroy(this.gameObject);
            }
        }
    }
}