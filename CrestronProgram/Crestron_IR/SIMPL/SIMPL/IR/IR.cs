using System;
using Crestron.SimplSharp; // For Basic SIMPL# Classes
using Crestron.SimplSharpPro; // For Basic SIMPL#Pro classes
namespace ILiveSmart.IR
{
    public class SmartIR
    {
        IROutputPort irTV = null;
        IROutputPort irLiving = null;
        public SmartIR(CP3Smart smart)
        {
            this.irTV= smart.myIROutputPort1;//Sony TV
            this.irLiving = smart.myIROutputPort6;//客厅空调
            string file = "\\User\\IR\\Sony TV.ir";
            try
            {
                uint i = irTV.LoadIRDriver(file);
                uint j = irLiving.LoadIRDriver(file);

         
            }
            catch (Exception ex)
            {

                ErrorLog.Error("IR failed registration. Cause: {0}", ex);
            }


        }
        #region TV
        public void TV_Open()
        {
            this.irTV.Press("Power");

        }
        public void TV_Down()
        {
            this.irTV.Press("DOWN");

        }
        public void TV_Up()
        {
            this.irTV.Press("UP");

        }
        #endregion

        #region 客厅空调
        public void LivingTempOpen()
        {
            this.irLiving.Press("ON");
        }
        public void LivingTempClose()
        {
            this.irLiving.Press("OFF");
        }

        public void LivingTempCoolLower()
        {
            this.irLiving.Press("CLow");
        }
        public void LivingTempCoolCenter()
        {
            this.irLiving.Press("CCenter");
        }
        public void LivingTempCoolHight()
        {
            this.irLiving.Press("CHight");
        }
        public void LivingTempHotLower()
        {
            this.irLiving.Press("HLow");
        }
        public void LivingTempHotCenter()
        {
            this.irLiving.Press("HCenter");
        }
        public void LivingTempHotHight()
        {
            this.irLiving.Press("HHight");
        }
        #endregion

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
