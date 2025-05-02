using UnityEngine.Pool;
using UnityEngine;
//用途：投射物池，管理投射物的生成、回收和重用
namespace Vampire
{
    public class ProjectilePool : Pool
    {
        protected ObjectPool<Projectile> pool; // Unity的物件池，用於管理投射物

        public override void Init(EntityManager entityManager, Character playerCharacter, GameObject prefab, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            base.Init(entityManager, playerCharacter, prefab, collectionCheck, defaultCapacity, maxSize);
            pool = new ObjectPool<Projectile>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPooledItem, collectionCheck, defaultCapacity, maxSize);
        }

        public Projectile Get()
        {
            return pool.Get(); // 從池中獲取一個投射物
        }

        public void Release(Projectile projectile)
        {
            pool.Release(projectile); // 將投射物返回到池中
        }

        protected Projectile CreatePooledItem()
        {
            Projectile projectile = Instantiate(prefab, transform).GetComponent<Projectile>();
            projectile.Init(entityManager, playerCharacter);
            return projectile;
        }

        protected void OnTakeFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true); // 從池中取出時啟用遊戲物件
        }

        protected void OnReturnedToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false); // 返回池中時禁用遊戲物件
        }

        protected void OnDestroyPooledItem(Projectile projectile)
        {
            Destroy(projectile.gameObject); // 銷毀投射物
        }
    }
}
