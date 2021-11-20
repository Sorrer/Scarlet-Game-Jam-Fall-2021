using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public List<GameObject> colliding = new();
    public ContactPoint[] contactPoints = new ContactPoint[0];
    public bool IsTriggered => colliding.Count > 0;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().isTrigger) return;   
        colliding.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        colliding.Remove(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        colliding.Add(other.gameObject);
        contactPoints = other.contacts;
    }

    private void OnCollisionExit(Collision other)
    {
        colliding.Remove(other.gameObject);
    }
}
