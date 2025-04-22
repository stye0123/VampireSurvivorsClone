//用途：磁鐵，可以吸引所有可見的硬幣和寶石
namespace Vampire
{
    public class Magnet : Collectable
    {   
        protected override void OnCollected()
        {
            entityManager.CollectAllCoinsAndGems();
            Destroy(gameObject);
        }
    }
}
