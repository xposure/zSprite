﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Asteroids.Assets;
using Atma.Asteroids.Assets.Types;
using Atma.Core;
using OpenGL = OpenTK.Graphics.OpenGL;
using ES11 = OpenTK.Graphics.ES11;
using TK = OpenTK;

namespace Atma.Asteroids.Engine.Subsystems.OpenTK
{
    public class OpenTKMesh : AbstractAsset<MeshData>, IMesh//, Mesh//, IAsset<IAssetData>
    {
        private static readonly Logger logger = Logger.getLogger(typeof(OpenTKMesh));

        private const int FLOAT_SIZE = 4;

        private const int VERTEX_SIZE = 3;
        private const int TEX_COORD_0_SIZE = 2;
        private const int TEX_COORD_1_SIZE = 3;
        private const int COLOR_SIZE = 4;
        private const int NORMAL_SIZE = 3;

        private MeshData data;

        private int stride;
        private int vertexOffset;
        private int texCoord0Offset;
        private int texCoord1Offset;
        private int colorOffset;
        private int normalOffset;

        private bool hasTexCoord0;
        private bool hasTexCoord1;
        private bool hasColor;
        private bool hasNormal;

        //private int vboVertexBuffer;
        //private int vboIndexBuffer;
        private int indexCount;

        public OpenTKMesh(AssetUri uri, MeshData data)
            : base(uri)
        {
            reload(data);
                
        }

        //public override List<float> getVertices()
        //{
        //    throw new NotImplementedException();
        //}

        public RenderOperationType OperationType { get; set; }

        public void reload(MeshData data)
        {
            buildMesh(data);
        }

        private void buildMesh(MeshData newData)
        {
            //this.data = data;
            this.data = newData;

            hasTexCoord0 = data.texCoord0 != null;
            hasTexCoord1 = data.texCoord1 != null;
            hasNormal = data.normals != null;
            hasColor = data.colors != null;
            stride = 0;
            vertexOffset = 0;
            texCoord0Offset = 0;
            texCoord1Offset = 0;
            colorOffset = 0;
            normalOffset = 0;
            indexCount = data.indices.Length;

            
            ////List<TFloatIterator> parts = Lists.newArrayList();
            ////TIntList partSizes = new TIntArrayList();
            //int vertexCount = newData.getVertices().Length;
            //int vertexSize = VERTEX_SIZE;
            //parts.add(newData.getVertices().iterator());
            //partSizes.add(VERTEX_SIZE);

            //if (newData.getTexCoord0() != null && newData.getTexCoord0().size() / TEX_COORD_0_SIZE == vertexCount)
            //{
            //    parts.add(newData.getTexCoord0().iterator());
            //    partSizes.add(TEX_COORD_0_SIZE);
            //    texCoord0Offset = vertexSize * FLOAT_SIZE;
            //    vertexSize += TEX_COORD_0_SIZE;
            //    hasTexCoord0 = true;
            //}
            //if (newData.getTexCoord1() != null && newData.getTexCoord1().size() / TEX_COORD_1_SIZE == vertexCount)
            //{
            //    parts.add(newData.getTexCoord1().iterator());
            //    partSizes.add(TEX_COORD_1_SIZE);
            //    texCoord1Offset = vertexSize * FLOAT_SIZE;
            //    vertexSize += TEX_COORD_1_SIZE;
            //    hasTexCoord1 = true;
            //}
            //if (newData.getNormals() != null && newData.getNormals().size() / NORMAL_SIZE == vertexCount)
            //{
            //    parts.add(newData.getNormals().iterator());
            //    partSizes.add(NORMAL_SIZE);
            //    normalOffset = vertexSize * FLOAT_SIZE;
            //    vertexSize += NORMAL_SIZE;
            //    hasNormal = true;
            //}
            //if (newData.getColors() != null && newData.getColors().size() / COLOR_SIZE == vertexCount)
            //{
            //    parts.add(newData.getColors().iterator());
            //    partSizes.add(COLOR_SIZE);
            //    colorOffset = vertexSize * FLOAT_SIZE;
            //    vertexSize += COLOR_SIZE;
            //    hasColor = true;
            //}
            //stride = vertexSize * FLOAT_SIZE;
            //indexCount = newData.getIndices().size();

            //createVertexBuffer(parts, partSizes, vertexCount, vertexSize);
            //createIndexBuffer(newData.getIndices());

            //aabb = AABB.createEncompasing(newData.getVertices());
        }

        protected override void ondispose()
        {
            

            hasTexCoord0 = false;
            hasTexCoord1 = false;
            hasColor = false;
            hasNormal = false;
            stride = 0;
            vertexOffset = 0;
            texCoord0Offset = 0;
            texCoord1Offset = 0;
            colorOffset = 0;
            normalOffset = 0;
            indexCount = 0;
            //if (vboVertexBuffer != 0)
            //{
            //    //bufferPool.dispose(vboVertexBuffer);
            //    vboVertexBuffer = 0;
            //}
            //if (vboIndexBuffer != 0)
            //{
            //    //bufferPool.dispose(vboIndexBuffer);
            //    vboIndexBuffer = 0;
            //}
        }

        public void render()
        {
            if (!isDisposed)
            {
                //OpenGL.GL.Begin(OpenGL.PrimitiveType.Triangles);

                //OpenGL.GL.Color3(Color.MidnightBlue.r, Color.MidnightBlue.g, Color.MintCream.b);
                //OpenGL.GL.Vertex2(-1.0f, 1.0f);
                //OpenGL.GL.Color3(Color.SpringGreen.r, Color.SpringGreen.g, Color.SpringGreen.b);
                //OpenGL.GL.Vertex2(0.0f, -1.0f);
                //OpenGL.GL.Color3(Color.Ivory.r, Color.Ivory.g, Color.Ivory.b);
                //OpenGL.GL.Vertex2(1.0f, 1.0f);

                //OpenGL.GL.End();

                preRender();
                dorender();
                postrender();
            }
            else
            {
                logger.error("Attempted to render disposed mesh: {0}", uri);
            }
        }

