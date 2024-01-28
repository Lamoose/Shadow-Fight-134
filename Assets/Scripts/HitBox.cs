using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]private PlayerManager pm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            string Player = collision.tag;
            Debug.Log(Player);
            pm.Hit(Player);
        }
    }
}
