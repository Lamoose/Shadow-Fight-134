using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GatoAnimCon : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim= GetComponent<Animator>();
    }

    public void roundEnd()
    {
        anim.Play("roundEnd");
    }
}
