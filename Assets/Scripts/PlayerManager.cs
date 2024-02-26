using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public Slider p1Slider;
    public Slider p2Slider;
    [SerializeField]private Player1 p1;
    [SerializeField]private Player2 p2;

    private int MaxHP=100;
    [SerializeField]private int P1Hp;
    [SerializeField]private int P2Hp;
    [SerializeField] private AnimCon anim;
    [SerializeField] private AnimCon2 anim2;
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
        if (P1Hp <= 0) StartNextRound();
        if (P2Hp <= 0) StartNextRound();
        if(MatchManager.currentTimer <= 0) StartNextRound();
    }

    public void Hit(int player, int dmg, string pos,Vector2 dir)
    {
 
        Debug.Log(player);
        if (player == 8)
        {

            P1Hp -= dmg;
            p1Slider.value = P1Hp;
            anim.Hit(pos);
            if (p1.isGrounded) anim.launch(dir);
        }


        if (player == 9)
        {
            P2Hp -= dmg;
            p2Slider.value = P2Hp;
            anim2.Hit(pos);
            if(p2.isGrounded)anim2.launch(dir);
        }

    }

    void StartNextRound()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
