using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Platform : MonoBehaviour
{
    [SerializeField] private int level;
    
    private Collider2D col;
    public void Start()
    {
        col = GetComponent<BoxCollider2D>();
        Player.Instance.OnLevelChanged += Player_LevelChanged;
    }

    private void Player_LevelChanged(object sender, Player.OnLevelChangedArgs e)
    {
        if (e.level < level)
        { 
            if(col.isTrigger)
                col.isTrigger=false;
            return;
        }

        col.isTrigger = true;
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
