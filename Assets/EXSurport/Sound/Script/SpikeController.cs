using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("13");
            collision.gameObject.GetComponent<PlayerController>().SpikeTrigger();
        }

    }
}
