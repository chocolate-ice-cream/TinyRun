using Unity.Entities;
using Unity.Tiny.Text;

namespace TinyRun {
    public struct TitlePanel : IComponentData {
        public Entity TitleLabel;
        public Entity StartLabel;
    }
}
