using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class Light : Component
    {
        public static Light Current { get; set; }

        public Color Ambient { get; set; }
        public Color Diffuse { get; set; }
        public Color Specular { get; set; }

        public Light()
        {
            Ambient = Color.White;
            Diffuse = Color.White;
            Specular = Color.White;
        }
    }
}
