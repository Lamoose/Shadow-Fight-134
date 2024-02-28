using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimCon : MonoBehaviour
{
    [SerializeField] private Player1 p;
    [SerializeField] private HitBox Hb;
    [SerializeField] private George G;
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

    void KomboajNapad(Attack napad)
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.G))
        {
            if (!p.isGrounded)
            {           
                for (int i = 0; i < napad.ComboNapadi.Count; i++)
                {
                    if (Input.GetKeyDown(napad.ComboNapadi[i].p1obicanInput) && napad.ComboNapadi[i].p1dirInput == "nista" && napad.ComboNapadi[i].pozicija == "air")
                    {
                        napadi.Push(napad.ComboNapadi[i]);
                        break;
                    }
                }

            }
            else
            {
                if (p.isTryingToCrouch)
                {
                    for (int i = 0; i < napad.ComboNapadi.Count; i++)
                    {
                        if (Input.GetKeyDown(napad.ComboNapadi[i].p1obicanInput) && napad.ComboNapadi[i].p1dirInput == "dole")
                        {
                            napadi.Push(napad.ComboNapadi[i]);
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < napad.ComboNapadi.Count; i++)
                    {
                        if (Input.GetKeyDown(napad.ComboNapadi[i].p1obicanInput) && napad.ComboNapadi[i].p1dirInput == "nista")
                        {
                            napadi.Push(napad.ComboNapadi[i]);
                            break;
                        }
                    }
                }
            }

            
        }

    }


    void Update()
    {
        #region Napadi na stack
        if (canOnStack)
        {
            if (G.trenutni.ime == "Air2Punch")
            {
                Debug.Log("uspeo sam");
                KomboajNapad(G.Air2Punch);
            }

            else if (G.trenutni.ime == "spin2win")
            {
                Debug.Log("uspeo sam");
                KomboajNapad(G.AirSpin2Win);
            }



            else if (G.trenutni.ime == "sweep")    // ovde dodaj && za svaki napad koji ce moci da se chainuje u bilo koji napad
            {
                KomboajNapad(G.sweep);

            }


            else if (G.trenutni.ime == "Punch")
            {
                KomboajNapad(G.punch);

            }

            else if (G.trenutni.ime == "kick")
            {
                KomboajNapad(G.kick);

            }

            else if (G.trenutni.ime == "triplekick")
            {
                KomboajNapad(G.triplekick);

            }

            else if (p.isDashing)
            {
                if (Input.GetKeyDown(KeyCode.F) && !p.isGrounded && !p.isTryingToCrouch)
                {
                    udarioUDashu = true;
                    napadi.Push(G.Air2Punch);
                }

                if (Input.GetKeyDown(KeyCode.G) && !p.isGrounded && !p.isTryingToCrouch)
                {
                    udarioUDashu = true;
                    napadi.Push(G.AirSpin2Win);
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    udarioUDashu = true;
                    napadi.Push(G.punch);
                }

                if (Input.GetKeyDown(KeyCode.G) && !p.isTryingToCrouch)
                {
                    udarioUDashu = true;
                    napadi.Push(G.kick);
                }

                if (Input.GetKeyDown(KeyCode.G) && p.isTryingToCrouch)
                {
                    udarioUDashu = true;
                    //p.isDashing = false;
                    napadi.Push(G.sweep);
                }

                if (Input.GetKeyDown(KeyCode.F) && p.isTryingToCrouch)
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
            if (Input.GetKeyDown(KeyCode.F) && !p.isGrounded && !p.isTryingToCrouch)
            {
                Air2Punch();
            }

            if (Input.GetKeyDown(KeyCode.G) && !p.isGrounded && !p.isTryingToCrouch)
            {
                AirSpin2Win();
            }


            if (Input.GetKeyDown(KeyCode.F) && p.isGrounded && !p.isTryingToCrouch)
            {
                punch();
            }

            if (Input.GetKeyDown(KeyCode.G) && !p.isTryingToCrouch && p.isGrounded)
            {
                kick();
            }

            if (Input.GetKeyDown(KeyCode.G) && p.isTryingToCrouch && p.isGrounded)
            {
                sweep();
            }
            if (Input.GetKeyDown(KeyCode.F) && p.isTryingToCrouch && p.isGrounded)
            {
                uppercut();
            }
        }
        #endregion

        #region Napadi na chain
        if (canChain)
        {
            if (napadi.Contains(G.Air2Punch))
            {
                Air2Punch();
                canChain = false;
            }

            if (napadi.Contains(G.AirSpin2Win))
            {
                AirSpin2Win();
                canChain = false;
            }


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

    private void Air2Punch()
    {
        G.trenutni.kopiraj(G.Air2Punch);
        anim.Play("Air2Punch");
        napadi.Clear();
    }

    private void AirSpin2Win()
    {
        G.trenutni.kopiraj(G.AirSpin2Win);
        anim.Play("AirSpin2Win");
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

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-hit-low")) anim.Play("George-hit-low 0");
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
            rb.velocity = new Vector2(0f, 0f);
            rb.gravityScale = 0f;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("George-Udaren-u-Vazduhu"))
            {
                anim.Play("George-Udaren-u-Vazduhu 0");
            }
            else anim.Play("George-Udaren-u-Vazduhu");
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
        GameObject p2;
        p2 = GameObject.Find("/Player2");
        if (gameObject.transform.position.x > p2.transform.position.x) rb.AddForce(dir);
        else
        {
            dir = new Vector2(-dir.x, dir.y);
            rb.AddForce(dir);
        }
        if (dir.y > 0f)
        {
            anim.Play("George-Launch");
            p.isGrounded = false;
        }
    }

    public void recovery ()
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
        if (napadi.Contains(G.Air2Punch))
        {
            Air2Punch();
            canChain = false;
        }
        if (napadi.Contains(G.AirSpin2Win))
        {
            AirSpin2Win();
            canChain = false;
        }
        if (napadi.Contains(G.uppercut))
        {
            uppercut();
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

        GameObject p2;
        p2 = GameObject.Find("/Player2");
        if (gameObject.transform.position.x > p2.transform.position.x)
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
        //MozeDaChainuje();
        MozeDaNapadne();
        MozeNaStack();
        nijeUNapadu();
        MozeDaHoda();
        Hb.ResetHit();
    }
    #endregion

    #endregion




}
