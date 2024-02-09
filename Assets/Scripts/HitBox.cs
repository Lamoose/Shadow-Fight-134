using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]private PlayerManager pm;
    [SerializeField] private bool canHit = true;
    [SerializeField]private float hitCoolDown;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "high" || collision.tag == "low" || collision.tag == "mid")
        {
            if (canHit)
            {
                int Player = collision.gameObject.layer;
                Debug.Log(Player);
                pm.Hit(Player);
                canHit = false;
            }
        }
    }

    public void ResetHit()
    {
        canHit = true;
    }
}
