using UnityEngine;

public class VfxPool : PoolBase<BloodVfx> {
    public new static VfxPool Instance {
        get {
            var instance = PoolBase<BloodVfx>.Instance as VfxPool;
            if (instance == null) {
                var go = new GameObject("[VfxPool]");
                instance = go.AddComponent<VfxPool>();
            }

            return instance;
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
    

    public BloodVfx Play(Vector3 position, Vector2 direction, BloodVfxColorMode colorMode) {
        return Play(DefaultPrefab, position, direction, colorMode);
    }

    public BloodVfx Play(Vector3 position, Vector2 direction) {
        return Play(position, direction, BloodVfxColorMode.Red);
    }

    protected override void OnCreate(BloodVfx item, BloodVfx prefab) {
        item.OnComplete = Release;
    }

    protected override void OnRelease(BloodVfx item) {
        item.StopImmediate();
    }

    protected override void OnDestroyItem(BloodVfx item) {
        item.OnComplete = null;
    }
}
