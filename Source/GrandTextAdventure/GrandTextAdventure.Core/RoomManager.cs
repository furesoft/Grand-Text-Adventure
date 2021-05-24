using System;
using System.Collections.Concurrent;
using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core
{
    public static class RoomManager
    {

        private static ConcurrentDictionary<RoomID, Room> _cache = new();

        public static bool IsRoomLoaded(RoomID id)
        {
            return _cache.ContainsKey(id);
        }

        public static Room GetRoom(RoomID id)
        {
            var loaded = LoadRoom(id);

            _cache.AddOrUpdate(id, (_) => loaded, (_, __) => __);

            return loaded;
        }

        private static Room LoadRoom(RoomID id)
        {
            var filename = id.ID + ".rdef";

            return new Room { Name = id.ID }; // ToDo: Replace with real room data
        }
    }
}