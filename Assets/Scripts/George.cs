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
    public Attack punch = new Attack("Punch", "mid", 3, 250f, 0f, "ground", "kamera", 7); 
    public Attack uppercut = new Attack("uppercut", "mid", 5, 250f, 700f, "ground", "kamera", 10);
    public Attack Air2Punch = new Attack("Air2Punch", "mid", 5, 1000f, 0f, "air", "oba", 7);
    #endregion

    #region kick
    public Attack sweep = new Attack("sweep", "low", 5, 250f, 0f, "ground", "screen", 12);
    public Attack kick = new Attack("kick", "high", 3, 250f, 0f, "ground", "kamera", 7);
    public Attack triplekick = new Attack("triplekick", "mid", 2, 250f, 0f, "ground", "screen", 13);
    public Attack AirSpin2Win = new Attack("spin2win", "high", 2, 0f, 10f, "air", "oba", 9 , 0f, -1000f);
    public Attack PunchKickAir = new Attack("PunchKickAir", "high", 2, 0f, 0f, "air", "oba", 9, 500f, 500f);
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

        PunchKickAir.p1obicanInput = KeyCode.G;
        PunchKickAir.p1dirInput = "napred";








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
        Air2Punch.ComboNapadi.Add(PunchKickAir);
    }
}
