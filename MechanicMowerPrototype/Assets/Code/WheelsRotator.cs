﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  GardenFlipperMower {
    public class WheelsRotator : MonoBehaviour {
        
        [SerializeField] protected float rotationSpeed;
        [SerializeField] protected Rigidbody wheelsRigidbody;
//        [SerializeField] protected CapsuleCollider wheelsCollider;

        protected float dt;
        protected Transform cachedTransform;
        protected Vector3 wheelsMovementVector;

        protected virtual void Start() {
            Initialise();
        }

        protected virtual void Initialise() {
            if (/*wheelsCollider == null || */wheelsRigidbody == null) {
                Debug.LogError("Assign proper components first.");
                return;
            }

            cachedTransform = transform;
        }

        protected virtual void OnEnable() {
            MechanicMowerEvents.OnMovementUpdate += UpdateWheelsRotation;
        }

        protected virtual void OnDisable() {
            MechanicMowerEvents.OnMovementUpdate -= UpdateWheelsRotation;
        }
        
        protected void UpdateWheelsRotation(Vector3 eulersFromInput) {
            dt = Time.deltaTime;
            RotateAlongZ(eulersFromInput.y);
        }

        protected void RotateAlongZ(float zRotation) {
            wheelsMovementVector = new Vector3(0, 0, -zRotation);
            var totalEulers = wheelsMovementVector * dt * rotationSpeed;
            cachedTransform.Rotate(totalEulers);
        }
    }
}
