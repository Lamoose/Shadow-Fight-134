using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimCon2 : MonoBehaviour
{
    [SerializeField]private Player2 p2;
    Rigidbody2D rb;
    Animator anim;
    public bool canMove = true;
    public bool canLAtk = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.O) && !p2.isDashing && p2.isGrounded && canLAtk)
            {
                anim.SetTrigger("Kick");
                canMove= false;
                canLAtk = false;
            }
            if (Input.GetKeyDown(KeyCode.I) && !p2.isDashing && p2.isGrounded && canLAtk)
            {
                anim.SetTrigger("Punch");
                canMove = false;
                canLAtk = false;
            }
        
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
