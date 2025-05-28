using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSensor : MonoBehaviour
{

    [SerializeField] private Platform platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exit");
            platform.EnablePlatform();
        }
    }
    
}
