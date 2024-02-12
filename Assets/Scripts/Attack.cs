using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string ime, pos;
    public int dmg;

    public Attack() { }

    public Attack (string pime, string ppos, int pdmg)
    {
        ime = pime;
        pos = ppos;
        dmg = pdmg;
    }

    public void kopiraj(Attack napad)
    {
        ime = napad.ime;
        pos = napad.pos;
        dmg = napad.dmg;
    }

}
