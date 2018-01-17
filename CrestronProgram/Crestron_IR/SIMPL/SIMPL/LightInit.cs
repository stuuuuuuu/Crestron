using System;
using Crestron.SimplSharp; // For Basic SIMPL# Classes
using Crestron.SimplSharpPro; // For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.Lighting;
using Crestron.SimplSharpPro.Lighting.Din;
using Socket;
namespace ILiveSmart.light {
    public delegate void LightWatchEventHandler (ushort lightlevel);

    public class ILiveSmartLight {

        public bool BedRoomScenceIsBusy = false;
        /// <summary>
        /// 玄关灯
        /// </summary>
        public event LightWatchEventHandler FoyerLightWatchEvent;
        /// <summary>
        /// 客厅吊灯
        /// </summary>
        public event LightWatchEventHandler DropLightWatchEvent;

        /// <summary>
        /// 客厅右侧筒灯
        /// </summary>
        public event LightWatchEventHandler RightLightWatchEvent;
        /// <summary>
        /// 客厅前筒灯
        /// </summary>
        public event LightWatchEventHandler FrontLightWatchEvent;
        /// <summary>
        /// 客厅后筒灯
        /// </summary>
        public event LightWatchEventHandler BackLightWatchEvent;
        /// <summary>
        /// 客厅灯带
        /// </summary>
        public event LightWatchEventHandler BeltLightWatchEvent;

        public Din8Sw8 din8sw8_03;
        public Din8Sw8 din8sw8_04;
        public Din8Sw8 din8sw8_05;

