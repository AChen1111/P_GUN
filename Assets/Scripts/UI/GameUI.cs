using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using QFramework;
using Game.Core;
using Game.Items;
using Game.Gameplay;

namespace Game.UI
{
    public class GameUI : ViewController {
        public UnityEngine.UI.Text BulletBagText;
        public UnityEngine.UI.Text BulletText;
        public UnityEngine.UI.Text WaveText;

        public static GameUI Instance;
        [Header("UI元素")]
        public GameObject WinPanel;
        public GameObject OverPanel;
        [SerializeField] private UIPanelBase winStackPanel;
        [SerializeField] private UIPanelBase overStackPanel;
        [Header("小地图")]
        public GameObject MiniMap;
        [Header("物品提示")]
        [SerializeField] private ItemTipPanel itemTipPanel;


        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake() {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            ResolveItemTipPanel();
            SetWaveTextVisible(false);
        }

        //订阅事件
        /// <summary>
        /// 注册启用时需要的监听.
        /// </summary>
        private void OnEnable() {
            AddButtonListeners();
            AddEventListeners();

            void AddEventListeners()
            {
                EventCenter.AddListener(CoreEvents.GameWin, ShowWinPanel);
                EventCenter.AddListener(CoreEvents.PlayerDied, ShowOverPanel);
                EventCenter.AddListener(CoreEvents.MiniMapToggleRequested, SwicthMinMapState);
                EventCenter.AddListener(ItemEvents.ItemTipShown, ShowItemTip);
                EventCenter.AddListener(ItemEvents.ItemTipHidden, HideItemTip);
                EventCenter.AddListener(GameplayEvents.BulletClipChanged, UpdateBulletText);
                EventCenter.AddListener(GameplayEvents.BulletBagChanged, UpdateBulletBagText);
                EventCenter.AddListener(GameplayEvents.RoomWaveDisplayChanged, UpdateWaveText);
                EventCenter.AddListener(CoreEvents.PlayerHeadMessageRequested, OnPlayerHeadMessageRequested);
            }

            void AddButtonListeners()
            {
                WinPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.AddListener(ResetGame);
                OverPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.AddListener(ResetGame);
                WinPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
                OverPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
            }
}

