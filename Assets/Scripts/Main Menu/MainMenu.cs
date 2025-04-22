using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//用途：主選單，可以選擇角色
namespace Vampire
{
    //被塞到起始介面中
    //要指定CharacterSelector   
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private CharacterSelector characterSelector;

        void Start()
        {
            characterSelector.Init();
        }
    }
}
