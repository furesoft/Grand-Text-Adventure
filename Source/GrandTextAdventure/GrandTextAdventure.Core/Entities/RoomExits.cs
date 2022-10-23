namespace GrandTextAdventure.Core.Entities;

public class RoomExits
{
    public Room East
    {
        get
        {
            return RoomManager.GetRoom(EastID);
        }
    }

    public Room South
    {
        get
        {
            return RoomManager.GetRoom(SouthID);
        }
    }

    public Room North
    {
        get
        {
            return RoomManager.GetRoom(NorthID);
        }
    }

    public Room West
    {
        get
        {
            return RoomManager.GetRoom(WestID);
        }
    }

    public RoomID EastID { get; set; }
    public RoomID WestID { get; set; }
    public RoomID SouthID { get; set; }
    public RoomID NorthID { get; set; }


}
