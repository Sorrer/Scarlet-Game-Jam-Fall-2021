using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace GameElements
{
    public class ToastScorer : MonoBehaviour
    {

        public float Radius;

        public TriggerObject triggerObject;
        
        public UnityEvent OnScoreEvent;

        private bool waitingForScore = false;

        public void StartScore()
        {
            if(!waitingForScore)StartCoroutine(WaitScore(ToastController.instance));
        }
        
        private void Score(ToastController toastController)
        {
            float largestScore = 0;
            foreach (Transform child in toastController.transform)
            {
                float score = GetScore(child.gameObject, toastController.transform.up);

                if (largestScore < score)
                {
                    largestScore = score;
                }
            }
            
            
            waitingForScore = false;
            Debug.Log("SCORE " + largestScore);
            toastController.ReturnHome();
            OnScoreEvent?.Invoke();
            
        }

        private IEnumerator WaitScore(ToastController toast)
        {
            waitingForScore = true;
            while (!toast.isFrozen)
            {
                yield return null;
            }

            if (triggerObject.IsTriggered)
            {
                bool isThere = false;
                foreach (Transform child in toast.transform)
                {
                    if (!triggerObject.colliding.Contains(child.gameObject))
                    {
                        isThere = true;
                        break;
                    }
                }
                
                if (!isThere)
                {
                    yield break;
                }
            }
            
            Score(toast);
        }

        public float GetScore(GameObject obj, Vector3 up)
        {
            var objPos = obj.transform.position;
            var curPos = this.transform.position;
            objPos.y = 0;
            objPos.z = 0;
            curPos.y = 0;
            curPos.z = 0;
            
            float dist = Vector3.Distance(objPos, curPos);
            float percentage = Radius / dist;
            if (up.y < 0)
            {
                Debug.Log("Jam side down");
                percentage /= 2;
            }
            else
            {
                Debug.Log(up);
                Debug.Log("Jam side up");
            }

            
            
            
            return percentage;
        }
        
    }
}
