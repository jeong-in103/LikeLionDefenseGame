using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharClass
{
    Vanguard,
    Guard,
    Defender,
    Sniper,
    Caster,
    Medic,
}

public enum CharState
{
    Start,
    Idle,
    Attack,
    Die
}

public enum AutoAttackType
{
    Melee,
    Ranged,
    Heal,
}

public enum SkillType
{
    Active,
    Passive,
}
