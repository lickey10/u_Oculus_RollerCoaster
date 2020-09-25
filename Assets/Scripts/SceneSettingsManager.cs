
public static class SceneSettingsManager
{
    public enum Rides
    { 
        None,
        RollerCoaster,
        FerrisWheel
    }

    public static Rides CurrentRide { get; set; }
    public static UnityEngine.GameObject SpawnPoint { get; set; }
}
