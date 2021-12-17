using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct FollowMovementData : IComponentData
{
    public float movementSpeed;
    public float3 target;
    public float distanceToTarget;
}
