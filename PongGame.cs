using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Base;
using Pong.Base.Graphics;

namespace Pong
{
    class PongGame : Core
    {
        private static readonly string PongTextureAtlasPath = "images/pong_atlas";

        private static readonly string PaddleRegionName = PongEntity.LeftPaddle.ToRegionName();

        private static readonly string BallRegionName = PongEntity.Ball.ToRegionName();

        private static readonly Rectangle PaddleSourceRectangle = PongEntity.LeftPaddle.ToSourceRectangle();

        private static readonly Rectangle BallSourceRectangle = PongEntity.Ball.ToSourceRectangle();

        private Vector2 LeftPaddlePosition = new(50, 700);

        private Vector2 RightPaddlePosition = new(3260, 700);

        private Vector2 BallPosition = new(1704, 704);

        private TextureRegion LeftPaddle;

        private TextureRegion RightPaddle;

        private TextureRegion Ball;

        private Texture2D PongAtlasTexture;

        public PongGame() : base("Pong", 3440, 1440, true)
        {
        }

        protected override void LoadContent()
        {
            PongAtlasTexture = Content.Load<Texture2D>(PongTextureAtlasPath);

            var atlas = new TextureAtlas(PongAtlasTexture);

            atlas.AddRegion(PaddleRegionName,
                PaddleSourceRectangle.X,
                PaddleSourceRectangle.Y,
                PaddleSourceRectangle.Width,
                PaddleSourceRectangle.Height
            );

            atlas.AddRegion(BallRegionName,
                BallSourceRectangle.X,
                BallSourceRectangle.Y,
                BallSourceRectangle.Width,
                BallSourceRectangle.Height
            );

            LeftPaddle = atlas.GetRegion(PaddleRegionName);
            RightPaddle = atlas.GetRegion(PaddleRegionName);
            Ball = atlas.GetRegion(BallRegionName);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            BallPosition += new Vector2(2.0f, 2.0f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Begin the sprite batch to prepare for rendering.
            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            LeftPaddle.Draw(SpriteBatch, LeftPaddlePosition, Color.White);
            RightPaddle.Draw(SpriteBatch, RightPaddlePosition, Color.White);

            Ball.Draw(
                SpriteBatch,
                BallPosition,
                Color.White,
                0.0f,
                new Vector2(Ball.Width / 2, Ball.Height / 2),
                1.0f,
                SpriteEffects.None,
                0.0f
            );

            // Always end the sprite batch when finished.
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

enum PongEntity
{
    LeftPaddle,
    RightPaddle,
    Ball
}

static class PongEntityExtension
{
    public static string ToRegionName(this PongEntity entity)
    {
        return entity switch
        {
            PongEntity.LeftPaddle => "paddle",
            PongEntity.RightPaddle => "paddle",
            PongEntity.Ball => "ball",
            _ => throw new NotSupportedException($"The PongEntity value '{entity}' is not supported.")
        };
    }

    public static Rectangle ToSourceRectangle(this PongEntity entity)
    {
        return entity switch
        {
            PongEntity.LeftPaddle => new Rectangle(0, 0, 32, 256),
            PongEntity.RightPaddle => new Rectangle(0, 0, 32, 256),
            PongEntity.Ball => new Rectangle(0, 128, 32, 32),
            _ => throw new NotSupportedException($"The PongEntity value '{entity}' is not supported.")
        };
    }
}