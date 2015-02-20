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

namespace RPG.Helpers.CustomShapes
{
	public class Circle
	{
		private GraphicsDeviceManager _graphics;
		private VertexPositionColor[] _vertices;
		private BasicEffect _effect;
		private Vector2 _position;
		private float _radius;
		private Color _color;


		public Circle(Vector2 position, int radius,
			Color color, GraphicsDeviceManager graphics)
		{
			this._position = position;
			this._radius = radius;
			this._color = color;
			this._graphics = graphics;

		    this.Initialize();
		}
		public Circle(Vector2 position, int radius,
			GraphicsDeviceManager graphics)
			: this(position, radius, Color.White, graphics) 
		{
		}

		#region Properties
		 
		public Vector2 Position
		{
			get { return this._position; }
			set 
			{
				this._position = value;
			    this.InitializeVertices(); 
			}
		}
	   
		public float Radius
		{
			get { return this._radius; }
			set 
			{
				this._radius = (value < 1) ? 1 : value;
			    this.InitializeVertices();
			}
		}

		public Color Color
		{
			get { return this._color; }
			set 
			{
				this._color = value;
			    this.InitializeVertices(); 
			}
		}

		public int Points
		{
			get { return this.CalculatePointCount(); }
		}

		#endregion

		public bool IsPointInCirlce(Vector2 point)
		{
			//(x - center_x)^2 + (y - center_y)^2 < radius^2
			double lhs = Math.Pow(this.Position.X - point.X, 2) + Math.Pow(this.Position.Y - point.Y, 2);
			double rhs = Math.Pow(this.Radius, 2);
			return lhs < rhs;
		}

		public void Draw()
		{
			this._effect.CurrentTechnique.Passes[0].Apply();
			this._graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip,
				this._vertices, 0, this._vertices.Length - 1);
		}

		#region Initializations
		
		private void Initialize()
		{
		    this.InitializeBasicEffect();
		    this.InitializeVertices();
		}

		private void InitializeBasicEffect()
		{
		   this._effect = new BasicEffect(this._graphics.GraphicsDevice);
		   this._effect.VertexColorEnabled = true;
		   this._effect.Projection = Matrix.CreateOrthographicOffCenter(0, this._graphics.GraphicsDevice.Viewport.Width,
				this._graphics.GraphicsDevice.Viewport.Height, 0, 0, 1);
		}

		private void InitializeVertices()
		{
			this._vertices = new VertexPositionColor[this.CalculatePointCount()];
			var pointTheta = ((float)Math.PI * 2) / (this._vertices.Length - 1);
			for (int i = 0; i < this._vertices.Length; i++)
			{
				var theta = pointTheta * i;
				var x = this.Position.X + ((float)Math.Sin(theta) *this.Radius);
				var y = this.Position.Y + ((float)Math.Cos(theta) *this.Radius);
				this._vertices[i].Position = new Vector3(x, y, 0);
				this._vertices[i].Color = this.Color;
			}

			this._vertices[this._vertices.Length - 1] = this._vertices[0];
		}

		#endregion

		private int CalculatePointCount()
		{
			return (int)Math.Ceiling(this.Radius * Math.PI);
		}
	}
}
