using UnityEngine;
using UnityEngine.Video;
using System;
namespace Atouas
{
    [Serializable]
    public struct Braccia
    {
        public bool singolo;
        
        [ShowIf("singolo", false)]
        public VideoClip idleSX;
        [ShowIf("singolo", false)]
        public Vector3 posIdleSX;
        [ShowIf("singolo", false)]
        public VideoClip idleDX;
        [ShowIf("singolo", false)]
        public Vector3 posIdleDX;
        [ShowIf("singolo", true)]
        public VideoClip idle;
        [ShowIf("singolo", true)]
        public Vector3 posIdle;
        [ShowIf("singolo", false)]
        public VideoClip runSX;
        [ShowIf("singolo", false)]
        public Vector3 posRunSX;
        [ShowIf("singolo", false)]
        public VideoClip runDX;
        [ShowIf("singolo", false)]
        public Vector3 posRunDX;
        [ShowIf("singolo", true)]
        public VideoClip run;
        [ShowIf("singolo", true)]
        public Vector3 posRun;
        [ShowIf("singolo", false)]
        public VideoClip attackSX;
        [ShowIf("singolo", false)]
        public Vector3 posAttackSX;
        [ShowIf("singolo", false)]
        public VideoClip attackDX;
        [ShowIf("singolo", false)]
        public Vector3 posAttackDX;
        [ShowIf("singolo", true)]
        public VideoClip attackSingolo;
        [ShowIf("singolo", true)]
        public Vector3 posAttackSingolo;
        public VideoClip carica;
        public Vector3 posCarica;
        public VideoClip attackCaricato;
        public Vector3 posAttackCaricato;
        
    }
}
