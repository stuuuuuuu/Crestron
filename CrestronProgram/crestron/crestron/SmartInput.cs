using System;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.GeneralIO;
using Crestron.SimplSharpPro.Keypads;
using Crestron.SimplSharpPro.UI;
namespace ILiveSmart {
    public class SmartInput {

        private CrestronControlSystem controlSystem = null;
        public Tsw1052 tsw1052;
        private SmartAPI _logic = null;
        public C2niCb c2nicb_BedRoom;
        public C2niCb c2nicb_StudyRoom;
        public C2niCb c2nicb_MettingRoom;
        public C2niCb c2nicb_GeneralRoom;
        public bool BedRoomScenceIsBusy = false;
        public SmartInput (CrestronControlSystem system, SmartAPI logic) {
            this.controlSystem = system;
            this._logic = logic;
        }

        #region 注册所有设备
        public void RegisterDevices () {
            #region 注册快思聪总线设备
            c2nicb_BedRoom = new C2niCb (0x20, this.controlSystem);
            c2nicb_BedRoom.ButtonStateChange += new ButtonEventHandler (c2nicb_BedRoom_ButtonStateChange);
            c2nicb_BedRoom.DigitalInputPorts[1].StateChange += new DigitalInputEventHandler (SmartInput_StateChange1);
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
                if(this.BedRoomScenceIsBusy)
                {
                    return ;
                }
                Button button = args.Button;
                switch (button.Number) {
                    case 2: //左上
                        {
                            new Thread (new ThreadCallbackFunction (this.BedRoomScenceWatchMovie), this, Thread.eThreadStartOptions.Running);
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
        public object BedRoomScenceWatchMovie (object o) {
            this.BedRoomScenceIsBusy = true;
            

        }

    }

}