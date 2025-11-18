using UnityEngine;
using UnityEngine.Video;
using System;
namespace Atouas
{
    [Serializable]
    public struct Braccia
    {
        public VideoClip idle;
        public Vector3 posIdle;
        public VideoClip run;
        public Vector3 posRun;
        public VideoClip[] attack;
        public Vector3 posAttack;
    }
}
