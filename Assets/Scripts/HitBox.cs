using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private George george;
    [SerializeField] private PlayerManager pm;
    [SerializeField] private bool canHit = true;
    [SerializeField] private float hitCoolDown;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hurtbox")
        {
            if (canHit)
            {
                int Player = collision.gameObject.layer;
                pm.Hit(Player, george.trenutni.dmg,george.trenutni.pos,george.trenutni.dir,george.trenutni.stranaUdarca, george.trenutni.blockRecovery,george.trenutni.pozicija);
                canHit = false;
            }
        }
    }

    public void ResetHit()
    {
        canHit = true;
    }
}
