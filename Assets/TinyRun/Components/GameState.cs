using Unity.Entities;

namespace TinyRun {
    public struct GameState : IComponentData {
        public bool Begun;
        public bool Playing;
        public bool GameOverPrev;
        public bool GameOver;
        public bool Retry;
        public float SpeedScale;

        public int HiScore;
        public int CurrentScore;

        public float Elapse;
    }
}
