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
            WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
            OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
        }

        private void RemoveButtonListeners()
        {
            WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
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
            EventCenter.RemoveListener<PlayerHeadMessageEvent>(GameEvent.PlayerHeadMessageRequested, OnPlayerHeadMessageRequested);
        }

        private void OnPlayerHeadMessageRequested(PlayerHeadMessageEvent payload)
        {
            ShowMessageOnPlayerHead(payload.Message, payload.Duration);
        }
    }
}
