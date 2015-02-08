using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace RPG.Helpers
{
    public class Circle
    {
        private GraphicsDeviceManager _graphics;
        private VertexPositionColor[] _vertices;
        private BasicEffect _effect;
        private float _x;
        private float _y;
        private float _radius;
        private Color _color;
        

        public Circle(float x, float y, int radius,
            Color color, GraphicsDeviceManager graphics)
        {
            this._x = x;
            this._y = y;
            this._radius = radius;
            this._color = color;
            this._graphics = graphics;

            Initialize();
        }
        public Circle(float x, float y, int radius,
            GraphicsDeviceManager graphics)
            : this(x, y, radius, Color.White, graphics) 
        {
        }

        public float X
        {
            get { return this._x; }
            set 
            {
                this._x = value; 
                InitializeVertices(); 
            }
        }
        public float Y
        {
            get { return this._y; }
            set 
            {
                this._y = value;
                InitializeVertices();
            }
        }
        public float Radius
        {
            get { return this._radius; }
            set 
            {
                this._radius = (value < 1) ? 1 : value; 
                InitializeVertices();
            }
        }
        public Color Color
        {
            get 
            { 
                return this._color; 
            }
            set 
            {
                this._color = value; 
                InitializeVertices(); 
            }
        }
        public int Points
        {
            get { return this.CalculatePointCount(); }
        }

        public void Draw()
        {
            this._effect.CurrentTechnique.Passes[0].Apply();
            this._graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip,
                this._vertices, 0, this._vertices.Length - 1);
        }

        private void Initialize()
        {
            InitializeBasicEffect();
            InitializeVertices();
        }

        private void InitializeBasicEffect()
        {
           this._effect = new BasicEffect(_graphics.GraphicsDevice);
           this._effect.VertexColorEnabled = true;
           this._effect.Projection = Matrix.CreateOrthographicOffCenter(0, this._graphics.GraphicsDevice.Viewport.Width,
                this._graphics.GraphicsDevice.Viewport.Height, 0, 0, 1);
        }

        private void InitializeVertices()
        {
            this._vertices = new VertexPositionColor[CalculatePointCount()];
            var pointTheta = ((float)Math.PI * 2) / (this._vertices.Length - 1);
            for (int i = 0; i < this._vertices.Length; i++)
            {
                var theta = pointTheta * i;
                var x = X + ((float)Math.Sin(theta) * Radius);
                var y = Y + ((float)Math.Cos(theta) * Radius);
                this._vertices[i].Position = new Vector3(x, y, 0);
                this._vertices[i].Color = Color;
            }

            this._vertices[this._vertices.Length - 1] = this._vertices[0];
        }

        private int CalculatePointCount()
        {
            return (int)Math.Ceiling(Radius * Math.PI);
        }
    }
}
