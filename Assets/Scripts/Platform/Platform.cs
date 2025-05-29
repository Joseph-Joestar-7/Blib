using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //[SerializeField] private int level;
    //public int Level => level;
    private Collider2D col;
    public void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void DisablePlatform()
    {
        
        col.enabled = false;
    }

    public void EnablePlatform()
    {
        col.enabled = true;
    }
}
