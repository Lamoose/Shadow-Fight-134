using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class George : MonoBehaviour
{

    [SerializeField]Animator anim;

    [SerializeField] private AnimCon AnimCon;
    public Attack trenutni = new Attack();
    public Attack jab = new Attack("jab","mid", 5);
    public Attack kick = new Attack("kick", "high", 20);

}
