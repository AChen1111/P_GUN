using System.Collections;
using System;
using System.Threading.Tasks;
using Edgar.Unity;
using Game.Core;
using Game.Gameplay.Save;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// GameScene 房间生成入口, 将 Addressables 中的 LevelGraph 注入 Edgar 生成器.
    /// </summary>
    [RequireComponent(typeof(DungeonGeneratorGrid2D))]
    public sealed class AddressableDungeonBootstrapper : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorGrid2D dungeonGenerator;
        [SerializeField] private string levelGraphAddress = "room/level1";
        [SerializeField] private bool generateOnStart = true;

        public static AddressableDungeonBootstrapper Active { get; private set; }

        private bool generated;
        private bool isGenerating;
        private int lastGeneratedSeed;

        public string LevelGraphAddress => levelGraphAddress;
        public int LastGeneratedSeed => lastGeneratedSeed;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            if (Active != null && Active != this)
            {
                throw new System.InvalidOperationException($"{nameof(AddressableDungeonBootstrapper)} already has an active instance.");
            }

            Active = this;
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

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        private void OnDestroy()
        {
            if (Active == this)
            {
                Active = null;
            }
        }
        public async void Generate()
        {
            try
            {
                await GenerateAsync();
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(AddressableDungeonBootstrapper)}: 生成房间失败, Error: {exception.Message}", this);
                throw;
            }
        }

        /// <summary>
        /// 异步加载关卡图后生成房间.
        /// </summary>
        public async Task GenerateAsync()
        {
            if (generated || isGenerating) return;

            isGenerating = true;
            ResolveGenerator();
            try
            {
                if (dungeonGenerator == null)
                {
                    throw new InvalidOperationException($"{nameof(AddressableDungeonBootstrapper)} requires {nameof(DungeonGeneratorGrid2D)}.");
                }

                SaveGameService.ApplyPendingGenerationSettings(this, dungeonGenerator);
                await ApplyAddressableLevelGraphAsync();
                var fixedGraphSettings = dungeonGenerator.FixedLevelGraphConfig;
                if (fixedGraphSettings.LevelGraph == null)
                {
                    throw new InvalidOperationException($"{nameof(AddressableDungeonBootstrapper)} requires a loaded LevelGraph.");
                }

                generated = true;
                var payload = dungeonGenerator.Generate() as DungeonGeneratorPayloadGrid2D;
                // 保存 Edgar 实际使用的 seed, 读档时用它重建同一张地图.
                lastGeneratedSeed = payload?.GeneratedLevel != null ? payload.GeneratedLevel.Seed : dungeonGenerator.RandomGeneratorSeed;
                StartCoroutine(RestorePendingSaveNextFrame());
            }
            finally
            {
                isGenerating = false;
            }

            IEnumerator RestorePendingSaveNextFrame()
            {
                // 房间实例的 Start 会在生成后一帧执行, 等门和房间初始化完成后再覆盖存档状态.
                yield return null;
                var restoreTask = SaveGameService.TryRestorePendingSaveAsync();
                while (!restoreTask.IsCompleted)
                {
                    yield return null;
                }

                if (restoreTask.IsFaulted)
                {
                    throw restoreTask.Exception;
                }
            }
        }

        /// <summary>
        /// 通过 AddressableLoader 加载关卡图.
        /// </summary>
        private async Task ApplyAddressableLevelGraphAsync()
        {
            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(AddressableDungeonBootstrapper)} requires {nameof(AddressableLoader)}.");
            }

            var fixedGraphSettings = dungeonGenerator.FixedLevelGraphConfig;
            fixedGraphSettings.LevelGraph = await loader.LoadAssetAsync<LevelGraph>(levelGraphAddress);
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
    }
}
