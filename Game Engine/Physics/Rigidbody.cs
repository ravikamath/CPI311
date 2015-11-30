using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class Rigidbody : Component, IUpdateable
    {
        private float mass;
        public float Mass {
            get { return mass; }
            set
            {
                if (value <= 0)
                    value = 0.01f;
                mass = value;
            }
        }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 Impulse { get; set; }

        public Rigidbody()
        {
            Mass = 1;
            Velocity = Vector3.Zero;
            Acceleration = Vector3.Zero;
            Impulse = Vector3.Zero;

        }

        public void Update()
        {
            Velocity += Impulse / Mass;
            Transform.Position += Velocity * Time.ElapsedGameTime + 0.5f * Acceleration * Time.ElapsedGameTime * Time.ElapsedGameTime;
            Velocity += Acceleration * Time.ElapsedGameTime;
            Impulse = Vector3.Zero;
        }
    }
}
