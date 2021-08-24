using System;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerScript
{
    public class PlayerMovementController : JoystickManager
    {
        [HideInInspector] public bool stopPlayerMovement;
        private Transform _playerT;
        private float _forwardSpeed, _movementSpeed, _targetY, _timeCount;
        private bool _isPlayerRun, _checkJoystick;

        public delegate void PlayerMovementDelegate();

        public static PlayerMovementDelegate StopPlayerMovementDelegate;
        private NavMeshAgent agent;
        public Transform target;

        private void OnEnable()
        {
            StopPlayerMovementDelegate += StopPlayerMovement;

            GameManager.LevelCompletedDelegate += StopPlayerMovement;

            GameManager.StartGameDelegate += StartPlayerMovement;
            GameManager.StartGameDelegate += CheckJoystickFalse;

            GameManager.GameOverDelegate += StopPlayerMovement;

            GameManager.ResetLevelDelegate += ResetMovement;
            GameManager.ResetLevelDelegate += CheckJoystickTrue;

            GameManager.NextLevelDelegate += ResetMovement;
            GameManager.NextLevelDelegate += CheckJoystickTrue;
        }

        private void Start()
        {
            CheckJoystickTrue();
            _playerT = transform;
            _forwardSpeed = 2.4f;
            _movementSpeed = 1.5f;
            _targetY = 0.24f;
            _timeCount = 0.0f;
            agent = GetComponent<NavMeshAgent>();
        }

        private void FixedUpdate()
        {
            
            agent.destination = target.position;

            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f* Time.deltaTime);  
            
            if (_isPlayerRun) StudentMovement();
            else if (_checkJoystick) CheckJoystick();
        }

        private static void CheckJoystick()
        {
            if (CheckJoystickHorizontal())
                GameManager.StartGameDelegate();
        }

        private void StudentMovement()
        {
            var pPos = _playerT.position;

            if (stopPlayerMovement)
            {
                if ((transform.position.y < 0.3f)) return;
                pPos = new Vector3(pPos.x, 0.24f, pPos.z);
                _playerT.position = pPos;
                return;
            }

            //StudentMovementRotatian(_playerT);
           // var targetZ = pPos.z + _forwardSpeed;
           var targetZ = pPos.z;

           
            var targetX = pPos.x + JoystickHorizontal * _movementSpeed;
            /*if (targetX <= -4f)
                targetX = -4f;
            else if (targetX >= 4f)
                targetX = 4f;*/
            var direction = new Vector3(x: targetX, _targetY, targetZ);

            transform.position = Vector3.MoveTowards(pPos, direction, 15f * Time.deltaTime);
        }


        private void StudentMovementRotatian(Transform playerT)
        {
            var direction = playerT.position + Vector3.right * (JoystickHorizontal * 3.5f * _timeCount);
            var lookRotation = Quaternion.LookRotation(direction);
            playerT.rotation = Quaternion.Slerp(playerT.rotation, lookRotation, _timeCount);
            _timeCount += Time.deltaTime;
        }

        private void StartPlayerMovement()
        {
            _isPlayerRun = true;
        }

        private void StopPlayerMovement()
        {
            _isPlayerRun = false;
        }

        private void CheckJoystickTrue()
        {
            _checkJoystick = true;
        }

        private void CheckJoystickFalse()
        {
            _checkJoystick = false;
        }

        private void ResetMovement()
        {
            _timeCount = 0f;
            StopPlayerMovementDelegate();
            gameObject.transform.position = new Vector3(0, 0.53f, 0);
            DoorManager.SetOldPlayerPosDelegate();
        }
    }
}