using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//用途：爆炸性投射物，在擊中目標或到達最大距離時會產生爆炸效果
namespace Vampire
{
    public class ExplosiveProjectile : Projectile
    {
        [SerializeField] protected LayerMask explosionLayerMask; // 爆炸可以影響的層
        [SerializeField] protected float explosionRadius; // 爆炸半徑
        [SerializeField] protected float explosionDamage; // 爆炸傷害
        [SerializeField] protected float explosionKnockback; // 爆炸擊退
        [SerializeField] protected float explosionDuration = 0.25f; // 爆炸持續時間
        [SerializeField] protected SpriteRenderer explosionSpriteRenderer; // 爆炸效果的精靈渲染器

        public void SetupExplosion(float explosionDamage, float explosionRadius, float explosionKnockback)
        {
            this.explosionDamage = explosionDamage;
            this.explosionRadius = explosionRadius;
            this.explosionKnockback = explosionKnockback;
        }

        protected override void HitDamageable(IDamageable damageable)
        {
            // 擊中可傷害目標時觸發爆炸
            StartCoroutine(Explosion());
        }

        protected override void HitNothing()
        {
            // 未擊中目標時也觸發爆炸
            StartCoroutine(Explosion());
        }

        protected IEnumerator Explosion()
        {
            projectileSpriteRenderer.gameObject.SetActive(false);
            destructionParticleSystem.Play();
            float t = 0;
            Dictionary<Collider2D, bool> damagedColliders = new Dictionary<Collider2D, bool>(); // 記錄已經受到傷害的碰撞器
            while (t < 1)
            {
                // 更新爆炸效果的視覺表現
                Color c = explosionSpriteRenderer.color;
                c.a = EasingUtils.EaseOutQuart(1-t)*0.5f;
                explosionSpriteRenderer.color = c;
                explosionSpriteRenderer.transform.localScale = Vector3.one * EasingUtils.EaseOutQuart(t) * explosionRadius * 10;

                // 檢測爆炸範圍內的目標
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, t*explosionRadius, explosionLayerMask);
                foreach (Collider2D collider in hitColliders)
                {
                    if (!damagedColliders.ContainsKey(collider))
                    {
                        Vector2 dir = collider.transform.position - transform.position;
                        collider.GetComponentInParent<IDamageable>().TakeDamage(explosionDamage, dir.normalized * explosionKnockback);
                        damagedColliders[collider] = true;
                        OnHitDamageable.Invoke(explosionDamage);
                    }
                }
                t += Time.deltaTime/explosionDuration;
                yield return null;
            }
            explosionSpriteRenderer.color = explosionSpriteRenderer.color - Color.black;
            if (explosionDuration < destructionParticleSystem.main.duration)
                yield return new WaitForSeconds(destructionParticleSystem.main.duration - explosionDuration);
            projectileSpriteRenderer.gameObject.SetActive(true);
            entityManager.DespawnProjectile(projectileIndex, this);
        }
    }
}
