
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;

namespace SurvivalWizard
{
    public class FloatingJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private RectTransform _backGround;
        [SerializeField] private RectTransform _handle;
        private RectTransform _joystick;

        [FormerlySerializedAs("movementRange")]
        [SerializeField]
        private float _movementRange = 50;

        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string _controlPath;

        public float MovementRange
        {
            get => _movementRange;
            set => _movementRange = value;
        }

        private Vector2 _startPos;
        private Vector2 _pointerDownPos;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _backGround.gameObject.SetActive(false);
        }

        private void Start()
        {
            if(!TryGetComponent(out _joystick))
            {
                throw new System.ArgumentNullException("RectTransform component not found");
            }
            _startPos = _handle.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            _backGround.gameObject.SetActive(true);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystick, eventData.position, eventData.pressEventCamera, out _pointerDownPos);
            _backGround.localPosition = _pointerDownPos - _startPos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_backGround, eventData.position, eventData.pressEventCamera, out _pointerDownPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_backGround, eventData.position, eventData.pressEventCamera, out var position);
            var delta = position - _pointerDownPos;

            delta = Vector2.ClampMagnitude(delta, MovementRange);
            _handle.anchoredPosition = _startPos + delta;

            var newPos = new Vector2(delta.x / MovementRange, delta.y / MovementRange);
            SendValueToControl(newPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _handle.anchoredPosition = _startPos;
            SendValueToControl(Vector2.zero);
            _backGround.gameObject.SetActive(false);
        }
    }
}
