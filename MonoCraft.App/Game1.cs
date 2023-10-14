using System;
using System.IO;
using System.Threading.Tasks;
using ConsoleClient;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoCraft.Net.Predefined;
using Newtonsoft.Json;

namespace MonoCraft.App;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private ServerStatusResponse _response;
    private Texture2D _image;
    private SpriteFont _font;
    
    public Game1(ServerStatusResponse response)
    {
        _response = response;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _image = Texture2D.FromFile(GraphicsDevice, "image.png");
        _font = Content.Load<SpriteFont>("font");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }
    
    

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        // render server icon
        _spriteBatch.Draw(_image, GraphicsDevice.Viewport.Bounds.Center.ToVector2(), null, Color.White, 0f, _image.Bounds.Center.ToVector2(), 5f, SpriteEffects.None, 0f);

        var position = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
        position.Y += _image.Height / 2f * 5f;
        position.X -= _image.Width / 2f * 5f;
        
        _spriteBatch.DrawString(_font, _response.Version.Name, position, Color.White);
        position.Y += _font.LineSpacing;
        string playerString = string.Format("Online: {0} / {1}", _response.PlayerList.Online, _response.PlayerList.Max);
        _spriteBatch.DrawString(_font, playerString, position, Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}