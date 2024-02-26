using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimCon2 : MonoBehaviour
{
    [SerializeField] private Player2 p;
    [SerializeField] private HitBox Hb;
    [SerializeField] private George G;
    Rigidbody2D rb;
    Animator anim;

    public bool udarioUDashu;
    public bool inAttack;
    public bool canAttack;
    public bool canOnStack;
    public bool disableMove;
    public bool isMoving;
    public bool canChain;
    Stack<Attack> napadi = new Stack<Attack>();



    void Start()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canOnStack)
        {
            if (G.trenutni.ime == "punch")
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    napadi.Push(G.punch);
                }
            }

            else if (G.trenutni.ime == "kick")
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    napadi.Push(G.triplekick);
                }
            }

            else if (G.trenutni.ime == "triplekick")
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    napadi.Push(G.kick);
                }
            }

            else if (p.isDashing)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    udarioUDashu = true;
                    //p.isDashing = false;
                    napadi.Push(G.punch);
                }

                if (Input.GetKeyDown(KeyCode.O) && !p.isCrouching)
                {
                    udarioUDashu = true;
                    //p.isDashing = false;
                    napadi.Push(G.kick);
                }

                if (Input.GetKeyDown(KeyCode.P) && p.isCrouching)
                {
                    udarioUDashu = true;
                    //p.isDashing = false;
                    napadi.Push(G.sweep);
                }
            }

        }


        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                napadi.Push(G.punch);
                punch();
            }

            if (Input.GetKeyDown(KeyCode.O) && !p.isCrouching)
            {
                napadi.Push(G.kick);
                kick();
            }

            if (Input.GetKeyDown(KeyCode.O) && p.isCrouching)
            {
                napadi.Push(G.sweep);
                sweep();
            }
            if (Input.GetKeyDown(KeyCode.P) && p.isCrouching)
            {
                napadi.Push(G.uppercut);
                uppercut();
            }
        }


        if (canChain)
        {
            if (G.trenutni.ime == "kick")
            {
                if (napadi.Contains(G.triplekick))
                {
                    triple_kick();
                    canChain = false;
                }
            }

            if (G.trenutni.ime == "triplekick")
            {
                if (napadi.Contains(G.kick))
                {
                    kick();
                    canChain = false;
                }
            }

            if (udarioUDashu)
            {
                udarioUDashu = false;
                p.isDashing = false;
                if (napadi.Contains(G.kick))
                {
                    kick();
                    canChain = false;
                }
                if (napadi.Contains(G.punch))
                {
                    punch();
                    canChain = false;
                }
                if (napadi.Contains(G.sweep))
                {
                    sweep();
                    canChain = false;
                }
            }
        }



        if (isMoving && !disableMove && !inAttack && !p.isCrouching && p.isGrounded && !p.isDashing)
        {
            anim.Play("walk1");
        }
        if (!isMoving && !disableMove && !inAttack && !p.isCrouching && p.isGrounded && !p.isDashing)
        {
            anim.Play("idle");
        }
        if (p.isCrouching && !disableMove && !inAttack && p.isGrounded && !p.isDashing)
        {
            anim.Play("Crouching");
        }
        if (p.isDashing && !inAttack)
        {
            anim.Play("George-Dash");
        }






    }


    private void punch()
    {
        G.trenutni.kopiraj(G.punch);
        anim.Play("punch");
        napadi.Clear();
    }

    private void kick()
    {
        G.trenutni.kopiraj(G.kick);
        anim.Play("kick");
        napadi.Clear();
    }

    private void uppercut()
    {
        G.trenutni.kopiraj(G.uppercut);
        anim.Play("George-Uppercut");
        napadi.Clear();
    }

    private void triple_kick()
    {
        G.trenutni.kopiraj(G.triplekick);
        anim.Play("george-tripe-kick");
        napadi.Clear();
    }

    private void sweep()
    {
        G.trenutni.kopiraj(G.sweep);
        anim.Play("George-Sweep");
        napadi.Clear();
    }








    private void ClearTrenutni()
    {
        G.trenutni.kopiraj(new Attack());
    }

    void MozeDaChainuje()
    {
        canChain = true;
    }
    void NeMozeDaChainuje()
    {
        canChain = false;
    }



    void MozeDaNapadne()
    {
        canAttack = true;
    }
    void NeMozeDaNapadne()
    {
        canAttack = false;
    }




    void MozeNaStack()
    {
        canOnStack = true;
    }
    void NeMozeNaStack()
    {
        canOnStack = false;
    }



    void uNapadu()
    {
        inAttack = true;
    }
    void nijeUNapadu()
    {
        inAttack = false;
    }




    void MozeDaHoda()
    {
        disableMove = false;
    }
    void NeMozeDaHoda()
    {
        disableMove = true;
    }

    private void Move(string s)
    {
        int t = 0;
        string x = "";
        string y = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ',')
            {
                t = i;
                break;
            }
            x = x + s[i];
        }
        for (int i = t + 1; i < s.Length; i++)
        {
            y = y + s[i];
        }



        float xf = float.Parse(x);
        float yf = float.Parse(y);


        Debug.Log(xf + " " + yf);

        Vector2 dir = new Vector2(xf, yf);
        rb.AddForce(dir);
    }

    private void resetSpeed()
    {
        rb.velocity = Vector2.zero;
    }
    public void Hit(string pos)
    {
        if (p.isGrounded)
        {
            if (pos == "high" && p.isCrouching) anim.Play("George-hit-mid");
            else anim.Play("George-hit-high");
        }
    }





}
