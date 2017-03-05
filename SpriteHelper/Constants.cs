namespace SpriteHelper
{
    public class Constants
    {
        public const int SpriteWidth = 8;           // never change
        public const int SpriteHeight = 8;          // never change

        public const int BackgroundTileWidth = 16;  // never change
        public const int BackgroundTileHeight = 16; // never change

        public const int ChrFileSpritesPerRow = 16; // never change
        public const int ChrFileRows = 16;          // never change

        public const int MaxZoom = 10;

        public const int PickerWidthInTiles = 6;
        public const int PickerWidth = PickerWidthInTiles * BackgroundTileWidth;

        public const int MaxUniqueTiles = 256;
        public const int CompressedMaxLength = 256 / MaxUniqueTiles; // = 8
    }
}
