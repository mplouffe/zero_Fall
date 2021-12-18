using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class FollowTargetMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities
            .WithoutBurst()
            .ForEach((ref PhysicsVelocity velocity, in FollowMovementData movement) => {
                var distance = Vector3.Magnitude(movement.target);

                var currentSpeed = Vector3.Magnitude(velocity.Linear);
                var diffToMax = currentSpeed - movement.maxSpeed;

                if (currentSpeed < movement.maxSpeed)
                {
                    if (distance < movement.falloffBoundary)
                    {
                        velocity.Linear -= ((movement.falloffBoundary - distance) / movement.falloffBoundary) * velocity.Linear * deltaTime;
                    }
                    else
                    {
                        var accelerationToAdd = Mathf.Min(movement.acceleration, movement.maxSpeed - currentSpeed);
                        velocity.Linear += math.normalize(movement.target) * accelerationToAdd * deltaTime;
                    }
                }
                else
                {
                    velocity.Linear -= velocity.Linear;
                }

        }).Schedule();
    }
}
