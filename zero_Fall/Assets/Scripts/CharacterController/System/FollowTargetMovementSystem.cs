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
                Debug.Log("distance: " + distance);
                var currVelocity = Vector3.Magnitude(velocity.Linear);
                Debug.Log("velocity: " + currVelocity);

                if (distance < 5)
                {
                    velocity.Linear -= ((5 - distance)/ distance) * velocity.Linear * deltaTime;
                }
                else
                {
                    velocity.Linear += movement.target * movement.movementSpeed * deltaTime;
                }

        }).Schedule();
    }
}
