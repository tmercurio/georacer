using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThankYouScript : MonoBehaviour
{
    public TMP_Text thankYou;

    // Start is called before the first frame update
    void Start()
    {
        if (MainManager.Instance != null) {
            thankYou.text = "Thanks for playing! You got " + MainManager.Instance.numCorrect + " out of 4 quiz questions correct";
        }
    }
}
