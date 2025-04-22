using UnityEngine.Pool;
using UnityEngine;
//用途：經驗寶石池，可以生成經驗寶石
namespace Vampire
{
    public class ExpGemPool : Pool
    {
        protected ObjectPool<ExpGem> pool;

        //初始化經驗寶石池
        public override void Init(EntityManager entityManager, Character playerCharacter, GameObject prefab, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            // Unity的Pool類是一個用於管理物件池的基類，提供了高效的物件重用機制。它可以減少物件的創建和銷毀開銷，從而提高性能。
            //使用Pool類時，可以指定預設容量和最大容量，並且可以選擇是否進行集合檢查。這樣可以確保在需要時能夠快速獲取物件，並在不再需要時將其釋放回池中。
            //這裡的base.Init是從Pool類繼承來的，負責初始化池的基本設置。
            base.Init(entityManager, playerCharacter, prefab, collectionCheck, defaultCapacity, maxSize);
            pool = new ObjectPool<ExpGem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPooledItem, collectionCheck, defaultCapacity, maxSize);
        }

        //從池中獲取經驗寶石
        // Get方法是從池中獲取一個經驗寶石實例，並返回該實例。
        public ExpGem Get()
        {
            return pool.Get();
        }

        // 釋放經驗寶石到池中
        public void Release(ExpGem expGem)
        {
            pool.Release(expGem);
        }

        // 創建經驗寶石
        protected ExpGem CreatePooledItem()
        {
            ExpGem expGem = Instantiate(prefab, transform).GetComponent<ExpGem>();
            expGem.Init(entityManager, playerCharacter);
            return expGem;
        }

        // OnTakeFromPool方法是當從池中獲取經驗寶石時，將其設置為啟用狀態。
        protected void OnTakeFromPool(ExpGem expGem)
        {
            expGem.gameObject.SetActive(true);
        }

        //返回經驗寶石到池中
        protected void OnReturnedToPool(ExpGem expGem)
        {
            expGem.gameObject.SetActive(false);
        }

        //銷毀經驗寶石 
        protected void OnDestroyPooledItem(ExpGem expGem)
        {
            Destroy(expGem.gameObject);
        }
    }
}
