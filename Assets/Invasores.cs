using UnityEngine;

public class Invasor : MonoBehaviour
{
    public int tipo = 1;

    private float unidadesX        = 0.5f;
    private float unidadesVertical = 0.25f;
    private int   passosPorDirecao = 5;
    public float waitTime         = 0.8f;

    public GameObject missilInimigoPreab;
    public float velocidadeMissil = 2f;

    public float intervaloDeTiro = 5f;

    private int   passoAtual      = 0;
    private int   direcao         = 1;
    private int   direcaoVertical = -1;
    private float timerMovimento  = 0f;
    private float timerTiro       = 0f;
    private float limiteInferior;
    private float limiteSuperior;
    private bool  gameOverChamado = false;

    void Start()
    {
        Camera cam     = Camera.main;
        limiteInferior = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + 0.5f;
        limiteSuperior = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 0.5f;

        switch (tipo)
        {
            case 1: intervaloDeTiro = Random.Range(6f, 9f);  break;
            case 2: intervaloDeTiro = Random.Range(4f, 6f);  break;
            case 3: intervaloDeTiro = Random.Range(2f, 4f);  break;
        }
        timerTiro = Random.Range(0f, intervaloDeTiro);
    }

    void Update()
    {
        if (gameOverChamado) return;

        timerMovimento += Time.deltaTime;
        if (timerMovimento >= waitTime)
        {
            ChangeState();
            timerMovimento = 0f;
        }

        // --- Tiro por timer ---
        timerTiro += Time.deltaTime;
        if (timerTiro >= intervaloDeTiro)
        {
            Atirar();
            timerTiro = 0f;
        }

        // --- Game over se tocar o fundo ---
        if (transform.position.y <= limiteInferior && !gameOverChamado)
        {
            gameOverChamado = true;
            GameManager.Instance.GameOver();
        }
    }

    void ChangeState()
    {
        Vector3 pos = transform.position;

        if (passoAtual < passosPorDirecao)
        {
            pos.x += unidadesX * direcao;
            passoAtual++;
        }
        else
        {
            pos.y += unidadesVertical * direcaoVertical;
            direcao = -direcao;
            passoAtual = 0;

            if (pos.y <= limiteInferior + 3f)
                direcaoVertical = 1;
            else if (pos.y >= limiteSuperior - 0.5f)
                direcaoVertical = -1;
        }

        transform.position = pos;
    }

    void Atirar()
    {
        if (missilInimigoPreab == null) return;

        Vector3 spawnPos = new Vector3(transform.position.x,
                                       transform.position.y - 0.5f, 0f);
        GameObject missil = Instantiate(missilInimigoPreab, spawnPos, Quaternion.identity);
        MissilInimigo comp = missil.GetComponent<MissilInimigo>();
        if (comp != null) comp.velocidade = velocidadeMissil;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MissilJogador"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.AdicionarPontos(PontuacaoPorTipo());
            GameManager.Instance.InvasorDestruido();
            Destroy(gameObject);
        }
    }

    int PontuacaoPorTipo()
    {
        switch (tipo)
        {
            case 1: return 10;
            case 2: return 20;
            case 3: return 30;
            default: return 10;
        }
    }

    public void SetVelocidade(float novoWaitTime)
    {
        waitTime = novoWaitTime;
    }
}