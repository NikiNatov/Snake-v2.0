namespace Engine
{
    /// <summary>
    /// Represents the map of the game world
    /// </summary>
    public class Map
    {
        #region Data members

        /// <summary>
        /// Map pixel data
        /// </summary>
        private byte[] mMapData;

        /// <summary>
        /// Stride of one row 
        /// </summary>
        private int mStride;

        /// <summary>
        /// Width of the map
        /// </summary>
        private int mWidth;

        /// <summary>
        /// Height of the map
        /// </summary>
        private int mHeight;

        /// <summary>
        /// The size in bits of the pixel
        /// </summary>
        private int mPixelBitSize;

        #endregion


        #region Properties

        public byte[] MapData
        {
            get { return mMapData; }
            set
            {
                if (mMapData == value)
                    return;

                if (value != null)
                    mMapData = value;
                else
                    Logger.LogError("Map is null!", "Failed loading the map");
            }
        }

        /// <summary>
        /// Gets and sets the stride of one row in the map pixel data array. The stride is the number of (pixels) * (number of color channels)
        /// </summary>
        public int Stride
        {
            get { return mStride; }
            set
            {
                if (mStride == value)
                    return;

                if (value > 0)
                    mStride = value;
                else
                    Logger.LogError("Invalid map stride!");
            }
        }

        /// <summary>
        /// Gets and sets the width of the map
        /// </summary>
        public int Width
        {
            get { return mWidth; }
            set
            {
                if (mWidth == value)
                    return;

                if (value > 0)
                    mWidth = value;
                else
                    Logger.LogError("Invalid map width!");
            }
        }

        /// <summary>
        /// Gets and sets the height of the map
        /// </summary>
        public int Height
        {
            get { return mHeight; }
            set
            {
                if (mHeight == value)
                    return;

                if (value > 0)
                    mHeight = value;
                else
                    Logger.LogError("Invalid map height!");
            }
        }

        /// <summary>
        /// Gets and sets the pixel size in bits
        /// </summary>
        public int PixelBitSize
        {
            get { return mPixelBitSize; }
            set
            {
                if (mPixelBitSize == value)
                    return;

                if (value == 8 || value == 16 || value == 24 || value == 32)
                    mPixelBitSize = value;
                else
                    Logger.LogError("Invalid pixel size!");
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a map from 15x15 image file
        /// </summary>
        /// <param name="filepath">Map filepath</param>
        public Map(string filepath, int width, int height)
        {
            var mapTexture = TextureManager.GetTexture(filepath);

            PixelBitSize = mapTexture.ImageSource.Format.BitsPerPixel;
            int colorChannels = PixelBitSize / 8;

            Stride = mapTexture.ImageSource.PixelWidth * colorChannels;
            MapData = new byte[mapTexture.ImageSource.PixelHeight * Stride];

            mapTexture.ImageSource.CopyPixels(MapData, Stride, 0);

            Width = width;
            Height = height;
        }

        #endregion
    }
}
