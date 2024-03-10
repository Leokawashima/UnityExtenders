using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GaMe.ExMesh
{
    public class ExGizmosDefaultDrawLine : ExGizmosDrawElement
    {
        [SerializeField] private Vector3 m_from = Vector3.zero;
        public Vector3 From { get => m_from; set => m_from = value; }

        [SerializeField] private Vector3 m_to = new(1.0f, 0.0f, 0.0f);
        public Vector3 To { get => m_to; set => m_to = value; }

        public override void Draw(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawLine(m_from, m_to);
        }
    }

    public class ExGizmosDefaultDrawLineList : ExGizmosDrawElement
    {
        [SerializeField] private Vector3[] m_points = new Vector3[]
        {
            Vector3.zero,
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
        };

        public override void Draw(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawLineList(m_points);
        }
    }

    public class ExGizmosDefaultDrawLineStrip : ExGizmosDrawElement
    {
        [SerializeField] private bool m_isLoop = true;
        public bool IsLoop { get => IsLoop; set => m_isLoop = value; }

        [SerializeField]
        private Vector3[] m_points = new Vector3[]
        {
            Vector3.zero,
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
        };

        public override void Draw(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawLineStrip(m_points, m_isLoop);
        }
    }

    public class ExGizmosDefaultDrawRay : ExGizmosDrawElement
    {
        [SerializeField] private Vector3 m_from = Vector3.zero;
        public Vector3 From { get => m_from; set => m_from = value; }

        [SerializeField] private Vector3 m_direction = new(1.0f, 0.0f, 0.0f);
        public Vector3 Direction { get => m_direction; set => m_direction = value; }

        public override void Draw(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawRay(new Ray(m_from, m_direction));
        }
    }

    public class ExGizmosDefaultDrawCube : ExGizmosDrawElement, IExGizmosWire
    {
        [SerializeField] private bool m_isWire = false;
        public bool IsWire { get => m_isWire; set => m_isWire = value; }

        [SerializeField] private Vector3 m_center = Vector3.zero;
        public Vector3 Center { get => m_center; set => m_center = value; }

        [SerializeField] private Vector3 m_size = Vector3.one;
        public Vector3 Size { get => m_size; set => m_size = value; }

        public override void Draw(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawCube(m_center, m_size);
        }

        public void DrawWire(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawWireCube(m_center, m_size);
        }
    }

    public class ExGizmosDefaultDrawSphere : ExGizmosDrawElement, IExGizmosWire
    {
        [SerializeField] private bool m_isWire = false;
        public bool IsWire { get => m_isWire; set => m_isWire = value; }

        [SerializeField] private Vector3 m_center = Vector3.zero;
        public Vector3 Center { get => m_center; set => m_center = value; }

        [SerializeField] private float m_radius = 1.0f;
        public float Radius { get => m_radius; set => m_radius = value; }

        public override void Draw(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawSphere(m_center, m_radius);
        }

        public void DrawWire(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawWireSphere(m_center, m_radius);
        }
    }

    public class ExGizmosDefaultDrawMesh : ExGizmosDrawElement, IExGizmosWire
    {
        [SerializeField] private bool m_isWire = false;
        public bool IsWire { get => m_isWire ; set => m_isWire = value; }

        [SerializeField] private Vector3 m_postion = Vector3.zero;
        public Vector3 Positon { get => m_postion; set => m_postion = value; }

        [SerializeField] private Quaternion m_rotation = Quaternion.identity;
        public Quaternion Rotation { get => m_rotation; set => m_rotation = value; }

        [SerializeField] private Vector3 m_scale = Vector3.one;
        public Vector3 Scale { get => m_scale; set => m_scale = value; }

        [SerializeField] private Mesh m_mesh = null;
        public Mesh Mesh { get => m_mesh; set => m_mesh = value; }

        [SerializeField] private int m_subMeshIndex = -1;
        public int SubMeshIndex { get => m_subMeshIndex; set => m_subMeshIndex = value; }

        public override void Draw(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawMesh(m_mesh, m_subMeshIndex, m_postion, m_rotation, m_scale);
        }

        public void DrawWire(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawWireMesh(m_mesh, m_subMeshIndex, m_postion, m_rotation, m_scale);
        }
    }

    public class ExGizmosDefaultDrawFrustum : ExGizmosDrawElement
    {
        [SerializeField] private Vector3 m_center = Vector3.zero;
        public Vector3 Center { get => m_center; set => m_center = value; }

        [SerializeField] private float m_fov = 60.0f;
        public float FOV { get => m_fov; set => m_fov = value; }

        [SerializeField] private float m_maxRange = 10.0f;
        public float MaxRange { get => m_maxRange; set => m_maxRange = value; }

        [SerializeField] private float m_minRange = 1.0f;
        public float MinRange { get => m_minRange; set => m_minRange = value; }

        [SerializeField] private float m_aspect = 1.0f;
        public float Aspect { get => m_aspect; set => m_aspect = value; }

        public override void Draw(ExGizmosDrawContext baseContext_)
        {
            var _thisContext = Context;

            {
                Gizmos.color = baseContext_.Color * _thisContext.Color;
                Gizmos.matrix = baseContext_.Matrix * _thisContext.Matrix;
            }

            Gizmos.DrawFrustum(m_center, m_fov, m_maxRange, m_minRange, m_aspect);
        }
    }

    public class ExGizmosDefaultDrawIcon
    {

    }

    public class ExGizmosDefaultDrawGUITexture
    {

    }
}