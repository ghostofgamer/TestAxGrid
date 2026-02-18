using AxGrid;
using AxGrid.Base;
using AxGrid.Path;
using SOContent;
using UnityEngine;

namespace View
{
    public class SlotView : MonoBehaviourExtBind
    {
        [SerializeField] private SpinConfig _spinConfig;

        [Header("Slot Items")] [SerializeField]
        private RectTransform[] _items;

        [Header("Particles")] [SerializeField] private ParticleSystem[] _spinParticles;
        [SerializeField] private ParticleSystem _winParticle;

        private bool _spinning;
        private float _speed;
        private CPath _path;
        private float _currentOffset;
        private float _stopDistance;
        private float _lastValue;
        private float _lastOffset;

        [OnStart]
        public void Init()
        {
            ArrangeItems();
            Settings.Model.EventManager.AddAction<string, int>("StartSpin", StartSpin);
            Settings.Model.EventManager.AddAction("StopingSpin", StopingSpin);
        }

        [OnUpdate]
        private void Update()
        {
            _path?.Update(Time.deltaTime);

            if (!_spinning)
                return;

            foreach (var item in _items)
            {
                item.anchoredPosition -= Vector2.up * _speed * Time.deltaTime;

                if (item.anchoredPosition.y < -_spinConfig.ItemSpacing)
                    MoveToTop(item);
            }
        }

        private void StartSpin(string eventStringArgs, int eventIntArgs)
        {
            _spinning = true;
            _speed = 0f;
            _lastOffset = 0f;

            foreach (ParticleSystem ps in _spinParticles)
                ps.Play();
            
            _path = CPath.Create()
                .EasingQuadEaseOut(_spinConfig.AccelerationTime, 0f, _spinConfig.MaxSpeed, v => _speed = v).Action(() =>
                {
                    _speed = _spinConfig.MaxSpeed;
                    _path = null;
                });
        }

        private void ArrangeItems()
        {
            int centerIndex = _items.Length / 2;

            for (int i = 0; i < _items.Length; i++)
            {
                int offsetIndex = i - centerIndex;
                _items[i].anchoredPosition = new Vector2(0, -offsetIndex * _spinConfig.ItemSpacing);
            }
        }

        private void StopingSpin()
        {
            _lastOffset = 0f;

            _path = CPath.Create()
                .EasingQuadEaseOut(_spinConfig.StopDuration, _speed, _spinConfig.MinSpeed, v => _speed = v).Action(() =>
                {
                    foreach (ParticleSystem ps in _spinParticles)
                        ps.Stop();

                    _spinning = false;
                    CenterToMiddleSmooth();
                });
        }

        private void CenterToMiddleSmooth()
        {
            RectTransform middle = FindClosestToCenter();
            float offsetToCenter = -middle.anchoredPosition.y;
            _lastOffset = 0f;

            _path = CPath.Create()
                .EasingQuadEaseOut(_spinConfig.SpeedCentered, 0f, offsetToCenter, MoveAll)
                .Action(FinishSpin);
        }

        private void MoveAll(float value)
        {
            float delta = value - _lastOffset;

            foreach (var i in _items)
                i.anchoredPosition += Vector2.up * delta;

            _lastOffset = value;
        }

        private void FinishSpin()
        {
            _winParticle?.Play();
            _lastOffset = 0f;
            _speed = 0f;
            Settings.Invoke("SpinFinished");
        }

        private void MoveToTop(RectTransform item)
        {
            float topY = GetTopY();
            item.anchoredPosition = new Vector2(0, topY + _spinConfig.ItemSpacing);
        }

        private float GetTopY()
        {
            float max = float.MinValue;

            foreach (var i in _items)
                if (i.anchoredPosition.y > max)
                    max = i.anchoredPosition.y;

            return max;
        }

        private RectTransform FindClosestToCenter()
        {
            RectTransform best = _items[0];
            float min = Mathf.Abs(_items[0].anchoredPosition.y);

            foreach (var i in _items)
            {
                float d = Mathf.Abs(i.anchoredPosition.y);

                if (d < min)
                {
                    min = d;
                    best = i;
                }
            }

            return best;
        }
    }
}