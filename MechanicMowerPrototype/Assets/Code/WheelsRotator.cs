using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  GardenFlipperMower {
    public class WheelsRotator : MonoBehaviour {
        
        [SerializeField] float rotationMultiplier;
        [SerializeField] Rigidbody wheelsRigidbody;
        [SerializeField] CapsuleCollider wheelsCollider;

        float dt;
        Transform cachedTransform;

        void Start() {
            Initialize();
        }

        void Initialize() {
            if (wheelsCollider == null || wheelsRigidbody == null) {
                Debug.LogError("Assign proper components first.");
                return;
            }

            cachedTransform = transform;
        }

        void OnEnable() {
            MechanicMowerEvents.OnInputUpdate += UpdateWheelsPoses;
        }

        void OnDisable() {
            MechanicMowerEvents.OnInputUpdate -= UpdateWheelsPoses;
        }

        void UpdateWheelsPoses(Vector3 inputVector) {
            dt = Time.deltaTime;
            var wheelsMovementVector = new Vector3(0,0, -inputVector.y);
            cachedTransform.Rotate(wheelsMovementVector * dt * rotationMultiplier);
        }
    }
}
