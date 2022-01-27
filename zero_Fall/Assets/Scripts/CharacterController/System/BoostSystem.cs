using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class BoostSystem : SystemBase
{
    protected override void OnUpdate()
    {      
        Entities
            .ForEach((
                ref PhysicsVelocity velocity,
                in Rotation rotation,
                in BoostData boostData,
                in PlayerInputData playerInput) => {
                    if (playerInput.boosting)
                    {
                        var currentSpeed = Vector3.Magnitude(velocity.Linear);
                        var diffToMax = boostData.maxBoostSpeed - currentSpeed;

                        if (diffToMax > 0)
                        {
                            var boostToAdd = Mathf.Min(boostData.boostForce, diffToMax);
                            var direction = math.mul(rotation.Value, new float3(0f, 0f, 1f));
                            velocity.Linear += direction * boostToAdd;
                        }
                    }
        }).Schedule();
    }
}
