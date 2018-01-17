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
using ILiveSmart.IR;
using Socket;
namespace ILiveSmart {
    public class SmartAPI {
        private CrestronControlSystem _controlSystem = null;
        private CP3Smart CP3 = null;
        private ILiveSmartLight light = null;
        private SmartIR ir = null;
        private CP3Smart smartExec = null;
        UDPAPI udp = new UDPAPI();
        public SmartAPI (CrestronControlSystem system) {
            this._controlSystem = system;
            this.init ();
            udp.SendData("44");
        }
        private void init () {
            CP3 = new CP3Smart (this._controlSystem);
            light = new ILiveSmartLight (this._controlSystem);
            UDPAPI udp = new UDPAPI();
            CP3.RegisterDevices ();
            light.RegisterDevice ();
            this.ir = new SmartIR(smartExec);
            if (udp.Receive("192.168.188.112", 8080) == 1)
            {
                ir.TV_Open();
                udp.SendData("asas");
                
            }
            if (udp.Receive("192.168.188.112", 8080) == 2)
            {
                ir.TV_Up();
                udp.SendData("asas");

            }
            if (udp.Receive("192.168.188.112", 8080) == 3)
            {
                ir.TV_Down();
                udp.SendData("asas");
            }

        }
      


    }

}