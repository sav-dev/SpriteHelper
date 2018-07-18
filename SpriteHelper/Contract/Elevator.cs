using System.Drawing;
using System.Runtime.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Elevator
    {
        // Position.
        [DataMember]
        public int X { get; set; }

        // Position.
        [DataMember]
        public int Y { get; set; }

        // Size
        [DataMember]
        public int Size { get; set; }

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

        // Whether initially the elevator is flipped.
        [DataMember]
        public bool InitialFlip { get; set; }

        // Screen the elevator is on.
        public int Screen => this.X / (Constants.ScreenWidthInTiles * Constants.BackgroundTileWidth);

        // Movement range.
        public int MovementRange => this.MaxPosition - this.MinPosition;

        // Width and height.
        public int Width => this.Size * Constants.SpriteWidth;
        public int Height => Constants.ElevatorHeight;

        // Initial movement distance left.
        public int InitialDistanceLeft
        {
            get
            {
                int position;
                switch (this.MovementType)
                {
                    case MovementType.Horizontal:
                        position = this.X;
                        break;
                    case MovementType.Vertical:
                        position = this.Y;
                        break;
                    case MovementType.None:
                    default:
                        return 0;
                }

                return this.InitialFlip ? position - this.MinPosition : this.MaxPosition - position;
            }
        }

        // Returns true if this elevator is within these coordinates.
        public bool Select(Point position)
        {
            var positionX = position.X / Constants.LevelEditorZoom;
            var positionY = position.Y / Constants.LevelEditorZoom;

            return position.X >= this.X && position.X < this.X + this.Width &&
                   position.Y >= this.Y && position.Y < this.Y + this.Height;
        }

        // Returns a rectangle where:
        //  - x1 = floor(this.x / 16)
        //  - x2 = roof((this.x + this.width) / 16)
        // Same for y
        public Rectangle GetGameRectangleInMetaTiles()
        {
            var x1 = this.X / Constants.BackgroundTileWidth;
            var x2 = (this.X + this.Width + Constants.BackgroundTileWidth - 1) / Constants.BackgroundTileWidth;
            var y1 = this.Y / Constants.BackgroundTileHeight;
            var y2 = (this.Y + this.Height + Constants.BackgroundTileHeight - 1) / Constants.BackgroundTileHeight;
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        // String representation.
        public override string ToString()
        {
            return $"Elevator ({this.X}/{this.Y})";
        }
    }
}
