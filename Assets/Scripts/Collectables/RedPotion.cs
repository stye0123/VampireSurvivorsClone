using System.Collections;
using UnityEngine;
//用途：紅色藥水，可以恢復生命值
namespace Vampire
{
    public class RedPotion : Collectable
    {
        [SerializeField] protected float healAmount = 50;
        [SerializeField] protected float healTime = 30;

        protected override void OnCollected()
        {
            gameObject.SetActive(true);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            StartCoroutine(HealOverTime());
        }

        private IEnumerator HealOverTime()
        {
            float t = 0;
            while (t < healTime)
            {
                t += Time.deltaTime;
                playerCharacter.GainHealth(Time.deltaTime * healAmount / healTime);
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
