using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[InternalBufferCapacity(7)]
public struct BounceNormals : IBufferElementData
{
    public float3 Value;
}
