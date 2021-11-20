using UnityEngine;

namespace GameElements
{
    public class ToastScorer : MonoBehaviour
    {

        public float Radius;

        public float GetScore(GameObject obj)
        {
            var objPos = obj.transform.position;
            var curPos = this.transform.position;
            objPos.y = 0;
            curPos.y = 0;

            float dist = Vector3.Distance(objPos, curPos);
            float percentage = Radius / dist;
            if (obj.transform.up.y < 0)
            {
                Debug.Log("Jam side down");
                percentage /= 2;
            }




            return percentage;
        }
        
    }
}
