using Microsoft.Xna.Framework.Graphics;

namespace RPG.GameLogic.Interface
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
        void LoadContent();
        void Update();
    }
}