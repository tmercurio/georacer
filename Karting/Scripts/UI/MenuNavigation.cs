using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuNavigation : MonoBehaviour
{
    public Selectable defaultSelection;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void LateUpdate()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetButtonDown(GameConstants.k_ButtonNameSubmit)
                || Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal) != 0
                || Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
            {
                EventSystem.current.SetSelectedGameObject(defaultSelection.gameObject);
            }
        }
    }

    public void level1Clicked() {
        //MainManager.Instance.level = 1;
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void level2Clicked() {
        //MainManager.Instance.level = 2;
        SceneManager.LoadSceneAsync("MainScene");
    }
}
