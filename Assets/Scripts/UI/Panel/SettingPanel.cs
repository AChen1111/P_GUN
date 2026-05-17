using UnityEngine;

namespace Game.UI
{

	public partial class SettingPanel : UIPanelBase
	{
		protected override void Awake()
		{
			base.Awake();
			GetBindComponents(gameObject);
		}
	}
}
