using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Usable
{
    public Status statusEffect;
    public int statusDuration;
    public static int i;
}

public abstract class Usable
{
    private int damage;
    private Class type;
    private Range range;

    public string name;
    public string desc;


    bool use(Player source, Player target);
}

public enum Range
{
    SELF,
    Melee,
    SINGLE_TARGET,
    MULTI_TARGET,
    BROADCAST,
    ALL
}

public enum Class
{
    Offense,
    Defense,
    Special
}