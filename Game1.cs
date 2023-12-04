using System;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace jeux1;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Maps map;

    private Player player;
    private const int TotalFrames = 5;
    private const int MillisecondsPerFrame = 100;

    private int size = 64;



    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        map = new Maps(new Texture2D[] {
            Content.Load<Texture2D>("void"),
            Content.Load<Texture2D>("grass"),
            Content.Load<Texture2D>("wallext"),
            Content.Load<Texture2D>("door")
            },
            size
        );

        // Charger la liste de textures pour l'animation du joueur
        Texture2D[] playerTextures = new Texture2D[]{
            Content.Load<Texture2D>("Bleu/Blue"),
            Content.Load<Texture2D>("Bleu/Blue2"),
            Content.Load<Texture2D>("Bleu/Blue3"),
            Content.Load<Texture2D>("Bleu/Blue4"),
            Content.Load<Texture2D>("Bleu/Blue5")
        };

        player = new Player(
            playerTextures,
            new Vector2(size, size),
            TotalFrames,
            MillisecondsPerFrame,
            new Keys[] {
                Keys.Z,
                Keys.D,
                Keys.S,
                Keys.Q
                },
            size
            );

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update(gameTime, map);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();
        map.Draw(spriteBatch, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - player.position.X, GraphicsDevice.Viewport.Bounds.Height / 2 - player.position.Y));
        player.Draw(spriteBatch, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2));
        spriteBatch.End();
        base.Draw(gameTime);
    }
}
