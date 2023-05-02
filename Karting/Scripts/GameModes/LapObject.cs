// File taken from kart game Unity tutorial and edited by Thomas Mercurio

using UnityEngine;

/// <summary>
/// This class inherits from TargetObject and represents a LapObject.
/// </summary>
public class LapObject : TargetObject
{
    [Header("LapObject")]
    [Tooltip("Is this the first/last lap object?")]
    public bool finishLap;

    [HideInInspector]
    public bool lapOverNextPass;

    public GameFlowManager gameFlow;

    void Start() {
        Register();
        gameFlow = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameFlowManager>();
        finishLap = false;
    }

    void OnEnable()
    {
        lapOverNextPass = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // First time through, set finishLap to true; second time through, set gameOver to true
        if (other.CompareTag("Player")) {
            Objective.OnUnregisterPickup?.Invoke(this);
            if (finishLap) {
                gameFlow.gameOver = true;
            }

            finishLap = true;
        }
    }
}
