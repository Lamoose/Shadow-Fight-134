using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimCon : MonoBehaviour
{
    [SerializeField]private Player1 p;
    [SerializeField]private HitBox Hb;
    [SerializeField] private George G;
    Rigidbody2D rb;
    Animator anim;
    public bool canMove = true;
    public bool canLAtk = true;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                G.trenutni.kopiraj(G.jab);
                anim.SetTrigger("Punch");
                Debug.Log(G.trenutni.pos);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                G.trenutni.kopiraj(G.kick);
                anim.SetTrigger("Kick");
                Debug.Log(G.trenutni.pos);
            }

            if (isMoving && p.isGrounded)
            {
                anim.SetBool("Move", true);
            }
            else anim.SetBool("Move", false);

            if (p.isDashing) anim.SetBool("Dash", true);
            else anim.SetBool("Dash", false);

        }
            if (p.isCrouching) anim.SetBool("Crouching", true);
            else anim.SetBool("Crouching", false);
    }


    private void ResetMove()
    {
        canMove = true;
    }

    private void DisableMove()
    {
        canMove = false;
    }

    private void Move()
    {
        Vector2 direction = new Vector2(0, -1f);
        gameObject.transform.Translate(direction);
    }

    private void ResetLAtk() 
    {
        canLAtk = true;
        Hb.ResetHit();
    }
    public void Hit(string pos)
    {
        if (p.isGrounded)
        {
            if (pos == "high" && p.isCrouching) anim.SetTrigger("mid");
            else anim.SetTrigger(pos);
        }
    }
}
