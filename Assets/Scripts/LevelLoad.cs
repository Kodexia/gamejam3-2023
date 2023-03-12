using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 2f;

    public void LoadNextLevel(int id)
    {
        StartCoroutine(LoadLevel(id));
    }

    IEnumerator LoadLevel(int SceneId)
    {
        //play animation
        transition.SetTrigger("Load");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneId);
    }



    public void exit()
    {
        Debug.Log("konec");
        Application.Quit();
    }

}
