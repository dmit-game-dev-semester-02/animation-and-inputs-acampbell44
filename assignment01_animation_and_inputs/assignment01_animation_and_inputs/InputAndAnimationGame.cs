using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_inputs;

public class InputAndAnimationGame : Game
{
    private const int _WindowWidth = 640;
    private const int _WindowHeight = 320;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background, _foreground;
    private CelAnimationSequence _sequence01;//, _sequence02;
    private CelAnimationPlayer _animation01;//, _animation02;


    

    public InputAndAnimationGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    _background = Content.Load<Texture2D>("parallax-mountain-bg");
    _foreground = Content.Load<Texture2D>("parallax-mountain-mountain-far");
    Texture2D spriteSheet = Content.Load<Texture2D>("animated_torch");
       //pass the constructor the spritesheet, the width of each cel,
       //and the amount of time to display each cell

       _sequence01 = new CelAnimationSequence(spriteSheet, 32, 1 / 8f);

      _animation01 = new CelAnimationPlayer();
      _animation01.Play(_sequence01);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        _animation01.Update(gameTime);

        // TODO: Add your update logic here

        base.Update(gameTime);
    }
 protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

       _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_foreground, Vector2.Zero, Color.White);
       _animation01.Draw(_spriteBatch, Vector2.Zero, SpriteEffects.None);
       _spriteBatch.End();

        base.Draw(gameTime);
    }
    
}
