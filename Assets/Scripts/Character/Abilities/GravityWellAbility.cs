using System.Collections;
using UnityEngine;
//用途：重力井技能，可以攻擊怪物

namespace Vampire
{
    public class GravityWellAbility : ThrowableAbility
    {
        [Header("Gravity Well Stats")]
        [SerializeField] protected UpgradeableDuration duration;
        [SerializeField] protected UpgradeableAOE wellRadius;
    }
}
