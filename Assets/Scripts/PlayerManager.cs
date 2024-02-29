using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public Slider p1Slider;
    public Slider p2Slider;
    [SerializeField] private Player1 p1;
    [SerializeField] private Player2 p2;
    [SerializeField] private FightGui Gui;
    [SerializeField] private GameObject Canvas;

    private int MaxHP=100;
    [SerializeField] private int P1Hp;
    [SerializeField] private int P2Hp;
    [SerializeField] private AnimCon anim;
    [SerializeField] private AnimCon2 anim2;
    [SerializeField] private GatoAnimCon Gatoanim;
    void Start()
    {
        P1Hp = MaxHP;
        P2Hp = MaxHP;
        p1Slider.maxValue = MaxHP;
        p1Slider.value = MaxHP;
        p2Slider.maxValue = MaxHP;
        p2Slider.value = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (P1Hp <= 0) StartCoroutine(StartNextRound());
        if (P2Hp <= 0) StartCoroutine(StartNextRound());
        if(MatchManager.currentTimer <= 0) StartCoroutine(StartNextRound());
    }

    public void Hit(int player, int dmg, string pos,Vector2 dir, string stranaUdarca, int blockRecovery)
    {
        if (player == 8)
        {
            if (anim.overheadBlock && (pos == "high" || pos == "mid"))
            {
                //Debug.Log(stranaUdarca);
                anim.Hit(pos, stranaUdarca, blockRecovery);
            }
            else if (anim.LowBlock && (pos == "low" || pos == "mid"))
            {
                anim.Hit(pos, stranaUdarca, blockRecovery);
            }
            else
            {
                P1Hp -= dmg;
                p1Slider.value = P1Hp;
                anim.Hit(pos, stranaUdarca, blockRecovery);
                anim.launch(dir);
            }
            
        }


        if (player == 9)
        {
            P2Hp -= dmg;
            p2Slider.value = P2Hp;
            anim2.Hit(pos);
            anim2.launch(dir);
            if (anim.dodajKnockback)
            {
                Debug.Log(anim.G.trenutni.ime);
                anim2.launch(anim.G.trenutni.posebanKnockback);
            }
        }

    }

    IEnumerator StartNextRound()
    {
        Canvas.SetActive(false);
        Gui.enabled = false;
        Gatoanim.roundEnd();

        if(P1Hp<=0)
        {
            anim.anim.Play("roundEnd");
        }

        else
        {
            anim2.anim.Play("roundEnd");
        }

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("SampleScene");
        
    }
}
