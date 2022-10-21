using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData instanceRef;

    #region BeatZone
    [Header("Beat Zone Variables")]
    private bool BUSY;
    [SerializeField]  private bool BeatZoneUsable;
    private bool BeatZoneActive;
    [SerializeField] private BaseCharacterObject CurrentTurn;
    [SerializeField] BaseBeatObject BeatTemplate;
    public float blipSpeed = 7f;
    #endregion
    #region KnownSceneObjects
    [Header("InScene Variables")]
    [SerializeField] public GameObject BlipSpawner;
    [SerializeField] public GameObject AttackBlip;
    [SerializeField] public GameObject DefenseBlip;
    [SerializeField] public GameObject TurnManager;
    #endregion
    #region PlayerData
    [Header("Player Data")]
    [SerializeField] public BaseCharacterObject[] CharactersInParty;
    #endregion
    public bool SetBusy
    {
        get { return BUSY; }
        set { BUSY = value; }
    }
    public bool BeatZoneUsableAccessor
    {
        get { return BeatZoneUsable; }
        set { BeatZoneUsable = value; }
    }
    public bool BeatZoneActiveCheck
    {
        get { return BeatZoneActive; }
        set { BeatZoneActive = value; }
    }
    public BaseCharacterObject CurrentTurnAccessor
    {
        get { return CurrentTurn; }
        set { CurrentTurn = value; }
    }
    public BaseBeatObject AccessBeat
    {
        get { return BeatTemplate; }
    }


    // Initialize on Load First
    private void Awake()
    {
        #region Init
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
            Debug.Log("WoopsyDoopsyLoadedAnOopsy");
        }
        #endregion
        Debug.Log("Data Initialized");
        BUSY = false;
        BeatZoneUsable = false;
        BeatZoneActive = false;
}

    public void ToggleBeatZone( bool usable)
    {
        BeatZoneUsable = usable;
        Debug.Log("BeatZone: " + usable);
    }
}
