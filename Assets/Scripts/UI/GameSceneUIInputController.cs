using System.Collections.Generic;
using Game.Core;
using Game.Gameplay;
using Game.UI.Save;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// 游戏场景UI
    /// </summary>
    public class GameSceneUIInputController : MonoBehaviour
    {
        [Header("游戏场景设置面板")]
        [SerializeField] private SettingsPanel settingsPanel;
        [Header("背包面板")]
        [SerializeField] private InventoryPanel inventoryPanel;
        [Header("存档槽位面板")]
        [SerializeField] private SaveSlotPanel saveSlotPanel;
        [Header("Buff调试")]
        [SerializeField] private BuffDataBase buffDebugDataBase;

        private bool settingsOpen;
        private bool inventoryOpen;
        private bool saveSlotOpen;
        private float previousTimeScale = 1f;
        private float inventoryPreviousTimeScale = 1f;
        private float saveSlotPreviousTimeScale = 1f;
        private readonly BuffDebugWindow buffDebugWindow = new BuffDebugWindow();

        /// <summary>
        /// 注册启用时需要的监听.
        /// </summary>
        private void OnEnable()
        {
            GameplayCursorState.Reset();
        }

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable()
        {
            if (settingsOpen)
            {
                RestoreSettingsCloseState();
            }

            if (inventoryOpen)
            {
                RestoreInventoryCloseState();
            }

            if (saveSlotOpen)
            {
                RestoreSaveSlotCloseState();
            }

            buffDebugWindow.SetVisible(false);
            GameplayCursorState.Reset();
        }

        /// <summary>
        /// 执行每帧更新逻辑.
        /// </summary>
        private void Update()
        {
            // Ctrl 只接管鼠标战斗, 不影响移动, 换枪, 装弹和地图快捷键.
            GameplayCursorState.SetControlKeyHeld(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));

            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                ToggleInventoryPanel();
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                ToggleSaveSlotPanel();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (saveSlotOpen)
                {
                    CloseSaveSlotPanel();
                    return;
                }

                if (inventoryOpen)
                {
                    CloseInventoryPanel();
                    return;
                }

                ToggleSettingsPanel();
            }

            if (IsBuffDebugTogglePressed())
            {
                ToggleBuffDebugWindow();
            }

            if (settingsOpen && settingsPanel != null && !settingsPanel.gameObject.activeSelf)
            {
                // 设置面板也可能通过 Back 按钮出栈, 这里统一恢复暂停和鼠标状态.
                RestoreSettingsCloseState();
            }

            if (inventoryOpen && inventoryPanel != null && !inventoryPanel.gameObject.activeSelf)
            {
                // 背包也可能被UI栈关闭, 这里统一恢复暂停和鼠标状态.
                RestoreInventoryCloseState();
            }

            if (saveSlotOpen && saveSlotPanel != null && !saveSlotPanel.gameObject.activeSelf)
            {
                // 存档面板也可能被返回按钮出栈, 这里统一恢复暂停和鼠标状态.
                RestoreSaveSlotCloseState();
            }

            static bool IsBuffDebugTogglePressed()
            {
                bool altHeld = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
                return altHeld && Input.GetKeyDown(KeyCode.UpArrow);
            }

            void ToggleSaveSlotPanel()
            {
                if (settingsOpen || inventoryOpen)
                {
                    return;
                }

                if (saveSlotOpen)
                {
                    CloseSaveSlotPanel();
                    return;
                }

                OpenSaveSlotPanel();
            }

            void ToggleInventoryPanel()
            {
                if (settingsOpen || saveSlotOpen)
                {
                    return;
                }

                if (inventoryOpen)
                {
                    CloseInventoryPanel();
                    return;
                }

                OpenInventoryPanel();
            }

            void ToggleBuffDebugWindow()
            {
                buffDebugWindow.Toggle();
                // Buff 调试窗口使用 IMGUI, 打开时需要显示鼠标并阻断玩家鼠标战斗输入.
                GameplayCursorState.SetDebugPanelOpen(buffDebugWindow.IsVisible);
            }

            void ToggleSettingsPanel()
            {
                if (inventoryOpen || saveSlotOpen)
                {
                    return;
                }

                if (settingsOpen)
                {
                    CloseSettingsPanel();
                    return;
                }

                OpenSettingsPanel();
            }

    void OpenSaveSlotPanel()
    {
        if (saveSlotPanel == null)
        {
            Debug.LogError("打开存档面板失败, SaveSlotPanel未绑定.", this);
            return;
        }

        saveSlotPreviousTimeScale = Time.timeScale;
        saveSlotOpen = true;
        Time.timeScale = 0f;
        GameplayCursorState.SetSaveSlotPanelOpen(true);
        saveSlotPanel.OpenForSafeHouse();
    }

    void OpenInventoryPanel()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("打开背包面板失败, InventoryPanel未绑定.", this);
            return;
        }

        UIStackManager stackManager = UIStackManager.Instance;
        if (stackManager == null)
        {
            return;
        }

        inventoryPreviousTimeScale = Time.timeScale;
        inventoryOpen = true;
        Time.timeScale = 0f;
        GameplayCursorState.SetInventoryPanelOpen(true);
        stackManager.Push(inventoryPanel);
    }

    void CloseSettingsPanel()
    {
        UIStackManager stackManager = UIStackManager.Instance;
        if (stackManager != null && stackManager.Peek() == settingsPanel)
        {
            stackManager.Pop();
        }

        RestoreSettingsCloseState();
    }

    void OpenSettingsPanel()
    {
        if (settingsPanel == null)
        {
            Debug.LogError("打开游戏设置面板失败, SettingsPanel未绑定.", this);
            return;
        }

        UIStackManager stackManager = UIStackManager.Instance;
        if (stackManager == null)
        {
            return;
        }

        previousTimeScale = Time.timeScale;
        settingsOpen = true;
        Time.timeScale = 0f;
        GameplayCursorState.SetSettingsPanelOpen(true);
        stackManager.Push(settingsPanel);
    }
}

        /// <summary>
        /// 绘制 IMGUI 调试界面.
        /// </summary>
        private void OnGUI()
        {
            bool wasDebugVisible = buffDebugWindow.IsVisible;
            buffDebugWindow.Draw(buffDebugDataBase, this);
            if (wasDebugVisible != buffDebugWindow.IsVisible)
            {
                GameplayCursorState.SetDebugPanelOpen(buffDebugWindow.IsVisible);
            }
        }

        /// <summary>
        /// 执行 RestoreSettingsCloseState 逻辑.
        /// </summary>
        private void RestoreSettingsCloseState()
        {
            settingsOpen = false;
            Time.timeScale = previousTimeScale;
            GameplayCursorState.SetSettingsPanelOpen(false);
        }

        /// <summary>
        /// 执行 CloseInventoryPanel 逻辑.
        /// </summary>
        private void CloseInventoryPanel()
        {
            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager != null && stackManager.Peek() == inventoryPanel)
            {
                stackManager.Pop();
            }

            RestoreInventoryCloseState();
        }

        /// <summary>
        /// 执行 RestoreInventoryCloseState 逻辑.
        /// </summary>
        private void RestoreInventoryCloseState()
        {
            inventoryOpen = false;
            Time.timeScale = inventoryPreviousTimeScale;
            GameplayCursorState.SetInventoryPanelOpen(false);
        }

        /// <summary>
        /// 执行 CloseSaveSlotPanel 逻辑.
        /// </summary>
        private void CloseSaveSlotPanel()
        {
            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager != null && stackManager.Peek() == saveSlotPanel)
            {
                stackManager.Pop();
            }

            RestoreSaveSlotCloseState();
        }

        /// <summary>
        /// 执行 RestoreSaveSlotCloseState 逻辑.
        /// </summary>
        private void RestoreSaveSlotCloseState()
        {
            saveSlotOpen = false;
            Time.timeScale = saveSlotPreviousTimeScale;
            GameplayCursorState.SetSaveSlotPanelOpen(false);
        }
    }

    /// <summary>
    /// Buff 调试窗口, 通过 IMGUI 在运行时快速添加或清理玩家 Buff.
    /// </summary>
    internal sealed class BuffDebugWindow
    {
        private const int WindowId = 73001;
        private const float EdgePadding = 16f;
        private const float DragHandleHeight = 24f;
        private const float DefaultWindowWidth = 420f;
        private const float DefaultWindowHeight = 560f;
        private const float MinWindowWidth = 320f;
        private const float MaxWindowWidth = 560f;
        private const float MinWindowHeight = 360f;
        private const float AbsoluteMinWindowWidth = 240f;
        private const float AbsoluteMinWindowHeight = 260f;
        private const float WindowWidthRatio = 0.42f;
        private const float WindowHeightRatio = 0.78f;
        private const float BuffListHeightRatio = 0.44f;
        private const float ActiveListHeightRatio = 0.22f;

        private Rect windowRect = new Rect(24f, 72f, DefaultWindowWidth, DefaultWindowHeight);
        private Vector2 buffScrollPosition;
        private Vector2 activeBuffScrollPosition;
        private string manualBuffId = string.Empty;
        private string statusText = string.Empty;
        private float buffListHeight = 250f;
        private float activeBuffListHeight = 130f;
        private float manualIdWidth = 90f;
        private float actionButtonWidth = 80f;
        private float removeButtonWidth = 70f;

        public bool IsVisible { get; private set; }

        /// <summary>
        /// 执行 SetVisible 逻辑.
        /// </summary>
        public void SetVisible(bool visible)
        {
            IsVisible = visible;
        }

        /// <summary>
        /// 执行 Toggle 逻辑.
        /// </summary>
        public void Toggle()
        {
            SetVisible(!IsVisible);
        }

        /// <summary>
        /// 执行 Draw 逻辑.
        /// </summary>
        public void Draw(BuffDataBase explicitDataBase, Object source)
        {
            if (!IsVisible)
            {
                return;
            }

            RefreshResponsiveLayout();
            windowRect = GUI.Window(WindowId, windowRect, id => DrawWindow(id, explicitDataBase, source), "Buff 添加调试");

            void RefreshResponsiveLayout()
            {
                float screenWidth = Mathf.Max(1f, Screen.width);
                float screenHeight = Mathf.Max(1f, Screen.height);
                float availableWidth = Mathf.Max(AbsoluteMinWindowWidth, screenWidth - EdgePadding * 2f);
                float availableHeight = Mathf.Max(AbsoluteMinWindowHeight, screenHeight - EdgePadding * 2f);
                // 按当前分辨率重新计算窗口尺寸, 避免 Game View 改分辨率后窗口越界.
                windowRect.width = Mathf.Min(Mathf.Clamp(screenWidth * WindowWidthRatio, MinWindowWidth, MaxWindowWidth), availableWidth);
                windowRect.height = Mathf.Min(Mathf.Clamp(screenHeight * WindowHeightRatio, MinWindowHeight, DefaultWindowHeight), availableHeight);
                windowRect.x = Mathf.Clamp(windowRect.x, EdgePadding, Mathf.Max(EdgePadding, screenWidth - windowRect.width - EdgePadding));
                windowRect.y = Mathf.Clamp(windowRect.y, EdgePadding, Mathf.Max(EdgePadding, screenHeight - windowRect.height - EdgePadding));
                // 列表区域随窗口高度变化, 小分辨率下优先保留按钮和状态文本空间.
                buffListHeight = Mathf.Clamp(windowRect.height * BuffListHeightRatio, 120f, 280f);
                activeBuffListHeight = Mathf.Clamp(windowRect.height * ActiveListHeightRatio, 80f, 170f);
                manualIdWidth = Mathf.Clamp(windowRect.width * 0.22f, 72f, 120f);
                actionButtonWidth = Mathf.Clamp(windowRect.width * 0.2f, 72f, 110f);
                removeButtonWidth = Mathf.Clamp(windowRect.width * 0.18f, 60f, 96f);
            }

            void DrawWindow(int windowId, BuffDataBase explicitDataBase, Object source)
            {
                BuffDataBase database = ResolveDatabase(explicitDataBase);
                BuffManager manager = ResolveBuffManager();
                DrawManualAdd(manager, source);
                GUILayout.Space(8f);
                DrawBuffList(database, manager, source);
                GUILayout.Space(8f);
                DrawActiveBuffs(manager);
                GUILayout.Space(8f);
                DrawFooter(manager);
                GUI.DragWindow(new Rect(0f, 0f, windowRect.width, DragHandleHeight));
            }

    static BuffManager ResolveBuffManager()
    {
        return Global.player != null ? Global.player.GetComponent<BuffManager>() : null;
    }

    static BuffDataBase ResolveDatabase(BuffDataBase explicitDataBase)
    {
        return explicitDataBase != null ? explicitDataBase : DataBaseManager.Instance?.Buffs;
    }

    void DrawFooter(BuffManager manager)
    {
        GUILayout.BeginHorizontal();
        GUI.enabled = manager != null;
        if (GUILayout.Button("清空全部"))
        {
            manager.ClearBuffs();
            statusText = "已清空全部 Buff.";
        }

        GUI.enabled = true;
        if (GUILayout.Button("关闭"))
        {
            SetVisible(false);
        }

        GUILayout.EndHorizontal();
        if (!string.IsNullOrWhiteSpace(statusText))
        {
            GUILayout.Label(statusText);
        }
    }

    void DrawActiveBuffs(BuffManager manager)
    {
        GUILayout.Label("当前 Buff");
        if (manager == null)
        {
            GUILayout.Label("Player 缺少 BuffManager.");
            return;
        }

        activeBuffScrollPosition = GUILayout.BeginScrollView(activeBuffScrollPosition, GUILayout.Height(activeBuffListHeight));
        IReadOnlyList<BuffRuntimeInfo> activeBuffs = manager.ActiveBuffs;
        if (activeBuffs.Count == 0)
        {
            GUILayout.Label("暂无 Buff.");
        }

        for (int i = 0; i < activeBuffs.Count; i++)
        {
            BuffRuntimeInfo info = activeBuffs[i];
            if (info?.Buff == null)
            {
                continue;
            }

            GUILayout.BeginHorizontal(GUI.skin.box);
            GUILayout.Label(BuildRuntimeTitle(info));
            if (GUILayout.Button("移除", GUILayout.Width(removeButtonWidth)))
            {
                manager.RemoveBuffById(info.Buff.Id);
                statusText = $"已移除 {info.Buff.BuffName}.";
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }

    void DrawBuffList(BuffDataBase database, BuffManager manager, Object source)
    {
        GUILayout.Label("数据库 Buff");
        if (database == null)
        {
            GUILayout.Label("Buff 数据库未加载.");
            return;
        }

        buffScrollPosition = GUILayout.BeginScrollView(buffScrollPosition, GUILayout.Height(buffListHeight));
        IReadOnlyList<Buff> buffs = database.Buffs;
        for (int i = 0; i < buffs.Count; i++)
        {
            Buff buff = buffs[i];
            if (buff == null)
            {
                continue;
            }

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label(BuildBuffTitle(buff));
            GUILayout.Label(buff.Description);
            GUI.enabled = manager != null;
            if (GUILayout.Button("添加 Buff"))
            {
                TryAddBuff(manager, buff, source);
            }

            GUI.enabled = true;
            GUILayout.EndVertical();
        }

        GUILayout.EndScrollView();
    }

    void DrawManualAdd(BuffManager manager, Object source)
    {
        GUILayout.Label("按 ID 添加");
        GUILayout.BeginHorizontal();
        manualBuffId = GUILayout.TextField(manualBuffId, GUILayout.Width(manualIdWidth));
        GUI.enabled = manager != null;
        if (GUILayout.Button("添加", GUILayout.Width(actionButtonWidth)))
        {
            TryAddBuffById(manager, source);
        }

        GUI.enabled = true;
        GUILayout.EndHorizontal();
    }

    static string BuildRuntimeTitle(BuffRuntimeInfo info)
    {
        string timeText = info.IsPermanent ? $"层数 {info.StackCount}" : $"剩余 {info.RemainingTime:0.0}s";
        return $"[{info.Buff.Id}] {info.Buff.BuffName} | {BuildTagText(info.Buff.Tag)} | {timeText}";
    }

    static string BuildBuffTitle(Buff buff)
    {
        string lifetime = buff.IsPermanent ? "永久" : $"{buff.Duration:0.#}s";
        return $"[{buff.Id}] {buff.BuffName} | {BuildTagText(buff.Tag)} | {lifetime} | 间隔 {buff.Interval:0.#}s";
    }

    void TryAddBuff(BuffManager manager, Buff buff, Object source)
    {
        BuffRuntimeInfo info = manager.AddBuff(buff, source);
        statusText = info != null ? $"已添加 {info.Buff.BuffName}." : $"添加失败, Buff: {buff.BuffName}.";
    }

    void TryAddBuffById(BuffManager manager, Object source)
    {
        if (!int.TryParse(manualBuffId, out int buffId))
        {
            statusText = "Buff ID 格式错误.";
            return;
        }

        BuffRuntimeInfo info = manager.AddBuffById(buffId, source);
        statusText = info != null ? $"已添加 {info.Buff.BuffName}." : $"添加失败, ID: {buffId}.";
    }
}

        /// <summary>
        /// 执行 BuildTagText 逻辑.
        /// </summary>
        private static string BuildTagText(BuffTag tag)
        {
            return tag == BuffTag.Negative ? "负面" : "正面";
        }
    }
}
