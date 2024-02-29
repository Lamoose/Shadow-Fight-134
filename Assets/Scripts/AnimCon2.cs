using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimCon2 : MonoBehaviour
{
    [SerializeField] private Player2 p;
    [SerializeField] private HitBox Hb;
    [SerializeField] public George G;
    [SerializeField] private AnimCon anim1;
    Rigidbody2D rb;
    public Animator anim;

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


        #region Napadi na stack
        if (canOnStack)
        {
            if ((G.trenutni.ime == "sweep"))    // ovde dodaj && za svaki napad koji ce moci da se chainuje u bilo koji napad
            {
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.O))
                {
                    if (p.isTryingToCrouch)
                    {
                        for (int i = 0; i < G.sweep.ComboNapadi.Count; i++)
                        {
                            if (Input.GetKeyDown(G.sweep.ComboNapadi[i].p2obicanInput) && G.sweep.ComboNapadi[i].p2dirInput == "dole")
                            {
                                napadi.Push(G.sweep.ComboNapadi[i]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < G.sweep.ComboNapadi.Count; i++)
                        {
                            if (Input.GetKeyDown(G.sweep.ComboNapadi[i].p2obicanInput) && G.sweep.ComboNapadi[i].p2dirInput == "nista")
                            {
                                napadi.Push(G.sweep.ComboNapadi[i]);
                                break;
                            }
                        }
                    }
                }

            }


            else if (G.trenutni.ime == "Punch")
            {
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.O))
                {
                    if (p.isTryingToCrouch)
                    {
                        for (int i = 0; i < G.punch.ComboNapadi.Count; i++)
                        {
                            if (Input.GetKeyDown(G.punch.ComboNapadi[i].p2obicanInput) && G.punch.ComboNapadi[i].p2dirInput == "dole")
                            {
                                napadi.Push(G.punch.ComboNapadi[i]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < G.punch.ComboNapadi.Count; i++)
                        {
                            if (Input.GetKeyDown(G.punch.ComboNapadi[i].p2obicanInput) && G.punch.ComboNapadi[i].p2dirInput == "nista")
                            {
                                napadi.Push(G.punch.ComboNapadi[i]);
                                break;
                            }
                        }
                    }
                }

            }

            else if (G.trenutni.ime == "kick")
            {
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.O))
                {
                    if (p.isTryingToCrouch)
                    {
                        for (int i = 0; i < G.kick.ComboNapadi.Count; i++)
                        {
                            if (Input.GetKeyDown(G.kick.ComboNapadi[i].p2obicanInput) && G.kick.ComboNapadi[i].p2dirInput == "dole")
                            {
                                napadi.Push(G.kick.ComboNapadi[i]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < G.kick.ComboNapadi.Count; i++)
                        {
                            if (Input.GetKeyDown(G.kick.ComboNapadi[i].p2obicanInput) && G.kick.ComboNapadi[i].p2dirInput == "nista")
                            {
                                napadi.Push(G.kick.ComboNapadi[i]);
                                break;
                            }
                        }
                    }
                }

            }

            else if (G.trenutni.ime == "triplekick")
            {
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.O))
                {
                    if (p.isTryingToCrouch)
                    {
                        for (int i = 0; i < G.triplekick.ComboNapadi.Count; i++)
                        {
                            if (Input.GetKeyDown(G.triplekick.ComboNapadi[i].p2obicanInput) && G.triplekick.ComboNapadi[i].p2dirInput == "dole")
                            {
                                napadi.Push(G.triplekick.ComboNapadi[i]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < G.triplekick.ComboNapadi.Count; i++)
                        {
                            if (Input.GetKeyDown(G.triplekick.ComboNapadi[i].p2obicanInput) && G.triplekick.ComboNapadi[i].p2dirInput == "nista")
                            {
                                napadi.Push(G.triplekick.ComboNapadi[i]);
                                break;
                            }
                        }
                    }
                }

            }

            else if (p.isDashing)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    udarioUDashu = true;
                    napadi.Push(G.punch);
                }

                if (Input.GetKeyDown(KeyCode.O) && !p.isCrouching)
                {
                    udarioUDashu = true;
                    napadi.Push(G.kick);
                }

                if (Input.GetKeyDown(KeyCode.P) && p.isCrouching)
                {
                    udarioUDashu = true;
                    //p.isDashing = false;
                    napadi.Push(G.sweep);
                }

                if (Input.GetKeyDown(KeyCode.O) && p.isCrouching)
                {
                    napadi.Push(G.uppercut);
                    uppercut();
                }
            }

        }
        #endregion

        #region Napadi iz idle
        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                napadi.Push(G.punch);
                punch();
            }

            if (Input.GetKeyDown(KeyCode.O) && !p.isTryingToCrouch)
            {
                napadi.Push(G.kick);
                kick();
            }

            if (Input.GetKeyDown(KeyCode.O) && p.isTryingToCrouch)
            {
                napadi.Push(G.sweep);
                sweep();
            }
            if (Input.GetKeyDown(KeyCode.P) && p.isTryingToCrouch)
            {
                napadi.Push(G.uppercut);
                uppercut();
            }
        }
        #endregion

        #region Napadi na chain
        if (canChain)
        {
            if (napadi.Contains(G.punch))
            {
                punch();
                canChain = false;
            }

            if (napadi.Contains(G.uppercut))
            {
                uppercut();
                canChain = false;
            }


            if (napadi.Contains(G.triplekick))
            {
                triple_kick();
                canChain = false;
            }


            if (napadi.Contains(G.kick))
            {
                kick();
                canChain = false;
            }

            if (napadi.Contains(G.sweep))
            {
                sweep();
                canChain = false;
            }


            if (udarioUDashu)
            {
                udarioUDashu = false;
                p.isDashing = false;
                ProveriCeoStack();
            }
        }
        #endregion

        #region kretanje

        if (!disableMove && !inAttack && !p.isCrouching && p.isGrounded && !p.isDashing)
        {
            if (isMoving)
            {
                anim.Play("walk1");
            }
            if (!isMoving)
            {
                anim.Play("idle");
            }
        }
        if (p.isCrouching && !disableMove && !inAttack && p.isGrounded && !p.isDashing)
        {
            anim.Play("Crouching");
        }
        if (p.isDashing && !inAttack)
        {
            anim.Play("George-Dash");
        }
        #endregion

    }

    #region funkcije

    #region napadi
    private void punch()
    {
        G.trenutni.kopiraj(G.punch);
        anim.Play("Punch");
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

    public void Hit(string pos)
    {
        if (p.isGrounded)
        {
            
            if (pos == "mid" && !p.isCrouching)
            {
                
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-hit-mid"))
                {
                    anim.Play("George-hit-mid 0");
                }
                else anim.Play("George-hit-mid");
            }

            else if (pos == "mid" && p.isCrouching)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-hit-mid")) anim.Play("George-hit-mid 0");
                else anim.Play("George-hit-mid");
            }

            else if (pos == "high" && !p.isCrouching)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-hit-mid")) anim.Play("George-hit-high 0");
                else anim.Play("George-hit-high");
            }
            else if (pos == "high" && p.isCrouching)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-hit-mid")) anim.Play("George-hit-mid 0");
                else anim.Play("George-hit-mid");
            }
            else if (pos == "low" && !p.isCrouching)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-hit-low"))
                {
                    anim.Play("George-hit-low 0");
                    Debug.Log(anim.GetCurrentAnimatorStateInfo(0));
                }
                else anim.Play("George-hit-low");
            }
            else if (pos == "low" && p.isCrouching)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-hit-mid")) anim.Play("George-hit-mid 0");
                else anim.Play("George-hit-mid");
            }
        }
        if (!p.isGrounded)
        {
            rb.gravityScale = 0f;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-Udaren-u-Vazduhu"))
            {
                anim.Play("George-Udaren-u-Vazduhu 0");
            }
            else anim.Play("George-Udaren-u-Vazduhu");
            rb.velocity = new Vector2(0f, 0f);
           
        }
    }

    public void ukljuciGravity()
    {
        rb.gravityScale = 3f;
    }

    private void islkuciGravity()
    {
        rb.gravityScale = 0f;
    }

    private void OnHit()
    {
        Hb.ResetHit();
    }

    public void launch(Vector2 dir)
    {
        if (anim1.dodajKnockback == true) Debug.Log(dir);
        GameObject p1;
        p1 = GameObject.Find("/Player1");
        if (gameObject.transform.position.x > p1.transform.position.x) rb.AddForce(dir);
        else
        {
            dir = new Vector2(-dir.x, dir.y);
            rb.AddForce(dir);
        }
        if (dir.y > 0f && p.isGrounded)
        {
            anim.Play("George-Launch");
            p.isGrounded = false;
        }
    }


    public void recovery()
    {
        anim.Play("George-Launch-Ustaje");
    }
    #endregion


    #region stack

    private void ProveriDaLiJeSvakiNapadKliknut()
    {
        if (Input.GetKeyDown(KeyCode.F) && !p.isCrouching)
        {
            napadi.Push(G.punch);
        }
        if (Input.GetKeyDown(KeyCode.G) && !p.isCrouching)
        {
            napadi.Push(G.kick);
        }
    }





    private void ProveriCeoStack()
    {
        if (napadi.Contains(G.punch))
        {
            punch();
            canChain = false;
        }
        if (napadi.Contains(G.kick))
        {
            kick();
            canChain = false;
        }
        if (napadi.Contains(G.sweep))
        {
            sweep();
            canChain = false;
        }

    }
    #endregion


    #region animacije-event
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
        isMoving = false;
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


        Vector2 dir = new Vector2(xf, yf);
        GameObject p1;
        p1 = GameObject.Find("/Player1");
        if (gameObject.transform.position.x > p1.transform.position.x)
        {
            dir = new Vector2(-dir.x, dir.y);
            rb.AddForce(dir);
        }
        else rb.AddForce(dir);
    }

    private void resetSpeed()
    {
        rb.velocity = Vector2.zero;
    }

    private void neMozeNista()
    {
        ClearTrenutni();
        NeMozeDaChainuje();
        NeMozeDaNapadne();
        NeMozeNaStack();
        uNapadu();
        NeMozeDaHoda();
    }

    private void mozeSve()
    {
        ClearTrenutni();
        MozeDaChainuje();
        MozeDaNapadne();
        MozeNaStack();
        nijeUNapadu();
        MozeDaHoda();
        Hb.ResetHit();

    }
    #endregion

    #endregion

    public void PrimiKnockback(Vector2 dir)
    {
        rb.AddForce(dir);
        //anim1.posebanKnockback = false;
    }
}
