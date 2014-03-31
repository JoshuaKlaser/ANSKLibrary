using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ModelAnimationLibrary
{
    //[StructLayout(LayoutKind.Explicit, Pack = 64)] 
    public struct ANSKVertexDeclaration : IVertexType
    {
        //[FieldOffset(0)]
        public Vector3 Position;
        //[FieldOffset(12)]
        public Vector2 Uv;
        //[FieldOffset(20)]
        public Vector3 Normal;
        public int4 Indices;
        public float4 Weights;

        public static readonly VertexDeclaration VertexDeclaration;

        public static readonly int SizeInBytes = (sizeof(float) * (3 + 2 + 3 + 4)) + (sizeof(int) * 4);

        public ANSKVertexDeclaration(Vector3 pos, Vector2 uv, Vector3 normal, int4 indices, float4 weights)
        {
            Position = pos;
            Uv = uv;
            Normal = normal;
            Indices = indices;
            Weights = weights;
        }

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexDeclaration; }
        }

        static ANSKVertexDeclaration()
        {
            VertexElement[] elements = new VertexElement[]
            {
                /*new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position,0),
                new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
                new VertexElement(20, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                new VertexElement(36, VertexElementFormat.Short4, VertexElementUsage.BlendIndices, 0),
                new VertexElement(40, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0)*/
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position,0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
                new VertexElement(sizeof(float) * 5, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                new VertexElement(sizeof(float) * 8, VertexElementFormat.Short4, VertexElementUsage.BlendIndices, 0),
                new VertexElement(sizeof(float) * 8 + sizeof(int) * 4, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0)
            };
            VertexDeclaration = new VertexDeclaration(elements);
        }
    }
}
