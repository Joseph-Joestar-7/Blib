using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : MonoBehaviour,I_Interactable
{
    [SerializeField] private GameObject bubblePrefab;

    [SerializeField] private Transform[] bubblePoints;
    public void Start()
    {
        Interact();
    }
    public void Interact()
    {
        foreach(Transform point in bubblePoints)
        {
            Instantiate(bubblePrefab, point.position, Quaternion.identity);
        }
    }
}
