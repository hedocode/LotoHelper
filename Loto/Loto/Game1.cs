using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Loto
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;

        public SpriteFont Sf;
        public SpriteFont Sf20;
        public SpriteFont Sf30;

        public int ScreenWidth;
        public int ScreenHeight;
        public int WindowWidth;
        public int WindowHeight;

        private Loto _loto;
        private Button _b;
        private Inputs _inputs;

        public bool Recommencer;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.Title = "LOTO";
        }

        protected override void Initialize()
        {
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //ScreenWidth = GraphicsDevice.DisplayMode.Width;
            //ScreenHeight = GraphicsDevice.DisplayMode.Height;
            Graphics.PreferredBackBufferWidth = (int)(ScreenWidth/1.5);
            Graphics.PreferredBackBufferHeight = (int)(ScreenHeight/1.5);
            WindowWidth = Graphics.PreferredBackBufferWidth;
            WindowHeight = Graphics.PreferredBackBufferHeight;
            _loto = new Loto(this);
            _b = new Button(this,new Vector2(WindowWidth - 125, WindowHeight - 50), 200, 50, "Recommencer", Replay);
            _inputs = new Inputs(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Sf20 = Content.Load<SpriteFont>("font1");
            Sf30 = Content.Load<SpriteFont>("font2");
            Sf = Sf20;
            _loto.LoadContent(Content);
            _b.LoadContent(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            WindowWidth = GraphicsDevice.Viewport.Width;
            WindowHeight = GraphicsDevice.Viewport.Height;
            if (WindowHeight > 900)
                Sf = Sf30;
            else
                Sf = Sf20;

            _loto.Update(gameTime);
            if(Recommencer)
                _b.Update(gameTime);
            _inputs.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            
            _spriteBatch.Begin();
            _loto.Draw(_spriteBatch);
            if(Recommencer)
                _b.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Replay()
        {
            _loto = new Loto(this);
            _loto.LoadContent(Content);
        }
    }
}