using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TransformLinkEntity : MonoBehaviour, IConvertGameObjectToEntity
{
    public TransformLink transformLink;

    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        transformLink.target = entity;
    }
}
