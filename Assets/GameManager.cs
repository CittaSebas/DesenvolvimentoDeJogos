using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text textoPontuacao;
    public Text textoVidas; 
    public Text textoContagem; 

    [Header("Contagem regressiva antes de começar")]
    public float tempoContagem = 3f;

    [Header("Velocidade dos invasores")]
    public float waitTimeInicial  = 0.8f;
    public float reducaoWaitTime  = 0.03f;
    public float waitTimeMinimo   = 0.15f;

    // estado
    private int   pontuacao          = 0;
    private int   invasoresRestantes = 0;
    private float waitTimeAtual;
    private bool  jogoAtivo          = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        waitTimeAtual        = waitTimeInicial;
        invasoresRestantes   = FindObjectsOfType<Invasor>().Length;

        SetInvasoresAtivos(false);

        AtualizarUI();
        StartCoroutine(ContagemRegressiva());
    }

    IEnumerator ContagemRegressiva()
    {
        if (textoContagem != null) textoContagem.gameObject.SetActive(true);

        for (int i = (int)tempoContagem; i > 0; i--)
        {
            if (textoContagem != null) textoContagem.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        if (textoContagem != null) textoContagem.text = "Destura os Ze Raqueteues";
        yield return new WaitForSeconds(0.7f);

        if (textoContagem != null) textoContagem.gameObject.SetActive(false);

        SetInvasoresAtivos(true);
        jogoAtivo = true;
    }

    void SetInvasoresAtivos(bool ativo)
    {
        foreach (Invasor inv in FindObjectsOfType<Invasor>(true))
            inv.enabled = ativo;
    }

    public void InvasorDestruido()
    {
        invasoresRestantes--;
        waitTimeAtual = Mathf.Max(waitTimeMinimo, waitTimeAtual - reducaoWaitTime);
        foreach (Invasor inv in FindObjectsOfType<Invasor>())
            inv.SetVelocidade(waitTimeAtual);

        if (invasoresRestantes <= 0)
            Vitoria();
    }

    public void AdicionarPontos(int pts)
    {
        pontuacao += pts;
        AtualizarUI();
    }

    public void GameOver()
    {
        if (!jogoAtivo) return;
        jogoAtivo = false;
        PlayerPrefs.SetInt("Pontuacao", pontuacao);
        PlayerPrefs.Save();
        StartCoroutine(CarregarCenaComAtraso("CenaDerrota", 1f));
    }

    public void Vitoria()
    {
        jogoAtivo = false;
        PlayerPrefs.SetInt("Pontuacao", pontuacao);
        PlayerPrefs.Save();
        StartCoroutine(CarregarCenaComAtraso("CenaVitoria", 1f));
    }

    IEnumerator CarregarCenaComAtraso(string nomeCena, float atraso)
    {
        yield return new WaitForSeconds(atraso);
        SceneManager.LoadScene(nomeCena);
    }

    void AtualizarUI()
    {
        if (textoPontuacao != null)
            textoPontuacao.text = "Pontos: " + pontuacao;

        PlayerController player = FindObjectOfType<PlayerController>();
        if (textoVidas != null && player != null)
            textoVidas.text = "Vidas: " + player.vidas;
    }
      public void JogarNovamente()
    {
        PlayerPrefs.DeleteKey("Pontuacao");
        SceneManager.LoadScene("SpaceInvaders");
    }
    public void AtualizarVidasUI(int vidas)
    {
        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;
    }
}