using System;
using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using Shmup.Scripts;
using static Raylib_cs.Raylib;

namespace Shmup
{
    public class Spawner : GameObject
    {
        private float displayTime = 3;
        private bool shouldDisplay = true;

        public override void Awake()
        {
            Instantiate(new Player());
            base.Awake();
        }

        public override void Update()
        {
            displayTime -= GetFrameTime();
            if (displayTime <= 0)
            {
                displayTime = 3;
                shouldDisplay = false;
            }
        }

        public override void UpdateUi()
        {
            if (shouldDisplay)
            {
                DrawText("Wave 1", 1920 / 2, 1080 / 2, 24, Color.YELLOW);
            }
        }
    }
}