using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowToast : MonoBehaviour
{
    public Transform follow;

    public float LerpSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Lerp(pos.x, follow.position.x, Time.deltaTime * LerpSpeed);
        this.transform.position = pos;
    }
}
