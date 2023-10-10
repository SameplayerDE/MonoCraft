using ConsoleClient;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monocraft.Application.Shared
{

    public struct Block
    {
        public int X, Y, Z;
        public int Id;
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        NetClient client = new NetClient();

        private List<Block> _blocks = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            client.ConnectAsync("localhost", 25565);
            PacketHandler.OnBlockChange += UpdateBlock;
            IsMouseVisible = true;
        }

        private void UpdateBlock(object sender, BlockChangeEventArgs e)
        {

            Console.WriteLine(e.ToString());

            int x = e.X;
            int y = e.Y;
            int z = e.Z;
            int id = e.Id;

            for (int i = _blocks.Count - 1; i >= 0; i--)
            {
                Block b = _blocks[i];
                if (b.X == x && b.Y == y && b.Z == z)
                {
                    _blocks.Remove(b);
                }
            }

            _blocks.Add(new Block()
            {
                X = x,
                Y = y,
                Z = z,
                Id = id
            });


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            for (int i = _blocks.Count - 1; i >= 0; i--)
            {
                Block block = _blocks[i];
                int x = block.X;
                int y = block.Y;
                int z = block.Z;
                int id = block.Id;
                if (id == 0)
                {
                    continue;
                }
                _spriteBatch.DrawString(_font, $"{id}", new Vector2(x, z) * 16, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
