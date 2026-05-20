using UnityEngine;
using Game.Pooling;

namespace Game.Presentation
{
    public class DamageTextPool : PoolBase<DamageText>
    {
        public new static DamageTextPool Instance => PoolBase<DamageText>.Instance as DamageTextPool;

        /// <summary>
        /// 执行 Play 逻辑.
        /// </summary>
        public DamageText Play(DamageText prefab, int damage, Vector3 position)
        {
            var damageText = Get(prefab, position, Quaternion.identity);
            if(damageText == null) return null;

            damageText.Play(damage, position);
            return damageText;
        }

        /// <summary>
        /// 执行 Play 逻辑.
        /// </summary>
        public DamageText Play(int damage, Vector3 position)
        {
            return Play(DefaultPrefab, damage, position);
        }

        /// <summary>
        /// 执行 OnCreate 逻辑.
        /// </summary>
        protected override void OnCreate(DamageText item, DamageText prefab)
        {
            // 完成动画后回收到池中, 避免伤害数字频繁创建和销毁.
            item.OnComplete = Release;
        }

        /// <summary>
        /// 执行 OnDestroyItem 逻辑.
        /// </summary>
        protected override void OnDestroyItem(DamageText item)
        {
            item.OnComplete = null;
        }
    }
}
