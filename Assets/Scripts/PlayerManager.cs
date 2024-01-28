using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private int MaxHP=100;
    [SerializeField]private int P1Hp;
    [SerializeField]private int P2Hp;

    void Start()
    {
        P1Hp = MaxHP;
        P2Hp = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (P1Hp >= 0) StartNextRound();
        if (P2Hp >= 0) StartNextRound();
    }

    public void Hit(string player)
    {
        Debug.Log(player);
        if (player == "Player1") P1Hp -= 10;
        if (player == "Player2") P2Hp -= 10;
    }

    void StartNextRound()
    {

    }
}
