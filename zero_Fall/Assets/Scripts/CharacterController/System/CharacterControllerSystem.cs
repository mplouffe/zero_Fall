using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public class CharacterControllerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        //UnityEngine.Debug.Log("Character Controller System Running");
        //var physicsWorldSystem = World.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
        //CollisionWorld collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;
        //EntityManager entityManager = EntityManager;
        //JobHandle controllerJob = Entities.WithReadOnly(collisionWorld).ForEach(
        //    (ref CharacterControllerData characterController, ref Translation translation, ref DynamicBuffer<BounceNormals> bounceNormalsBuffer) =>
        //    {
        //        bounceNormalsBuffer.Clear();
        //        DynamicBuffer<float3> bounceNormals = bounceNormalsBuffer.Reinterpret<float3>();

        //        Move(ref characterController, ref translation, ref bounceNormals, collisionWorld);
        //        characterController.onGround = GetGrounded(bounceNormals, characterController);
        //        characterController.moveDelta = float3.zero;
        //    }).ScheduleParallel(JobHandle.CombineDependencies(Dependency, physicsWorldSystem.GetOutputDependency()));
        //controllerJob.Complete();

        var physicsWorldSystem = World.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
        CollisionWorld collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;
        Entities
            .WithReadOnly(collisionWorld)
            .ForEach((ref JumpData jumpData, ref Translation translation, ref PhysicsCollider collider) =>
            {

            }).Run();
    }

    //private static unsafe void Move(
    //    ref CharacterControllerData characterController,
    //    ref Translation translation,
    //    ref DynamicBuffer<float3> bounceNormals,
    //    in CollisionWorld collisionWorld)
    //{
    //    var filter = characterController.Filter;
    //    CapsuleGeometry capsuleGeometry = new CapsuleGeometry()
    //    {
    //        Vertex0 = characterController.vertexTop,
    //        Vertex1 = characterController.vertexBottom,
    //        Radius = characterController.radius
    //    };
    //    BlobAssetReference<Collider> capsuleCollider = CapsuleCollider.Create(capsuleGeometry, filter);
    //    float3 delta = characterController.moveDelta;
    //    var hits = new NativeList<DistanceHit>(Allocator.Temp);

    //    CheckForClipping(ref delta, ref translation, ref bounceNormals, characterController, collisionWorld);

    //    translation.Value += delta;

    //    ColliderDistanceInput collisionCheck = new ColliderDistanceInput()
    //    {
    //        Collider = (Collider*)capsuleCollider.GetUnsafePtr(),
    //        MaxDistance = 0,
    //        Transform = new RigidTransform(quaternion.identity, translation.Value)
    //    };

    //    hits.Clear();
    //    if (collisionWorld.CalculateDistance(collisionCheck, ref hits))
    //    {
    //        var bounces = GetBounces(hits);
    //        if (CheckForWalls(bounces, characterController.maxAngle))
    //        {
    //            for (int i = 0; i < bounces.Length; i++)
    //            {
    //                bounceNormals.Add(bounces[i]);
    //            }
    //            translation.Value -= GetBounceNormal(bounces);
    //        }
    //        else
    //        {
    //            SnapToGround(hits, ref translation, ref characterController);
    //            bounceNormals.Add(new float3(0, -1, 0));
    //        }
    //    }
    //    hits.Dispose();
    //}

    //private static unsafe bool CheckForClipping(
    //    ref float3 delta,
    //    ref Translation translation,
    //    ref DynamicBuffer<float3> bounces,
    //    in CharacterControllerData characterController,
    //    in CollisionWorld collisionWorld)
    //{
    //    var geometry = new SphereGeometry()
    //    {
    //        Radius = 0.01f
    //    };
    //    BlobAssetReference<Collider> collider = SphereCollider.Create(geometry, characterController.Filter);

    //    var bodyCheckInput = new ColliderCastInput()
    //    {
    //        Collider = (Collider*)collider.GetUnsafePtr(),
    //        Orientation = quaternion.identity,
    //        Start = translation.Value + characterController.center,
    //        End = translation.Value + characterController.center + delta
    //    };

    //    if (collisionWorld.CastCollider(bodyCheckInput, out ColliderCastHit bodyHit))
    //    {
    //        bounces.Add(bodyHit.SurfaceNormal);
    //        delta *= bodyHit.Fraction;
    //        return true;
    //    }
    //    return false;
    //}

    //private static void SnapToGround(NativeList<DistanceHit> hits, ref Translation translation, ref CharacterControllerData characterController)
    //{
    //    float maxHeight = 0;
    //    for (int i = 0; i < hits.Length; i++)
    //    {
    //        float3 delta = hits[i].Position - (translation.Value + characterController.footOffset);
    //        float angle = math.acos(math.distance(delta.ProjectOnPlane(new float3(0, 1, 0)), float3.zero) / characterController.radius);
    //        float offset = characterController.radius = math.sin(angle) * characterController.radius;

    //        if (delta.y - offset > maxHeight)
    //        {
    //            maxHeight = delta.y - offset;
    //        }
    //    }

    //    translation.Value += new float3(0, maxHeight, 0);
    //}

    //private static float3 GetBounceNormal(NativeArray<float3> bounces)
    //{
    //    float3 maxDists = float3.zero;
    //    for (int i = 0; i < bounces.Length; i++)
    //    {
    //        float3 vector = bounces[i];
    //        maxDists = new float3()
    //        {
    //            x = (math.abs(maxDists.x) < math.abs(vector.x)) ? vector.x : maxDists.x,
    //            y = (math.abs(maxDists.y) < math.abs(vector.y)) ? vector.y : maxDists.y,
    //            z = (math.abs(maxDists.z) < math.abs(vector.z)) ? vector.z : maxDists.z
    //        };
    //    }
    //    return maxDists;
    //}

    //private static bool GetGrounded(in DynamicBuffer<float3> bounceNormals, in CharacterControllerData characterController)
    //{
    //    for (int i = 0; i < bounceNormals.Length; i++)
    //    {
    //        if (bounceNormals[i].AngleFrom(new float3(0, -1, 0)) < math.radians(characterController.maxAngle))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //private static NativeList<float3> GetBounces(NativeList<DistanceHit> hits)
    //{
    //    var bounces = new NativeList<float3>(Allocator.Temp);
    //    for (int i = 0; i < hits.Length; i++)
    //    {
    //        bounces.Add(hits[i].SurfaceNormal * hits[i].Distance);
    //    }
    //    return bounces;
    //}

    //private static bool CheckForWalls(NativeArray<float3> bounces, float maxAngle)
    //{
    //    for (int i = 0; i < bounces.Length; i++)
    //    {
    //        if (bounces[i].AngleFrom(new float3(0, -1, 0)) > math.radians(maxAngle))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
}
