using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MusicData
{
    public string musicName;
    public string composerName;
    public string editorName;

    public int difficult;
    public float turnSpeed;
    public float bpm;
    
    public string noteSkin;
    public string mapSkin;

    public List<float> noteSpawnTime;
}
