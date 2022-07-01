using ArcadeRacing.Classes.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes
{
    partial class MainGameClass
    {
        private List<Segment> segments = new List<Segment>();
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<Car> cars = new List<Car>();
        private Player player = new Player();
        private Random random = new Random();
        private float prev;
        private float dz = 0;
        private float curvetureTotal = 0;
        private float k = 0;
        private int renderDistance = 500;
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
        public void Update(GameTime gameTime)
        {
            //System.Diagnostics.Debug.WriteLine(player.GetZ);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateSegments();
            CheckCarToCarCollisions();
            CheckCarToObjectCollision();

            player.Update(dt, segments[0].curveture);

            if (player.GetZ - prev >= Segment.segmentLength)
            {
                prev = player.GetZ;
                for (int i = 0; i < 30; i++)
                {
                    if (segments[0].z - player.GetZ < Segment.segmentLength * (SegentDistructorMult-1))
                    {
                        AddSegment();
                        RemoveSegment();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        public void AddSegment()
        {
            segments.Add(new Segment(k, 
                (float)(Math.Sin(k * 0.15f)* Math.Sin(k * 0.15f)* Math.Cos(k * 0.2f) * Math.Sin(k * 0.002f))/2
                ));
            k += Segment.segmentLength;
        }
        public void RemoveSegment()
        {
            curvetureTotal += segments[0].curveture;
            segments.RemoveAt(0);
        }
        public void AddGameOblect(int z)
        {
            GameObject gameObject;
            int ob = random.Next(0, 2);
            switch (ob)
            {
                case 0:
                    gameObjects.Add(new BillBoard(z));
                    break;
                case 1:
                    if (Math.Abs(segments[0].curveture)<0.3f)
                    {
                        //int pos = random.Next(0, 2);
                        for (int i = 0; i < 50; i++)
                        {
                            gameObjects.Add(new Obsticle(z + i / 8f, 0));
                            gameObjects.Add(new Obsticle(z + i / 8f, 1));
                        }
                    }
                    break;
                default:
                    break;
            }
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
                    KillCars(player, cars[j]);
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
                if (gameObjects[i].IsIntersecting(player))
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
