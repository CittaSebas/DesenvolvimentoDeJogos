using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ================================================================
// BotoesUI.cs
// Coloque esse script num GameObject vazio chamado "BotoesUI"
// em CADA cena de fim de jogo (CenaDerrota e CenaVitoria).
// Depois aponte os botões para os métodos abaixo.
// ================================================================
public class BotoesUI : MonoBehaviour
{
    [Header("UI — opcional, preencha se quiser mostrar a pontuação")]
    public Text textoPontuacaoFinal;
    public Text textoTitulo;
    public Text textoDescricao;

    void Start()
    {
        int pontuacao = PlayerPrefs.GetInt("Pontuacao", 0);

        if (textoPontuacaoFinal != null)
            textoPontuacaoFinal.text = "Pontuação: " + pontuacao;

        // Detecta automaticamente qual cena está ativa para ajustar os textos
        string cenaAtual = SceneManager.GetActiveScene().name;

        if (cenaAtual == "CenaDerrota")
        {
            if (textoTitulo != null)     textoTitulo.text     = "GAME OVER";
            if (textoDescricao != null)  textoDescricao.text  = "Os alienígenas destruíram a Terra!";
        }
        else if (cenaAtual == "CenaVitoria")
        {
            if (textoTitulo != null)     textoTitulo.text     = "VITÓRIA!";
            if (textoDescricao != null)  textoDescricao.text  = "Você expulsou os invasores!";
        }
    }

    // -------------------------------------------------------
    // BOTÃO "JOGAR NOVAMENTE"
    // Como conectar:
    // 1. Selecione o Button na Hierarchy
    // 2. No Inspector, em "On Click ()" clique no "+"
    // 3. Arraste o GameObject "BotoesUI" para o campo vazio
    // 4. No dropdown, escolha: BotoesUI > JogarNovamente()
    // -------------------------------------------------------
    public void JogarNovamente()
    {
        PlayerPrefs.DeleteKey("Pontuacao");
        SceneManager.LoadScene("CenaJogo");
    }

    // -------------------------------------------------------
    // BOTÃO "MENU"
    // Mesmos passos acima, mas escolha: BotoesUI > IrParaMenu()
    // -------------------------------------------------------
    public void IrParaMenu()
    {
        PlayerPrefs.DeleteKey("Pontuacao");
        SceneManager.LoadScene("CenaMenu");
    }
}
