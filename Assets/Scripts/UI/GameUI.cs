using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using QFramework;
public class GameUI : ViewController {
    public UnityEngine.UI.Text BulletBagText;
    public UnityEngine.UI.Text BulletText;

    public static GameUI Instance;
    [Header("UI元素")]
    public GameObject WinPanel;
    public GameObject OverPanel;
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
        WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
        OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
    }
    //取消订阅事件
    private void OnDisable() {
        WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
        OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
    }
    //更新HP文本

    public void ResetGame()
    {
        Global.player?.Restart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    
    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ShowOverPanel()
    {
        OverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowMiniMap()
    {
        MiniMap.SetActive(true);
    }
    public void HideMiniMap()
    {
        MiniMap.SetActive(false);
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
}
