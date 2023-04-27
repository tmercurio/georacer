using UnityEngine;
using KartGame.KartSystems;

/// <summary>
/// This class inherits from TargetObject and represents a PickupObject.
/// </summary>
public class PickupObject : TargetObject
{
    /*
    [SerializeField]
    private ArcadeKart kart;

    public float speedChange;

    [Header("PickupObject")]

    public GameFlowManager gameFlow;

    public bool correct;
    public bool choice;
    public DisplayMessage correctMessage;
    public DisplayMessage incorrectMessage;

    //[Tooltip("New Gameobject (a VFX for example) to spawn when you trigger this PickupObject")]
    //public GameObject spawnPrefabOnPickup;

    //[Tooltip("Destroy the spawned spawnPrefabOnPickup gameobject after this delay time. Time is in seconds.")]
    //public float destroySpawnPrefabDelay = 10;

    [Tooltip("Destroy this gameobject after collectDuration seconds")]
    public float collectDuration = 0f;

    void Start() {
        Register();
        gameFlow = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameFlowManager>();
        correctMessage.gameObject.SetActive(false);
        incorrectMessage.gameObject.SetActive(false);
    }

    void OnCollect()
    {
        if (CollectSound)
        {
            AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }

/*
        if (spawnPrefabOnPickup)
        {
            var vfx = Instantiate(spawnPrefabOnPickup, CollectVFXSpawnPoint.position, Quaternion.identity);
            Destroy(vfx, destroySpawnPrefabDelay);
        }



    //    Objective.OnUnregisterPickup(this);

        TimeManager.OnAdjustTime(TimeGained);

        gameFlow.incQuestion();

        kart.changeKartSpeed(1 + speedChange);

        Debug.Log("Choice: " + choice + "; Correct: " + correct);
        if (choice) {
            if (correct)
                correctMessage.gameObject.SetActive(true);
            else
                incorrectMessage.gameObject.SetActive(true);
        }

        //Destroy(gameObject, collectDuration);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
        {
            OnCollect();
        }
    }
    */
}
