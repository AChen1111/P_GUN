using System.Collections;
using UnityEngine;

namespace Game.UI
{
    public class UIStackInitializer : MonoBehaviour
    {
        [Header("场景主面板")]
        [SerializeField] private UIPanelBase mainPanel;
        private IEnumerator Start()
        {
            Debug.Log($"UIStackInitializer: Start {gameObject.name}.", this);
            // 等待场景切换事件清空旧 UI 栈后, 再压入本场景主面板.
            yield return null;
            Debug.Log($"UIStackInitializer: Initialize {mainPanel?.name}.", this);
            if (mainPanel == null)
            {
                Debug.LogError("UIStackInitializer初始化失败, 请指定场景主面板.");
                yield break;
            }

            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager == null)
            {
                yield break;
            }

            // 每个场景初始化时清空旧栈, 并压入当前场景主面板.
            stackManager.Initialize(mainPanel);
        }
    }
}
