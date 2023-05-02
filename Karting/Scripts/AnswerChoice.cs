// File completely written by Thomas Mercurio for driving through answer choices

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class AnswerChoice : MonoBehaviour
{
    private ArcadeKart player_kart;

    private ArcadeKart AI_kart;

    ArcadeKart[] karts = new ArcadeKart[2];

    public float speedChange;

    [Header("PickupObject")]

    public GameFlowManager gameFlow;

    public bool correct;
    public bool choice;
    public DisplayMessage correctMessage;
    public DisplayMessage incorrectMessage;
    bool[] colliding = {false, false};

    // Width of the answer choice you're driving through
    public float width = 3f;

    // Buffers for collsion checking with kart and answer choice
    public float heightAboveBuffer = 3f;
    public float heightBelowBuffer = 2f;

    // Keeps track of which turn in track we are on for collision detection
    public int turn;

    public AudioClip CollectSound;

    // Start is called before the first frame update
    void Start()
    {
        gameFlow = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameFlowManager>();
        correctMessage.gameObject.SetActive(false);
        incorrectMessage.gameObject.SetActive(false);
        karts[0] = GameObject.FindGameObjectWithTag("Player").GetComponent<ArcadeKart>();
        karts[1] = GameObject.FindGameObjectWithTag("AI").GetComponent<ArcadeKart>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for collision--physics written by Thomas Mercurio
        for (int i = 0; i < karts.Length; i++) {
            // Checking collision on first turn of race
            if (turn == 1) {
                if (((transform.position.x - width / 2) < karts[i].transform.position.x) &&
                        ((transform.position.x + width / 2) > karts[i].transform.position.x) &&
                        ((transform.position.y + heightAboveBuffer) > karts[i].transform.position.y) &&
                        ((transform.position.y - heightBelowBuffer) < karts[i].transform.position.y) &&
                        (transform.position.z < (karts[i].transform.position.z + karts[i].length / 2)) &&
                        (transform.position.z > (karts[i].transform.position.z - karts[i].length / 2))) {
                    if (!colliding[i]) {
                        bool isPlayer = (i == 0) ? true : false;
                        OnCollect(karts[i], isPlayer);
                        colliding[i] = true;
                    }
                }
                else if (colliding[i])
                    colliding[i] = false;
            }

            // Checking collision on second turn of race
            else if (turn == 2) {
                if (((transform.localPosition.x + 15) > (karts[i].transform.position.x - karts[i].length / 2)) &&
                        ((transform.localPosition.x + 15) < (karts[i].transform.position.x + karts[i].length / 2)) &&
                        ((transform.localPosition.z - 1.5) < karts[i].transform.position.z) &&
                        ((transform.localPosition.z + 1.5) > karts[i].transform.position.z)) {
                    if (!colliding[i]) {
                        bool isPlayer = (i == 0) ? true : false;
                        OnCollect(karts[i], isPlayer);
                        colliding[i] = true;
                    }
                }
                else if (colliding[i])
                    colliding[i] = false;
            }

            // Checking collision on third turn of the race
            else if (turn == 3) {
                if (((transform.position.x + 2.5) > karts[i].transform.position.x) &&
                        ((transform.position.x - 1.5) < karts[i].transform.position.x) &&
                        ((correct && (transform.position.z > (karts[i].transform.position.z - karts[i].length / 2)) &&
                        (transform.position.z < (karts[i].transform.position.z + karts[i].length / 2)))
                        || (!correct && ((transform.position.z - 20) > (karts[i].transform.position.z - karts[i].length / 2)) &&
                        ((transform.position.z - 20) < (karts[i].transform.position.z + karts[i].length / 2))))) {
                    if (!colliding[i]) {
                        bool isPlayer = (i == 0) ? true : false;
                        OnCollect(karts[i], isPlayer);
                        colliding[i] = true;
                    }
                }
                else if (colliding[i])
                    colliding[i] = false;
            }

            // Checking collision on fourth turn of the race
            else if (turn == 4) {
                if (((transform.position.x) > (karts[i].transform.position.x - karts[i].length / 2)) &&
                        ((transform.position.x) < (karts[i].transform.position.x + karts[i].length / 2)) &&
                        ((transform.position.z - 1.7) < karts[i].transform.position.z) &&
                        ((transform.position.z + 1.7) > karts[i].transform.position.z)) {
                    if (!colliding[i]) {
                        bool isPlayer = (i == 0) ? true : false;
                        OnCollect(karts[i], isPlayer);
                        colliding[i] = true;
                    }
                }
                else if (colliding[i])
                    colliding[i] = false;
            }
        }

    }

    void OnCollect(ArcadeKart kart, bool isPlayer)
    {
        if (correct)
            speedChange = (float) 0.5;
        else
            speedChange = (float) -0.5;

        // Kart speeds up or slows down based on if the answer is correct or not
        kart.changeKartSpeed(1 + speedChange);

        if (isPlayer) {
            if (CollectSound)
            {
                AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }

            gameFlow.incQuestion();

            if (choice) {
                if (correct) {
                    correctMessage.gameObject.SetActive(true);
                }
                else {
                    incorrectMessage.gameObject.SetActive(true);
                }
            }
        }
    }
}
