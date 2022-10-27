using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelManager : MonoBehaviour
{
    [SerializeField] Transform PanSpawn;
    [SerializeField] Transform PanDelete;
    [SerializeField] float walkSpeed = 5f;
    GameObject OldPanorama;
    BaseLevelObject Level;
    public void Start()
    {
        SceneData.instanceRef.CurrentEvent = -1;
        SceneData.instanceRef.walkSpeed = walkSpeed;
    }

    public void InitializeNextEvent()
    {
        Level = SceneData.instanceRef.CurrentLevel;
        SceneData.instanceRef.CurrentEvent++;
        OldPanorama = SceneData.instanceRef.CurrentPanorama;
        createPanorama();
        StartCoroutine(walkSpeed.Tweeng((p) => OldPanorama.transform.position = p, OldPanorama.transform.position, PanDelete.position));
        StartCoroutine(walkSpeed.Tweeng((p) => SceneData.instanceRef.CurrentPanorama.transform.position = p, SceneData.instanceRef.CurrentPanorama.transform.position, new Vector3(0,0,0)));
        doEvent();
    }
    #region doBackground
    void createPanorama()
    {
        int choice = Random.Range(0, Level.Panoramas.Length);
        SceneData.instanceRef.CurrentPanorama = Instantiate(Level.Panoramas[choice], PanSpawn);
        SceneData.instanceRef.EnemyDollSpawnLocations = SceneData.instanceRef.CurrentPanorama.FindComponentsInChildrenWithTag<Transform>("EnemySpawn");
        SceneData.instanceRef.EncounterSpawnLocation = SceneData.instanceRef.CurrentPanorama.FindComponentInChildWithTag<Transform>("ExploreSpawn");
    }
    IEnumerator travelToDeath()
    {
        yield return new WaitForSeconds(walkSpeed);
        Destroy(OldPanorama);

    }
    #endregion
    void doEvent()
    {
        switch(Level.floorplan[SceneData.instanceRef.CurrentEvent])
        {
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
    }
    void doRest()
    {

    }
}
