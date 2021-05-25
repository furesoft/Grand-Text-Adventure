using System;
using System.Collections.Concurrent;
using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core
{
    public static class RoomManager
    {

        private static readonly ConcurrentDictionary<RoomID, Room> s_cache = new();

        public static bool IsRoomLoaded(RoomID id)
        {
            return s_cache.ContainsKey(id);
        }

        public static void AddRoom(RoomID id, Room room)
        {
            s_cache.AddOrUpdate(id, (_) => room, (_, __) => __);
        }

        public static Room GetRoom(RoomID id)
        {
            var loaded = LoadRoom(id);

            s_cache.AddOrUpdate(id, (_) => loaded, (_, __) => __);

            return loaded;
        }

        private static Room LoadRoom(RoomID id)
        {
            var filename = id.ID + ".rdef";

            return new Room { Name = id.ID, Exits = new RoomExits() { NorthID = new RoomID("Basic Street") } }; // ToDo: Replace with real room data
        }
    }
}