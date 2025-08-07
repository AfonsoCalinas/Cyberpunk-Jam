using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelSettings
{
    private static readonly Dictionary<int, LevelConfigs> LevelConfigs = new Dictionary<int, LevelConfigs>
    {
        { 2, new LevelConfigs(200f, 12f, 95f, "95_Road Puncher") },
        { 3, new LevelConfigs(250f, 10f, 110f,"110_Balloon Saloon") },
        { 4, new LevelConfigs(300f, 8f, 132f, "132_Gun Flourish") },
        { 5, new LevelConfigs(350f, 6f, 138f, "138_Overhyped Car") },
        // ... more level setups
    };

    private static readonly LevelConfigs DefaultConfig = new LevelConfigs();

    private static LevelConfigs GetConfig()
    {
        int idx = SceneManager.GetActiveScene().buildIndex;
        return LevelConfigs.TryGetValue(idx, out var cfg) ? cfg : DefaultConfig;
    }

    public static LevelConfigs GetFullConfig() => GetConfig();

    // Convenience methods:
    
    
    public static AudioClip GetMusic() => GetConfig().LoadMusicClip();
    
    public static string GetMusicName() => GetConfig().MusicName;
    public static float GetSpeedForCurrentLevel() => GetConfig().Speed;
    public static float GetEndingForCurrentLevel() => GetConfig().Ending;
    
    public static float GetBpmForCurrentLevel() => GetConfig().Bpm;
    
    public static float GetStepsForCurrentLevel() => GetConfig().Steps;

}