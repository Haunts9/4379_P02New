using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EncounterManager : MonoBehaviour
{ 
    [SerializeField] GameObject window;
    [SerializeField] GameObject[] buttons;
    GameObject tempEncounterProp;
    GameObject EncounterEffect;
    BaseEncounterObject encounter;
    public void InitializeEncounter()
    {
        StartCoroutine(wait());
    }
    void ContinueInitialize()
    {
        int tempcount = 0;
        window.SetActive(true);
        encounter = SceneData.instanceRef.CurrentEncounter;
        SceneData.instanceRef.mainText.text = encounter.encounterDescription;
        #region buttons
        foreach (string option in encounter.encounterOptions)
        {
            buttons[tempcount].SetActive(true);
            buttons[tempcount].GetComponentInChildren<TextMeshProUGUI>().text = option;
            tempcount++;
        }
        #endregion
        if (encounter.encounterProp != null)
        {
            tempEncounterProp = Instantiate(encounter.encounterProp, SceneData.instanceRef.EncounterSpawnLocation);
        }
    }
    public void OtherButton(int choice)
    {
        SceneData.instanceRef.choice = choice;
        EncounterEffect = Instantiate(encounter.encounterEffect);
    }
    public void PassButton()
    {
        CleanUp();
    }
    public void CleanUp()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        Destroy(EncounterEffect);
        SceneData.instanceRef.ClearText();
        window.SetActive(false);
        SceneData.instanceRef.TravelManager.GetComponent<TravelManager>().InitializeNextEvent();
    }
    public void hideOptions()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }
    public void swapProp()
    {
        encounter = SceneData.instanceRef.CurrentEncounter;
        Destroy(tempEncounterProp);
        if (encounter.encounterPropUsed != null)
        {
            tempEncounterProp = Instantiate(encounter.encounterPropUsed, SceneData.instanceRef.EncounterSpawnLocation);
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3f);
        ContinueInitialize();
    }
}
