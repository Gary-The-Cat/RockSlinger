using CarSimulation;
using CarSimulation.Screens;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Arkanoid_SFML.Screens
{
    public class GameScreen : Screen
    {
        internal RenderTexture texture;

        internal int frame;

        readonly Vector2f TitleBarSize = new Vector2f(12, 57);

        List<Drawable> entities;

        public CircleShape CreateCircleShape(Vector2f pos, int radius) => new CircleShape(radius) { Position = pos, Origin = new Vector2f(radius, radius)};

        public Vertex[] dirt;
        public Vertex[] grass;
        public Vertex[] background;

        public GameScreen(
            RenderWindow window,
            FloatRect configuration)
            : base(window, configuration)
        {
            frame = 0;
            texture = new RenderTexture(Configuration.Width, Configuration.Height);

            entities = new List<Drawable>();

            var dirtBottom = new Color(0x62, 0x43, 0x32);
            var dirtTop = new Color(0x9B, 0x6B, 0x4F);

            var grassBottom = new Color(0x4C, 0x91, 0x4B);
            var grassTop = new Color(0x4C, 0x91, 0x4B);

            var backgroundBottom = new Color(0x87, 0xB5, 0xC2);
            var backgroundTop = new Color(0x4B, 0x7D, 0x91);

            var dirtHeight = 76;
            var grassHeight = 8;
            var topOfGrass = dirtHeight + grassHeight;

            dirt = new Vertex[]
            {
                new Vertex(new Vector2f(0, Configuration.Height - dirtHeight), dirtBottom),
                new Vertex(new Vector2f(Configuration.Width, Configuration.Height - dirtHeight), dirtBottom),
                new Vertex(new Vector2f(Configuration.Width, Configuration.Height), dirtTop),
                new Vertex(new Vector2f(0, Configuration.Height), dirtTop),
            };

            grass = new Vertex[]
            {
                new Vertex(new Vector2f(0, Configuration.Height - topOfGrass), grassBottom),
                new Vertex(new Vector2f(Configuration.Width, Configuration.Height - topOfGrass), grassBottom),
                new Vertex(new Vector2f(Configuration.Width, Configuration.Height - dirtHeight), grassTop),
                new Vertex(new Vector2f(0, Configuration.Height - dirtHeight), grassTop),
            };

            background = new Vertex[]
            {
                new Vertex(new Vector2f(0, Configuration.Height), backgroundTop),
                new Vertex(new Vector2f(Configuration.Width, Configuration.Height), backgroundTop),
                new Vertex(new Vector2f(Configuration.Width, 0), backgroundTop),
                new Vertex(new Vector2f(0, 0), backgroundBottom),
            };
        }

        /// <summary>
        /// Update - Here we add all our logic for updating components in this screen. 
        /// This includes checking for user input, updating the position of moving objects and more!
        /// </summary>
        /// <param name="deltaT">The amount of time that has passed since the last frame was drawn.</param>
        public override void Update(float deltaT)
        {
            
        }

        /// <summary>
        /// Draw - Here we don't update any of the components, only draw them in their current state to the screen.
        /// </summary>
        /// <param name="deltaT">The amount of time that has passed since the last frame was drawn.</param>
        public override void Draw(float deltaT)
        {
            window.Clear();
            texture.Clear();

            texture.Draw(background, 0, 4, PrimitiveType.Quads);
            texture.Draw(dirt, 0, 4, PrimitiveType.Quads);
            texture.Draw(grass, 0, 4, PrimitiveType.Quads);

            foreach (var entity in entities)
            {
                texture.Draw(entity);
            }

            frame++;
        }

        public Vector2f GetMousePosition()
        {
            var position = Mouse.GetPosition();

            var adjustedPosition = position - window.Position;

            return new Vector2f(adjustedPosition.X - TitleBarSize.X, adjustedPosition.Y - TitleBarSize.Y);
        }

        public void AddVisual(Drawable visual)
        {
            entities.Add(visual);
        }
    }
}
