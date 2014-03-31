using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ModelAnimationLibrary
{
    public class Joint
    {
        private string _name;
        private int _id;
        private int _parentId;
        private Matrix _translation;
        private Matrix _rotation;
        private Matrix _scale;
        private Matrix _transformation;
        private Joint _parent;
        private List<Joint> _children;
        // The order of indices and weights must match.
        private List<int> _indices;
        private List<float> _weights;

        public int Id { get { return _id; } }
        public int ParentId { get { return _parentId; } }
        public bool IsRootJoint { get { return (_id == _parentId); } }

        public Joint(string name, int id, int parentId, Vector3 translation, Vector3 rotation, Vector3 scale, List<int> indices, List<float> weights)
        {
            _name = name;
            _id = id;
            _parentId = parentId;
            _parent = null;
            _children = new List<Joint>();
            _translation = Matrix.CreateTranslation(translation);
            _rotation = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            _rotation += Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            _rotation += Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
            _scale = Matrix.CreateScale(scale);
            _transformation = _scale * _rotation * _translation;
            _indices = indices;
            _weights = weights;
        }

        public bool AddChildNode(Joint child)
        {
            // If the child Joint is the parent of the child we need to add.
            if (_id == child.ParentId)
            {
                child._parent = this;
                _children.Add(child);
                return true;
            }

            // If the child Joint is not this Joint's child, check through the current children
            // of this Joint.
            for (int i = 0; i < _children.Count; i++)
            {
                bool done = _children[i].AddChildNode(child);

                if (done)
                    return true;
            }

            // If this Joint and it's children are not the parent or we hit the end Joint,
            // just go back up the tree.
            return false;
        }
    }
}
