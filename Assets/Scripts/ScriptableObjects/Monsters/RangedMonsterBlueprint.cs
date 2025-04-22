using UnityEngine;
//用途：遠程怪物藍圖，可以生成遠程怪物
namespace Vampire
{
    [CreateAssetMenu(fileName = "Ranged Monster", menuName = "Blueprints/Monsters/Ranged Monster", order = 1)]
    public class RangedMonsterBlueprint : MonsterBlueprint
    {
        [Header("Ranged Monster")]
        public GameObject projectilePrefab;
        public LayerMask targetLayer;
        public float projectileSpeed;
        public float range;
        public float timeAllowedOutsideRange;
    }
}
