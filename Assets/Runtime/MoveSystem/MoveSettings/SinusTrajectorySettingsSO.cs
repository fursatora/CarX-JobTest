using UnityEngine;

namespace Runtime.MoveSystem.MoveSettings
{
    [CreateAssetMenu(fileName = "SinusMoveSettingsSO", menuName = "Game/Settings/Move/Sinus")]
    public class SinusTrajectorySettingsSO : BaseTrajectorySettings
    {
        [SerializeField] private float amplitude;
        [SerializeField] private float frequency;

        public float Amplitude => amplitude;
        public float Frequency => frequency;
    }
}