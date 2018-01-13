using System;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.GeneralIO;
using Crestron.SimplSharpPro.Keypads;
using Crestron.SimplSharpPro.UI;
using ILiveSmart.light;
namespace ILiveSmart {
    public class SmartInput {

        private CrestronControlSystem controlSystem = null;
        public Tsw1052 tsw1052;
        private ILiveSmartLight _logic = null;
        public C2niCb c2nicb_BedRoom;
        public C2niCb c2nicb_StudyRoom;
        public C2niCb c2nicb_MettingRoom;
        public C2niCb c2nicb_GeneralRoom;

        public SmartInput (CrestronControlSystem system, ILiveSmartLight logic) {
            this.controlSystem = system;
            this._logic = logic;
        }

        #region 注册所有设备
        public void RegisterDevices () {
            #region 注册快思聪总线设备
            c2nicb_BedRoom = new C2niCb (0x20, this.controlSystem);
            c2nicb_BedRoom.ButtonStateChange += new ButtonEventHandler (c2nicb_BedRoom_ButtonStateChange);
            //c2nicb_BedRoom.DigitalInputPorts[1].StateChange += new DigitalInputEventHandler (SmartInput_StateChange1);
            if (c2nicb_BedRoom.Register () != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error ("c2nicb_BedRoom failed registration. Cause: {0}", c2nicb_BedRoom.RegistrationFailureReason);

            c2nicb_StudyRoom = new C2niCb (0x21, this.controlSystem);
            c2nicb_StudyRoom.ButtonStateChange += new ButtonEventHandler (c2nicb_StudyRoom_ButtonStateChange);
            if (c2nicb_StudyRoom.Register () != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error ("c2nicb_StudyRoom failed registration. Cause: {0}", c2nicb_StudyRoom.RegistrationFailureReason);

        }

        #endregion
        #region 卧室面板事件
        /// <summary>
        /// 卧室面板按钮事件
        /// </summary>
        void c2nicb_BedRoom_ButtonStateChange (GenericBase device, ButtonEventArgs args) {
            if (args.NewButtonState == eButtonState.Pressed) {
                if (this._logic.BedRoomScenceIsBusy) {
                    return;
                }
                Button button = args.Button;
                switch (button.Number) {
                    case 2: //左上
                        {
                            new Thread (new ThreadCallbackFunction (this.OpenBedRoomALLLights), this, Thread.eThreadStartOptions.Running);
                            break;
                        }
                    case 4:
                        {
                            new Thread (new ThreadCallbackFunction (this.CloseBedRoomALLLights), this, Thread.eThreadStartOptions.Running);
                            break;
                        }
                    case 6:
                        {

                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 9:
                        {
                            break;
                        }
                    case 11:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        #endregion
        #region 书房面板事件
        /// <summary>
        /// 书房面板按钮事件
        /// </summary>
        void c2nicb_StudyRoom_ButtonStateChange (GenericBase device, ButtonEventArgs args) {
            if (args.NewButtonState == eButtonState.Pressed) {

                Button button = args.Button;
                switch (button.Number) {
                    case 2: //左上
                        {
                            new Thread (new ThreadCallbackFunction (this.OpenStudyRoomALLLights), this, Thread.eThreadStartOptions.Running);
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 9:
                        {
                            break;
                        }
                    case 11:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        #endregion
        #region  卧室灯光全开
        public object OpenBedRoomALLLights (object o) {
            this._logic.BedRoomScenceIsBusy = true;
            this.c2nicb_BedRoom.Feedbacks[1].BlinkPattern = eButtonBlinkPattern.SlowBlink;
            this.c2nicb_BedRoom.Feedbacks[2].BlinkPattern = eButtonBlinkPattern.SlowBlink;
            this.c2nicb_BedRoom.Feedbacks[1].State = true;
            this.c2nicb_BedRoom.Feedbacks[2].State = true;
            this.c2nicb_BedRoom.Feedbacks[3].State = false;
            this.c2nicb_BedRoom.Feedbacks[4].State = false;
            this.c2nicb_BedRoom.Feedbacks[5].State = false;
            this.c2nicb_BedRoom.Feedbacks[6].State = false;
            this.c2nicb_BedRoom.Feedbacks[7].State = false;
            this.c2nicb_BedRoom.Feedbacks[8].State = false;
            this.c2nicb_BedRoom.Feedbacks[9].State = false;
            this.c2nicb_BedRoom.Feedbacks[10].State = false;
            this.c2nicb_BedRoom.Feedbacks[11].State = false;
            this.c2nicb_BedRoom.Feedbacks[12].State = false;
            this._logic.BedRoomLightAllOpen ();
            this.c2nicb_BedRoom.Feedbacks[1].BlinkPattern = eButtonBlinkPattern.AlwaysOn;
            this.c2nicb_BedRoom.Feedbacks[2].BlinkPattern = eButtonBlinkPattern.AlwaysOn;
            this._logic.BedRoomScenceIsBusy = false;
            return o;

        }
        #endregion
        #region  卧室灯光全灭
        public object CloseBedRoomALLLights (object o) {
            this.c2nicb_BedRoom.Feedbacks[3].BlinkPattern = eButtonBlinkPattern.SlowBlink;
            this.c2nicb_BedRoom.Feedbacks[4].BlinkPattern = eButtonBlinkPattern.SlowBlink;
            this.c2nicb_BedRoom.Feedbacks[1].State = false;
            this.c2nicb_BedRoom.Feedbacks[2].State = false;
            this.c2nicb_BedRoom.Feedbacks[3].State = true;
            this.c2nicb_BedRoom.Feedbacks[4].State = true;
            this.c2nicb_BedRoom.Feedbacks[5].State = false;
            this.c2nicb_BedRoom.Feedbacks[6].State = false;
            this.c2nicb_BedRoom.Feedbacks[7].State = false;
            this.c2nicb_BedRoom.Feedbacks[8].State = false;
            this.c2nicb_BedRoom.Feedbacks[9].State = false;
            this.c2nicb_BedRoom.Feedbacks[10].State = false;
            this.c2nicb_BedRoom.Feedbacks[11].State = false;
            this.c2nicb_BedRoom.Feedbacks[12].State = false;
            this._logic.BedRoomLightAllClose ();
            this.c2nicb_BedRoom.Feedbacks[3].BlinkPattern = eButtonBlinkPattern.AlwaysOn;
            this.c2nicb_BedRoom.Feedbacks[4].BlinkPattern = eButtonBlinkPattern.AlwaysOn;
            return o;
        }
        #endregion
        #region 书房灯光全开
        public object OpenStudyRoomALLLights (object o) {
            this.c2nicb_StudyRoom.Feedbacks[1].BlinkPattern = eButtonBlinkPattern.SlowBlink;
            this.c2nicb_StudyRoom.Feedbacks[2].BlinkPattern = eButtonBlinkPattern.SlowBlink;
            this.c2nicb_StudyRoom.Feedbacks[1].State = true;
            this.c2nicb_StudyRoom.Feedbacks[2].State = true;
            this.c2nicb_StudyRoom.Feedbacks[3].State = false;
            this.c2nicb_StudyRoom.Feedbacks[4].State = false;
            this.c2nicb_StudyRoom.Feedbacks[5].State = false;
            this.c2nicb_StudyRoom.Feedbacks[6].State = false;
            this.c2nicb_StudyRoom.Feedbacks[7].State = false;
            this.c2nicb_StudyRoom.Feedbacks[8].State = false;
            this.c2nicb_StudyRoom.Feedbacks[9].State = false;
            this.c2nicb_StudyRoom.Feedbacks[10].State = false;
            this.c2nicb_StudyRoom.Feedbacks[11].State = false;
            this.c2nicb_StudyRoom.Feedbacks[12].State = false;
            this._logic.StudyLightAllOpen();
            this.c2nicb_StudyRoom.Feedbacks[1].BlinkPattern = eButtonBlinkPattern.AlwaysOn;
            this.c2nicb_StudyRoom.Feedbacks[2].BlinkPattern = eButtonBlinkPattern.AlwaysOn;
            return o;

        }
        #endregion

    }

}
        #endregion

