using ArcadeRacing.Classes.Cars;
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
        private float curvetureTotal = 0;
        private float k = 0;
        private int renderDistance = 500;
        public void Start()
        {
            curvetureTotal = 0;
            k = -10;
            prev = 0;
            player.Reset();
            player.LoadContent(content);
            gameObjects?.Clear();
            segments?.Clear();
            for (int i = 1; i < 8; i++)
            {
                AddEnemy(0.5f, i);
                AddEnemy(-0.5f, i);
            }
            AddEnemy(-0.5f, 0);
            player.GetX = 0.5f;
            player.GetZ = 0;
            //gameObjects.Add(player);

            for (int i = 0; i < renderDistance; i++)
            {
                AddSegment();
            }
            int _z = 0;
            int finishZ = 100;
            for (int i = 0; i < 100 && _z < finishZ; i++)
            {
                AddGameOblect(_z);
                _z += random.Next(10, 14);
            }
            gameObjects.Add(new FinishLine(finishZ));
            foreach (var go in gameObjects)
            {
                if (go.GetTexture == null)
                {
                    go.LoadContent(content);
                }
            }
        }
        public void AddEnemy(float x, float z)
        {
            Enemy enemy = new Enemy() { GetX = x, GetZ = z};
            cars.Add(enemy);
            gameObjects.Add(enemy);
        }
        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateCars(dt, curvitureFromZ(Player.CameraZ));
            CheckCarToCarCollisions();
            CheckCarToObjectCollision();

            player.Update(dt, segments[0].curveture);

            if (Player.CameraZ - prev >= Segment.segmentLength)
            {
                prev = Player.CameraZ;
                for (int i = 0; i < 30; i++)
                {
                    if (segments[0].z - Player.CameraZ < Segment.segmentLength * (SegentDistructorMult - 1))
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
                curvitureFromZ(k)
                ));
            k += Segment.segmentLength;
        }
        public float curvitureFromZ(float z) =>
            0;// (float)(Math.Sin(z * 0.15f) * Math.Sin(z * 0.15f) * Math.Cos(z * 0.2f) * Math.Sin(z * 0.002f)) / 2;
        public void RemoveSegment()
        {
            curvetureTotal += segments[0].curveture;
            segments.RemoveAt(0);
        }
        public void AddGameOblect(int z)
        {
            int ob = random.Next(0, 2);
            switch (ob)
            {
                case 0:
                    gameObjects.Add(new BillBoard(z));
                    break;
                case 1:
                    if (Math.Abs(segments[0].curveture) < 0.1f)
                    {
                        //int pos = random.Next(0, 2);
                        for (int i = 0; i < 30; i++)
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
                            //ColideCars(cars[i], cars[j]);
                        }
                    }
                }
            }

            for (int j = 0; j < cars.Count; j++)
            {
                if (player.IsIntersecting(player))
                {
                }
                if (player.IsIntersecting(cars[j]))
                {
                    //ColideCars(player, cars[j]);
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
                        cars[j].OnCollision(gameObjects[i]);
                    }
                }
                if (gameObjects[i].IsIntersecting(player))
                {
                    player.OnCollision(gameObjects[i]);
                    gameObjects[i].OnCollision(player);
                }
            }
        }
        public void ColideCars(Car car1, Car car2)
        {
            car1.OnCollision(car2);
            car2.OnCollision(car1);
        }
        public void UpdateCars(float dt, float seg0curv)
        {
            foreach (var car in cars)
            {
                if (car.GetZ - player.GetZ < 200)
                {
                    car.Update(dt, curvitureFromZ(car.GetZ));
                }
            }
        }
    }
}
