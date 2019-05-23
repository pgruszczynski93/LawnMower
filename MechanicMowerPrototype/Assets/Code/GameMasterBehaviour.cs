using UnityEngine;

namespace GardenFlipperMower {
    public class GameMasterBehaviour : MonoBehaviour{

        void Update() {
            MechanicMowerEvents.BroadcastOnMechanicMowerUpdate();    
        }

        void LateUpdate() {
            MechanicMowerEvents.BroadcastOnCustomLateUpdate();
        }
    }
}