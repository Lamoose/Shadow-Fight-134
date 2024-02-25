using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string ime, pos;
    public int dmg;

    public Vector2 dir;

    public Attack() { }

    public Attack(string pime, string ppos, int pdmg, float x, float y)
    {
        ime = pime;
        pos = ppos;
        dmg = pdmg;
        dir = new Vector2(x, y);
    }

    public void kopiraj(Attack napad)
    {
        ime = napad.ime;
        pos = napad.pos;
        dmg = napad.dmg;
        dir = napad.dir;
    }

}
