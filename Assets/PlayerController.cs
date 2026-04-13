using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar o jogo

public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 7f;
    public float forcaPulo = 10f;
    
    private Rigidbody2D rb;
    private bool estaNoChao;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) moveX = 1;
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) moveX = -1;

        
        rb.linearVelocity = new Vector2(moveX * velocidade, rb.linearVelocity.y);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }

        
        if (anim != null)
        {
            anim.SetBool("pulando", !estaNoChao);
            anim.SetFloat("velocidadeX", Mathf.Abs(moveX));
        }
    }

    // COLISÕES
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            estaNoChao = true;
        }

        
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            
            if (transform.position.y > collision.transform.position.y + 0.4f)
            {
                Destroy(collision.gameObject);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            }
            else
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
