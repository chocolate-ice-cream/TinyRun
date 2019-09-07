using Unity.Entities;

namespace TinyRun {
    public struct Animations : IComponentData {
        public Entity SequenceIdleEntity;
        public Entity SequenceRunEntity;
        public Entity SequenceJumpEntity;
        public Entity SequenceDeathEntity;
    }
}
