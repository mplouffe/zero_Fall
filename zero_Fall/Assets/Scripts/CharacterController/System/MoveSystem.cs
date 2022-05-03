using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class MoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref PhysicsVelocity velocity, in MovementData movement) =>
        {   
            var normalizedLinearVelocity = math.normalize(velocity.Linear);
            var velocityTargetDot = math.dot(normalizedLinearVelocity, movement.target);

            var accelerationToAdd = float3.zero;
            
            if (velocityTargetDot >= -0.5)
            {
                var currentSpeed = Vector3.Magnitude(velocity.Linear);
                var diffToMax = movement.maxSpeed - currentSpeed;
                if (diffToMax > 0)
                {
                    accelerationToAdd = Mathf.Min(movement.acceleration, diffToMax);
                }
            }
            else
            {
                accelerationToAdd = movement.maxSpeed;
            }

            velocity.Linear += movement.target * accelerationToAdd;
        }).Run();
    }
}
