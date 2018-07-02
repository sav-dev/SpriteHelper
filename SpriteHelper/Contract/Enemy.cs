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

        // Position.
        [DataMember]
        public int X { get; set; }

        // Position.
        [DataMember]
        public int Y { get; set; }

        // Movement type.
        [DataMember]
        public MovementType MovementType { get; set; }

        // Speed
        [DataMember]
        public int Speed { get; set; }

        // Movement range.
        [DataMember]
        public int MinPosition { get; set; }

        // Movement range.
        [DataMember]
        public int MaxPosition { get; set; }

        // Whether initially the enemy is flipped
        [DataMember]
        public bool InitialFlip { get; set; }

        private int width;
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
            this.width = firstFrame.Width * Constants.SpriteWidth;
            this.height = firstFrame.Height * Constants.SpriteHeight;
        }

        // Returns true if this enemy is within these coordinates.
        public bool Select(Point position)
        {
            var positionX = position.X / Constants.LevelEditorZoom;
            var positionY = position.Y / Constants.LevelEditorZoom;

            return position.X >= this.X && position.X < this.X + this.width &&
                   position.Y >= this.Y && position.Y < this.Y + this.height;
        }

        // Returns a rectangle where:
        //  - x1 = floor(this.x / 16)
        //  - x2 = roof((this.x + this.width) / 16)
        // Same for y
        public Rectangle GetGameRectangleInMetaTiles()
        {
            var x1 = this.X / Constants.BackgroundTileWidth;
            var x2 = (this.X + this.width + Constants.BackgroundTileWidth - 1) / Constants.BackgroundTileWidth;
            var y1 = this.Y / Constants.BackgroundTileHeight;
            var y2 = (this.Y + this.height + Constants.BackgroundTileHeight - 1) / Constants.BackgroundTileHeight;
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        // String representation.
        public override string ToString()
        {
            return $"{this.Name} ({this.X}/{this.Y})";
        }
    }
}
