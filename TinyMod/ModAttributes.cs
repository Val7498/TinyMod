
using System;

namespace TinyMod.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class ModEntry : Attribute
    {
        string MODNAME;
        string DESCRIPTION;
        int MAJOR;
        int MINOR;
        int REV;

        public string Name { get { return MODNAME; } }
        public string Description { get { return DESCRIPTION; } }
        public int Major { get { return MAJOR; } }
        public int Minor { get { return MINOR; } }
        public int Rev { get { return REV; } }
        public ModEntry(string name, string description, int major = 0, int minor = 0, int rev = 0)
        {
            MODNAME = name;
            DESCRIPTION = description;
            MAJOR = major;
            MINOR = minor;
            REV = rev;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class Script : Attribute
    {
        string _FQN;
        public string FQN { get { return _FQN; } }

        public Script(string fqn)
        {
            _FQN = fqn;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class Patch : Attribute
    {
        string _FQN;
        public string FQN { get { return _FQN; } }

        public Patch(string fqn)
        {
            _FQN = fqn;
        }
    }
}