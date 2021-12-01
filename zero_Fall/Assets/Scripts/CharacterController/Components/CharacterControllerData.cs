using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct CharacterControllerData : IComponentData
{
    public float radius;                // (.25)
    public float height;                // (2)
    public float skin;                  // distance out from controller to check for objects we are touching, set to something small (0.01)
    public float maxAngle;              // max angle not to slide down (45)
    public bool onGround;
    public float3 footOffset;           // position collider by base instead of center
    public float3 moveDelta;            // distance to try and move
    public LayerMask layersToIgnore;    // an easy way to set layers

    // some things to make life easier
    public float3 center => footOffset + new float3(0, height / 2, 0);
    public float3 vertexTop => footOffset + new float3(0, height - radius, 0);
    public float3 vertexBottom => footOffset + new float3(0, radius, 0);
    public float3 top => footOffset + new float3(0, height, 0);
    public CollisionFilter Filter
    {
        get
        {
            return new CollisionFilter()
            {
                BelongsTo = (uint)(~layersToIgnore.value),
                CollidesWith = (uint)(~layersToIgnore.value)
            };
        }
    }
}
