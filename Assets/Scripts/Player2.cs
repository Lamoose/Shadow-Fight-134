using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player2 : MonoBehaviour
{
    #region jump
    public bool isGrounded = true;
    [SerializeField] private float JumpForce = 12f;
    [SerializeField] private float JumpTime = 0.2f;
    [SerializeField] private float TimeSinceJump;
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool Jumped = false;
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
    [SerializeField] private GameObject player2;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed;
    [SerializeField] public bool isCrouching;
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7); //ignorise koliziju od drugog igraca da bi mogao da prolazis kroz njega
    }

    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("HorizontalS");
        float verticalInput = Input.GetAxisRaw("VerticalS");
        TimeSinceDash += Time.deltaTime; //gleda koklo dugo se nije dashovao
        TimeSinceJump += Time.deltaTime;
        #region flip
        if (player2.transform.position.x < gameObject.transform.position.x)
        {
            flip();
        }

        if (player2.transform.position.x > gameObject.transform.position.x)
        {
            flip(0, 180f);
        }
        #endregion

        if (horizontalInput == 0 && isGrounded && !isDashing && !anim.inAttack)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            anim.isMoving = false;
        }

        if (anim.disableMove && anim.inAttack)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            anim.isMoving = false;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCrouching = false;
        }

        if (horizontalInput == 0 && !isGrounded || anim.disableMove && !isGrounded) // malo dodao da bih jumpovi bili malo precizniji
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        #region kretanje
        if (!anim.disableMove)
        {
            if (horizontalInput != 0 && !isDashing)
            {


                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

                anim.isMoving = true;

            }

            if (Input.GetKeyDown(KeyCode.K) && !isDashing && !dashed && TimeSinceDash >= dashCooldown && horizontalInput != 0f)
            {
                StartCoroutine(Dash(horizontalInput));

                TimeSinceDash = 0f; //resetuje dash poslednje dash vreme
            }

            if (Input.GetKeyDown(KeyCode.J) && !isDashing)
            {
                if (canDoubleJump) StartCoroutine(Jump());
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
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


    IEnumerator Jump()
    {
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
        }
        dashed = false;
    }

    void flip(float p1Rotate = 180f, float p2Rotate = 0)
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, p1Rotate, 0f);
        player2.transform.rotation = Quaternion.Euler(0f, p2Rotate, 0f);
    }

}
