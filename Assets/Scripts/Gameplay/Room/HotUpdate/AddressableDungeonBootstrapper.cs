using System.Collections;
using Edgar.Unity;
using Game.Core;
using Game.Gameplay.Save;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// GameScene 房间生成入口, 将 Addressables 中的 LevelGraph 注入 Edgar 生成器.
    /// </summary>
    public sealed class AddressableDungeonBootstrapper : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorGrid2D dungeonGenerator;
        [SerializeField] private string levelGraphAddress = "room/level1";
        [SerializeField] private bool generateOnStart = true;

        private bool generated;
        private int lastGeneratedSeed;

        public string LevelGraphAddress => levelGraphAddress;
        public int LastGeneratedSeed => lastGeneratedSeed;

        private void Awake()
        {
            ResolveGenerator();

            if (dungeonGenerator != null)
            {
                // 关卡图必须等 Root 热更新流程完成后再赋值, 因此禁止 Edgar 自己在 Awake/Start 生成.
                dungeonGenerator.GenerateOn = GenerateOn.Manually;
            }
        }

        private void Start()
        {
            if (generateOnStart)
            {
                Generate();
            }
        }

        public void Generate()
        {
            if (generated) return;

            ResolveGenerator();
            if (dungeonGenerator == null)
            {
                Debug.LogError($"{nameof(AddressableDungeonBootstrapper)}: DungeonGeneratorGrid2D 未绑定.", this);
                return;
            }

            SaveGameService.ApplyPendingGenerationSettings(this, dungeonGenerator);
            ApplyAddressableLevelGraph();
            if (dungeonGenerator.FixedLevelGraphConfig.LevelGraph == null)
            {
                Debug.LogError($"{nameof(AddressableDungeonBootstrapper)}: LevelGraph 未加载, 无法生成房间.", this);
                return;
            }

            generated = true;
            var payload = dungeonGenerator.Generate() as DungeonGeneratorPayloadGrid2D;
            // 保存 Edgar 实际使用的 seed, 读档时用它重建同一张地图.
            lastGeneratedSeed = payload?.GeneratedLevel != null ? payload.GeneratedLevel.Seed : dungeonGenerator.RandomGeneratorSeed;
            StartCoroutine(RestorePendingSaveNextFrame());
        }

        public void OverrideLevelGraphAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return;

            levelGraphAddress = address;
        }

        private void ResolveGenerator()
        {
            if (dungeonGenerator == null)
            {
                dungeonGenerator = GetComponent<DungeonGeneratorGrid2D>();
            }
        }

        private void ApplyAddressableLevelGraph()
        {
            var content = AddressableRuntimeContent.Instance;
            if (content == null)
            {
                // 允许从 GameScene 直接 Play, 此时继续使用 Inspector 中的本地 LevelGraph.
                Debug.LogWarning($"{nameof(AddressableDungeonBootstrapper)}: 找不到 AddressableRuntimeContent, 使用场景内 LevelGraph.", this);
                return;
            }

            if (content.TryGetAsset<LevelGraph>(levelGraphAddress, out var levelGraph))
            {
                dungeonGenerator.FixedLevelGraphConfig.LevelGraph = levelGraph;
                return;
            }

            Debug.LogError($"{nameof(AddressableDungeonBootstrapper)}: 找不到已预加载的 LevelGraph, Address: {levelGraphAddress}.", this);
        }

        private IEnumerator RestorePendingSaveNextFrame()
        {
            // 房间实例的 Start 会在生成后一帧执行, 等门和房间初始化完成后再覆盖存档状态.
            yield return null;
            SaveGameService.TryRestorePendingSave();
        }
    }
}
