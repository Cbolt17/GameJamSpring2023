using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;

[System.Serializable]
public class Item
{
    public int damage;
    public Type type;
    public Range range;
    public string display;
    public string desc;
    public Status statusEffect;
    public float statusDuration;
    public Sprite sprite;


    public Item(int dmg, Type t, Range r, string n, string desc, Status s, float dur, Sprite sprite)
    {
        this.damage = dmg;
        this.type = t;
        this.range = r;
        this.display = n;
        this.desc = desc;
        this.statusEffect = s;
        this.statusDuration = dur;
        this.sprite = sprite;
    }

    public void use(Player src, Player trg, Range r)
    {
        if (r == Range.SELF)
        {
            src.takeDamage(damage);
            src.nextStatus = this.statusEffect;
            src.nextDuration = this.statusDuration;
        }
        else
        {
            trg.takeDamage(damage);
            trg.nextStatus = this.statusEffect;
            trg.nextDuration = this.statusDuration;
        }
    }

    public void use(Player src, Player[] trg)
    {
        switch (range)
        {
            case Range.SELF:
                use(src, trg[0], Range.SELF);
                break;
            case Range.Melee:
            case Range.SINGLE_TARGET:
                use(src, trg[0], Range.SINGLE_TARGET);
                break;
            case Range.MULTI_TARGET:
            case Range.BROADCAST:
                foreach (Player p in trg)
                {
                    use(src, p, Range.MULTI_TARGET);
                }
                break;
            case Range.ALL:
                use(src, trg[0], Range.SELF);
                foreach (Player p in trg)
                {
                    use(src, p, Range.ALL);
                }
                break;
        }
    }
}