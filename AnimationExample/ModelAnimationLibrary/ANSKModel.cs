using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModelAnimationLibrary
{
    public class ANSKModel
    {
        private ANSK _ansk;
        private ANSKTagData _tagData;
        private AnimationPlayer _player;
        private AnimationClip _currentClip;
        private List<Vector3> _verts;
        private List<int> _indicies;
        private List<Vector2> _uvs;
        private List<int> _uvIndicies;
        private List<int> _edges;
        private List<Vector3> _normals;
        private Effect _effect;
        private ANSKVertexDeclaration[] _verticies;
        private VertexBuffer _vertBuffer;
        private GraphicsDevice _gDevice;

        public List<Vector3> Verticies { get { return _verts; } set { _verts = value; CreateDeclarationList(); } }

        public ANSKTagData TagData { get { return _tagData; } }

        public ANSKModel(ANSKModelContent content)
        {
            _tagData = content.TagData;
            _verts = content.Verticies;
            _indicies = content.VertexIndicies;
            _uvs = content.Uvs;
            _uvIndicies = content.UvIndicies;
            _edges = content.Edges;
            _normals = content.Normals;

            // Find a way to load in the effect;

            _verticies = new ANSKVertexDeclaration[_verts.Count];
        }

        public void ManualInitialise(GraphicsDevice device, Effect effect, Game game)
        {
            _effect = effect;
            _gDevice = device;

            try
            {
                _vertBuffer = new VertexBuffer(_gDevice, typeof(ANSKVertexDeclaration), _verts.Count, BufferUsage.None);
            }
            catch (Exception e)
            {
                int poo = 0;
            }
            _ansk = new ANSK(this, game);
            _player = new AnimationPlayer(_tagData.SkinData);

            CreateDeclarationList();
        }

        public void CreateDeclarationList()
        {
            for (int i = 0; i < _verts.Count; i++)
            {
                // TODO -- This is commented as we do not have the logic that generates the
                // required indices and weights per vertex.
                //_verticies[i] = new ANSKVertexDeclaration(_verts[i], _uvs[i], _normals[i]);
            }

            _vertBuffer.SetData<ANSKVertexDeclaration>(_verticies.ToArray<ANSKVertexDeclaration>());
        }

        public void Update(GameTime gameTime, Matrix transform)
        {
            _ansk.Update(gameTime);
            _player.Update(gameTime.ElapsedGameTime, true, transform);
        }

        public void Draw(GameTime gameTime, Matrix transform)
        {
            Matrix[] bones = _player.GetSkinTransforms();
            Matrix[] transforms = new Matrix[_ansk.SkinningAndBasicAnims.SkeletonHierarchy.Count];

            _gDevice.SetVertexBuffer(_vertBuffer);

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                _gDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _verts.Count);
            }
            // We use the graphics device Draw Indexed Primitives or something similar.
        }
    }
}