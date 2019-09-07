using Unity.Authoring.Core;
using Unity.Entities;
using Unity.Mathematics;

namespace TinyRun
{
    public struct Joystick : IComponentData
    {
        public float DeadZone;
        public float StickZoneRadius;
        public Entity Stick;
        [HideInInspector]
        public float2 Direction;
        [HideInInspector]
        public bool IsPressed;
        [HideInInspector]
        public bool IsPressedSpaceKey;
        [HideInInspector]
        public bool IsPressingSpaceKey;
        [HideInInspector]
        public bool IsPressedUpArrowKey;
        [HideInInspector]
        public bool IsPressingUpArrowKey;
        [HideInInspector]
        public bool IsPressedDownArrowKey;
        [HideInInspector]
        public bool IsPressingDownArrowKey;
        [HideInInspector]
        public int CurrentFingerID;

        public static Joystick Default
        {
            get
            {
                var joystick = new Joystick();
                joystick.DeadZone = 0.1f;
                joystick.StickZoneRadius = 100f;
                joystick.CurrentFingerID = -1;
                return joystick;
            }
        }
    }
}
