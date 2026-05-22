using UnityEngine;
using Game.Pooling;

namespace Game.Presentation
{
    public class DamageTextPool : PoolBase<DamageText>
    {
        public new static DamageTextPool Instance => PoolBase<DamageText>.Instance as DamageTextPool;
        public DamageText Play(DamageText prefab, int damage, Vector3 position)
        {
            var damageText = Get(prefab, position, Quaternion.identity);
            if(damageText == null) return null;

            damageText.Play(damage, position);
            return damageText;
        }
        public DamageText Play(int damage, Vector3 position)
        {
            return Play(DefaultPrefab, damage, position);
        }
        protected override void OnCreate(DamageText item, DamageText prefab)
        {
            // 完成动画后回收到池中, 避免伤害数字频繁创建和销毁.
            item.OnComplete = Release;
        }
        protected override void OnDestroyItem(DamageText item)
        {
            item.OnComplete = null;
        }
    }
}
