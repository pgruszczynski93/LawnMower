using System.Data;
using UnityEngine;

namespace GardenFlipperMower {
    public class MechanicMowerMovement : MonoBehaviour {

        [SerializeField] float rotationSpeed;
        [SerializeField] float movementSpeed;
        [SerializeField] WheelsRotator[] wheelsRotators;

        float dt;
        Vector3 movementDirection;
        Vector3 rotationDirection;
        Transform cachedTransform;
        
        void Start() {
            Initialize();
        }

        void Initialize() {
            if (wheelsRotators == null || wheelsRotators.Length == 0) {
                Debug.LogError("Assign proper components first.");
                return;
            }
            cachedTransform = transform;
        }

        void OnEnable() {
            MechanicMowerEvents.OnInputUpdate += UpdateMovePose;
        }

        void OnDisable() {
            MechanicMowerEvents.OnInputUpdate -= UpdateMovePose;
        }


        void UpdateMovePose(Vector3 inputVector) {
            dt = Time.deltaTime;
            movementDirection = new Vector3(inputVector.y, 0, 0);
            rotationDirection = new Vector3(0, inputVector.x, 0);

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
                UpdatePosition();
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
                UpdateRotation();
            }
        }

        void UpdateRotation() {
//            var fromRotation = cachedTransform.rotation;
//            var currentRotationAngles = cachedTransform.eulerAngles;
//            var toRotation = Quaternion.Euler(currentRotationAngles + rotationDirection);
//            var slerpFraction = dt * rotationSpeed;
//            var newRotation = Quaternion.Slerp(fromRotation, toRotation, slerpFraction);
//            cachedTransform.rotation = newRotation;

            var totalEulers = rotationDirection * rotationSpeed * dt;
            cachedTransform.Rotate(totalEulers);

        }

        void UpdatePosition() {
//            var fromPosition = cachedTransform.position;
//            var toPosition = new Vector3(fromPosition.x + movementDirection.x, 0, 0);
//            var lerpFraction = dt * movementSpeed;
//            var newPosition = Vector3.Lerp(fromPosition, toPosition, lerpFraction);
//            cachedTransform.position = newPosition;
            var translation = movementDirection * movementSpeed * dt;
            cachedTransform.Translate(translation);
        }
    }
}