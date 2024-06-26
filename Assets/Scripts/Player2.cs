using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    #region jump
    public bool isGrounded = true;
    [SerializeField] private float JumpForce = 12f;
    [SerializeField] private float JumpTime = 0.2f;
    [SerializeField] private float TimeSinceJump;
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool Jumped = false;
    [SerializeField] public bool jumpCancel;
    #endregion

    #region dash
    public bool isDashing = false;
    private bool dashed = false;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 0.3f;
    [SerializeField] private float TimeSinceDash;
    #endregion

    #region general
    [SerializeField] private AnimCon2 anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed;
    [SerializeField] public bool isCrouching;
    [SerializeField] public bool isTryingToCrouch;
    [SerializeField] public float horizontalInput;
    [SerializeField] private Controller controller;
    #endregion


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7); //ignorise koliziju od drugog igraca da bi mogao da prolazis kroz njega
    }

    private void Awake()
    {
        controller = new Controller();
    }
    void Update()
    {
        #region crouch
        if (Input.GetKeyDown(KeyCode.DownArrow) || anim.CheckGamepad() && Gamepad.current.dpad.down.isPressed)
        {
            isTryingToCrouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || anim.CheckGamepad() && Gamepad.current.dpad.down.wasReleasedThisFrame)
        {
            isTryingToCrouch = false;
        }
        #endregion

        #region input i vreme
        if (anim.CheckGamepad() && Gamepad.current.dpad.right.isPressed)
        {
            horizontalInput = 1f;
        }
        else if (anim.CheckGamepad() && Gamepad.current.dpad.left.isPressed)
        {
            horizontalInput = -1f;
        }
        else
        {
            horizontalInput = 0f;
            horizontalInput = Input.GetAxisRaw("HorizontalS");
            float verticalInput = Input.GetAxisRaw("VerticalS");
        }
        TimeSinceDash += Time.deltaTime; //gleda koklo dugo se nije dashovao
        TimeSinceJump += Time.deltaTime;
        #endregion
       
        #region kretanje kad je horizontal 0 
        if (horizontalInput == 0 && isGrounded && !isDashing || anim.disableMove)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            anim.isMoving = false;
        }

        if (!anim.disableMove && anim.inAttack)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            anim.isMoving = false;
        }
        #endregion

        #region jump i crouch
        if (Input.GetKeyUp(KeyCode.DownArrow) || anim.CheckGamepad() && Gamepad.current.dpad.down.wasReleasedThisFrame)
        {
            isCrouching = false;
        }

        if (horizontalInput == 0 && !isGrounded || anim.CheckGamepad() && anim.disableMove && !isGrounded) // malo dodao da bih jumpovi bili malo precizniji
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        #endregion

        #region kretanje
        if (!anim.disableMove && !anim.inAttack)
        {
            if (horizontalInput != 0 && !isDashing)
            {


                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

                anim.isMoving = true;

            }

            if (Input.GetKeyDown(KeyCode.K) && !isDashing && !dashed && TimeSinceDash >= dashCooldown && horizontalInput != 0f || anim.CheckGamepad() && Gamepad.current.rightTrigger.wasPressedThisFrame && !isDashing && !dashed && TimeSinceDash >= dashCooldown && horizontalInput != 0f)
            {
                StartCoroutine(Dash(horizontalInput));

                TimeSinceDash = 0f; //resetuje dash poslednje dash vreme
            }

            if (Input.GetKeyDown(KeyCode.L) && !isDashing || anim.CheckGamepad() && Gamepad.current.dpad.up.wasPressedThisFrame && !isDashing)
            {
                if (canDoubleJump) StartCoroutine(Jump());
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || anim.CheckGamepad() && Gamepad.current.dpad.down.isPressed)
            {
                isCrouching = true;
            }
        }
        #endregion
    }

    //dash funkcija

    IEnumerator Dash(float horizontal)
    {
        isDashing = true;
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.gravityScale = 0f;
            Vector2 dashDirection = new Vector2(horizontal, rb.velocity.y).normalized;
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime, Space.World); //zbog Space.World se lepo krecu nakon sto su rotirani


            yield return null;

        }
        rb.gravityScale = 3;
        if (!isGrounded) dashed = true;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
    }


    public IEnumerator Jump()
    {
        anim.anim.Play("George-Jump");
        isGrounded = false;
        canDoubleJump = false;
        float startTime = Time.time;
        while (Time.time < startTime + JumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            Vector2 JumpDirection = new Vector2(rb.velocity.x, 5).normalized;
            transform.Translate(JumpDirection * JumpForce * Time.deltaTime, Space.World); //zbog Space.World se lepo krecu nakon sto su rotirani


            yield return null;

        }
        if (!Jumped) canDoubleJump = true;
        else if (Jumped) canDoubleJump = false;
        Jumped = true;
        yield return new WaitForSeconds(TimeSinceJump);

    }
    //gleda dal je na zemlji

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Jumped = false;
            canDoubleJump = true;
            if (anim.disableMove && !anim.inAttack)
            {
                anim.recovery();
            }
        }
        dashed = false;
    }
}
