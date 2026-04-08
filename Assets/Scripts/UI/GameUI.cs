using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using QFramework;
namespace QFramework.PG {
    public partial class GameUI : ViewController {
        public static GameUI Instance;
        [Header("UI元素")]
        public GameObject WinPanel;
        public GameObject OverPanel;
        public Text HPText;

        private void Awake() {
            Instance = this;
            WinPanel = transform.Find("WinPanel").gameObject;
            OverPanel = transform.Find("OverPanel").gameObject;
            //HPText = transform.Find("HPText").GetComponent<Text>();
            UpdateHPText();
        }
        //订阅事件
        private void OnEnable() {
            WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
            OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
            Global.OnHPChange += UpdateHPText;
        }
        //取消订阅事件
        private void OnDisable() {
            WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
            Global.OnHPChange -= UpdateHPText;
        }
        //更新HP文本
        private void UpdateHPText() {
            HPText.text = "HP: " + Global.HP;
        }

        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Global.Restart();
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
                BulletBugText.gameObject.SetActive(false);
                return;
            }
            BulletBugText.gameObject.SetActive(true);
            string text = $"({bulletBag.currentBullet}/{bulletBag.maxBullet})";
            BulletBugText.text = text;
        }
    }
}