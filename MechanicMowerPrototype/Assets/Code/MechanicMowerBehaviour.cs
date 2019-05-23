using UnityEngine;

namespace  GardenFlipperMower {
    public class MechanicMowerBehaviour : MonoBehaviour {

        [SerializeField] MowerInput mowerInput;
        [SerializeField] MechanicMowerMovement mowerMovement;

        void Start() {
            Initialize();
        }

        void Initialize() {
            if (mowerMovement == null || mowerInput == null) {
                Debug.LogError("Assign proper components first.");
                return;
            }
        }
        
        void OnEnable() {
        }

        void OnDisable() {
        }

        
        
        void Update() {
            MechanicMowerEvents.BroadcastOnMechanicMowerUpdate();    
        }
    }
}
