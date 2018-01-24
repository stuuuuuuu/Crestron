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
using Crestron.SimplSharp.CrestronSockets;
namespace ILiveSmart {
    public class SmartAPI {
        private CrestronControlSystem _controlSystem ;
        private CP3Smart CP3 ;
        private ILiveSmartLight light ;
        private SmartIR ir ;
        private SmartInput input;
        private ILiveSmartLight Scence;
        //\\UDPAPI udp = new \\UDPAPI();
        private CP3Smart smartExec =null ;
        public SmartAPI (CrestronControlSystem system) {
            this._controlSystem = system;
            this.init ();
         
        }
        private void init () {

            CP3 = new CP3Smart (this._controlSystem);
            light = new ILiveSmartLight (this._controlSystem);
            input = new SmartInput(this._controlSystem,Scence);
         

           
          

            
            CP3.RegisterDevices();

            input.RegisterDevices();
            light.RegisterDevice();

            this.ir = new SmartIR(CP3);
            
      

            new Thread(new ThreadCallbackFunction(this.Receive), this, Thread.eThreadStartOptions.Running);

        }
  
       
        public object Receive(object o)
        {

            //\\UDPAPI a = new \\UDPAPI();
            this.ir.TV_Open();
            Thread.Sleep(500);
            this.ir.Rx_Open();
            Thread.Sleep(500);
            this.ir.DVD_Open();
           
           
            
            //Thread.Sleep(500);
        
            //a.ReceiveAsync();
                
            return o;
        }
     
      


    }

}