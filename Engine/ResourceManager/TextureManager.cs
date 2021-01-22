using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Engine
{
    /// <summary>
    /// Static class that stores all loaded textures by their filepath
    /// </summary>
    public static class TextureManager
    {
        #region Data members

        /// <summary>
        /// Hash map of all textures
        /// </summary>
        private static Hashtable mTextureMap = new Hashtable();

        #endregion

        #region Methods

        /// <summary>
        /// Loads a texture from a file
        /// </summary>
        /// <param name="filepath">The texture filepath</param>
        /// <returns></returns>
        public static Texture UploadTexture(string filepath)
        {
            if (mTextureMap.Contains(filepath))
                return (Texture)mTextureMap[filepath];
            else
            {
                Texture newTexture = new Texture(filepath);
                mTextureMap[filepath] = newTexture;

                return newTexture;
            }
        }

        /// <summary>
        /// Loads a texture from file with specified tile mode and viewport
        /// </summary>
        /// <param name="filepath">Texture filepath</param>
        /// <param name="tileMode">Texture tile mode</param>
        /// <param name="viewport">Texture viewport</param>
        /// <returns></returns>
        public static Texture UploadTexture(string filepath, TileMode tileMode, Rect viewport)
        {
            if (mTextureMap.Contains(filepath))
                return (Texture)mTextureMap[filepath];
            else
            {
                Texture newTexture = new Texture(filepath, tileMode, viewport);
                mTextureMap[filepath] = newTexture;

                return newTexture;
            }
        }

        /// <summary>
        /// Returns the texture object represented by the specified filepath if it exists in the hash map
        /// </summary>
        /// <param name="name">The texture filepath</param>
        /// <returns></returns>
        public static Texture GetTexture(string filepath)
        {
            return mTextureMap.Contains(filepath) ? (Texture)mTextureMap[filepath] : null;
        }

        #endregion
    }
}
