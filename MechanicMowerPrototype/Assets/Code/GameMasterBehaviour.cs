using UnityEngine;

namespace GardenFlipperMower {
    public class GameMasterBehaviour : MonoBehaviour {

        [SerializeField] MechanicMowerBehaviour mowerBehaviour;

        void Start() {
            Initialise();
        }

        void Initialise() {
            if (mowerBehaviour == null) {
                Debug.LogError("Assign proper components first.");
                return;
            }

            //dopisa c wlaczanie wylaczanie kosiarki, dodac dzwiek i particle
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