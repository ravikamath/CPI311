using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class Collider : Component
    {
        public virtual bool Collides(Collider other) { return false; }

        public virtual bool Collides(Collider other, out Vector3 normal)
        {
            normal = Vector3.Zero;
            return false;
        }

        public virtual bool Intersects(Ray ray) { return false; }

        public virtual bool Intersects(Ray ray, out float parameter)
        {
            parameter = 0;
            return false;
        }
    }
}
