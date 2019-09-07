using Unity.Entities;
using Unity.Mathematics;

namespace TinyRun {
    /// <summary>
    /// スクロールコンポーネント
    /// </summary>
    public struct Scroll : IComponentData {
        /// <summary>コンポーネントの有効/無効</summary>
        public bool Enable;
        /// <summary>ループするかどうか</summary>
        public bool Loop;
        /// <summary>スクロール開始位置</summary>
        public float2 StartPosition;
        /// <summary>スクロール距離</summary>
        public float Distance;
        /// <summary>スクロールスピード</summary>
        public float Speed;
        /// <summary>初期スクロールスピード</summary>
        public float DefaultSpeed;
        /// <summary>スクロール方向</summary>
        public float2 Direction;
        /// <summary>現在のスクロール距離</summary>
        public float2 CurrentDistance;
    }
}
