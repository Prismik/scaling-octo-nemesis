using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TTUI.Util
{
    public static class Resolution
    {
        static int _width = 800;
        static int _height = 600;
        static int _vWidth = 1024;
        static int _vHeight = 768;
        static bool _dirtyMatrix;
        static GraphicsDeviceManager _device;
        static Matrix _globalTransformation;
        public static void Initialize(ref GraphicsDeviceManager device)
        {
            _device = device;
            _width = device.PreferredBackBufferWidth;
            _height = device.PreferredBackBufferHeight;
            _dirtyMatrix = true;
            ApplyResolutionSettings();
        }

        public static void ChangeVirtualResolution(int width, int height)
        {
            _vWidth = width;
            _vHeight = height;

            _dirtyMatrix = true;
        }
            
        public static void ChangeResolution(int width, int height)
        {
            _width = width;
            _height = height;

            ApplyResolutionSettings();
        }

        private static void ApplyResolutionSettings()
        {
            // If we are using full screen mode, we should check to make sure that the display
            // adapter can handle the video mode we are trying to set.  To do this, we will
            // iterate through the display modes supported by the adapter and check them against
            // the mode we want to set.
            foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                // Check the width and height of each mode against the passed values
                //    if ((dm.Width == _width) && (dm.Height == _height))
                if ((_width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (_height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    // The mode is supported, so set the buffer formats, apply changes and return
                    _device.PreferredBackBufferWidth = _width;
                    _device.PreferredBackBufferHeight = _height;
                    //_device.IsFullScreen = true; // maybe change after
                    
                }
            }
            _device.ApplyChanges();
            _dirtyMatrix = true;

            _width =   _device.PreferredBackBufferWidth;
            _height = _device.PreferredBackBufferHeight;
        }

        private static void InitViewport()
        {
            Viewport vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = _width;
            vp.Height = _height;
            _device.GraphicsDevice.Viewport = vp;
        }

        private static void ResetViewport()
        {
            float targetAspectRatio = (float)_vWidth / (float)_vHeight;
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            int width = _device.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;

            if (height > _device.PreferredBackBufferHeight)
            {
                height = _device.PreferredBackBufferHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }

            // set up the new viewport centered in the backbuffer
            Viewport viewport = new Viewport();

            viewport.X = (_device.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (_device.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            
            if (changed)
                _dirtyMatrix = true;

            _device.GraphicsDevice.Viewport = viewport;
        }

        /// <summary>
        /// Sets the device to use the draw pump
        /// Sets correct aspect ratio
        /// </summary>
        static public void BeginDraw()
        {
            // Start by reseting viewport to (0,0,1,1)
            InitViewport();
            // Clear to Black
            _device.GraphicsDevice.Clear(Color.Black);
            // Calculate Proper Viewport according to Aspect Ratio
            ResetViewport();
            // and clear that
            // This way we are gonna have black bars if aspect ratio requires it and
            // the clear color on the rest
            _device.GraphicsDevice.Clear(FlatColors.CLOUDS);
        }


        public static Point PointToResolution(Vector2 point)
        {
            Matrix inverseViewMatrix = Matrix.Invert(_globalTransformation);
            Vector2 virtualPoint = Vector2.Transform(point, inverseViewMatrix);

            return new Point((int)virtualPoint.X, (int)virtualPoint.Y);
        }

        public static object SupportedResolutions
        {
            get
            {
                List<Vector2> resolutions = new List<Vector2>();
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                    resolutions.Add(new Vector2(dm.Width, dm.Height));
                
                return resolutions;
            }
        }

        public static Vector2 CurrentResolution
        {
            get
            {
                return new Vector2(_width, _height);
            }
        }

        public static Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(_device.GraphicsDevice.Viewport.Bounds.Width / 2, _device.GraphicsDevice.Viewport.Bounds.Height / 2);
            }
        }

        public static Vector2 VirtualViewport
        {
            get
            {
                return new Vector2(_vWidth, _vHeight);
            }
        }

        private static void RecreateMatrix()
        {
            _dirtyMatrix = false;
            _globalTransformation = Matrix.CreateScale(
                           (float)_device.GraphicsDevice.Viewport.Width / _vWidth,
                           (float)_device.GraphicsDevice.Viewport.Width / _vWidth,
                           1f);
        }

        public static Matrix TransformMatrix 
        { 
            get 
            {
                if (_dirtyMatrix)
                    RecreateMatrix();

                return _globalTransformation; 
            } 
            
            set 
            { 
                _globalTransformation = value; 
            } 
        }

    }
}
