namespace RPG.Graphics.CustomShapes
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Circle
	{
		private Vector2 position;
		private float radius;

		public Circle(Vector2 position, int radius)
		{
			this.position = position;
			this.radius = radius;
		}

		#region Properties
		 
		public Vector2 Position
		{
			get { return this.position; }
			set 
			{
				this.position = value;
			}
		}
	   
		public float Radius
		{
			get { return this.radius; }
			set 
			{
				this.radius = (value < 1) ? 1 : value;
			}
		}

        #endregion

        public bool IsPointInCirlce(Vector2 point)
        {
            //(x - center_x)^2 + (y - center_y)^2 < radius^2
            double lhs = Math.Pow(this.Position.X - point.X, 2) + Math.Pow(this.Position.Y - point.Y, 2);
            double rhs = Math.Pow(this.Radius, 2);
            return lhs < rhs;
        }
    }
}
