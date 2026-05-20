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

		/// <summary>
		/// 初始化运行时依赖.
		/// </summary>
		protected override void Awake()
		{
            base.Awake();
			GetBindComponents(gameObject);
		}

        /// <summary>
        /// 注册启用时需要的监听.
        /// </summary>
        void OnEnable()
        {
            m_Btn_Start.onClick.AddListener(StartGame);
            m_Btn_Load.onClick.AddListener(OpenLoadPanel);
            m_Btn_Setting.onClick.AddListener(OpenSettingPanel);
			m_Btn_Quit.onClick.AddListener(ExitGame);
        }

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        void OnDisable()
        {
            m_Btn_Start.onClick.RemoveListener(StartGame);
            m_Btn_Load.onClick.RemoveListener(OpenLoadPanel);
            m_Btn_Setting.onClick.RemoveListener(OpenSettingPanel);
			m_Btn_Quit.onClick.RemoveListener(ExitGame);
        }

        /// <summary>
        /// 执行 StartGame 逻辑.
        /// </summary>
        public void StartGame()
		{
			SceneManager.LoadScene("GameScene");
		}
		/// <summary>
		/// 执行 ExitGame 逻辑.
		/// </summary>
		public void ExitGame()
		{
			Application.Quit();
		}
		/// <summary>
		/// 执行 OpenSettingPanel 逻辑.
		/// </summary>
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

        /// <summary>
        /// 执行 OpenLoadPanel 逻辑.
        /// </summary>
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
