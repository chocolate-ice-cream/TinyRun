using Unity.Entities;
using Unity.Mathematics;

namespace TinyRun {
    /// <summary>
    /// �X�N���[���R���|�[�l���g
    /// </summary>
    public struct Scroll : IComponentData {
        /// <summary>�R���|�[�l���g�̗L��/����</summary>
        public bool Enable;
        /// <summary>���[�v���邩�ǂ���</summary>
        public bool Loop;
        /// <summary>�X�N���[���J�n�ʒu</summary>
        public float2 StartPosition;
        /// <summary>�X�N���[������</summary>
        public float Distance;
        /// <summary>�X�N���[���X�s�[�h</summary>
        public float Speed;
        /// <summary>�����X�N���[���X�s�[�h</summary>
        public float DefaultSpeed;
        /// <summary>�X�N���[������</summary>
        public float2 Direction;
        /// <summary>���݂̃X�N���[������</summary>
        public float2 CurrentDistance;
    }
}
