using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configura��es de Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 12f;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator animator; // Para controlar as anima��es que criamos

    [Header("Verifica��o de Ch�o")]
    public Transform verificadorChao; // Um objeto vazio nos p�s da tartaruga
    public LayerMask camadaChao;      // O que o jogo considera "ch�o"
    private bool estaNoChao;
    private float raioVerificacao = 0.2f;

    private float inputHorizontal;
    private bool olhandoParaDireita = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Recebe a entrada do teclado (Setas ou A/D)
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        // 2. Verifica o bot�o de Pulo (Barra de Espa�o)
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            Pular();
        }

        // 3. Verifica a dire��o do olhar
        if (inputHorizontal > 0 && !olhandoParaDireita)
        {
            Virar();
        }
        else if (inputHorizontal < 0 && olhandoParaDireita)
        {
            Virar();
        }

        // 4. Atualiza as Anima��es
        AtualizarAnimacoes();
    }

    void FixedUpdate()
    {
        // A f�sica deve ser tratada no FixedUpdate
        Mover();

        // Verifica se est� tocando no ch�o
        estaNoChao = Physics2D.OverlapCircle(verificadorChao.position, raioVerificacao, camadaChao);
    }

    void Mover()
    {
        // Aplica velocidade no eixo X, mant�m a velocidade atual no eixo Y (gravidade)
        rb.linearVelocity = new Vector2(inputHorizontal * velocidade, rb.linearVelocity.y);
    }

    void Pular()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
    }

    void Virar()
    {
        olhandoParaDireita = !olhandoParaDireita;
        // Inverte a escala do objeto para virar o sprite e a arma (p�) junto
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void AtualizarAnimacoes()
    {
        if (animator != null)
        {
            // "IsWalking" deve ser um par�metro Bool no seu Animator
            // Se o input for diferente de 0, ela est� andando
            animator.SetBool("IsWalking", inputHorizontal != 0);

            // "IsJumping" deve ser um par�metro Bool no seu Animator
            animator.SetBool("IsJumping", !estaNoChao);
        }
    }
}
