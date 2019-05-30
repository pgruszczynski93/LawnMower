using UnityEngine;

namespace GardenFlipperMower {
    public class MowerMasterBehaviour : MonoBehaviour {

        [SerializeField] MechanicMowerBehaviour mowerBehaviour;

        void Start() {
            Initialise();
        }

        void Initialise() {
            if (mowerBehaviour == null) {
                Debug.LogError("Assign proper components first.");
                return;
            }
        }

        void Update() {
            MechanicMowerEvents.BroadcastOnCustomUpdate();
        }
        
        void FixedUpdate() {
            RunFixedUpdateEvents();
        }

        void LateUpdate() {
            MechanicMowerEvents.BroadcastOnCustomLateUpdate();
        }

        void RunFixedUpdateEvents() {
            if (mowerBehaviour.IsWorking) {
                MechanicMowerEvents.BroadcastOnCustomFixedUpdate();    
            }
        }
        
    }
}