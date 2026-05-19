using UnityEngine;
using UnityEngine.SceneManagement;
using Game.UI.Save;

namespace Game.UI
{

	public partial class MainPanel : UIPanelBase
	{
        [Header("主菜单面板")]
        [SerializeField] private SettingsPanel settingsPanel;
        [SerializeField] private SaveSlotPanel saveSlotPanel;

		protected override void Awake()
		{
            base.Awake();
			GetBindComponents(gameObject);
		}

        void OnEnable()
        {
            m_Btn_Start.onClick.AddListener(StartGame);
            m_Btn_Load.onClick.AddListener(OpenLoadPanel);
            m_Btn_Setting.onClick.AddListener(OpenSettingPanel);
			m_Btn_Quit.onClick.AddListener(ExitGame);
        }

        void OnDisable()
        {
            m_Btn_Start.onClick.RemoveListener(StartGame);
            m_Btn_Load.onClick.RemoveListener(OpenLoadPanel);
            m_Btn_Setting.onClick.RemoveListener(OpenSettingPanel);
			m_Btn_Quit.onClick.RemoveListener(ExitGame);
        }

        public void StartGame()
		{
			SceneManager.LoadScene("GameScene");
		}
		public void ExitGame()
		{
			Application.Quit();
		}
		public void OpenSettingPanel()
		{
            if (settingsPanel == null)
            {
                Debug.LogError("打开设置面板失败, SettingsPanel未绑定.");
                return;
            }

            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager == null)
            {
                return;
            }

            // 设置界面作为主菜单上的栈式弹窗打开.
            stackManager.Push(settingsPanel);
		}

        public void OpenLoadPanel()
        {
            if (saveSlotPanel == null)
            {
                Debug.LogError("打开读档面板失败, SaveSlotPanel未绑定.");
                return;
            }

            // 主菜单只开放读档和删除, 不允许保存空进度.
            saveSlotPanel.OpenForMainMenu();
        }
		
	}
}
