using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCubeNucleus : MultiCube, ICarrier
{
    float orbitPhase = 0;
    float orbitX;
    float orbitY;

    Vector3 orbitLocation = Vector3.zero;

    public float orbitSize = 5;
    public float orbitSpeed = 0.5f;
    public Transform orbitingBody;

    public Transform OrbitSlotUIPrefab;
    public Transform orbitSlotUI;

    public Transform HeldObject { get; set; }

    void Update()
    {
        ProgressOrbit();

        if(orbitingBody == null)
        {
            if (orbitSlotUI == null)
                orbitSlotUI = Instantiate(OrbitSlotUIPrefab, this.transform);

            orbitSlotUI.position = orbitLocation;
        }
        else
        {
            if (orbitSlotUI != null)
                Destroy(orbitSlotUI.gameObject);

            orbitingBody.position = orbitLocation;
        }
    }

    void ProgressOrbit()
    {
        orbitPhase += Time.deltaTime * orbitSpeed;

        orbitX = Mathf.Sin(orbitPhase);
        orbitY = Mathf.Cos(orbitPhase);

        orbitLocation = transform.TransformPoint(new Vector3(orbitX, 0, orbitY) * orbitSize);
    }

    public void Grasp(Transform t)
    {
        t.GetComponent<ICarryable>().PickUp(this);
        orbitingBody = t;
    }

    public void Release()
    {
        orbitingBody = null;
    }
}
