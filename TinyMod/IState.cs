using System;
using System.Collections.Generic;
using System.Text;

namespace TinyMod
{
    public interface IState
    {
        void loadFinish();
        void sceneChange();
    }

    public class State : IState
    {
        void IState.loadFinish()
        {

        }

        void IState.sceneChange()
        {

        }
    }

}
