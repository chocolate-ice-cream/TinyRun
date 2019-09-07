using Unity.Entities;

namespace TinyRun {
    /// <summary>
    /// ジャンプコンポーネント
    /// </summary>
    public struct Jump : IComponentData {
        /// <summary>現在の速さ</summary>
        public float v;
        /// <summary>初速</summary>
        public float v0;
        /// <summary>経過時間</summary>
        public float t;
        /// <summary>重力加速度</summary>
        public float g;

        /// <summary>床のY座標</summary>
        public float floorY;
        /// <summary>重力加速度による減速を無視する時間</summary>
        public float durationIgnoreGravity;
    }
}
