using System;
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
        public double Speed { get; set; }

        // Movement range.
        [DataMember]
        public int MinPosition { get; set; }

        // Movement range.
        [DataMember]
        public int MaxPosition { get; set; }

        // Initial direction
        [DataMember]
        public Direction Direction { get; set; }

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
                switch (this.MovementType)
                {
                    case MovementType.Horizontal:
                        return this.Direction == Direction.Left ? this.X - this.MinPosition : this.MaxPosition - this.X;
                    case MovementType.Vertical:
                        return this.Direction == Direction.Up ? this.Y - this.MinPosition : this.MaxPosition - this.Y;
                    case MovementType.None:
                    case MovementType.NoneAlwaysAnimate:
                    default:
                        return 0;
                }
            }
        }

        // Player rectangle (the rectangle the player can be in either when on the platform on when colliding with the platform -
        // the latter includes the former)
        public Rectangle PlayerRectangle
        {
            get
            {
                var elevatorRectangle = this.ElevatorRectangle;

                // Not sure about this logic, let's be more strict than needed (hence the +/- 2)
                var x1 = elevatorRectangle.X - Constants.PlayerPlatformBoxWidth;
                var x2 = elevatorRectangle.X + elevatorRectangle.Width + Constants.PlayerPlatformBoxWidth;
                var y1 = elevatorRectangle.Y - Math.Abs(Constants.PlayerPlatformBoxHeight);
                var y2 = elevatorRectangle.Y + elevatorRectangle.Height + Math.Abs(Constants.PlayerPlatformBoxHeight);

                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
        }

        // Elevator rectangle (the rectangle the elevator moves in).
        public Rectangle ElevatorRectangle
        {
            get
            {
                var x1 = this.MovementType == MovementType.Horizontal ? this.MinPosition : this.X;
                var x2 = (this.MovementType == MovementType.Horizontal ? this.MaxPosition : this.X) + this.Size * Constants.ElevatorWidthPerBlock;
                var y1 = this.MovementType == MovementType.Vertical ? this.MinPosition : this.Y;
                var y2 = (this.MovementType == MovementType.Vertical ? this.MaxPosition : this.Y) + Constants.ElevatorHeight;

                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
        }

        // Movement rectangle (how the top left point moves)
        public Rectangle MovementRectangle
        {
            get
            {
                var elevatorMinX = -1;
                var elevatorMaxX = -1;
                var elevatorMinY = -1;
                var elevatorMaxY = -1;

                if (this.MovementType == MovementType.None)
                {
                    elevatorMinX = this.X;
                    elevatorMaxX = this.X;
                    elevatorMinY = this.Y;
                    elevatorMaxY = this.Y;
                }
                else if (this.MovementType == MovementType.Horizontal)
                {
                    elevatorMinX = this.MinPosition;
                    elevatorMaxX = this.MaxPosition;
                    elevatorMinY = this.Y;
                    elevatorMaxY = this.Y;
                }
                else if (this.MovementType == MovementType.Vertical)
                {
                    elevatorMinX = this.X;
                    elevatorMaxX = this.X;
                    elevatorMinY = this.MinPosition;
                    elevatorMaxY = this.MaxPosition;
                }

                if (elevatorMinX == -1 || elevatorMaxX == -1 || elevatorMinY == -1 || elevatorMaxY == -1)
                {
                    throw new Exception("This should never happen");
                }

                return new Rectangle(elevatorMinX, elevatorMinY, elevatorMaxX - elevatorMinX, elevatorMaxY - elevatorMinY);
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
