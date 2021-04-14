using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactable
{

    public bool isLit = true;

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
        Debug.Log("torch turned off");
    }
}
