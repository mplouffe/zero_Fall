using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class ConvertCamera : MonoBehaviour, IConvertGameObjectToEntity
{
    public EntityManager entityManager;

    public Entity targetEntity, lookAtEntity;
    public float3 offset;
    public float movementSpeed;
    public float rotateSpeed;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<CopyTransformToGameObject>(entity);

        dstManager.AddComponentData(entity, new FollowMovementData { movementSpeed = movementSpeed });
        dstManager.AddComponentData(entity, new TargetData { followEntity = targetEntity, lookAtEntity = lookAtEntity, targetOffset = offset });
        dstManager.AddComponentData(entity, new RotateData { rotateSpeed = rotateSpeed });
    }

    // Start is called before the first frame update
    void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }
}
