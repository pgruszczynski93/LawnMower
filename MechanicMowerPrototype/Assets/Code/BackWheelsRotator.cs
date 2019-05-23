using UnityEngine;

namespace GardenFlipperMower {
    public class BackWheelsRotator : WheelsRotator{
        
        [Range(0, 1)] [SerializeField] float mowerDecelartion;

        protected override void OnEnable() {
            base.OnEnable();
            MechanicMowerEvents.OnMovementUpdate += UpdateBackWheelsRotation;
        }

        protected override void OnDisable() {
            base.OnDisable();
            MechanicMowerEvents.OnMovementUpdate += UpdateBackWheelsRotation;
        }

        protected void UpdateBackWheelsRotation(Vector3 eulersFromInput) {
            var inputAbsoluteValue = Mathf.Abs(eulersFromInput.y);
            if (inputAbsoluteValue > mowerDecelartion)
                return;

            RotateAlongZ(-eulersFromInput.x);
        }
    }
}