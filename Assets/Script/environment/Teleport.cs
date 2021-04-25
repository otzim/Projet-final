using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    Vector3 destination;

    private void OnCollisionEnter(Collision col)
    {
        if (this.name =="Portail1")
        {
            destination = GameObject.Find("Portail2").transform.position;
        }
        else
        {
            destination = GameObject.Find("Portail1").transform.position;
        }

        col.transform.position = destination - Vector3.forward * 3;
        col.transform.Rotate(Vector3.up * 180);
    }
}
