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
    Melee, // 근거리
    Ranged, // 원거리
}

public enum SkillType
{
    Active,
    Passive,
}

public static class LocatedCoolTime
{
    public const float fast = 18f;
    public const float medium_Merchant = 25f;
    public const float medium_Agent = 35f;
    public const float slow = 70f;
    public const float slow_much = 200f;
}