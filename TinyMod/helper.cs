using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyMod
{
    public struct Mod
    {
        public string Name;
        public int Major;
        public int Minor;
        public int Revision;

        public Mod(string name, int major, int minor, int rev)
        {
            Name = name;
            Major = major;
            Minor = minor;
            Revision = rev;
        }
    }
}
