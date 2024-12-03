using UnityEngine;

public class TestNote : MonoBehaviour
{
    public float turnSpeed;

    public GameObject mainCircle;
    public GameObject detectCircle;
    public GameObject notePrefab;

    public GameObject noteObj;

    public float goodFloat;
    public float perfectFloat;
    public float badFloat;


    private void Update()
    {
        mainCircle.transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.L))
        {
            Destroy(noteObj);
            noteObj = Instantiate(notePrefab, detectCircle.transform.position, detectCircle.transform.rotation);
            
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Vector3.Distance(detectCircle.transform.position, noteObj.transform.position) <= perfectFloat)
            {
                Debug.Log("perfectFloat");
                Destroy(noteObj, 1);
                noteObj.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            }
            else if (Vector3.Distance(detectCircle.transform.position, noteObj.transform.position) <= goodFloat)
            {
                Debug.Log("goodFloat");
                Destroy(noteObj, 1);
                noteObj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
            }
            else if (Vector3.Distance(detectCircle.transform.position, noteObj.transform.position) <= badFloat)
            {
                Debug.Log("badFloat");
                Destroy(noteObj,1);
                noteObj.GetComponent<SpriteRenderer>().color = new Color(1,0, 0);
            }            
            else
            {
                Debug.Log("miss");
                Destroy(noteObj, 1);
                noteObj.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
            }
        }
        
        
    }



}
