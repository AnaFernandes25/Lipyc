using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigModel
{
    public Resolution Resolution;
    public LimitFPS LimitFPS;
    public bool WindowMode;
    public Quality Quality;
    public bool Bloom;
    public bool Reflection;
    public float GlobalVolume;
    public float MusicVolume;
    public float EffectsVolume;
    public bool AutoSave;
}

[System.Serializable]
public enum Quality
{
    VeryLow,
    Low,
    Medium,
    High,
    Ultra
}

[System.Serializable]
public class Resolution
{
    public int Width;
    public int Height;
}

[System.Serializable]
public class LimitFPS
{
    public bool Limit;
    public int FPS;
}
