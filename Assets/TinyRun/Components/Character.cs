using Unity.Authoring.Core;
using Unity.Entities;

namespace TinyRun {
    public struct Character : IComponentData {
        public Entity CharacterVisual;
        public float MoveTiltAngle;
        public float MoveTiltSpeed;
        [HideInInspector]
        public float CurrentTiltAngle;
        public float MoveSpeed;

        public bool enableJump;
        public bool jumpingPrev;
        public bool jumping;
    }
}
