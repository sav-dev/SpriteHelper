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
        public string Name { get; set; }

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
        private int width;

        // Height (accounting for Zoom)
        private int height;

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
            this.width = firstFrame.Width * Constants.SpriteWidth * Constants.LevelEditorZoom;
            this.height = firstFrame.Height * Constants.SpriteHeight * Constants.LevelEditorZoom;
        }

        // Returns true if this enemy is within these coordinates.
        public bool Select(Point position)
        {
            return position.X >= this.X && position.X < this.X + this.width &&
                   position.Y >= this.Y && position.Y < this.Y + this.height;
        }


        // String representation.
        public override string ToString()
        {
            return $"{this.Name} ({this.X}/{this.Y})";
        }
    }
}
