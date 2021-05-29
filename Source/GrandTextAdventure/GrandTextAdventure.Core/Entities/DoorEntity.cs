namespace GrandTextAdventure.Core.Entities
{
    [EntityInstance]
    public class DoorEntity : GameObject, IEnterable
    {
        public DoorEntity()
        {
            Type = GameObjectType.Door;
        }

        public bool IsEnterable() => true;

        public void OnEnter(Position pos)
        {
            throw new System.NotImplementedException();
        }

        public void OnExit(Position pos)
        {

        }
    }
}
