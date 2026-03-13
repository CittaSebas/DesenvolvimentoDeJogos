using UnityEngine;

// -------------------------------------------------------
// Míssil do Jogador — Slide 9
// -------------------------------------------------------
public class MissilJogador : MonoBehaviour
{
    public float velocidade = 8f;

    private float limiteSuperior;

    void Start()
    {
        // Tag para identificação nas colisões
        gameObject.tag = "MissilJogador";

        Camera cam = Camera.main;
        limiteSuperior = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1f;
    }

    void Update()
    {
        // Slide 9: posição Y incrementada a cada loop (move para cima)
        transform.position += Vector3.up * velocidade * Time.deltaTime;

        // Slide 9: colide com parede superior → remove o míssil
        if (transform.position.y >= limiteSuperior)
        {
            Destroy(gameObject);
        }
    }

    // Colisão com nave inimiga é tratada no script do Invasor/NaveChefe
}
