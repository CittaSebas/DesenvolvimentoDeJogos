using UnityEngine;

// ================================================================
// NaveChefe.cs
//
// CORREÇÃO DO BUG: O problema anterior era que o script chamava
// gameObject.SetActive(false), o que MATA todas as Coroutines do objeto.
// A solução é esconder a nave com o SpriteRenderer + Collider
// em vez de desativar o GameObject inteiro.
// ================================================================
public class NaveChefe : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 3f;

    [Header("Aparição")]
    public float tempoMinimoEntreAparicoes = 15f;
    public float tempoMaximoEntreAparicoes = 25f;

    [Header("Pontuação")]
    public int pontuacao = 50;

    // componentes
    private SpriteRenderer sr;
    private Collider2D      col;

    // limites
    private float limiteDireito;
    private float limiteEsquerdo;
    private float topoY;

    // estado
    private bool  ativa          = false;
    private float timerAparicao  = 0f;
    private float proximaAparicao;

    void Start()
    {
        sr  = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        Camera cam     = Camera.main;
        limiteEsquerdo = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).x - 1f;
        limiteDireito  = cam.ViewportToWorldPoint(new Vector3(1, 1, 0)).x + 1f;
        topoY          = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 0.5f;

        // Esconde a nave sem desativar o GameObject
        Esconder();

        // Define quando vai aparecer pela primeira vez
        proximaAparicao = Random.Range(tempoMinimoEntreAparicoes, tempoMaximoEntreAparicoes);
    }

    void Update()
    {
        if (!ativa)
        {
            // Conta o tempo até a próxima aparição
            timerAparicao += Time.deltaTime;
            if (timerAparicao >= proximaAparicao)
            {
                Aparecer();
            }
        }
        else
        {
            // Move para a direita
            transform.position += Vector3.right * velocidade * Time.deltaTime;

            // Chegou no lado direito: esconde e agenda próxima aparição
            if (transform.position.x >= limiteDireito)
            {
                Esconder();
            }
        }
    }

    void Aparecer()
    {
        // Posiciona no canto superior esquerdo
        transform.position = new Vector3(limiteEsquerdo, topoY, 0f);

        // Mostra a nave
        if (sr  != null) sr.enabled  = true;
        if (col != null) col.enabled = true;

        ativa = true;
    }

    void Esconder()
    {
        // Apenas esconde — NÃO desativa o GameObject para a Coroutine continuar
        if (sr  != null) sr.enabled  = false;
        if (col != null) col.enabled = false;

        ativa         = false;
        timerAparicao = 0f;
        proximaAparicao = Random.Range(tempoMinimoEntreAparicoes, tempoMaximoEntreAparicoes);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MissilJogador"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.AdicionarPontos(pontuacao);
            Esconder();
        }
    }
}