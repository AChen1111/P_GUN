using UnityEngine;

namespace Game.UI
{

	public partial class SettingPanel : UIPanelBase
	{
		private void Awake()
		{
			GetBindComponents(gameObject);
		}
	}
}
