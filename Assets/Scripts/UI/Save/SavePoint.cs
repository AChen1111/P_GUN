using Game.Gameplay;
using UnityEngine;

namespace Game.UI.Save
{
    /// <summary>
    /// 场景存档点交互入口, 预制体只查找场景 UI 控制器, 不直接引用存档面板.
    /// </summary>
    public class SavePoint : MonoBehaviour
    {
        [SerializeField] private GameObject btn;
        [SerializeField] private AudioPlay audioPlay;

        private GameSceneUIInputController uiInputController;
        private bool playerInRange;

        /// <summary>
        /// 初始化存档点依赖.
        /// </summary>
        private void Awake()
        {
            ResolveAudioPlay();
        }

        /// <summary>
        /// 执行每帧交互检测.
        /// </summary>
        private void Update()
        {
            if (!playerInRange || !Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            ResolveUiInputController().OpenSaveSlotPanelFromWorld();
        }

        /// <summary>
        /// 玩家进入交互范围时显示提示.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            playerInRange = true;
            audioPlay?.Play();
            if (btn != null)
            {
                btn.SetActive(true);
            }
        }

        /// <summary>
        /// 玩家离开交互范围时隐藏提示.
        /// </summary>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            playerInRange = false;
            if (btn != null)
            {
                btn.SetActive(false);
            }
        }

        /// <summary>
        /// 查找场景中的游戏 UI 输入控制器.
        /// </summary>
        private GameSceneUIInputController ResolveUiInputController()
        {
            if (uiInputController != null)
            {
                return uiInputController;
            }

            uiInputController = FindObjectOfType<GameSceneUIInputController>(true);
            if (uiInputController == null)
            {
                throw new System.InvalidOperationException($"{nameof(SavePoint)} requires {nameof(GameSceneUIInputController)} in scene.");
            }

            return uiInputController;
        }

        /// <summary>
        /// 查找存档点上的音效播放组件.
        /// </summary>
        private void ResolveAudioPlay()
        {
            if (audioPlay != null)
            {
                return;
            }

            audioPlay = GetComponent<AudioPlay>();
            if (audioPlay == null)
            {
                audioPlay = GetComponentInChildren<AudioPlay>();
            }
        }
    }
}
