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
        private float totalK = 0;
        private int renderDistance = 500;
        const float dst = 2.5f;
        const float strartX = 0.5f;
        public void Start()
        {
            curvetureTotal = 0;
            totalK = k;
            k = -10;
            prev = 0;
            player.Reset();
            player.LoadContent(content);
            gameObjects?.Clear();
            segments?.Clear();
            cars?.Clear();
            for (int i = 1; i < 8; i++)
            {
                AddEnemy(strartX, i * dst);
                AddEnemy(-strartX, i * dst + dst/2);
            }

            if (GamePad.GetState(1).IsConnected)
            {
                Player p2 = new Player() { GetX = -strartX, GetZ = dst / 2, playerId = 1 };
                cars.Add(p2);
                gameObjects.Add(p2);
            }
            else
            {
                AddEnemy(-strartX, dst / 2);
            }


            player.GetX = strartX;
            player.GetZ = 0;
            //gameObjects.Add(player);

            for (int i = 0; i < renderDistance; i++)
            {
                AddSegment();
            }
            int _z = 0;
            int finishZ = 200;
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

            if (player.GetGlobalCarState == GlobalCarState.Dead
                || player.GetGlobalCarState == GlobalCarState.Finished)
                GamePad.SetVibration(0, 0, 0);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateCars(dt, curvitureFromZ(Player.CameraZ));

            CheckCarToObjectCollision();

            player.Update(dt, curvitureFromZ(player.GetZ), player);

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
            //totalK += Segment.segmentLength;
        }
        public float curvitureFromZ(float z) =>
             (float)(Math.Sin((z + totalK) * 0.15f) * Math.Sin((z + totalK) * 0.15f) * Math.Cos((z + totalK) * 0.2f) * Math.Sin((z + totalK) * 0.002f)) / 5;
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
                    //int pos = random.Next(0, 2);
                    for (int i = 0; i < 30; i++)
                    {
                        gameObjects.Add(new Obsticle(z + i / 8f, 0));
                        gameObjects.Add(new Obsticle(z + i / 8f, 1));
                    }
                    break;
                default:
                    break;
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
                        if (gameObjects[i] is Car)
                        {
                            //gameObjects[i].OnCollision(gameObjects[j]);
                        }
                    }
                }
                if (gameObjects[i].IsIntersecting(player))
                {
                    player.OnCollision(gameObjects[i]);
                    gameObjects[i].OnCollision(player);
                }
            }
        }
        public void UpdateCars(float dt, float seg0curv)
        {
            foreach (var car in cars)
            {
                car.Update(dt, curvitureFromZ(car.GetZ), player);
            }
        }
    }
}
