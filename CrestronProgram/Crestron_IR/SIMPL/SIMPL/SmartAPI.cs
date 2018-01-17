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
        private CrestronControlSystem _controlSystem ;
        private CP3Smart CP3 ;
        private ILiveSmartLight light ;
        private SmartIR ir ;
        private SmartInput input;
        private ILiveSmartLight Scence;
        UDPAPI udp = new UDPAPI();
        private CP3Smart smartExec ;
        public SmartAPI (CrestronControlSystem system) {
            this._controlSystem = system;
            udp.SendData("192.168.188.112", 8080, "init");
            this.init ();
         
        }
        private void init () {

            CP3 = new CP3Smart (this._controlSystem);
            light = new ILiveSmartLight (this._controlSystem);
            input = new SmartInput(this._controlSystem,Scence);
            //ir = new SmartIR(smartExec);
          
            udp.SendData("192.168.188.112", 8080, "register");
            
            CP3.RegisterDevices();

            input.RegisterDevices();
            light.RegisterDevice();
            udp.SendData("192.168.188.112", 8080, "IR");
        
            //this.ir = new SmartIR(smartExec);
       
            //if (udp.Receive("192.168.188.112", 8080) == 1)
            //{
            //    //ir.TV_Open();
            //    udp.SendData("192.168.188.112", 8080, "5");
                
            //}
            //if (udp.Receive("192.168.188.112", 8080) == 2)
            //{
            //    //ir.TV_Up();
            //    udp.SendData("192.168.188.112", 8080, "6");

            //}
            //if (udp.Receive("192.168.188.112", 8080) == 3)
            //{
            //    //ir.TV_Down();
            //    udp.SendData("192.168.188.112", 8080, "7");
            //}

        }
      


    }

}