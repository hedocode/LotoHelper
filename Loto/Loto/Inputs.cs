using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Loto
{
    class Inputs
    {
        private readonly Game1 _game;
        private KeyboardState _stdin;
        private MouseState _ms;
        private bool _fIsDown;
        private bool _rIsDown;

        private int _w;
        private int _h;

        public Inputs(Game1 game)
        {
            _game = game;
        }

        public void Update(GameTime gt)
        {
            _stdin = Keyboard.GetState();
            _ms = Mouse.GetState();

            if (_stdin.IsKeyUp(Keys.F))
                _fIsDown = false;

            if (_stdin.IsKeyDown(Keys.F))
            {
                if (!_fIsDown)
                {
                    if (_game.Graphics.IsFullScreen)
                    {
                        _game.Graphics.ToggleFullScreen();
                        _game.Graphics.ApplyChanges();
                        _game.Graphics.PreferredBackBufferWidth = _w;
                        _game.Graphics.PreferredBackBufferHeight = _h;
                        _game.Graphics.ApplyChanges();
                    }
                    else
                    {
                        _w = _game.WindowWidth;
                        _h = _game.WindowHeight;
                        _game.Graphics.PreferredBackBufferWidth = _game.ScreenWidth;
                        _game.Graphics.PreferredBackBufferHeight = _game.ScreenHeight;
                        _game.Graphics.ApplyChanges();
                        _game.Graphics.ToggleFullScreen();
                        _game.Graphics.ApplyChanges();
                    }

                }
                _fIsDown = true;
            }

            if (_stdin.IsKeyUp(Keys.R))
                _rIsDown = false;

            if (_stdin.IsKeyDown(Keys.R))
                if (!_rIsDown)
                {
                    _game.Recommencer = !_game.Recommencer;
                    _rIsDown = true;
                }
                    
        }
    }
}
