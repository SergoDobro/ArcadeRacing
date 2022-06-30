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
        List<Segment> segments = new List<Segment>();
        List<GameObject> gameObjects = new List<GameObject>();
        List<Car> cars = new List<Car>();
        Player player = new Player();

        int renderDistance = 500;
        int currentSegment;
        public void MainGaemClass()
        {
            Start();
        }

        public void Start()
        {
            for (int i = 0; i < 100; i++)
            {
                AddGameOblects(i*5);
            }
            for (int i = 0; i < renderDistance; i++)
            {
                AddSegment();
            }
        }
        public float prev;
        public float dz = 0;
        public float speed = 0f;
        public const float maxSpeed = 20;
        public float movecoef = 2;
        public float accelaration = maxSpeed / 5;
        public float decel = -maxSpeed / 5;
        public void Update(GameTime gameTime)
        {
            System.Diagnostics.Debug.WriteLine(player.GetZ);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateSegments();
            CheckCarToCarCollisions();
            CheckCarToObjectCollision();

            player.GetX += -dt * segments[0].curveture  * movecoef/2 * speed / maxSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                speed += dt * accelaration;
                if (speed > maxSpeed) 
                    speed = maxSpeed;
            }
            else
            {
                speed += dt*decel;
                if (speed<0)
                {
                    speed = 0;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D)) player.GetX += dt * movecoef * speed / maxSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) player.GetX -= dt * movecoef * speed / maxSpeed;

            player.GetZ += dt * speed;
            player.GetX = Math.Clamp(player.GetX, -2, 2);
            if (player.GetZ - prev >= Segment.segmentLength)
            {
                prev = player.GetZ;
                for (int i = 0; i < 30; i++)
                {
                    if (segments[0].z - player.GetZ < Segment.segmentLength * SegentDistructorMult)
                    {
                        AddSegment();
                        segments.RemoveAt(0);
                        
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        float k = 0;
        public void AddSegment()
        {
            segments.Add(new Segment(k, 
                (float)(Math.Sin(k * 0.15f)* Math.Sin(k * 0.15f)* Math.Cos(k * 0.2f) * Math.Sin(k * 0.002f))
                ));
            k += Segment.segmentLength;
        }
        public void AddGameOblects(int z)
        {
            gameObjects.Add(new BillBoard() { GetZ = z });
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