        public Din1Dim4 din1Dim4_10;
        public Din1Dim4 din1Dim4_11;
        public Din1Dim4 din1Dim4_12;
        public Din1Dim4 din1Dim4_13;
        UDPAPI udp = new UDPAPI();
        private CrestronControlSystem controlSystem ;
        public ILiveSmartLight (CrestronControlSystem system) {
            this.controlSystem = system;
         
        }
        public void RegisterDevice(){
            #region 注册灯光模块
         
            din1Dim4_10 = new Din1Dim4 (0x10, this.controlSystem);
        
            if (din1Dim4_10.Register () != eDeviceRegistrationUnRegistrationResponse.Success)

                ErrorLog.Error ("din1Dim4_10 failed registration. Cause: {0}", din1Dim4_10.RegistrationFailureReason);

            din1Dim4_11 = new Din1Dim4 (0x11, this.controlSystem);
            if (din1Dim4_11.Register () != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error ("din1Dim4_11 failed registration. Cause: {0}", din1Dim4_11.RegistrationFailureReason);

            din1Dim4_12 = new Din1Dim4 (0x12, this.controlSystem);
            if (din1Dim4_12.Register () != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error ("din1Dim4_12 failed registration. Cause: {0}", din1Dim4_12.RegistrationFailureReason);

            din1Dim4_13 = new Din1Dim4 (0x13, this.controlSystem);
            if (din1Dim4_13.Register () != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error ("din1Dim4_13 failed registration. Cause: {0}", din1Dim4_13.RegistrationFailureReason);

            din8sw8_03 = new Din8Sw8 (0x03, this.controlSystem);
            din8sw8_03.LoadStateChange += new LoadEventHandler (din8sw8_LoadStateChange);
            if (din8sw8_03.Register () != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error ("din8sw8_03 failed registration. Cause: {0}", din8sw8_03.RegistrationFailureReason);

            din8sw8_04 = new Din8Sw8 (0x4, this.controlSystem);
            din8sw8_04.LoadStateChange += new LoadEventHandler (din8sw8_LoadStateChange);
            if (din8sw8_04.Register () != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error ("din8sw8_04 failed registration. Cause: {0}", din8sw8_04.RegistrationFailureReason);

            din8sw8_05 = new Din8Sw8 (0x5, this.controlSystem);
            din8sw8_05.LoadStateChange += new LoadEventHandler (din8sw8_LoadStateChange);
            if (din8sw8_05.Register () != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error ("din8sw8_05 failed registration. Cause: {0}", din8sw8_05.RegistrationFailureReason);
            udp.SendData("192.168.188.112", 8080, "light_success");
            #endregion
        }
        #region  书房灯光全开
        public void StudyLightAllOpen () {
            this.din1Dim4_12.DinLoads[1].LevelIn.CreateRamp (65535, 800);
            Thread.Sleep (800);
            this.din1Dim4_12.DinLoads[2].LevelIn.CreateRamp (65535, 800);
            Thread.Sleep (800);
            this.din1Dim4_12.DinLoads[3].LevelIn.CreateRamp (65535, 800);
        }
        #endregion
        #region  书房灯光全关
        public void StudyLightAllClose () {
            this.din1Dim4_12.DinLoads[1].LevelIn.CreateRamp (0, 800);
            Thread.Sleep (800);
            this.din1Dim4_12.DinLoads[2].LevelIn.CreateRamp (0, 800);
            Thread.Sleep (800);
            this.din1Dim4_12.DinLoads[3].LevelIn.CreateRamp(0, 800);
        }
        #endregion

        #region 玄关灯
        /// <summary>
        /// 玄关灯
        /// </summary>
        private ushort _LivingFoyerLightLevel = 0;
        public ushort LiveingFoyerLightLevel {
            get {
                return this._LivingFoyerLightLevel;
            }
            set {
                if (this._LivingFoyerLightLevel != value) {
                    this._LivingFoyerLightLevel = value;
                    this.din1Dim4_10.DinLoads[1].LevelIn.UShortValue = this._LivingFoyerLightLevel;
                    if (FoyerLightWatchEvent != null) {
                        this.FoyerLightWatchEvent (_LivingFoyerLightLevel);
                    }

                }
            }
        }
        #endregion
        #region 客厅吊灯

        private ushort _LivingDropLightLevel = 0;
        /// <summary>
        /// 客厅吊灯
        /// </summary>
        public ushort LivingDropLightLevel {
            get {
                return this._LivingDropLightLevel;
            }
            set {
                this._LivingDropLightLevel = value;
                this.din1Dim4_10.DinLoads[2].LevelIn.UShortValue = this._LivingDropLightLevel;
                if (DropLightWatchEvent != null) {
                    this.DropLightWatchEvent (this._LivingDropLightLevel);
                }
            }
        }
        #endregion

        #region 客厅灯带
        private ushort _LivingBeltLightLevel = 0;
        /// <summary>
        /// 客厅灯带
        /// </summary>
        public ushort LivingBeltLightLevel {
            get {
                return this._LivingBeltLightLevel;
            }
            set {
                this._LivingBeltLightLevel = value;
                this.din1Dim4_11.DinLoads[3].LevelIn.UShortValue = this._LivingBeltLightLevel;
                if (this.BeltLightWatchEvent != null) {
                    this.BeltLightWatchEvent (this._LivingBeltLightLevel);
                }
            }
        }
        #endregion
        #region 客厅前筒灯
        private ushort _LivingFrontLightLevel = 0;
        /// <summary>
        /// 客厅前筒灯
        /// </summary>
        public ushort LivingFrontLightLevel {
            get {
                return this._LivingFrontLightLevel;
            }
            set {
                this._LivingFrontLightLevel = value;
                this.din1Dim4_10.DinLoads[4].LevelIn.UShortValue = this._LivingFrontLightLevel;
                this.din1Dim4_11.DinLoads[2].LevelIn.UShortValue = this._LivingFrontLightLevel;
                if (this.FrontLightWatchEvent != null) {
                    this.FrontLightWatchEvent (this._LivingFrontLightLevel);
                }
            }
        }
        #endregion

        #region 客厅后筒灯
        private ushort _LivingBackLightLevel = 0;
        /// <summary>
        /// 客厅后筒灯
        /// </summary>
        public ushort LivingBackLightLevel {
            get {
                return this._LivingBackLightLevel;
            }
            set {
                this._LivingBackLightLevel = value;
                this.din1Dim4_11.DinLoads[1].LevelIn.UShortValue = this._LivingBackLightLevel;
                if (this.BackLightWatchEvent != null) {
                    this.BackLightWatchEvent (this._LivingBackLightLevel);
                }
            }
        }
        #endregion

        #region 客厅右侧筒灯

        private ushort _LivingRightLightLevel = 0;
        /// <summary>
        /// 客厅右侧筒灯
        /// </summary>
        public ushort LivingRightLightLevel {
            get {
                return this._LivingRightLightLevel;
            }
            set {
                this._LivingRightLightLevel = value;
                this.din1Dim4_10.DinLoads[3].LevelIn.UShortValue = this._LivingRightLightLevel;
                if (this.RightLightWatchEvent != null) {
                    this.RightLightWatchEvent (this._LivingRightLightLevel);
                }
            }
        }
        #endregion

        #region 书房灯光
        public void StudyRoomLightAllClose () {
            this.din1Dim4_12.DinLoads[1].LevelIn.CreateRamp (0, 500);

            Thread.Sleep (100);
            this.din1Dim4_12.DinLoads[2].LevelIn.CreateRamp (0, 500);
            Thread.Sleep (100);
            this.din1Dim4_12.DinLoads[3].LevelIn.CreateRamp (0, 500);
        }
        public void StudyRoomLightAllOpen () {
            this.din1Dim4_12.DinLoads[1].LevelIn.CreateRamp (65535, 500);
            Thread.Sleep (100);
            this.din1Dim4_12.DinLoads[2].LevelIn.CreateRamp (65535, 500);
            Thread.Sleep (100);
            this.din1Dim4_12.DinLoads[3].LevelIn.CreateRamp (65535, 500);
        }

        #endregion
        #region 看书灯光
        /// <summary>
        /// 看书灯光
        /// </summary>
        public void StudyRoomLightWatchBook () {
            this.din1Dim4_12.DinLoads[1].LevelIn.CreateRamp (ushort.MinValue, 500);
            Thread.Sleep (100);
            this.din1Dim4_12.DinLoads[2].LevelIn.CreateRamp (ushort.MaxValue, 500);
            Thread.Sleep (100);
            this.din1Dim4_12.DinLoads[3].LevelIn.CreateRamp (ushort.MinValue, 500);
        }
        #endregion
        #region 办公灯光
        /// <summary>
        /// 办公灯光
        /// </summary>
        public void StudyRoomLightOffice () {
            this.din1Dim4_12.DinLoads[1].LevelIn.CreateRamp (ushort.MaxValue, 500);
            Thread.Sleep (100);
            this.din1Dim4_12.DinLoads[2].LevelIn.CreateRamp (ushort.MinValue, 500);
            Thread.Sleep (100);
            this.din1Dim4_12.DinLoads[3].LevelIn.CreateRamp (ushort.MaxValue, 500);
        }
        #endregion

        #region 卧室灯光
        public void BedRoomLightAllOpen () {
            this.din1Dim4_12.DinLoads[4].LevelIn.CreateRamp (65535, 600);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[1].LevelIn.CreateRamp (65535, 600);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[2].LevelIn.CreateRamp (65535, 600);
        }
        public void BedRoomLightAllClose () {
            this.din1Dim4_12.DinLoads[4].LevelIn.CreateRamp (0, 600);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[1].LevelIn.CreateRamp (0, 600);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[2].LevelIn.CreateRamp (0, 600);
        }
        public void BedRoomLightLanMan () {
            this.din1Dim4_12.DinLoads[4].LevelIn.CreateRamp (30000, 600);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[1].LevelIn.CreateRamp (30000, 600);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[2].LevelIn.CreateRamp (30000, 600);
        }
        public void BedRoomLightGetUp () {
            this.din1Dim4_12.DinLoads[4].LevelIn.CreateRamp (0, 600);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[1].LevelIn.CreateRamp (20000, 600);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[2].LevelIn.CreateRamp (0, 600);
        }
        #endregion

        #region 卫生间灯光
        public void WashRoomLightAllOpen () {
            this.din1Dim4_13.DinLoads[3].LevelIn.CreateRamp (65535, 500);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[4].LevelIn.CreateRamp (65535, 500);
        }
        public void WashRoomLightAllClose () {
            this.din1Dim4_13.DinLoads[3].LevelIn.CreateRamp (0, 500);
            Thread.Sleep (100);
            this.din1Dim4_13.DinLoads[4].LevelIn.CreateRamp (0, 500);
        }
        public void WashRoomLightGetUp () {
            this.din1Dim4_13.DinLoads[4].LevelIn.CreateRamp (40000, 500);
        }
        #endregion

        #region 开关模块事件
        void din8sw8_LoadStateChange (LightingBase lightingObject, LoadEventArgs args) {

            // ILiveDebug.WriteLine(args.Load.Number.ToString());
            //throw new NotImplementedException();
          
        }
        #endregion

        #region 调光模块事件
        void din1Dim4_BaseEvent (GenericBase device, BaseEventArgs args) {
            //throw new NotImplementedException();
        }
        #endregion
    }

}