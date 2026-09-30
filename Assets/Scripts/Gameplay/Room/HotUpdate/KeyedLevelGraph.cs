using System;
using System.Collections.Generic;
using Edgar.Unity;
using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class KeyedRoomTemplates
    {
        public int index;
        [AddressableKey(AddressableAssetKind.Prefab)] public List<string> keys = new List<string>();
    }

    /// <summary>
    /// Edgar 保留拓扑编辑能力, 模板资源由短名配置提供, 不修改第三方运行时.
    /// </summary>
    [CreateAssetMenu(menuName = "PG/Room/Keyed Level Graph")]
    public sealed class KeyedLevelGraph : ScriptableObject
    {
        public LevelGraph topology;
        public List<KeyedRoomTemplates> rooms = new List<KeyedRoomTemplates>();
        public List<KeyedRoomTemplates> connections = new List<KeyedRoomTemplates>();
        [AddressableKey(AddressableAssetKind.Prefab)] public List<string> corridors = new List<string>();
        [AddressableKey(AddressableAssetKind.Prefab)] public List<string> defaults = new List<string>();

        public LevelGraph CreateRuntimeGraph(List<ScriptableObject> ownedObjects)
        {
            var graph = Instantiate(topology);
            ownedObjects.Add(graph);
            graph.Rooms = new List<RoomBase>();
            graph.Connections = new List<ConnectionBase>();
            var roomMap = new Dictionary<RoomBase, RoomBase>();
            foreach (var source in topology.Rooms)
            {
                var room = Instantiate(source);
                room.name = source.name;
                ownedObjects.Add(room);
                graph.Rooms.Add(room);
                roomMap.Add(source, room);
            }
            foreach (var source in topology.Connections)
            {
                var connection = Instantiate(source);
                ownedObjects.Add(connection);
                connection.From = roomMap[source.From];
                connection.To = roomMap[source.To];
                graph.Connections.Add(connection);
            }
            foreach (var binding in rooms)
            {
                var room = graph.Rooms[binding.index] as Edgar.Unity.Room ?? throw new InvalidOperationException("Keyed graph requires Edgar Room nodes.");
                room.IndividualRoomTemplates = Resolve(binding.keys);
                room.RoomTemplateSets = new List<RoomTemplatesSet>();
            }
            foreach (var binding in connections)
            {
                var connection = graph.Connections[binding.index] as Connection ?? throw new InvalidOperationException("Keyed graph requires Edgar Connection nodes.");
                connection.RoomTemplates = Resolve(binding.keys);
            }
            graph.CorridorIndividualRoomTemplates = Resolve(corridors);
            graph.DefaultIndividualRoomTemplates = Resolve(defaults);
            graph.CorridorRoomTemplateSets = new List<RoomTemplatesSet>();
            graph.DefaultRoomTemplateSets = new List<RoomTemplatesSet>();
            return graph;
        }

        private static List<GameObject> Resolve(List<string> keys) => AddressableAssetAccess.List<GameObject>(keys);
    }
}
