using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct PlayerMovementData : IComponentData
{
    public float maxSpeed;
    public float acceleration;
    public float airAcceleration;
    public float groundFriction;
    public float groundingDistance;
    public float3 gravity;
    public float3 velocity;
}
