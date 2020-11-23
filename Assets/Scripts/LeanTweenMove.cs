using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeanTweenMove : MonoBehaviour
{
    public Transform movePoint;

    public Button button;

    private void OnEnable()
    {
        button.interactable = false;

        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();

        StartCoroutine(DisableButtonTimer());
    }

    IEnumerator DisableButtonTimer()
    {
        LeanTween.move(gameObject, movePoint, 1f).setEaseInBounce();
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.2f);

        yield return new WaitForSeconds(1f);
        button.interactable = true;
    }
}
