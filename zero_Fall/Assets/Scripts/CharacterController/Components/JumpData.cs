using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct JumpData : IComponentData
{
    public float jumpForce;
    public float3 jumpDirection;
}
