using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BounceNormalsAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddBuffer<BounceNormals>(entity);
    }
}
