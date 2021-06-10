using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCube : MonoBehaviour, ICarryable
{ 

    Rigidbody rBody;
    public bool BeingCarried { get; set; }

    public ICarrier Carrier { get; set; }

    public float charge { get; set; }

    void Awake()
    {
        charge = 0;
        rBody = GetComponent<Rigidbody>();
    }

    public void PickUp(ICarrier carrier)
    {
        Carrier = carrier;
        rBody.isKinematic = true;
        BeingCarried = true;
    }

    public void PutDown()
    {
        rBody.isKinematic = false;
        BeingCarried = false;
        Carrier = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.collider.tag);

        if(collision.collider.tag == "OrbitingUI")
        {
            if (BeingCarried)
                Carrier.Release();

            collision.transform.root.GetComponent<ICarrier>().Grasp(this.transform);
        }
    }
}
