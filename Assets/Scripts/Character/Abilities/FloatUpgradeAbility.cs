
using UnityEngine;
//用途：浮點數升級技能，可以升級浮點數


namespace Vampire
{
    public class FloatUpgradeAbility<T> : Ability where T : UpgradeableFloat
    {
        [SerializeField] protected float[] upgrades;
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
            abilityManager.UpgradeValue<T, float>(upgrades[level]);
        }

        protected override void Upgrade()
        {
            abilityManager.UpgradeValue<T, float>(upgrades[level]);
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