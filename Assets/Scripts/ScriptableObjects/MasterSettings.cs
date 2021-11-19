using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    
    [CreateAssetMenu(fileName = "MasterSettings", menuName = "SO/MasterSettings")]
    public class MasterSettings : ScriptableObject
    {
        /// <summary>
        /// Game forward that the level will head towards, and the toast will launch towards
        /// </summary>
        public Vector3 GameForward;

        /// <summary>
        /// Toast move strength
        /// </summary>
        public float ToastLaunchStrength;
        
        
        public float MaxToastLaunchStrength;
        
        /// <summary>
        /// Speed the toast constantly rotates as
        /// </summary>
        public Vector3 ToastConstantRotationSpeed;



        public float ToastInvulnerableTime;


    }
}