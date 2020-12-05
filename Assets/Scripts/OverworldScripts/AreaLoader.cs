using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaLoader : MonoBehaviour
{
    public Animator transition;
    public string AreaToLoad;
    public bool delay = false;
    public int goingToDim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !delay)
        {
            PlayerMovement.firstTime = false;
             
            LoadNextLevel();
            //SceneManager.LoadScene(AreaToLoad);
        }
        else if (delay) delay = false;
    }

    public void LoadNextLevel()
    {
        ActiveOverworldEntity.dim = goingToDim;
        StartCoroutine(LoadLevel("LevelOneScene"));
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("LevelOneScene");
    }

}
