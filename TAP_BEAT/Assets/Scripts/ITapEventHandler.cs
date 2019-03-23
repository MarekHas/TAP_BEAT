using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TapBeat
{
    public interface ITapEventHandler
    {
        void Tapped(int current);
    }

}
