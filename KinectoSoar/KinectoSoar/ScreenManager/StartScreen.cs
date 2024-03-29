﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KinectoSoar.ScreenManager
{
    public class StartScreen : Screen
    {
        private KeyboardState _currentState;
        private KeyboardState _prevState;

        public StartScreen(Game game, SpriteBatch sprite)
            : base(game, sprite)
        {
            GameProperties.Instance.Start = false;
            GameProperties.Instance.Reset = false;
            GameProperties.Instance.Ready = false;
            GameProperties.Instance.GameOver = false;
            _currentState = Keyboard.GetState();
            _prevState = _currentState;
        }

        public override void Update(ref Screen activeScreen, GameTime gameTime)
        {
            _currentState = Keyboard.GetState();
            if (_currentState != _prevState && _currentState.IsKeyDown(Keys.Enter))
            {
                activeScreen = new GameScreen(_game, _spriteBatch);
            }
            _prevState = _currentState;

            if (GameProperties.Instance.Start)
            {
                GameProperties.Instance.Start = false;
                activeScreen = new GameScreen(_game, _spriteBatch);
            }
        }
        public override void Draw()
        {
            Vector2 screenSize = new Vector2(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
            _spriteBatch.Draw(Resources.Instance.GetTexture("Soar"), new Rectangle((int)(150 * _game.GraphicsDevice.Viewport.Width / 900), (int)(150 * _game.GraphicsDevice.Viewport.Height / 900), Resources.Instance.GetTexture("Soar").Width * _game.GraphicsDevice.Viewport.Width / 900, Resources.Instance.GetTexture("Soar").Height * _game.GraphicsDevice.Viewport.Height / 900), Color.White); 
            Rectangle background = new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y);
            _spriteBatch.Draw(Resources.Instance.GetTexture("Background"), background, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1.0f);
            if( GameProperties.Instance.Ready )
            {
                Texture2D texture = Resources.Instance.GetTexture( "Ready" );
                _spriteBatch.Draw(texture, new Rectangle(((int)(screenSize.X - 200) / 2), (((int)(screenSize.Y - 200) / 2) + 200), 200, 200), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
            Vector2 scoreSize = Resources.Instance.GetFont("Cooper").MeasureString("High Score: " + GameProperties.Instance.HighScore.ToString());
            _spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "High Score: " + GameProperties.Instance.HighScore.ToString(), new Vector2((_game.GraphicsDevice.Viewport.Width - scoreSize.X) / 2, 10), Color.Yellow);
        }

    }
}
