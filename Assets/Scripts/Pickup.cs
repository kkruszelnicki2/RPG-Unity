using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{

    [SerializeField] private CustomSignal pickUpSignal;
    override protected void Interaction()
    {
        pickUpSignal.Raise();
        Destroy(gameObject);
    }
}
