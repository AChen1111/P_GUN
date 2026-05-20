using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    // 负责按栈结构管理UI面板的打开, 暂停, 恢复和关闭.
    public class UIStackManager : MonoBehaviour
    {
        // 每次新面板入栈时提升排序层级, 避免多个Canvas显示顺序冲突.
        private const int DefaultSortingOrderStep = 10;

        private static UIStackManager instance;
        private static bool isQuitting;

        // 栈顶表示当前正在交互的面板, 栈底通常是主面板.
        private readonly Stack<UIPanelBase> panelStack = new Stack<UIPanelBase>();
        private int nextSortingOrder;

        public static UIStackManager Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                if (isQuitting)
                {
                    return null;
                }

                if (instance == null)
                {
                    Debug.LogError("UIStackManager获取失败, 请在场景中添加并绑定UIStackManager.");
                }

                return instance;
            }
        }

        public int Count
        {
            get
            {
                return panelStack.Count;
            }
        }

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                // 重复管理器只禁用组件, 避免误销毁挂载它的UI根节点和面板.
                enabled = false;
                return;
            }

            instance = this;
        }

        /// <summary>
        /// 注册启用时需要的监听.
        /// </summary>
        private void OnEnable()
        {
            // 监听场景切换, 避免栈中保留旧场景UI对象.
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        /// <summary>
        /// 执行 OnApplicationQuit 逻辑.
        /// </summary>
        private void OnApplicationQuit()
        {
            // 应用退出时阻止Instance再次自动创建新对象.
            isQuitting = true;
        }

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        /// <summary>
        /// 执行 Initialize 逻辑.
        /// </summary>
        public void Initialize(UIPanelBase mainPanel)
        {
            if (mainPanel == null)
            {
                Debug.LogError("UIStackManager初始化失败, 主面板不能为空.");
                return;
            }

            // 初始化时主面板作为栈底, 不暂停任何已有面板.
            Clear();
            PushInternal(mainPanel, false);
        }

        /// <summary>
        /// 执行 Push 逻辑.
        /// </summary>
        public void Push(UIPanelBase panel)
        {
            if (panel == null)
            {
                Debug.LogError("UIStackManager入栈失败, 面板不能为空.");
                return;
            }

            PushInternal(panel, true);
        }

        /// <summary>
        /// 执行 Pop 逻辑.
        /// </summary>
        public bool Pop()
        {
            if (panelStack.Count <= 1)
            {
                // 主面板作为栈底, 默认不允许被普通返回关闭.
                return false;
            }

            UIPanelBase topPanel = panelStack.Pop();

            if (topPanel != null)
            {
                HidePanel(topPanel);
            }

            UIPanelBase resumePanel = Peek();

            if (resumePanel != null)
            {
                // 恢复新的栈顶面板交互.
                resumePanel.Resume();
            }

            return true;
        }

        /// <summary>
        /// 执行 Clear 逻辑.
        /// </summary>
        public void Clear()
        {
            while (panelStack.Count > 0)
            {
                UIPanelBase panel = panelStack.Pop();

                if (panel != null)
                {
                    HidePanel(panel);
                }
            }

            nextSortingOrder = 0;
        }

        /// <summary>
        /// 执行 Peek 逻辑.
        /// </summary>
        public UIPanelBase Peek()
        {
            return panelStack.Count > 0 ? panelStack.Peek() : null;
        }

        /// <summary>
        /// 执行 PushInternal 逻辑.
        /// </summary>
        private void PushInternal(UIPanelBase panel, bool pauseCurrentPanel)
        {
            if (pauseCurrentPanel && panelStack.Count > 0)
            {
                // 新面板覆盖当前面板时, 暂停旧栈顶的交互.
                panelStack.Peek().Pause();
            }

            // 新面板入栈前先提升到最上层显示.
            panel.BringToTop(nextSortingOrder);
            nextSortingOrder += DefaultSortingOrderStep;

            // 打开面板后再入栈, 使其成为新的当前面板.
            panel.Open();
            panelStack.Push(panel);
        }

        /// <summary>
        /// 执行 HidePanel 逻辑.
        /// </summary>
        private void HidePanel(UIPanelBase panel)
        {
            // 面板退出栈时只隐藏和禁用交互, 不销毁GameObject.
            panel.Close();
        }

        /// <summary>
        /// 执行 OnActiveSceneChanged 逻辑.
        /// </summary>
        private void OnActiveSceneChanged(Scene previousScene, Scene nextScene)
        {
            // 场景切换时清空旧面板引用, 防止持有已销毁对象.
            Clear();
        }
    }
}
