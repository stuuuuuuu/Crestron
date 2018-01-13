using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net.Http;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro.Lighting;
using ILiveSmart;
using ILiveSmart.light;
namespace ILiveSmart {
    public class SmartAPI {
        private CrestronControlSystem _controlSystem = null;
        private CP3Smart CP3 = null;
        private ILiveSmartLight light = null;
        public SmartAPI (CrestronControlSystem system) {
            this._controlSystem = system;
            this.init ();
        }
        private void init () {
            CP3 = new CP3Smart (this._controlSystem);
            light = new ILiveSmartLight (this._controlSystem);
            CP3.RegisterDevices ();
            light.RegisterDevice ();

        }
      


    }

}