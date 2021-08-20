using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Shmup
{
    public class Bullet : GameObject
    {
        private Rectangle _rectangle = new(0, 0, 10, 10);
        private readonly float _projectileSpeed;
        private float TimeToLive = 10;
        
        public Bullet(float x, float y, float projectileSpeed)
        {
            _rectangle.x = x;
            _rectangle.y = y;
            _projectileSpeed = projectileSpeed;
        }

        public override void Awake()
        {
            Console.WriteLine(Id);
            base.Awake();
        }

        public override void Update()
        {
            TimeToLive -= GetFrameTime();
            if (TimeToLive <= 0)
                Destroy(this);
            
            _rectangle.x += _projectileSpeed * GetFrameTime();
            DrawRectangleRec(_rectangle, Color.RED);
        }
    }
}