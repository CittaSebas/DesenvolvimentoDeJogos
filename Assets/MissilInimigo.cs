using UnityEngine;

// -------------------------------------------------------
// Míssil Inimigo — Slide 10
// -------------------------------------------------------
public class MissilInimigo : MonoBehaviour
{
    public float velocidade = 3f;

    private float limiteInferior;

    void Start()
    {
        gameObject.tag = "MissilInimigo";

        Camera cam = Camera.main;
        limiteInferior = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 1f;
    }

    void Update()
    {
        // Slide 10: posição Y decrementada a cada loop (move para baixo)
        transform.position += Vector3.down * velocidade * Time.deltaTime;

        // Slide 10: colide com parede inferior → remove o míssil
        if (transform.position.y <= limiteInferior)
        {
            Destroy(gameObject);
        }
    }

    // Colisão com player é tratada no PlayerController
}
