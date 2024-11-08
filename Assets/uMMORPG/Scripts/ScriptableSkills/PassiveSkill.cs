﻿using UnityEngine;

[CreateAssetMenu(menuName="uMMORPG Skill/Passive Skill", order=999)]
public class PassiveSkill : BonusSkill
{
    public override bool CheckTarget(Entity caster) { return false; }
    public override bool CheckDistance(Entity caster, int skillLevel, out Vector2 destination)
    {
        destination = caster.transform.position;
        return false;
    }
    public override void Apply(Entity caster, int skillLevel, Vector2 direction) {}
}
