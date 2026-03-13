using UnityEngine;
using UnityEngine.InputSystem;  // <- novo Input System

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;

    [Header("Míssil")]
    public GameObject missilPrefab;
    public float velocidadeMissil = 8f;
    public float cooldownTiro     = 0.5f;

    [Header("Vidas")]
    public int vidas = 3;

    private bool  podeAtirar    = true;
    private float timerCooldown = 0f;
    private float limiteEsquerdo;
    private float limiteDireito;

    void Start()
    {
        Camera cam = Camera.main;
        limiteEsquerdo = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 0.5f;
        limiteDireito  = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 0.5f;
    }

    void Update()
    {
        Mover();
        Atirar();
    }

    // -------------------------------------------------------
    // Slide 9: Seta direita => aumenta X | Seta esquerda => diminui X
    // -------------------------------------------------------
    void Mover()
    {
        // Novo Input System: Keyboard.current em vez de Input.GetAxisRaw
        float entrada = 0f;
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            entrada = 1f;
        else if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            entrada = -1f;

        Vector3 pos = transform.position;
        pos.x += entrada * velocidade * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, limiteEsquerdo, limiteDireito);
        transform.position = pos;
    }

    // -------------------------------------------------------
    // Slide 9: Espaço => cria míssil no centro do player
    // -------------------------------------------------------
    void Atirar()
    {
        // Conta o cooldown
        if (!podeAtirar)
        {
            timerCooldown += Time.deltaTime;
            if (timerCooldown >= cooldownTiro)
            {
                podeAtirar    = true;
                timerCooldown = 0f;
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && podeAtirar)
        {
            Vector3 spawnPos = new Vector3(transform.position.x,
                                           transform.position.y + 0.5f,
                                           0f);
            GameObject missil = Instantiate(missilPrefab, spawnPos, Quaternion.identity);
            missil.GetComponent<MissilJogador>().velocidade = velocidadeMissil;

            podeAtirar    = false; // bloqueia até o cooldown acabar
            timerCooldown = 0f;
        }
    }

    // -------------------------------------------------------
    // Slide 10 / Slide 13: Player perde 1 vida ao ser atingido
    // -------------------------------------------------------
    public void PerderVida()
    {
        vidas--;
        GameManager.Instance.AtualizarVidasUI(vidas); // atualiza o texto de vidas na tela

        if (vidas <= 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            StartCoroutine(PiscarPlayer());
        }
    }

    System.Collections.IEnumerator PiscarPlayer()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 4; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Colisão com míssil inimigo (tag "MissilInimigo")
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MissilInimigo"))
        {
            Destroy(other.gameObject);
            PerderVida();
        }
    }
}