namespace GrandTextAdventure.Core.Entities
{
    [EntityInstance]
    public class BlockingEntity : GameObject, IBlockable
    {
        public BlockingEntity()
        {
            Type = GameObjectType.Blocking;
        }

        public bool IsBlocked => true;
    }
}
