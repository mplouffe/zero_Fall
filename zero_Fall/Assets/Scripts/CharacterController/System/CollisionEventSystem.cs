using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public class CollisionEventSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();
        buildPhysicsWorldSystem = World.GetExistingSystem<BuildPhysicsWorld>();
        stepPhysicsWorldSystem = World.GetExistingSystem<StepPhysicsWorld>();
        commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new CollisionEventJob().Schedule(
            stepPhysicsWorldSystem.Simulation,
            ref buildPhysicsWorldSystem.PhysicsWorld,
            inputDeps);
        commandBufferSystem.AddJobHandleForProducer(jobHandle);
        return jobHandle;
    }

    private BuildPhysicsWorld buildPhysicsWorldSystem;
    private StepPhysicsWorld stepPhysicsWorldSystem;
    private EndSimulationEntityCommandBufferSystem commandBufferSystem;

    [BurstCompile]
    struct CollisionEventJob : ICollisionEventsJob
    {
        public void Execute(CollisionEvent evt)
        {
            Debug.Log($"Collision between entities { evt.EntityA.Index } and { evt.EntityB.Index }");
        }
    }
}
