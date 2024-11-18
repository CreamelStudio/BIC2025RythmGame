using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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

    private bool isStart;


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
            

            mainCircle.transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.L))
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
        isStart = true;
        yield return new WaitForSeconds(0.5f);
        timerUI.gameObject.SetActive(false);

        yield return new WaitForSeconds(10);
        SaveData("Test");
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
