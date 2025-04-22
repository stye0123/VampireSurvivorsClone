using System.Collections;
using UnityEngine;
//用途：槍械技能，可以射擊槍械
namespace Vampire
{
    public class GunAbility : ProjectileAbility
    {
        [Header("Gun Stats")]
        [SerializeField] protected UpgradeableProjectileCount projectileCount;
        [SerializeField] protected UpgradeableDamageRate firerate;

        protected override void Attack()
        {
            StartCoroutine(FireClip());
        }

        protected IEnumerator FireClip()
        {
            int clipSize = projectileCount.Value;
            timeSinceLastAttack -= clipSize/firerate.Value;
            for (int i = 0; i < clipSize; i++)
            {
                LaunchProjectile();
                yield return new WaitForSeconds(1/firerate.Value);
            }
        }
    }
}
