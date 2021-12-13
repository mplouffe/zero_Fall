using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct BoostData : IComponentData
{
    public float boostForce;
    public float3 boostDirection;
    public float boostCooldown;
    public bool onGround;
}
