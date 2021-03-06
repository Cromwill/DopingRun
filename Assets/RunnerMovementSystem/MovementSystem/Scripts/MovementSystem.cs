using UnityEngine;
using UnityEngine.Events;
using RunnerMovementSystem.Model;

namespace RunnerMovementSystem
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private RoadSegment _firstRoad;
        [SerializeField] private MovementOptions _options;

        private MovementBehaviour _movementBehaviour;
        private RoadMovement _roadMovement;
        private TransitionMovement _transitionMovement;
        private IMovement _currentMovement;

        public event UnityAction<PathSegment> PathChanged;
        public event UnityAction<TransitionSegment> TransitedToSegment;

        public IMovement CurrentMovement => _currentMovement;
        public MovementOptions Options => _options;
        public RoadSegment FirstRoad => _firstRoad;
        public float Offset => _currentMovement.Offset;
        public float CurrentSpeed => _movementBehaviour.GetCurrentSpeed();
        public bool IsOnTransition => _currentMovement is TransitionMovement;
        public PathSegment CurrentRoad => _currentMovement.PathSegment;

        private void Awake()
        {
            _movementBehaviour = new MovementBehaviour(transform, _options);

            _roadMovement = new RoadMovement(_movementBehaviour);
            _transitionMovement = new TransitionMovement(_movementBehaviour);
        }

        private void OnEnable()
        {
            _roadMovement.EndReached += OnRoadEnd;
            _transitionMovement.EndReached += OnTransitionEnd;
        }

        private void OnDisable()
        {
            _roadMovement.EndReached -= OnRoadEnd;
            _transitionMovement.EndReached -= OnTransitionEnd;
        }

        private void Start()
        {
            if (_firstRoad)
                Init(_firstRoad);
        }

        private void Update()
        {
            _currentMovement?.Update();
        }

        public void SetDirection(Direction direction)
        {
            _options.SetDirection(direction);
        }

        public void SetOptions(MovementOptions options)
        {
            _options = options;
        }

        public void SetFirstRoad(RoadSegment segment)
        {
            _firstRoad = segment;
        }

        public void Init(RoadSegment firstRoad)
        {
            _firstRoad = firstRoad;
            _roadMovement.Init(_firstRoad);
            _currentMovement = _roadMovement;
        }

        public void MoveForward()
        {
            if (enabled)
                _currentMovement.MoveForward();
        }

        public void SetOffset(float offset)
        {
            if (enabled)
                _currentMovement.SetOffset(offset);
        }

        public void SetCureentMovent(IMovement movement)
        {
            _currentMovement = movement;
        }

        public void Transit(TransitionSegment transition)
        {
            TransitedToSegment?.Invoke(transition);
            _transitionMovement.Init(transition);
            _currentMovement = _transitionMovement;

            PathChanged?.Invoke(transition);
        }

        private void OnRoadEnd(RoadSegment roadSegment)
        {
            var nearestRoad = roadSegment.GetNearestRoad(transform.position);
            if (nearestRoad == null)
                return;

            _roadMovement.Init(nearestRoad);
            _currentMovement = _roadMovement;

            PathChanged?.Invoke(nearestRoad);
        }

        private void OnTransitionEnd(TransitionSegment transition)
        {
            var nearestRoad = transition.GetNearestRoad(transform.position);
            _roadMovement.Init(nearestRoad);
            _currentMovement = _roadMovement;


            PathChanged?.Invoke(nearestRoad);
        }
    }
}
