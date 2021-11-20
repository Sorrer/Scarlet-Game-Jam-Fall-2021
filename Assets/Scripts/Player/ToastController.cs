using System;
using System.Collections;
using System.Runtime.CompilerServices;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class ToastController : MonoBehaviour
    {

        public Rigidbody _rigidbody;
        public MasterSettings _masterSettings;
        public bool isFrozen = false;

        public Transform home;
        public TriggerObject triggerObject;
        private bool isInvulnerable = false;
        public bool isFalling = false;
        private float isFallingCheckFreezeTime = 3f;

        private Coroutine CheckFreezeFall;
        private ContactPoint[] _contactPoints = new ContactPoint[0];
        private bool freezeNextFrame;

        private const int FROZEN_FRAME_CHECK = 10;
        private int frozenFramesCount;
        
        private void Start()
        {
            FreezeToast();
        }

        private void Update()
        {

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

        private void FixedUpdate()
        {
            if (this.transform.position.y < -100) ReturnHome();
            
            if (freezeNextFrame)
            {
                if (frozenFramesCount != FROZEN_FRAME_CHECK)
                {
                    if (_rigidbody.velocity.magnitude != 0)
                    {
                        frozenFramesCount = 0;
                    }
                    else
                    {
                        frozenFramesCount++;
                    }

                    return;
                }
                
                
                isFrozen = true;
                isFalling = false;
                
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                freezeNextFrame = false;
                
                
                if (triggerObject.IsTriggered)
                {
                    ReturnHome();
                }
            }
            else
            {
                
                if (!isFrozen && !isFalling)
                {
                    //_rigidbody.angularVelocity = _masterSettings.ToastConstantRotationSpeed;
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
            if (freezeNextFrame) return;
            freezeNextFrame = true;
            frozenFramesCount = 0;
        }

        public void UnfreezeToast()
        {            
            isFrozen = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }

        public void LaunchToast(Vector2 vectorForce, float rotationMultiplier)
        {
            vectorForce = vectorForce +  _masterSettings.ToastInitialLaunchStrength;
            float magnitude = vectorForce.magnitude;
            vectorForce /= magnitude;
            vectorForce *= Mathf.Clamp(magnitude, float.Epsilon, _masterSettings.MaxToastLaunchStrength);
            if (magnitude == 0) return;
            Debug.Log("Force being applied - " + vectorForce + " " + magnitude);

            UnfreezeToast();
            _rigidbody.AddForce(vectorForce, ForceMode.Impulse);
            _rigidbody.AddTorque(_masterSettings.ToastConstantRotationSpeed * rotationMultiplier, ForceMode.Impulse);
            FreezeToast();
        }


        
        public bool IsFaceDown()
        {
            return false;
        }


        private void OnCollisionEnter(Collision other)
        {
            //_contactPoints = other.contacts;
            //FreezeToast();
        }


        public void ReturnHome(bool freeze = true)
        {
            if(freeze) FreezeToast();
            this.transform.position = home.transform.position;
            this.transform.eulerAngles = Vector3.zero;
        }
        
        
        
    }
}