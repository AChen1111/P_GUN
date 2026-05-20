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
                EventCenter.AddListener(GameEvent.GameWin, ShowWinPanel);
                EventCenter.AddListener(GameEvent.PlayerDied, ShowOverPanel);
                EventCenter.AddListener(GameEvent.MiniMapToggleRequested, SwicthMinMapState);
                EventCenter.AddListener<ItemData>(GameEvent.ItemTipShown, ShowItemTip);
                EventCenter.AddListener(GameEvent.ItemTipHidden, HideItemTip);
                EventCenter.AddListener<GunClip>(GameEvent.BulletClipChanged, UpdateBulletText);
                EventCenter.AddListener<BulletBag>(GameEvent.BulletBagChanged, UpdateBulletBagText);
                EventCenter.AddListener<RoomWaveDisplayEvent>(GameEvent.RoomWaveDisplayChanged, UpdateWaveText);
                EventCenter.AddListener<PlayerHeadMessageEvent>(GameEvent.PlayerHeadMessageRequested, OnPlayerHeadMessageRequested);
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

        /// <summary>
        /// 执行 ResetGame 逻辑.
        /// </summary>
        public void ResetGame()
        {
            Global.player?.Restart();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        /// <summary>
        /// 执行 ReturnToMainMenu 逻辑.
        /// </summary>
        public void ReturnToMainMenu()
        {
            // 返回主菜单前恢复时间流速, 避免主菜单继承游戏结束暂停状态.
            Time.timeScale = 1;
            SceneManager.LoadScene("StartScene");
        }

        /// <summary>
        /// 执行 ShowWinPanel 逻辑.
        /// </summary>
        public void ShowWinPanel()
        {
            PushStackPanel(WinPanel, ref winStackPanel);
            Time.timeScale = 0;
        }
        /// <summary>
        /// 执行 ShowOverPanel 逻辑.
        /// </summary>
        public void ShowOverPanel()
        {
            PushStackPanel(OverPanel, ref overStackPanel);
            Time.timeScale = 0;
        }

        /// <summary>
        /// 执行 ShowMiniMap 逻辑.
        /// </summary>
        public void ShowMiniMap()
        {
            MiniMap.SetActive(true);
            EventCenter.Trigger(GameEvent.MiniMapShown);
        }
        /// <summary>
        /// 执行 HideMiniMap 逻辑.
        /// </summary>
        public void HideMiniMap()
        {
            MiniMap.SetActive(false);
            EventCenter.Trigger(GameEvent.MiniMapHidden);
        }

        /// <summary>
        /// 执行 SwicthMinMapState 逻辑.
        /// </summary>
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

        /// <summary>
        /// 执行 UpdateWaveText 逻辑.
        /// </summary>
        public void UpdateWaveText(RoomWaveDisplayEvent payload)
        {
            SetWaveTextVisible(payload.IsVisible);
            if (!payload.IsVisible || WaveText == null) return;

            // 文案集中在 UI 层, 房间系统只负责提供波数数据.
            WaveText.text = $"当前波数 {payload.CurrentWave}/{payload.TotalWave}";
        }

        /// <summary>
        /// 执行 ShowMessageOnPlayerHead 逻辑.
        /// </summary>
        public void ShowMessageOnPlayerHead(string message,float duration)
        {
            Global.player?.ShowDisPlayer(message, duration);
        }

        /// <summary>
        /// 执行 ShowItemTip 逻辑.
        /// </summary>
        public void ShowItemTip(ItemData data)
        {
            ResolveItemTipPanel();
            itemTipPanel?.Show(data);
        }

        /// <summary>
        /// 执行 HideItemTip 逻辑.
        /// </summary>
        public void HideItemTip()
        {
            ResolveItemTipPanel();
            itemTipPanel?.Hide();
        }

        /// <summary>
        /// 执行 ResolveItemTipPanel 逻辑.
        /// </summary>
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

        /// <summary>
        /// 执行 PushStackPanel 逻辑.
        /// </summary>
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

        /// <summary>
        /// 执行 RemoveButtonListeners 逻辑.
        /// </summary>
        private void RemoveButtonListeners()
        {
            WinPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            OverPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            WinPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.RemoveListener(ReturnToMainMenu);
            OverPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.RemoveListener(ReturnToMainMenu);
        }

        /// <summary>
        /// 执行 RemoveEventListeners 逻辑.
        /// </summary>
        private void RemoveEventListeners()
        {
            EventCenter.RemoveListener(GameEvent.GameWin, ShowWinPanel);
            EventCenter.RemoveListener(GameEvent.PlayerDied, ShowOverPanel);
            EventCenter.RemoveListener(GameEvent.MiniMapToggleRequested, SwicthMinMapState);
            EventCenter.RemoveListener<ItemData>(GameEvent.ItemTipShown, ShowItemTip);
            EventCenter.RemoveListener(GameEvent.ItemTipHidden, HideItemTip);
            EventCenter.RemoveListener<GunClip>(GameEvent.BulletClipChanged, UpdateBulletText);
            EventCenter.RemoveListener<BulletBag>(GameEvent.BulletBagChanged, UpdateBulletBagText);
            EventCenter.RemoveListener<RoomWaveDisplayEvent>(GameEvent.RoomWaveDisplayChanged, UpdateWaveText);
            EventCenter.RemoveListener<PlayerHeadMessageEvent>(GameEvent.PlayerHeadMessageRequested, OnPlayerHeadMessageRequested);
        }

        /// <summary>
        /// 执行 SetWaveTextVisible 逻辑.
        /// </summary>
        private void SetWaveTextVisible(bool isVisible)
        {
            if (WaveText != null)
            {
                WaveText.gameObject.SetActive(isVisible);
            }
        }

        /// <summary>
        /// 执行 OnPlayerHeadMessageRequested 逻辑.
        /// </summary>
        private void OnPlayerHeadMessageRequested(PlayerHeadMessageEvent payload)
        {
            ShowMessageOnPlayerHead(payload.Message, payload.Duration);
        }
    }
}
