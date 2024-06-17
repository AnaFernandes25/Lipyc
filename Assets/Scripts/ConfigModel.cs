using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ConfigModel 
{
    public Resolution Resolution { get; set; }
    public LimitFPS LimitFPS { get; set; }
    public bool WindowMode { get; set; }
    public Quality Quality { get; set; }
    public bool Bloom { get; set; }
    public bool Reflection { get; set; }
    public float GlobalVolume { get; set; }
    public float MusicVolume { get; set; }
    public float EffectsVolume { get; set; }
    public bool AutoSave { get; set; }

}

[SerializeField]
public enum Quality
{
    VeryLow,
    Low,
    Medium,
    High,
    Ultra
}

[SerializeField]
public class Resolution
{
    public int Width { get; set; }
    public int Height { get; set; }

}

[SerializeField]
public class LimitFPS
{
    public bool Limit { get; set; }
    public int FPS { get; set; }

}
