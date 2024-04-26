using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        Manager.instance.BoxClick(this.gameObject);
    }
}
