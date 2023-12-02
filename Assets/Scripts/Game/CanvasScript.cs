using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject pickupMessager;
    public void PickupMessage()
    {
        pickupMessager.SetActive(true);
        StartCoroutine(DisableMessager());
    }
    
    private IEnumerator DisableMessager()
    {
        yield return new WaitForSeconds(3);
        pickupMessager.SetActive(false);
    }
}
