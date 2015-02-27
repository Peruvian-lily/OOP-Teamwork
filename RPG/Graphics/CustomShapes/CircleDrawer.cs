namespace RPG.Graphics.CustomShapes
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class CircleDrawer
    {
        private GraphicsDeviceManager graphics;
        private VertexPositionColor[] vertices;
        private BasicEffect effect;
        private Color color;
        private Circle cirlce;

        public CircleDrawer(GraphicsDeviceManager graphics, Color color)
        {
            this.Color = color;
            this.graphics = graphics;
        }
        public CircleDrawer(GraphicsDeviceManager graphics) 
            : this(graphics, Color.Black)
        {
        }

        public Color Color
        {
            get
            {
                return this.color;
            }

            private set
            {
                this.color = value;
            }
        }

        public Circle Circle
        {
            get
            {
                return this.cirlce;
            }

            set
            {
                this.cirlce = value;
                this.InitializeCirlceGraphics();
            }
        }

        #region Initializations

        private void InitializeCirlceGraphics()
        {
            this.InitializeBasicEffect();
            this.InitializeVertices();
        }

        private void InitializeBasicEffect()
        {
            this.effect = new BasicEffect(this.graphics.GraphicsDevice);
            this.effect.VertexColorEnabled = true;
            this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, this.graphics.GraphicsDevice.Viewport.Width, this.graphics.GraphicsDevice.Viewport.Height, 0, 0, 1);
        }

        private void InitializeVertices()
        {
            this.vertices = new VertexPositionColor[this.CalculatePointCount()];
            var pointTheta = ((float)Math.PI * 2) / (this.vertices.Length - 1);
            for (int i = 0; i < this.vertices.Length; i++)
            {
                var theta = pointTheta * i;
                var x = this.Circle.Position.X + ((float)Math.Sin(theta) * this.Circle.Radius);
                var y = this.Circle.Position.Y + ((float)Math.Cos(theta) * this.Circle.Radius);
                this.vertices[i].Position = new Vector3(x, y, 0);
                this.vertices[i].Color = this.Color;
            }

            this.vertices[this.vertices.Length - 1] = this.vertices[0];
        }

        #endregion

        private int CalculatePointCount()
        {
            return (int)Math.Ceiling(this.Circle.Radius * Math.PI);
        }

        public void Draw(Circle cirlce)
        {
            this.Circle = cirlce;
            this.effect.CurrentTechnique.Passes[0].Apply();
            this.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, this.vertices, 0, this.vertices.Length - 1);
        }
    }
}
