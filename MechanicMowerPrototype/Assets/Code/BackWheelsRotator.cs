using UnityEngine;

namespace GardenFlipperMower {
    public class BackWheelsRotator : WheelsRotator{
        protected override void OnEnable() {
            base.OnEnable();
            MechanicMowerEvents.OnInputUpdate += UpdateBackWheelsRotation;
        }

        protected override void OnDisable() {
            base.OnDisable();
            MechanicMowerEvents.OnInputUpdate += UpdateBackWheelsRotation;
        }

        protected void UpdateBackWheelsRotation(Vector3 eulers) {
            
        }
    }
}