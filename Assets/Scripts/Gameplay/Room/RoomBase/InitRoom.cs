using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class InitRoom : Room
    {
        [Header("玩家出生点")]
        [SerializeField] private Transform playerSpawnPoint;

        [Header("玩家预制体")]
        [SerializeField] private Player playerPrefab;
        protected override void OnRoomInitialized()
        {
            needGenerateDoors = false;
            PlacePlayerAtSpawn();

            void PlacePlayerAtSpawn()
            {
                var spawnPosition = playerSpawnPoint != null ? playerSpawnPoint.position : transform.position;
                if (PlayerRegistry.Current != null)
                {
                    PlayerRegistry.Current.transform.position = spawnPosition;
                    return;
                }

                if (playerPrefab != null)
                {
                    var player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
                    player.gameObject.SetActive(true);
                }
            }
}
    }
}
