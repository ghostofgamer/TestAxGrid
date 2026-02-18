using UnityEngine;

namespace SOContent
{
    [CreateAssetMenu(fileName = "SpinConfig", menuName = "SlotMachine/SpinConfig", order = 1)]
    public class SpinConfig : ScriptableObject
    {
        [Header("Slot Items")]
        [SerializeField] private float _itemSpacing = 300f;

        [Header("Speed Settings")]
        [SerializeField] private float _maxSpeed = 800f;
        [SerializeField] private float _minSpeed = 50f;
        [SerializeField] private float _accelerationTime = 1f;
        [SerializeField] private float _stopDuration = 1f;
        [SerializeField] private float _speedCentered = 1f;
        
        public float ItemSpacing => _itemSpacing;
        public float MaxSpeed => _maxSpeed;
        public float MinSpeed => _minSpeed;
        public float AccelerationTime => _accelerationTime;
        public float StopDuration => _stopDuration;
        public float SpeedCentered => _speedCentered;
    }
}