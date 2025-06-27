using System;
using System.Collections.Generic;
using Runtime.MoveSystem.Contracts;
using Runtime.MoveSystem.MoveSettings;

namespace Runtime.MoveSystem.MoveTrajectories
{
    public class ScriptableTrajectoriesFactory
    {
        private static readonly Dictionary<Type, Func<BaseTrajectorySettings, IMoveTrajectory>> _builders = new()
        {
            {
                typeof(LinearTrajectorySettingsSO),
                settings =>
                {
                    var s = (LinearTrajectorySettingsSO)settings;
                    return new LinearTrajectory(s.Speed);
                }
            },

            {
                typeof(SinusTrajectorySettingsSO),
                settings =>
                {
                    var s = (SinusTrajectorySettingsSO)settings;
                    return new SinusTrajectory(s.Speed, s.Amplitude, s.Frequency);
                }
            }
        };

        public IMoveTrajectory GetMethodBySettings(BaseTrajectorySettings settings)
        {
            if (_builders.TryGetValue(settings.GetType(), out var builder))
                return builder(settings);
            return null;
        }
    }
}