using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct MovementData : IComponentData
{
    public float movementSpeed;
    public float acceleration;
    public float3 target;
}
