using UnityEngine;
//用途：投擲型怪物藍圖，可以生成投擲型怪物
namespace Vampire
{
    [CreateAssetMenu(fileName = "Boomerang Monster", menuName = "Blueprints/Monsters/Boomerang Monster", order = 1)]
    public class BoomerangMonsterBlueprint : MonsterBlueprint
    {
        [Header("Boomerang Monster")]
        public GameObject boomerangPrefab;
        public LayerMask targetLayer;
        public float boomerangDamage;
        public float boomerangAttackSpeed;
        public float range;
        public float throwRange;
        public float throwTime;
    }
}
