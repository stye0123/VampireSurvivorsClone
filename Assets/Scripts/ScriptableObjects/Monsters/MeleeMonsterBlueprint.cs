using UnityEngine;
//用途：近戰怪物藍圖，可以生成近戰怪物
namespace Vampire
{
    [CreateAssetMenu(fileName = "Melee Monster", menuName = "Blueprints/Monsters/Melee Monster", order = 1)]
    public class MeleeMonsterBlueprint : MonsterBlueprint
    {
        [Header("Melee Monster")]
        public LayerMask meleeLayer;
    }
}
