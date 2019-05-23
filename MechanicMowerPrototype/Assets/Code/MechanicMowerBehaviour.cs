using UnityEngine;

namespace  GardenFlipperMower {
    public class MechanicMowerBehaviour : MonoBehaviour {

        [SerializeField] bool isWorking;
        [SerializeField] MowerInputSystem mowerInputSystem;
        [SerializeField] MechanicMowerMovement mowerMovement;

        public bool IsWorking {
            get { return isWorking; }
            set { isWorking = value; }
        }


        void Start() {
            Initialize();
        }

        void Initialize() {
            if (mowerMovement == null || mowerInputSystem == null) {
                Debug.LogError("Assign proper components first.");
                return;
            }

            isWorking = false;
        }

        void OnEnable() {

            MechanicMowerEvents.OnMechanicMowerStarted += EnableMower;
            MechanicMowerEvents.OnMechanicMowerFinished += DisableMower;
        }

        void OnDisable() {
            MechanicMowerEvents.OnMechanicMowerStarted -= EnableMower;
            MechanicMowerEvents.OnMechanicMowerFinished -= DisableMower;
        }

        void EnableMower() {
            if(isWorking == false)
                isWorking = true;
        }

        void DisableMower() {
            if(isWorking)
                isWorking = false;
        }
    }
}
