using UnityEngine;
//用途：硬幣袋，可以收集硬幣
namespace Vampire
{
    public class CoinPouch : MonoBehaviour
    {
        private Character playerCharacter;

        public void Init(Character playerCharacter)
        {
            this.playerCharacter = playerCharacter;
        }

        
    }
}
