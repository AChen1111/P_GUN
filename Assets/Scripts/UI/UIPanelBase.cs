using Game.Pooling;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    // 所有栈式UI面板的基类, 统一处理显示状态, 交互状态和默认焦点.
    public class UIPanelBase : MonoBehaviour
    {
        [Header("UI基础配置")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button defaultSelectedButton;

        public CanvasGroup CanvasGroup => canvasGroup;
        public Button DefaultSelectedButton => defaultSelectedButton;

        // 面板的 Lua 模块, 需要 Lua 逻辑的面板在预制体上挂 LuaComponet.
        private LuaBehaviourHost panelLua;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        protected virtual void Awake()
        {
            // 运行时确保面板具备CanvasGroup, 后续才能统一控制显示和交互.
            ResolveCanvasGroup();
            panelLua = GetComponent<LuaBehaviourHost>();
        }

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        protected virtual void Reset()
        {
            // 编辑器中重置组件时自动补全CanvasGroup引用.
            ResolveCanvasGroup();
        }
        public void Open()
        {
            // 公共打开流程, 先显示并恢复交互, 再执行子类打开逻辑, 最后交给 Lua 模块.
            SetVisible(true, true);
            OnOpen();
            panelLua?.CallLuaFunction("OnOpen");
            FocusDefaultButton();
        }
        public void Close()
        {
            // 公共关闭流程, 先执行子类清理逻辑, 再交给 Lua 模块, 最后隐藏并停止交互.
            OnClose();
            panelLua?.CallLuaFunction("OnClose");
            SetVisible(false, false);
        }
        public void Pause()
        {
            // 公共暂停流程, 保留显示, 但禁止交互和射线.
            SetVisible(true, false);
            OnPause();
            panelLua?.CallLuaFunction("OnPause");
        }
        public void Resume()
        {
            // 公共恢复流程, 重新启用交互, 再执行子类恢复逻辑.
            SetVisible(true, true);
            OnResume();
            panelLua?.CallLuaFunction("OnResume");
            FocusDefaultButton();
        }
        protected virtual void OnOpen()
        {
            // 子类可重写此方法, 添加面板打开后的业务逻辑.
        }
        protected virtual void OnClose()
        {
            // 子类可重写此方法, 添加面板关闭前的清理逻辑.
        }
        protected virtual void OnPause()
        {
            // 子类可重写此方法, 添加面板暂停后的业务逻辑.
        }
        protected virtual void OnResume()
        {
            // 子类可重写此方法, 添加面板恢复后的业务逻辑.
        }
        internal void BringToTop(int sortingOrder)
        {
            // 先调整同级顺序, 保证普通UI节点显示在兄弟节点上方.
            transform.SetAsLastSibling();

            if (TryGetComponent(out Canvas canvas))
            {
                // 独立Canvas需要显式排序, 保证新入栈面板渲染在最上层.
                canvas.overrideSorting = true;
                canvas.sortingOrder = sortingOrder;
            }
        }
        protected void SetDefaultSelectedButton(Button button)
        {
            // 允许子类在运行时指定默认选中的按钮.
            defaultSelectedButton = button;
        }
        protected void FocusDefaultButton()
        {
            if (defaultSelectedButton == null || EventSystem.current == null)
            {
                return;
            }

            // 等待一帧后设置焦点, 避免刚显示时UI对象状态尚未刷新.
            StartCoroutine(FocusDefaultButtonNextFrame());

            IEnumerator FocusDefaultButtonNextFrame()
            {
                yield return null;
                if (defaultSelectedButton == null || EventSystem.current == null || !defaultSelectedButton.gameObject.activeInHierarchy)
                {
                    yield break;
                }

                // 先清空再设置, 确保EventSystem触发新的选中状态.
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedButton.gameObject);
            }
}
        protected void SetVisible(bool visible, bool interactable)
        {
            // 所有面板统一通过CanvasGroup控制可见性, 交互和射线拦截.
            ResolveCanvasGroup();
            gameObject.SetActive(visible);

            canvasGroup.alpha = visible ? 1f : 0f;
            canvasGroup.interactable = visible && interactable;
            canvasGroup.blocksRaycasts = visible && interactable;
        }
        private void ResolveCanvasGroup()
        {
            if (canvasGroup != null)
            {
                return;
            }

            if (!TryGetComponent(out canvasGroup))
            {
                throw new System.InvalidOperationException($"{nameof(UIPanelBase)} requires {nameof(CanvasGroup)}.");
            }
        }
    }
}
