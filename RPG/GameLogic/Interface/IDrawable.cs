using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.GameLogic.Interface
{
    public interface IDrawable
    {
        void Initialize(Texture2D texture, Vector2 position);
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}