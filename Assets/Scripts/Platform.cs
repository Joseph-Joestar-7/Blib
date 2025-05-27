using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour,I_Platform
{
    [SerializeField] private int level;
    private Collider2D col;
    public void Start()
    {
        col = GetComponent<Collider2D>();
    }

    public void DisablePlatform()
    {
        col.enabled = false;
    }

    public void EnablePlatform()
    {
        col.enabled = true;
    }

    public int getLevel()
    {
        return level;
    }
}
