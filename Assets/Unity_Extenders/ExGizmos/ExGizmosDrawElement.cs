using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GaMe.ExMesh
{
    public interface IExGizmosDrawElement
    {
        public bool Enabled { get; set; }

        public void Draw(ExGizmosDrawContext baseContext_);
    }

    public interface IExGizmosWire
    {
        public bool IsWire { get; set; }
        public void DrawWire(ExGizmosDrawContext baseContext_);
    }

    [Serializable]
    public abstract class ExGizmosDrawElement : IExGizmosDrawElement
    {
        [SerializeField] protected bool m_enabled = true;
        public bool Enabled { get => m_enabled; set => m_enabled = value; }

        [SerializeField] protected ExGizmosDrawContext m_context = ExGizmosDrawContext.Identity;
        public ExGizmosDrawContext Context { get => m_context; set => m_context = value; }

        public abstract void Draw(ExGizmosDrawContext baseContext_);
    }
}