using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public KeyCode upKey;
    [HideInInspector]
    public KeyCode downKey;
    [HideInInspector]
    public KeyCode leftKey;
    [HideInInspector]
    public KeyCode rightKey;
    [HideInInspector]
    public KeyCode lightAttackKey;
    [HideInInspector]
    public KeyCode heavyAttackKey;
    [HideInInspector]
    public KeyCode specialAttackKey;
    [HideInInspector]
    public KeyCode defenseKey;
    [HideInInspector]
    public KeyCode dashKey;


    //------- Player Movements Variables--------

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundDetection;
    [SerializeField] private LayerMask groundLayer;


    //---------

    private float horizontal;
    public float playerSpeed;
    public float jumpSpeed;
    private bool isFacingRight = true;

    //-------
    //------- Variables para los efectos adicionales de salto--------
    public float jumpHeightMultiplier = 1.5f; // Factor multiplicador para altura del salto.
    public float fallMultiplier = 2.5f; // Factor multiplicador de la gravedad al caer.
    public float lowJumpMultiplier = 2f; // Factor multiplicador para saltos pequeños.
    public float hangTime = 0.2f; // Tiempo de "flotación" en el aire al llegar al pico del salto.
    public float peakMovementBonus = 1.2f; // Factor de velocidad adicional en el pico del salto.
    public float jumpTimeStretch = 0.15f; // Tiempo extra al moverse horizontalmente en el aire.

    private float hangTimeCounter;
    private bool isJumping;

    private bool cancelNormalImpulse = false;

    //---- V ariable de Doble Salto
    private bool canDoubleJump = false; // Indica si el jugador puede hacer un doble salto.

    //--- Para Actualizar los inputs de forma remota----
    void OnEnable()
    {
        SettingsManager.OnKeyBindingChanged += ReloadKeyBindings;

        PlayerSkills playerSkills  = gameObject.GetComponent<PlayerSkills>();
        if (playerSkills != null)
        {
            playerSkills.OnMovementSkillsChanged += HandleMovementSkillsChanged;
        }
    }

    void OnDisable()
    {
        SettingsManager.OnKeyBindingChanged -= ReloadKeyBindings;
        PlayerSkills playerSkills  = gameObject.GetComponent<PlayerSkills>();
        if (playerSkills != null)
        {
            playerSkills.OnMovementSkillsChanged -= HandleMovementSkillsChanged;
        }
    }

    void Start()
    {
        // Cargar las configuraciones de teclas desde PlayerPrefs, o mantener el valor actual si no hay configuración guardada
        ReloadKeyBindings();
    }

    KeyCode LoadKeyFromPrefs(string keyName, KeyCode defaultKey)
    {
        string keyString = PlayerPrefs.GetString(keyName, defaultKey.ToString());
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), keyString);
    }

    void Update()
    {
        //-- Logica del movimiento Horizontal--
        MovePlayer_ByHorizontal();

        //-- Logica del movimiento Vetical--

         if (Input.GetKeyDown(upKey))
        {
            if (PlayerIsGrounded())
            {
                isJumping = true;
                canDoubleJump = true; // Habilitar el doble salto después de saltar desde el suelo.
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                hangTimeCounter = hangTime; // Resetea el contador de tiempo de hang.
            }
            else if (canDoubleJump)
            {
                // Doble salto
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                canDoubleJump = false; // Deshabilitar el doble salto después de usarlo.
            }
        }

        if (Input.GetKeyUp(upKey))
        {
            isJumping = false;
            if (rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        if (Input.GetKeyDown(downKey))
        {
            // Acción para tecla de movimiento hacia abajo
        }

        FlipPlayer();

        //---------------------
        //------ Botones que ataque


        if (Input.GetKeyDown(lightAttackKey))
        {
            // Acción para ataque ligero
        }
        if (Input.GetKeyDown(heavyAttackKey))
        {
            // Acción para ataque fuerte
        }
        if (Input.GetKeyDown(specialAttackKey))
        {
            // Acción para ataque especial
        }
        if (Input.GetKeyDown(defenseKey))
        {
            // Acción para defensa
        }
        if (Input.GetKeyDown(dashKey))
        {
            // Acción para dash
        }

        
    }

    private void FixedUpdate() 
    {
        if (!cancelNormalImpulse)
        {
            rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        }
        

    }

    void MovePlayer_ByHorizontal()
        {
        horizontal = 0;

        if (Input.GetKey(leftKey))
        {
            horizontal -= 1;
        }

        if (Input.GetKey(rightKey))
        {
            horizontal += 1;
        }
    }

    private bool PlayerIsGrounded()
    {
        return Physics2D.OverlapCircle(groundDetection.position, 0.2f, groundLayer);
    }

    private void FlipPlayer()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void ReloadKeyBindings()
    {
        // Lógica para recargar las teclas
        upKey = LoadKeyFromPrefs("upKey", KeyCode.W);
        downKey = LoadKeyFromPrefs("downKey", KeyCode.S);
        leftKey = LoadKeyFromPrefs("leftKey", KeyCode.A);
        rightKey = LoadKeyFromPrefs("rightKey", KeyCode.D);
        lightAttackKey = LoadKeyFromPrefs("lightAttackKey", KeyCode.H);
        heavyAttackKey = LoadKeyFromPrefs("heavyAttackKey", KeyCode.J);
        specialAttackKey = LoadKeyFromPrefs("specialAttackKey", KeyCode.K);
        defenseKey = LoadKeyFromPrefs("defenseKey", KeyCode.L);
        dashKey = LoadKeyFromPrefs("dashKey", KeyCode.Space);
    }

    void HandleMovementSkillsChanged(bool isUsing)
    {
        cancelNormalImpulse = isUsing;
    }
}
