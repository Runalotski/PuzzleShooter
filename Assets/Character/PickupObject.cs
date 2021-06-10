using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour, ICarrier
{
    RaycastHit hit;
    int ignorePlayerMask;

    public Transform HeldObject { get; set; }
    Vector3 holdOffset = new Vector3(0,0.75f, 2);

    // Start is called before the first frame update
    void Start()
    {
        ignorePlayerMask = ~(1 << (int)GlobaValues.UnityLayers.Player);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if(HeldObject != null)
            {
                Release();
            }
            else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4, ignorePlayerMask))
            {
                if (hit.transform.GetComponent<ICarryable>() != null)
                {
                    ICarryable ic = hit.transform.GetComponent<ICarryable>();
                    if (ic.BeingCarried)
                        ic.Carrier.Release();

                    Grasp(hit.transform);
                }
            }
        }

        if(HeldObject != null)
        {
            HoldItem();
        }
    }

    public void Grasp(Transform t)
    {
        t.GetComponent<ICarryable>().PickUp(this);
        HeldObject = t;
        HeldObject.transform.position = transform.TransformPoint(holdOffset);
        HeldObject.transform.rotation = Quaternion.identity;
    }

    public void HoldItem()
    {
        HeldObject.transform.position = transform.TransformPoint(holdOffset);
        HeldObject.transform.forward = transform.forward;
    }

    public void Release()
    {
        HeldObject.GetComponent<ICarryable>().PutDown();
        HeldObject.GetComponent<Rigidbody>().velocity = GetComponent<CharacterController>().velocity;
        HeldObject = null;
    }
}
