using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//用途：起始能力容器，可以顯示起始能力
namespace Vampire
{
    public class StartingAbilityContainer : MonoBehaviour
    {
        [field: SerializeField]
        public Image AbilityImage { get; private set; }
        [field: SerializeField]
        public RectTransform ImageRect { get; private set; }
        [field: SerializeField]
        public RectTransform ContainerRect { get; private set; }
    }
}
