// Code inspired by looking at https://learn.unity.com/tutorial/implement-data-persistence-between-scenes
// and used to keep track of variables across scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int numCorrect;

    public int level;

    private void Awake() {

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
