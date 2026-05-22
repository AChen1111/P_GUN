using System;
using System.Threading.Tasks;
using Game.Gameplay;
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
        private bool isStartingGame;

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
        public async void StartGame()
		{
            if (isStartingGame)
            {
                return;
            }

            isStartingGame = true;
            try
            {
                await EnsureDatabasesLoadedAsync();
			    SceneManager.LoadScene("GameScene");
            }
            catch (Exception exception)
            {
                isStartingGame = false;
                Debug.LogError($"{nameof(MainPanel)}: 进入游戏失败, Error: {exception.Message}", this);
                throw;
            }
		}

        /// <summary>
        /// 进入 GameScene 前加载全局数据库.
        /// </summary>
        private static Task EnsureDatabasesLoadedAsync()
        {
            var manager = DataBaseManager.Instance;
            if (manager == null)
            {
                throw new InvalidOperationException($"{nameof(DataBaseManager)} must exist before entering GameScene.");
            }

            return manager.EnsureLoadedAsync();
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
