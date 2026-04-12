using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// 道具配置（ScriptableObject）：展示用 Sprite、名称/Id、效果列表等。
    /// </summary>
    [CreateAssetMenu(fileName = "ItemSO", menuName = "PG/Item/Item SO", order = 0)]
    public class ItemSO : ScriptableObject
    {
        [Tooltip("用于地面拾取物或 UI 的图标/贴图")]
        public Sprite sprite;

        [Tooltip("展示名或逻辑 Id，按项目约定使用")]
        public string itemKey;

        [Tooltip("拾取时执行的效果资产")]
        public List<ItemEffectBase> effects;

        [Tooltip("拾取时播放的音频")]
        public AudioClip pickupAudio;
    }
}
