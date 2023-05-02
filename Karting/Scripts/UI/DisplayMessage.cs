// File taken from kart game Unity tutorial and edited by Thomas Mercurio to display geography questions

using System.Collections;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{
    public GameFlowManager gameFlow;

    [Tooltip("The text that will be displayed")]
    [TextArea]
    public string message;
    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;
    [Tooltip("Delay before displaying the message")]
    public float delayBeforeShowing;


    float m_InitTime = float.NegativeInfinity;

    public bool autoDisplayOnAwake;
    bool m_WasDisplayed;
    DisplayMessageManager m_DisplayMessageManager;

    public bool question;
    public int questionNum;

    // Used for recurring messages like correct and incorrect
    public bool recurring;

    private NotificationToast notification;


    void Start() {
        gameFlow = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameFlowManager>();
    }

    void OnEnable()
    {
        m_InitTime = Time.time;
        if (m_DisplayMessageManager == null)
            m_DisplayMessageManager = FindObjectOfType<DisplayMessageManager>();

        DebugUtility.HandleErrorIfNullFindObject<DisplayMessageManager, DisplayMessage>(m_DisplayMessageManager, this);


        m_WasDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!autoDisplayOnAwake) return;
        if (m_WasDisplayed) return;

        if (Time.time - m_InitTime > delayBeforeShowing) {
            if (!question)
                Display();
            else if (questionNum == gameFlow.curQuestion)
                Display();
        }

    }

    public void Display()
    {
        notification = messagePrefab.getObject(true,m_DisplayMessageManager.DisplayMessageRect.transform).GetComponent<NotificationToast>();

        notification.Initialize(message);

        m_DisplayMessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);

        m_WasDisplayed = true;

        StartCoroutine(messagePrefab.ReturnWithDelay(notification.gameObject,notification.TotalRunTime));

    }


}
