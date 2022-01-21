using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class ClickObject : SerializedMonoBehaviour
{
    public virtual void Click()
    {
        Debug.Log("!!");
    }

    private bool inMouse;
    private void OnMouseUp()
    {
        if (inMouse)
        {
            Click();
        }
    }

    private void OnMouseEnter()
    {
        inMouse = true;
    }

    private void OnMouseExit()
    {
        inMouse = false;
    }
}
