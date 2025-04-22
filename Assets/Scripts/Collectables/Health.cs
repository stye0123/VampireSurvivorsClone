using UnityEngine;
//用途：生命值，可以恢復生命值
namespace Vampire
{
    public class Health : Collectable
    {
        [SerializeField] protected float healAmount = 30;

        protected override void OnCollected()
        {
            playerCharacter.GainHealth(healAmount);
            Destroy(gameObject);
        }
    }
}
