﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes
{
    partial class MainGameClass
    {
        List<Segment> segments = new List<Segment>();
        List<IGameObject> gameObjects = new List<IGameObject>();
        List<Car> cars = new List<Car>();
        Player player = new Player();

        int renderDistance = 50;
        int currentSegment;
        public void MainGaemClass()
        {
            Start();
        }

        public void Start()
        {
            for (int i = 0; i < renderDistance; i++)
            {
                AddSegment();
            }
        }
        public float prev;
        public float dz = 0;
        public void Update()
        {
            UpdateSegments();
            CheckCarToCarCollisions();
            CheckCarToObjectCollision();

            player.GetZ += 0.01f;
            if (player.GetZ - prev >= Segment.segmentLength)
            {
                prev = player.GetZ;

                AddSegment();
                //segments.RemoveAt(0);
                //for (int i = 0; i < 5; i++)
                //{
                //    if (player.GetZ - segments[0].z > Segment.segmentLength+ Segment.segmentLength)
                //    {
                //        AddSegment();
                //        segments.RemoveAt(0);
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
            }
        }
        float k = 0;
        public void AddSegment()
        {
            segments.Add(new Segment(k, graphicsDevice, (float)Math.Sin(k * 0.05f)));
            k += Segment.segmentLength;
        }

        public void CheckCollisions()
        {
            CheckCarToCarCollisions();
            CheckCarToObjectCollision();

        }
        public void CheckCarToCarCollisions()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                for (int j = 0; j < cars.Count; j++)
                {
                    if (i != j)
                    {
                        if (cars[i].IsIntersecting(cars[j]))
                        {
                            KillCars(cars[i], cars[j]);
                        }
                    }
                }
            }

            for (int j = 0; j < cars.Count; j++)
            {
                if (player.IsIntersecting(cars[j]))
                {
                    KillCars(player as Car, cars[j]);
                }
            }

        }
        public void CheckCarToObjectCollision()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = 0; j < cars.Count; j++)
                {
                    if (gameObjects[i].IsIntersecting(cars[j]))
                    {
                        cars[j].Killed();
                    }
                }
                if (gameObjects[i].IsIntersecting(player as Car))
                {
                    player.Killed();
                }
            }
        }
        public void KillCars(Car car1, Car car2)
        {
            car1.Killed();
            car2.Killed();
        }

        public void UpdateSegments()
        {
            foreach (var seg in segments)
            {
                //update segments
            }
            //add if necceserily
        }
    }
}
