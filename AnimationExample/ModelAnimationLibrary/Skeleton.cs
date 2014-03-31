using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ModelAnimationLibrary
{
    public class Skeleton
    {
        private Joint _rootJoint;

        private List<Joint> _unTreedJoints;

        public Skeleton()
        {
            _rootJoint = null;
            _unTreedJoints = new List<Joint>();
        }

        public void AddJoint(Joint joint)
        {
            if (joint.IsRootJoint)
            {
                _rootJoint = joint;
                UpdateChildren();
            }
            else if (_rootJoint == null)
            {
                _unTreedJoints.Add(joint);
                return;
            }
            else
            {
                _unTreedJoints.Add(joint);
                UpdateChildren();
            }
        }

        private void UpdateChildren()
        {
            for (int i = 0; i < _unTreedJoints.Count; i++)
            {
                bool added = _rootJoint.AddChildNode(_unTreedJoints[i]);

                if (added)
                {
                    _unTreedJoints.RemoveAt(i);
                    i--;
                }
            }
        }

        public void FinaliseJointTree()
        {
            UpdateChildren();

            if (_unTreedJoints.Count > 0)
                throw new Exception("Joints still unlinked in final check.");
            else
                _unTreedJoints = null;
        }
    }
}
