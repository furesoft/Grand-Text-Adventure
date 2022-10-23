using System.Linq;
using System.Runtime.ConstrainedExecution;
using LiteDB;

namespace GrandTextAdventure.Core;

public class Settings
{
    public BsonAutoId SettingsID { get; set; }

    private static Settings s_instance;
    public static Settings Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new Settings();
            }

            return s_instance;
        }
    }

    public bool WantPlayTutorial { get; set; }
    public bool TutorialDone { get; set; }

    public int TutorialIndex { get; set; }
    public bool IsFirstStart { get; set; }

    public static void Load()
    {
        using LiteDatabase db = new("gta.conf"); // ToDo: need to set config path
        var settingsCollection = db.GetCollection<Settings>("settings");

        if (settingsCollection.Count() == 1)
        {
            s_instance = settingsCollection.FindAll().First();
        }
    }

    public static void Save()
    {
        using LiteDatabase db = new("gta.conf"); // ToDo: need to set config path
        var settingsCollection = db.GetCollection<Settings>("settings");

        if (settingsCollection.Count() == 1)
        {
            settingsCollection.Update(Instance);
        }
        else
        {
            settingsCollection.Insert(Instance);
        }
    }
}
