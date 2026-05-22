using UnityEngine;

namespace Game.UI
{
    public class UIStackInitializer : MonoBehaviour
    {
        [Header("场景主面板")]
        [SerializeField] private UIPanelBase mainPanel;
        private void Start()
        {
            if (mainPanel == null)
            {
                Debug.LogError("UIStackInitializer初始化失败, 请指定场景主面板.");
                return;
            }

            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager == null)
            {
                return;
            }

            // 每个场景初始化时清空旧栈, 并压入当前场景主面板.
            stackManager.Initialize(mainPanel);
        }
    }
}
