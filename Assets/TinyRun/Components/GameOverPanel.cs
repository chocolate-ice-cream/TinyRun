using Unity.Entities;
using Unity.Tiny.Text;

namespace TinyRun {
    public struct GameOverPanel : IComponentData {
        public Entity GameOverLabel;
        public Entity RetrySprite;
    }
}