        public void preRender()
        {
            if (!isDisposed)
            {
                //OpenGL.GL.Begin(OpenGL.PrimitiveType.Triangles);
                //return;

                //glEnableClientState(GL_VERTEX_ARRAY);
                ES11.GL.EnableClientState(ES11.EnableCap.VertexArray);


                if (hasTexCoord0 || hasTexCoord1)
                {
                    //glEnableClientState(GL_TEXTURE_COORD_ARRAY);
                    ES11.GL.EnableClientState(ES11.EnableCap.TextureCoordArray);
                }
                if (hasColor)
                {
                    //glEnableClientState(GL_COLOR_ARRAY);
                    ES11.GL.EnableClientState(ES11.EnableCap.ColorArray);
                }
                if (hasNormal)
                {
                    //glEnableClientState(GL_NORMAL_ARRAY);
                    ES11.GL.EnableClientState(ES11.EnableCap.NormalArray);
                }

                //GL15.glBindBuffer(GL15.GL_ARRAY_BUFFER, vboVertexBuffer);
                //ES11.GL.BindBuffer(ES11.All.ArrayBuffer, vboVertexBuffer);
                
                //GL15.glBindBuffer(GL15.GL_ELEMENT_ARRAY_BUFFER, vboIndexBuffer);
                //ES11.GL.BindBuffer(ES11.All.ElementArrayBuffer, vboIndexBuffer);

                //glVertexPointer(VERTEX_SIZE, GL11.GL_FLOAT, stride, vertexOffset);
                ES11.GL.VertexPointer(VERTEX_SIZE, ES11.VertexPointerType.Float, stride, data.getVertices());


                if (hasTexCoord0)
                {
                    //GL13.glClientActiveTexture(GL13.GL_TEXTURE0);
                    ES11.GL.ClientActiveTexture(ES11.TextureUnit.Texture0);
                    //glTexCoordPointer(TEX_COORD_0_SIZE, GL11.GL_FLOAT, stride, texCoord0Offset);
                    ES11.GL.TexCoordPointer(TEX_COORD_0_SIZE, ES11.TexCoordPointerType.Float, stride, data.getTexCoord0());
                }

                if (hasTexCoord1)
                {
                    //GL13.glClientActiveTexture(GL13.GL_TEXTURE1);
                    ES11.GL.ClientActiveTexture(ES11.TextureUnit.Texture1);
                    //glTexCoordPointer(TEX_COORD_1_SIZE, GL11.GL_FLOAT, stride, texCoord1Offset);
                    ES11.GL.TexCoordPointer(TEX_COORD_1_SIZE, ES11.TexCoordPointerType.Float, stride, data.getTexCoord1());
                }

                if (hasColor)
                {
                    //glColorPointer(COLOR_SIZE, GL11.GL_FLOAT, stride, colorOffset);
                    ES11.GL.ColorPointer(COLOR_SIZE, ES11.ColorPointerType.Float, stride, data.getColors());
                }
                if (hasNormal)
                {
                    //glNormalPointer(GL11.GL_FLOAT, stride, normalOffset);
                    ES11.GL.NormalPointer(ES11.NormalPointerType.Float, stride, data.getNormals());
                }
            }
            else
            {
                logger.error("Attempted to render disposed mesh: {0}", uri);
            }
        }

        private void dorender()
        {
            if (!isDisposed)
            {
               
                //GL11.glDrawElements(GL11.GL_TRIANGLES, indexCount, GL_UNSIGNED_INT, 0);
                ES11.GL.DrawElements(ES11.PrimitiveType.Triangles, indexCount, ES11.All.UnsignedShort, data.getIndices());
                //ES11.GL.DrawArrays(ES11.PrimitiveType.Triangles, 0, indexCount);
            }
            else
            {
                logger.error("Attempted to render disposed mesh: {0}", uri);
            }
        }

        private void postrender()
        {
            if (!isDisposed)
            {
                //OpenGL.GL.End();
                //return;
                if (hasNormal)
                {
                    //glDisableClientState(GL_NORMAL_ARRAY);
                    ES11.GL.DisableClientState(ES11.EnableCap.NormalArray);
                }
                if (hasColor)
                {
                    //glDisableClientState(GL_COLOR_ARRAY);
                    ES11.GL.DisableClientState(ES11.EnableCap.ColorArray);
                }
                if (hasTexCoord0 || hasTexCoord1)
                {
                    //glDisableClientState(GL_TEXTURE_COORD_ARRAY);
                    ES11.GL.DisableClientState(ES11.EnableCap.TextureCoordArray);
                }
                //glDisableClientState(GL_VERTEX_ARRAY);
                ES11.GL.DisableClientState(ES11.EnableCap.VertexArray);

                //GL15.glBindBuffer(GL15.GL_ARRAY_BUFFER, 0);
                //ES11.GL.BindBuffer(ES11.All.ArrayBuffer, 0);
                //GL15.glBindBuffer(GL15.GL_ELEMENT_ARRAY_BUFFER, 0);
                //ES11.GL.BindBuffer(ES11.All.ElementArrayBuffer, 0);
            }
            else
            {
                logger.error("Attempted to render disposed mesh: {0}", uri);
            }
        }
    }
}
