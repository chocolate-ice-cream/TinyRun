using Unity.Entities;

namespace TinyRun {
    /// <summary>
    /// �W�����v�R���|�[�l���g
    /// </summary>
    public struct Jump : IComponentData {
        /// <summary>���݂̑���</summary>
        public float v;
        /// <summary>����</summary>
        public float v0;
        /// <summary>�o�ߎ���</summary>
        public float t;
        /// <summary>�d�͉����x</summary>
        public float g;

        /// <summary>����Y���W</summary>
        public float floorY;
        /// <summary>�d�͉����x�ɂ�錸���𖳎����鎞��</summary>
        public float durationIgnoreGravity;
    }
}
