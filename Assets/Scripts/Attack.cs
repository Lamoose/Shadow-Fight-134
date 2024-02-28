using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string ime, pos;
    public int dmg;
    public List<Attack> ComboNapadi = new List<Attack>();
    public Vector2 dir;

    public KeyCode p1obicanInput;
    public string p1dirInput;
    public KeyCode p2obicanInput;
    public string p2dirInput;
    public string pozicija;


    public Attack() { }

    public Attack(string pime, string ppos, int pdmg, float x, float y, string ppozicija)
    {
        ime = pime;
        pos = ppos;
        dmg = pdmg;
        dir = new Vector2(x, y);
        pozicija = ppozicija;

    }

    public void kopiraj(Attack napad)
    {
        ime = napad.ime;
        pos = napad.pos;
        dmg = napad.dmg;
        dir = napad.dir;
        ComboNapadi.Clear();
        for (int i = 0; i < napad.ComboNapadi.Count; i++)
        {
            ComboNapadi.Add(napad.ComboNapadi[i]);
        }
    }

}
