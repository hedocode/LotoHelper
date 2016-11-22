using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Loto
{
    class Case
    {
        private readonly Loto _loto;
        public int Numero;
        public Vector2 Position;
        public int Height;
        public int Width;
        private float _opacity = 1f;
        public int X;
        public int Y;

        private Texture2D _currentTexture;
        private Texture2D _normal;
        private Texture2D _click;

        private bool _verif;
        private bool _isChecked;

        public Case(Loto l, int num, int x, int y)
        {
            _loto = l;
            Numero = num;
            X = x;
            Y = y;
            int w = _loto.Game.WindowHeight - 30;
            while (w % 9 != 0) w++;
            int h = w / 9;

            Position = new Vector2(x * h - 5 * h + _loto.Game.WindowWidth / 2f + x, 10 + y * h + y);
            Width = w / 9;
            Height = w / 9;
        }

        public void LoadContent(ContentManager cm)
        {
            _normal = cm.Load<Texture2D>("IMGS/Blank");
            _click = cm.Load<Texture2D>("IMGS/Checked");
            _currentTexture = _normal;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_currentTexture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.White*_opacity);
            sb.DrawString(_loto.Game.Sf,Numero+"",new Vector2(Position.X+Width/2f- _loto.Game.Sf.MeasureString(Numero+"").X/2,Position.Y+Height/2f- _loto.Game.Sf.MeasureString(Numero + "").Y / 2),Color.Black*_opacity);
        }

        public void Update(GameTime gt)
        {
            MouseState ms = Mouse.GetState();

            if (!_isChecked)
            {
                if (ms.Position.X >= Position.X && ms.Position.X <= Position.X + Width &&
                    ms.Position.Y >= Position.Y
                    && ms.Position.Y <= Position.Y + Height &&
                    ms.LeftButton == ButtonState.Released && _verif)
                {
                    _verif = false;
                    _currentTexture = _click;
                    _loto.DernieresCases.Add(this);
                    if (_loto.ButtonList.Count == 5)
                    {
                        _loto.ButtonList.RemoveRange(0, 5);
                        int aux = 0;
                        int w = _loto.Game.WindowHeight - 30;
                        while (w%9 != 0) w++;
                        int h = w/9;
                        for (int i = _loto.DernieresCases.Count - 5; i < _loto.DernieresCases.Count; i++)
                        {
                            _loto.ButtonList.Add(new Button(_loto.Game,
                                new Vector2((_loto.Game.WindowWidth/2 - 5*h - 5 - h)/2f, 100 + 3*Width/2*aux), 3*Width/2,
                                3*Width/2, _loto.DernieresCases[i].Numero + "", _loto.DernieresCases[i].Uncheck));
                            aux++;
                        }
                        foreach (Button b in _loto.ButtonList)
                        {
                            b.LoadContent(_loto.Game.Content);
                        }
                        _isChecked = true;
                    }
                    else
                    {
                        _loto.ButtonList.Add(new Button(_loto.Game,
                            new Vector2(100, 100 + 3*Width/2*_loto.ButtonList.Count), 3*Width/2, 3*Width/2, Numero + "",
                            Uncheck));
                        _loto.ButtonList[_loto.ButtonList.Count - 1].LoadContent(_loto.Game.Content);
                        _isChecked = true;
                    }
                }
                else if (ms.Position.X >= Position.X && Mouse.GetState().Position.X <= Position.X + Width &&
                         ms.Position.Y >= Position.Y
                         && ms.Position.Y <= Position.Y + Height &&
                         ms.LeftButton == ButtonState.Released)
                {
                    _opacity = 0.95f;
                }
                else if (ms.Position.X >= Position.X && Mouse.GetState().Position.X <= Position.X + Width &&
                         ms.Position.Y >= Position.Y
                         && ms.Position.Y <= Position.Y + Height &&
                         ms.LeftButton == ButtonState.Pressed)
                {
                    _currentTexture = _click;
                    _opacity = 1f;
                    _verif = true;
                }
                else
                {
                    _currentTexture = _normal;
                    _opacity = 0.90f;
                    _verif = false;
                }
            }
            else
            {
                if (_loto.Game.Recommencer)
                {
                    if (ms.Position.X >= Position.X && ms.Position.X <= Position.X + Width &&
                    ms.Position.Y >= Position.Y
                    && ms.Position.Y <= Position.Y + Height &&
                    ms.LeftButton == ButtonState.Released && _verif)
                    {
                        _verif = false;
                        _currentTexture = _normal;
                        Uncheck();
                    }
                    else if (ms.Position.X >= Position.X && Mouse.GetState().Position.X <= Position.X + Width &&
                             ms.Position.Y >= Position.Y
                             && ms.Position.Y <= Position.Y + Height &&
                             ms.LeftButton == ButtonState.Released)
                    {
                        _opacity = 0.85f;
                    }
                    else if (ms.Position.X >= Position.X && Mouse.GetState().Position.X <= Position.X + Width &&
                             ms.Position.Y >= Position.Y
                             && ms.Position.Y <= Position.Y + Height &&
                             ms.LeftButton == ButtonState.Pressed)
                    {
                        _currentTexture = _normal;
                        _opacity = 0.75f;
                        _verif = true;
                    }
                    else
                    {
                        _currentTexture = _click;
                        _opacity = 1f;
                        _verif = false;
                    }
                }
            }
        }

        public void Uncheck()
        {
            _isChecked = false;
            _loto.DernieresCases.Remove(this);
            _loto.ButtonList.RemoveRange(0, _loto.ButtonList.Count);
            int aux = 0;
            int w = _loto.Game.WindowHeight - 30;
            while (w % 9 != 0) w++;
            int h = w / 9;
            if (_loto.DernieresCases.Count < 5)
            {
                for (int i = 0; i < _loto.DernieresCases.Count; i++)
                {
                    _loto.ButtonList.Add(new Button(_loto.Game, new Vector2((_loto.Game.WindowWidth / 2 - 5 * h - 5 - h) / 2f, 100 + 100 * aux), 3 * Width/2, 3 * Width/2, _loto.DernieresCases[i].Numero + "", _loto.DernieresCases[i].Uncheck));
                    aux++;
                }
            }
            else
            {
                for (int i = _loto.DernieresCases.Count - 5; i < _loto.DernieresCases.Count; i++)
                {
                    _loto.ButtonList.Add(new Button(_loto.Game, new Vector2((_loto.Game.WindowWidth / 2 - 5 * h - 5 - h) / 2f, 100 + 100 * aux), 3 * Width / 2, 3 * Width / 2, _loto.DernieresCases[i].Numero + "", _loto.DernieresCases[i].Uncheck));
                    aux++;
                }
            }
            foreach (Button b in _loto.ButtonList)
            {
                b.LoadContent(_loto.Game.Content);
            }
        }
    }
}
