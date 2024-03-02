using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimConMaki : MonoBehaviour
{
    [SerializeField] private PlayerMaki p;
    [SerializeField] private HitBox Hb;
    [SerializeField] public Maki M;
    public AnimCon2 anim2;
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
    public bool overheadBlock;
    public bool LowBlock;
    public int frameovi;
    public bool inBlockAnim;
    public bool dodajKnockback;



    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void KomboajNapad(Attack napad)
    {

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.G))
        {
            if (!p.isGrounded && horizontal() > 0 && !p.isTryingToCrouch)
            {
                for (int i = 0; i < napad.ComboNapadi.Count; i++)
                {
                    if (Input.GetKeyDown(napad.ComboNapadi[i].p1obicanInput) && napad.ComboNapadi[i].p1dirInput == "napred" && napad.ComboNapadi[i].pozicija == "air")
                    {
                        napadi.Push(napad.ComboNapadi[i]);
                        break;
                    }
                }
            }

            else if (!p.isGrounded && horizontal() == 0)
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
            
        }
        #endregion

        #region Napadi iz idle
        if (canAttack)
        {
            



        }
        #endregion

        #region Napadi na chain
        if (canChain)
        {
           
        }
        #endregion

        #region kretanje

        if (!disableMove && !inAttack && !p.isCrouching && p.isGrounded && !p.isDashing)
        {
            if (isMoving)
            {
                anim.Play("walk");
            }
            if (!isMoving)
            {
                anim.Play("idle");
            }
        }
        if (p.isCrouching && !disableMove && !inAttack && p.isGrounded && !p.isDashing)
        {
            //anim.Play("Crouching");
        }
        if (p.isDashing && !inAttack)
        {
            //anim.Play("George-Dash");
        }
        #endregion


        if (!p.isDashing && !inAttack && Input.GetButton("left ctrl") && (!p.isTryingToCrouch || !p.isCrouching))
        {
            overheadBlock = true;
        }
        else
        {
            overheadBlock = false;
        }

        if (!p.isDashing && !inAttack && Input.GetButton("left ctrl") && (p.isTryingToCrouch || p.isCrouching))
        {
            LowBlock = true;
        }
        else
        {
            LowBlock = false;
        }


        if (inBlockAnim)
        {
            if (Time.frameCount > frameovi)
            {
                disableMove = false;
                inBlockAnim = false;
            }
        }


    }

    #region funkcije

    #region napadi




    public void Hit(string pos, string stranaUdarca, int blockRecovery)
    {

        frameovi = Time.frameCount + blockRecovery;

        if (p.isGrounded)
        {
            


            
        }
        if (!p.isGrounded)
        {
            

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
        if (gameObject.transform.position.x > p2.transform.position.x)
        {
            if (!p.isGrounded) dir.y = dir.y / 2;
            rb.AddForce(dir);
        }
        else
        {
            if (!p.isGrounded) dir.y = dir.y / 2;
            dir = new Vector2(-dir.x, dir.y);
            rb.AddForce(dir);
        }
        if (dir.y > 0f && p.isGrounded)
        {
            //anim.Play("George-Launch");
            p.isGrounded = false;
        }
    }



    public void recovery()
    {
        //anim.Play("George-Launch-Ustaje");
    }


    #endregion


    #region stack






    private void ProveriCeoStack()
    {
        


    }
    #endregion


    #region animacije-event
    private void ClearTrenutni()
    {
        M.trenutni.kopiraj(new Attack());
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


    public void Knockback()
    {
        dodajKnockback = true;
    }
    public void SkloniKnockback()
    {
        dodajKnockback = false;
    }


    public float horizontal()
    {
        GameObject p2;
        p2 = GameObject.Find("/Player2");

        if (gameObject.transform.position.x > p2.transform.position.x)
        {
            return -p.horizontalInput;
        }
        else
        {
            return p.horizontalInput;
        }
    }



    private void neMozeNista()
    {
        //ClearTrenutni();
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
