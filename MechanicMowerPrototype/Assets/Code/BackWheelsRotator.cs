using UnityEngine;

namespace GardenFlipperMower {
    public class BackWheelsRotator : WheelsRotator{
        
        [Range(0, 1)] [SerializeField] float mowerDecelartion;

        protected override void OnEnable() {
            base.OnEnable();
            MechanicMowerEvents.OnInputUpdate += UpdateBackWheelsRotation;
        }

        protected override void OnDisable() {
            base.OnDisable();
            MechanicMowerEvents.OnInputUpdate += UpdateBackWheelsRotation;
        }

        protected void UpdateBackWheelsRotation(Vector3 eulersFromInput) {
            var absoluteValue = Mathf.Abs(eulersFromInput.y);
            if (absoluteValue > mowerDecelartion)
                return;

            RotateAlongZ(-eulersFromInput.x);
        }
    }
}