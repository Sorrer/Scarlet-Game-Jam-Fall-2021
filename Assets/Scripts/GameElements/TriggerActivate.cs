using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerActivate : MonoBehaviour
{
    public UnityEvent Triggered;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        Triggered?.Invoke();
    }
}
