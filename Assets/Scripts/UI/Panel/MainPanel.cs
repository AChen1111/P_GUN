using UnityEngine;

namespace Game.UI
{

	public partial class MainPanel : MonoBehaviour
	{
		private void Awake()
		{
			GetBindComponents(gameObject);
		}
	}
}
