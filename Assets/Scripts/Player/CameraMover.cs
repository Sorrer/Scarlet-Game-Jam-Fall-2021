using System;
using UnityEngine;

namespace Player
{
    public class CameraMover : MonoBehaviour
    {

        public float LerpSpeed = 5f;
        
        private Vector3 startAngle, startPosition;

        private Vector3 targetAngle, targetPosition;
        private void Start()
        {
            startAngle = this.transform.localEulerAngles;
            startPosition = this.transform.localPosition;

            targetAngle = startAngle;
            targetPosition = startPosition;
        }

        private void Update()
        {
            var euler = this.transform.localEulerAngles;

            euler.y = Mathf.LerpAngle(euler.y, targetAngle.y, LerpSpeed * Time.deltaTime);
            this.transform.eulerAngles = euler;
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, targetPosition, LerpSpeed * Time.deltaTime);
        }


        public void InvertTargetOfStart()
        {
            targetAngle.y = -1 * startAngle.y;
            targetPosition.x = -1 * startPosition.x;
        }

        public void TargetOfStart()
        {
            targetAngle = startAngle;
            targetPosition = startPosition;
        }
        
    }
}