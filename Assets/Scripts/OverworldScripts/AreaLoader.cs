using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaLoader : MonoBehaviour
{
    public Animator transition;
    public string AreaToLoad;
    public bool delay = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !delay)
        {
            PlayerMovement.firstTime = false;
            LocationRememberer.awokenDim[FindObjectOfType<LocationLoader>().num] = true;
            LoadNextLevel();
            //SceneManager.LoadScene(AreaToLoad);
        }
        else if (delay) delay = false;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel("LevelOneScene"));
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("LevelOneScene");
    }

}
