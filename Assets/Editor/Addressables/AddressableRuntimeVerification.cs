using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Game.Core;
using Game.Gameplay;
using Game.Items;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Build.DataBuilders;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

/// <summary>
/// 批处理验证真实启动与场景流程, 自动跳过更新确认, 不改变玩家存档或更新服务器.
/// </summary>
[InitializeOnLoad]
public static class AddressableRuntimeVerification
{
    private const string RunningKey = "Pgun.KeyVerification.Running";
    private static bool started;
    private static readonly List<string> Errors = new List<string>();

    static AddressableRuntimeVerification()
    {
        if (!SessionState.GetBool(RunningKey, false)) return;
        Application.logMessageReceived += Capture;
        EditorApplication.update += Tick;
        EditorApplication.playModeStateChanged += state =>
        {
            if (state == PlayModeStateChange.EnteredEditMode && SessionState.GetBool(RunningKey, false))
            {
                var exitCode = SessionState.GetInt("Pgun.KeyVerification.Exit", 1);
                if (Errors.Count > 0) { Debug.LogError("ADDRESSABLE_RUNTIME_SHUTDOWN_FAILED: " + Errors[0]); exitCode = 1; }
                if (SessionState.GetBool("Pgun.KeyVerification.Packed", false))
                {
                    AddressableAssetSettingsDefaultObject.Settings.ActivePlayModeDataBuilderIndex = SessionState.GetInt("Pgun.KeyVerification.OriginalBuilder", 0);
                    SessionState.SetBool("Pgun.KeyVerification.Packed", false);
                }
                SessionState.SetBool(RunningKey, false);
                if (exitCode == 0) Debug.Log("ADDRESSABLE_RUNTIME_VERIFICATION_SUCCESS: startup, catalogs, cache, bundles/scenes, dungeon, combat, pools, reload and shutdown.");
                EditorApplication.Exit(exitCode);
            }
        };
    }

    public static void Run()
    {
        if (!EditorApplication.ExecuteMenuItem("XLua/Hotfix Inject In Editor")) throw new InvalidOperationException("xLua injection menu is unavailable.");
        SessionState.SetBool(RunningKey, true);
        EditorSceneManager.OpenScene("Assets/Scenes/Root.unity", OpenSceneMode.Single);
        EditorApplication.isPlaying = true;
    }

    public static void RunPacked()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        SessionState.SetInt("Pgun.KeyVerification.OriginalBuilder", settings.ActivePlayModeDataBuilderIndex);
        var index = settings.DataBuilders.FindIndex(builder => builder is BuildScriptPackedPlayMode);
        if (index < 0) throw new InvalidOperationException("Packed Play Mode builder is missing.");
        settings.ActivePlayModeDataBuilderIndex = index;
        SessionState.SetBool("Pgun.KeyVerification.Packed", true);
        Run();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void UseBuiltBundlesForVerification()
    {
        if (!SessionState.GetBool(RunningKey, false) || !SessionState.GetBool("Pgun.KeyVerification.Packed", false)) return;
        // 仅验证进程把远程资源 URL 映射到本次构建文件, 不修改项目服务器配置或上传资源.
        const string remote = "https://achen1o1.xyz/AB/P_GUN/";
        var cache = Path.GetFullPath("Library/AddressableKeyVerificationCache/" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(cache);
        Addressables.InternalIdTransformFunc = location =>
        {
            var id = location.InternalId.Replace('\\', '/');
            if (id.StartsWith(remote, StringComparison.Ordinal)) return new Uri(Path.GetFullPath("ServerData/P_GUN/" + id.Substring(remote.Length))).AbsoluteUri;
            // 隔离旧版本 Catalog 缓存, 测试不读取或改写玩家缓存与存档.
            if (id.Contains("/com.unity.addressables/catalog_")) return Path.Combine(cache, Path.GetFileName(id));
            return location.InternalId;
        };
        Debug.Log("ADDRESSABLE_PACKED_VERIFICATION: using built bundles with local remote-path mapping.");
    }

    private static void Capture(string message, string trace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert) Errors.Add(message + "\n" + trace);
    }

