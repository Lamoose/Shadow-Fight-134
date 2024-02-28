using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class George : MonoBehaviour
{

    [SerializeField]Animator anim;

    [SerializeField] private AnimCon AnimCon;
    public Attack trenutni = new Attack();
    public Attack cuvaj = new Attack();

    #region punch
    public Attack punch = new Attack("Punch", "mid", 3, 250f, 0f, "ground"); 
    public Attack uppercut = new Attack("uppercut", "mid", 5, 250f, 1000f, "ground");
    public Attack Air2Punch = new Attack("Air2Punch", "mid", 5, 0f, 0f, "air");
    #endregion

    #region kick
    public Attack sweep = new Attack("sweep", "low", 5, 250f, 0f, "ground");
    public Attack kick = new Attack("kick", "high", 3, 250f, 0f, "ground");
    public Attack triplekick = new Attack("triplekick", "mid", 2, 250f, 0f, "ground");
    public Attack AirSpin2Win = new Attack("spin2win", "mid", 2, 0f, -250f, "air");
    #endregion


    private void Start()
    {
        punch.p1obicanInput = KeyCode.F;
        punch.p1dirInput = "nista";

        kick.p1obicanInput = KeyCode.G;
        kick.p1dirInput = "nista";

        sweep.p1obicanInput = KeyCode.G;
        sweep.p1dirInput = "dole";

        uppercut.p1obicanInput = KeyCode.F;
        uppercut.p1dirInput = "dole";

        triplekick.p1obicanInput = KeyCode.G;
        triplekick.p1dirInput = "nista";

        Air2Punch.p1obicanInput = KeyCode.F;
        Air2Punch.p1dirInput = "nista";

        AirSpin2Win.p1obicanInput = KeyCode.G;
        AirSpin2Win.p1dirInput = "nista";


        punch.p2obicanInput = KeyCode.P;
        punch.p2dirInput = "nista";

        kick.p2obicanInput = KeyCode.O;
        kick.p2dirInput = "nista";

        sweep.p2obicanInput = KeyCode.O;
        sweep.p2dirInput = "dole";

        uppercut.p2obicanInput = KeyCode.P;
        uppercut.p2dirInput = "dole";

        triplekick.p2obicanInput = KeyCode.O;
        triplekick.p2dirInput = "nista";

        Air2Punch.p2obicanInput = KeyCode.I;
        Air2Punch.p2dirInput = "nista";

        AirSpin2Win.p2obicanInput = KeyCode.O;
        AirSpin2Win.p2dirInput = "nista";




        punch.ComboNapadi.Add(uppercut);
        punch.ComboNapadi.Add(kick);
        punch.ComboNapadi.Add(sweep);

        kick.ComboNapadi.Add(sweep);
        kick.ComboNapadi.Add(triplekick);

        triplekick.ComboNapadi.Add(kick);
        triplekick.ComboNapadi.Add(sweep);

        sweep.ComboNapadi.Add(kick);
        sweep.ComboNapadi.Add(punch);

        AirSpin2Win.ComboNapadi.Add(Air2Punch);

        Air2Punch.ComboNapadi.Add(AirSpin2Win);
    }
}
