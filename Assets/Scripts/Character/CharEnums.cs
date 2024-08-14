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
    Wait,
    Start,
    Idle,
    Attack,
    Die
}

public enum AutoAttackType
{
    Melee, // 근거리
    Ranged, // 원거리
}

public enum SkillType
{
    Active,
    Passive,
}