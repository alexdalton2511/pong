using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.Base;

namespace Pong
{
    class PongGame : Core
    {
        public PongGame() : base("Pong", 3440, 1440, true)
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}