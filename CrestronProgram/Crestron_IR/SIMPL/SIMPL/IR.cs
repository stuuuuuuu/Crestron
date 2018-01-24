using System;
using Crestron.SimplSharp; // For Basic SIMPL# Classes
using Crestron.SimplSharpPro; // For Basic SIMPL#Pro classes
using Socket;
namespace ILiveSmart.IR
{
    public class SmartIR
    {
        IROutputPort irRx = null;
        IROutputPort irDVD = null;
        IROutputPort irTV = null;
    
        //\\UDPAPI udp = new \\UDPAPI();
        public SmartIR(CP3Smart smart)
        {
           
            this.irRx = smart.myIROutputPort3;//Yamaha
            //this.irDVD = smart.myIROutputPort2; //  BDP
            //this.irTV = smart.myIROutputPort1; //sony 
       
          
            string Rxfile = "\\User\\IR\\Yamaha RX-V377.ir";
            string DVDfile = "\\User\\IR\\BDP-X500SE.ir";
            string TVfile = "\\User\\IR\\Sony TV.ir";
            try
            {
               
                uint i = irRx.LoadIRDriver(Rxfile);
                //uint j = irDVD.LoadIRDriver(DVDfile);
                //uint k = irTV.LoadIRDriver(TVfile);

         
            }
            catch (Exception ex)
            {

                ErrorLog.Error("IR failed registration. Cause: {0}", ex);
            }


        }
        #region TV
        public void Rx_Open()
        {
            this.irRx.PressAndRelease("POWER_ON",1000);

        }
        public void Rx_Open()
        {
            this.irRx.PressAndRelease("POWER_ON", 1000);

        }
        public void TV_Open()
        {
            this.irTV.PressAndRelease("POWER_ON", 1000);


        }
        public void DVD_Open()
        {
            this.irDVD.PressAndRelease("Power_Switch", 1000);

        }
        #endregion

        //#region 客厅空调
        //public void LivingTempOpen()
        //{
        //    this.irLiving.Press("ON");
        //}
        //public void LivingTempClose()
        //{
        //    this.irLiving.Press("OFF");
        //}

        //public void LivingTempCoolLower()
        //{
        //    this.irLiving.Press("CLow");
        //}
        //public void LivingTempCoolCenter()
        //{
        //    this.irLiving.Press("CCenter");
        //}
        //public void LivingTempCoolHight()
        //{
        //    this.irLiving.Press("CHight");
        //}
        //public void LivingTempHotLower()
        //{
        //    this.irLiving.Press("HLow");
        //}
        //public void LivingTempHotCenter()
        //{
        //    this.irLiving.Press("HCenter");
        //}
        //public void LivingTempHotHight()
        //{
        //    this.irLiving.Press("HHight");
        //}
        //#endregion

    //    #region 书房空调
    //    internal void StudyTempOff()
    //    {
    //        this.irStudyRoom.Press("OFF");
    //    }

    //    internal void StudyTempOn()
    //    {
    //        this.irTV.Press("ON");
    //    }
    //    #endregion
    }


}
