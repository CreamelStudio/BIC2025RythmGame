using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class LoadNote : MonoBehaviour
{
    [Serializable]
    public class MusicData
    {
        public List<float> noteSpawnTime;
        public string musicName;
        public float turnSpeed;
    }
    [SerializeField]
    private MusicData musicdata;

    private float time;
    [SerializeField]
    private GameObject mainCircle;

    [SerializeField]
    private GameObject disCircle;

    [SerializeField]
    private GameObject notePrefab;
    [SerializeField]
    private GameObject noteObj;
    [SerializeField]
    private GameObject mainDetectCircle;
    [SerializeField]
    private GameObject disDetectCircle;

    [SerializeField]
    private GameObject video;
    [SerializeField]
    private VideoPlayer vp;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private GameObject outline;

    [SerializeField]
    private Text timerUI;
    [SerializeField]
    private Text musicName;

    private bool isStart;
    [SerializeField]
    private int nowSpawnNum;
    [SerializeField]
    private int nowClickNum;
    [SerializeField]
    private List<GameObject> noteList;

    [SerializeField]
    private float goodFloat;
    [SerializeField]
    private float perfectFloat;
    [SerializeField]
    private float badFloat;
    [SerializeField]
    private float missFloat;

    private bool isEndSpawn;

    private float mainRotate;
    private float disRotate;

    private bool isPlay;

    [SerializeField]
    private Text scoreText;
    private int score;

    [SerializeField]
    public int comboCount;
    [SerializeField]
    private GameObject comboText;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private string jsonFileName;

    [SerializeField]
    private float Sync;
    private void Start()
    {
        comboCount = 0;
        LoadJson(jsonFileName);
        musicName.text = $"{musicdata.musicName}";
        StartCoroutine(Co_StartDelay());
    }

    void Update()
    {
        time += Time.deltaTime;
        outline.transform.Rotate(0,0,((turnSpeed/2) * Time.deltaTime));
        disCircle.transform.Rotate(0, 0, -turnSpeed* Time.deltaTime);
        disRotate += turnSpeed * Time.deltaTime;
        Debug.Log("Dis Rotate :" + disRotate);
        Debug.Log($"Time {musicdata.noteSpawnTime[nowSpawnNum]} + {time + 3 + (-Sync)}");
        if (musicdata.noteSpawnTime[nowSpawnNum] <= time + 3 + (-Sync) && !isEndSpawn)
        {
            noteObj = Instantiate(notePrefab, disDetectCircle.transform.position, disDetectCircle.transform.rotation);
            noteList.Add(noteObj);
            Debug.Log("Spawn Note");
            Debug.Log($"List N {musicdata.noteSpawnTime.Count}, {nowSpawnNum}");
            if(musicdata.noteSpawnTime.Count - 1 == nowSpawnNum)
            {
                isEndSpawn = true;
            }
            else
            {
                nowSpawnNum++;
            }
            
        }

        if (isStart && musicdata.musicName != null)
        {
            
            mainCircle.transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
            mainRotate += turnSpeed * Time.deltaTime;
            Debug.Log("Main Rotate :" + mainRotate);
            musicName.text = $"{musicdata.musicName}\ntime: {time}";
            
        }

        

        if (Input.anyKeyDown)
        {
            if (Vector3.Distance(mainDetectCircle.transform.position, noteList[nowClickNum].transform.position) <= perfectFloat)
            {
                Debug.Log("perfectFloat");
                Destroy(noteList[nowClickNum], 0.1f);
                noteList[nowClickNum].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                score += 5;
                comboCount++;
                GameObject combo = Instantiate(comboText, canvas.transform);
                combo.GetComponent<Text>().text = $"Combo!\n{comboCount}";
                Destroy(combo, 0.7f);
                nowClickNum++;
                scoreText.text = score.ToString();
            }
            else if (Vector3.Distance(mainDetectCircle.transform.position, noteList[nowClickNum].transform.position) <= goodFloat)
            {
                Debug.Log("goodFloat");
                Destroy(noteList[nowClickNum], 0.1f);
                noteList[nowClickNum].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
                score += 3;
                comboCount++;
                GameObject combo = Instantiate(comboText, canvas.transform);
                combo.GetComponent<Text>().text = $"Combo!\n{comboCount}";
                Destroy(combo, 0.7f);
                nowClickNum++;
                scoreText.text = score.ToString();
            }
            else if (Vector3.Distance(mainDetectCircle.transform.position, noteList[nowClickNum].transform.position) <= badFloat)
            {
                Debug.Log("badFloat");
                Destroy(noteList[nowClickNum], 0.1f);
                noteList[nowClickNum].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                score += 1;
                comboCount = 0;
                nowClickNum++;
                scoreText.text = score.ToString();
            }
            else 
            {
                Debug.Log("miss");
                Destroy(noteList[nowClickNum], 0.1f);
                noteList[nowClickNum].GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
                score -= 1;
                comboCount = 0;
                nowClickNum++;
                scoreText.text = score.ToString();
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
        vp.Play();
        video.SetActive(true);
        isStart = true;
        yield return new WaitForSeconds(0.5f);
        timerUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        isPlay = true;
    }



    public void LoadJson(string fileName)
    {
        string path = Path.Combine(Application.dataPath, fileName + ".json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            musicdata = JsonUtility.FromJson<MusicData>(json);
        }
        Debug.Log("Json Data Complete");
    }
}