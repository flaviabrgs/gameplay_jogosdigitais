using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 7f;
    public float forcaPulo = 10f;
    public float forcaSuperPulo = 18f; 
    public float limiteQueda = -15f; 

    [Header("Sistema de Recursos (Vida e Moedas)")]
    public int vidaAtual = 100;
    public int custoSuperPulo = 20; 
    public int contadorMoedas = 0;

    [Header("Interface (UI)")]
    public TextMeshProUGUI textoVidaUI;
    public TextMeshProUGUI textoPlacarUI;

    private Rigidbody2D rb;
    private bool estaNoChao;
    private Animator anim;

    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        AtualizarInterface(); 
    }

    void Update()
    {
        // 1. MOVIMENTAÇÃO LATERAL
        float moveX = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) moveX = 1;
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) moveX = -1;

        rb.linearVelocity = new Vector2(moveX * velocidade, rb.linearVelocity.y);

        // 2. PULO NORMAL 
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            estaNoChao = false; 
        }

        // 3. SUPER PULO
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && estaNoChao)
        {
            if (vidaAtual > custoSuperPulo)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaSuperPulo);
                estaNoChao = false;
                
                vidaAtual -= custoSuperPulo; // Consome o recurso "Vida"
                AtualizarInterface();
                Debug.Log("Especial ativado! Vida restante: " + vidaAtual);
            }
        }

        // 4. MORTE
        if (transform.position.y < limiteQueda || vidaAtual <= 0)
        {
            Morrer();
        }

        // 5. ANIMAÇÕES
        if (anim != null)
        {
            anim.SetBool("pulando", !estaNoChao);
            anim.SetFloat("velocidadeX", Mathf.Abs(moveX));
        }
    }

    // DETECÇÃO DE CHÃO E INIMIGOS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.relativeVelocity.y >= 0)
        {
            estaNoChao = true;
        }

        if (collision.gameObject.CompareTag("Inimigo"))
        {
            
            if (transform.position.y > collision.transform.position.y + 0.4f)
            {
                Destroy(collision.gameObject);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
                estaNoChao = true; 
            }
            else
            {
                Morrer();
            }
        }
    }

    // SISTEMA DE RECOMPENSAS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Moeda"))
        {
            contadorMoedas++;
            AtualizarInterface();
            Destroy(collision.gameObject);
            Debug.Log("Moeda coletada! Total: " + contadorMoedas);
        }
    }

    void AtualizarInterface()
    {
        if (textoVidaUI != null) textoVidaUI.text = "Vida: " + vidaAtual;
        if (textoPlacarUI != null) textoPlacarUI.text = "Moedas: " + contadorMoedas;
    }

    void Morrer()
    {
        var gerenciador = Object.FindFirstObjectByType<GerenciadorGameOver>();
        if (gerenciador != null)
        {
            gerenciador.AtivarGameOver();
        }
    }
}
