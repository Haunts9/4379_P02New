using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownMenuScript : MonoBehaviour
{
    RectTransform target;
    bool currentlyDown = false;
    Vector3 upPos = new Vector3(-500, 1040);
    Vector3 downPos = new Vector3(-500, 100);
    [SerializeField] GameObject[] menus;
    private void Start()
    {
        MainMenuButton(0);
    }
    public void DropDownButtonClick()
    {
        target = GetComponent<RectTransform>();
        //Debug.Log(target.anchoredPosition);
        if (currentlyDown == false)
        {
            currentlyDown = true;
            StartCoroutine(moveToPos(downPos, 1f));
        }
        else
        {
            currentlyDown = false;
            StartCoroutine(moveToPos(upPos, 1f));
        }
    }
    public void MainMenuButton(int value)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        menus[value].SetActive(true);
    }
    private IEnumerator moveToPos(Vector3 pos, float duration)
    {
        float time = 0;
        Vector3 currentPos = target.anchoredPosition;
        while (time < duration)
        {
            time += Time.deltaTime;
            target.anchoredPosition = Vector3.Lerp(currentPos, pos, time / duration);
            yield return null;
        }
        target.anchoredPosition = pos;

    }
}
