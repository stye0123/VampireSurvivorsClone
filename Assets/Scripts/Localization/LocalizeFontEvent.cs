using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
//用途：本地化字體事件，可以本地化字體
namespace Vampire
{
    [Serializable]
    public class UnityEventTMPFont : UnityEvent<TMP_FontAsset> {}
    
    [AddComponentMenu("Localization/Asset/Localize Font Event")]
    public class LocalizeFontEvent : LocalizedAssetEvent<TMP_FontAsset, LocalizedTmpFont, UnityEventTMPFont>
    {
        [SerializeField] private TextMeshProUGUI[] _tmpUITexts;
        [SerializeField] private TextMeshPro[] _tmpTexts;
        
        protected override void UpdateAsset(TMP_FontAsset font)
        {
            base.UpdateAsset(font);
            foreach (var tmp in _tmpUITexts)
            {
                tmp.font = font;
            }
            foreach (var tmp in _tmpTexts)
            {
                tmp.font = font;
            }
        }
    }
}