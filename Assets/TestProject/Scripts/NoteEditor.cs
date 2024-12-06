using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class NoteEditor : MonoBehaviour
{
    [Serializable]
    public class MusicData
    {
        public List<float> noteSpawnTime;
        public string musicName;
        public float turnSpeed;
    }

    private float time;
    [SerializeField]
    private GameObject mainCircle;
    [SerializeField]
    private GameObject notePrefab;
    [SerializeField]
    private GameObject noteObj;
    [SerializeField]
    private GameObject detectCircle;

    [SerializeField]
    private MusicData musicData;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private Text timerUI;

    [SerializeField]
    private GameObject videoimg;
    [SerializeField]
    private VideoPlayer vp;

    private bool isStart;
    private bool isPlay;

    [SerializeField]
    private string jsonFileName;


    private void Start()
    {
        
        StartCoroutine(Co_StartDelay());
        musicData.musicName = "Test Music";
        musicData.turnSpeed = turnSpeed;
    }

    void Update()
    {
        time += Time.deltaTime;
        
        
        if (isStart)
        {
            if (isPlay && !vp.isPlaying)
            {
                SaveData(jsonFileName);
            }

            mainCircle.transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
            if (Input.anyKeyDown)
            {
                Destroy(noteObj);
                noteObj = Instantiate(notePrefab, detectCircle.transform.position, detectCircle.transform.rotation);
                musicData.noteSpawnTime.Add(time);
                Debug.Log("Save Key : " + time);
            }
        }

    }

    IEnumerator Co_StartDelay()
    {
        timerUI.text = "3";
        yield return new WaitForSeconds(1);
        timerUI.text = "2";
        yield return new WaitForSeconds(1);
        timerUI.text = "1";
        yield return new WaitForSeconds(1);
        timerUI.text = "Start";
        videoimg.SetActive(true);
        vp.Play();
        isStart = true;
        yield return new WaitForSeconds(0.5f);
        timerUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        isPlay = true;
        
    }

    public void SaveData(string fileName)
    {
        string json = JsonUtility.ToJson(musicData, true);
        File.WriteAllText(Path.Combine(Application.dataPath, fileName + ".json"), json);
        Debug.Log("Save On " + Path.Combine(Application.dataPath, fileName + ".json"));
        timerUI.text = "END";
        isStart = false;
        timerUI.gameObject.SetActive(true);
    }

}
