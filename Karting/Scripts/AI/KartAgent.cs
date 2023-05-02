// File taken from kart game Unity tutorial and edited by Thomas Mercurio

using KartGame.KartSystems;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace KartGame.AI
{
    /// <summary>
    /// Sensors hold information such as the position of rotation of the origin of the raycast and its hit threshold
    /// to consider a "crash".
    /// </summary>
    [System.Serializable]
    public struct Sensor
    {
        public Transform Transform;
        public float RayDistance;
        public float HitValidationDistance;
    }

    /// <summary>
    /// We only want certain behaviours when the agent runs.
    /// Training would allow certain functions such as OnAgentReset() be called and execute, while Inferencing will
    /// assume that the agent will continuously run and not reset.
    /// </summary>
    public enum AgentMode
    {
        Training,
        Inferencing
    }

    /// <summary>
    /// The KartAgent will drive the inputs for the KartController.
    /// </summary>
    public class KartAgent : Agent, IInput
    {
#region Training Modes
        [Tooltip("Are we training the agent or is the agent production ready?")]
        public AgentMode Mode = AgentMode.Training;
        [Tooltip("What is the initial checkpoint the agent will go to? This value is only for inferencing.")]
        public ushort InitCheckpointIndex;

#endregion

#region Senses
        [Header("Observation Params")]
        [Tooltip("What objects should the raycasts hit and detect?")]
        public LayerMask Mask;
        [Tooltip("Sensors contain ray information to sense out the world, you can have as many sensors as you need.")]
        public Sensor[] Sensors;
        [Header("Checkpoints"), Tooltip("What are the series of checkpoints for the agent to seek and pass through?")]
        public Collider[] Colliders;
        [Tooltip("What layer are the checkpoints on? This should be an exclusive layer for the agent to use.")]
        public LayerMask CheckpointMask;

        [Space]
        [Tooltip("Would the agent need a custom transform to be able to raycast and hit the track? " +
            "If not assigned, then the root transform will be used.")]
        public Transform AgentSensorTransform;
#endregion

#region Rewards
        [Header("Rewards"), Tooltip("What penatly is given when the agent crashes?")]
        public float HitPenalty = -1f;
        [Tooltip("How much reward is given when the agent successfully passes the checkpoints?")]
        public float PassCheckpointReward;
        [Tooltip("Should typically be a small value, but we reward the agent for moving in the right direction.")]
        public float TowardsCheckpointReward;
        [Tooltip("Typically if the agent moves faster, we want to reward it for finishing the track quickly.")]
        public float SpeedReward;
        [Tooltip("Reward the agent when it keeps accelerating")]
        public float AccelerationReward;
        #endregion

        #region ResetParams
        [Header("Inference Reset Params")]
        [Tooltip("What is the unique mask that the agent should detect when it falls out of the track?")]
        public LayerMask OutOfBoundsMask;
        [Tooltip("What are the layers we want to detect for the track and the ground?")]
        public LayerMask TrackMask;
        [Tooltip("How far should the ray be when casted? For larger karts - this value should be larger too.")]
        public float GroundCastDistance;
#endregion

#region Debugging
        [Header("Debug Option")] [Tooltip("Should we visualize the rays that the agent draws?")]
        public bool ShowRaycasts;
#endregion

        ArcadeKart m_Kart;
        bool m_Acceleration;
        bool m_Brake;
        float m_Steering;
        int m_CheckpointIndex;

        bool m_EndEpisode;
        float m_LastAccumulatedReward;

        public GameFlowManager gameFlow;

        private ArcadeKart player_kart;

        public Rigidbody Rigidbody;

        void Awake()
        {
            m_Kart = GetComponent<ArcadeKart>();
            if (AgentSensorTransform == null) AgentSensorTransform = transform;
        }

        void Start()
        {
            gameFlow = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameFlowManager>();
            player_kart = GameObject.FindGameObjectWithTag("Player").GetComponent<ArcadeKart>();
        }

        // Written by Thomas Mercurio, coding actions of AIKart
        void Update()
        {
            // Check if kart is within sight of the player and if it should speed up
            if (Math.Sqrt(Math.Pow((transform.position.x - player_kart.transform.position.x), 2) +
                Math.Pow((transform.position.z - player_kart.transform.position.z), 2)) < 20) {
                    m_Acceleration = true;
                    m_Brake = false;
                    // Car should be going around the turns (turning left) at these points
                    if ((transform.position.x > 5 && transform.position.z > 40) ||
                        (transform.position.x < -21 && transform.position.z > 50) ||
                        (transform.position.x < -30 && transform.position.z < -23) ||
                        (transform.position.x > 0 && transform.position.z < -27)) {
                        if (Rigidbody.velocity.magnitude > 5)
                            m_Steering = -0.8f;
                        else
                            m_Steering = -0.2f;
                    }
                    // Course-correct to go through the correct answer choice
                    else if ((transform.position.x < 0 && transform.position.x > -20 && transform.position.z > 60) ||
                        (transform.position.z < 50 && transform.position.z > 2 && transform.position.x < -43) ||
                        (transform.position.x > -30 && transform.position.x < -15 && (transform.position.z < -41 || transform.localRotation.y > 90))) {
                        m_Steering = -0.2f;
                    }
                    else if ((transform.position.x < 0 && transform.position.x > -20 && transform.position.z < 58.5 && transform.position.z > 50) ||
                        (transform.position.z < 50 && transform.position.z > 2 && transform.position.x > -41.5 && transform.position.x < -38) ||
                        (transform.position.x > -30 && transform.position.x < -15 && transform.position.z > -38 && transform.position.z < -36)) {
                        m_Steering = 0.2f;
                    }
                    else {
                        m_Steering = 0;
                    }
            }
            // If AI kart is not close to player kart, don't accelerate and don't turn anywhere
            else {
                m_Acceleration = false;
                m_Brake = false;
                m_Steering = 0;
            }
        }

        float Sign(float value)
        {
            if (value > 0)
            {
                return 1;
            }
            if (value < 0)
            {
                return -1;
            }
            return 0;
        }

        void InterpretDiscreteActions(ActionBuffers actions)
        {
            m_Steering = actions.DiscreteActions[0] - 1f;
            m_Acceleration = actions.DiscreteActions[1] >= 1.0f;
            m_Brake = actions.DiscreteActions[1] < 1.0f;
        }

        public InputData GenerateInput()
        {
            return new InputData
            {
                Accelerate = m_Acceleration,
                Brake = m_Brake,
                TurnInput = m_Steering
            };
        }
    }
}
