using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Mathematics;

namespace TinyRun {
    /// <summary>
    /// Entityのスクロールシステム
    /// </summary>
    public class ScrollSystem : ComponentSystem {

        protected override void OnUpdate() {
            var deltaTime = World.TinyEnvironment().frameDeltaTime;

            Entities.ForEach((Entity entity, ref Scroll scroll, ref Translation transformPosition) => {
                if (scroll.Enable) {
                    var move = scroll.Direction * scroll.Speed * deltaTime;
                    var pos = transformPosition.Value;
                    pos.x += move.x;
                    pos.y += move.y;

                    transformPosition.Value = pos;
                    scroll.CurrentDistance += move;

                    if (scroll.Loop) {
                        var sqr = scroll.Distance * scroll.Distance;

                        if (math.distancesq(scroll.CurrentDistance.x, scroll.CurrentDistance.y) > sqr) {
                            transformPosition.Value = new float3(scroll.StartPosition.x, scroll.StartPosition.y, 0.0f);
                            scroll.CurrentDistance = float2.zero;
                        }
                    }
                }
            });
        }
    }
}
