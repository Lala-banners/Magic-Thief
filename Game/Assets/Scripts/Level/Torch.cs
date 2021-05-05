using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (!EventSystem.current.IsPointerOverGameObject())
            Activate();
    }

    private void Activate()
    {
        GameManager.Instance.Activate(this);
    }

    public override void Interact()
    {
        isLit = !isLit;
        torchLight.enabled = isLit;
    }
}
