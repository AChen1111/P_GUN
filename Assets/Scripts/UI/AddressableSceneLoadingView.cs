using Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// 场景原有 Canvas 下显式配置的加载提示, 订阅统一加载器的阶段进度.
    /// </summary>
    public sealed class AddressableSceneLoadingView : MonoBehaviour
    {
        [SerializeField] private Text statusText;
        [SerializeField] private Slider progressSlider;
        [SerializeField] private GameObject content;

        private void OnEnable()
        {
            if (AddressableLoader.Instance != null) AddressableLoader.Instance.SceneLoadProgress += Refresh;
            content.SetActive(false);
        }
        private void OnDisable()
        {
            if (AddressableLoader.Instance != null) AddressableLoader.Instance.SceneLoadProgress -= Refresh;
        }
        private void Refresh(string status, float progress)
        {
            content.SetActive(!string.IsNullOrEmpty(status));
            statusText.text = status;
            progressSlider.value = progress;
        }
    }
}
