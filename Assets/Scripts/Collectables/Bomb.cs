using UnityEngine;
//用途：炸彈，可以傷害所有可見的敵人
namespace Vampire
{
    public class Bomb : Collectable
    {
        [SerializeField] protected float bombDamage;

        protected override void OnCollected()
        {
            entityManager.DamageAllVisibileEnemies(bombDamage);
            Destroy(gameObject);
        }
    }
}
