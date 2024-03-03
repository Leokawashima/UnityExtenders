using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GaMe.ExMesh
{
    public class ExGizmosDefaultDrawLine
    {
        
    }

    public class ExGizmosDefaultDrawLineList
    {

    }

    public class ExGizmosDefaultDrawLineStrip
    {

    }

    public class ExGizmosDefaultDrawRay
    {
        
    }

    public class ExGizmosDefaultDrawCube : ExGizmosDrawElement
    {
        public override void Draw(Matrix4x4 matrix_)
        {
            //Gizmos.DrawCube(pos_ + Position, scale_ + Scale);
        }
    }

    public class ExGizmosDefaultDrawSphere : ExGizmosDrawElement
    {
        [SerializeField] private float m_radius;
        public float Radius { get => m_radius; set => m_radius = value; }

        public override void Draw(Matrix4x4 matrix_)
        {
            
        }
    }

    public class ExGizmosDefaultDrawMesh
    {

    }

    public class ExGizmosDefaultDrawFrustum
    {

    }

    public class ExGizmosDefaultDrawIcon
    {

    }

    public class ExGizmosDefaultDrawGUITexture
    {

    }
}