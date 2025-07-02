using System.Collections.Generic;
using System.Linq;
using Runtime.TargetSystem.Contracts;
using UnityEngine;

namespace Runtime.TargetSystem
{
    public static class TargetSelector 
    {
        private static readonly List<ITargetProvider> _providers = new();

        public static void Register(ITargetProvider provider)
        {
            if (!_providers.Contains(provider))
                _providers.Add(provider);
        }

        public static void Unregister(ITargetProvider provider)
        {
            if (_providers.Contains(provider))
                _providers.Remove(provider);
        }

        public static ITargetProvider GetNearest(Vector3 position)
        {
            return _providers
                .OrderBy(p => Vector3.Distance(position, p.Position))
                .FirstOrDefault();
        }
    }
}