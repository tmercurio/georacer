// Written by Thomas Mercurio and used for PlayAgain button

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    [Tooltip("What is the name of the scene we want to load when clicking the button?")]
    public string SceneName;

    public void OnPlayAgainButtonClick()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

}
