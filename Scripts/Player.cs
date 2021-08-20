using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.KeyboardKey;

namespace Shmup.Scripts
{
    public class Player : GameObject
    {
        private const float ShootDelay = 0.2f;
        private const int PlayerSpeed = 300;

        private Rectangle _rectangle = new(0, 0, 30, 30);
        private Vector2 _position;
        readonly Random _random = new();
        private float _timeToNextShot;

        public override void Start()
        {
            _position.X = 0;
            _position.Y = 0;
        }

        public override async void Update()
        {
            var deltaTime = GetFrameTime();

            Shoot(deltaTime);
            GetInput(deltaTime);
            DrawRectangleRec(_rectangle, Color.BLUE);
        }

        private void GetInput(float deltaTime)
        {
            if (IsKeyDown(KEY_W) || IsKeyDown(KEY_UP))
                _position.Y += -PlayerSpeed * deltaTime;
            if (IsKeyDown(KEY_S) || IsKeyDown(KEY_DOWN))
                _position.Y -= -PlayerSpeed * deltaTime;
            if (IsKeyDown(KEY_A) || IsKeyDown(KEY_LEFT))
                _position.X -= PlayerSpeed * deltaTime;
            if (IsKeyDown(KEY_D) || IsKeyDown(KEY_RIGHT))
                _position.X += PlayerSpeed * deltaTime;
                
            _rectangle.x = _position.X;
            _rectangle.y = _position.Y;
        }

        private void Shoot(float deltaTime)
        {
            _timeToNextShot -= deltaTime;
            if (IsKeyDown(KEY_SPACE) && _timeToNextShot <= 0)
            {
                Instantiate(new Bullet(_position.X + _rectangle.width, _position.Y + _rectangle.height / 2, 500));
                _timeToNextShot = ShootDelay;
            }
        }
    }
}