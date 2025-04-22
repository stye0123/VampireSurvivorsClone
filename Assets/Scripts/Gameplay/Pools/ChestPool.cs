using UnityEngine.Pool;
using UnityEngine;
//用途：寶箱池，可以生成寶箱
namespace Vampire
{
    public class ChestPool : Pool
    {
        protected ObjectPool<Chest> pool;

        public override void Init(EntityManager entityManager, Character playerCharacter, GameObject prefab, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            base.Init(entityManager, playerCharacter, prefab, collectionCheck, defaultCapacity, maxSize);
            pool = new ObjectPool<Chest>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPooledItem, collectionCheck, defaultCapacity, maxSize);
        }

        public Chest Get()
        {
            return pool.Get();
        }

        public void Release(Chest chest)
        {
            pool.Release(chest);
        }
        //創建寶箱
        protected Chest CreatePooledItem()
        {
            //實例化寶箱
            Chest chest = Instantiate(prefab, transform).GetComponent<Chest>();
            //初始化寶箱
            chest.Init(entityManager, playerCharacter, transform);
            return chest;
        }

        protected void OnTakeFromPool(Chest chest)
        {
            chest.gameObject.SetActive(true);
        }

        protected void OnReturnedToPool(Chest chest)
        {
            chest.gameObject.SetActive(false);
        }

        protected void OnDestroyPooledItem(Chest chest)
        {
            Destroy(chest.gameObject);
        }
    }
}
