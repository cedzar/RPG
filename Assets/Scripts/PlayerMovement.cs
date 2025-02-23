using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    public static bool canMove = true; // ✅ Nouvelle variable globale

    // ✅ Ajout pour les particules
    public ParticleSystem footstepParticles; // Référence au Particle System

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove)
        {
            animator.Play("Idle");

            // ✅ On arrête les particules en Idle
            if (footstepParticles.isPlaying)
            {
                footstepParticles.Stop();
            }
            return;
        }

        // Récupère les entrées ZQSD
        movement.x = Input.GetAxisRaw("Horizontal"); // Q (-1) / D (+1)
        movement.y = Input.GetAxisRaw("Vertical");   // Z (+1) / S (-1)

        bool isMoving = movement.sqrMagnitude > 0.1f; // Détecte le mouvement

        // ✅ On joue les particules si le joueur se déplace
        if (isMoving)
        {
            if (!footstepParticles.isPlaying)
            {
                footstepParticles.Play();
            }
        }
        else
        {
            footstepParticles.Stop();
        }

        // Si on ne bouge plus, repasser en Idle
        if (!isMoving)
        {
            animator.Play("Idle");
            return;
        }

        // Si on monte, on joue Walk_Up
        if (movement.y > 0)
        {
            animator.Play("Walk_Up");
        }
        else // Sinon, on joue Walk_Down pour tout le reste (droite, gauche, bas)
        {
            animator.Play("Walk_Down");
        }

        // Gestion du Flip pour gauche/droite
        if (movement.x != 0)
        {
            spriteRenderer.flipX = (movement.x < 0);
        }
    }

    void FixedUpdate()
    {
        if (canMove) // ✅ Applique le mouvement seulement si c'est autorisé
        {
            rb.linearVelocity = movement.normalized * speed; // ✅ Correction de linearVelocity à velocity
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // ✅ Stoppe le mouvement
        }
    }
}
