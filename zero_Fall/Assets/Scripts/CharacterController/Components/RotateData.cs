using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct RotateData : IComponentData
{
    public float rotateSpeed;
    public float3 rotateTargetPosition;
}
