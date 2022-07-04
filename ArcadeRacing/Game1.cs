using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ArcadeRacing.Classes;

namespace ArcadeRacing
{
    public class Game1 : Game
    {
                
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1920;//800;
            _graphics.PreferredBackBufferHeight = 1080;//480;
            GlobalRenderSettings.windowHeight = _graphics.PreferredBackBufferHeight;
            GlobalRenderSettings.windowWidth = _graphics.PreferredBackBufferWidth;
            ProgramManager.Init();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ProgramManager.LoadContent(GraphicsDevice, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ProgramManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ProgramManager.Render(GraphicsDevice, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
