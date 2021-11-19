using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class ToastController : MonoBehaviour
    {

        public Rigidbody _rigidbody;
        public MasterSettings _masterSettings;
        public bool isFrozen = false;

        private bool isInvulnerable = false;
        public bool isFalling = false;
        private float isFallingCheckFreezeTime = 2f;

        private Coroutine CheckFreezeFall;
        
        private void Start()
        {
            FreezeToast();
        }

        private void Update()
        {
            if (!isFrozen && !isFalling)
            {
                _rigidbody.angularVelocity = _masterSettings.ToastConstantRotationSpeed;
            }

            if (isFalling && _rigidbody.velocity.magnitude == 0)
            {
                if (CheckFreezeFall == null)
                {
                    CheckFreezeFall = StartCoroutine(CheckFreezeFallCoroutine());
                }
            }
            else
            {
                if (CheckFreezeFall != null)
                {
                    StopCoroutine(CheckFreezeFall);
                    CheckFreezeFall = null;
                }
            }
        }

        private IEnumerator CheckFreezeFallCoroutine()
        {
            yield return new WaitForSeconds(isFallingCheckFreezeTime);
            FreezeToast();
        }

        public void StopToast()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            isFalling = true;
        }

        public void FreezeToast()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            isFrozen = true;
            isFalling = false;
        }

        public void UnfreezeToast()
        {            
            isFrozen = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }

        public void LaunchToast(Vector2 vectorForce)
        {
            float magnitude = vectorForce.magnitude;
            vectorForce /= magnitude;
            vectorForce *= Mathf.Clamp(magnitude, float.Epsilon, _masterSettings.MaxToastLaunchStrength);
            if (magnitude == 0) return;
            Debug.Log("Force being applied - " + vectorForce + " " + magnitude);
            
            UnfreezeToast();
            _rigidbody.AddForce(vectorForce, ForceMode.Impulse);
        }

        
        public bool IsFaceDown()
        {
            return true;
        }


        private void OnCollisionEnter(Collision other)
        {
            FreezeToast();
        }
        
        
    }
}