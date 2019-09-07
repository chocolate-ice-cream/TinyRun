using Unity.Entities;

namespace TinyRun {
    public struct ScorePanel : IComponentData {
        public Entity HiLabel;
        public Entity HiScoreLabel;
        public Entity CurrentScoreLabel;
    }
}
