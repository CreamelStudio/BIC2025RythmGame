using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadMusicMap : MonoBehaviour
{
    [SerializeField]
    private List<MusicData> musicList;
    [SerializeField]
    public Vector3 buttonPosition;
    [SerializeField]
    public GameObject buttonPrefab;

    private void Start()
    {
        LoadJson();
    }

    public void LoadJson()
    {
        string[] jsonFiles = Directory.GetFiles(Application.dataPath + "/MainProject/Resources/MusicJson", "*.json");
        for(int i = 0; i < jsonFiles.Length; i++)
        {
            if (File.Exists(jsonFiles[i]))
            {
                string json = File.ReadAllText(jsonFiles[i]);
                musicList.Add(JsonUtility.FromJson<MusicData>(json));
            }
        }
        Debug.Log("Json Data Complete");
        Debug.Log(musicList);
    }
}
