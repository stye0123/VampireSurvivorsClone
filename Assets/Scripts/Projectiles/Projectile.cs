using System.Collections;
using UnityEngine;
using UnityEngine.Events;
//用途：投射物基類，定義了投射物的基本行為和屬性
namespace Vampire
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer projectileSpriteRenderer; // 投射物的精靈渲染器
        [SerializeField] protected float maxDistance; // 投射物的最大飛行距離
        [SerializeField] protected float rotationSpeed = 0; // 投射物的旋轉速度
        [SerializeField] protected float airResistance = 0; // 投射物的空氣阻力
        [SerializeField] protected ParticleSystem destructionParticleSystem; // 投射物銷毀時的粒子效果
        protected float despawnTime = 1;  // 投射物離開螢幕後多久會被銷毀
        protected LayerMask targetLayer; // 可以擊中的目標層
        protected float speed; // 投射物的速度
        protected float damage; // 投射物的傷害值
        protected float knockback; // 投射物的擊退值
        protected EntityManager entityManager; // 實體管理器
        protected Character playerCharacter; // 玩家角色
        protected Collider2D col; // 碰撞器
        protected ZPositioner zPositioner; // Z軸位置控制器
        protected Coroutine moveCoroutine; // 移動協程
        protected int projectileIndex; // 投射物在池中的索引
        protected Vector2 direction; // 投射物的移動方向
        protected TrailRenderer trailRenderer = null; // 拖尾效果
        public UnityEvent<float> OnHitDamageable { get; private set; } // 擊中可傷害目標時的事件

        protected virtual void Awake()
        {
            col = GetComponent<Collider2D>();
            zPositioner = gameObject.AddComponent<ZPositioner>();
            TryGetComponent<TrailRenderer>(out trailRenderer);
        }

        public virtual void Init(EntityManager entityManager, Character playerCharacter)
        {
            this.entityManager = entityManager;
            this.playerCharacter = playerCharacter;
            zPositioner.Init(playerCharacter.transform);
        }
        
        public virtual void Setup(int projectileIndex, Vector2 position, float damage, float knockback, float speed, LayerMask targetLayer)
        {
            transform.position = position;
            trailRenderer?.Clear();
            this.projectileIndex = projectileIndex;
            this.damage = damage;
            this.knockback = knockback;
            this.speed = speed;
            this.targetLayer = targetLayer;
            col.enabled = true;
            OnHitDamageable = new UnityEvent<float>();
        }

        public virtual void Launch(Vector2 direction)
        {
            this.direction = direction.normalized;
            moveCoroutine = StartCoroutine(Move());
        }

        public virtual IEnumerator Move()
        {
            float distanceTravelled = 0;
            float timeOffScreen = 0;
            while (distanceTravelled < maxDistance && timeOffScreen < despawnTime && speed > 0)
            {
                float step = speed * Time.deltaTime;
                transform.position += step * (Vector3)direction;
                distanceTravelled += step;
                transform.RotateAround(transform.position, Vector3.back, Time.deltaTime*100*rotationSpeed);
                speed -= airResistance * Time.deltaTime;
                yield return null;
            }
            HitNothing();
        }

        protected virtual void HitDamageable(IDamageable damageable)
        {
            damageable.TakeDamage(damage, knockback * direction);
            OnHitDamageable.Invoke(damage);
            DestroyProjectile();
        }

        protected virtual void HitNothing()
        {
            DestroyProjectile();
        }

        protected virtual void DestroyProjectile()
        {
            StartCoroutine(DestroyProjectileAnimation());
        }

        protected IEnumerator DestroyProjectileAnimation()
        {
            projectileSpriteRenderer.gameObject.SetActive(false);
            destructionParticleSystem.Play();
            yield return new WaitForSeconds(destructionParticleSystem.main.duration);
            projectileSpriteRenderer.gameObject.SetActive(true);
            entityManager.DespawnProjectile(projectileIndex, this);
        }

        protected void CollisionCheck(Collider2D collider)
        {
            if ((targetLayer & (1 << collider.gameObject.layer)) != 0)
            {
                col.enabled = false;
                StopCoroutine(moveCoroutine);
                if (collider.transform.parent.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    HitDamageable(collider.gameObject.GetComponentInParent<IDamageable>());
                }
                else
                {
                    HitNothing();
                }
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            CollisionCheck(collider);
        }

        // void OnColliderEnter2D(Collider2D collider)
        // {
        //     CollisionCheck(collider);
        // }
    }
}
