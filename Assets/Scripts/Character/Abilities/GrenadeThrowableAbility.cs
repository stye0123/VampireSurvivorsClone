using System.Collections;
using UnityEngine;
//用途：手榴彈技能，可以投擲手榴彈
namespace Vampire
{
    public class GrenadeThrowableAbility : ThrowableAbility
    {
        [Header("Grenade Stats")]
        [SerializeField] protected UpgradeableProjectileCount fragmentCount;

        protected override void LaunchThrowable()
        {
            GrenadeThrowable throwable = (GrenadeThrowable) entityManager.SpawnThrowable(throwableIndex, playerCharacter.CenterTransform.position, damage.Value, knockback.Value, 0, monsterLayer);
            throwable.SetupGrenade(fragmentCount.Value);
            throwable.Throw((Vector2)playerCharacter.transform.position + Random.insideUnitCircle * throwRadius);
            throwable.OnHitDamageable.AddListener(playerCharacter.OnDealDamage.Invoke);
        }
    }
}
