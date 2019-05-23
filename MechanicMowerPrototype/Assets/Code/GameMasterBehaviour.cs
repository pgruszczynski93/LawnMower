using UnityEngine;

namespace GardenFlipperMower {
    public class GameMasterBehaviour : MonoBehaviour {

        [SerializeField] bool canStartMowing;
        [SerializeField] MechanicMowerBehaviour mowerBehaviour;

        void Start() {
            Initiailze();
        }

        void Initiailze() {
            if (mowerBehaviour == null) {
                Debug.LogError("Assign proper components first.");
                return;
            }

            canStartMowing = mowerBehaviour.IsWorking;
            
            //dopisa c wlaczanie wylaczanie kosiarki, dodac dzwiek i particle
        }

        void Update() {
            MechanicMowerEvents.BroadcastOnCustomUpdate();
        }
        
        void FixedUpdate() {
            MechanicMowerEvents.BroadcastOnCustomFixedUpdate();    
        }

        void LateUpdate() {
            MechanicMowerEvents.BroadcastOnCustomLateUpdate();
        }

        void RunFixedUpdateEvents() {
        }
        
    }
}