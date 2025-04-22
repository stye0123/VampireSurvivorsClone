using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Vampire
{
    //被塞到MainMenu中
    //應該是整個選擇介面的總體
    //要指定CharacterBlueprint角色藍圖的資料
    //要指定CharacterCardPrefab角色卡(這是一組樣板UI，就是卡片介面本身，他會把藍圖的資料塞到卡片中)
    //要指定CoinDisplay目前持有的金幣數量
    
    public class CharacterSelector : MonoBehaviour
    {
        [SerializeField] protected CharacterBlueprint[] characterBlueprints;
        [SerializeField] protected GameObject characterCardPrefab;
        [SerializeField] protected CoinDisplay coinDisplay;

        private CharacterCard[] characterCards;
        
        // 初始化角色卡片陣列，根據角色藍圖的數量創建相應的角色卡片實例。
        // 對於每個角色藍圖，實例化角色卡片並將其初始化，
        // 傳入當前的角色選擇器、角色藍圖和金幣顯示。
        // 強制重建佈局以確保角色卡片正確顯示。
        // 更新每個角色卡片的佈局以反映當前的狀態。
        public void Init()
        {
            //初始化角色卡片陣列，根據角色藍圖的數量創建相應的角色卡片實例。
            characterCards = new CharacterCard[characterBlueprints.Length];
            for (int i = 0; i < characterBlueprints.Length; i++)
            {
                characterCards[i] = Instantiate(characterCardPrefab, this.transform).GetComponent<CharacterCard>();
                characterCards[i].Init(this, characterBlueprints[i], coinDisplay);
            }
            //強制重建佈局以確保角色卡片正確顯示。
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            //更新每個角色卡片的佈局以反映當前的狀態。
            for (int i = 0; i < characterBlueprints.Length; i++)
            {
                characterCards[i].UpdateLayout();
            }
        }
        
        // 此方法用於啟動遊戲，接收一個角色藍圖作為參數。
        // 將選擇的角色藍圖儲存到跨場景資料中，以便在其他場景中使用。
        // 然後加載場景1，這通常是遊戲的主要遊玩場景。
        public void StartGame(CharacterBlueprint characterBlueprint)
        {
            //將選擇的角色藍圖儲存到跨場景資料中，以便在其他場景中使用。
            CrossSceneData.CharacterBlueprint = characterBlueprint;
            //加載場景1，這通常是遊戲的主要遊玩場景。
            SceneManager.LoadScene(1);
        }
    }
}
