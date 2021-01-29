using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Engine
{
    /// <summary>
    /// Class representing a texture file. Its main use is to paint game objects.
    /// </summary>
    public class Texture
    {
        #region Data members

        /// <summary>
        /// Stores the color information for painting geometry
        /// </summary>
        private ImageBrush mBrush;

        /// <summary>
        /// Image source data
        /// </summary>
        private BitmapImage mImageSource;

        /// <summary>
        /// The width of the texture (in pixels)
        /// </summary>
        private int mWidth;

        /// <summary>
        /// The height of the texture (in pixels)
        /// </summary>
        private int mHeight;

        /// <summary>
        /// The name of the texture file
        /// </summary>
        private string mName;

        /// <summary>
        /// The full filepath of the texture
        /// </summary>
        private string mFilepath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and private sets the paint brush information
        /// </summary>
        public ImageBrush Brush
        {
            get { return mBrush; }
            private set { mBrush = value != null ? value : new ImageBrush(); }
        }

        /// <summary>
        /// Gets and private sets the image source data
        /// </summary>
        public BitmapImage ImageSource
        {
            get { return mImageSource; }
            private set { mImageSource = value != null ? value : new BitmapImage(); }
        }

        /// <summary>
        /// Gets and private sets the texture width
        /// </summary>
        public int Width
        {
            get { return mWidth; }
            private set { mWidth = value >= 0 ? value : 0; }
        }

        /// <summary>
        /// Gets and private sets the texture height
        /// </summary>
        public int Height
        {
            get { return mHeight; }
            private set { mHeight = value >= 0 ? value : 0; }
        }

        /// <summary>
        /// Gets and private sets the texure file name
        /// </summary>
        public string Name
        {
            get { return mName; }
            private set { mName = value != null ? value : ""; }
        }

        /// <summary>
        /// Gets and private sets the texture full filepath
        /// </summary>
        public string Filepath
        {
            get { return mFilepath; }
            private set { mFilepath = value != null ? value : ""; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// General use constructor that creates a texture from a given filepath
        /// </summary>
        /// <param name="filepath">The filepath of the texture file</param>
        public Texture(string filepath)
        {
            ImageSource = new BitmapImage(new Uri(filepath, UriKind.RelativeOrAbsolute)); 
            Brush = new ImageBrush(ImageSource);
            Brush.TileMode = TileMode.Tile;
            Brush.Viewport = new Rect(0.0, 0.0, 1.0, 1.0);

            Width = ImageSource.PixelWidth;
            Height = ImageSource.PixelHeight;
            Name = filepath.Substring(filepath.LastIndexOf('\\') + 1);
            Filepath = filepath;   
        }

        /// <summary>
        /// Creates a texture from a filepath with specified tile mode and viewport
        /// </summary>
        /// <param name="filepath">Texture filepath</param>
        /// <param name="tileMode">Tile mode of the texture</param>
        /// <param name="viewPort">Viewport of the texture</param>
        public Texture(string filepath, TileMode tileMode, Rect viewport)
        {
            ImageSource = new BitmapImage(new Uri(filepath, UriKind.RelativeOrAbsolute));
            Brush = new ImageBrush(ImageSource);
            Brush.TileMode = tileMode;
            Brush.Viewport = viewport;

            Width = ImageSource.PixelWidth;
            Height = ImageSource.PixelHeight;
            Name = filepath.Substring(filepath.LastIndexOf('/') + 1);
            Filepath = filepath;
        }

        #endregion
    }
}
