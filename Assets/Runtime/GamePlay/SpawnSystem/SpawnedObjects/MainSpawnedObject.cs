using Runtime.SpawnSystem;
using UnityEngine;

namespace Runtime.Gameplay.SpawnSystem.SpawnedObjects
{
    public abstract class MainSpawnedObject : MonoBehaviour, ISpawnedObject
    {
        public Transform Transform => transform;
    }
}