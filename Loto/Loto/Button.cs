using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Loto
{
    public class Button
    {
        private readonly Game1 _game;
        public Vector2 Position;
        private int _width;
        private int _height;
        private float _opacity;
        private readonly string _text;
        private bool _verif;
        private readonly string _textvar;

        private Texture2D _currentTexture;
        private Texture2D _normal;
        private Texture2D _mouseOver;
        private Texture2D _click;


        public Action A;

        public void LoadContent(ContentManager content)
        {
            _normal = content.Load<Texture2D>("IMGS/Blank");
            _mouseOver = _normal;
            _click = content.Load<Texture2D>("IMGS/Checked");
            _currentTexture = _normal;
        }

        public void Trigger()
        {
            A();
        }

        public Button(Game1 game, Vector2 pos, int width, int height, string text, Action a)
        {
            _game = game;
            _opacity = 0.7f;
            _width = width;
            _height = height;
            Position = pos;
            Position.X -= (float)_width / 2;
            Position.Y -= (float)_height / 2;
            _text = text;
            _textvar = null;
            A = a;
        }

        public Button(Game1 game, Vector2 pos, int width, int height, string text, ref string textvar, Action a)
        {
            _game = game;
            _opacity = 0.7f;
            Position = pos;
            Position.X -= (float)width / 2;
            Position.Y -= (float)height / 2;
            _width = width;
            _height = height;
            _text = text;
            _textvar = textvar;
            A = a;
        }

        public void Update(GameTime gt)
        {
            if (Mouse.GetState().Position.X >= Position.X && Mouse.GetState().Position.X <= Position.X + _width &&
                Mouse.GetState().Position.Y >= Position.Y
                && Mouse.GetState().Position.Y <= Position.Y + _height &&
                Mouse.GetState().LeftButton == ButtonState.Released && _verif)
            {
                _verif = false;
                Trigger();
            }
            else if (Mouse.GetState().Position.X >= Position.X && Mouse.GetState().Position.X <= Position.X + _width && Mouse.GetState().Position.Y >= Position.Y
                && Mouse.GetState().Position.Y <= Position.Y + _height && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                _currentTexture = _mouseOver;
                _opacity = 0.85f;
            }
            else if (Mouse.GetState().Position.X >= Position.X && Mouse.GetState().Position.X <= Position.X + _width &&
                     Mouse.GetState().Position.Y >= Position.Y
                     && Mouse.GetState().Position.Y <= Position.Y + _height &&
                     Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _currentTexture = _click;
                _opacity = 1f;
                _verif = true;
            }
            else
            {

                _currentTexture = _normal;
                _opacity = 0.7f;
                _verif = false;
            }
        }

        public void Update2()
        {
            if (_text == "Recommencer")
            {
                if (_game.GraphicsDevice.Viewport.Height < 900)
                {
                    Position = new Vector2(_game.WindowWidth - 125, _game.WindowHeight - 50);
                    _width = 200;
                    _height = 50;
                    Position.X -= _width / 2f;
                    Position.Y -= _height / 2f;
                }
                else
                {
                    Position = new Vector2(_game.WindowWidth - 200, _game.WindowHeight - 100);
                    _width = 300;
                    _height = 100;
                    Position.X -= _width / 2f;
                    Position.Y -= _height / 2f;
                }

            }
            else
            {
                int w = _game.WindowHeight - 30;
                while (w % 9 != 0) w++;
                int h = w / 9;
                _width = 3 * h / 2;
                _height = _width;
                Position.X = (_game.WindowWidth / 2 - 5 * h - 5 - _width) / 2f;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_currentTexture, new Rectangle((int)Position.X, (int)Position.Y, _width, _height), Color.White * _opacity);
            if (_textvar != null)
            {
                sb.DrawString(_game.Sf, _text + _textvar,
                    new Vector2(Position.X + (float)_width / 2 - _game.Sf.MeasureString(_text).X / 2, (float)_height / 2 + Position.Y - _game.Sf.MeasureString(_text).Y / 2),
                    Color.White * _opacity);
            }
            else
            {
                sb.DrawString(_game.Sf, _text, new Vector2(Position.X + _width / 2f - _game.Sf.MeasureString(_text).X / 2, _height / 2f + Position.Y - _game.Sf.MeasureString(_text).Y / 2), Color.Black * _opacity);
            }

        }

        public void Draw(SpriteBatch sb, float opacity)
        {
            sb.Draw(_currentTexture, new Rectangle((int)Position.X, (int)Position.Y, _width, _height), Color.Green * opacity);
            if (_textvar != null)
            {
                sb.DrawString(_game.Sf, _text + _textvar,
                    new Vector2(Position.X + (float)_width / 2 - _game.Sf.MeasureString(_text).X / 2, (float)_height / 2 + Position.Y - _game.Sf.MeasureString(_text).Y / 2),
                    Color.White * opacity);
            }
            else
            {
                sb.DrawString(_game.Sf, _text, new Vector2(Position.X + _width / 2f - _game.Sf.MeasureString(_text).X / 2, _height / 2f + Position.Y - _game.Sf.MeasureString(_text).Y / 2), Color.Black * opacity);
            }

        }
    }
}
