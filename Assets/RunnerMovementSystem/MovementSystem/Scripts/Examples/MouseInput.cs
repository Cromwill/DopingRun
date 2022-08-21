using UnityEngine;

namespace RunnerMovementSystem.Examples
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private MovementSystem _roadMovement;
        [SerializeField] private StartLevelButton _startLevelButton;
        [SerializeField] private float _sensitivity = 0.01f;

        private Vector3 _mousePosition;
        private float _saveOffset;
        private bool _isStart;

        public bool IsMoved { get; private set; }

        private void OnEnable()
        {
            _roadMovement.PathChanged += OnPathChanged;
            _startLevelButton.RunStarted += OnRunStart;
        }

        private void OnDisable()
        {
            _roadMovement.PathChanged -= OnPathChanged;
            _startLevelButton.RunStarted -= OnRunStart;

            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        private void OnPathChanged(PathSegment _)
        {
            _saveOffset = _roadMovement.Offset;
            _mousePosition = Input.mousePosition;
        }

        private void OnRunStart()
        {
            _isStart = true;
        }

        private void Update()
        {
            if (_isStart == false || Time.timeScale == 0)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _saveOffset = _roadMovement.Offset;
                _mousePosition = Input.mousePosition;
                IsMoved = true;
            }

            if (Input.GetMouseButton(0))
            {
#if UNITY_WEBGL
                Texture2D cursor = new Texture2D(0, 0);
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
#else
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
#endif

                var offset = Input.mousePosition - _mousePosition;
                _roadMovement.SetOffset(_saveOffset + offset.x * _sensitivity);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }

            if(IsMoved)
                _roadMovement.MoveForward();
        }

        public void SetSensitivity(float value)
        {
            _sensitivity = value;
        }
    }
}
