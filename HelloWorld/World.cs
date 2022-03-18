using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Falcon;
namespace Hello
{
    public class World: MonoBehaviour
    {
        TinyMod.ModLoader tm;
        
        public void Awake()
        {
            tm = TinyMod.ModLoader.Instance;
            tm.Log("Hello World from Awake()!");
        }
        public void Start()
        {
            tm.Log("Hello World from Start()!");

        }
    }
}
