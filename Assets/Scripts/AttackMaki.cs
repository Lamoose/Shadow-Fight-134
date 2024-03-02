using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMaki
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
    public string stranaUdarca;
    public int blockRecovery;
    public Vector2 posebanKnockback;


    public AttackMaki() { }

    public AttackMaki(string pime, string ppos, int pdmg, float x, float y, string ppozicija, string pstranaUdarca, int pblockRecovery)
    {
        ime = pime;
        pos = ppos;
        dmg = pdmg;
        dir = new Vector2(x, y);
        pozicija = ppozicija;
        stranaUdarca = pstranaUdarca;
        blockRecovery = pblockRecovery;
    }

    public AttackMaki(string pime, string ppos, int pdmg, float x, float y, string ppozicija, string pstranaUdarca, int pblockRecovery, float posebanX, float posebanY)
    {
        ime = pime;
        pos = ppos;
        dmg = pdmg;
        dir = new Vector2(x, y);
        pozicija = ppozicija;
        stranaUdarca = pstranaUdarca;
        blockRecovery = pblockRecovery;
        posebanKnockback = new Vector2(posebanX, posebanY);
    }


    //public Attack PunchKickAir = new Attack("PunchKickAir", "high", 7, 0f, 0f, "air", "oba", 9, 500f, 0f);
    public void kopiraj(Attack napad)
    {
        ime = napad.ime;
        pos = napad.pos;
        dmg = napad.dmg;
        dir = napad.dir;
        pozicija = napad.pozicija;
        stranaUdarca = napad.stranaUdarca;
        blockRecovery = napad.blockRecovery;
        posebanKnockback = napad.posebanKnockback;
        ComboNapadi.Clear();
        for (int i = 0; i < napad.ComboNapadi.Count; i++)
        {
            ComboNapadi.Add(napad.ComboNapadi[i]);
        }
    }

}
