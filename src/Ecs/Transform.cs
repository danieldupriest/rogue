﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecs
{
    
    public class Transform : Component
    {
        public Transform parent = null;
        public List<Transform> children = new List<Transform>();
        public Vec2i position = new Vec2i();

        public void SetParent(Transform transform)
        {
            this.parent = transform;
            this.parent.children.Add(transform);
            return;
        }

        public void Translate(int dx, int dy)
        {
            this.position.x += dx;
            this.position.y += dy;
            return;
        }

    }
    
}