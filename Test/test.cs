using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Falcon.Game2;
namespace Vander
{
    public class Test :MonoBehaviour
    {
        TinyMod.ModLoader tm;
        FlightGame fg;

        bool playerplane = false;
        private void Awake()
        {
            tm = TinyMod.ModLoader.Instance;
        }
        
        private void onPlayerSpawn()
        {

        }
        private void Update()
        {
            if (!playerplane)
            {
                fg = FlightGame.Instance;
                if (fg != null)
                {
                    if (fg.PlayerAircraft != null)
                    {
                        tm.LogInfo("FOUND PLAYER INSTANCE!");
                        tm.LogInfo("CURRENTLY FLYING A... ");
                        tm.LogInfo(fg.PlayerAircraft.transform.position.ToString());
                        tm.LogInfo(fg.PlayerAircraft.Data.Name);
                        fg.PlayerAircraft.transform.position += Vector3.up * 500f;
                        fg.PlayerAircraft.Rigidbody.velocity += fg.PlayerAircraft.transform.forward * 500f;
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        tm.LogInfo("kyoob!");
                        cube.transform.position = fg.PlayerAircraft.transform.position + transform.forward * 1000;
                        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Shader Graphs/Tiny Diffuse");
                        MeshRenderer mesh = cube.GetComponent<MeshRenderer>();
                        mesh.material = new Material(Shader.Find("Shader Graphs/TinyDiffuse"));
                        mesh.material.color = Color.white;
                        //mesh.material.shader = Shader.Find("Shader Graphs/TinyDiffuse");
                        //cube.transform.parent = fg.PlayerAircraft.Rigidbody.transform.root;
                        
                        //cube.transform.localPosition = Vector3.zero;
                        cube.transform.localScale = Vector3.one * 100;
                        tm.LogInfo("Spawning at full speed and high alt");

                        playerplane = true;
                    }
                }
            }
        }
    }
}