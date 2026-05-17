using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{

	public partial class MainPanel : UIPanelBase
	{
        [Header("主菜单面板")]
        [SerializeField] private SettingsPanel settingsPanel;

		protected override void Awake()
		{
            base.Awake();
			GetBindComponents(gameObject);
		}

        void OnEnable()
        {
            m_Btn_Start.onClick.AddListener(StartGame);
            m_Btn_Setting.onClick.AddListener(OpenSettingPanel);
			m_Btn_Quit.onClick.AddListener(ExitGame);
        }

        void OnDisable()
        {
            m_Btn_Start.onClick.RemoveListener(StartGame);
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
		
	}
}
