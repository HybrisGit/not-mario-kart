using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarComponent))]
public class CarDriver : MonoBehaviour
{
    [HideInInspector]
    public CarController carController;


}
