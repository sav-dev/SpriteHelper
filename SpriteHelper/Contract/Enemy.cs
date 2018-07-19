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

        // Whether initially the enemy is flipped.
        [DataMember]
        public bool InitialFlip { get; set; }

        // Shooting frequency.
        [DataMember]
        public int ShootingFrequency { get; set; }

        // Shooting frequency offset.
        [DataMember]
        public int ShootingInitialFrequency { get; set; }

        // Screen the enemy is on.
        public int Screen => this.X / (Constants.ScreenWidthInTiles * Constants.BackgroundTileWidth);

        // Movement range.
        public int MovementRange => this.MaxPosition - this.MinPosition;

        // Movement orientation.
        // A bit of a hack to store the movement and shooting direction in one byte.
        public int MovementOrientation
        {
            get
            {
                // all values are hardcoded in the game
                //   0 = 00000000 - none
                //   1 = 00000001 - static horizontal
                //   2 = 00000010 - static vertical
                //   5 = 00000101 - moving horizontal
                //   6 = 00000110 - moving vertical
                switch (this.MovementType)
                {
                    case MovementType.None:
                        switch (this.animation.Flip)
                        {
                            case Flip.None:
                                return 0;
                            case Flip.Horizontal:
                                return 1;
                            case Flip.Vertical:
                                return 2;
                        }
                        break;
                    case MovementType.Horizontal:
                        return 5;
                    case MovementType.Vertical:
                        return 6;
                }
                throw new System.Exception("We should never get here");
            }
        }

        // Animation.
        public Animation Animation => this.animation;
        private Animation animation;

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

        // Width getter.
        public int Width => this.width;

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
            this.animation = animation;
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
