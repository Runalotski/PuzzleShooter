using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarrier
{
    Transform HeldObject { get; set; }
    void Grasp(Transform t);
    void Release();
}
