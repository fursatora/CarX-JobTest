using Runtime.Gameplay.SpawnSystem.SpawnedObjects;
using Runtime.TargetSystem;
using Runtime.TargetSystem.Contracts;
using UnityEngine;

namespace Runtime.Gameplay.SpawnSystem
{
    public class RecieverSpawnedObject : MainSpawnedObject, IHaveTargetReceiver
    {
        [SerializeField] private TargetReceiver receiver;
        public TargetReceiver Receiver => receiver;
    }
}