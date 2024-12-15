using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotGadgets
{
    public class EACHideyourfaceconfiguration : IRocketPluginConfiguration
    {
        public List<ushort> Masklist = new List<ushort>();
        public double timerate = new double();
        public void LoadDefaults()
        {
            Masklist = new List<ushort> { 434, 435 , 1270 };
            timerate = 0.5; // Prefered is 0.5, if you have a good server you can lower.
        }
    }
}
