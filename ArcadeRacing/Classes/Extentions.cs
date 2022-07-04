using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes
{
    static class Extention
    {
        const float sizeX = 6.9f;
        static float sizeY = sizeX / ((float)GlobalRenderSettings.windowWidth / GlobalRenderSettings.windowHeight);
        public static float ConvertToMono_x(this float x)
        {
            return (GlobalRenderSettings.windowWidth / 2) + (x / sizeX) * (GlobalRenderSettings.windowWidth / 2);//((x+sizeX) / (2 * sizeX)) * width; // + 0.5f * width;
        }
        public static float ConvertToMono_y(this float y)
        {
            return ((sizeY - y) / (sizeY * 2)) * GlobalRenderSettings.windowHeight;
        }
    }
}
