using System;
using UnityEngine;

namespace  GardenFlipperMower {
    public static class MechanicMowerEvents {
        public static event Action OnMechanicMowerStarted;
        public static event Action OnCustomUpdate;

        public static event Action OnCustomLateUpdate;
        
        public static event Action<Vector3> OnInputUpdate;
        
        public static event Action OnMechanicMowerFinished;

        public static void BroadcastOnMechanicMowerStarted() {
            OnMechanicMowerStarted?.Invoke();
        }

        public static void BroadcastOnPositionUpdate(Vector3 vector) {
            OnInputUpdate?.Invoke(vector);
        }

        public static void BroadcastOnCustomLateUpdate() {
            OnCustomLateUpdate?.Invoke();
        }

        public static void BroadcastOnMechanicMowerUpdate() {
            OnCustomUpdate?.Invoke();
        }

        public static void BroadcastOnMechanicMowerFinished() {
            OnMechanicMowerFinished?.Invoke();
        }
    }
}
