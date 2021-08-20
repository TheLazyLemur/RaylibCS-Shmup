using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using Raylib_cs;
using Shmup.Scripts;
using static Raylib_cs.Raylib;

namespace Shmup
{

    static class Program
    {
        private static List<GameObject> Scene { get; } = new()
        {
            new Player(),
            new Spawner()
        };

        public static List<GameObject> EntitiesToAdd { get; set; } = new();
        private static List<GameObject> EntitiesToRemove { get; set; } = new();

        private static void Main(string[] args)
        {

            foreach (var obj in Scene)
                obj.IsActive = true;

            InitWindow(1920, 1080, "Demo game");

            var camera = new Camera2D
            {
                target = new Vector2(),
                offset = new Vector2(),
                rotation = 0.0f,
                zoom = 1.0f
            };

            SetTargetFPS(60);

            while (!WindowShouldClose())
            {
                ClearBackground(Color.BLACK);
                BeginMode2D(camera);

                CallBacks();
                UpdateScene();

                EndMode2D();

                UpdateUi();

                EndDrawing();
            }
        }

        private static void CallBacks()
        {
            var entitiesToRemove = Scene.Where(entity => entity.IsActive == false).ToList();
            foreach (var ent in entitiesToRemove)
            {
                Scene.Remove(ent);
            }

            foreach (var entity in Scene.Where(entity => entity.HasWoken == false))
            {
                entity.Awake();
                entity.HasWoken = true;
            }

            foreach (var entity in Scene.Where(entity => entity.HasStarted == false))
            {
                entity.Start();
                entity.HasStarted = true;
            }

            foreach (var entity in Scene)
            {
                entity.Update();
            }
        }

        private static void UpdateUi()
        {
            DrawFPS(10, 10);

            foreach (var entity in Scene)
            {
                entity.UpdateUi();
            }
        }

        private static void UpdateScene()
        {
            foreach (var entity in EntitiesToAdd)
                Scene.Add(entity);

            foreach (var entity in EntitiesToRemove)
                Scene.Remove(entity);

            EntitiesToAdd = new List<GameObject>();
            EntitiesToRemove = new List<GameObject>();
        }
    }
}