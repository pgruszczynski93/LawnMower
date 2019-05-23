using UnityEngine;

namespace GardenFlipperMower {
    public class GameMasterBehaviour : MonoBehaviour{

        void FixedUpdate() {
            MechanicMowerEvents.BroadcastOnMechanicMowerUpdate();    
        }

        void LateUpdate() {
            MechanicMowerEvents.BroadcastOnCustomLateUpdate();
        }
    }
}