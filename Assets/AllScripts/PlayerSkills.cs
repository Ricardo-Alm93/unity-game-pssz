using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSkills : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public bool canDashInAir = true;

    private bool canDash = true;
    private bool using_MovementSkills = false;
    private Rigidbody2D rb;
    private PlayerControl playerControl;

    public delegate void MovementSkillsChanged(bool isUsing);
    public event MovementSkillsChanged OnMovementSkillsChanged;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControl = GetComponent<PlayerControl>();
    }

    void Update()
    {
        HandleDash();
    }

    void HandleDash()
    {
        // Check if the dash key is pressed and the player is allowed to dash
        if (Input.GetKeyDown(playerControl.dashKey) && canDash && !using_MovementSkills)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        using_MovementSkills = true;
        canDash = false;

         if (OnMovementSkillsChanged != null)
         {
            OnMovementSkillsChanged.Invoke(true); // Notify subscribers
         }


        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; // Disable gravity during dash
        rb.velocity = new Vector2(GetDashDirection() * dashSpeed, 0f); // Horizontal dash

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero; // Stop the dash
        using_MovementSkills = false;

        if (OnMovementSkillsChanged != null)
         {
            OnMovementSkillsChanged.Invoke(false); // Notify subscribers
         }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public float GetDashDirection()
    {
        // Devuelve 1 para la derecha o -1 para la izquierda según la dirección en la que el jugador está mirando
        return transform.localScale.x > 0 ? 1f : -1f;
    }
}
