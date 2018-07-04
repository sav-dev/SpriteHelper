namespace SpriteHelper
{
    public class Constants
    {
        // Number of sprites for the player.
        public const int PlayerSprites = 9;

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
    }
}