        //取消订阅事件
        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable() {
            RemoveButtonListeners();
            RemoveEventListeners();
        }

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        private void OnDestroy()
        {
            RemoveButtonListeners();
            RemoveEventListeners();

            if (Instance == this)
            {
                Instance = null;
            }
        }
        public void ResetGame()
        {
            PlayerRegistry.Current?.Restart();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
        public void ReturnToMainMenu()
        {
            // 返回主菜单前恢复时间流速, 避免主菜单继承游戏结束暂停状态.
            Time.timeScale = 1;
            SceneManager.LoadScene("StartScene");
        }
        public void ShowWinPanel()
        {
            PushStackPanel(WinPanel, ref winStackPanel);
            Time.timeScale = 0;
        }
        public void ShowOverPanel()
        {
            PushStackPanel(OverPanel, ref overStackPanel);
            Time.timeScale = 0;
        }
        public void ShowMiniMap()
        {
            MiniMap.SetActive(true);
            EventCenter.Trigger(CoreEvents.MiniMapShown);
        }
        public void HideMiniMap()
        {
            MiniMap.SetActive(false);
            EventCenter.Trigger(CoreEvents.MiniMapHidden);
        }
        public void SwicthMinMapState()
        {
            if(MiniMap.activeSelf)
            {
                HideMiniMap();
            }
            else
            {
                ShowMiniMap();
            }
        }

        ///<summary>
        ///更新弹药文本
        ///</summary>
        ///<param name="gunClip">枪弹夹</param>
        public void UpdateBulletText(GunClip gunClip)
        {
            BulletText.gameObject.SetActive(true);
            if(gunClip.maxAmmo == -1)
            {
                BulletText.text = "Bullet: ∞";
                return;
            }
            string text = $"Bullet: {gunClip.currentAmmo}/{gunClip.maxAmmo}";
            BulletText.text = text;
        }


        ///<summary>
        ///更新备弹弹药文本
        ///</summary>
        ///<param name="bulletBag">子弹袋</param>
        public void UpdateBulletBagText(BulletBag bulletBag)
        {
            ///如果子弹袋最大子弹数为-1，则不显示
            if(bulletBag.maxBullet == -1)
            {
                BulletBagText.gameObject.SetActive(false);
                return;
            }
            BulletBagText.gameObject.SetActive(true);
            string text = $"({bulletBag.currentBullet}/{bulletBag.maxBullet})";
            BulletBagText.text = text;
        }
        public void UpdateWaveText(RoomWaveDisplayEvent payload)
        {
            SetWaveTextVisible(payload.IsVisible);
            if (!payload.IsVisible || WaveText == null) return;

            // 文案集中在 UI 层, 房间系统只负责提供波数数据.
            WaveText.text = $"当前波数 {payload.CurrentWave}/{payload.TotalWave}";
        }
        public void ShowMessageOnPlayerHead(string message,float duration)
        {
            PlayerRegistry.Current?.ShowDisPlayer(message, duration);
        }
        public void ShowItemTip(ItemData data)
        {
            ResolveItemTipPanel();
            itemTipPanel?.Show(data);
        }
        public void HideItemTip()
        {
            ResolveItemTipPanel();
            itemTipPanel?.Hide();
        }
        private void ResolveItemTipPanel()
        {
            if (itemTipPanel == null)
            {
                itemTipPanel = GetComponentInChildren<ItemTipPanel>(true);
            }

            if (itemTipPanel == null)
            {
                throw new InvalidOperationException($"{nameof(GameUI)} requires {nameof(ItemTipPanel)} binding.");
            }
        }
        private void PushStackPanel(GameObject panelObject, ref UIPanelBase cachedPanel)
        {
            if (panelObject == null)
            {
                Debug.LogError("打开栈式面板失败, 面板对象未绑定.", this);
                return;
            }

            if (cachedPanel == null && !panelObject.TryGetComponent(out cachedPanel))
            {
                Debug.LogError($"打开栈式面板失败, {panelObject.name} 缺少 UIPanelBase 组件.", panelObject);
                return;
            }

            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager == null)
            {
                return;
            }

            // 胜利和失败面板统一作为栈式弹窗打开.
            if (stackManager.Peek() != cachedPanel)
            {
                stackManager.Push(cachedPanel);
            }
        }
        private void RemoveButtonListeners()
        {
            WinPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            OverPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            WinPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.RemoveListener(ReturnToMainMenu);
            OverPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.RemoveListener(ReturnToMainMenu);
        }
        private void RemoveEventListeners()
        {
            EventCenter.RemoveListener(CoreEvents.GameWin, ShowWinPanel);
            EventCenter.RemoveListener(CoreEvents.PlayerDied, ShowOverPanel);
            EventCenter.RemoveListener(CoreEvents.MiniMapToggleRequested, SwicthMinMapState);
            EventCenter.RemoveListener(ItemEvents.ItemTipShown, ShowItemTip);
            EventCenter.RemoveListener(ItemEvents.ItemTipHidden, HideItemTip);
            EventCenter.RemoveListener(GameplayEvents.BulletClipChanged, UpdateBulletText);
            EventCenter.RemoveListener(GameplayEvents.BulletBagChanged, UpdateBulletBagText);
            EventCenter.RemoveListener(GameplayEvents.RoomWaveDisplayChanged, UpdateWaveText);
            EventCenter.RemoveListener(CoreEvents.PlayerHeadMessageRequested, OnPlayerHeadMessageRequested);
        }
        private void SetWaveTextVisible(bool isVisible)
        {
            if (WaveText != null)
            {
                WaveText.gameObject.SetActive(isVisible);
            }
        }
        private void OnPlayerHeadMessageRequested(PlayerHeadMessageEvent payload)
        {
            ShowMessageOnPlayerHead(payload.Message, payload.Duration);
        }
    }
}
