using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;

public class Item : Usable
{
    private int id;
    private int damage;
    private Type type;
    private Range range;


    Item(int id, int dmg, Type t, Range r, string n, string desc, Status s, float dur)
    {
        this.id = id;
        this.damage = dmg;
        this.type = t;
        this.range = r;
        super(n, desc, s, dur);
    }

    public bool use(Player src, Player[] trg)
    {
        switch (range) {
            case Range.SELF:
                super.use(src, trg[0], Range.SELF);
                break;
            case Range.Melee:
            case Range.SINGLE_TARGET:
                super.use(src, trg[0], Range.SINGLE_TARGET);
                break;
            case Range.MULTI_TARGET:
            case Range.BROADCAST:
                foreach (Player p in trg) {
                    super.use(src, p, Range.MULTI_TARGET);
                }
                break;
            case Range.ALL:
                super.use(src, trg[0], Range.SELF);
                foreach (Player p in trg) {
                    super.use(src, p, Range.ALL);
                }
        }
    }
}

public abstract class Usable
{
    public string name;
    public string desc;
    public Status statusEffect;
    public float statusDuration;
    public static int i;


    Usable(string n, string desc, Status s, float dur)
    {
        this.name = n;
        this.desc = desc;
        this.statusEffect = s;
        this.statusDuration = dur;
        i += 1;
    }


    public bool use(Player source, Player target, Range r)
    {
        if (r == Range.SELF) {
            src.takeDamage(damage);
            src.nextStatus = this.statusEffect;
            src.nextDuration = this.statusDuration;
        } else {
            trg.takeDamage(damage);
            trg.nextStatus = this.statusEffect;
            trg.nextDuration = this.statusDuration;
        }

        return true;
    }
}
