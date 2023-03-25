using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;

public class Item : Usable
{
    Item(int id, int dmg, Type t, Range r, string n, string desc, Status s, float dur)
    {
        this.id = id;
        this.damage = dmg;
        this.type = t;
        this.range = r;
        this.name = n;
        this.desc = desc;
        this.statusEffect = s;
        this.statusDuration = dur;
        i += 1;
    }

    public void use(Player src, Player[] trg)
    {
        switch (range) {
            case Range.SELF:
                use(src, trg[0], Range.SELF);
                break;
            case Range.Melee:
            case Range.SINGLE_TARGET:
                use(src, trg[0], Range.SINGLE_TARGET);
                break;
            case Range.MULTI_TARGET:
            case Range.BROADCAST:
                foreach (Player p in trg) {
                    use(src, p, Range.MULTI_TARGET);
                }
                break;
            case Range.ALL:
                use(src, trg[0], Range.SELF);
                foreach (Player p in trg) {
                    use(src, p, Range.ALL);
                }
                break;
        }
    }
}

public abstract class Usable
{
    protected int id;

    public int damage;
    public Type type;
    public Range range;
    public string name;
    public string desc;
    public Status statusEffect;
    public float statusDuration;
    public static int i;


    public int getID() { return id; }

    public bool use(Player src, Player trg, Range r)
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
