using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Loto
{
    class Loto
    {
        public Game1 Game;
        public Case[,] Grille;
        public List<Case> DernieresCases;
        public List<Button> ButtonList;
        public Vector2 LastWindowSize;

        public Loto(Game1 game)
        {
            Game = game;

            int num = 90;
            Grille = new Case[10,9];
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Grille[x, y] = new Case(this, num, x, y);
                    if (num == 90) num = 0;
                    num ++;
                }
            }
            DernieresCases = new List<Case>();
            ButtonList = new List<Button>(5);
        }

        public void LoadContent(ContentManager cm)
        {
            foreach (Case c in Grille)
                c.LoadContent(cm);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Case c in Grille)
                c.Draw(sb);

            for (int i = 0; i < ButtonList.Count; i++)
            {
                ButtonList[i].Draw(sb, 0.2f+i/5f);
            }
        }

        public void Update(GameTime gt)
        {
            foreach (Case c in Grille)
                c.Update(gt);

            int w = Game.WindowHeight - 30;
            while (w % 9 != 0) w++;
            int h = w / 9;
            for (int i = 0; i < ButtonList.Count; i++)
            {
                ButtonList[i].Update(gt);
                ButtonList[i].Position = new Vector2(ButtonList[i].Position.X, 100 + 3 * h / 2 * i);
            }
                

            if (LastWindowSize != new Vector2(Game.WindowWidth, Game.WindowHeight))
            {
                foreach (Case c in Grille)
                {
                    c.Position = new Vector2(c.X * h - 5 * h + Game.WindowWidth / 2f + c.X, 10 + c.Y * h + c.Y);
                    c.Width = h;
                    c.Height = c.Width;
                }

                foreach (Button b in ButtonList)
                    b.Update2();

                LastWindowSize = new Vector2(Game.WindowWidth, Game.WindowHeight);
            }
            

        }
    }
}
