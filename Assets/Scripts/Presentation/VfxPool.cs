using UnityEngine;
using Game.Core;
using Game.Pooling;

namespace Game.Presentation
{
    public class VfxPool : PoolBase<BloodVfx> {
        public new static VfxPool Instance {
            get {
                return PoolBase<BloodVfx>.Instance as VfxPool;
            }
        }

        /// <summary>
        /// 从池中取出一个 BloodVfx 并播放。
        /// </summary>
        public BloodVfx Play(BloodVfx prefab, Vector3 position, Vector2 direction, BloodVfxColorMode colorMode) {
            var vfx = Get(prefab, position, Quaternion.identity);
            if (vfx == null) return null;

            vfx.Play(position, direction, colorMode);
            return vfx;
        }


        /// <summary>
        /// 执行 Play 逻辑.
        /// </summary>
        public BloodVfx Play(Vector3 position, Vector2 direction, BloodVfxColorMode colorMode) {
            return Play(DefaultPrefab, position, direction, colorMode);
        }

        /// <summary>
        /// 执行 Play 逻辑.
        /// </summary>
        public BloodVfx Play(Vector3 position, Vector2 direction) {
            return Play(position, direction, BloodVfxColorMode.Red);
        }

        /// <summary>
        /// 执行 OnCreate 逻辑.
        /// </summary>
        protected override void OnCreate(BloodVfx item, BloodVfx prefab) {
            item.OnComplete = Release;
        }

        /// <summary>
        /// 执行 OnDestroyItem 逻辑.
        /// </summary>
        protected override void OnDestroyItem(BloodVfx item) {
            item.OnComplete = null;
        }
    }
}
