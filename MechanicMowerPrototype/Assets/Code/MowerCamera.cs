using UnityEngine;

namespace GardenFlipperMower {
    public class MowerCamera : MonoBehaviour {
        
        [SerializeField] Camera camera;
        [SerializeField] Transform cameraTarget;
        [SerializeField] Vector3 cameraOffset;
        [SerializeField] Vector3 lookOffset;
        
        Vector3 lookDirection;
        Transform cachedTransform;
        void Start() {
            Initalize();
        }

        void Initalize() {
            if (camera == null || cameraTarget == null) {
                Debug.LogError("Assign proper components first.");
                return;
            }

            cachedTransform = transform;
            lookDirection = cameraTarget.position - cachedTransform.position;
        }

        void OnEnable() {
            MechanicMowerEvents.OnCustomLateUpdate += CameraFollowMovement;
        }

        void OnDisable() {
            MechanicMowerEvents.OnCustomLateUpdate -= CameraFollowMovement;

        }

        void CameraFollowMovement() {
            cachedTransform.position = cameraTarget.TransformPoint(-lookDirection-cameraOffset);
            cachedTransform.LookAt(cameraTarget.position-lookOffset);
        }
    }
}