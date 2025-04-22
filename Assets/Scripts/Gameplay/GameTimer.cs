using UnityEngine;
using TMPro;
//用途：遊戲計時器，可以顯示遊戲時間
namespace Vampire
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameTimer : MonoBehaviour
    {
        private TextMeshProUGUI timerText;

        void Awake()
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }

        public void SetTime(float t)
        {
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(t);
            timerText.text = timeSpan.ToString(@"mm\:ss");
        }
    }
}
