using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum Status
    {
        NORMAL,
        FROZEN,
        MEDITATING,
        DEFENDED,
        DEFLECTING,
        VULNERABLE,
        num_statuses
    };

    public enum Range
    {
        SELF,
        MELEE,
        MELEE_B,
        SINGLE_TARGET,
        MULTI_TARGET,
        BROADCAST,
        ALL,
        num_ranges
    };

    public enum Type
    {
        OFFENSE,
        DEFENSE,
        SPECIAL,
        num_types
    };
}
