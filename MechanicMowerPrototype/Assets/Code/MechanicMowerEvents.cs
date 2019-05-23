using System;
using UnityEngine;

namespace  GardenFlipperMower {
    public static class MechanicMowerEvents {
        public static event Action OnMechanicMowerStarted;
        public static event Action OnMechanicMowerUpdate;
        public static event Action<Vector3> OnInputUpdate;
        public static event Action OnMechanicMowerFinished;

        public static void BroadcastOnMechanicMowerStarted() {
            OnMechanicMowerStarted?.Invoke();
        }

        public static void BroadcastOnPositionUpdate(Vector3 vector) {
            OnInputUpdate?.Invoke(vector);
        }

        public static void BroadcastOnMechanicMowerUpdate() {
            OnMechanicMowerUpdate?.Invoke();
        }

        public static void BroadcastOnMechanicMowerFinished() {
            OnMechanicMowerFinished?.Invoke();
        }
    }
}
