using System;
using System.Collections.Generic;
using OpenTK;

namespace Jack.Core
{
    public class Node
    {
        public static int NodeCount { get; private set; }

        public int Id { get; } = ++NodeCount;
        public string Name { get; set; }

        public Node Parent { get; set; }
        public List<Node> Children { get; set; } = null;

        public Transform Transform { get; set; }

        private Transform _globalTransform;
        public Transform GlobalTransform
        {
            get
            {
                if (Parent == null)
                {
                    return Transform;
                }
                else
                {
                    _globalTransform.Position = Parent.Transform.Position + Transform.Position;
                    _globalTransform.Scale = Parent.Transform.Scale + Transform.Scale;
                    _globalTransform.Rotation = Parent.Transform.Rotation + Transform.Rotation;

                    return _globalTransform;
                }
            }
            set
            {
                if (Parent == null)
                {
                    Transform = value;
                }
                else
                {
                    // todo: find a way to make a setter for this
                }
            }
        }

        public Node(string name = null)
        {
            Name = name == null ? "Node-" + NodeCount : name;
            _globalTransform = new Transform();
            Transform = new Transform { Node = this };
        }

        public void AddChild(Node node)
        {
            // note: adding this gave a 10 fps increase at 5000 quad nodes
            if (Children == null)
            {
                Children = new List<Node>();
            }

            node.Parent = this;
            Children.Add(node);
        }

        public void RemoveChild(Node node)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (node.Id == Children[i].Id)
                {
                    node.Parent = null;
                    Children.RemoveAt(i);
                }
            }
        }
    }
}