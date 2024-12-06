using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public void ToChronomia()
    {
        SceneManager.LoadScene("NoteTestChronomia");
    }

    public void ToFreedomDive()
    {
        SceneManager.LoadScene("NoteTestFreedomDive");
    }
}
