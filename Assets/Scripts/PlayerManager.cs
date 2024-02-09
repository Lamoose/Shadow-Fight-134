using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public Slider p1Slider;
    public Slider p2Slider;
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

    public void Hit(int player)
    {
        

        Debug.Log(player);
        if (player == 6)
        {
            P1Hp -= 10;
            p1Slider.value = P1Hp;
            anim.Hit();
        }


        if (player == 7)
        {
            P2Hp -= 10;
            p2Slider.value = P2Hp;
        }

    }

    void StartNextRound()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
