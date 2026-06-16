using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 自动生成于: 2026/5/30 0:02:40
	public partial class Panel
	{

		private Button m_Btn_bt1;
		private Button m_Btn_b2;
		private Text m_Txt_t1;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Btn_bt1 = autoBindTool.GetBindComponent<Button>(0);
			m_Btn_b2 = autoBindTool.GetBindComponent<Button>(1);
			m_Txt_t1 = autoBindTool.GetBindComponent<Text>(2);
		}
	}
