using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    /// <summary>
    /// Represents a camera to manage viewing information
    /// Should be attached to a GameObject
    /// --- Incomplete --- 
    /// 1. Make it work for both perspective and orthographic cameras
    /// 2. Use other methods (off-center cameras)
    /// </summary>
    public class Camera : Component
    {
        public static Camera Current { get; set; }

        public float FieldOfView { get; set; }
        public float AspectRatio { get; set; }
        public float NearPlane { get; set; }
        public float FarPlane { get; set; }

        /// <summary>
        /// Normalized viewport position for this camera
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Normalized viewport size for this camera
        /// </summary>
        public Vector2 Size { get; set; }
        /// <summary>
        /// The non-normalized viewport of this camera
        /// </summary>
        public Viewport Viewport
        {
            get
            {
                return new Viewport((int)(Screen.Width * Position.X),
                            (int)(Screen.Height * Position.Y),
                            (int)(Screen.Width * Size.X),
                            (int)(Screen.Height * Size.Y));
            }
        }

        // The contructor is public, so we can still create one outside
        // but it is perhaps not a good idea.
        // Solution: change to internal
        public Camera()
        {
            FieldOfView = MathHelper.PiOver2;
            AspectRatio = Screen.Viewport.AspectRatio;
            NearPlane = 0.1f;
            FarPlane = 100f;
            Position = Vector2.Zero;
            Size = Vector2.One;
        }

        /// <summary>
        /// Computes a viewing matrix using the transform
        /// this camera is attached to. If there is no parent,
        /// it returns the identity matrix.
        /// </summary>
        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(Transform.Position,
                        Transform.Position + Transform.Forward,
                        Transform.Up);
            }
        }

        /// <summary>
        /// Computes the Projection matrix using properties of this camera
        /// </summary>
        public Matrix Projection
        {
            // Right now, we only create perspective on-center projection
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(
                    FieldOfView, AspectRatio, NearPlane, FarPlane);
            }
        }

        /// <summary>
        /// Computes a ray that projects from the camera position into
        /// the screen point provided as input
        /// </summary>
        /// <param name="position">Position in Screen Space</param>
        /// <returns>Non-normalized ray</returns>
        public Ray ScreenPointToWorldRay(Vector2 position)
        {
            Vector3 start = Viewport.Unproject(
                new Vector3(position, 0), Projection, View, Matrix.Identity);
            Vector3 end = Viewport.Unproject(
                new Vector3(position, 1), Projection, View, Matrix.Identity);
            return new Ray(start, end - start);
        }
    }
}
