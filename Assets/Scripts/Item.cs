using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;

[System.Serializable]
public class Item
{
    private static int meleBonus = 5;

    public int damage;
    public Type type;
    public Range range;
    public string display;
    public string desc;
    public Status statusEffect;
    public int statusDuration;
    public Sprite sprite;


    public Item(int dmg, Type t, Range r, string n, string desc, Status s, int dur)
    {
        this.damage = dmg;
        this.type = t;
        this.range = r;
        this.display = n;
        this.desc = desc;
        this.statusEffect = s;
        this.statusDuration = dur;
    }

    public void use(Player src, Player trg, Range r)
    {
        switch (r) {
            case Range.SELF:
                src.takeDamage(damage);
                src.nextStatus = this.statusEffect;
                src.nextDuration = this.statusDuration;
                break;
            case Range.MELEE:
            case Range.MELEE_B:
                trg.takeDamage(damage + meleBonus);
                trg.nextStatus = this.statusEffect;
                trg.nextDuration = this.statusDuration;
                break;
            case Range.SINGLE_TARGET:
            case Range.MULTI_TARGET:
            case Range.BROADCAST:
            case Range.ALL:
                trg.takeDamage(damage);
                trg.nextStatus = this.statusEffect;
                trg.nextDuration = this.statusDuration;
                break;
        }

        trg.updateStatus();
    }

    public void use(Player src, Player[] trg)
    {
        Player target;
        switch (range) {
            case Range.SELF:
                use(src, trg[0], Range.SELF);
                break;
            case Range.MELEE:
            case Range.SINGLE_TARGET:
                target = checkStatus(src, trg[0]);
                use(src, target, Range.SINGLE_TARGET);
                break;
            case Range.MELEE_B:
                foreach (Player p in src.getConnections())
                {
                    target = checkStatus(src, p);
                    use(src, target, Range.MULTI_TARGET);
                }
                break;
            case Range.MULTI_TARGET:
            case Range.BROADCAST:
                foreach (Player p in trg) {
                    target = checkStatus(src, p);
                    use(src, target, Range.MULTI_TARGET);
                }
                break;
            case Range.ALL:
                use(src, trg[0], Range.SELF);
                foreach (Player p in trg) {
                    target = checkStatus(src, p);
                    use(src, target, Range.ALL);
                }
                break;
        }

        Player checkStatus(Player src, Player target)
        {
            if (target.status == Status.DEFLECTING)
            {
                return src;
            }
            return target;
        }
    }
}
