using CarSimulation.Screens;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Arkanoid_SFML.Screens
{
    class GameScreen : Screen
    {
        private int frame;

        readonly Vector2f TitleBarSize = new Vector2f(10, 55);

        List<Drawable> entities;

        public GameScreen(
            RenderWindow window,
            FloatRect configuration)
            : base(window, configuration)
        {
            frame = 0;

            entities = new List<Drawable>();

            window.MouseButtonPressed += MouseButtonPressed;
            window.MouseButtonPressed += MouseButtonReleased;
            window.KeyPressed += KeyPressed;
            window.KeyReleased += KeyReleased;
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

            foreach (var entity in entities)
            {
                window.Draw(entity);
            }
            
            window.SetView(Camera.GetView());

            window.Display();

            frame++;
        }


        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = GetMousePosition();
            entities.Add(new CircleShape(10) { Position = mousePosition });
        }

        private void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = GetMousePosition();
        }

        private void KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.C)
            {
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
            }
        }

        private Vector2f GetMousePosition()
        {
            var position = Mouse.GetPosition();

            var adjustedPosition = position - window.Position;

            return new Vector2f(adjustedPosition.X - TitleBarSize.X, adjustedPosition.Y - TitleBarSize.Y);
        }
    }
}
