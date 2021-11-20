using System;
using System.Collections;
using DigitalRubyShared;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerControllerInput : MonoBehaviour
    {

        public MasterSettings masterSettings;

        public ToastController ToastController;

        public RectTransform StartPointDEBUG;
        public RectTransform EndPointDEBUG;
        

        public bool IsDraggin = false;

        private Vector3 StartDragPos, EndDragPos;
        private Coroutine debugEndDrag;

        public CameraMover cameraMover;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsDraggin && !ToastController.isFalling)
            {
                if (!ToastController.isFrozen)
                {
                    ToastController.StopToast();
                    return;
                }
                
                
                //ToastController.LaunchToast((Vector3.right + Vector3.up).normalized * masterSettings.ToastLaunchStrength);
                //Debug.Log(Input.mousePosition);
                StartDrag(Input.mousePosition);
                EndPointDEBUG.gameObject.SetActive(true);
                StartPointDEBUG.gameObject.SetActive(true);
                
                IsDraggin = true;
            }

            if (IsDraggin)
            {
                EndPointDEBUG.position = Input.mousePosition;

                if (Input.mousePosition.x > StartDragPos.x)
                {
                    cameraMover.InvertTargetOfStart();
                }
                else
                {
                    cameraMover.TargetOfStart();
                }
                
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                if(IsDraggin) EndDrag(Input.mousePosition);
                IsDraggin = false;
            }

            var touches = Input.touches;

            
            
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
            //Debug.Log(Vector3.Dot((screenCenter - Input.mousePosition).normalized, Vector3.up));
            
        }

        public void StartDrag(Vector3 position)
        {
            position.z = 0;
            StartDragPos = position;
            
            
            StartPointDEBUG.gameObject.SetActive(true);
            StartPointDEBUG.position = position;
            
            if(debugEndDrag != null) StopCoroutine(debugEndDrag);
        }

        public void EndDrag(Vector3 position)
        {
            position.z = 0;
            EndDragPos = position;
            
            EndPointDEBUG.gameObject.SetActive(true);
            EndPointDEBUG.position = position;
            debugEndDrag = StartCoroutine(DISABLE_DEBUG());

            //Debug.Log("Drag start end - " + StartDragPos + " " + EndDragPos);
            Vector3 dir = (StartDragPos - EndDragPos);
            
            float halfWidth = Screen.width / 2;
            halfWidth *= masterSettings.halfScreenMaxPullPercentage;

            Debug.Log(dir.normalized);
            
            float newX = dir.x / halfWidth;
            float newY = dir.y * (newX / dir.x);

            dir.x = newX;
            dir.y = newY;
            
            Debug.Log(dir.normalized);
            Debug.Log(dir.sqrMagnitude);
            Debug.Log("Hello World!");
            if (dir.sqrMagnitude == 0 || float.IsNaN(dir.sqrMagnitude))
            {
                return;
            }
            
            ToastController.LaunchToast(dir * masterSettings.ToastLaunchStrength, dir.x < 0 ? -1.0f : 1.0f);
        }

        public IEnumerator DISABLE_DEBUG()
        {
            yield return new WaitForSeconds(1.0f);
            EndPointDEBUG.gameObject.SetActive(false);
            StartPointDEBUG.gameObject.SetActive(false);
        }

        public Vector3 ConvertPointsToTrajectory(Vector2 firstPoint, Vector2 secondPoint)
        {
            Vector3 amg = Vector3.zero;
            
            Vector2 dir = secondPoint - firstPoint;

            amg = dir.normalized;

            amg *= masterSettings.ToastLaunchStrength;
            
            return amg;
        }
        
        
    }
}
