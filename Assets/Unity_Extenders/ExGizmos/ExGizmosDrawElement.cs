using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GaMe.ExMesh
{
    public interface IExGizmosDrawElement
    {
        public bool Enabled { get; set; }

        public Color Color { get; set; }
        public Matrix4x4 Matrix { get; set; }
        public Texture Exposure { get; set; }
        public float ProbeSize { get; set; }

        public void Draw(Matrix4x4 matrix_);
    }

    public interface IExGizmosWire
    {
        public bool IsWire { get; set; }
    }

    [Serializable]
    public abstract class ExGizmosDrawElement : IExGizmosDrawElement
    {
        [SerializeField] protected bool m_enabled = true;
        public bool Enabled { get => m_enabled; set => m_enabled = value; }

        [SerializeField] protected Color m_color = Color.green;
        public Color Color { get => m_color; set => m_color = value; }

        [SerializeField] protected Matrix4x4 m_matrix = Matrix4x4.identity;
        public Matrix4x4 Matrix { get => m_matrix; set => m_matrix = value;}

        [SerializeField] protected Texture m_texture = null;
        public Texture Exposure { get => m_texture; set => m_texture = value; }

        [SerializeField] float m_probeSize = 1.0f;
        public float ProbeSize { get => m_probeSize; set => m_probeSize = value;}

        public abstract void Draw(Matrix4x4 matrix_);
    }
}