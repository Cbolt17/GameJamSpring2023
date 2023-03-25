using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum Status
    {
        NEUTRAL,
        FROZEN,
        MEDITATING
    };

    public enum Range
    {
        SELF,
        Melee,
        SINGLE_TARGET,
        MULTI_TARGET,
        BROADCAST,
        ALL
    };

    public enum Type
    {
        OFFENSE,
        DEFENSE,
        SPECIAL,
        LAZER
    };
}
