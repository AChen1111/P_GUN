using System;
using Edgar.Unity;
using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// Attach to the same GameObject as DungeonGeneratorGrid2D.
    /// It checks whether all rooms in the generated level were instantiated.
    /// </summary>
    public class AllRoomsGeneratedChecker : DungeonGeneratorPostProcessingComponentGrid2D
    {
        public static event Action OnAllRoomsGenerated = delegate { };

        public static bool IsAllRoomsGenerated { get; private set; }

        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            if (level == null || level.LevelDescription == null)
            {
                IsAllRoomsGenerated = false;
                return;
            }

            var expectedCount = level.LevelDescription.GetGraphWithCorridors().VerticesCount;
            var generatedCount = level.RoomInstances.Count;

            IsAllRoomsGenerated = generatedCount == expectedCount;

            if (IsAllRoomsGenerated)
            {
                OnAllRoomsGenerated.Invoke();
            }
            else
            {
                Debug.LogWarning($"Room generation incomplete. expected={expectedCount}, generated={generatedCount}");
            }
        }
    }
}
