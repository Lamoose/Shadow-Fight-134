using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player1 : MonoBehaviour
{
    #region jump
    public bool isGrounded = true;
    [SerializeField] private float JumpForce = 12f;
    #endregion

    #region dash
    public bool isDashing = false;
    private bool dashed=false;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 0.3f;
    [SerializeField] private float TimeSinceDash;
    #endregion

    [SerializeField]private AnimCon anim;
    public GameObject player2;
    public Rigidbody2D rb;
    public int speed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7); //ignorise koliziju od drugog igraca da bi mogao da prolazis kroz njega
    }

    void Update()
    {
        
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            TimeSinceDash += Time.deltaTime; //gleda koklo dugo se nije dashovao

            if (player2.transform.position.x < gameObject.transform.position.x)
            {
                flip();
            }

            if (player2.transform.position.x > gameObject.transform.position.x)
            {
                flip(0, 180f);
            }

            if (horizontalInput == 0 && isGrounded && !isDashing || !anim.canMove)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                anim.isMoving = false;
            }

            if (horizontalInput == 0 && !isGrounded || !anim.canMove && !isGrounded) // malo dodao da bih jumpovi bili malo precizniji
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            if (anim.canMove)   
            {
                 if (horizontalInput != 0 && !isDashing)
                 {


                     rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
                     
                     //rb.velocity = movement;
                     anim.isMoving = true;

                 }

                 if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !dashed && TimeSinceDash >= dashCooldown && horizontalInput != 0f)
                 {
                     StartCoroutine(Dash(horizontalInput));

                     TimeSinceDash = 0f; //resetuje dash poslednje dash vreme
                 }

                 if (Input.GetKeyDown(KeyCode.Space) && !isDashing && isGrounded)
                 {
                    Jump();
                 }

            }
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
        if(!isGrounded)dashed = true;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
    }


    void Jump()
    {  
        isGrounded = false;

            float horizontal = Input.GetAxisRaw("Horizontal");

            if (!isDashing)
            {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

            }
            

    }
    //gleda dal je na zemlji

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        dashed = false;
    }

    void flip(float p1Rotate = 180f, float p2Rotate = 0)
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, p1Rotate, 0f);
        player2.transform.rotation = Quaternion.Euler(0f, p2Rotate, 0f);

    }

}
