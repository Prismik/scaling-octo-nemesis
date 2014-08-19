using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScalingOctoNemesis.Util
{
    public static class Resolution
    {
        static int _width;
        static int _height;
        static GraphicsDevice _device;
        static Matrix _globalTransformation;
        public static void Initialize(GraphicsDevice device)
        {
            _device = device;
        }

        public static void ChangeResolution(int width, int height)
        {
            _width = width;
            _height = height;
            
            float horScaling = (float)_device.PresentationParameters.BackBufferWidth / _width;
            float verScaling = (float)_device.PresentationParameters.BackBufferHeight / _height;
            Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            _globalTransformation = Matrix.CreateScale(screenScalingFactor);
        }

        public static Matrix TransformMatrix { get { return _globalTransformation; } private set { _globalTransformation = value; } }
    }
}
