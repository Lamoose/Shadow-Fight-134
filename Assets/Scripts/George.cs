using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class George : MonoBehaviour
{

    [SerializeField]Animator anim;


    public Attack trenutni = new Attack();
    public Attack jab = new Attack("jab","mid", 5);
    public Attack kick = new Attack("kick", "high", 20);


    void Start()
    {
        anim.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            trenutni.kopiraj(jab);
            anim.SetTrigger("Punch");
            Debug.Log(trenutni.pos);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            trenutni.kopiraj(kick);
            anim.SetTrigger("Kick");
            Debug.Log(trenutni.pos);
        }

    }




}
