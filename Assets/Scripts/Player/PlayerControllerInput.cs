using System;
using System.Collections;
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
                Debug.Log(Input.mousePosition);
                StartDrag(Input.mousePosition);
                EndPointDEBUG.gameObject.SetActive(true);
                StartPointDEBUG.gameObject.SetActive(true);
                
                IsDraggin = true;
            }

            if (IsDraggin)
            {
                EndPointDEBUG.position = Input.mousePosition;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                if(IsDraggin) EndDrag(Input.mousePosition);
                IsDraggin = false;
            }

            var touches = Input.touches;
            
            
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

            Debug.Log("Drag start end - " + StartDragPos + " " + EndDragPos);
            Vector3 dir = (StartDragPos - EndDragPos);
            
            ToastController.LaunchToast(dir * masterSettings.ToastLaunchStrength);
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
