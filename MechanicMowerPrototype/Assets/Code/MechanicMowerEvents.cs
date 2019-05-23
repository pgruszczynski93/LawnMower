using System;
using UnityEngine;

namespace  GardenFlipperMower {
    public static class MechanicMowerEvents {
        public static event Action OnMechanicMowerStarted;
        public static event Action OnCustomUpdate;
        public static event Action OnCustomFixedUpdate;
        public static event Action OnCustomLateUpdate;
        public static event Action<Vector3> OnMovementUpdate;
        public static event Action OnMechanicMowerFinished;

        public static void BroadcastOnMechanicMowerStarted() {
            OnMechanicMowerStarted?.Invoke();
        }
        public static void BroadcastOnCustomUpdate() {
            OnCustomUpdate?.Invoke();
        }
        public static void BroadcastOnMovementUpdate(Vector3 vector) {
            OnMovementUpdate?.Invoke(vector);
        }
        public static void BroadcastOnCustomLateUpdate() {
            OnCustomLateUpdate?.Invoke();
        }
        public static void BroadcastOnCustomFixedUpdate() {
            OnCustomFixedUpdate?.Invoke();
        }
        public static void BroadcastOnMechanicMowerFinished() {
            OnMechanicMowerFinished?.Invoke();
        }
    }
}
