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
    public Attack punch = new Attack("Punch", "mid", 5, 1f, 0f); 
    public Attack uppercut = new Attack("uppercut", "mid", 20, 1f, 1f);
    #endregion

    #region kick
    public Attack sweep = new Attack("sweep", "low", 15, 1f, 0f);
    public Attack kick = new Attack("kick", "high", 20, 1f, 0f);
    public Attack triplekick = new Attack("triplekick", "mid", 20, 1f, 0f);
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




        punch.ComboNapadi.Add(uppercut);
        punch.ComboNapadi.Add(kick);
        punch.ComboNapadi.Add(sweep);

        kick.ComboNapadi.Add(sweep);
        kick.ComboNapadi.Add(triplekick);

        triplekick.ComboNapadi.Add(kick);
        triplekick.ComboNapadi.Add(sweep);

        sweep.ComboNapadi.Add(kick);
        sweep.ComboNapadi.Add(punch);
    }
}
