namespace SpriteHelper
{
    public class Constants
    {
        // Number of sprites for the player.
        public const int PlayerSprites = 9;

        // Max number of bullets on screen.
        public const int MaxBullets = 5 + 10;

        // Elevators, hardcoded in game code.
        public const int ElevatorSprite = 249;
        public const int ElevatorEndRSprite = 250;
        public const int ElevatorPalette = 2;
        public const int ElevatorHeight = 6;
        public const int ElevatorWidthPerBlock = SpriteWidth;
        public const int ElevatorsLimitPerTwoScreens = 6;
        public const int MinElevatorSize = 2;
        public const int MaxElevatorSize = 8;

        // Player constants, hardcoded in the game code.
        public const int PlayerXOffset = 8;
        public const int PlayerYOffset = 31;
        public const int PlayerPlatformBoxHeight = -31;
        public const int PlayerPlatformBoxWidth = 15;
        public const int PlayerGunOXffL = -3;
        public const int PlayerGunOXffR = 18;
        public const int PlayerGunYOff = -20;
        public const int PlayerGunYOffCrouch = -12;
        public const int PlayerThreatBoxXOff = 1;
        public const int PlayerThreatBoxYOff = -2;
        public const int PlayerThreatBoxHeight = -25;
        public const int PlayerThreatBoxHeightCrouch = -17;
        public const int PlayerThreatBoxWidth = 13;
        public const int PlayerSpeed = 2;

        // Bullet constants.
        public const int BulletHOff = 2;
        public const int BulletVOff = 2;

        // Number of sprites for an explosion.
        public const int ExplosionSprites = 4;
        public const int ExplosionWidth = 2 * SpriteWidth;
        public const int ExplosionHeight = 2 * SpriteHeight;

        // Offsets for threat box.
        public const int ThreatXOff = 2;
        public const int ThreatYOff = 2;

        // NES constants, never change these
        public const int SpriteWidth = 8;
        public const int SpriteHeight = 8;    
        public const int BackgroundTileWidth = 16;
        public const int BackgroundTileHeight = 16;
        public const int ScreenWidthInTiles = 16;
        public const int ScreenHeightInTiles = 15;
        public const int ScreenWidthInAtts = 8;
        public const int ScreenHeightInAtts = 8;
        public const int ChrFileSpritesPerRow = 16;
        public const int ChrFileRows = 16;
        public const int ScreenWidth = Constants.ScreenWidthInTiles * Constants.BackgroundTileWidth;
        public const int ScreenHeight = Constants.ScreenHeightInTiles * Constants.BackgroundTileHeight;
        public const int Framerate = 60;
        public const int SpritesPerFrame = 64;

        // Max zoom for sprite viweres.
        public const int MaxZoom = 10;

        // Editor zoom
        public const int LevelEditorZoom = 2;

        // Picker width for background tiles.
        public const int PickerWidthInTiles = 6;
        public const int PickerWidth = PickerWidthInTiles * BackgroundTileWidth;

        // Max unique tiles limit.
        public const int MaxUniqueTiles = 256;
        public const int CompressedMaxLength = 256 / MaxUniqueTiles; // = 8

        // Max enemies per consecutive screens.
        public const int EnemiesLimitPerTwoScreens = 10;
        public const int EnemiesLimitPerLevel = 80;

        // Memory sizes.
        public const int EnemyDefinitionSize = 16;
        public const int EnemyInMemorySize = 20;
        public const int EnemyInLevelDataSize = 18;
        public const int ElevatorInMemorySize = 8;
        public const int ElevatorInLevelDataSize = 9;

        // Transparency.
        public const int TransparentAlpha = 50;

        // Exit width and height (in game pixels).
        public const int ExitXOff = 4;
        public const int ExitYOff = 13;
        public const int ExitWidth = 24;
        public const int ExitHeight = 35;
    }
}
