using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 自动生成于: 2026/5/17 18:55:59
namespace Game.UI
{

	public partial class SettingPanel
	{

		private Toggle m_Tog_Display;
		private Toggle m_Tog_Audio;
		private RectTransform m_Rect_DisplayPage;
		private TMP_Dropdown m_Drop_Resolution;
		private Toggle m_Tog_FullScreen;
		private Toggle m_Tog_VSync;
		private TMP_Dropdown m_Drop_FrameRate;
		private Button m_Btn_Apply;
		private Button m_Btn_Back;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Tog_Display = autoBindTool.GetBindComponent<Toggle>(0);
			m_Tog_Audio = autoBindTool.GetBindComponent<Toggle>(1);
			m_Rect_DisplayPage = autoBindTool.GetBindComponent<RectTransform>(2);
			m_Drop_Resolution = autoBindTool.GetBindComponent<TMP_Dropdown>(3);
			m_Tog_FullScreen = autoBindTool.GetBindComponent<Toggle>(4);
			m_Tog_VSync = autoBindTool.GetBindComponent<Toggle>(5);
			m_Drop_FrameRate = autoBindTool.GetBindComponent<TMP_Dropdown>(6);
			m_Btn_Apply = autoBindTool.GetBindComponent<Button>(7);
			m_Btn_Back = autoBindTool.GetBindComponent<Button>(8);
		}
	}
}
