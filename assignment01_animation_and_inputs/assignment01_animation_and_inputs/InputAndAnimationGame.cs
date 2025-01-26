using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_inputs;

public class InputAndAnimationGame : Game
{
    private const int _WindowWidth = 272;
    private const int _WindowHeight = 160;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _arial;

    private Texture2D _background, _foreground;
    private CelAnimationSequence _sequence01;
    private CelAnimationPlayer _animation01;
    private CelAnimationSequenceMultiRow _flyingSequenceLeft;
    private CelAnimationSequenceMultiRow _flyingSequenceRight;
    private CelAnimationSequenceMultiRow _flyingSequenceUp;
    private CelAnimationSequenceMultiRow _flyingSequenceDown;
    private CelAnimationPlayerMultiRow _flyingAnimation;
    private string _message = "Hi. It's cold out.";
    private KeyboardState _kbPreviousState;
    private Vector2 _position;
    
    

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
    _foreground = Content.Load<Texture2D>("mountain-fair");
    Texture2D torch = Content.Load<Texture2D>("animated_torch");
       //pass the constructor the spritesheet, the width of each cel,
       //and the amount of time to display each cell

       _sequence01 = new CelAnimationSequence(torch, 32, 1 / 8f);

      _animation01 = new CelAnimationPlayer();
      _animation01.Play(_sequence01);

    Texture2D spriteSheet = Content.Load<Texture2D>("Flying");
    _position = new Vector2(0,0);
    _flyingSequenceLeft = new CelAnimationSequenceMultiRow(spriteSheet, 32, 32, 1/8f, 3);
    _flyingAnimation = new CelAnimationPlayerMultiRow();
    _flyingAnimation.Play(_flyingSequenceLeft);
    _flyingSequenceRight = new CelAnimationSequenceMultiRow(spriteSheet, 32, 32, 1/8f, 1);
    _flyingAnimation = new CelAnimationPlayerMultiRow();
    _flyingAnimation.Play(_flyingSequenceRight);
    _flyingSequenceUp = new CelAnimationSequenceMultiRow(spriteSheet, 32, 32, 1/8f, 2);
    _flyingAnimation = new CelAnimationPlayerMultiRow();
    _flyingAnimation.Play(_flyingSequenceUp);
    _flyingSequenceDown = new CelAnimationSequenceMultiRow(spriteSheet, 32, 32, 1/8f, 0);
    _flyingAnimation = new CelAnimationPlayerMultiRow();
    _flyingAnimation.Play(_flyingSequenceDown);
    
        // TODO: u = new Cese this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        _animation01.Update(gameTime);
        _flyingAnimation.Update(gameTime);
         KeyboardState kbCurrentState = Keyboard.GetState();

        _message = "";

        #region arrow keys
        if(kbCurrentState.IsKeyDown(Keys.Down))//"Keys.Down" represents the down arrow on the keyboard
        {
           _flyingAnimation.Play(_flyingSequenceDown);
          _position.Y += 1;
        }
        if(kbCurrentState.IsKeyDown(Keys.Up))
        {
            _flyingAnimation.Play(_flyingSequenceUp);
            _position.Y -= 1;
        }
        if(kbCurrentState.IsKeyDown(Keys.Left))
        {
            _flyingAnimation.Play(_flyingSequenceLeft);
            _position.X -= 1;
        }
        if(kbCurrentState.IsKeyDown(Keys.Right))
        {
            _flyingAnimation.Play(_flyingSequenceRight);
            _position.X += 1;
        }
        #endregion
        
        #region "key down" event
        if(_kbPreviousState.IsKeyUp(Keys.Space) && kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";            
        }
        #endregion 
        //"key hold" event
        else if(kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "Space ";
        }
        #region "key up" event
        else if(_kbPreviousState.IsKeyDown(Keys.Space))
        {
            //the space key is not being held down right now
            //but it was being held down on the last call to Update()
            //so, this is a "key up" event
            _message += "######################################################\n";
            _message += "######################################################\n";
            _message += "######################################################\n";
            _message += "######################################################\n";
            _message += "######################################################\n";
            _message += "######################################################\n";
        }
        #endregion


        //remember the state of the keyboard for the next call to update
        _kbPreviousState = kbCurrentState;

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
       _flyingAnimation.Draw(_spriteBatch, _position, SpriteEffects.None );
       _spriteBatch.End();

        base.Draw(gameTime);
    }
    
}
