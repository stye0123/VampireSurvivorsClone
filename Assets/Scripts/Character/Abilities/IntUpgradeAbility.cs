
using UnityEngine;
//用途：整數升級技能，可以升級整數
namespace Vampire
{
    public class IntUpgradeAbility<T> : Ability where T : UpgradeableInt
    {
        [SerializeField] protected int[] upgrades;
        public override string Description 
        { 
            get { 
                return GetUpgradeDescription();
            } 
        }

        protected override void Use()
        {
            base.Use();
            gameObject.SetActive(true);
            abilityManager.UpgradeValue<T, int>(upgrades[level]);
        }

        protected override void Upgrade()
        {
            abilityManager.UpgradeValue<T, int>(upgrades[level]);
        }

        public override bool RequirementsMet()
        {
            return level < upgrades.Length;
        }

        protected string GetUpgradeDescription()
        {
            return DescriptionUtils.GetUpgradeDescription(localizedDescription.GetLocalizedString(), upgrades[level]);
        }
    }
}