using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCancel : MonoBehaviour
{
    public Player1 p1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player1"))
        {
            Debug.Log("enter");
            p1.jumpCancel = true;
            p1.canDoubleJump = true;
        }    

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        if (collision.CompareTag("Player1"))
        {
            p1.jumpCancel = false;
        }
    }


}
