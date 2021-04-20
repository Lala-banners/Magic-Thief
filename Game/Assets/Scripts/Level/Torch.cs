using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactable
{
    private Light torchLight;
    public bool isLit = true;

    private void Start()
    {
        torchLight = GetComponentInChildren<Light>();
    }

    private void OnMouseDown()
    {
        Activate();
    }

    private void Activate()
    {
        GameManager.Instance.Activate(this);
    }

    public override void Interact()
    {
        isLit = false;
        torchLight.enabled = isLit;
        Debug.Log("torch turned off");
    }
}
