using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int rotationX, rotationY, rotationZ;

    void Update()
    {
        Rotation();
    }

    public virtual void Picked()
    {
        Debug.Log("Podnioslem");
        Destroy(this.gameObject);
    }

    public void Rotation()
    {
        transform.Rotate(new Vector3(rotationX, rotationY, rotationZ));
    }
}
