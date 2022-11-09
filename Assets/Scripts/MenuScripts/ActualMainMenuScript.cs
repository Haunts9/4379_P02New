using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActualMainMenuScript : MonoBehaviour
{
    [SerializeField] Image fade;
    private void Start()
    {
        Color32 black = new Color32(0, 0, 0, 255);
        Color32 clear = new Color32(0, 0, 0, 0);
        StartCoroutine(3f.TweengC((p) => fade.color = p, black, clear));
    }
    public void SceneButton(int value)
    {
        Debug.Log("Clicked");
        Color32 black = new Color32(0, 0, 0, 255);
        StartCoroutine(1.5f.TweengC((p) => fade.color = p, fade.color, black));
        StartCoroutine(wait(value));
    }
    public void SettingsButton()
    {
        //SceneManager.LoadScene(0);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    IEnumerator wait(int scene)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }
}