    private static void Tick()
    {
        if (!EditorApplication.isPlaying || EditorApplication.isPaused) return;
        var boot = UnityEngine.Object.FindFirstObjectByType<RootHotUpdateController>();
        if (boot != null && typeof(RootHotUpdateController).GetField("updatePromptCompletion", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(boot) != null)
            typeof(RootHotUpdateController).GetMethod("HandleUpdateSkipped", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(boot, null);
        if (started) return;
        started = true;
        _ = VerifyAsync();
    }

    private static async Task WaitUntil(Func<bool> predicate, string description)
    {
        var deadline = DateTime.UtcNow.AddSeconds(120);
        while (!predicate())
        {
            if (Errors.Count > 0) throw new InvalidOperationException(Errors[0]);
            if (DateTime.UtcNow > deadline) throw new TimeoutException(description);
            await Task.Delay(100);
        }
    }

    private static async Task VerifyAsync()
    {
        try
        {
            await WaitUntil(() => SceneManager.GetActiveScene().name == "StartScene" && DataBaseManager.Instance != null && DataBaseManager.Instance.IsLoaded, "Root startup did not finish.");
            var loader = AddressableLoader.Instance;
            var first = loader.LoadAssetAsync<Sprite>("Potion_Round_Red");
            var second = loader.LoadAssetAsync<Sprite>("Potion_Round_Red");
            await Task.WhenAll(first, second);
            if (!ReferenceEquals(first.Result, second.Result)) throw new InvalidOperationException("Concurrent requests did not share their asset.");
            try { await loader.LoadAssetAsync<Sprite>("__missing_test_key__"); throw new InvalidOperationException("Unknown key did not fail."); }
            catch (System.Collections.Generic.KeyNotFoundException) { }
            loader.Release<Sprite>("Potion_Round_Red");
            await loader.LoadAssetAsync<Sprite>("Potion_Round_Red");
            await loader.LoadSceneAsync("GameScene");
            await WaitUntil(() => PlayerRegistry.Current != null && PlayerRegistry.Current.gun != null && Game.Gameplay.Room.ActiveRooms.Count > 0, "Dungeon/player/weapon initialization did not finish.");
            var player = PlayerRegistry.Current;
            player.gun.Shoot(Vector2.right);
            var info = player.buffManager.AddBuffById(0);
            if (info == null) throw new InvalidOperationException("Buff Lua was not created.");
            player.buffManager.RemoveBuffById(0);
            var item = ItemPool.Instance.Get("Heart", player.transform.position, Quaternion.identity);
            if (item.ItemId != 1) throw new InvalidOperationException("Item key resolved to wrong prefab.");
            ItemPool.Instance.Release(item);
            await Task.Delay(500);
            await loader.LoadSceneAsync("StartScene");
            await loader.LoadSceneAsync("GameScene");
            await WaitUntil(() => PlayerRegistry.Current != null && PlayerRegistry.Current.gun != null, "Second gameplay initialization did not finish.");
            await loader.ReloadSceneAsync("GameScene");
            await WaitUntil(() => PlayerRegistry.Current != null && PlayerRegistry.Current.gun != null, "Gameplay reload did not finish.");
            await Task.Delay(300);
            if (Errors.Count > 0) throw new InvalidOperationException(Errors[0]);
            Debug.Log("ADDRESSABLE_RUNTIME_FLOW_FINISHED: Root, Catalogs, concurrent loading, cache release, scene transitions, dungeon, shooting, Buff Lua, item pooling, reload.");
            SessionState.SetInt("Pgun.KeyVerification.Exit", 0);
        }
        catch (Exception exception)
        {
            Debug.LogError("ADDRESSABLE_RUNTIME_VERIFICATION_FAILED: " + exception);
            SessionState.SetInt("Pgun.KeyVerification.Exit", 1);
        }
        finally { EditorApplication.isPlaying = false; }
    }

    public static void BuildContent()
    {
        AddressableCatalogSetup.ConfigureCatalogVersion();
        AddressableCatalogSetup.Validate();
        UnityEditor.AddressableAssets.Settings.AddressableAssetSettings.BuildPlayerContent(out AddressablesPlayerBuildResult result);
        if (!string.IsNullOrEmpty(result.Error)) throw new InvalidOperationException(result.Error);
        Debug.Log("ADDRESSABLE_CONTENT_BUILD_SUCCESS: " + result.OutputPath);
    }

    public static void GenerateLuaBridges()
    {
        if (!EditorApplication.ExecuteMenuItem("XLua/Generate Code")) throw new InvalidOperationException("xLua generation menu is unavailable.");
    }

    public static void RestoreAssetDatabasePlayMode()
    {
        // 无图形测试崩溃可能中断恢复, 手动收尾时恢复项目原有的 AssetDatabase 播放模式.
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        settings.ActivePlayModeDataBuilderIndex = settings.DataBuilders.FindIndex(builder => builder is BuildScriptFastMode);
    }
}
