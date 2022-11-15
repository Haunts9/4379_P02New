using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelManager : MonoBehaviour
{
    [SerializeField] BaseEncounterObject RestEncounter;
    [SerializeField] Transform PanSpawn;
    [SerializeField] Transform PanDelete;
    [SerializeField] float walkSpeed = 5f;
    GameObject OldPanorama;
    BaseLevelObject Level;
    public void Start()
    {
        SceneData.instanceRef.CurrentEvent = 0;
        SceneData.instanceRef.walkSpeed = walkSpeed;
        //Level = SceneData.instanceRef.CurrentLevel;
        SceneData.instanceRef.AudioManager.GetComponent<AudioManager>().StartTrack();
    }

    public void InitializeNextEvent()
    {
        Level = SceneData.instanceRef.CurrentLevel;
        SceneData.instanceRef.CurrentEvent++;
        OldPanorama = SceneData.instanceRef.CurrentPanorama;
        if (SceneData.instanceRef.CurrentEvent == 1)
        {
            createEntrancePanorama();
        }
        else
        {
            createPanorama();
        }

        StartCoroutine(walkSpeed.Tweeng((p) => OldPanorama.transform.position = p, OldPanorama.transform.position, PanDelete.position));
        StartCoroutine(walkSpeed.Tweeng((p) => SceneData.instanceRef.CurrentPanorama.transform.position = p, SceneData.instanceRef.CurrentPanorama.transform.position, new Vector3(0,0,0)));
        StartCoroutine(waitTillNextEvent());
        StartCoroutine(Walking());
    }
    #region doBackground
    void createPanorama()
    {
        int choice = Random.Range(0, Level.Panoramas.Length);
        SceneData.instanceRef.CurrentPanorama = Instantiate(Level.Panoramas[choice], PanSpawn);
        SceneData.instanceRef.EnemyDollSpawnLocations = SceneData.instanceRef.CurrentPanorama.FindComponentsInChildrenWithTag<Transform>("EnemySpawn");
        SceneData.instanceRef.EncounterSpawnLocation = SceneData.instanceRef.CurrentPanorama.FindComponentInChildWithTag<Transform>("ExploreSpawn");
    }
    void createEntrancePanorama()
    {
        SceneData.instanceRef.CurrentPanorama = Instantiate(Level.EntrancePanorama, PanSpawn);
        SceneData.instanceRef.EnemyDollSpawnLocations = SceneData.instanceRef.CurrentPanorama.FindComponentsInChildrenWithTag<Transform>("EnemySpawn");
        SceneData.instanceRef.EncounterSpawnLocation = SceneData.instanceRef.CurrentPanorama.FindComponentInChildWithTag<Transform>("ExploreSpawn");
    }
    IEnumerator travelToDeath()
    {
        yield return new WaitForSeconds(walkSpeed);
        Destroy(OldPanorama);

    }
    #endregion
    IEnumerator waitTillNextEvent()
    {
        yield return new WaitForSeconds(1f);
        doEvent();
    }
    IEnumerator Walking()
    { 
        yield return new WaitForSeconds(.1f);
        foreach (GameObject character in SceneData.instanceRef.PartyDolls)
        {
            AnimationController anim = character.GetComponentInChildren<AnimationController>();
            anim.SetWalking();
        }
        yield return new WaitForSeconds(walkSpeed-.5f);
        foreach (GameObject character in SceneData.instanceRef.PartyDolls)
        {
            AnimationController anim = character.GetComponentInChildren<AnimationController>();
            anim.SetWalking();
        }
    }
    void doEvent()
    {
        switch(Level.floorplan[SceneData.instanceRef.CurrentEvent])
        {
            case "Entrance":
                doEntrance();
                break;
            case "Battle":
                doBattle();
                break;
            case "Boss":
                doBoss();
                break;
            case "Encounter":
                doEncounter();
                break;
            case "Rest":
                doRest();
                break;
            case "End":
                doLevelEnd();
                break;

        }
    }
    void doBattle()
    {
        int choice = Random.Range(0, Level.possibleBattles.Length);
        SceneData.instanceRef.CurrentBattle = SceneData.instanceRef.CurrentLevel.possibleBattles[choice];
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().enabled = false;
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().enabled = true;
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().InitializeTurnOrder();
    }
    void doBoss()
    {
        int choice = Random.Range(0, Level.possibleBossBattles.Length);
        SceneData.instanceRef.CurrentBattle = SceneData.instanceRef.CurrentLevel.possibleBossBattles[choice];
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().enabled = false;
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().enabled = true;
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().InitializeTurnOrder();
    }
    void doEncounter()
    {

        int choice = Random.Range(0, Level.possibleEncounters.Length);
        SceneData.instanceRef.CurrentEncounter = SceneData.instanceRef.CurrentLevel.possibleEncounters[choice];
        Debug.Log(SceneData.instanceRef.CurrentEncounter + " initialized.");
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().enabled = false;
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().enabled = true;
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().InitializeEncounter();
    }
    void doRest()
    {
        SceneData.instanceRef.CurrentEncounter = RestEncounter;
        Debug.Log(SceneData.instanceRef.CurrentEncounter + " initialized.");
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().enabled = false;
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().enabled = true;
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().InitializeEncounter();
    }
    void doEntrance()
    {
        SceneData.instanceRef.CurrentEncounter = Level.Entrance;
        Debug.Log(SceneData.instanceRef.CurrentEncounter + " initialized.");
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().enabled = false;
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().enabled = true;
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().InitializeEncounter();
    }
    void doLevelEnd()
    {
        SceneData.instanceRef.levelCount++;
        if (SceneData.instanceRef.levelCount < SceneData.instanceRef.AllLevels.Length)
        {
            SceneData.instanceRef.CurrentEvent = 0;
            SceneData.instanceRef.CurrentLevel = SceneData.instanceRef.AllLevels[SceneData.instanceRef.levelCount];
            SceneData.instanceRef.AudioManager.GetComponent<AudioManager>().StartTrack();
        }
        else
        {
            SceneData.instanceRef.isWin = true;
        }

    }
}
