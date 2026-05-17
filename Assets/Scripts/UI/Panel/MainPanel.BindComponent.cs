using UnityEngine;
using UnityEngine.UI;

// 自动生成于: 2026/5/17 17:44:48
namespace Game.UI
{

	public partial class MainPanel
	{

		private Button m_Btn_Start;
		private Button m_Btn_Load;
		private Button m_Btn_Setting;
		private Button m_Btn_Quit;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Btn_Start = autoBindTool.GetBindComponent<Button>(0);
			m_Btn_Load = autoBindTool.GetBindComponent<Button>(1);
			m_Btn_Setting = autoBindTool.GetBindComponent<Button>(2);
			m_Btn_Quit = autoBindTool.GetBindComponent<Button>(3);
		}
	}
}
