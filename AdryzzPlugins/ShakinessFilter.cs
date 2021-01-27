using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;
using System;
using System.Numerics;

namespace AdryzzPlugins
{
    [PluginName("Shakiness Filter")]
    public class ShakinessFilter : Notifier, IFilter
    {
        public FilterStage FilterStage => FilterStage.PostTranspose;

        DateTime start = DateTime.Now;

        public Vector2 Filter(Vector2 point)
        {
            if (DateTime.Now.Subtract(start).TotalMilliseconds >= Response)
            {
                Random r = new Random();
                start = DateTime.Now;
                return new Vector2(point.X + r.Next(-RngX, RngX), point.Y + r.Next(-RngY, RngY));
            }
            return point;
        }

        [Property("X distance"), Unit("px")]
        public int RngX { get; set; } = 20;

        [Property("Y distance"), Unit("px")]
        public int RngY { get; set; } = 20;

        [Property("Shakiness"), Unit("ms")]
        public int Response { get; set; } = 5;
    }
}
