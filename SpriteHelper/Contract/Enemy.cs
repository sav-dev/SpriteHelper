using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Enemy
    {
        // Same as Animation.Name
        [DataMember]
        public string Name { get; private set; }

        // Whether initially the enemy is flipped
        [DataMember]
        public bool InitialFlip { get; set; }

        // Position.
        [DataMember]
        public int X { get; set; }

        // Position.
        [DataMember]
        public int Y { get; set; }

        // Width (accounting for Zoom)
        public int Width { get; private set; }

        // Height (accounting for Zoom)
        public int Height { get; private set; }

        // Default constructor.
        public Enemy()
        {          
        }

        // Static factory.
        public static Enemy CreateInitialized(Animation animation)
        {
            var result = new Enemy();
            result.Initialize(animation);
            return result;
        }

        // Initialization method.
        public void Initialize(Animation animation)
        {
            this.Name = animation.Name;
            var firstFrame = animation.Frames.First();
            this.Width = firstFrame.Width * Constants.SpriteWidth * Constants.LevelEditorZoom;
            this.Height = firstFrame.Height * Constants.SpriteHeight * Constants.LevelEditorZoom;
        }

        // Returns true if this enemy is within these coordinates.
        public bool Select(Point position)
        {
            return position.X >= this.X && position.X < this.X + this.Width &&
                   position.Y >= this.Y && position.Y < this.Y + this.Height;
        }


        // String representation.
        public override string ToString()
        {
            return $"{this.Name} ({this.X}/{this.Y})";
        }
    }
}
