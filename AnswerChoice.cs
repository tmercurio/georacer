using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class AnswerChoice : MonoBehaviour
{
    [SerializeField]
    private ArcadeKart kart;

    public float speedChange;

    [Header("PickupObject")]

    public GameFlowManager gameFlow;

    public bool correct;
    public bool choice;
    public DisplayMessage correctMessage;
    public DisplayMessage incorrectMessage;
    public bool colliding = false;

    // Width of the answer choice you're driving through
    public float width = 3f;

    // Buffers for collsion checking with kart and answer choice
    public float heightAboveBuffer = 3f;
    public float heightBelowBuffer = 2f;

    public AudioClip CollectSound;

    // Start is called before the first frame update
    void Start()
    {
        gameFlow = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameFlowManager>();
        correctMessage.gameObject.SetActive(false);
        incorrectMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for collision
        if (((transform.position.x - width / 2) < kart.transform.position.x) &&
                ((transform.position.x + width / 2) > kart.transform.position.x) &&
                ((transform.position.y + heightAboveBuffer) > kart.transform.position.y) &&
                ((transform.position.y - heightBelowBuffer) < kart.transform.position.y) &&
                (transform.position.z < (kart.transform.position.z + kart.length / 2)) &&
                (transform.position.z > (kart.transform.position.z - kart.length / 2))) {
            if (!colliding) {
                OnCollect();
                colliding = true;
            }
        }
        else if (colliding)
            colliding = false;

    }

    void OnCollect()
    {
        if (CollectSound)
        {
            AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }

        gameFlow.incQuestion();

        kart.changeKartSpeed(1 + speedChange);

        Debug.Log("Choice: " + choice + "; Correct: " + correct);
        if (choice) {
            if (correct)
                correctMessage.gameObject.SetActive(true);
            else
                incorrectMessage.gameObject.SetActive(true);
        }
    }
}
