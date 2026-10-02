using System;
using System.Linq;
using Game.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 将 GameScene 的旧相机小地图迁移为 HUD 九宫格, 可重复执行且保留原来的显隐绑定.
/// </summary>
public static class RoomGenerationSceneSetup
{
    /// <summary>
    /// 批处理先等待编辑器启动回调完成, 避免 Search 初始化错误被当成游戏运行错误.
    /// </summary>
    public static void VerifyRuntime()
    {
        var readyAt = EditorApplication.timeSinceStartup + 5d;
        EditorApplication.update += WaitForEditor;
        void WaitForEditor()
        {
            if (EditorApplication.timeSinceStartup < readyAt) return;
            EditorApplication.update -= WaitForEditor;
            AddressableRuntimeVerification.Run();
        }
    }

    [MenuItem("Tools/P_GUN/Rooms/Setup Local Minimap")]
    public static void Apply()
    {
        const string scenePath = "Assets/Scenes/GameScene.unity";
        var scene = SceneManager.GetSceneByPath(scenePath);
        var openedHere = !scene.isLoaded;
        if (openedHere) scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
        try
        {
            var objects = scene.GetRootGameObjects().SelectMany(root => root.GetComponentsInChildren<Transform>(true)).ToArray();
            var panel = objects.Single(item => item.name == "MiniMap");
            var oldDisplay = panel.GetComponentInChildren<RawImage>(true);
            LocalMinimapGraphic graphic;
            if (oldDisplay != null)
            {
                var display = oldDisplay.gameObject;
                UnityEngine.Object.DestroyImmediate(oldDisplay);
                graphic = display.AddComponent<LocalMinimapGraphic>();
                display.name = "Img_LocalMinimap";
            }
            else
            {
                graphic = panel.GetComponentInChildren<LocalMinimapGraphic>(true);
                if (graphic == null) throw new InvalidOperationException("MiniMap 缺少原 RawImage 或九宫格组件.");
            }
            // 子图始终填充原 HUD 面板, 消除旧 RenderTexture 的缩放与偏移.
            var rect = graphic.rectTransform;
            rect.localScale = Vector3.one;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            graphic.raycastTarget = false;
            panel.GetComponent<Image>().raycastTarget = false;
            var oldCamera = objects.SingleOrDefault(item => item.name == "MiniMapCamera");
            if (oldCamera != null) UnityEngine.Object.DestroyImmediate(oldCamera.gameObject);
            EditorSceneManager.MarkSceneDirty(scene);
            if (!EditorSceneManager.SaveScene(scene)) throw new InvalidOperationException("GameScene 九宫格配置保存失败.");
            Debug.Log("LOCAL_MINIMAP_SETUP_SUCCESS: 已绑定 HUD 九宫格并移除旧小地图相机.");
        }
        finally
        {
            if (openedHere) EditorSceneManager.CloseScene(scene, true);
        }
    }
}
