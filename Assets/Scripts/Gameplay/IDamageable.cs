using UnityEngine;
//用途：可受傷害，可以受到傷害
namespace Vampire
{
    public abstract class IDamageable : MonoBehaviour
    {
        public abstract void TakeDamage(float damage, Vector2 knockback = default(Vector2));
        public abstract void Knockback(Vector2 knockback);
    }
}
