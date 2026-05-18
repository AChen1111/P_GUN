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
        private void OnEnable() {
            AddButtonListeners();
            AddEventListeners();
        }

        //取消订阅事件
        private void OnDisable() {
            RemoveButtonListeners();
            RemoveEventListeners();
        }

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
            Global.player?.Restart();
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
            EventCenter.Trigger(GameEvent.MiniMapShown);
        }
        public void HideMiniMap()
        {
            MiniMap.SetActive(false);
            EventCenter.Trigger(GameEvent.MiniMapHidden);
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
            Global.player?.ShowDisPlayer(message, duration);
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
                itemTipPanel = ItemTipPanel.CreateDefault(transform);
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

        private void AddButtonListeners()
        {
            WinPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.AddListener(ResetGame);
            OverPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.AddListener(ResetGame);
            WinPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
            OverPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
        }

        private void RemoveButtonListeners()
        {
            WinPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            OverPanel.transform.Find("Btn_Reset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            WinPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.RemoveListener(ReturnToMainMenu);
            OverPanel.transform.Find("Btn_MainMenu").GetComponent<Button>().onClick.RemoveListener(ReturnToMainMenu);
        }

        private void AddEventListeners()
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
