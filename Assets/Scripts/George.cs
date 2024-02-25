using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class George : MonoBehaviour
{

    [SerializeField]Animator anim;

    [SerializeField] private AnimCon AnimCon;
    public Attack trenutni = new Attack();
    public Attack cuvaj = new Attack();
    
    public Attack punch = new Attack("punch", "mid", 5, 1f, 0f); 
    
    public Attack sweep = new Attack("sweep", "low", 15, 1f, 0f);
    public Attack kick = new Attack("kick", "high", 20, 1f, 0f);
    public Attack triplekick = new Attack("triplekick", "mid", 20, 1f, 0f);

}
