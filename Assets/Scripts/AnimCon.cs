using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimCon : MonoBehaviour
{
    [SerializeField]private Player1 p;
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
        
        if (Input.GetKeyDown(KeyCode.G) && !p.isDashing && p.isGrounded && canLAtk)
        {
            anim.SetTrigger("Kick");
            canMove= false;
            canLAtk = false;
        }
        if (Input.GetKeyDown(KeyCode.F) && !p.isDashing && p.isGrounded && canLAtk)
        {
             anim.SetTrigger("Punch");
             canMove = false;
             canLAtk = false;
        }
        if (isMoving && p.isGrounded && canMove)
        {
            anim.SetBool("Move", true);
        }
        else anim.SetBool("Move", false);

        if (p.isDashing) anim.SetBool("Dash", true);
        else anim.SetBool("Dash", false);
    }


    private void ResetMove()
    {
        canMove = true;
    }

    private void Move()
    {
        Vector2 direction = new Vector2(0, -1f);
        gameObject.transform.Translate(direction);
    }

    private void ResetLAtk() 
    {
        canLAtk = true;
    }

}
