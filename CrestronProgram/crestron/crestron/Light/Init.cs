using System;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       				// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.Lighting;
using Crestron.SimplSharpPro.Lighting.Din;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.CrestronThread;
namespace ILiveSmart.light
{
    public class  IliveSmart
    {
        public Din8Sw8 din8sw8_03;
        public Din8Sw8 din8sw8_04;
        public Din8Sw8 din8sw8_05;

        public Din1Dim4 din1Dim4_10;
        public Din1Dim4 din1Dim4_11;
        public Din1Dim4 din1Dim4_12;
        public Din1Dim4 din1Dim4_13;
        private CrestronControlSystem controlSystem = null;
        public void RegisterDevice()
        {
            #region 注册灯光模块
            din1Dim4_10 = new Din1Dim4(0x10, this.controlSystem);
            if (din1Dim4_10.Register() != eDeviceRegistrationUnRegistrationResponse.Success)

                ErrorLog.Error("din1Dim4_10 failed registration. Cause: {0}", din1Dim4_10.RegistrationFailureReason);

            din1Dim4_11 = new Din1Dim4(0x11, this.controlSystem);
            if (din1Dim4_11.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("din1Dim4_11 failed registration. Cause: {0}", din1Dim4_11.RegistrationFailureReason);

            din1Dim4_12 = new Din1Dim4(0x12, this.controlSystem);
            if (din1Dim4_12.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("din1Dim4_12 failed registration. Cause: {0}", din1Dim4_12.RegistrationFailureReason);

            din1Dim4_13 = new Din1Dim4(0x13, this.controlSystem);
            if (din1Dim4_13.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("din1Dim4_13 failed registration. Cause: {0}", din1Dim4_13.RegistrationFailureReason);

            din8sw8_03 = new Din8Sw8(0x03, this.controlSystem);
            din8sw8_03.LoadStateChange += new LoadEventHandler(din8sw8_LoadStateChange);
            if (din8sw8_03.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("din8sw8_03 failed registration. Cause: {0}", din8sw8_03.RegistrationFailureReason);

            din8sw8_04 = new Din8Sw8(0x4, this.controlSystem);
            din8sw8_04.LoadStateChange += new LoadEventHandler(din8sw8_LoadStateChange);
            if (din8sw8_04.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("din8sw8_04 failed registration. Cause: {0}", din8sw8_04.RegistrationFailureReason);

            din8sw8_05 = new Din8Sw8(0x5, this.controlSystem);
            din8sw8_05.LoadStateChange += new LoadEventHandler(din8sw8_LoadStateChange);
            if (din8sw8_05.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("din8sw8_05 failed registration. Cause: {0}", din8sw8_05.RegistrationFailureReason);
            #endregion
        }

        #region 开关模块事件
        void din8sw8_LoadStateChange(LightingBase lightingObject, LoadEventArgs args)
        {

            // ILiveDebug.WriteLine(args.Load.Number.ToString());
            //throw new NotImplementedException();
        }
        #endregion

        #region 调光模块事件
        void din1Dim4_BaseEvent(GenericBase device, BaseEventArgs args)
        {
            //throw new NotImplementedException();
        }
        #endregion
     }
    

}