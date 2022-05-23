using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : MonoBehaviour
{
    public virtual void ItemAction()
    {

    }

    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 4)
            ItemAction();

    }
}
