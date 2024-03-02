using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maki : MonoBehaviour
{
    public Attack trenutni = new Attack();


    public Attack stab = new Attack("stab", "mid", 3, 250f, 0f, "ground", "kamera", 7);
    public Attack stabdole = new Attack("stabdole", "low", 3, 250f, 0f, "ground", "kamera", 7);


    private void Start()
    {
        stab.p1obicanInput = KeyCode.F;
        stab.p1dirInput = "nista";

        stabdole.p1obicanInput = KeyCode.F;
        stabdole.p1dirInput = "dole";



        stab.ComboNapadi.Add(stabdole);

    }

}
