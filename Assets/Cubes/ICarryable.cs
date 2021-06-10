using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarryable
{
    ICarrier Carrier { get; set; }
    bool BeingCarried { get; set; }
    void PickUp(ICarrier carrier);
    void PutDown();

}
