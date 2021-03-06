﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arkanoid_SFML.CrateTools;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid_SFML.Screens
{
    public class MainGameScreen : GameScreen
    {
        CrateManager crateManager;
        RectangleShape potentialCratePosition;
        Texture potentialCrateTexture;
        Texture crateTexture;
        Texture crateBrokenTexture;
        Texture crateTattersTexture;

        public MainGameScreen(RenderWindow window, FloatRect configuration) : base(window, configuration)
        {
            window.MouseButtonPressed += MouseButtonPressed;
            window.MouseButtonPressed += MouseButtonReleased;
            crateManager = new CrateManager();
            potentialCrateTexture = new Texture(new Image("PotentialCrate.png"));
            crateTexture = new Texture(new Image("Crate.png"));
            crateBrokenTexture = new Texture(new Image("CrateBroken.png"));
            crateTattersTexture = new Texture(new Image("CrateVeryBroken.png"));
        }

        public override void Update(float deltaT)
        {
            base.Update(deltaT);

            var mousePosition = GetMousePosition();
            var crateIndex = crateManager.GetCrateIndexFromPosition(mousePosition);

            if (crateIndex.Y < 0 || crateIndex.Y == 7)
            {
                potentialCratePosition = null;
                return;
            }

            var position = crateManager.GetCrateCentreFromIndex(crateIndex);
            potentialCratePosition = new RectangleShape(new Vector2f(128, 128));
            potentialCratePosition.Texture = potentialCrateTexture;
            potentialCratePosition.Origin = new Vector2f(64, 64);
            potentialCratePosition.Position = position;
        }

        public override void Draw(float deltaT)
        {
            base.Draw(deltaT);

            if(potentialCratePosition != null)
            {
                texture.Draw(potentialCratePosition);
            }

            foreach(var cratePosition in crateManager.GetCrates())
            {
                if(cratePosition == null)
                {
                    continue;
                }

                var crate = new RectangleShape(new Vector2f(128, 128));
                crate.Texture = GetCrateTexture(cratePosition.CrateType);
                crate.Origin = new Vector2f(64, 64);
                crate.Position = cratePosition.Centroid;
                texture.Draw(crate);
            }

            texture.Display();
            var sprite = new Sprite(texture.Texture);

            window.Draw(sprite);
        }

        private Texture GetCrateTexture(CrateType crateType)
        {
            if (crateType == CrateType.Full)
            {
                return crateTexture;
            }
            if (crateType == CrateType.Broken)
            {
                return crateBrokenTexture;
            }
            if(crateType == CrateType.Tatters)
            {
                return crateTattersTexture;
            }

            return crateTexture;
        }

        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = GetMousePosition();
            crateManager.AddCrate(mousePosition);
        }

        private void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = GetMousePosition();
        }

    }
}
