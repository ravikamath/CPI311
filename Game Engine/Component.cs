
namespace GameEngine
{
    public abstract class Component
    {
        /// <summary>
        /// GameeObject instance this component is attached to
        /// </summary>
        public GameObject GameObject {get; internal set; }

        /// <summary>
        /// The Transform instance this component is attached to
        /// (retrieved from the GameObject instance)
        /// </summary>
        public Transform Transform { get { return GameObject.Transform; } }
    }
}
