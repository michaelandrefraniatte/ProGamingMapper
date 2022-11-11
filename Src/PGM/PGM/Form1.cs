using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Reflection;
using Microsoft.Win32.SafeHandles;
using System.Threading;
using SharpDX.DirectInput;
using SharpDX.XInput;
using Bitmap = System.Drawing.Bitmap;
using FastColoredTextBoxNS;
using Range = FastColoredTextBoxNS.Range;
using System.Text.RegularExpressions;
using AForge;
using AForge.Imaging;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;
using controller;
using DualSenseAPI;
using DualSenseAPI.State;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using Device.Net;
using DualSenseAPI.Util;
using System.Reactive.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;

namespace PGM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint ms);
        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(out int x, out int y);
        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int X, int Y);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        public static uint CurrentResolution = 0;
        public AForge.Video.DirectShow.FilterInfoCollection CaptureDevice;
        public AForge.Video.DirectShow.VideoCaptureDevice FinalFrame;
        public static Bitmap img, EditableImg;
        private static AForge.Imaging.BlobCounter blobCounter = new AForge.Imaging.BlobCounter();
        public static AForge.Imaging.Filters.BlobsFiltering blobfilter = new AForge.Imaging.Filters.BlobsFiltering();
        public static AForge.Imaging.Filters.ConnectedComponentsLabeling componentfilter = new AForge.Imaging.Filters.ConnectedComponentsLabeling();
        public static AForge.Imaging.Blob[] blobs;
        public static List<AForge.IntPoint> corners = new List<AForge.IntPoint>();
        public static AForge.Math.Geometry.SimpleShapeChecker shapeChecker = new AForge.Math.Geometry.SimpleShapeChecker();
        public static AForge.Imaging.Filters.BrightnessCorrection brightnessfilter;
        public static AForge.Imaging.Filters.ColorFiltering colorfilter = new AForge.Imaging.Filters.ColorFiltering();
        public static AForge.Imaging.Filters.Grayscale grayscalefilter = new AForge.Imaging.Filters.Grayscale(1, 0, 0);
        public static AForge.Imaging.Filters.EuclideanColorFiltering euclideanfilter = new AForge.Imaging.Filters.EuclideanColorFiltering();
        public static int radius = 175, brightness = -50, red = 0, green = 205, blue = 205;
        public static Form2 form2 = new Form2();
        public static Form3 form3 = new Form3();
        public static Form4 form4 = new Form4();
        public static Form5 form5 = new Form5();
        public static Form7 form7 = new Form7();
        public static Form8 form8 = new Form8();
        public static Form9 form9 = new Form9();
        public static Form10 form10 = new Form10();
        public static bool form2visible = false, form3visible = false, form4visible = false, form5visible = false, form7visible = false, form8visible = false, form9visible = false, form10visible = false;
        private static string openFilePath = "", fastColoredTextBoxSaved = "";
        private static bool justSaved = true;
        private static DialogResult result;
        private static ContextMenu contextMenu = new ContextMenu();
        private static MenuItem menuItem;
        private static string filename = "", stringscript = "", savepath = "";
        public static bool scriptrunning = false, runstopbool = false;
        private static Range range;
        private static Style InputStyle = new TextStyle(Brushes.Blue, null, System.Drawing.FontStyle.Regular), OutputStyle = new TextStyle(Brushes.Orange, null, System.Drawing.FontStyle.Regular);
        public static double backpointX, posRightX, backpointY, posRightY, camx, camy;
        private static string[] UsersAllowedList, SpeechToText;
        public static int width = Screen.PrimaryScreen.Bounds.Width;
        public static int height = Screen.PrimaryScreen.Bounds.Height;
        private static bool checkingusername = false;
        private static int sleeptime = 1;
        private Type program;
        private object obj;
        private Assembly assembly;
        private System.CodeDom.Compiler.CompilerResults results;
        private Microsoft.CSharp.CSharpCodeProvider provider;
        private System.CodeDom.Compiler.CompilerParameters parameters;
        private string code = @"
                using System;
                using System.Runtime.InteropServices;
                namespace StringToCode
                {
                    public class FooClass 
                    { 
                        public int irmode = 1, gyromode = 1, sleeptime = 1, keys12345 = 0, keys54321 = 0;
                        public bool testbool = false, EnableXC = false, EnableKM = false, EnableRI = false, EnableDI = false, EnableXI = false, EnableJI = false, EnablePI = false, JoyconLeftGyroCenter = false, JoyconRightGyroCenter = false, ProControllerGyroCenter = false, JoyconLeftAccelCenter = false, JoyconRightAccelCenter = false, ProControllerAccelCenter = false, PS5ControllerGyroCenter = false, PS5ControllerAccelCenter = false, JoyconLeftStickCenter = false, JoyconRightStickCenter = false, ProControllerStickCenter = false, Controller1GyroCenter = false, Controller2GyroCenter = false;
                        public bool[] getstate = new bool[36];
                        public int[] pollcount = new int[36];
                        double centery = 160f, mousexp = 0, mouseyp = 0, testdouble;
                        string[] UsersAllowedList = new string[] { };
                        string[] SpeechToText = new string[] { };
                        string KeyboardMouseDriverType = """"; double MouseMoveX; double MouseMoveY; double MouseAbsX; double MouseAbsY; double MouseDesktopX; double MouseDesktopY; bool SendLeftClick; bool SendRightClick; bool SendMiddleClick; bool SendWheelUp; bool SendWheelDown; bool SendLeft; bool SendRight; bool SendUp; bool SendDown; bool SendLButton; bool SendRButton; bool SendCancel; bool SendMBUTTON; bool SendXBUTTON1; bool SendXBUTTON2; bool SendBack; bool SendTab; bool SendClear; bool SendReturn; bool SendSHIFT; bool SendCONTROL; bool SendMENU; bool SendPAUSE; bool SendCAPITAL; bool SendKANA; bool SendHANGEUL; bool SendHANGUL; bool SendJUNJA; bool SendFINAL; bool SendHANJA; bool SendKANJI; bool SendEscape; bool SendCONVERT; bool SendNONCONVERT; bool SendACCEPT; bool SendMODECHANGE; bool SendSpace; bool SendPRIOR; bool SendNEXT; bool SendEND; bool SendHOME; bool SendLEFT; bool SendUP; bool SendRIGHT; bool SendDOWN; bool SendSELECT; bool SendPRINT; bool SendEXECUTE; bool SendSNAPSHOT; bool SendINSERT; bool SendDELETE; bool SendHELP; bool SendAPOSTROPHE; bool Send0; bool Send1; bool Send2; bool Send3; bool Send4; bool Send5; bool Send6; bool Send7; bool Send8; bool Send9; bool SendA; bool SendB; bool SendC; bool SendD; bool SendE; bool SendF; bool SendG; bool SendH; bool SendI; bool SendJ; bool SendK; bool SendL; bool SendM; bool SendN; bool SendO; bool SendP; bool SendQ; bool SendR; bool SendS; bool SendT; bool SendU; bool SendV; bool SendW; bool SendX; bool SendY; bool SendZ; bool SendLWIN; bool SendRWIN; bool SendAPPS; bool SendSLEEP; bool SendNUMPAD0; bool SendNUMPAD1; bool SendNUMPAD2; bool SendNUMPAD3; bool SendNUMPAD4; bool SendNUMPAD5; bool SendNUMPAD6; bool SendNUMPAD7; bool SendNUMPAD8; bool SendNUMPAD9; bool SendMULTIPLY; bool SendADD; bool SendSEPARATOR; bool SendSUBTRACT; bool SendDECIMAL; bool SendDIVIDE; bool SendF1; bool SendF2; bool SendF3; bool SendF4; bool SendF5; bool SendF6; bool SendF7; bool SendF8; bool SendF9; bool SendF10; bool SendF11; bool SendF12; bool SendF13; bool SendF14; bool SendF15; bool SendF16; bool SendF17; bool SendF18; bool SendF19; bool SendF20; bool SendF21; bool SendF22; bool SendF23; bool SendF24; bool SendNUMLOCK; bool SendSCROLL; bool SendLeftShift; bool SendRightShift; bool SendLeftControl; bool SendRightControl; bool SendLMENU; bool SendRMENU;
                        bool controller1_send_back; bool controller1_send_start; bool controller1_send_A; bool controller1_send_B; bool controller1_send_X; bool controller1_send_Y; bool controller1_send_up; bool controller1_send_left; bool controller1_send_down; bool controller1_send_right; bool controller1_send_leftstick; bool controller1_send_rightstick; bool controller1_send_leftbumper; bool controller1_send_rightbumper; bool controller1_send_lefttrigger; bool controller1_send_righttrigger; double controller1_send_leftstickx; double controller1_send_leftsticky; double controller1_send_rightstickx; double controller1_send_rightsticky; bool controller2_send_back; bool controller2_send_start; bool controller2_send_A; bool controller2_send_B; bool controller2_send_X; bool controller2_send_Y; bool controller2_send_up; bool controller2_send_left; bool controller2_send_down; bool controller2_send_right; bool controller2_send_leftstick; bool controller2_send_rightstick; bool controller2_send_leftbumper; bool controller2_send_rightbumper; bool controller2_send_lefttrigger; bool controller2_send_righttrigger; double controller2_send_leftstickx; double controller2_send_leftsticky; double controller2_send_rightstickx; double controller2_send_rightsticky; double controller1_send_lefttriggerposition; double controller1_send_righttriggerposition; double controller2_send_lefttriggerposition; double controller2_send_righttriggerposition;
                        public static int[] wd = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
                        public static int[] wu = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
                        public static void valchanged(int n, bool val)
                        {
                            if (val)
                            {
                                if (wd[n] <= 1)
                                {
                                    wd[n] = wd[n] + 1;
                                }
                                wu[n] = 0;
                            }
                            else
                            {
                                if (wu[n] <= 1)
                                {
                                    wu[n] = wu[n] + 1;
                                }
                                wd[n] = 0;
                            }
                        }
                        public object[] Main(int width, int height, bool Key_LBUTTON, bool Key_RBUTTON, bool Key_CANCEL, bool Key_MBUTTON, bool Key_XBUTTON1, bool Key_XBUTTON2, bool Key_BACK, bool Key_Tab, bool Key_CLEAR, bool Key_Return, bool Key_SHIFT, bool Key_CONTROL, bool Key_MENU, bool Key_PAUSE, bool Key_CAPITAL, bool Key_KANA, bool Key_HANGEUL, bool Key_HANGUL, bool Key_JUNJA, bool Key_FINAL, bool Key_HANJA, bool Key_KANJI, bool Key_Escape, bool Key_CONVERT, bool Key_NONCONVERT, bool Key_ACCEPT, bool Key_MODECHANGE, bool Key_Space, bool Key_PRIOR, bool Key_NEXT, bool Key_END, bool Key_HOME, bool Key_LEFT, bool Key_UP, bool Key_RIGHT, bool Key_DOWN, bool Key_SELECT, bool Key_PRINT, bool Key_EXECUTE, bool Key_SNAPSHOT, bool Key_INSERT, bool Key_DELETE, bool Key_HELP, bool Key_APOSTROPHE, bool Key_0, bool Key_1, bool Key_2, bool Key_3, bool Key_4, bool Key_5, bool Key_6, bool Key_7, bool Key_8, bool Key_9, bool Key_A, bool Key_B, bool Key_C, bool Key_D, bool Key_E, bool Key_F, bool Key_G, bool Key_H, bool Key_I, bool Key_J, bool Key_K, bool Key_L, bool Key_M, bool Key_N, bool Key_O, bool Key_P, bool Key_Q, bool Key_R, bool Key_S, bool Key_T, bool Key_U, bool Key_V, bool Key_W, bool Key_X, bool Key_Y, bool Key_Z, bool Key_LWIN, bool Key_RWIN, bool Key_APPS, bool Key_SLEEP, bool Key_NUMPAD0, bool Key_NUMPAD1, bool Key_NUMPAD2, bool Key_NUMPAD3, bool Key_NUMPAD4, bool Key_NUMPAD5, bool Key_NUMPAD6, bool Key_NUMPAD7, bool Key_NUMPAD8, bool Key_NUMPAD9, bool Key_MULTIPLY, bool Key_ADD, bool Key_SEPARATOR, bool Key_SUBTRACT, bool Key_DECIMAL, bool Key_DIVIDE, bool Key_F1, bool Key_F2, bool Key_F3, bool Key_F4, bool Key_F5, bool Key_F6, bool Key_F7, bool Key_F8, bool Key_F9, bool Key_F10, bool Key_F11, bool Key_F12, bool Key_F13, bool Key_F14, bool Key_F15, bool Key_F16, bool Key_F17, bool Key_F18, bool Key_F19, bool Key_F20, bool Key_F21, bool Key_F22, bool Key_F23, bool Key_F24, bool Key_NUMLOCK, bool Key_SCROLL, bool Key_LeftShift, bool Key_RightShift, bool Key_LeftControl, bool Key_RightControl, bool Key_LMENU, bool Key_RMENU, bool Key_BROWSER_BACK, bool Key_BROWSER_FORWARD, bool Key_BROWSER_REFRESH, bool Key_BROWSER_STOP, bool Key_BROWSER_SEARCH, bool Key_BROWSER_FAVORITES, bool Key_BROWSER_HOME, bool Key_VOLUME_MUTE, bool Key_VOLUME_DOWN, bool Key_VOLUME_UP, bool Key_MEDIA_NEXT_TRACK, bool Key_MEDIA_PREV_TRACK, bool Key_MEDIA_STOP, bool Key_MEDIA_PLAY_PAUSE, bool Key_LAUNCH_MAIL, bool Key_LAUNCH_MEDIA_SELECT, bool Key_LAUNCH_APP1, bool Key_LAUNCH_APP2, bool Key_OEM_1, bool Key_OEM_PLUS, bool Key_OEM_COMMA, bool Key_OEM_MINUS, bool Key_OEM_PERIOD, bool Key_OEM_2, bool Key_OEM_3, bool Key_OEM_4, bool Key_OEM_5, bool Key_OEM_6, bool Key_OEM_7, bool Key_OEM_8, bool Key_OEM_102, bool Key_PROCESSKEY, bool Key_PACKET, bool Key_ATTN, bool Key_CRSEL, bool Key_EXSEL, bool Key_EREOF, bool Key_PLAY, bool Key_ZOOM, bool Key_NONAME, bool Key_PA1, bool Key_OEM_CLEAR, double irx, double iry, bool WiimoteButtonStateA, bool WiimoteButtonStateB, bool WiimoteButtonStateMinus, bool WiimoteButtonStateHome, bool WiimoteButtonStatePlus, bool WiimoteButtonStateOne, bool WiimoteButtonStateTwo, bool WiimoteButtonStateUp, bool WiimoteButtonStateDown, bool WiimoteButtonStateLeft, bool WiimoteButtonStateRight, double WiimoteRawValuesX, double WiimoteRawValuesY, double WiimoteRawValuesZ, double WiimoteNunchuckStateRawJoystickX, double WiimoteNunchuckStateRawJoystickY, double WiimoteNunchuckStateRawValuesX, double WiimoteNunchuckStateRawValuesY, double WiimoteNunchuckStateRawValuesZ, bool WiimoteNunchuckStateC, bool WiimoteNunchuckStateZ, double JoyconRightStickX, double JoyconRightStickY, bool JoyconRightButtonSHOULDER_1, bool JoyconRightButtonSHOULDER_2, bool JoyconRightButtonSR, bool JoyconRightButtonSL, bool JoyconRightButtonDPAD_DOWN, bool JoyconRightButtonDPAD_RIGHT, bool JoyconRightButtonDPAD_UP, bool JoyconRightButtonDPAD_LEFT, bool JoyconRightButtonPLUS, bool JoyconRightButtonHOME, bool JoyconRightButtonSTICK, bool JoyconRightButtonACC, bool JoyconRightButtonSPA, bool JoyconRightRollLeft, bool JoyconRightRollRight, double JoyconRightAccelX, double JoyconRightAccelY, double JoyconRightGyroX, double JoyconRightGyroY, double JoyconLeftStickX, double JoyconLeftStickY, bool JoyconLeftButtonSHOULDER_1, bool JoyconLeftButtonSHOULDER_2, bool JoyconLeftButtonSR, bool JoyconLeftButtonSL, bool JoyconLeftButtonDPAD_DOWN, bool JoyconLeftButtonDPAD_RIGHT, bool JoyconLeftButtonDPAD_UP, bool JoyconLeftButtonDPAD_LEFT, bool JoyconLeftButtonMINUS, bool JoyconLeftButtonCAPTURE, bool JoyconLeftButtonSTICK, bool JoyconLeftButtonACC, bool JoyconLeftButtonSMA, bool JoyconLeftRollLeft, bool JoyconLeftRollRight, double JoyconLeftAccelX, double JoyconLeftAccelY, double JoyconLeftGyroX, double JoyconLeftGyroY, double ProControllerLeftStickX, double ProControllerLeftStickY, double ProControllerRightStickX, double ProControllerRightStickY, bool ProControllerButtonSHOULDER_Left_1, bool ProControllerButtonSHOULDER_Left_2, bool ProControllerButtonDPAD_DOWN, bool ProControllerButtonDPAD_RIGHT, bool ProControllerButtonDPAD_UP, bool ProControllerButtonDPAD_LEFT, bool ProControllerButtonMINUS, bool ProControllerButtonCAPTURE, bool ProControllerButtonSTICK_Left, bool ProControllerButtonSHOULDER_Right_1, bool ProControllerButtonSHOULDER_Right_2, bool ProControllerButtonA, bool ProControllerButtonB, bool ProControllerButtonX, bool ProControllerButtonY, bool ProControllerButtonPLUS, bool ProControllerButtonHOME, bool ProControllerButtonSTICK_Right, double ProControllerAccelX, double ProControllerAccelY, double ProControllerGyroX, double ProControllerGyroY, double camx, double camy, bool Controller1ButtonAPressed, bool Controller2ButtonAPressed, bool Controller1ButtonBPressed, bool Controller2ButtonBPressed, bool Controller1ButtonXPressed, bool Controller2ButtonXPressed, bool Controller1ButtonYPressed, bool Controller2ButtonYPressed, bool Controller1ButtonStartPressed, bool Controller2ButtonStartPressed, bool Controller1ButtonBackPressed, bool Controller2ButtonBackPressed, bool Controller1ButtonDownPressed, bool Controller2ButtonDownPressed, bool Controller1ButtonUpPressed, bool Controller2ButtonUpPressed, bool Controller1ButtonLeftPressed, bool Controller2ButtonLeftPressed, bool Controller1ButtonRightPressed, bool Controller2ButtonRightPressed, bool Controller1ButtonShoulderLeftPressed, bool Controller2ButtonShoulderLeftPressed, bool Controller1ButtonShoulderRightPressed, bool Controller2ButtonShoulderRightPressed, bool Controller1ThumbpadLeftPressed, bool Controller2ThumbpadLeftPressed, bool Controller1ThumbpadRightPressed, bool Controller2ThumbpadRightPressed, double Controller1TriggerLeftPosition, double Controller2TriggerLeftPosition, double Controller1TriggerRightPosition, double Controller2TriggerRightPosition, double Controller1ThumbLeftX, double Controller2ThumbLeftX, double Controller1ThumbLeftY, double Controller2ThumbLeftY, double Controller1ThumbRightX, double Controller2ThumbRightX, double Controller1ThumbRightY, double Controller2ThumbRightY, int Joystick1AxisX, int Joystick1AxisY, int Joystick1AxisZ, int Joystick1RotationX, int Joystick1RotationY, int Joystick1RotationZ, int Joystick1Sliders0, int Joystick1Sliders1, int Joystick1PointOfViewControllers0, int Joystick1PointOfViewControllers1, int Joystick1PointOfViewControllers2, int Joystick1PointOfViewControllers3, int Joystick1VelocityX, int Joystick1VelocityY, int Joystick1VelocityZ, int Joystick1AngularVelocityX, int Joystick1AngularVelocityY, int Joystick1AngularVelocityZ, int Joystick1VelocitySliders0, int Joystick1VelocitySliders1, int Joystick1AccelerationX, int Joystick1AccelerationY, int Joystick1AccelerationZ, int Joystick1AngularAccelerationX, int Joystick1AngularAccelerationY, int Joystick1AngularAccelerationZ, int Joystick1AccelerationSliders0, int Joystick1AccelerationSliders1, int Joystick1ForceX, int Joystick1ForceY, int Joystick1ForceZ, int Joystick1TorqueX, int Joystick1TorqueY, int Joystick1TorqueZ, int Joystick1ForceSliders0, int Joystick1ForceSliders1, bool Joystick1Buttons0, bool Joystick1Buttons1, bool Joystick1Buttons2, bool Joystick1Buttons3, bool Joystick1Buttons4, bool Joystick1Buttons5, bool Joystick1Buttons6, bool Joystick1Buttons7, bool Joystick1Buttons8, bool Joystick1Buttons9, bool Joystick1Buttons10, bool Joystick1Buttons11, bool Joystick1Buttons12, bool Joystick1Buttons13, bool Joystick1Buttons14, bool Joystick1Buttons15, bool Joystick1Buttons16, bool Joystick1Buttons17, bool Joystick1Buttons18, bool Joystick1Buttons19, bool Joystick1Buttons20, bool Joystick1Buttons21, bool Joystick1Buttons22, bool Joystick1Buttons23, bool Joystick1Buttons24, bool Joystick1Buttons25, bool Joystick1Buttons26, bool Joystick1Buttons27, bool Joystick1Buttons28, bool Joystick1Buttons29, bool Joystick1Buttons30, bool Joystick1Buttons31, bool Joystick1Buttons32, bool Joystick1Buttons33, bool Joystick1Buttons34, bool Joystick1Buttons35, bool Joystick1Buttons36, bool Joystick1Buttons37, bool Joystick1Buttons38, bool Joystick1Buttons39, bool Joystick1Buttons40, bool Joystick1Buttons41, bool Joystick1Buttons42, bool Joystick1Buttons43, bool Joystick1Buttons44, bool Joystick1Buttons45, bool Joystick1Buttons46, bool Joystick1Buttons47, bool Joystick1Buttons48, bool Joystick1Buttons49, bool Joystick1Buttons50, bool Joystick1Buttons51, bool Joystick1Buttons52, bool Joystick1Buttons53, bool Joystick1Buttons54, bool Joystick1Buttons55, bool Joystick1Buttons56, bool Joystick1Buttons57, bool Joystick1Buttons58, bool Joystick1Buttons59, bool Joystick1Buttons60, bool Joystick1Buttons61, bool Joystick1Buttons62, bool Joystick1Buttons63, bool Joystick1Buttons64, bool Joystick1Buttons65, bool Joystick1Buttons66, bool Joystick1Buttons67, bool Joystick1Buttons68, bool Joystick1Buttons69, bool Joystick1Buttons70, bool Joystick1Buttons71, bool Joystick1Buttons72, bool Joystick1Buttons73, bool Joystick1Buttons74, bool Joystick1Buttons75, bool Joystick1Buttons76, bool Joystick1Buttons77, bool Joystick1Buttons78, bool Joystick1Buttons79, bool Joystick1Buttons80, bool Joystick1Buttons81, bool Joystick1Buttons82, bool Joystick1Buttons83, bool Joystick1Buttons84, bool Joystick1Buttons85, bool Joystick1Buttons86, bool Joystick1Buttons87, bool Joystick1Buttons88, bool Joystick1Buttons89, bool Joystick1Buttons90, bool Joystick1Buttons91, bool Joystick1Buttons92, bool Joystick1Buttons93, bool Joystick1Buttons94, bool Joystick1Buttons95, bool Joystick1Buttons96, bool Joystick1Buttons97, bool Joystick1Buttons98, bool Joystick1Buttons99, bool Joystick1Buttons100, bool Joystick1Buttons101, bool Joystick1Buttons102, bool Joystick1Buttons103, bool Joystick1Buttons104, bool Joystick1Buttons105, bool Joystick1Buttons106, bool Joystick1Buttons107, bool Joystick1Buttons108, bool Joystick1Buttons109, bool Joystick1Buttons110, bool Joystick1Buttons111, bool Joystick1Buttons112, bool Joystick1Buttons113, bool Joystick1Buttons114, bool Joystick1Buttons115, bool Joystick1Buttons116, bool Joystick1Buttons117, bool Joystick1Buttons118, bool Joystick1Buttons119, bool Joystick1Buttons120, bool Joystick1Buttons121, bool Joystick1Buttons122, bool Joystick1Buttons123, bool Joystick1Buttons124, bool Joystick1Buttons125, bool Joystick1Buttons126, bool Joystick1Buttons127, int Joystick2AxisX, int Joystick2AxisY, int Joystick2AxisZ, int Joystick2RotationX, int Joystick2RotationY, int Joystick2RotationZ, int Joystick2Sliders0, int Joystick2Sliders1, int Joystick2PointOfViewControllers0, int Joystick2PointOfViewControllers1, int Joystick2PointOfViewControllers2, int Joystick2PointOfViewControllers3, int Joystick2VelocityX, int Joystick2VelocityY, int Joystick2VelocityZ, int Joystick2AngularVelocityX, int Joystick2AngularVelocityY, int Joystick2AngularVelocityZ, int Joystick2VelocitySliders0, int Joystick2VelocitySliders1, int Joystick2AccelerationX, int Joystick2AccelerationY, int Joystick2AccelerationZ, int Joystick2AngularAccelerationX, int Joystick2AngularAccelerationY, int Joystick2AngularAccelerationZ, int Joystick2AccelerationSliders0, int Joystick2AccelerationSliders1, int Joystick2ForceX, int Joystick2ForceY, int Joystick2ForceZ, int Joystick2TorqueX, int Joystick2TorqueY, int Joystick2TorqueZ, int Joystick2ForceSliders0, int Joystick2ForceSliders1, bool Joystick2Buttons0, bool Joystick2Buttons1, bool Joystick2Buttons2, bool Joystick2Buttons3, bool Joystick2Buttons4, bool Joystick2Buttons5, bool Joystick2Buttons6, bool Joystick2Buttons7, bool Joystick2Buttons8, bool Joystick2Buttons9, bool Joystick2Buttons10, bool Joystick2Buttons11, bool Joystick2Buttons12, bool Joystick2Buttons13, bool Joystick2Buttons14, bool Joystick2Buttons15, bool Joystick2Buttons16, bool Joystick2Buttons17, bool Joystick2Buttons18, bool Joystick2Buttons19, bool Joystick2Buttons20, bool Joystick2Buttons21, bool Joystick2Buttons22, bool Joystick2Buttons23, bool Joystick2Buttons24, bool Joystick2Buttons25, bool Joystick2Buttons26, bool Joystick2Buttons27, bool Joystick2Buttons28, bool Joystick2Buttons29, bool Joystick2Buttons30, bool Joystick2Buttons31, bool Joystick2Buttons32, bool Joystick2Buttons33, bool Joystick2Buttons34, bool Joystick2Buttons35, bool Joystick2Buttons36, bool Joystick2Buttons37, bool Joystick2Buttons38, bool Joystick2Buttons39, bool Joystick2Buttons40, bool Joystick2Buttons41, bool Joystick2Buttons42, bool Joystick2Buttons43, bool Joystick2Buttons44, bool Joystick2Buttons45, bool Joystick2Buttons46, bool Joystick2Buttons47, bool Joystick2Buttons48, bool Joystick2Buttons49, bool Joystick2Buttons50, bool Joystick2Buttons51, bool Joystick2Buttons52, bool Joystick2Buttons53, bool Joystick2Buttons54, bool Joystick2Buttons55, bool Joystick2Buttons56, bool Joystick2Buttons57, bool Joystick2Buttons58, bool Joystick2Buttons59, bool Joystick2Buttons60, bool Joystick2Buttons61, bool Joystick2Buttons62, bool Joystick2Buttons63, bool Joystick2Buttons64, bool Joystick2Buttons65, bool Joystick2Buttons66, bool Joystick2Buttons67, bool Joystick2Buttons68, bool Joystick2Buttons69, bool Joystick2Buttons70, bool Joystick2Buttons71, bool Joystick2Buttons72, bool Joystick2Buttons73, bool Joystick2Buttons74, bool Joystick2Buttons75, bool Joystick2Buttons76, bool Joystick2Buttons77, bool Joystick2Buttons78, bool Joystick2Buttons79, bool Joystick2Buttons80, bool Joystick2Buttons81, bool Joystick2Buttons82, bool Joystick2Buttons83, bool Joystick2Buttons84, bool Joystick2Buttons85, bool Joystick2Buttons86, bool Joystick2Buttons87, bool Joystick2Buttons88, bool Joystick2Buttons89, bool Joystick2Buttons90, bool Joystick2Buttons91, bool Joystick2Buttons92, bool Joystick2Buttons93, bool Joystick2Buttons94, bool Joystick2Buttons95, bool Joystick2Buttons96, bool Joystick2Buttons97, bool Joystick2Buttons98, bool Joystick2Buttons99, bool Joystick2Buttons100, bool Joystick2Buttons101, bool Joystick2Buttons102, bool Joystick2Buttons103, bool Joystick2Buttons104, bool Joystick2Buttons105, bool Joystick2Buttons106, bool Joystick2Buttons107, bool Joystick2Buttons108, bool Joystick2Buttons109, bool Joystick2Buttons110, bool Joystick2Buttons111, bool Joystick2Buttons112, bool Joystick2Buttons113, bool Joystick2Buttons114, bool Joystick2Buttons115, bool Joystick2Buttons116, bool Joystick2Buttons117, bool Joystick2Buttons118, bool Joystick2Buttons119, bool Joystick2Buttons120, bool Joystick2Buttons121, bool Joystick2Buttons122, bool Joystick2Buttons123, bool Joystick2Buttons124, bool Joystick2Buttons125, bool Joystick2Buttons126, bool Joystick2Buttons127, bool Mouse1Buttons0, bool Mouse1Buttons1, bool Mouse1Buttons2, bool Mouse1Buttons3, bool Mouse1Buttons4, bool Mouse1Buttons5, bool Mouse1Buttons6, bool Mouse1Buttons7, int MouseHookX, int MouseHookY, int Mouse1AxisX, int Mouse1AxisY, int Mouse1AxisZ, bool Mouse2Buttons0, bool Mouse2Buttons1, bool Mouse2Buttons2, bool Mouse2Buttons3, bool Mouse2Buttons4, bool Mouse2Buttons5, bool Mouse2Buttons6, bool Mouse2Buttons7, int Mouse2AxisX, int Mouse2AxisY, int Mouse2AxisZ, bool Keyboard1KeyEscape, bool Keyboard1KeyD1, bool Keyboard1KeyD2, bool Keyboard1KeyD3, bool Keyboard1KeyD4, bool Keyboard1KeyD5, bool Keyboard1KeyD6, bool Keyboard1KeyD7, bool Keyboard1KeyD8, bool Keyboard1KeyD9, bool Keyboard1KeyD0, bool Keyboard1KeyMinus, bool Keyboard1KeyEquals, bool Keyboard1KeyBack, bool Keyboard1KeyTab, bool Keyboard1KeyQ, bool Keyboard1KeyW, bool Keyboard1KeyE, bool Keyboard1KeyR, bool Keyboard1KeyT, bool Keyboard1KeyY, bool Keyboard1KeyU, bool Keyboard1KeyI, bool Keyboard1KeyO, bool Keyboard1KeyP, bool Keyboard1KeyLeftBracket, bool Keyboard1KeyRightBracket, bool Keyboard1KeyReturn, bool Keyboard1KeyLeftControl, bool Keyboard1KeyA, bool Keyboard1KeyS, bool Keyboard1KeyD, bool Keyboard1KeyF, bool Keyboard1KeyG, bool Keyboard1KeyH, bool Keyboard1KeyJ, bool Keyboard1KeyK, bool Keyboard1KeyL, bool Keyboard1KeySemicolon, bool Keyboard1KeyApostrophe, bool Keyboard1KeyGrave, bool Keyboard1KeyLeftShift, bool Keyboard1KeyBackslash, bool Keyboard1KeyZ, bool Keyboard1KeyX, bool Keyboard1KeyC, bool Keyboard1KeyV, bool Keyboard1KeyB, bool Keyboard1KeyN, bool Keyboard1KeyM, bool Keyboard1KeyComma, bool Keyboard1KeyPeriod, bool Keyboard1KeySlash, bool Keyboard1KeyRightShift, bool Keyboard1KeyMultiply, bool Keyboard1KeyLeftAlt, bool Keyboard1KeySpace, bool Keyboard1KeyCapital, bool Keyboard1KeyF1, bool Keyboard1KeyF2, bool Keyboard1KeyF3, bool Keyboard1KeyF4, bool Keyboard1KeyF5, bool Keyboard1KeyF6, bool Keyboard1KeyF7, bool Keyboard1KeyF8, bool Keyboard1KeyF9, bool Keyboard1KeyF10, bool Keyboard1KeyNumberLock, bool Keyboard1KeyScrollLock, bool Keyboard1KeyNumberPad7, bool Keyboard1KeyNumberPad8, bool Keyboard1KeyNumberPad9, bool Keyboard1KeySubtract, bool Keyboard1KeyNumberPad4, bool Keyboard1KeyNumberPad5, bool Keyboard1KeyNumberPad6, bool Keyboard1KeyAdd, bool Keyboard1KeyNumberPad1, bool Keyboard1KeyNumberPad2, bool Keyboard1KeyNumberPad3, bool Keyboard1KeyNumberPad0, bool Keyboard1KeyDecimal, bool Keyboard1KeyOem102, bool Keyboard1KeyF11, bool Keyboard1KeyF12, bool Keyboard1KeyF13, bool Keyboard1KeyF14, bool Keyboard1KeyF15, bool Keyboard1KeyKana, bool Keyboard1KeyAbntC1, bool Keyboard1KeyConvert, bool Keyboard1KeyNoConvert, bool Keyboard1KeyYen, bool Keyboard1KeyAbntC2, bool Keyboard1KeyNumberPadEquals, bool Keyboard1KeyPreviousTrack, bool Keyboard1KeyAT, bool Keyboard1KeyColon, bool Keyboard1KeyUnderline, bool Keyboard1KeyKanji, bool Keyboard1KeyStop, bool Keyboard1KeyAX, bool Keyboard1KeyUnlabeled, bool Keyboard1KeyNextTrack, bool Keyboard1KeyNumberPadEnter, bool Keyboard1KeyRightControl, bool Keyboard1KeyMute, bool Keyboard1KeyCalculator, bool Keyboard1KeyPlayPause, bool Keyboard1KeyMediaStop, bool Keyboard1KeyVolumeDown, bool Keyboard1KeyVolumeUp, bool Keyboard1KeyWebHome, bool Keyboard1KeyNumberPadComma, bool Keyboard1KeyDivide, bool Keyboard1KeyPrintScreen, bool Keyboard1KeyRightAlt, bool Keyboard1KeyPause, bool Keyboard1KeyHome, bool Keyboard1KeyUp, bool Keyboard1KeyPageUp, bool Keyboard1KeyLeft, bool Keyboard1KeyRight, bool Keyboard1KeyEnd, bool Keyboard1KeyDown, bool Keyboard1KeyPageDown, bool Keyboard1KeyInsert, bool Keyboard1KeyDelete, bool Keyboard1KeyLeftWindowsKey, bool Keyboard1KeyRightWindowsKey, bool Keyboard1KeyApplications, bool Keyboard1KeyPower, bool Keyboard1KeySleep, bool Keyboard1KeyWake, bool Keyboard1KeyWebSearch, bool Keyboard1KeyWebFavorites, bool Keyboard1KeyWebRefresh, bool Keyboard1KeyWebStop, bool Keyboard1KeyWebForward, bool Keyboard1KeyWebBack, bool Keyboard1KeyMyComputer, bool Keyboard1KeyMail, bool Keyboard1KeyMediaSelect, bool Keyboard1KeyUnknown, bool Keyboard2KeyEscape, bool Keyboard2KeyD1, bool Keyboard2KeyD2, bool Keyboard2KeyD3, bool Keyboard2KeyD4, bool Keyboard2KeyD5, bool Keyboard2KeyD6, bool Keyboard2KeyD7, bool Keyboard2KeyD8, bool Keyboard2KeyD9, bool Keyboard2KeyD0, bool Keyboard2KeyMinus, bool Keyboard2KeyEquals, bool Keyboard2KeyBack, bool Keyboard2KeyTab, bool Keyboard2KeyQ, bool Keyboard2KeyW, bool Keyboard2KeyE, bool Keyboard2KeyR, bool Keyboard2KeyT, bool Keyboard2KeyY, bool Keyboard2KeyU, bool Keyboard2KeyI, bool Keyboard2KeyO, bool Keyboard2KeyP, bool Keyboard2KeyLeftBracket, bool Keyboard2KeyRightBracket, bool Keyboard2KeyReturn, bool Keyboard2KeyLeftControl, bool Keyboard2KeyA, bool Keyboard2KeyS, bool Keyboard2KeyD, bool Keyboard2KeyF, bool Keyboard2KeyG, bool Keyboard2KeyH, bool Keyboard2KeyJ, bool Keyboard2KeyK, bool Keyboard2KeyL, bool Keyboard2KeySemicolon, bool Keyboard2KeyApostrophe, bool Keyboard2KeyGrave, bool Keyboard2KeyLeftShift, bool Keyboard2KeyBackslash, bool Keyboard2KeyZ, bool Keyboard2KeyX, bool Keyboard2KeyC, bool Keyboard2KeyV, bool Keyboard2KeyB, bool Keyboard2KeyN, bool Keyboard2KeyM, bool Keyboard2KeyComma, bool Keyboard2KeyPeriod, bool Keyboard2KeySlash, bool Keyboard2KeyRightShift, bool Keyboard2KeyMultiply, bool Keyboard2KeyLeftAlt, bool Keyboard2KeySpace, bool Keyboard2KeyCapital, bool Keyboard2KeyF1, bool Keyboard2KeyF2, bool Keyboard2KeyF3, bool Keyboard2KeyF4, bool Keyboard2KeyF5, bool Keyboard2KeyF6, bool Keyboard2KeyF7, bool Keyboard2KeyF8, bool Keyboard2KeyF9, bool Keyboard2KeyF10, bool Keyboard2KeyNumberLock, bool Keyboard2KeyScrollLock, bool Keyboard2KeyNumberPad7, bool Keyboard2KeyNumberPad8, bool Keyboard2KeyNumberPad9, bool Keyboard2KeySubtract, bool Keyboard2KeyNumberPad4, bool Keyboard2KeyNumberPad5, bool Keyboard2KeyNumberPad6, bool Keyboard2KeyAdd, bool Keyboard2KeyNumberPad1, bool Keyboard2KeyNumberPad2, bool Keyboard2KeyNumberPad3, bool Keyboard2KeyNumberPad0, bool Keyboard2KeyDecimal, bool Keyboard2KeyOem102, bool Keyboard2KeyF11, bool Keyboard2KeyF12, bool Keyboard2KeyF13, bool Keyboard2KeyF14, bool Keyboard2KeyF15, bool Keyboard2KeyKana, bool Keyboard2KeyAbntC1, bool Keyboard2KeyConvert, bool Keyboard2KeyNoConvert, bool Keyboard2KeyYen, bool Keyboard2KeyAbntC2, bool Keyboard2KeyNumberPadEquals, bool Keyboard2KeyPreviousTrack, bool Keyboard2KeyAT, bool Keyboard2KeyColon, bool Keyboard2KeyUnderline, bool Keyboard2KeyKanji, bool Keyboard2KeyStop, bool Keyboard2KeyAX, bool Keyboard2KeyUnlabeled, bool Keyboard2KeyNextTrack, bool Keyboard2KeyNumberPadEnter, bool Keyboard2KeyRightControl, bool Keyboard2KeyMute, bool Keyboard2KeyCalculator, bool Keyboard2KeyPlayPause, bool Keyboard2KeyMediaStop, bool Keyboard2KeyVolumeDown, bool Keyboard2KeyVolumeUp, bool Keyboard2KeyWebHome, bool Keyboard2KeyNumberPadComma, bool Keyboard2KeyDivide, bool Keyboard2KeyPrintScreen, bool Keyboard2KeyRightAlt, bool Keyboard2KeyPause, bool Keyboard2KeyHome, bool Keyboard2KeyUp, bool Keyboard2KeyPageUp, bool Keyboard2KeyLeft, bool Keyboard2KeyRight, bool Keyboard2KeyEnd, bool Keyboard2KeyDown, bool Keyboard2KeyPageDown, bool Keyboard2KeyInsert, bool Keyboard2KeyDelete, bool Keyboard2KeyLeftWindowsKey, bool Keyboard2KeyRightWindowsKey, bool Keyboard2KeyApplications, bool Keyboard2KeyPower, bool Keyboard2KeySleep, bool Keyboard2KeyWake, bool Keyboard2KeyWebSearch, bool Keyboard2KeyWebFavorites, bool Keyboard2KeyWebRefresh, bool Keyboard2KeyWebStop, bool Keyboard2KeyWebForward, bool Keyboard2KeyWebBack, bool Keyboard2KeyMyComputer, bool Keyboard2KeyMail, bool Keyboard2KeyMediaSelect, bool Keyboard2KeyUnknown, string TextFromSpeech, double PS5ControllerLeftStickX, double PS5ControllerLeftStickY, double PS5ControllerRightStickX, double PS5ControllerRightStickY, double PS5ControllerLeftTriggerPosition, double PS5ControllerRightTriggerPosition, double PS5ControllerTouchX, double PS5ControllerTouchY, bool PS5ControllerTouchOn, double PS5ControllerGyroX, double PS5ControllerGyroY, double PS5ControllerAccelX, double PS5ControllerAccelY, bool PS5ControllerButtonCrossPressed, bool PS5ControllerButtonCirclePressed, bool PS5ControllerButtonSquarePressed, bool PS5ControllerButtonTrianglePressed, bool PS5ControllerButtonDPadUpPressed, bool PS5ControllerButtonDPadRightPressed, bool PS5ControllerButtonDPadDownPressed, bool PS5ControllerButtonDPadLeftPressed, bool PS5ControllerButtonL1Pressed, bool PS5ControllerButtonR1Pressed, bool PS5ControllerButtonL2Pressed, bool PS5ControllerButtonR2Pressed, bool PS5ControllerButtonL3Pressed, bool PS5ControllerButtonR3Pressed, bool PS5ControllerButtonCreatePressed, bool PS5ControllerButtonMenuPressed, bool PS5ControllerButtonLogoPressed, bool PS5ControllerButtonTouchpadPressed, bool PS5ControllerButtonMicPressed, double Controller1GyroX, double Controller1GyroY, double Controller2GyroX, double Controller2GyroY)
                        {
                            funct_driver
                            return new object[] { UsersAllowedList, sleeptime, KeyboardMouseDriverType, MouseMoveX, MouseMoveY, MouseAbsX, MouseAbsY, MouseDesktopX, MouseDesktopY, SendLeftClick, SendRightClick, SendMiddleClick, SendWheelUp, SendWheelDown, SendLeft, SendRight, SendUp, SendDown, SendLButton, SendRButton, SendCancel, SendMBUTTON, SendXBUTTON1, SendXBUTTON2, SendBack, SendTab, SendClear, SendReturn, SendSHIFT, SendCONTROL, SendMENU, SendPAUSE, SendCAPITAL, SendKANA, SendHANGEUL, SendHANGUL, SendJUNJA, SendFINAL, SendHANJA, SendKANJI, SendEscape, SendCONVERT, SendNONCONVERT, SendACCEPT, SendMODECHANGE, SendSpace, SendPRIOR, SendNEXT, SendEND, SendHOME, SendLEFT, SendUP, SendRIGHT, SendDOWN, SendSELECT, SendPRINT, SendEXECUTE, SendSNAPSHOT, SendINSERT, SendDELETE, SendHELP, SendAPOSTROPHE, Send0, Send1, Send2, Send3, Send4, Send5, Send6, Send7, Send8, Send9, SendA, SendB, SendC, SendD, SendE, SendF, SendG, SendH, SendI, SendJ, SendK, SendL, SendM, SendN, SendO, SendP, SendQ, SendR, SendS, SendT, SendU, SendV, SendW, SendX, SendY, SendZ, SendLWIN, SendRWIN, SendAPPS, SendSLEEP, SendNUMPAD0, SendNUMPAD1, SendNUMPAD2, SendNUMPAD3, SendNUMPAD4, SendNUMPAD5, SendNUMPAD6, SendNUMPAD7, SendNUMPAD8, SendNUMPAD9, SendMULTIPLY, SendADD, SendSEPARATOR, SendSUBTRACT, SendDECIMAL, SendDIVIDE, SendF1, SendF2, SendF3, SendF4, SendF5, SendF6, SendF7, SendF8, SendF9, SendF10, SendF11, SendF12, SendF13, SendF14, SendF15, SendF16, SendF17, SendF18, SendF19, SendF20, SendF21, SendF22, SendF23, SendF24, SendNUMLOCK, SendSCROLL, SendLeftShift, SendRightShift, SendLeftControl, SendRightControl, SendLMENU, SendRMENU, centery, irmode, SpeechToText, controller1_send_back, controller1_send_start, controller1_send_A, controller1_send_B, controller1_send_X, controller1_send_Y, controller1_send_up, controller1_send_left, controller1_send_down, controller1_send_right, controller1_send_leftstick, controller1_send_rightstick, controller1_send_leftbumper, controller1_send_rightbumper, controller1_send_lefttrigger, controller1_send_righttrigger, controller1_send_leftstickx, controller1_send_leftsticky, controller1_send_rightstickx, controller1_send_rightsticky, controller2_send_back, controller2_send_start, controller2_send_A, controller2_send_B, controller2_send_X, controller2_send_Y, controller2_send_up, controller2_send_left, controller2_send_down, controller2_send_right, controller2_send_leftstick, controller2_send_rightstick, controller2_send_leftbumper, controller2_send_rightbumper, controller2_send_lefttrigger, controller2_send_righttrigger, controller2_send_leftstickx, controller2_send_leftsticky, controller2_send_rightstickx, controller2_send_rightsticky, pollcount, keys12345, keys54321, getstate, mousexp, mouseyp, testdouble, testbool, EnableKM, EnableXC, EnableRI, EnableDI, EnableXI, EnableJI, EnablePI, JoyconLeftGyroCenter, JoyconRightGyroCenter, ProControllerGyroCenter, JoyconLeftAccelCenter, JoyconRightAccelCenter, ProControllerAccelCenter, gyromode, controller1_send_lefttriggerposition, controller1_send_righttriggerposition, controller2_send_lefttriggerposition, controller2_send_righttriggerposition, PS5ControllerGyroCenter, PS5ControllerAccelCenter, JoyconLeftStickCenter, JoyconRightStickCenter, ProControllerStickCenter, Controller1GyroCenter, Controller2GyroCenter };
                        }
                        public double Scale(double value, double min, double max, double minScale, double maxScale)
                        {
                            double scaled = minScale + (double)(value - min) / (max - min) * (maxScale - minScale);
                            return scaled;
                        }
                    }
                }";
        KeyboardHook keyboardHook = new KeyboardHook();
        MouseHook mouseHook = new MouseHook();
        DirectInput directInput = new DirectInput();
        public static int MouseHookX, MouseHookY;
        public static int vkCode, scanCode;
        public static bool KeyboardHookButtonDown, KeyboardHookButtonUp;
        public static System.Collections.Generic.List<double> time = new System.Collections.Generic.List<double>();
        private static string username;
        private static Ozeki.Media.Microphone microphone;
        private static Ozeki.Media.MediaConnector connector;
        private static Ozeki.Media.SpeechToText speechToText;
        private static string TextFromSpeech;
        private int pollcount = 0, keys12345 = 0, keys54321 = 0;
        private bool getstate = false, testbool = false;
        private double mousexp, mouseyp, testdouble;
        private static bool ps5controllerconneced = false, procontrollerconnected = false, wiimoteconnected = false, joyconleftconnected = false, joyconrightconnected = false, wiimotejoyconrightconnected = false, wiimotejoyconleftconnected = false, joyconsconnected = false, controllerconnected = false, gamepadconnected = false, mouseconnected = false, keyboardconnected = false, EnableXC = false, EnableKM = false, EnableRI = false, EnableDI = false, EnableXI = false, EnableJI = false, EnablePI = false;
        private string KeyboardMouseDriverType; double MouseMoveX; double MouseMoveY; double MouseAbsX; double MouseAbsY; double MouseDesktopX; double MouseDesktopY; bool SendLeftClick; bool SendRightClick; bool SendMiddleClick; bool SendWheelUp; bool SendWheelDown; bool SendLeft; bool SendRight; bool SendUp; bool SendDown; bool SendLButton; bool SendRButton; bool SendCancel; bool SendMBUTTON; bool SendXBUTTON1; bool SendXBUTTON2; bool SendBack; bool SendTab; bool SendClear; bool SendReturn; bool SendSHIFT; bool SendCONTROL; bool SendMENU; bool SendPAUSE; bool SendCAPITAL; bool SendKANA; bool SendHANGEUL; bool SendHANGUL; bool SendJUNJA; bool SendFINAL; bool SendHANJA; bool SendKANJI; bool SendEscape; bool SendCONVERT; bool SendNONCONVERT; bool SendACCEPT; bool SendMODECHANGE; bool SendSpace; bool SendPRIOR; bool SendNEXT; bool SendEND; bool SendHOME; bool SendLEFT; bool SendUP; bool SendRIGHT; bool SendDOWN; bool SendSELECT; bool SendPRINT; bool SendEXECUTE; bool SendSNAPSHOT; bool SendINSERT; bool SendDELETE; bool SendHELP; bool SendAPOSTROPHE; bool Send0; bool Send1; bool Send2; bool Send3; bool Send4; bool Send5; bool Send6; bool Send7; bool Send8; bool Send9; bool SendA; bool SendB; bool SendC; bool SendD; bool SendE; bool SendF; bool SendG; bool SendH; bool SendI; bool SendJ; bool SendK; bool SendL; bool SendM; bool SendN; bool SendO; bool SendP; bool SendQ; bool SendR; bool SendS; bool SendT; bool SendU; bool SendV; bool SendW; bool SendX; bool SendY; bool SendZ; bool SendLWIN; bool SendRWIN; bool SendAPPS; bool SendSLEEP; bool SendNUMPAD0; bool SendNUMPAD1; bool SendNUMPAD2; bool SendNUMPAD3; bool SendNUMPAD4; bool SendNUMPAD5; bool SendNUMPAD6; bool SendNUMPAD7; bool SendNUMPAD8; bool SendNUMPAD9; bool SendMULTIPLY; bool SendADD; bool SendSEPARATOR; bool SendSUBTRACT; bool SendDECIMAL; bool SendDIVIDE; bool SendF1; bool SendF2; bool SendF3; bool SendF4; bool SendF5; bool SendF6; bool SendF7; bool SendF8; bool SendF9; bool SendF10; bool SendF11; bool SendF12; bool SendF13; bool SendF14; bool SendF15; bool SendF16; bool SendF17; bool SendF18; bool SendF19; bool SendF20; bool SendF21; bool SendF22; bool SendF23; bool SendF24; bool SendNUMLOCK; bool SendSCROLL; bool SendLeftShift; bool SendRightShift; bool SendLeftControl; bool SendRightControl; bool SendLMENU; bool SendRMENU;
        private bool controller1_send_back; bool controller1_send_start; bool controller1_send_A; bool controller1_send_B; bool controller1_send_X; bool controller1_send_Y; bool controller1_send_up; bool controller1_send_left; bool controller1_send_down; bool controller1_send_right; bool controller1_send_leftstick; bool controller1_send_rightstick; bool controller1_send_leftbumper; bool controller1_send_rightbumper; bool controller1_send_lefttrigger; bool controller1_send_righttrigger; double controller1_send_leftstickx; double controller1_send_leftsticky; double controller1_send_rightstickx; double controller1_send_rightsticky; bool controller2_send_back; bool controller2_send_start; bool controller2_send_A; bool controller2_send_B; bool controller2_send_X; bool controller2_send_Y; bool controller2_send_up; bool controller2_send_left; bool controller2_send_down; bool controller2_send_right; bool controller2_send_leftstick; bool controller2_send_rightstick; bool controller2_send_leftbumper; bool controller2_send_rightbumper; bool controller2_send_lefttrigger; bool controller2_send_righttrigger; double controller2_send_leftstickx; double controller2_send_leftsticky; double controller2_send_rightstickx; double controller2_send_rightsticky; double controller1_send_lefttriggerposition; double controller1_send_righttriggerposition; double controller2_send_lefttriggerposition; double controller2_send_righttriggerposition;
        public static int[] wd = { 2 };
        public static int[] wu = { 2 };
        public static void valchanged(int n, bool val)
        {
            if (val)
            {
                if (wd[n] <= 1)
                {
                    wd[n] = wd[n] + 1;
                }
                wu[n] = 0;
            }
            else
            {
                if (wu[n] <= 1)
                {
                    wu[n] = wu[n] + 1;
                }
                wd[n] = 0;
            }
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "• Input Devices : Mouse (2), Keyboard (2), XInput Controller (2), DirectInput Controller (2), Joycons as DirectInput Controller (1), Pro Controller as DirectInput Controller (1), Wiimote and Sensor Bar and Nunchuck (1), Joycon left and Joycon right (1), Pro Controller, Dualsense Controller, Webcam and red Led (1), Voice Speech to Text (1).\n\r\n\r• Output Devices : Keyboard (2), Mouse (1), Xbox Controller (2).\n\r\n\r• Pairing : Use the dedicated buttons for Bluetooth Controllers.\n\r\n\r• Data : Script must running to see data values changing, but it's not the case for webcam.";
            const string caption = "Help";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "• Author: Michaël André Franiatte.\n\r\n\r• Copyrights: All rights reserved, no permissions granted.\n\r\n\r• Contact: michael.franiatte@gmail.com.";
            const string caption = "About";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ChangeScriptColors(object sender)
        {
            try
            {
                range = (sender as FastColoredTextBox).Range;
                range.SetStyle(InputStyle, new Regex(@"getstate"));
                range.SetStyle(InputStyle, new Regex(@"System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width"));
                range.SetStyle(InputStyle, new Regex(@"System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height"));
                range.SetStyle(InputStyle, new Regex(@"Math.Abs"));
                range.SetStyle(InputStyle, new Regex(@"Math.Pow"));
                range.SetStyle(InputStyle, new Regex(@"Math.Sign"));
                range.SetStyle(InputStyle, new Regex(@"wd"));
                range.SetStyle(InputStyle, new Regex(@"wu"));
                range.SetStyle(InputStyle, new Regex(@"valchanged"));
                range.SetStyle(InputStyle, new Regex(@"Scale"));
                range.SetStyle(InputStyle, new Regex(@"width"));
                range.SetStyle(InputStyle, new Regex(@"height"));
                range.SetStyle(InputStyle, new Regex(@"Key_LBUTTON"));
                range.SetStyle(InputStyle, new Regex(@"Key_RBUTTON"));
                range.SetStyle(InputStyle, new Regex(@"Key_CANCEL"));
                range.SetStyle(InputStyle, new Regex(@"Key_MBUTTON"));
                range.SetStyle(InputStyle, new Regex(@"Key_XBUTTON1"));
                range.SetStyle(InputStyle, new Regex(@"Key_XBUTTON2"));
                range.SetStyle(InputStyle, new Regex(@"Key_BACK"));
                range.SetStyle(InputStyle, new Regex(@"Key_Tab"));
                range.SetStyle(InputStyle, new Regex(@"Key_CLEAR"));
                range.SetStyle(InputStyle, new Regex(@"Key_Return"));
                range.SetStyle(InputStyle, new Regex(@"Key_SHIFT"));
                range.SetStyle(InputStyle, new Regex(@"Key_CONTROL"));
                range.SetStyle(InputStyle, new Regex(@"Key_MENU"));
                range.SetStyle(InputStyle, new Regex(@"Key_PAUSE"));
                range.SetStyle(InputStyle, new Regex(@"Key_CAPITAL"));
                range.SetStyle(InputStyle, new Regex(@"Key_KANA"));
                range.SetStyle(InputStyle, new Regex(@"Key_HANGEUL"));
                range.SetStyle(InputStyle, new Regex(@"Key_HANGUL"));
                range.SetStyle(InputStyle, new Regex(@"Key_JUNJA"));
                range.SetStyle(InputStyle, new Regex(@"Key_FINAL"));
                range.SetStyle(InputStyle, new Regex(@"Key_HANJA"));
                range.SetStyle(InputStyle, new Regex(@"Key_KANJI"));
                range.SetStyle(InputStyle, new Regex(@"Key_Escape"));
                range.SetStyle(InputStyle, new Regex(@"Key_CONVERT"));
                range.SetStyle(InputStyle, new Regex(@"Key_NONCONVERT"));
                range.SetStyle(InputStyle, new Regex(@"Key_ACCEPT"));
                range.SetStyle(InputStyle, new Regex(@"Key_MODECHANGE"));
                range.SetStyle(InputStyle, new Regex(@"Key_Space"));
                range.SetStyle(InputStyle, new Regex(@"Key_PRIOR"));
                range.SetStyle(InputStyle, new Regex(@"Key_NEXT"));
                range.SetStyle(InputStyle, new Regex(@"Key_END"));
                range.SetStyle(InputStyle, new Regex(@"Key_HOME"));
                range.SetStyle(InputStyle, new Regex(@"Key_LEFT"));
                range.SetStyle(InputStyle, new Regex(@"Key_UP"));
                range.SetStyle(InputStyle, new Regex(@"Key_RIGHT"));
                range.SetStyle(InputStyle, new Regex(@"Key_DOWN"));
                range.SetStyle(InputStyle, new Regex(@"Key_SELECT"));
                range.SetStyle(InputStyle, new Regex(@"Key_PRINT"));
                range.SetStyle(InputStyle, new Regex(@"Key_EXECUTE"));
                range.SetStyle(InputStyle, new Regex(@"Key_SNAPSHOT"));
                range.SetStyle(InputStyle, new Regex(@"Key_INSERT"));
                range.SetStyle(InputStyle, new Regex(@"Key_DELETE"));
                range.SetStyle(InputStyle, new Regex(@"Key_HELP"));
                range.SetStyle(InputStyle, new Regex(@"Key_APOSTROPHE"));
                range.SetStyle(InputStyle, new Regex(@"Key_0"));
                range.SetStyle(InputStyle, new Regex(@"Key_1"));
                range.SetStyle(InputStyle, new Regex(@"Key_2"));
                range.SetStyle(InputStyle, new Regex(@"Key_3"));
                range.SetStyle(InputStyle, new Regex(@"Key_4"));
                range.SetStyle(InputStyle, new Regex(@"Key_5"));
                range.SetStyle(InputStyle, new Regex(@"Key_6"));
                range.SetStyle(InputStyle, new Regex(@"Key_7"));
                range.SetStyle(InputStyle, new Regex(@"Key_8"));
                range.SetStyle(InputStyle, new Regex(@"Key_9"));
                range.SetStyle(InputStyle, new Regex(@"Key_A"));
                range.SetStyle(InputStyle, new Regex(@"Key_B"));
                range.SetStyle(InputStyle, new Regex(@"Key_C"));
                range.SetStyle(InputStyle, new Regex(@"Key_D"));
                range.SetStyle(InputStyle, new Regex(@"Key_E"));
                range.SetStyle(InputStyle, new Regex(@"Key_F"));
                range.SetStyle(InputStyle, new Regex(@"Key_G"));
                range.SetStyle(InputStyle, new Regex(@"Key_H"));
                range.SetStyle(InputStyle, new Regex(@"Key_I"));
                range.SetStyle(InputStyle, new Regex(@"Key_J"));
                range.SetStyle(InputStyle, new Regex(@"Key_K"));
                range.SetStyle(InputStyle, new Regex(@"Key_L"));
                range.SetStyle(InputStyle, new Regex(@"Key_M"));
                range.SetStyle(InputStyle, new Regex(@"Key_N"));
                range.SetStyle(InputStyle, new Regex(@"Key_O"));
                range.SetStyle(InputStyle, new Regex(@"Key_P"));
                range.SetStyle(InputStyle, new Regex(@"Key_Q"));
                range.SetStyle(InputStyle, new Regex(@"Key_R"));
                range.SetStyle(InputStyle, new Regex(@"Key_S"));
                range.SetStyle(InputStyle, new Regex(@"Key_T"));
                range.SetStyle(InputStyle, new Regex(@"Key_U"));
                range.SetStyle(InputStyle, new Regex(@"Key_V"));
                range.SetStyle(InputStyle, new Regex(@"Key_W"));
                range.SetStyle(InputStyle, new Regex(@"Key_X"));
                range.SetStyle(InputStyle, new Regex(@"Key_Y"));
                range.SetStyle(InputStyle, new Regex(@"Key_Z"));
                range.SetStyle(InputStyle, new Regex(@"Key_LWIN"));
                range.SetStyle(InputStyle, new Regex(@"Key_RWIN"));
                range.SetStyle(InputStyle, new Regex(@"Key_APPS"));
                range.SetStyle(InputStyle, new Regex(@"Key_SLEEP"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD0"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD1"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD2"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD3"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD4"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD5"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD6"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD7"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD8"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMPAD9"));
                range.SetStyle(InputStyle, new Regex(@"Key_MULTIPLY"));
                range.SetStyle(InputStyle, new Regex(@"Key_ADD"));
                range.SetStyle(InputStyle, new Regex(@"Key_SEPARATOR"));
                range.SetStyle(InputStyle, new Regex(@"Key_SUBTRACT"));
                range.SetStyle(InputStyle, new Regex(@"Key_DECIMAL"));
                range.SetStyle(InputStyle, new Regex(@"Key_DIVIDE"));
                range.SetStyle(InputStyle, new Regex(@"Key_F1"));
                range.SetStyle(InputStyle, new Regex(@"Key_F2"));
                range.SetStyle(InputStyle, new Regex(@"Key_F3"));
                range.SetStyle(InputStyle, new Regex(@"Key_F4"));
                range.SetStyle(InputStyle, new Regex(@"Key_F5"));
                range.SetStyle(InputStyle, new Regex(@"Key_F6"));
                range.SetStyle(InputStyle, new Regex(@"Key_F7"));
                range.SetStyle(InputStyle, new Regex(@"Key_F8"));
                range.SetStyle(InputStyle, new Regex(@"Key_F9"));
                range.SetStyle(InputStyle, new Regex(@"Key_F10"));
                range.SetStyle(InputStyle, new Regex(@"Key_F11"));
                range.SetStyle(InputStyle, new Regex(@"Key_F12"));
                range.SetStyle(InputStyle, new Regex(@"Key_F13"));
                range.SetStyle(InputStyle, new Regex(@"Key_F14"));
                range.SetStyle(InputStyle, new Regex(@"Key_F15"));
                range.SetStyle(InputStyle, new Regex(@"Key_F16"));
                range.SetStyle(InputStyle, new Regex(@"Key_F17"));
                range.SetStyle(InputStyle, new Regex(@"Key_F18"));
                range.SetStyle(InputStyle, new Regex(@"Key_F19"));
                range.SetStyle(InputStyle, new Regex(@"Key_F20"));
                range.SetStyle(InputStyle, new Regex(@"Key_F21"));
                range.SetStyle(InputStyle, new Regex(@"Key_F22"));
                range.SetStyle(InputStyle, new Regex(@"Key_F23"));
                range.SetStyle(InputStyle, new Regex(@"Key_F24"));
                range.SetStyle(InputStyle, new Regex(@"Key_NUMLOCK"));
                range.SetStyle(InputStyle, new Regex(@"Key_SCROLL"));
                range.SetStyle(InputStyle, new Regex(@"Key_LeftShift"));
                range.SetStyle(InputStyle, new Regex(@"Key_RightShift"));
                range.SetStyle(InputStyle, new Regex(@"Key_LeftControl"));
                range.SetStyle(InputStyle, new Regex(@"Key_RightControl"));
                range.SetStyle(InputStyle, new Regex(@"Key_LMENU"));
                range.SetStyle(InputStyle, new Regex(@"Key_RMENU"));
                range.SetStyle(InputStyle, new Regex(@"Key_BROWSER_BACK"));
                range.SetStyle(InputStyle, new Regex(@"Key_BROWSER_FORWARD"));
                range.SetStyle(InputStyle, new Regex(@"Key_BROWSER_REFRESH"));
                range.SetStyle(InputStyle, new Regex(@"Key_BROWSER_STOP"));
                range.SetStyle(InputStyle, new Regex(@"Key_BROWSER_SEARCH"));
                range.SetStyle(InputStyle, new Regex(@"Key_BROWSER_FAVORITES"));
                range.SetStyle(InputStyle, new Regex(@"Key_BROWSER_HOME"));
                range.SetStyle(InputStyle, new Regex(@"Key_VOLUME_MUTE"));
                range.SetStyle(InputStyle, new Regex(@"Key_VOLUME_DOWN"));
                range.SetStyle(InputStyle, new Regex(@"Key_VOLUME_UP"));
                range.SetStyle(InputStyle, new Regex(@"Key_MEDIA_NEXT_TRACK"));
                range.SetStyle(InputStyle, new Regex(@"Key_MEDIA_PREV_TRACK"));
                range.SetStyle(InputStyle, new Regex(@"Key_MEDIA_STOP"));
                range.SetStyle(InputStyle, new Regex(@"Key_MEDIA_PLAY_PAUSE"));
                range.SetStyle(InputStyle, new Regex(@"Key_LAUNCH_MAIL"));
                range.SetStyle(InputStyle, new Regex(@"Key_LAUNCH_MEDIA_SELECT"));
                range.SetStyle(InputStyle, new Regex(@"Key_LAUNCH_APP1"));
                range.SetStyle(InputStyle, new Regex(@"Key_LAUNCH_APP2"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_1"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_PLUS"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_COMMA"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_MINUS"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_PERIOD"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_2"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_3"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_4"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_5"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_6"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_7"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_8"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_102"));
                range.SetStyle(InputStyle, new Regex(@"Key_PROCESSKEY"));
                range.SetStyle(InputStyle, new Regex(@"Key_PACKET"));
                range.SetStyle(InputStyle, new Regex(@"Key_ATTN"));
                range.SetStyle(InputStyle, new Regex(@"Key_CRSEL"));
                range.SetStyle(InputStyle, new Regex(@"Key_EXSEL"));
                range.SetStyle(InputStyle, new Regex(@"Key_EREOF"));
                range.SetStyle(InputStyle, new Regex(@"Key_PLAY"));
                range.SetStyle(InputStyle, new Regex(@"Key_ZOOM"));
                range.SetStyle(InputStyle, new Regex(@"Key_NONAME"));
                range.SetStyle(InputStyle, new Regex(@"Key_PA1"));
                range.SetStyle(InputStyle, new Regex(@"Key_OEM_CLEAR"));
                range.SetStyle(InputStyle, new Regex(@"irx"));
                range.SetStyle(InputStyle, new Regex(@"iry"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateA"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateB"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateMinus"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateHome"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStatePlus"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateOne"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateTwo"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateUp"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateDown"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateLeft"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteButtonStateRight"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteRawValuesX"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteRawValuesY"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteRawValuesZ"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteNunchuckStateRawJoystickX"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteNunchuckStateRawJoystickY"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteNunchuckStateRawValuesX"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteNunchuckStateRawValuesY"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteNunchuckStateRawValuesZ"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteNunchuckStateC"));
                range.SetStyle(InputStyle, new Regex(@"WiimoteNunchuckStateZ"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightStickX"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightStickY"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonSHOULDER_1"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonSHOULDER_2"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonSR"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonSL"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonDPAD_DOWN"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonDPAD_RIGHT"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonDPAD_UP"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonDPAD_LEFT"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonPLUS"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonHOME"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonSTICK"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonACC"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightButtonSPA"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightRollLeft"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightRollRight"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightAccelX"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightAccelY"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightGyroX"));
                range.SetStyle(InputStyle, new Regex(@"JoyconRightGyroY"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftStickX"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftStickY"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonSHOULDER_1"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonSHOULDER_2"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonSR"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonSL"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonDPAD_DOWN"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonDPAD_RIGHT"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonDPAD_UP"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonDPAD_LEFT"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonMINUS"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonCAPTURE"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonSTICK"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonACC"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftButtonSMA"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftRollLeft"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftRollRight"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftAccelX"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftAccelY"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftGyroX"));
                range.SetStyle(InputStyle, new Regex(@"JoyconLeftGyroY"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerLeftStickX"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerLeftStickY"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerRightStickX"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerRightStickY"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonSHOULDER_Left_1"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonSHOULDER_Left_2"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonDPAD_DOWN"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonDPAD_RIGHT"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonDPAD_UP"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonDPAD_LEFT"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonMINUS"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonCAPTURE"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonSTICK_Left"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonSHOULDER_Right_1"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonSHOULDER_Right_2"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonA"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonB"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonX"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonY"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonPLUS"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonHOME"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerButtonSTICK_Right"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerAccelX"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerAccelY"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerGyroX"));
                range.SetStyle(InputStyle, new Regex(@"ProControllerGyroY"));
                range.SetStyle(InputStyle, new Regex(@"camx"));
                range.SetStyle(InputStyle, new Regex(@"camy"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonAPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonAPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonBPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonBPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonXPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonXPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonYPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonYPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonStartPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonStartPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonBackPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonBackPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonDownPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonDownPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonUpPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonUpPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonLeftPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonLeftPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonRightPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonRightPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonShoulderLeftPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonShoulderLeftPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ButtonShoulderRightPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ButtonShoulderRightPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ThumbpadLeftPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ThumbpadLeftPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ThumbpadRightPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ThumbpadRightPressed"));
                range.SetStyle(InputStyle, new Regex(@"Controller1TriggerLeftPosition"));
                range.SetStyle(InputStyle, new Regex(@"Controller2TriggerLeftPosition"));
                range.SetStyle(InputStyle, new Regex(@"Controller1TriggerRightPosition"));
                range.SetStyle(InputStyle, new Regex(@"Controller2TriggerRightPosition"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ThumbLeftX"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ThumbLeftX"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ThumbLeftY"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ThumbLeftY"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ThumbRightX"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ThumbRightX"));
                range.SetStyle(InputStyle, new Regex(@"Controller1ThumbRightY"));
                range.SetStyle(InputStyle, new Regex(@"Controller2ThumbRightY"));
                range.SetStyle(InputStyle, new Regex(@"Controller1GyroX"));
                range.SetStyle(InputStyle, new Regex(@"Controller1GyroY"));
                range.SetStyle(InputStyle, new Regex(@"Controller2GyroX"));
                range.SetStyle(InputStyle, new Regex(@"Controller2GyroY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AxisX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AxisY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AxisZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1RotationX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1RotationY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1RotationZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Sliders0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Sliders1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1PointOfViewControllers0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1PointOfViewControllers1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1PointOfViewControllers2"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1PointOfViewControllers3"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1VelocityX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1VelocityY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1VelocityZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AngularVelocityX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AngularVelocityY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AngularVelocityZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1VelocitySliders0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1VelocitySliders1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AccelerationX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AccelerationY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AccelerationZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AngularAccelerationX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AngularAccelerationY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AngularAccelerationZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AccelerationSliders0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1AccelerationSliders1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1ForceX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1ForceY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1ForceZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1TorqueX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1TorqueY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1TorqueZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1ForceSliders0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1ForceSliders1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons2"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons3"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons4"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons5"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons6"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons7"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons8"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons9"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons10"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons11"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons12"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons13"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons14"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons15"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons16"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons17"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons18"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons19"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons20"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons21"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons22"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons23"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons24"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons25"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons26"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons27"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons28"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons29"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons30"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons31"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons32"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons33"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons34"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons35"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons36"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons37"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons38"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons39"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons40"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons41"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons42"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons43"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons44"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons45"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons46"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons47"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons48"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons49"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons50"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons51"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons52"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons53"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons54"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons55"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons56"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons57"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons58"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons59"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons60"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons61"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons62"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons63"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons64"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons65"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons66"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons67"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons68"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons69"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons70"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons71"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons72"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons73"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons74"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons75"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons76"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons77"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons78"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons79"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons80"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons81"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons82"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons83"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons84"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons85"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons86"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons87"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons88"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons89"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons90"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons91"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons92"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons93"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons94"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons95"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons96"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons97"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons98"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons99"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons100"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons101"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons102"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons103"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons104"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons105"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons106"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons107"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons108"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons109"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons110"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons111"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons112"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons113"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons114"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons115"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons116"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons117"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons118"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons119"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons120"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons121"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons122"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons123"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons124"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons125"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons126"));
                range.SetStyle(InputStyle, new Regex(@"Joystick1Buttons127"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AxisX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AxisY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AxisZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2RotationX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2RotationY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2RotationZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Sliders0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Sliders1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2PointOfViewControllers0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2PointOfViewControllers1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2PointOfViewControllers2"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2PointOfViewControllers3"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2VelocityX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2VelocityY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2VelocityZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AngularVelocityX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AngularVelocityY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AngularVelocityZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2VelocitySliders0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2VelocitySliders1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AccelerationX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AccelerationY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AccelerationZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AngularAccelerationX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AngularAccelerationY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AngularAccelerationZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AccelerationSliders0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2AccelerationSliders1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2ForceX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2ForceY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2ForceZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2TorqueX"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2TorqueY"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2TorqueZ"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2ForceSliders0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2ForceSliders1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons0"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons1"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons2"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons3"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons4"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons5"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons6"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons7"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons8"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons9"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons10"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons11"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons12"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons13"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons14"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons15"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons16"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons17"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons18"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons19"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons20"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons21"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons22"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons23"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons24"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons25"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons26"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons27"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons28"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons29"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons30"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons31"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons32"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons33"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons34"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons35"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons36"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons37"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons38"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons39"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons40"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons41"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons42"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons43"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons44"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons45"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons46"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons47"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons48"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons49"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons50"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons51"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons52"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons53"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons54"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons55"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons56"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons57"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons58"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons59"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons60"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons61"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons62"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons63"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons64"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons65"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons66"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons67"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons68"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons69"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons70"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons71"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons72"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons73"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons74"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons75"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons76"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons77"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons78"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons79"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons80"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons81"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons82"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons83"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons84"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons85"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons86"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons87"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons88"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons89"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons90"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons91"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons92"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons93"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons94"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons95"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons96"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons97"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons98"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons99"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons100"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons101"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons102"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons103"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons104"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons105"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons106"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons107"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons108"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons109"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons110"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons111"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons112"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons113"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons114"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons115"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons116"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons117"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons118"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons119"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons120"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons121"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons122"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons123"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons124"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons125"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons126"));
                range.SetStyle(InputStyle, new Regex(@"Joystick2Buttons127"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1Buttons0"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1Buttons1"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1Buttons2"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1Buttons3"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1Buttons4"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1Buttons5"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1Buttons6"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1Buttons7"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1AxisX"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1AxisY"));
                range.SetStyle(InputStyle, new Regex(@"Mouse1AxisZ"));
                range.SetStyle(InputStyle, new Regex(@"MouseHookX"));
                range.SetStyle(InputStyle, new Regex(@"MouseHookY"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2Buttons0"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2Buttons1"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2Buttons2"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2Buttons3"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2Buttons4"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2Buttons5"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2Buttons6"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2Buttons7"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2AxisX"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2AxisY"));
                range.SetStyle(InputStyle, new Regex(@"Mouse2AxisZ"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyEscape"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD1"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD2"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD3"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD4"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD5"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD6"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD7"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD8"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD9"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD0"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyMinus"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyEquals"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyBack"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyTab"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyQ"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyW"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyE"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyR"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyT"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyY"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyU"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyI"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyO"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyP"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyLeftBracket"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyRightBracket"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyReturn"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyLeftControl"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyA"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyS"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyD"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyG"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyH"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyJ"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyK"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyL"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeySemicolon"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyApostrophe"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyGrave"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyLeftShift"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyBackslash"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyZ"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyX"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyC"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyV"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyB"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyN"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyM"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyComma"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyPeriod"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeySlash"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyRightShift"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyMultiply"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyLeftAlt"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeySpace"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyCapital"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF1"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF2"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF3"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF4"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF5"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF6"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF7"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF8"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF9"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF10"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberLock"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyScrollLock"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad7"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad8"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad9"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeySubtract"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad4"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad5"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad6"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyAdd"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad1"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad2"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad3"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPad0"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyDecimal"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyOem102"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF11"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF12"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF13"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF14"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyF15"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyKana"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyAbntC1"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyConvert"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNoConvert"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyYen"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyAbntC2"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPadEquals"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyPreviousTrack"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyAT"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyColon"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyUnderline"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyKanji"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyStop"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyAX"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyUnlabeled"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNextTrack"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPadEnter"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyRightControl"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyMute"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyCalculator"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyPlayPause"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyMediaStop"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyVolumeDown"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyVolumeUp"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyWebHome"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyNumberPadComma"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyDivide"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyPrintScreen"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyRightAlt"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyPause"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyHome"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyUp"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyPageUp"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyLeft"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyRight"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyEnd"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyDown"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyPageDown"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyInsert"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyDelete"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyLeftWindowsKey"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyRightWindowsKey"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyApplications"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyPower"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeySleep"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyWake"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyWebSearch"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyWebFavorites"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyWebRefresh"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyWebStop"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyWebForward"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyWebBack"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyMyComputer"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyMail"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyMediaSelect"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard1KeyUnknown"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyEscape"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD1"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD2"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD3"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD4"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD5"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD6"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD7"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD8"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD9"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD0"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyMinus"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyEquals"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyBack"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyTab"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyQ"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyW"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyE"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyR"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyT"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyY"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyU"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyI"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyO"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyP"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyLeftBracket"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyRightBracket"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyReturn"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyLeftControl"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyA"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyS"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyD"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyG"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyH"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyJ"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyK"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyL"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeySemicolon"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyApostrophe"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyGrave"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyLeftShift"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyBackslash"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyZ"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyX"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyC"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyV"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyB"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyN"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyM"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyComma"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyPeriod"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeySlash"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyRightShift"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyMultiply"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyLeftAlt"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeySpace"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyCapital"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF1"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF2"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF3"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF4"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF5"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF6"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF7"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF8"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF9"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF10"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberLock"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyScrollLock"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad7"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad8"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad9"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeySubtract"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad4"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad5"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad6"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyAdd"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad1"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad2"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad3"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPad0"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyDecimal"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyOem102"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF11"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF12"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF13"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF14"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyF15"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyKana"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyAbntC1"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyConvert"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNoConvert"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyYen"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyAbntC2"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPadEquals"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyPreviousTrack"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyAT"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyColon"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyUnderline"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyKanji"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyStop"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyAX"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyUnlabeled"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNextTrack"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPadEnter"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyRightControl"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyMute"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyCalculator"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyPlayPause"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyMediaStop"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyVolumeDown"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyVolumeUp"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyWebHome"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyNumberPadComma"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyDivide"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyPrintScreen"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyRightAlt"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyPause"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyHome"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyUp"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyPageUp"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyLeft"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyRight"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyEnd"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyDown"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyPageDown"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyInsert"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyDelete"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyLeftWindowsKey"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyRightWindowsKey"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyApplications"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyPower"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeySleep"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyWake"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyWebSearch"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyWebFavorites"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyWebRefresh"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyWebStop"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyWebForward"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyWebBack"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyMyComputer"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyMail"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyMediaSelect"));
                range.SetStyle(InputStyle, new Regex(@"Keyboard2KeyUnknown"));
                range.SetStyle(InputStyle, new Regex(@"TextFromSpeech"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerLeftStickX"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerLeftStickY"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerRightStickX"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerRightStickY"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerLeftTriggerPosition"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerRightTriggerPosition"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerTouchX"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerTouchY"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerTouchOn"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerGyroX"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerGyroY"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerAccelX"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerAccelY"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonCrossPressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonCirclePressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonSquarePressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonTrianglePressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonDPadUpPressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonDPadRightPressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonDPadDownPressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonDPadLeftPressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonL1Pressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonR1Pressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonL2Pressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonR2Pressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonL3Pressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonR3Pressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonCreatePressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonMenuPressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonLogoPressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonTouchpadPressed"));
                range.SetStyle(InputStyle, new Regex(@"PS5ControllerButtonMicPressed"));
                range.SetStyle(OutputStyle, new Regex(@"UsersAllowedList"));
                range.SetStyle(OutputStyle, new Regex(@"sleeptime"));
                range.SetStyle(OutputStyle, new Regex(@"KeyboardMouseDriverType"));
                range.SetStyle(OutputStyle, new Regex(@"MouseMoveX"));
                range.SetStyle(OutputStyle, new Regex(@"MouseMoveY"));
                range.SetStyle(OutputStyle, new Regex(@"MouseAbsX"));
                range.SetStyle(OutputStyle, new Regex(@"MouseAbsY"));
                range.SetStyle(OutputStyle, new Regex(@"MouseDesktopX"));
                range.SetStyle(OutputStyle, new Regex(@"MouseDesktopY"));
                range.SetStyle(OutputStyle, new Regex(@"SendLeftClick"));
                range.SetStyle(OutputStyle, new Regex(@"SendRightClick"));
                range.SetStyle(OutputStyle, new Regex(@"SendMiddleClick"));
                range.SetStyle(OutputStyle, new Regex(@"SendWheelUp"));
                range.SetStyle(OutputStyle, new Regex(@"SendWheelDown"));
                range.SetStyle(OutputStyle, new Regex(@"SendLeft"));
                range.SetStyle(OutputStyle, new Regex(@"SendRight"));
                range.SetStyle(OutputStyle, new Regex(@"SendUp"));
                range.SetStyle(OutputStyle, new Regex(@"SendDown"));
                range.SetStyle(OutputStyle, new Regex(@"SendLButton"));
                range.SetStyle(OutputStyle, new Regex(@"SendRButton"));
                range.SetStyle(OutputStyle, new Regex(@"SendCancel"));
                range.SetStyle(OutputStyle, new Regex(@"SendMBUTTON"));
                range.SetStyle(OutputStyle, new Regex(@"SendXBUTTON1"));
                range.SetStyle(OutputStyle, new Regex(@"SendXBUTTON2"));
                range.SetStyle(OutputStyle, new Regex(@"SendBack"));
                range.SetStyle(OutputStyle, new Regex(@"SendTab"));
                range.SetStyle(OutputStyle, new Regex(@"SendClear"));
                range.SetStyle(OutputStyle, new Regex(@"SendReturn"));
                range.SetStyle(OutputStyle, new Regex(@"SendSHIFT"));
                range.SetStyle(OutputStyle, new Regex(@"SendCONTROL"));
                range.SetStyle(OutputStyle, new Regex(@"SendMENU"));
                range.SetStyle(OutputStyle, new Regex(@"SendPAUSE"));
                range.SetStyle(OutputStyle, new Regex(@"SendCAPITAL"));
                range.SetStyle(OutputStyle, new Regex(@"SendKANA"));
                range.SetStyle(OutputStyle, new Regex(@"SendHANGEUL"));
                range.SetStyle(OutputStyle, new Regex(@"SendHANGUL"));
                range.SetStyle(OutputStyle, new Regex(@"SendJUNJA"));
                range.SetStyle(OutputStyle, new Regex(@"SendFINAL"));
                range.SetStyle(OutputStyle, new Regex(@"SendHANJA"));
                range.SetStyle(OutputStyle, new Regex(@"SendKANJI"));
                range.SetStyle(OutputStyle, new Regex(@"SendEscape"));
                range.SetStyle(OutputStyle, new Regex(@"SendCONVERT"));
                range.SetStyle(OutputStyle, new Regex(@"SendNONCONVERT"));
                range.SetStyle(OutputStyle, new Regex(@"SendACCEPT"));
                range.SetStyle(OutputStyle, new Regex(@"SendMODECHANGE"));
                range.SetStyle(OutputStyle, new Regex(@"SendSpace"));
                range.SetStyle(OutputStyle, new Regex(@"SendPRIOR"));
                range.SetStyle(OutputStyle, new Regex(@"SendNEXT"));
                range.SetStyle(OutputStyle, new Regex(@"SendEND"));
                range.SetStyle(OutputStyle, new Regex(@"SendHOME"));
                range.SetStyle(OutputStyle, new Regex(@"SendLEFT"));
                range.SetStyle(OutputStyle, new Regex(@"SendUP"));
                range.SetStyle(OutputStyle, new Regex(@"SendRIGHT"));
                range.SetStyle(OutputStyle, new Regex(@"SendDOWN"));
                range.SetStyle(OutputStyle, new Regex(@"SendSELECT"));
                range.SetStyle(OutputStyle, new Regex(@"SendPRINT"));
                range.SetStyle(OutputStyle, new Regex(@"SendEXECUTE"));
                range.SetStyle(OutputStyle, new Regex(@"SendSNAPSHOT"));
                range.SetStyle(OutputStyle, new Regex(@"SendINSERT"));
                range.SetStyle(OutputStyle, new Regex(@"SendDELETE"));
                range.SetStyle(OutputStyle, new Regex(@"SendHELP"));
                range.SetStyle(OutputStyle, new Regex(@"SendAPOSTROPHE"));
                range.SetStyle(OutputStyle, new Regex(@"Send0"));
                range.SetStyle(OutputStyle, new Regex(@"Send1"));
                range.SetStyle(OutputStyle, new Regex(@"Send2"));
                range.SetStyle(OutputStyle, new Regex(@"Send3"));
                range.SetStyle(OutputStyle, new Regex(@"Send4"));
                range.SetStyle(OutputStyle, new Regex(@"Send5"));
                range.SetStyle(OutputStyle, new Regex(@"Send6"));
                range.SetStyle(OutputStyle, new Regex(@"Send7"));
                range.SetStyle(OutputStyle, new Regex(@"Send8"));
                range.SetStyle(OutputStyle, new Regex(@"Send9"));
                range.SetStyle(OutputStyle, new Regex(@"SendA"));
                range.SetStyle(OutputStyle, new Regex(@"SendB"));
                range.SetStyle(OutputStyle, new Regex(@"SendC"));
                range.SetStyle(OutputStyle, new Regex(@"SendD"));
                range.SetStyle(OutputStyle, new Regex(@"SendE"));
                range.SetStyle(OutputStyle, new Regex(@"SendF"));
                range.SetStyle(OutputStyle, new Regex(@"SendG"));
                range.SetStyle(OutputStyle, new Regex(@"SendH"));
                range.SetStyle(OutputStyle, new Regex(@"SendI"));
                range.SetStyle(OutputStyle, new Regex(@"SendJ"));
                range.SetStyle(OutputStyle, new Regex(@"SendK"));
                range.SetStyle(OutputStyle, new Regex(@"SendL"));
                range.SetStyle(OutputStyle, new Regex(@"SendM"));
                range.SetStyle(OutputStyle, new Regex(@"SendN"));
                range.SetStyle(OutputStyle, new Regex(@"SendO"));
                range.SetStyle(OutputStyle, new Regex(@"SendP"));
                range.SetStyle(OutputStyle, new Regex(@"SendQ"));
                range.SetStyle(OutputStyle, new Regex(@"SendR"));
                range.SetStyle(OutputStyle, new Regex(@"SendS"));
                range.SetStyle(OutputStyle, new Regex(@"SendT"));
                range.SetStyle(OutputStyle, new Regex(@"SendU"));
                range.SetStyle(OutputStyle, new Regex(@"SendV"));
                range.SetStyle(OutputStyle, new Regex(@"SendW"));
                range.SetStyle(OutputStyle, new Regex(@"SendX"));
                range.SetStyle(OutputStyle, new Regex(@"SendY"));
                range.SetStyle(OutputStyle, new Regex(@"SendZ"));
                range.SetStyle(OutputStyle, new Regex(@"SendLWIN"));
                range.SetStyle(OutputStyle, new Regex(@"SendRWIN"));
                range.SetStyle(OutputStyle, new Regex(@"SendAPPS"));
                range.SetStyle(OutputStyle, new Regex(@"SendSLEEP"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD0"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD1"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD2"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD3"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD4"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD5"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD6"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD7"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD8"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMPAD9"));
                range.SetStyle(OutputStyle, new Regex(@"SendMULTIPLY"));
                range.SetStyle(OutputStyle, new Regex(@"SendADD"));
                range.SetStyle(OutputStyle, new Regex(@"SendSEPARATOR"));
                range.SetStyle(OutputStyle, new Regex(@"SendSUBTRACT"));
                range.SetStyle(OutputStyle, new Regex(@"SendDECIMAL"));
                range.SetStyle(OutputStyle, new Regex(@"SendDIVIDE"));
                range.SetStyle(OutputStyle, new Regex(@"SendF1"));
                range.SetStyle(OutputStyle, new Regex(@"SendF2"));
                range.SetStyle(OutputStyle, new Regex(@"SendF3"));
                range.SetStyle(OutputStyle, new Regex(@"SendF4"));
                range.SetStyle(OutputStyle, new Regex(@"SendF5"));
                range.SetStyle(OutputStyle, new Regex(@"SendF6"));
                range.SetStyle(OutputStyle, new Regex(@"SendF7"));
                range.SetStyle(OutputStyle, new Regex(@"SendF8"));
                range.SetStyle(OutputStyle, new Regex(@"SendF9"));
                range.SetStyle(OutputStyle, new Regex(@"SendF10"));
                range.SetStyle(OutputStyle, new Regex(@"SendF11"));
                range.SetStyle(OutputStyle, new Regex(@"SendF12"));
                range.SetStyle(OutputStyle, new Regex(@"SendF13"));
                range.SetStyle(OutputStyle, new Regex(@"SendF14"));
                range.SetStyle(OutputStyle, new Regex(@"SendF15"));
                range.SetStyle(OutputStyle, new Regex(@"SendF16"));
                range.SetStyle(OutputStyle, new Regex(@"SendF17"));
                range.SetStyle(OutputStyle, new Regex(@"SendF18"));
                range.SetStyle(OutputStyle, new Regex(@"SendF19"));
                range.SetStyle(OutputStyle, new Regex(@"SendF20"));
                range.SetStyle(OutputStyle, new Regex(@"SendF21"));
                range.SetStyle(OutputStyle, new Regex(@"SendF22"));
                range.SetStyle(OutputStyle, new Regex(@"SendF23"));
                range.SetStyle(OutputStyle, new Regex(@"SendF24"));
                range.SetStyle(OutputStyle, new Regex(@"SendNUMLOCK"));
                range.SetStyle(OutputStyle, new Regex(@"SendSCROLL"));
                range.SetStyle(OutputStyle, new Regex(@"SendLeftShift"));
                range.SetStyle(OutputStyle, new Regex(@"SendRightShift"));
                range.SetStyle(OutputStyle, new Regex(@"SendLeftControl"));
                range.SetStyle(OutputStyle, new Regex(@"SendRightControl"));
                range.SetStyle(OutputStyle, new Regex(@"SendLMENU"));
                range.SetStyle(OutputStyle, new Regex(@"SendRMENU"));
                range.SetStyle(OutputStyle, new Regex(@"centery"));
                range.SetStyle(OutputStyle, new Regex(@"irmode"));
                range.SetStyle(OutputStyle, new Regex(@"gyromode"));
                range.SetStyle(OutputStyle, new Regex(@"SpeechToText"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_back"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_start"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_A"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_B"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_X"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_Y"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_up"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_left"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_down"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_right"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_leftstick"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_rightstick"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_leftbumper"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_rightbumper"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_lefttrigger"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_righttrigger"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_leftstickx"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_leftsticky"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_rightstickx"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_rightsticky"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_back"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_start"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_A"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_B"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_X"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_Y"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_up"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_left"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_down"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_right"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_leftstick"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_rightstick"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_leftbumper"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_rightbumper"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_lefttrigger"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_righttrigger"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_leftstickx"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_leftsticky"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_rightstickx"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_rightsticky"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_lefttriggerposition"));
                range.SetStyle(OutputStyle, new Regex(@"controller1_send_righttriggerposition"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_lefttriggerposition"));
                range.SetStyle(OutputStyle, new Regex(@"controller2_send_righttriggerposition"));
                range.SetStyle(OutputStyle, new Regex(@"EnableKM"));
                range.SetStyle(OutputStyle, new Regex(@"EnableXC"));
                range.SetStyle(OutputStyle, new Regex(@"EnableRI"));
                range.SetStyle(OutputStyle, new Regex(@"EnableXI"));
                range.SetStyle(OutputStyle, new Regex(@"EnableDI"));
                range.SetStyle(OutputStyle, new Regex(@"EnableJI"));
                range.SetStyle(OutputStyle, new Regex(@"EnablePI"));
                range.SetStyle(OutputStyle, new Regex(@"pollcount"));
                range.SetStyle(OutputStyle, new Regex(@"keys12345"));
                range.SetStyle(OutputStyle, new Regex(@"keys54321"));
                range.SetStyle(OutputStyle, new Regex(@"mousexp"));
                range.SetStyle(OutputStyle, new Regex(@"mouseyp"));
                range.SetStyle(OutputStyle, new Regex(@"testdouble"));
                range.SetStyle(OutputStyle, new Regex(@"testbool"));
                range.SetStyle(OutputStyle, new Regex(@"JoyconLeftGyroCenter"));
                range.SetStyle(OutputStyle, new Regex(@"JoyconRightGyroCenter"));
                range.SetStyle(OutputStyle, new Regex(@"ProControllerGyroCenter"));
                range.SetStyle(OutputStyle, new Regex(@"PS5ControllerGyroCenter"));
                range.SetStyle(OutputStyle, new Regex(@"Controller1GyroCenter"));
                range.SetStyle(OutputStyle, new Regex(@"Controller2GyroCenter"));
                range.SetStyle(OutputStyle, new Regex(@"JoyconLeftAccelCenter"));
                range.SetStyle(OutputStyle, new Regex(@"JoyconRightAccelCenter"));
                range.SetStyle(OutputStyle, new Regex(@"ProControllerAccelCenter"));
                range.SetStyle(OutputStyle, new Regex(@"PS5ControllerAccelCenter"));
                range.SetStyle(OutputStyle, new Regex(@"JoyconLeftStickCenter"));
                range.SetStyle(OutputStyle, new Regex(@"JoyconRightStickCenter"));
                range.SetStyle(OutputStyle, new Regex(@"ProControllerStickCenter"));
            }
            catch { }
        }
        private void SetClassicInputs()
        {
            string str = "width : " + width + Environment.NewLine;
            str += "height : " + height + Environment.NewLine;
            str += "MouseHookX : " + MouseHookX + Environment.NewLine;
            str += "MouseHookY : " + MouseHookY + Environment.NewLine;
            str += "Mouse1AxisX : " + Mouse1AxisX + Environment.NewLine;
            str += "Mouse1AxisY : " + Mouse1AxisY + Environment.NewLine;
            str += "Mouse1AxisZ : " + Mouse1AxisZ + Environment.NewLine;
            str += "Mouse1Buttons0 : " + Mouse1Buttons0 + Environment.NewLine;
            str += "Mouse1Buttons1 : " + Mouse1Buttons1 + Environment.NewLine;
            str += "Mouse1Buttons2 : " + Mouse1Buttons2 + Environment.NewLine;
            str += "Mouse1Buttons3 : " + Mouse1Buttons3 + Environment.NewLine;
            str += "Mouse1Buttons4 : " + Mouse1Buttons4 + Environment.NewLine;
            str += "Mouse1Buttons5 : " + Mouse1Buttons5 + Environment.NewLine;
            str += "Mouse1Buttons6 : " + Mouse1Buttons6 + Environment.NewLine;
            str += "Mouse1Buttons7 : " + Mouse1Buttons7 + Environment.NewLine;
            str += "Mouse2AxisX : " + Mouse2AxisX + Environment.NewLine;
            str += "Mouse2AxisY : " + Mouse2AxisY + Environment.NewLine;
            str += "Mouse2AxisZ : " + Mouse2AxisZ + Environment.NewLine;
            str += "Mouse2Buttons0 : " + Mouse2Buttons0 + Environment.NewLine;
            str += "Mouse2Buttons1 : " + Mouse2Buttons1 + Environment.NewLine;
            str += "Mouse2Buttons2 : " + Mouse2Buttons2 + Environment.NewLine;
            str += "Mouse2Buttons3 : " + Mouse2Buttons3 + Environment.NewLine;
            str += "Mouse2Buttons4 : " + Mouse2Buttons4 + Environment.NewLine;
            str += "Mouse2Buttons5 : " + Mouse2Buttons5 + Environment.NewLine;
            str += "Mouse2Buttons6 : " + Mouse2Buttons6 + Environment.NewLine;
            str += "Mouse2Buttons7 : " + Mouse2Buttons7 + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form5.SetLabel1Text(str);
            }
            catch { }
            str = "Key_0 : " + Key_0 + Environment.NewLine;
            str += "Key_1 : " + Key_1 + Environment.NewLine;
            str += "Key_2 : " + Key_2 + Environment.NewLine;
            str += "Key_3 : " + Key_3 + Environment.NewLine;
            str += "Key_4 : " + Key_4 + Environment.NewLine;
            str += "Key_5 : " + Key_5 + Environment.NewLine;
            str += "Key_6 : " + Key_6 + Environment.NewLine;
            str += "Key_7 : " + Key_7 + Environment.NewLine;
            str += "Key_8 : " + Key_8 + Environment.NewLine;
            str += "Key_9 : " + Key_9 + Environment.NewLine;
            str += "Key_A : " + Key_A + Environment.NewLine;
            str += "Key_B : " + Key_B + Environment.NewLine;
            str += "Key_C : " + Key_C + Environment.NewLine;
            str += "Key_D : " + Key_D + Environment.NewLine;
            str += "Key_E : " + Key_E + Environment.NewLine;
            str += "Key_F : " + Key_F + Environment.NewLine;
            str += "Key_G : " + Key_G + Environment.NewLine;
            str += "Key_H : " + Key_H + Environment.NewLine;
            str += "Key_I : " + Key_I + Environment.NewLine;
            str += "Key_J : " + Key_J + Environment.NewLine;
            str += "Key_K : " + Key_K + Environment.NewLine;
            str += "Key_L : " + Key_L + Environment.NewLine;
            str += "Key_M : " + Key_M + Environment.NewLine;
            str += "Key_N : " + Key_N + Environment.NewLine;
            str += "Key_O : " + Key_O + Environment.NewLine;
            str += "Key_P : " + Key_P + Environment.NewLine;
            str += "Key_Q : " + Key_Q + Environment.NewLine;
            str += "Key_R : " + Key_R + Environment.NewLine;
            str += "Key_S : " + Key_S + Environment.NewLine;
            str += "Key_T : " + Key_T + Environment.NewLine;
            str += "Key_U : " + Key_U + Environment.NewLine;
            str += "Key_V : " + Key_V + Environment.NewLine;
            str += "Key_W : " + Key_W + Environment.NewLine;
            str += "Key_X : " + Key_X + Environment.NewLine;
            str += "Key_Y : " + Key_Y + Environment.NewLine;
            str += "Key_Z : " + Key_Z + Environment.NewLine;
            str += "Key_F1 : " + Key_F1 + Environment.NewLine;
            str += "Key_F2 : " + Key_F2 + Environment.NewLine;
            str += "Key_F3 : " + Key_F3 + Environment.NewLine;
            str += "Key_F4 : " + Key_F4 + Environment.NewLine;
            str += "Key_F5 : " + Key_F5 + Environment.NewLine;
            str += "Key_F6 : " + Key_F6 + Environment.NewLine;
            str += "Key_F7 : " + Key_F7 + Environment.NewLine;
            str += "Key_F8 : " + Key_F8 + Environment.NewLine;
            str += "Key_F9 : " + Key_F9 + Environment.NewLine;
            str += "Key_F10 : " + Key_F10 + Environment.NewLine;
            str += "Key_F11 : " + Key_F11 + Environment.NewLine;
            str += "Key_F12 : " + Key_F12 + Environment.NewLine;
            str += "Key_F13 : " + Key_F13 + Environment.NewLine;
            str += "Key_F14 : " + Key_F14 + Environment.NewLine;
            str += "Key_F15 : " + Key_F15 + Environment.NewLine;
            str += "Key_F16 : " + Key_F16 + Environment.NewLine;
            str += "Key_F17 : " + Key_F17 + Environment.NewLine;
            str += "Key_F18 : " + Key_F18 + Environment.NewLine;
            str += "Key_F19 : " + Key_F19 + Environment.NewLine;
            str += "Key_F20 : " + Key_F20 + Environment.NewLine;
            str += "Key_F21 : " + Key_F21 + Environment.NewLine;
            str += "Key_F22 : " + Key_F22 + Environment.NewLine;
            str += "Key_F23 : " + Key_F23 + Environment.NewLine;
            str += "Key_F24 : " + Key_F24 + Environment.NewLine;
            str += "Key_LWIN : " + Key_LWIN + Environment.NewLine;
            str += "Key_RWIN : " + Key_RWIN + Environment.NewLine;
            str += "Key_APPS : " + Key_APPS + Environment.NewLine;
            str += "Key_SLEEP : " + Key_SLEEP + Environment.NewLine;
            str += "Key_LBUTTON : " + Key_LBUTTON + Environment.NewLine;
            str += "Key_RBUTTON : " + Key_RBUTTON + Environment.NewLine;
            str += "Key_CANCEL : " + Key_CANCEL + Environment.NewLine;
            str += "Key_MBUTTON : " + Key_MBUTTON + Environment.NewLine;
            str += "Key_XBUTTON1 : " + Key_XBUTTON1 + Environment.NewLine;
            str += "Key_XBUTTON2 : " + Key_XBUTTON2 + Environment.NewLine;
            str += "Key_BACK : " + Key_BACK + Environment.NewLine;
            str += "Key_Tab : " + Key_Tab + Environment.NewLine;
            str += "Key_CLEAR : " + Key_CLEAR + Environment.NewLine;
            str += "Key_Return : " + Key_Return + Environment.NewLine;
            str += "Key_SHIFT : " + Key_SHIFT + Environment.NewLine;
            str += "Key_CONTROL : " + Key_CONTROL + Environment.NewLine;
            str += "Key_MENU : " + Key_MENU + Environment.NewLine;
            str += "Key_PAUSE : " + Key_PAUSE + Environment.NewLine;
            str += "Key_CAPITAL : " + Key_CAPITAL + Environment.NewLine;
            str += "Key_KANA : " + Key_KANA + Environment.NewLine;
            str += "Key_HANGEUL : " + Key_HANGEUL + Environment.NewLine;
            str += "Key_HANGUL : " + Key_HANGUL + Environment.NewLine;
            str += "Key_JUNJA : " + Key_JUNJA + Environment.NewLine;
            str += "Key_FINAL : " + Key_FINAL + Environment.NewLine;
            str += "Key_HANJA : " + Key_HANJA + Environment.NewLine;
            str += "Key_KANJI : " + Key_KANJI + Environment.NewLine;
            str += "Key_Escape : " + Key_Escape + Environment.NewLine;
            str += "Key_CONVERT : " + Key_CONVERT + Environment.NewLine;
            str += "Key_NONCONVERT : " + Key_NONCONVERT + Environment.NewLine;
            str += "Key_ACCEPT : " + Key_ACCEPT + Environment.NewLine;
            str += "Key_MODECHANGE : " + Key_MODECHANGE + Environment.NewLine;
            str += "Key_Space : " + Key_Space + Environment.NewLine;
            str += "Key_PRIOR : " + Key_PRIOR + Environment.NewLine;
            str += "Key_NEXT : " + Key_NEXT + Environment.NewLine;
            str += "Key_END : " + Key_END + Environment.NewLine;
            str += "Key_HOME : " + Key_HOME + Environment.NewLine;
            str += "Key_LEFT : " + Key_LEFT + Environment.NewLine;
            str += "Key_UP : " + Key_UP + Environment.NewLine;
            str += "Key_RIGHT : " + Key_RIGHT + Environment.NewLine;
            str += "Key_DOWN : " + Key_DOWN + Environment.NewLine;
            str += "Key_SELECT : " + Key_SELECT + Environment.NewLine;
            str += "Key_PRINT : " + Key_PRINT + Environment.NewLine;
            str += "Key_EXECUTE : " + Key_EXECUTE + Environment.NewLine;
            str += "Key_SNAPSHOT : " + Key_SNAPSHOT + Environment.NewLine;
            str += "Key_INSERT : " + Key_INSERT + Environment.NewLine;
            str += "Key_DELETE : " + Key_DELETE + Environment.NewLine;
            str += "Key_HELP : " + Key_HELP + Environment.NewLine;
            str += "Key_APOSTROPHE : " + Key_APOSTROPHE + Environment.NewLine;
            str += "Key_NUMPAD0 : " + Key_NUMPAD0 + Environment.NewLine;
            str += "Key_NUMPAD1 : " + Key_NUMPAD1 + Environment.NewLine;
            str += "Key_NUMPAD2 : " + Key_NUMPAD2 + Environment.NewLine;
            str += "Key_NUMPAD3 : " + Key_NUMPAD3 + Environment.NewLine;
            str += "Key_NUMPAD4 : " + Key_NUMPAD4 + Environment.NewLine;
            str += "Key_NUMPAD5 : " + Key_NUMPAD5 + Environment.NewLine;
            str += "Key_NUMPAD6 : " + Key_NUMPAD6 + Environment.NewLine;
            str += "Key_NUMPAD7 : " + Key_NUMPAD7 + Environment.NewLine;
            str += "Key_NUMPAD8 : " + Key_NUMPAD8 + Environment.NewLine;
            str += "Key_NUMPAD9 : " + Key_NUMPAD9 + Environment.NewLine;
            str += "Key_MULTIPLY : " + Key_MULTIPLY + Environment.NewLine;
            str += "Key_ADD : " + Key_ADD + Environment.NewLine;
            str += "Key_SEPARATOR : " + Key_SEPARATOR + Environment.NewLine;
            str += "Key_SUBTRACT : " + Key_SUBTRACT + Environment.NewLine;
            str += "Key_DECIMAL : " + Key_DECIMAL + Environment.NewLine;
            str += "Key_DIVIDE : " + Key_DIVIDE + Environment.NewLine;
            str += "Key_NUMLOCK : " + Key_NUMLOCK + Environment.NewLine;
            str += "Key_SCROLL : " + Key_SCROLL + Environment.NewLine;
            str += "Key_LeftShift : " + Key_LeftShift + Environment.NewLine;
            str += "Key_RightShift : " + Key_RightShift + Environment.NewLine;
            str += "Key_LeftControl : " + Key_LeftControl + Environment.NewLine;
            str += "Key_RightControl : " + Key_RightControl + Environment.NewLine;
            str += "Key_LMENU : " + Key_LMENU + Environment.NewLine;
            str += "Key_RMENU : " + Key_RMENU + Environment.NewLine;
            str += "Key_BROWSER_BACK : " + Key_BROWSER_BACK + Environment.NewLine;
            str += "Key_BROWSER_FORWARD : " + Key_BROWSER_FORWARD + Environment.NewLine;
            str += "Key_BROWSER_REFRESH : " + Key_BROWSER_REFRESH + Environment.NewLine;
            str += "Key_BROWSER_STOP : " + Key_BROWSER_STOP + Environment.NewLine;
            str += "Key_BROWSER_SEARCH : " + Key_BROWSER_SEARCH + Environment.NewLine;
            str += "Key_BROWSER_FAVORITES : " + Key_BROWSER_FAVORITES + Environment.NewLine;
            str += "Key_BROWSER_HOME : " + Key_BROWSER_HOME + Environment.NewLine;
            str += "Key_VOLUME_MUTE : " + Key_VOLUME_MUTE + Environment.NewLine;
            str += "Key_VOLUME_DOWN : " + Key_VOLUME_DOWN + Environment.NewLine;
            str += "Key_VOLUME_UP : " + Key_VOLUME_UP + Environment.NewLine;
            str += "Key_MEDIA_NEXT_TRACK : " + Key_MEDIA_NEXT_TRACK + Environment.NewLine;
            str += "Key_MEDIA_PREV_TRACK : " + Key_MEDIA_PREV_TRACK + Environment.NewLine;
            str += "Key_MEDIA_STOP : " + Key_MEDIA_STOP + Environment.NewLine;
            str += "Key_MEDIA_PLAY_PAUSE : " + Key_MEDIA_PLAY_PAUSE + Environment.NewLine;
            str += "Key_LAUNCH_MAIL : " + Key_LAUNCH_MAIL + Environment.NewLine;
            str += "Key_LAUNCH_MEDIA_SELECT : " + Key_LAUNCH_MEDIA_SELECT + Environment.NewLine;
            str += "Key_LAUNCH_APP1 : " + Key_LAUNCH_APP1 + Environment.NewLine;
            str += "Key_LAUNCH_APP2 : " + Key_LAUNCH_APP2 + Environment.NewLine;
            str += "Key_OEM_1 : " + Key_OEM_1 + Environment.NewLine;
            str += "Key_OEM_PLUS : " + Key_OEM_PLUS + Environment.NewLine;
            str += "Key_OEM_COMMA : " + Key_OEM_COMMA + Environment.NewLine;
            str += "Key_OEM_MINUS : " + Key_OEM_MINUS + Environment.NewLine;
            str += "Key_OEM_PERIOD : " + Key_OEM_PERIOD + Environment.NewLine;
            str += "Key_OEM_2 : " + Key_OEM_2 + Environment.NewLine;
            str += "Key_OEM_3 : " + Key_OEM_3 + Environment.NewLine;
            str += "Key_OEM_4 : " + Key_OEM_4 + Environment.NewLine;
            str += "Key_OEM_5 : " + Key_OEM_5 + Environment.NewLine;
            str += "Key_OEM_6 : " + Key_OEM_6 + Environment.NewLine;
            str += "Key_OEM_7 : " + Key_OEM_7 + Environment.NewLine;
            str += "Key_OEM_8 : " + Key_OEM_8 + Environment.NewLine;
            str += "Key_OEM_102 : " + Key_OEM_102 + Environment.NewLine;
            str += "Key_PROCESSKEY : " + Key_PROCESSKEY + Environment.NewLine;
            str += "Key_PACKET : " + Key_PACKET + Environment.NewLine;
            str += "Key_ATTN : " + Key_ATTN + Environment.NewLine;
            str += "Key_CRSEL : " + Key_CRSEL + Environment.NewLine;
            str += "Key_EXSEL : " + Key_EXSEL + Environment.NewLine;
            str += "Key_EREOF : " + Key_EREOF + Environment.NewLine;
            str += "Key_PLAY : " + Key_PLAY + Environment.NewLine;
            str += "Key_ZOOM : " + Key_ZOOM + Environment.NewLine;
            str += "Key_NONAME : " + Key_NONAME + Environment.NewLine;
            str += "Key_PA1 : " + Key_PA1 + Environment.NewLine;
            str += "Key_OEM_CLEAR : " + Key_OEM_CLEAR + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form5.SetLabel2Text(str);
            }
            catch { }
            str = "Keyboard1KeyA : " + Keyboard1KeyA + Environment.NewLine;
            str += "Keyboard1KeyB : " + Keyboard1KeyB + Environment.NewLine;
            str += "Keyboard1KeyC : " + Keyboard1KeyC + Environment.NewLine;
            str += "Keyboard1KeyD : " + Keyboard1KeyD + Environment.NewLine;
            str += "Keyboard1KeyE : " + Keyboard1KeyE + Environment.NewLine;
            str += "Keyboard1KeyF : " + Keyboard1KeyF + Environment.NewLine;
            str += "Keyboard1KeyG : " + Keyboard1KeyG + Environment.NewLine;
            str += "Keyboard1KeyH : " + Keyboard1KeyH + Environment.NewLine;
            str += "Keyboard1KeyI : " + Keyboard1KeyI + Environment.NewLine;
            str += "Keyboard1KeyJ : " + Keyboard1KeyJ + Environment.NewLine;
            str += "Keyboard1KeyK : " + Keyboard1KeyK + Environment.NewLine;
            str += "Keyboard1KeyL : " + Keyboard1KeyL + Environment.NewLine;
            str += "Keyboard1KeyM : " + Keyboard1KeyM + Environment.NewLine;
            str += "Keyboard1KeyN : " + Keyboard1KeyN + Environment.NewLine;
            str += "Keyboard1KeyO : " + Keyboard1KeyO + Environment.NewLine;
            str += "Keyboard1KeyP : " + Keyboard1KeyP + Environment.NewLine;
            str += "Keyboard1KeyQ : " + Keyboard1KeyQ + Environment.NewLine;
            str += "Keyboard1KeyR : " + Keyboard1KeyR + Environment.NewLine;
            str += "Keyboard1KeyS : " + Keyboard1KeyS + Environment.NewLine;
            str += "Keyboard1KeyT : " + Keyboard1KeyT + Environment.NewLine;
            str += "Keyboard1KeyU : " + Keyboard1KeyU + Environment.NewLine;
            str += "Keyboard1KeyV : " + Keyboard1KeyV + Environment.NewLine;
            str += "Keyboard1KeyW : " + Keyboard1KeyW + Environment.NewLine;
            str += "Keyboard1KeyX : " + Keyboard1KeyX + Environment.NewLine;
            str += "Keyboard1KeyY : " + Keyboard1KeyY + Environment.NewLine;
            str += "Keyboard1KeyZ : " + Keyboard1KeyZ + Environment.NewLine;
            str += "Keyboard1KeyEscape : " + Keyboard1KeyEscape + Environment.NewLine;
            str += "Keyboard1KeyD1 : " + Keyboard1KeyD1 + Environment.NewLine;
            str += "Keyboard1KeyD2 : " + Keyboard1KeyD2 + Environment.NewLine;
            str += "Keyboard1KeyD3 : " + Keyboard1KeyD3 + Environment.NewLine;
            str += "Keyboard1KeyD4 : " + Keyboard1KeyD4 + Environment.NewLine;
            str += "Keyboard1KeyD5 : " + Keyboard1KeyD5 + Environment.NewLine;
            str += "Keyboard1KeyD6 : " + Keyboard1KeyD6 + Environment.NewLine;
            str += "Keyboard1KeyD7 : " + Keyboard1KeyD7 + Environment.NewLine;
            str += "Keyboard1KeyD8 : " + Keyboard1KeyD8 + Environment.NewLine;
            str += "Keyboard1KeyD9 : " + Keyboard1KeyD9 + Environment.NewLine;
            str += "Keyboard1KeyD0 : " + Keyboard1KeyD0 + Environment.NewLine;
            str += "Keyboard1KeyMinus : " + Keyboard1KeyMinus + Environment.NewLine;
            str += "Keyboard1KeyEquals : " + Keyboard1KeyEquals + Environment.NewLine;
            str += "Keyboard1KeyBack : " + Keyboard1KeyBack + Environment.NewLine;
            str += "Keyboard1KeyTab : " + Keyboard1KeyTab + Environment.NewLine;
            str += "Keyboard1KeyLeftBracket : " + Keyboard1KeyLeftBracket + Environment.NewLine;
            str += "Keyboard1KeyRightBracket : " + Keyboard1KeyRightBracket + Environment.NewLine;
            str += "Keyboard1KeyReturn : " + Keyboard1KeyReturn + Environment.NewLine;
            str += "Keyboard1KeyLeftControl : " + Keyboard1KeyLeftControl + Environment.NewLine;
            str += "Keyboard1KeySemicolon : " + Keyboard1KeySemicolon + Environment.NewLine;
            str += "Keyboard1KeyApostrophe : " + Keyboard1KeyApostrophe + Environment.NewLine;
            str += "Keyboard1KeyGrave : " + Keyboard1KeyGrave + Environment.NewLine;
            str += "Keyboard1KeyLeftShift : " + Keyboard1KeyLeftShift + Environment.NewLine;
            str += "Keyboard1KeyBackslash : " + Keyboard1KeyBackslash + Environment.NewLine;
            str += "Keyboard1KeyComma : " + Keyboard1KeyComma + Environment.NewLine;
            str += "Keyboard1KeyPeriod : " + Keyboard1KeyPeriod + Environment.NewLine;
            str += "Keyboard1KeySlash : " + Keyboard1KeySlash + Environment.NewLine;
            str += "Keyboard1KeyRightShift : " + Keyboard1KeyRightShift + Environment.NewLine;
            str += "Keyboard1KeyMultiply : " + Keyboard1KeyMultiply + Environment.NewLine;
            str += "Keyboard1KeyLeftAlt : " + Keyboard1KeyLeftAlt + Environment.NewLine;
            str += "Keyboard1KeySpace : " + Keyboard1KeySpace + Environment.NewLine;
            str += "Keyboard1KeyCapital : " + Keyboard1KeyCapital + Environment.NewLine;
            str += "Keyboard1KeyF1 : " + Keyboard1KeyF1 + Environment.NewLine;
            str += "Keyboard1KeyF2 : " + Keyboard1KeyF2 + Environment.NewLine;
            str += "Keyboard1KeyF3 : " + Keyboard1KeyF3 + Environment.NewLine;
            str += "Keyboard1KeyF4 : " + Keyboard1KeyF4 + Environment.NewLine;
            str += "Keyboard1KeyF5 : " + Keyboard1KeyF5 + Environment.NewLine;
            str += "Keyboard1KeyF6 : " + Keyboard1KeyF6 + Environment.NewLine;
            str += "Keyboard1KeyF7 : " + Keyboard1KeyF7 + Environment.NewLine;
            str += "Keyboard1KeyF8 : " + Keyboard1KeyF8 + Environment.NewLine;
            str += "Keyboard1KeyF9 : " + Keyboard1KeyF9 + Environment.NewLine;
            str += "Keyboard1KeyF10 : " + Keyboard1KeyF10 + Environment.NewLine;
            str += "Keyboard1KeyF11 : " + Keyboard1KeyF11 + Environment.NewLine;
            str += "Keyboard1KeyF12 : " + Keyboard1KeyF12 + Environment.NewLine;
            str += "Keyboard1KeyF13 : " + Keyboard1KeyF13 + Environment.NewLine;
            str += "Keyboard1KeyF14 : " + Keyboard1KeyF14 + Environment.NewLine;
            str += "Keyboard1KeyF15 : " + Keyboard1KeyF15 + Environment.NewLine;
            str += "Keyboard1KeyNumberLock : " + Keyboard1KeyNumberLock + Environment.NewLine;
            str += "Keyboard1KeyScrollLock : " + Keyboard1KeyScrollLock + Environment.NewLine;
            str += "Keyboard1KeyNumberPad0 : " + Keyboard1KeyNumberPad0 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad1 : " + Keyboard1KeyNumberPad1 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad2 : " + Keyboard1KeyNumberPad2 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad3 : " + Keyboard1KeyNumberPad3 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad4 : " + Keyboard1KeyNumberPad4 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad5 : " + Keyboard1KeyNumberPad5 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad6 : " + Keyboard1KeyNumberPad6 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad7 : " + Keyboard1KeyNumberPad7 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad8 : " + Keyboard1KeyNumberPad8 + Environment.NewLine;
            str += "Keyboard1KeyNumberPad9 : " + Keyboard1KeyNumberPad9 + Environment.NewLine;
            str += "Keyboard1KeySubtract : " + Keyboard1KeySubtract + Environment.NewLine;
            str += "Keyboard1KeyAdd : " + Keyboard1KeyAdd + Environment.NewLine;
            str += "Keyboard1KeyDecimal : " + Keyboard1KeyDecimal + Environment.NewLine;
            str += "Keyboard1KeyOem102 : " + Keyboard1KeyOem102 + Environment.NewLine;
            str += "Keyboard1KeyKana : " + Keyboard1KeyKana + Environment.NewLine;
            str += "Keyboard1KeyAbntC1 : " + Keyboard1KeyAbntC1 + Environment.NewLine;
            str += "Keyboard1KeyConvert : " + Keyboard1KeyConvert + Environment.NewLine;
            str += "Keyboard1KeyNoConvert : " + Keyboard1KeyNoConvert + Environment.NewLine;
            str += "Keyboard1KeyYen : " + Keyboard1KeyYen + Environment.NewLine;
            str += "Keyboard1KeyAbntC2 : " + Keyboard1KeyAbntC2 + Environment.NewLine;
            str += "Keyboard1KeyNumberPadEquals : " + Keyboard1KeyNumberPadEquals + Environment.NewLine;
            str += "Keyboard1KeyPreviousTrack : " + Keyboard1KeyPreviousTrack + Environment.NewLine;
            str += "Keyboard1KeyAT : " + Keyboard1KeyAT + Environment.NewLine;
            str += "Keyboard1KeyColon : " + Keyboard1KeyColon + Environment.NewLine;
            str += "Keyboard1KeyUnderline : " + Keyboard1KeyUnderline + Environment.NewLine;
            str += "Keyboard1KeyKanji : " + Keyboard1KeyKanji + Environment.NewLine;
            str += "Keyboard1KeyStop : " + Keyboard1KeyStop + Environment.NewLine;
            str += "Keyboard1KeyAX : " + Keyboard1KeyAX + Environment.NewLine;
            str += "Keyboard1KeyUnlabeled : " + Keyboard1KeyUnlabeled + Environment.NewLine;
            str += "Keyboard1KeyNextTrack : " + Keyboard1KeyNextTrack + Environment.NewLine;
            str += "Keyboard1KeyNumberPadEnter : " + Keyboard1KeyNumberPadEnter + Environment.NewLine;
            str += "Keyboard1KeyRightControl : " + Keyboard1KeyRightControl + Environment.NewLine;
            str += "Keyboard1KeyMute : " + Keyboard1KeyMute + Environment.NewLine;
            str += "Keyboard1KeyCalculator : " + Keyboard1KeyCalculator + Environment.NewLine;
            str += "Keyboard1KeyPlayPause : " + Keyboard1KeyPlayPause + Environment.NewLine;
            str += "Keyboard1KeyMediaStop : " + Keyboard1KeyMediaStop + Environment.NewLine;
            str += "Keyboard1KeyVolumeDown : " + Keyboard1KeyVolumeDown + Environment.NewLine;
            str += "Keyboard1KeyVolumeUp : " + Keyboard1KeyVolumeUp + Environment.NewLine;
            str += "Keyboard1KeyWebHome : " + Keyboard1KeyWebHome + Environment.NewLine;
            str += "Keyboard1KeyNumberPadComma : " + Keyboard1KeyNumberPadComma + Environment.NewLine;
            str += "Keyboard1KeyDivide : " + Keyboard1KeyDivide + Environment.NewLine;
            str += "Keyboard1KeyPrintScreen : " + Keyboard1KeyPrintScreen + Environment.NewLine;
            str += "Keyboard1KeyRightAlt : " + Keyboard1KeyRightAlt + Environment.NewLine;
            str += "Keyboard1KeyPause : " + Keyboard1KeyPause + Environment.NewLine;
            str += "Keyboard1KeyHome : " + Keyboard1KeyHome + Environment.NewLine;
            str += "Keyboard1KeyUp : " + Keyboard1KeyUp + Environment.NewLine;
            str += "Keyboard1KeyPageUp : " + Keyboard1KeyPageUp + Environment.NewLine;
            str += "Keyboard1KeyLeft : " + Keyboard1KeyLeft + Environment.NewLine;
            str += "Keyboard1KeyRight : " + Keyboard1KeyRight + Environment.NewLine;
            str += "Keyboard1KeyEnd : " + Keyboard1KeyEnd + Environment.NewLine;
            str += "Keyboard1KeyDown : " + Keyboard1KeyDown + Environment.NewLine;
            str += "Keyboard1KeyPageDown : " + Keyboard1KeyPageDown + Environment.NewLine;
            str += "Keyboard1KeyInsert : " + Keyboard1KeyInsert + Environment.NewLine;
            str += "Keyboard1KeyDelete : " + Keyboard1KeyDelete + Environment.NewLine;
            str += "Keyboard1KeyLeftWindowsKey : " + Keyboard1KeyLeftWindowsKey + Environment.NewLine;
            str += "Keyboard1KeyRightWindowsKey : " + Keyboard1KeyRightWindowsKey + Environment.NewLine;
            str += "Keyboard1KeyApplications : " + Keyboard1KeyApplications + Environment.NewLine;
            str += "Keyboard1KeyPower : " + Keyboard1KeyPower + Environment.NewLine;
            str += "Keyboard1KeySleep : " + Keyboard1KeySleep + Environment.NewLine;
            str += "Keyboard1KeyWake : " + Keyboard1KeyWake + Environment.NewLine;
            str += "Keyboard1KeyWebSearch : " + Keyboard1KeyWebSearch + Environment.NewLine;
            str += "Keyboard1KeyWebFavorites : " + Keyboard1KeyWebFavorites + Environment.NewLine;
            str += "Keyboard1KeyWebRefresh : " + Keyboard1KeyWebRefresh + Environment.NewLine;
            str += "Keyboard1KeyWebStop : " + Keyboard1KeyWebStop + Environment.NewLine;
            str += "Keyboard1KeyWebForward : " + Keyboard1KeyWebForward + Environment.NewLine;
            str += "Keyboard1KeyWebBack : " + Keyboard1KeyWebBack + Environment.NewLine;
            str += "Keyboard1KeyMyComputer : " + Keyboard1KeyMyComputer + Environment.NewLine;
            str += "Keyboard1KeyMail : " + Keyboard1KeyMail + Environment.NewLine;
            str += "Keyboard1KeyMediaSelect : " + Keyboard1KeyMediaSelect + Environment.NewLine;
            str += "Keyboard1KeyUnknown : " + Keyboard1KeyUnknown + Environment.NewLine;
            str += "Keyboard2KeyA : " + Keyboard2KeyA + Environment.NewLine;
            str += "Keyboard2KeyB : " + Keyboard2KeyB + Environment.NewLine;
            str += "Keyboard2KeyC : " + Keyboard2KeyC + Environment.NewLine;
            str += "Keyboard2KeyD : " + Keyboard2KeyD + Environment.NewLine;
            str += "Keyboard2KeyE : " + Keyboard2KeyE + Environment.NewLine;
            str += "Keyboard2KeyF : " + Keyboard2KeyF + Environment.NewLine;
            str += "Keyboard2KeyG : " + Keyboard2KeyG + Environment.NewLine;
            str += "Keyboard2KeyH : " + Keyboard2KeyH + Environment.NewLine;
            str += "Keyboard2KeyI : " + Keyboard2KeyI + Environment.NewLine;
            str += "Keyboard2KeyJ : " + Keyboard2KeyJ + Environment.NewLine;
            str += "Keyboard2KeyK : " + Keyboard2KeyK + Environment.NewLine;
            str += "Keyboard2KeyL : " + Keyboard2KeyL + Environment.NewLine;
            str += "Keyboard2KeyM : " + Keyboard2KeyM + Environment.NewLine;
            str += "Keyboard2KeyN : " + Keyboard2KeyN + Environment.NewLine;
            str += "Keyboard2KeyO : " + Keyboard2KeyO + Environment.NewLine;
            str += "Keyboard2KeyP : " + Keyboard2KeyP + Environment.NewLine;
            str += "Keyboard2KeyQ : " + Keyboard2KeyQ + Environment.NewLine;
            str += "Keyboard2KeyR : " + Keyboard2KeyR + Environment.NewLine;
            str += "Keyboard2KeyS : " + Keyboard2KeyS + Environment.NewLine;
            str += "Keyboard2KeyT : " + Keyboard2KeyT + Environment.NewLine;
            str += "Keyboard2KeyU : " + Keyboard2KeyU + Environment.NewLine;
            str += "Keyboard2KeyV : " + Keyboard2KeyV + Environment.NewLine;
            str += "Keyboard2KeyW : " + Keyboard2KeyW + Environment.NewLine;
            str += "Keyboard2KeyX : " + Keyboard2KeyX + Environment.NewLine;
            str += "Keyboard2KeyY : " + Keyboard2KeyY + Environment.NewLine;
            str += "Keyboard2KeyZ : " + Keyboard2KeyZ + Environment.NewLine;
            str += "Keyboard2KeyEscape : " + Keyboard2KeyEscape + Environment.NewLine;
            str += "Keyboard2KeyD1 : " + Keyboard2KeyD1 + Environment.NewLine;
            str += "Keyboard2KeyD2 : " + Keyboard2KeyD2 + Environment.NewLine;
            str += "Keyboard2KeyD3 : " + Keyboard2KeyD3 + Environment.NewLine;
            str += "Keyboard2KeyD4 : " + Keyboard2KeyD4 + Environment.NewLine;
            str += "Keyboard2KeyD5 : " + Keyboard2KeyD5 + Environment.NewLine;
            str += "Keyboard2KeyD6 : " + Keyboard2KeyD6 + Environment.NewLine;
            str += "Keyboard2KeyD7 : " + Keyboard2KeyD7 + Environment.NewLine;
            str += "Keyboard2KeyD8 : " + Keyboard2KeyD8 + Environment.NewLine;
            str += "Keyboard2KeyD9 : " + Keyboard2KeyD9 + Environment.NewLine;
            str += "Keyboard2KeyD0 : " + Keyboard2KeyD0 + Environment.NewLine;
            str += "Keyboard2KeyMinus : " + Keyboard2KeyMinus + Environment.NewLine;
            str += "Keyboard2KeyEquals : " + Keyboard2KeyEquals + Environment.NewLine;
            str += "Keyboard2KeyBack : " + Keyboard2KeyBack + Environment.NewLine;
            str += "Keyboard2KeyTab : " + Keyboard2KeyTab + Environment.NewLine;
            str += "Keyboard2KeyLeftBracket : " + Keyboard2KeyLeftBracket + Environment.NewLine;
            str += "Keyboard2KeyRightBracket : " + Keyboard2KeyRightBracket + Environment.NewLine;
            str += "Keyboard2KeyReturn : " + Keyboard2KeyReturn + Environment.NewLine;
            str += "Keyboard2KeyLeftControl : " + Keyboard2KeyLeftControl + Environment.NewLine;
            str += "Keyboard2KeySemicolon : " + Keyboard2KeySemicolon + Environment.NewLine;
            str += "Keyboard2KeyApostrophe : " + Keyboard2KeyApostrophe + Environment.NewLine;
            str += "Keyboard2KeyGrave : " + Keyboard2KeyGrave + Environment.NewLine;
            str += "Keyboard2KeyLeftShift : " + Keyboard2KeyLeftShift + Environment.NewLine;
            str += "Keyboard2KeyBackslash : " + Keyboard2KeyBackslash + Environment.NewLine;
            str += "Keyboard2KeyComma : " + Keyboard2KeyComma + Environment.NewLine;
            str += "Keyboard2KeyPeriod : " + Keyboard2KeyPeriod + Environment.NewLine;
            str += "Keyboard2KeySlash : " + Keyboard2KeySlash + Environment.NewLine;
            str += "Keyboard2KeyRightShift : " + Keyboard2KeyRightShift + Environment.NewLine;
            str += "Keyboard2KeyMultiply : " + Keyboard2KeyMultiply + Environment.NewLine;
            str += "Keyboard2KeyLeftAlt : " + Keyboard2KeyLeftAlt + Environment.NewLine;
            str += "Keyboard2KeySpace : " + Keyboard2KeySpace + Environment.NewLine;
            str += "Keyboard2KeyCapital : " + Keyboard2KeyCapital + Environment.NewLine;
            str += "Keyboard2KeyF1 : " + Keyboard2KeyF1 + Environment.NewLine;
            str += "Keyboard2KeyF2 : " + Keyboard2KeyF2 + Environment.NewLine;
            str += "Keyboard2KeyF3 : " + Keyboard2KeyF3 + Environment.NewLine;
            str += "Keyboard2KeyF4 : " + Keyboard2KeyF4 + Environment.NewLine;
            str += "Keyboard2KeyF5 : " + Keyboard2KeyF5 + Environment.NewLine;
            str += "Keyboard2KeyF6 : " + Keyboard2KeyF6 + Environment.NewLine;
            str += "Keyboard2KeyF7 : " + Keyboard2KeyF7 + Environment.NewLine;
            str += "Keyboard2KeyF8 : " + Keyboard2KeyF8 + Environment.NewLine;
            str += "Keyboard2KeyF9 : " + Keyboard2KeyF9 + Environment.NewLine;
            str += "Keyboard2KeyF10 : " + Keyboard2KeyF10 + Environment.NewLine;
            str += "Keyboard2KeyF11 : " + Keyboard2KeyF11 + Environment.NewLine;
            str += "Keyboard2KeyF12 : " + Keyboard2KeyF12 + Environment.NewLine;
            str += "Keyboard2KeyF13 : " + Keyboard2KeyF13 + Environment.NewLine;
            str += "Keyboard2KeyF14 : " + Keyboard2KeyF14 + Environment.NewLine;
            str += "Keyboard2KeyF15 : " + Keyboard2KeyF15 + Environment.NewLine;
            str += "Keyboard2KeyNumberLock : " + Keyboard2KeyNumberLock + Environment.NewLine;
            str += "Keyboard2KeyScrollLock : " + Keyboard2KeyScrollLock + Environment.NewLine;
            str += "Keyboard2KeyNumberPad0 : " + Keyboard2KeyNumberPad0 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad1 : " + Keyboard2KeyNumberPad1 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad2 : " + Keyboard2KeyNumberPad2 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad3 : " + Keyboard2KeyNumberPad3 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad4 : " + Keyboard2KeyNumberPad4 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad5 : " + Keyboard2KeyNumberPad5 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad6 : " + Keyboard2KeyNumberPad6 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad7 : " + Keyboard2KeyNumberPad7 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad8 : " + Keyboard2KeyNumberPad8 + Environment.NewLine;
            str += "Keyboard2KeyNumberPad9 : " + Keyboard2KeyNumberPad9 + Environment.NewLine;
            str += "Keyboard2KeySubtract : " + Keyboard2KeySubtract + Environment.NewLine;
            str += "Keyboard2KeyAdd : " + Keyboard2KeyAdd + Environment.NewLine;
            str += "Keyboard2KeyDecimal : " + Keyboard2KeyDecimal + Environment.NewLine;
            str += "Keyboard2KeyOem102 : " + Keyboard2KeyOem102 + Environment.NewLine;
            str += "Keyboard2KeyKana : " + Keyboard2KeyKana + Environment.NewLine;
            str += "Keyboard2KeyAbntC1 : " + Keyboard2KeyAbntC1 + Environment.NewLine;
            str += "Keyboard2KeyConvert : " + Keyboard2KeyConvert + Environment.NewLine;
            str += "Keyboard2KeyNoConvert : " + Keyboard2KeyNoConvert + Environment.NewLine;
            str += "Keyboard2KeyYen : " + Keyboard2KeyYen + Environment.NewLine;
            str += "Keyboard2KeyAbntC2 : " + Keyboard2KeyAbntC2 + Environment.NewLine;
            str += "Keyboard2KeyNumberPadEquals : " + Keyboard2KeyNumberPadEquals + Environment.NewLine;
            str += "Keyboard2KeyPreviousTrack : " + Keyboard2KeyPreviousTrack + Environment.NewLine;
            str += "Keyboard2KeyAT : " + Keyboard2KeyAT + Environment.NewLine;
            str += "Keyboard2KeyColon : " + Keyboard2KeyColon + Environment.NewLine;
            str += "Keyboard2KeyUnderline : " + Keyboard2KeyUnderline + Environment.NewLine;
            str += "Keyboard2KeyKanji : " + Keyboard2KeyKanji + Environment.NewLine;
            str += "Keyboard2KeyStop : " + Keyboard2KeyStop + Environment.NewLine;
            str += "Keyboard2KeyAX : " + Keyboard2KeyAX + Environment.NewLine;
            str += "Keyboard2KeyUnlabeled : " + Keyboard2KeyUnlabeled + Environment.NewLine;
            str += "Keyboard2KeyNextTrack : " + Keyboard2KeyNextTrack + Environment.NewLine;
            str += "Keyboard2KeyNumberPadEnter : " + Keyboard2KeyNumberPadEnter + Environment.NewLine;
            str += "Keyboard2KeyRightControl : " + Keyboard2KeyRightControl + Environment.NewLine;
            str += "Keyboard2KeyMute : " + Keyboard2KeyMute + Environment.NewLine;
            str += "Keyboard2KeyCalculator : " + Keyboard2KeyCalculator + Environment.NewLine;
            str += "Keyboard2KeyPlayPause : " + Keyboard2KeyPlayPause + Environment.NewLine;
            str += "Keyboard2KeyMediaStop : " + Keyboard2KeyMediaStop + Environment.NewLine;
            str += "Keyboard2KeyVolumeDown : " + Keyboard2KeyVolumeDown + Environment.NewLine;
            str += "Keyboard2KeyVolumeUp : " + Keyboard2KeyVolumeUp + Environment.NewLine;
            str += "Keyboard2KeyWebHome : " + Keyboard2KeyWebHome + Environment.NewLine;
            str += "Keyboard2KeyNumberPadComma : " + Keyboard2KeyNumberPadComma + Environment.NewLine;
            str += "Keyboard2KeyDivide : " + Keyboard2KeyDivide + Environment.NewLine;
            str += "Keyboard2KeyPrintScreen : " + Keyboard2KeyPrintScreen + Environment.NewLine;
            str += "Keyboard2KeyRightAlt : " + Keyboard2KeyRightAlt + Environment.NewLine;
            str += "Keyboard2KeyPause : " + Keyboard2KeyPause + Environment.NewLine;
            str += "Keyboard2KeyHome : " + Keyboard2KeyHome + Environment.NewLine;
            str += "Keyboard2KeyUp : " + Keyboard2KeyUp + Environment.NewLine;
            str += "Keyboard2KeyPageUp : " + Keyboard2KeyPageUp + Environment.NewLine;
            str += "Keyboard2KeyLeft : " + Keyboard2KeyLeft + Environment.NewLine;
            str += "Keyboard2KeyRight : " + Keyboard2KeyRight + Environment.NewLine;
            str += "Keyboard2KeyEnd : " + Keyboard2KeyEnd + Environment.NewLine;
            str += "Keyboard2KeyDown : " + Keyboard2KeyDown + Environment.NewLine;
            str += "Keyboard2KeyPageDown : " + Keyboard2KeyPageDown + Environment.NewLine;
            str += "Keyboard2KeyInsert : " + Keyboard2KeyInsert + Environment.NewLine;
            str += "Keyboard2KeyDelete : " + Keyboard2KeyDelete + Environment.NewLine;
            str += "Keyboard2KeyLeftWindowsKey : " + Keyboard2KeyLeftWindowsKey + Environment.NewLine;
            str += "Keyboard2KeyRightWindowsKey : " + Keyboard2KeyRightWindowsKey + Environment.NewLine;
            str += "Keyboard2KeyApplications : " + Keyboard2KeyApplications + Environment.NewLine;
            str += "Keyboard2KeyPower : " + Keyboard2KeyPower + Environment.NewLine;
            str += "Keyboard2KeySleep : " + Keyboard2KeySleep + Environment.NewLine;
            str += "Keyboard2KeyWake : " + Keyboard2KeyWake + Environment.NewLine;
            str += "Keyboard2KeyWebSearch : " + Keyboard2KeyWebSearch + Environment.NewLine;
            str += "Keyboard2KeyWebFavorites : " + Keyboard2KeyWebFavorites + Environment.NewLine;
            str += "Keyboard2KeyWebRefresh : " + Keyboard2KeyWebRefresh + Environment.NewLine;
            str += "Keyboard2KeyWebStop : " + Keyboard2KeyWebStop + Environment.NewLine;
            str += "Keyboard2KeyWebForward : " + Keyboard2KeyWebForward + Environment.NewLine;
            str += "Keyboard2KeyWebBack : " + Keyboard2KeyWebBack + Environment.NewLine;
            str += "Keyboard2KeyMyComputer : " + Keyboard2KeyMyComputer + Environment.NewLine;
            str += "Keyboard2KeyMail : " + Keyboard2KeyMail + Environment.NewLine;
            str += "Keyboard2KeyMediaSelect : " + Keyboard2KeyMediaSelect + Environment.NewLine;
            str += "Keyboard2KeyUnknown : " + Keyboard2KeyUnknown + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form5.SetLabel3Text(str);
            }
            catch { }
        }
        private void SetXInputs()
        {
            string str = "Controller1ButtonAPressed : " + Controller1ButtonAPressed + Environment.NewLine;
            str += "Controller1ButtonBPressed : " + Controller1ButtonBPressed + Environment.NewLine;
            str += "Controller1ButtonXPressed : " + Controller1ButtonXPressed + Environment.NewLine;
            str += "Controller1ButtonYPressed : " + Controller1ButtonYPressed + Environment.NewLine;
            str += "Controller1ButtonStartPressed : " + Controller1ButtonStartPressed + Environment.NewLine;
            str += "Controller1ButtonBackPressed : " + Controller1ButtonBackPressed + Environment.NewLine;
            str += "Controller1ButtonDownPressed : " + Controller1ButtonDownPressed + Environment.NewLine;
            str += "Controller1ButtonUpPressed : " + Controller1ButtonUpPressed + Environment.NewLine;
            str += "Controller1ButtonLeftPressed : " + Controller1ButtonLeftPressed + Environment.NewLine;
            str += "Controller1ButtonRightPressed : " + Controller1ButtonRightPressed + Environment.NewLine;
            str += "Controller1ButtonShoulderLeftPressed : " + Controller1ButtonShoulderLeftPressed + Environment.NewLine;
            str += "Controller1ButtonShoulderRightPressed : " + Controller1ButtonShoulderRightPressed + Environment.NewLine;
            str += "Controller1ThumbpadLeftPressed : " + Controller1ThumbpadLeftPressed + Environment.NewLine;
            str += "Controller1ThumbpadRightPressed : " + Controller1ThumbpadRightPressed + Environment.NewLine;
            str += "Controller1TriggerLeftPosition : " + Controller1TriggerLeftPosition + Environment.NewLine;
            str += "Controller1TriggerRightPosition : " + Controller1TriggerRightPosition + Environment.NewLine;
            str += "Controller1ThumbLeftX : " + Controller1ThumbLeftX + Environment.NewLine;
            str += "Controller1ThumbLeftY : " + Controller1ThumbLeftY + Environment.NewLine;
            str += "Controller1ThumbRightX : " + Controller1ThumbRightX + Environment.NewLine;
            str += "Controller1ThumbRightY : " + Controller1ThumbRightY + Environment.NewLine;
            str += "Controller1GyroX : " + Controller1GyroX + Environment.NewLine;
            str += "Controller1GyroY : " + Controller1GyroY + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form7.SetLabel1Text(str);
            }
            catch { }
            str = "Controller2ButtonAPressed : " + Controller2ButtonAPressed + Environment.NewLine;
            str += "Controller2ButtonBPressed : " + Controller2ButtonBPressed + Environment.NewLine;
            str += "Controller2ButtonXPressed : " + Controller2ButtonXPressed + Environment.NewLine;
            str += "Controller2ButtonYPressed : " + Controller2ButtonYPressed + Environment.NewLine;
            str += "Controller2ButtonStartPressed : " + Controller2ButtonStartPressed + Environment.NewLine;
            str += "Controller2ButtonBackPressed : " + Controller2ButtonBackPressed + Environment.NewLine;
            str += "Controller2ButtonDownPressed : " + Controller2ButtonDownPressed + Environment.NewLine;
            str += "Controller2ButtonUpPressed : " + Controller2ButtonUpPressed + Environment.NewLine;
            str += "Controller2ButtonLeftPressed : " + Controller2ButtonLeftPressed + Environment.NewLine;
            str += "Controller2ButtonRightPressed : " + Controller2ButtonRightPressed + Environment.NewLine;
            str += "Controller2ButtonShoulderLeftPressed : " + Controller2ButtonShoulderLeftPressed + Environment.NewLine;
            str += "Controller2ButtonShoulderRightPressed : " + Controller2ButtonShoulderRightPressed + Environment.NewLine;
            str += "Controller2ThumbpadLeftPressed : " + Controller2ThumbpadLeftPressed + Environment.NewLine;
            str += "Controller2ThumbpadRightPressed : " + Controller2ThumbpadRightPressed + Environment.NewLine;
            str += "Controller2TriggerLeftPosition : " + Controller2TriggerLeftPosition + Environment.NewLine;
            str += "Controller2TriggerRightPosition : " + Controller2TriggerRightPosition + Environment.NewLine;
            str += "Controller2ThumbLeftX : " + Controller2ThumbLeftX + Environment.NewLine;
            str += "Controller2ThumbLeftY : " + Controller2ThumbLeftY + Environment.NewLine;
            str += "Controller2ThumbRightX : " + Controller2ThumbRightX + Environment.NewLine;
            str += "Controller2ThumbRightY : " + Controller2ThumbRightY + Environment.NewLine;
            str += "Controller2GyroX : " + Controller2GyroX + Environment.NewLine;
            str += "Controller2GyroY : " + Controller2GyroY + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form7.SetLabel2Text(str);
            }
            catch { }
        }
        private void SetDirectInputs()
        {
            string str = "Joystick1AxisX : " + Joystick1AxisX + Environment.NewLine;
            str += "Joystick1AxisY : " + Joystick1AxisY + Environment.NewLine;
            str += "Joystick1AxisZ : " + Joystick1AxisZ + Environment.NewLine;
            str += "Joystick1RotationX : " + Joystick1RotationX + Environment.NewLine;
            str += "Joystick1RotationY : " + Joystick1RotationY + Environment.NewLine;
            str += "Joystick1RotationZ : " + Joystick1RotationZ + Environment.NewLine;
            str += "Joystick1Sliders0 : " + Joystick1Sliders0 + Environment.NewLine;
            str += "Joystick1Sliders1 : " + Joystick1Sliders1 + Environment.NewLine;
            str += "Joystick1PointOfViewControllers0 : " + Joystick1PointOfViewControllers0 + Environment.NewLine;
            str += "Joystick1PointOfViewControllers1 : " + Joystick1PointOfViewControllers1 + Environment.NewLine;
            str += "Joystick1PointOfViewControllers2 : " + Joystick1PointOfViewControllers2 + Environment.NewLine;
            str += "Joystick1PointOfViewControllers3 : " + Joystick1PointOfViewControllers3 + Environment.NewLine;
            str += "Joystick1VelocityX : " + Joystick1VelocityX + Environment.NewLine;
            str += "Joystick1VelocityY : " + Joystick1VelocityY + Environment.NewLine;
            str += "Joystick1VelocityZ : " + Joystick1VelocityZ + Environment.NewLine;
            str += "Joystick1AngularVelocityX : " + Joystick1AngularVelocityX + Environment.NewLine;
            str += "Joystick1AngularVelocityY : " + Joystick1AngularVelocityY + Environment.NewLine;
            str += "Joystick1AngularVelocityZ : " + Joystick1AngularVelocityZ + Environment.NewLine;
            str += "Joystick1VelocitySliders0 : " + Joystick1VelocitySliders0 + Environment.NewLine;
            str += "Joystick1VelocitySliders1 : " + Joystick1VelocitySliders1 + Environment.NewLine;
            str += "Joystick1AccelerationX : " + Joystick1AccelerationX + Environment.NewLine;
            str += "Joystick1AccelerationY : " + Joystick1AccelerationY + Environment.NewLine;
            str += "Joystick1AccelerationZ : " + Joystick1AccelerationZ + Environment.NewLine;
            str += "Joystick1AngularAccelerationX : " + Joystick1AngularAccelerationX + Environment.NewLine;
            str += "Joystick1AngularAccelerationY : " + Joystick1AngularAccelerationY + Environment.NewLine;
            str += "Joystick1AngularAccelerationZ : " + Joystick1AngularAccelerationZ + Environment.NewLine;
            str += "Joystick1AccelerationSliders0 : " + Joystick1AccelerationSliders0 + Environment.NewLine;
            str += "Joystick1AccelerationSliders1 : " + Joystick1AccelerationSliders1 + Environment.NewLine;
            str += "Joystick1ForceX : " + Joystick1ForceX + Environment.NewLine;
            str += "Joystick1ForceY : " + Joystick1ForceY + Environment.NewLine;
            str += "Joystick1ForceZ : " + Joystick1ForceZ + Environment.NewLine;
            str += "Joystick1TorqueX : " + Joystick1TorqueX + Environment.NewLine;
            str += "Joystick1TorqueY : " + Joystick1TorqueY + Environment.NewLine;
            str += "Joystick1TorqueZ : " + Joystick1TorqueZ + Environment.NewLine;
            str += "Joystick1ForceSliders0 : " + Joystick1ForceSliders0 + Environment.NewLine;
            str += "Joystick1ForceSliders1 : " + Joystick1ForceSliders1 + Environment.NewLine;
            str += "Joystick1Buttons0 : " + Joystick1Buttons0 + Environment.NewLine;
            str += "Joystick1Buttons1 : " + Joystick1Buttons1 + Environment.NewLine;
            str += "Joystick1Buttons2 : " + Joystick1Buttons2 + Environment.NewLine;
            str += "Joystick1Buttons3 : " + Joystick1Buttons3 + Environment.NewLine;
            str += "Joystick1Buttons4 : " + Joystick1Buttons4 + Environment.NewLine;
            str += "Joystick1Buttons5 : " + Joystick1Buttons5 + Environment.NewLine;
            str += "Joystick1Buttons6 : " + Joystick1Buttons6 + Environment.NewLine;
            str += "Joystick1Buttons7 : " + Joystick1Buttons7 + Environment.NewLine;
            str += "Joystick1Buttons8 : " + Joystick1Buttons8 + Environment.NewLine;
            str += "Joystick1Buttons9 : " + Joystick1Buttons9 + Environment.NewLine;
            str += "Joystick1Buttons10 : " + Joystick1Buttons10 + Environment.NewLine;
            str += "Joystick1Buttons11 : " + Joystick1Buttons11 + Environment.NewLine;
            str += "Joystick1Buttons12 : " + Joystick1Buttons12 + Environment.NewLine;
            str += "Joystick1Buttons13 : " + Joystick1Buttons13 + Environment.NewLine;
            str += "Joystick1Buttons14 : " + Joystick1Buttons14 + Environment.NewLine;
            str += "Joystick1Buttons15 : " + Joystick1Buttons15 + Environment.NewLine;
            str += "Joystick1Buttons16 : " + Joystick1Buttons16 + Environment.NewLine;
            str += "Joystick1Buttons17 : " + Joystick1Buttons17 + Environment.NewLine;
            str += "Joystick1Buttons18 : " + Joystick1Buttons18 + Environment.NewLine;
            str += "Joystick1Buttons19 : " + Joystick1Buttons19 + Environment.NewLine;
            str += "Joystick1Buttons20 : " + Joystick1Buttons20 + Environment.NewLine;
            str += "Joystick1Buttons21 : " + Joystick1Buttons21 + Environment.NewLine;
            str += "Joystick1Buttons22 : " + Joystick1Buttons22 + Environment.NewLine;
            str += "Joystick1Buttons23 : " + Joystick1Buttons23 + Environment.NewLine;
            str += "Joystick1Buttons24 : " + Joystick1Buttons24 + Environment.NewLine;
            str += "Joystick1Buttons25 : " + Joystick1Buttons25 + Environment.NewLine;
            str += "Joystick1Buttons26 : " + Joystick1Buttons26 + Environment.NewLine;
            str += "Joystick1Buttons27 : " + Joystick1Buttons27 + Environment.NewLine;
            str += "Joystick1Buttons28 : " + Joystick1Buttons28 + Environment.NewLine;
            str += "Joystick1Buttons29 : " + Joystick1Buttons29 + Environment.NewLine;
            str += "Joystick1Buttons30 : " + Joystick1Buttons30 + Environment.NewLine;
            str += "Joystick1Buttons31 : " + Joystick1Buttons31 + Environment.NewLine;
            str += "Joystick1Buttons32 : " + Joystick1Buttons32 + Environment.NewLine;
            str += "Joystick1Buttons33 : " + Joystick1Buttons33 + Environment.NewLine;
            str += "Joystick1Buttons34 : " + Joystick1Buttons34 + Environment.NewLine;
            str += "Joystick1Buttons35 : " + Joystick1Buttons35 + Environment.NewLine;
            str += "Joystick1Buttons36 : " + Joystick1Buttons36 + Environment.NewLine;
            str += "Joystick1Buttons37 : " + Joystick1Buttons37 + Environment.NewLine;
            str += "Joystick1Buttons38 : " + Joystick1Buttons38 + Environment.NewLine;
            str += "Joystick1Buttons39 : " + Joystick1Buttons39 + Environment.NewLine;
            str += "Joystick1Buttons40 : " + Joystick1Buttons40 + Environment.NewLine;
            str += "Joystick1Buttons41 : " + Joystick1Buttons41 + Environment.NewLine;
            str += "Joystick1Buttons42 : " + Joystick1Buttons42 + Environment.NewLine;
            str += "Joystick1Buttons43 : " + Joystick1Buttons43 + Environment.NewLine;
            str += "Joystick1Buttons44 : " + Joystick1Buttons44 + Environment.NewLine;
            str += "Joystick1Buttons45 : " + Joystick1Buttons45 + Environment.NewLine;
            str += "Joystick1Buttons46 : " + Joystick1Buttons46 + Environment.NewLine;
            str += "Joystick1Buttons47 : " + Joystick1Buttons47 + Environment.NewLine;
            str += "Joystick1Buttons48 : " + Joystick1Buttons48 + Environment.NewLine;
            str += "Joystick1Buttons49 : " + Joystick1Buttons49 + Environment.NewLine;
            str += "Joystick1Buttons50 : " + Joystick1Buttons50 + Environment.NewLine;
            str += "Joystick1Buttons51 : " + Joystick1Buttons51 + Environment.NewLine;
            str += "Joystick1Buttons52 : " + Joystick1Buttons52 + Environment.NewLine;
            str += "Joystick1Buttons53 : " + Joystick1Buttons53 + Environment.NewLine;
            str += "Joystick1Buttons54 : " + Joystick1Buttons54 + Environment.NewLine;
            str += "Joystick1Buttons55 : " + Joystick1Buttons55 + Environment.NewLine;
            str += "Joystick1Buttons56 : " + Joystick1Buttons56 + Environment.NewLine;
            str += "Joystick1Buttons57 : " + Joystick1Buttons57 + Environment.NewLine;
            str += "Joystick1Buttons58 : " + Joystick1Buttons58 + Environment.NewLine;
            str += "Joystick1Buttons59 : " + Joystick1Buttons59 + Environment.NewLine;
            str += "Joystick1Buttons60 : " + Joystick1Buttons60 + Environment.NewLine;
            str += "Joystick1Buttons61 : " + Joystick1Buttons61 + Environment.NewLine;
            str += "Joystick1Buttons62 : " + Joystick1Buttons62 + Environment.NewLine;
            str += "Joystick1Buttons63 : " + Joystick1Buttons63 + Environment.NewLine;
            str += "Joystick1Buttons64 : " + Joystick1Buttons64 + Environment.NewLine;
            str += "Joystick1Buttons65 : " + Joystick1Buttons65 + Environment.NewLine;
            str += "Joystick1Buttons66 : " + Joystick1Buttons66 + Environment.NewLine;
            str += "Joystick1Buttons67 : " + Joystick1Buttons67 + Environment.NewLine;
            str += "Joystick1Buttons68 : " + Joystick1Buttons68 + Environment.NewLine;
            str += "Joystick1Buttons69 : " + Joystick1Buttons69 + Environment.NewLine;
            str += "Joystick1Buttons70 : " + Joystick1Buttons70 + Environment.NewLine;
            str += "Joystick1Buttons71 : " + Joystick1Buttons71 + Environment.NewLine;
            str += "Joystick1Buttons72 : " + Joystick1Buttons72 + Environment.NewLine;
            str += "Joystick1Buttons73 : " + Joystick1Buttons73 + Environment.NewLine;
            str += "Joystick1Buttons74 : " + Joystick1Buttons74 + Environment.NewLine;
            str += "Joystick1Buttons75 : " + Joystick1Buttons75 + Environment.NewLine;
            str += "Joystick1Buttons76 : " + Joystick1Buttons76 + Environment.NewLine;
            str += "Joystick1Buttons77 : " + Joystick1Buttons77 + Environment.NewLine;
            str += "Joystick1Buttons78 : " + Joystick1Buttons78 + Environment.NewLine;
            str += "Joystick1Buttons79 : " + Joystick1Buttons79 + Environment.NewLine;
            str += "Joystick1Buttons80 : " + Joystick1Buttons80 + Environment.NewLine;
            str += "Joystick1Buttons81 : " + Joystick1Buttons81 + Environment.NewLine;
            str += "Joystick1Buttons82 : " + Joystick1Buttons82 + Environment.NewLine;
            str += "Joystick1Buttons83 : " + Joystick1Buttons83 + Environment.NewLine;
            str += "Joystick1Buttons84 : " + Joystick1Buttons84 + Environment.NewLine;
            str += "Joystick1Buttons85 : " + Joystick1Buttons85 + Environment.NewLine;
            str += "Joystick1Buttons86 : " + Joystick1Buttons86 + Environment.NewLine;
            str += "Joystick1Buttons87 : " + Joystick1Buttons87 + Environment.NewLine;
            str += "Joystick1Buttons88 : " + Joystick1Buttons88 + Environment.NewLine;
            str += "Joystick1Buttons89 : " + Joystick1Buttons89 + Environment.NewLine;
            str += "Joystick1Buttons90 : " + Joystick1Buttons90 + Environment.NewLine;
            str += "Joystick1Buttons91 : " + Joystick1Buttons91 + Environment.NewLine;
            str += "Joystick1Buttons92 : " + Joystick1Buttons92 + Environment.NewLine;
            str += "Joystick1Buttons93 : " + Joystick1Buttons93 + Environment.NewLine;
            str += "Joystick1Buttons94 : " + Joystick1Buttons94 + Environment.NewLine;
            str += "Joystick1Buttons95 : " + Joystick1Buttons95 + Environment.NewLine;
            str += "Joystick1Buttons96 : " + Joystick1Buttons96 + Environment.NewLine;
            str += "Joystick1Buttons97 : " + Joystick1Buttons97 + Environment.NewLine;
            str += "Joystick1Buttons98 : " + Joystick1Buttons98 + Environment.NewLine;
            str += "Joystick1Buttons99 : " + Joystick1Buttons99 + Environment.NewLine;
            str += "Joystick1Buttons100 : " + Joystick1Buttons100 + Environment.NewLine;
            str += "Joystick1Buttons101 : " + Joystick1Buttons101 + Environment.NewLine;
            str += "Joystick1Buttons102 : " + Joystick1Buttons102 + Environment.NewLine;
            str += "Joystick1Buttons103 : " + Joystick1Buttons103 + Environment.NewLine;
            str += "Joystick1Buttons104 : " + Joystick1Buttons104 + Environment.NewLine;
            str += "Joystick1Buttons105 : " + Joystick1Buttons105 + Environment.NewLine;
            str += "Joystick1Buttons106 : " + Joystick1Buttons106 + Environment.NewLine;
            str += "Joystick1Buttons107 : " + Joystick1Buttons107 + Environment.NewLine;
            str += "Joystick1Buttons108 : " + Joystick1Buttons108 + Environment.NewLine;
            str += "Joystick1Buttons109 : " + Joystick1Buttons109 + Environment.NewLine;
            str += "Joystick1Buttons110 : " + Joystick1Buttons110 + Environment.NewLine;
            str += "Joystick1Buttons111 : " + Joystick1Buttons111 + Environment.NewLine;
            str += "Joystick1Buttons112 : " + Joystick1Buttons112 + Environment.NewLine;
            str += "Joystick1Buttons113 : " + Joystick1Buttons113 + Environment.NewLine;
            str += "Joystick1Buttons114 : " + Joystick1Buttons114 + Environment.NewLine;
            str += "Joystick1Buttons115 : " + Joystick1Buttons115 + Environment.NewLine;
            str += "Joystick1Buttons116 : " + Joystick1Buttons116 + Environment.NewLine;
            str += "Joystick1Buttons117 : " + Joystick1Buttons117 + Environment.NewLine;
            str += "Joystick1Buttons118 : " + Joystick1Buttons118 + Environment.NewLine;
            str += "Joystick1Buttons119 : " + Joystick1Buttons119 + Environment.NewLine;
            str += "Joystick1Buttons120 : " + Joystick1Buttons120 + Environment.NewLine;
            str += "Joystick1Buttons121 : " + Joystick1Buttons121 + Environment.NewLine;
            str += "Joystick1Buttons122 : " + Joystick1Buttons122 + Environment.NewLine;
            str += "Joystick1Buttons123 : " + Joystick1Buttons123 + Environment.NewLine;
            str += "Joystick1Buttons124 : " + Joystick1Buttons124 + Environment.NewLine;
            str += "Joystick1Buttons125 : " + Joystick1Buttons125 + Environment.NewLine;
            str += "Joystick1Buttons126 : " + Joystick1Buttons126 + Environment.NewLine;
            str += "Joystick1Buttons127 : " + Joystick1Buttons127 + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form8.SetLabel1Text(str);
            }
            catch { }
            str = "Joystick2AxisX : " + Joystick2AxisX + Environment.NewLine;
            str += "Joystick2AxisY : " + Joystick2AxisY + Environment.NewLine;
            str += "Joystick2AxisZ : " + Joystick2AxisZ + Environment.NewLine;
            str += "Joystick2RotationX : " + Joystick2RotationX + Environment.NewLine;
            str += "Joystick2RotationY : " + Joystick2RotationY + Environment.NewLine;
            str += "Joystick2RotationZ : " + Joystick2RotationZ + Environment.NewLine;
            str += "Joystick2Sliders0 : " + Joystick2Sliders0 + Environment.NewLine;
            str += "Joystick2Sliders1 : " + Joystick2Sliders1 + Environment.NewLine;
            str += "Joystick2PointOfViewControllers0 : " + Joystick2PointOfViewControllers0 + Environment.NewLine;
            str += "Joystick2PointOfViewControllers1 : " + Joystick2PointOfViewControllers1 + Environment.NewLine;
            str += "Joystick2PointOfViewControllers2 : " + Joystick2PointOfViewControllers2 + Environment.NewLine;
            str += "Joystick2PointOfViewControllers3 : " + Joystick2PointOfViewControllers3 + Environment.NewLine;
            str += "Joystick2VelocityX : " + Joystick2VelocityX + Environment.NewLine;
            str += "Joystick2VelocityY : " + Joystick2VelocityY + Environment.NewLine;
            str += "Joystick2VelocityZ : " + Joystick2VelocityZ + Environment.NewLine;
            str += "Joystick2AngularVelocityX : " + Joystick2AngularVelocityX + Environment.NewLine;
            str += "Joystick2AngularVelocityY : " + Joystick2AngularVelocityY + Environment.NewLine;
            str += "Joystick2AngularVelocityZ : " + Joystick2AngularVelocityZ + Environment.NewLine;
            str += "Joystick2VelocitySliders0 : " + Joystick2VelocitySliders0 + Environment.NewLine;
            str += "Joystick2VelocitySliders1 : " + Joystick2VelocitySliders1 + Environment.NewLine;
            str += "Joystick2AccelerationX : " + Joystick2AccelerationX + Environment.NewLine;
            str += "Joystick2AccelerationY : " + Joystick2AccelerationY + Environment.NewLine;
            str += "Joystick2AccelerationZ : " + Joystick2AccelerationZ + Environment.NewLine;
            str += "Joystick2AngularAccelerationX : " + Joystick2AngularAccelerationX + Environment.NewLine;
            str += "Joystick2AngularAccelerationY : " + Joystick2AngularAccelerationY + Environment.NewLine;
            str += "Joystick2AngularAccelerationZ : " + Joystick2AngularAccelerationZ + Environment.NewLine;
            str += "Joystick2AccelerationSliders0 : " + Joystick2AccelerationSliders0 + Environment.NewLine;
            str += "Joystick2AccelerationSliders1 : " + Joystick2AccelerationSliders1 + Environment.NewLine;
            str += "Joystick2ForceX : " + Joystick2ForceX + Environment.NewLine;
            str += "Joystick2ForceY : " + Joystick2ForceY + Environment.NewLine;
            str += "Joystick2ForceZ : " + Joystick2ForceZ + Environment.NewLine;
            str += "Joystick2TorqueX : " + Joystick2TorqueX + Environment.NewLine;
            str += "Joystick2TorqueY : " + Joystick2TorqueY + Environment.NewLine;
            str += "Joystick2TorqueZ : " + Joystick2TorqueZ + Environment.NewLine;
            str += "Joystick2ForceSliders0 : " + Joystick2ForceSliders0 + Environment.NewLine;
            str += "Joystick2ForceSliders1 : " + Joystick2ForceSliders1 + Environment.NewLine;
            str += "Joystick2Buttons0 : " + Joystick2Buttons0 + Environment.NewLine;
            str += "Joystick2Buttons1 : " + Joystick2Buttons1 + Environment.NewLine;
            str += "Joystick2Buttons2 : " + Joystick2Buttons2 + Environment.NewLine;
            str += "Joystick2Buttons3 : " + Joystick2Buttons3 + Environment.NewLine;
            str += "Joystick2Buttons4 : " + Joystick2Buttons4 + Environment.NewLine;
            str += "Joystick2Buttons5 : " + Joystick2Buttons5 + Environment.NewLine;
            str += "Joystick2Buttons6 : " + Joystick2Buttons6 + Environment.NewLine;
            str += "Joystick2Buttons7 : " + Joystick2Buttons7 + Environment.NewLine;
            str += "Joystick2Buttons8 : " + Joystick2Buttons8 + Environment.NewLine;
            str += "Joystick2Buttons9 : " + Joystick2Buttons9 + Environment.NewLine;
            str += "Joystick2Buttons10 : " + Joystick2Buttons10 + Environment.NewLine;
            str += "Joystick2Buttons11 : " + Joystick2Buttons11 + Environment.NewLine;
            str += "Joystick2Buttons12 : " + Joystick2Buttons12 + Environment.NewLine;
            str += "Joystick2Buttons13 : " + Joystick2Buttons13 + Environment.NewLine;
            str += "Joystick2Buttons14 : " + Joystick2Buttons14 + Environment.NewLine;
            str += "Joystick2Buttons15 : " + Joystick2Buttons15 + Environment.NewLine;
            str += "Joystick2Buttons16 : " + Joystick2Buttons16 + Environment.NewLine;
            str += "Joystick2Buttons17 : " + Joystick2Buttons17 + Environment.NewLine;
            str += "Joystick2Buttons18 : " + Joystick2Buttons18 + Environment.NewLine;
            str += "Joystick2Buttons19 : " + Joystick2Buttons19 + Environment.NewLine;
            str += "Joystick2Buttons20 : " + Joystick2Buttons20 + Environment.NewLine;
            str += "Joystick2Buttons21 : " + Joystick2Buttons21 + Environment.NewLine;
            str += "Joystick2Buttons22 : " + Joystick2Buttons22 + Environment.NewLine;
            str += "Joystick2Buttons23 : " + Joystick2Buttons23 + Environment.NewLine;
            str += "Joystick2Buttons24 : " + Joystick2Buttons24 + Environment.NewLine;
            str += "Joystick2Buttons25 : " + Joystick2Buttons25 + Environment.NewLine;
            str += "Joystick2Buttons26 : " + Joystick2Buttons26 + Environment.NewLine;
            str += "Joystick2Buttons27 : " + Joystick2Buttons27 + Environment.NewLine;
            str += "Joystick2Buttons28 : " + Joystick2Buttons28 + Environment.NewLine;
            str += "Joystick2Buttons29 : " + Joystick2Buttons29 + Environment.NewLine;
            str += "Joystick2Buttons30 : " + Joystick2Buttons30 + Environment.NewLine;
            str += "Joystick2Buttons31 : " + Joystick2Buttons31 + Environment.NewLine;
            str += "Joystick2Buttons32 : " + Joystick2Buttons32 + Environment.NewLine;
            str += "Joystick2Buttons33 : " + Joystick2Buttons33 + Environment.NewLine;
            str += "Joystick2Buttons34 : " + Joystick2Buttons34 + Environment.NewLine;
            str += "Joystick2Buttons35 : " + Joystick2Buttons35 + Environment.NewLine;
            str += "Joystick2Buttons36 : " + Joystick2Buttons36 + Environment.NewLine;
            str += "Joystick2Buttons37 : " + Joystick2Buttons37 + Environment.NewLine;
            str += "Joystick2Buttons38 : " + Joystick2Buttons38 + Environment.NewLine;
            str += "Joystick2Buttons39 : " + Joystick2Buttons39 + Environment.NewLine;
            str += "Joystick2Buttons40 : " + Joystick2Buttons40 + Environment.NewLine;
            str += "Joystick2Buttons41 : " + Joystick2Buttons41 + Environment.NewLine;
            str += "Joystick2Buttons42 : " + Joystick2Buttons42 + Environment.NewLine;
            str += "Joystick2Buttons43 : " + Joystick2Buttons43 + Environment.NewLine;
            str += "Joystick2Buttons44 : " + Joystick2Buttons44 + Environment.NewLine;
            str += "Joystick2Buttons45 : " + Joystick2Buttons45 + Environment.NewLine;
            str += "Joystick2Buttons46 : " + Joystick2Buttons46 + Environment.NewLine;
            str += "Joystick2Buttons47 : " + Joystick2Buttons47 + Environment.NewLine;
            str += "Joystick2Buttons48 : " + Joystick2Buttons48 + Environment.NewLine;
            str += "Joystick2Buttons49 : " + Joystick2Buttons49 + Environment.NewLine;
            str += "Joystick2Buttons50 : " + Joystick2Buttons50 + Environment.NewLine;
            str += "Joystick2Buttons51 : " + Joystick2Buttons51 + Environment.NewLine;
            str += "Joystick2Buttons52 : " + Joystick2Buttons52 + Environment.NewLine;
            str += "Joystick2Buttons53 : " + Joystick2Buttons53 + Environment.NewLine;
            str += "Joystick2Buttons54 : " + Joystick2Buttons54 + Environment.NewLine;
            str += "Joystick2Buttons55 : " + Joystick2Buttons55 + Environment.NewLine;
            str += "Joystick2Buttons56 : " + Joystick2Buttons56 + Environment.NewLine;
            str += "Joystick2Buttons57 : " + Joystick2Buttons57 + Environment.NewLine;
            str += "Joystick2Buttons58 : " + Joystick2Buttons58 + Environment.NewLine;
            str += "Joystick2Buttons59 : " + Joystick2Buttons59 + Environment.NewLine;
            str += "Joystick2Buttons60 : " + Joystick2Buttons60 + Environment.NewLine;
            str += "Joystick2Buttons61 : " + Joystick2Buttons61 + Environment.NewLine;
            str += "Joystick2Buttons62 : " + Joystick2Buttons62 + Environment.NewLine;
            str += "Joystick2Buttons63 : " + Joystick2Buttons63 + Environment.NewLine;
            str += "Joystick2Buttons64 : " + Joystick2Buttons64 + Environment.NewLine;
            str += "Joystick2Buttons65 : " + Joystick2Buttons65 + Environment.NewLine;
            str += "Joystick2Buttons66 : " + Joystick2Buttons66 + Environment.NewLine;
            str += "Joystick2Buttons67 : " + Joystick2Buttons67 + Environment.NewLine;
            str += "Joystick2Buttons68 : " + Joystick2Buttons68 + Environment.NewLine;
            str += "Joystick2Buttons69 : " + Joystick2Buttons69 + Environment.NewLine;
            str += "Joystick2Buttons70 : " + Joystick2Buttons70 + Environment.NewLine;
            str += "Joystick2Buttons71 : " + Joystick2Buttons71 + Environment.NewLine;
            str += "Joystick2Buttons72 : " + Joystick2Buttons72 + Environment.NewLine;
            str += "Joystick2Buttons73 : " + Joystick2Buttons73 + Environment.NewLine;
            str += "Joystick2Buttons74 : " + Joystick2Buttons74 + Environment.NewLine;
            str += "Joystick2Buttons75 : " + Joystick2Buttons75 + Environment.NewLine;
            str += "Joystick2Buttons76 : " + Joystick2Buttons76 + Environment.NewLine;
            str += "Joystick2Buttons77 : " + Joystick2Buttons77 + Environment.NewLine;
            str += "Joystick2Buttons78 : " + Joystick2Buttons78 + Environment.NewLine;
            str += "Joystick2Buttons79 : " + Joystick2Buttons79 + Environment.NewLine;
            str += "Joystick2Buttons80 : " + Joystick2Buttons80 + Environment.NewLine;
            str += "Joystick2Buttons81 : " + Joystick2Buttons81 + Environment.NewLine;
            str += "Joystick2Buttons82 : " + Joystick2Buttons82 + Environment.NewLine;
            str += "Joystick2Buttons83 : " + Joystick2Buttons83 + Environment.NewLine;
            str += "Joystick2Buttons84 : " + Joystick2Buttons84 + Environment.NewLine;
            str += "Joystick2Buttons85 : " + Joystick2Buttons85 + Environment.NewLine;
            str += "Joystick2Buttons86 : " + Joystick2Buttons86 + Environment.NewLine;
            str += "Joystick2Buttons87 : " + Joystick2Buttons87 + Environment.NewLine;
            str += "Joystick2Buttons88 : " + Joystick2Buttons88 + Environment.NewLine;
            str += "Joystick2Buttons89 : " + Joystick2Buttons89 + Environment.NewLine;
            str += "Joystick2Buttons90 : " + Joystick2Buttons90 + Environment.NewLine;
            str += "Joystick2Buttons91 : " + Joystick2Buttons91 + Environment.NewLine;
            str += "Joystick2Buttons92 : " + Joystick2Buttons92 + Environment.NewLine;
            str += "Joystick2Buttons93 : " + Joystick2Buttons93 + Environment.NewLine;
            str += "Joystick2Buttons94 : " + Joystick2Buttons94 + Environment.NewLine;
            str += "Joystick2Buttons95 : " + Joystick2Buttons95 + Environment.NewLine;
            str += "Joystick2Buttons96 : " + Joystick2Buttons96 + Environment.NewLine;
            str += "Joystick2Buttons97 : " + Joystick2Buttons97 + Environment.NewLine;
            str += "Joystick2Buttons98 : " + Joystick2Buttons98 + Environment.NewLine;
            str += "Joystick2Buttons99 : " + Joystick2Buttons99 + Environment.NewLine;
            str += "Joystick2Buttons100 : " + Joystick2Buttons100 + Environment.NewLine;
            str += "Joystick2Buttons101 : " + Joystick2Buttons101 + Environment.NewLine;
            str += "Joystick2Buttons102 : " + Joystick2Buttons102 + Environment.NewLine;
            str += "Joystick2Buttons103 : " + Joystick2Buttons103 + Environment.NewLine;
            str += "Joystick2Buttons104 : " + Joystick2Buttons104 + Environment.NewLine;
            str += "Joystick2Buttons105 : " + Joystick2Buttons105 + Environment.NewLine;
            str += "Joystick2Buttons106 : " + Joystick2Buttons106 + Environment.NewLine;
            str += "Joystick2Buttons107 : " + Joystick2Buttons107 + Environment.NewLine;
            str += "Joystick2Buttons108 : " + Joystick2Buttons108 + Environment.NewLine;
            str += "Joystick2Buttons109 : " + Joystick2Buttons109 + Environment.NewLine;
            str += "Joystick2Buttons110 : " + Joystick2Buttons110 + Environment.NewLine;
            str += "Joystick2Buttons111 : " + Joystick2Buttons111 + Environment.NewLine;
            str += "Joystick2Buttons112 : " + Joystick2Buttons112 + Environment.NewLine;
            str += "Joystick2Buttons113 : " + Joystick2Buttons113 + Environment.NewLine;
            str += "Joystick2Buttons114 : " + Joystick2Buttons114 + Environment.NewLine;
            str += "Joystick2Buttons115 : " + Joystick2Buttons115 + Environment.NewLine;
            str += "Joystick2Buttons116 : " + Joystick2Buttons116 + Environment.NewLine;
            str += "Joystick2Buttons117 : " + Joystick2Buttons117 + Environment.NewLine;
            str += "Joystick2Buttons118 : " + Joystick2Buttons118 + Environment.NewLine;
            str += "Joystick2Buttons119 : " + Joystick2Buttons119 + Environment.NewLine;
            str += "Joystick2Buttons120 : " + Joystick2Buttons120 + Environment.NewLine;
            str += "Joystick2Buttons121 : " + Joystick2Buttons121 + Environment.NewLine;
            str += "Joystick2Buttons122 : " + Joystick2Buttons122 + Environment.NewLine;
            str += "Joystick2Buttons123 : " + Joystick2Buttons123 + Environment.NewLine;
            str += "Joystick2Buttons124 : " + Joystick2Buttons124 + Environment.NewLine;
            str += "Joystick2Buttons125 : " + Joystick2Buttons125 + Environment.NewLine;
            str += "Joystick2Buttons126 : " + Joystick2Buttons126 + Environment.NewLine;
            str += "Joystick2Buttons127 : " + Joystick2Buttons127 + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form8.SetLabel2Text(str);
            }
            catch { }
        }
        private void SetExtraInputs()
        {
            string str = "TextFromSpeech : " + TextFromSpeech + Environment.NewLine;
            str += "camx : " + camx + Environment.NewLine;
            str += "camy : " + camy + Environment.NewLine;
            str += "irx : " + irx + Environment.NewLine;
            str += "iry : " + iry + Environment.NewLine;
            str += "WiimoteButtonStateA : " + WiimoteButtonStateA + Environment.NewLine;
            str += "WiimoteButtonStateB : " + WiimoteButtonStateB + Environment.NewLine;
            str += "WiimoteButtonStateMinus : " + WiimoteButtonStateMinus + Environment.NewLine;
            str += "WiimoteButtonStateHome : " + WiimoteButtonStateHome + Environment.NewLine;
            str += "WiimoteButtonStatePlus : " + WiimoteButtonStatePlus + Environment.NewLine;
            str += "WiimoteButtonStateOne : " + WiimoteButtonStateOne + Environment.NewLine;
            str += "WiimoteButtonStateTwo : " + WiimoteButtonStateTwo + Environment.NewLine;
            str += "WiimoteButtonStateUp : " + WiimoteButtonStateUp + Environment.NewLine;
            str += "WiimoteButtonStateDown : " + WiimoteButtonStateDown + Environment.NewLine;
            str += "WiimoteButtonStateLeft : " + WiimoteButtonStateLeft + Environment.NewLine;
            str += "WiimoteButtonStateRight : " + WiimoteButtonStateRight + Environment.NewLine;
            str += "WiimoteRawValuesX : " + WiimoteRawValuesX + Environment.NewLine;
            str += "WiimoteRawValuesY : " + WiimoteRawValuesY + Environment.NewLine;
            str += "WiimoteRawValuesZ : " + WiimoteRawValuesZ + Environment.NewLine;
            str += "WiimoteNunchuckStateRawJoystickX : " + WiimoteNunchuckStateRawJoystickX + Environment.NewLine;
            str += "WiimoteNunchuckStateRawJoystickY : " + WiimoteNunchuckStateRawJoystickY + Environment.NewLine;
            str += "WiimoteNunchuckStateRawValuesX : " + WiimoteNunchuckStateRawValuesX + Environment.NewLine;
            str += "WiimoteNunchuckStateRawValuesY : " + WiimoteNunchuckStateRawValuesY + Environment.NewLine;
            str += "WiimoteNunchuckStateRawValuesZ : " + WiimoteNunchuckStateRawValuesZ + Environment.NewLine;
            str += "WiimoteNunchuckStateC : " + WiimoteNunchuckStateC + Environment.NewLine;
            str += "WiimoteNunchuckStateZ : " + WiimoteNunchuckStateZ + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form3.SetLabel1Text(str);
            }
            catch { }
            str = "JoyconRightStickX : " + JoyconRightStickX + Environment.NewLine;
            str += "JoyconRightStickY : " + JoyconRightStickY + Environment.NewLine;
            str += "JoyconRightButtonSHOULDER_1 : " + JoyconRightButtonSHOULDER_1 + Environment.NewLine;
            str += "JoyconRightButtonSHOULDER_2 : " + JoyconRightButtonSHOULDER_2 + Environment.NewLine;
            str += "JoyconRightButtonSR : " + JoyconRightButtonSR + Environment.NewLine;
            str += "JoyconRightButtonSL : " + JoyconRightButtonSL + Environment.NewLine;
            str += "JoyconRightButtonDPAD_DOWN : " + JoyconRightButtonDPAD_DOWN + Environment.NewLine;
            str += "JoyconRightButtonDPAD_RIGHT : " + JoyconRightButtonDPAD_RIGHT + Environment.NewLine;
            str += "JoyconRightButtonDPAD_UP : " + JoyconRightButtonDPAD_UP + Environment.NewLine;
            str += "JoyconRightButtonDPAD_LEFT : " + JoyconRightButtonDPAD_LEFT + Environment.NewLine;
            str += "JoyconRightButtonPLUS : " + JoyconRightButtonPLUS + Environment.NewLine;
            str += "JoyconRightButtonHOME : " + JoyconRightButtonHOME + Environment.NewLine;
            str += "JoyconRightButtonSTICK : " + JoyconRightButtonSTICK + Environment.NewLine;
            str += "JoyconRightButtonACC : " + JoyconRightButtonACC + Environment.NewLine;
            str += "JoyconRightButtonSPA : " + JoyconRightButtonSPA + Environment.NewLine;
            str += "JoyconRightRollLeft : " + JoyconRightRollLeft + Environment.NewLine;
            str += "JoyconRightRollRight : " + JoyconRightRollRight + Environment.NewLine;
            str += "JoyconRightAccelX : " + JoyconRightAccelX + Environment.NewLine;
            str += "JoyconRightAccelY : " + JoyconRightAccelY + Environment.NewLine;
            str += "JoyconRightGyroX : " + JoyconRightGyroX + Environment.NewLine;
            str += "JoyconRightGyroY : " + JoyconRightGyroY + Environment.NewLine;
            str += "JoyconLeftStickX : " + JoyconLeftStickX + Environment.NewLine;
            str += "JoyconLeftStickY : " + JoyconLeftStickY + Environment.NewLine;
            str += "JoyconLeftButtonSHOULDER_1 : " + JoyconLeftButtonSHOULDER_1 + Environment.NewLine;
            str += "JoyconLeftButtonSHOULDER_2 : " + JoyconLeftButtonSHOULDER_2 + Environment.NewLine;
            str += "JoyconLeftButtonSR : " + JoyconLeftButtonSR + Environment.NewLine;
            str += "JoyconLeftButtonSL : " + JoyconLeftButtonSL + Environment.NewLine;
            str += "JoyconLeftButtonDPAD_DOWN : " + JoyconLeftButtonDPAD_DOWN + Environment.NewLine;
            str += "JoyconLeftButtonDPAD_RIGHT : " + JoyconLeftButtonDPAD_RIGHT + Environment.NewLine;
            str += "JoyconLeftButtonDPAD_UP : " + JoyconLeftButtonDPAD_UP + Environment.NewLine;
            str += "JoyconLeftButtonDPAD_LEFT : " + JoyconLeftButtonDPAD_LEFT + Environment.NewLine;
            str += "JoyconLeftButtonMINUS : " + JoyconLeftButtonMINUS + Environment.NewLine;
            str += "JoyconLeftButtonCAPTURE : " + JoyconLeftButtonCAPTURE + Environment.NewLine;
            str += "JoyconLeftButtonSTICK : " + JoyconLeftButtonSTICK + Environment.NewLine;
            str += "JoyconLeftButtonACC : " + JoyconLeftButtonACC + Environment.NewLine;
            str += "JoyconLeftButtonSMA : " + JoyconLeftButtonSMA + Environment.NewLine;
            str += "JoyconLeftRollLeft : " + JoyconLeftRollLeft + Environment.NewLine;
            str += "JoyconLeftRollRight : " + JoyconLeftRollRight + Environment.NewLine;
            str += "JoyconLeftAccelX : " + JoyconLeftAccelX + Environment.NewLine;
            str += "JoyconLeftAccelY : " + JoyconLeftAccelY + Environment.NewLine;
            str += "JoyconLeftGyroX : " + JoyconLeftGyroX + Environment.NewLine;
            str += "JoyconLeftGyroY : " + JoyconLeftGyroY + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form3.SetLabel2Text(str);
            }
            catch { }
            str = "ProControllerLeftStickX : " + ProControllerLeftStickX + Environment.NewLine;
            str += "ProControllerLeftStickY : " + ProControllerLeftStickY + Environment.NewLine;
            str += "ProControllerRightStickX : " + ProControllerRightStickX + Environment.NewLine;
            str += "ProControllerRightStickY : " + ProControllerRightStickY + Environment.NewLine;
            str += "ProControllerButtonSHOULDER_Left_1 : " + ProControllerButtonSHOULDER_Left_1 + Environment.NewLine;
            str += "ProControllerButtonSHOULDER_Left_2 : " + ProControllerButtonSHOULDER_Left_2 + Environment.NewLine;
            str += "ProControllerButtonDPAD_DOWN : " + ProControllerButtonDPAD_DOWN + Environment.NewLine;
            str += "ProControllerButtonDPAD_RIGHT : " + ProControllerButtonDPAD_RIGHT + Environment.NewLine;
            str += "ProControllerButtonDPAD_UP : " + ProControllerButtonDPAD_UP + Environment.NewLine;
            str += "ProControllerButtonDPAD_LEFT : " + ProControllerButtonDPAD_LEFT + Environment.NewLine;
            str += "ProControllerButtonMINUS : " + ProControllerButtonMINUS + Environment.NewLine;
            str += "ProControllerButtonCAPTURE : " + ProControllerButtonCAPTURE + Environment.NewLine;
            str += "ProControllerButtonSTICK_Left : " + ProControllerButtonSTICK_Left + Environment.NewLine;
            str += "ProControllerButtonSHOULDER_Right_1 : " + ProControllerButtonSHOULDER_Right_1 + Environment.NewLine;
            str += "ProControllerButtonSHOULDER_Right_2 : " + ProControllerButtonSHOULDER_Right_2 + Environment.NewLine;
            str += "ProControllerButtonA : " + ProControllerButtonA + Environment.NewLine;
            str += "ProControllerButtonB : " + ProControllerButtonB + Environment.NewLine;
            str += "ProControllerButtonX : " + ProControllerButtonX + Environment.NewLine;
            str += "ProControllerButtonY : " + ProControllerButtonY + Environment.NewLine;
            str += "ProControllerButtonPLUS : " + ProControllerButtonPLUS + Environment.NewLine;
            str += "ProControllerButtonHOME : " + ProControllerButtonHOME + Environment.NewLine;
            str += "ProControllerButtonSTICK_Right : " + ProControllerButtonSTICK_Right + Environment.NewLine;
            str += "ProControllerAccelX : " + ProControllerAccelX + Environment.NewLine;
            str += "ProControllerAccelY : " + ProControllerAccelY + Environment.NewLine;
            str += "ProControllerGyroX : " + ProControllerGyroX + Environment.NewLine;
            str += "ProControllerGyroY : " + ProControllerGyroY + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form3.SetLabel3Text(str);
            }
            catch { }
            str = "PS5ControllerLeftStickX : " + PS5ControllerLeftStickX + Environment.NewLine;
            str += "PS5ControllerLeftStickY : " + PS5ControllerLeftStickY + Environment.NewLine;
            str += "PS5ControllerRightStickX : " + PS5ControllerRightStickX + Environment.NewLine;
            str += "PS5ControllerRightStickY : " + PS5ControllerRightStickY + Environment.NewLine;
            str += "PS5ControllerLeftTriggerPosition : " + PS5ControllerLeftTriggerPosition + Environment.NewLine;
            str += "PS5ControllerRightTriggerPosition : " + PS5ControllerRightTriggerPosition + Environment.NewLine;
            str += "PS5ControllerTouchX : " + PS5ControllerTouchX + Environment.NewLine;
            str += "PS5ControllerTouchY : " + PS5ControllerTouchY + Environment.NewLine;
            str += "PS5ControllerTouchOn : " + PS5ControllerTouchOn + Environment.NewLine;
            str += "PS5ControllerGyroX : " + PS5ControllerGyroX + Environment.NewLine;
            str += "PS5ControllerGyroY : " + PS5ControllerGyroY + Environment.NewLine;
            str += "PS5ControllerAccelX : " + PS5ControllerAccelX + Environment.NewLine;
            str += "PS5ControllerAccelY : " + PS5ControllerAccelY + Environment.NewLine;
            str += "PS5ControllerButtonCrossPressed : " + PS5ControllerButtonCrossPressed + Environment.NewLine;
            str += "PS5ControllerButtonCirclePressed : " + PS5ControllerButtonCirclePressed + Environment.NewLine;
            str += "PS5ControllerButtonSquarePressed : " + PS5ControllerButtonSquarePressed + Environment.NewLine;
            str += "PS5ControllerButtonTrianglePressed : " + PS5ControllerButtonTrianglePressed + Environment.NewLine;
            str += "PS5ControllerButtonDPadUpPressed : " + PS5ControllerButtonDPadUpPressed + Environment.NewLine;
            str += "PS5ControllerButtonDPadRightPressed : " + PS5ControllerButtonDPadRightPressed + Environment.NewLine;
            str += "PS5ControllerButtonDPadDownPressed : " + PS5ControllerButtonDPadDownPressed + Environment.NewLine;
            str += "PS5ControllerButtonDPadLeftPressed : " + PS5ControllerButtonDPadLeftPressed + Environment.NewLine;
            str += "PS5ControllerButtonL1Pressed : " + PS5ControllerButtonL1Pressed + Environment.NewLine;
            str += "PS5ControllerButtonR1Pressed : " + PS5ControllerButtonR1Pressed + Environment.NewLine;
            str += "PS5ControllerButtonL2Pressed : " + PS5ControllerButtonL2Pressed + Environment.NewLine;
            str += "PS5ControllerButtonR2Pressed : " + PS5ControllerButtonR2Pressed + Environment.NewLine;
            str += "PS5ControllerButtonL3Pressed : " + PS5ControllerButtonL3Pressed + Environment.NewLine;
            str += "PS5ControllerButtonR3Pressed : " + PS5ControllerButtonR3Pressed + Environment.NewLine;
            str += "PS5ControllerButtonCreatePressed : " + PS5ControllerButtonCreatePressed + Environment.NewLine;
            str += "PS5ControllerButtonMenuPressed : " + PS5ControllerButtonMenuPressed + Environment.NewLine;
            str += "PS5ControllerButtonLogoPressed : " + PS5ControllerButtonLogoPressed + Environment.NewLine;
            str += "PS5ControllerButtonTouchpadPressed : " + PS5ControllerButtonTouchpadPressed + Environment.NewLine;
            str += "PS5ControllerButtonMicPressed : " + PS5ControllerButtonMicPressed + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form3.SetLabel4Text(str);
            }
            catch { }
        }
        private void SetClassicOutputs()
        {
            string str = "KeyboardMouseDriverType : " + KeyboardMouseDriverType + Environment.NewLine;
            str += "MouseMoveX : " + MouseMoveX + Environment.NewLine;
            str += "MouseMoveY : " + MouseMoveY + Environment.NewLine;
            str += "MouseAbsX : " + MouseAbsX + Environment.NewLine;
            str += "MouseAbsY : " + MouseAbsY + Environment.NewLine;
            str += "MouseDesktopX : " + MouseDesktopX + Environment.NewLine;
            str += "MouseDesktopY : " + MouseDesktopY + Environment.NewLine;
            str += "SendLeftClick : " + SendLeftClick + Environment.NewLine;
            str += "SendRightClick : " + SendRightClick + Environment.NewLine;
            str += "SendMiddleClick : " + SendMiddleClick + Environment.NewLine;
            str += "SendWheelUp : " + SendWheelUp + Environment.NewLine;
            str += "SendWheelDown : " + SendWheelDown + Environment.NewLine;
            str += "SendLeft : " + SendLeft + Environment.NewLine;
            str += "SendRight : " + SendRight + Environment.NewLine;
            str += "SendUp : " + SendUp + Environment.NewLine;
            str += "SendDown : " + SendDown + Environment.NewLine;
            str += "SendLButton : " + SendLButton + Environment.NewLine;
            str += "SendRButton : " + SendRButton + Environment.NewLine;
            str += "SendCancel : " + SendCancel + Environment.NewLine;
            str += "SendMBUTTON : " + SendMBUTTON + Environment.NewLine;
            str += "SendXBUTTON1 : " + SendXBUTTON1 + Environment.NewLine;
            str += "SendXBUTTON2 : " + SendXBUTTON2 + Environment.NewLine;
            str += "SendBack : " + SendBack + Environment.NewLine;
            str += "SendTab : " + SendTab + Environment.NewLine;
            str += "SendClear : " + SendClear + Environment.NewLine;
            str += "SendReturn : " + SendReturn + Environment.NewLine;
            str += "SendSHIFT : " + SendSHIFT + Environment.NewLine;
            str += "SendCONTROL : " + SendCONTROL + Environment.NewLine;
            str += "SendMENU : " + SendMENU + Environment.NewLine;
            str += "SendPAUSE : " + SendPAUSE + Environment.NewLine;
            str += "SendCAPITAL : " + SendCAPITAL + Environment.NewLine;
            str += "SendKANA : " + SendKANA + Environment.NewLine;
            str += "SendHANGEUL : " + SendHANGEUL + Environment.NewLine;
            str += "SendHANGUL : " + SendHANGUL + Environment.NewLine;
            str += "SendJUNJA : " + SendJUNJA + Environment.NewLine;
            str += "SendFINAL : " + SendFINAL + Environment.NewLine;
            str += "SendHANJA : " + SendHANJA + Environment.NewLine;
            str += "SendKANJI : " + SendKANJI + Environment.NewLine;
            str += "SendEscape : " + SendEscape + Environment.NewLine;
            str += "SendCONVERT : " + SendCONVERT + Environment.NewLine;
            str += "SendNONCONVERT : " + SendNONCONVERT + Environment.NewLine;
            str += "SendACCEPT : " + SendACCEPT + Environment.NewLine;
            str += "SendMODECHANGE : " + SendMODECHANGE + Environment.NewLine;
            str += "SendSpace : " + SendSpace + Environment.NewLine;
            str += "SendPRIOR : " + SendPRIOR + Environment.NewLine;
            str += "SendNEXT : " + SendNEXT + Environment.NewLine;
            str += "SendEND : " + SendEND + Environment.NewLine;
            str += "SendHOME : " + SendHOME + Environment.NewLine;
            str += "SendLEFT : " + SendLEFT + Environment.NewLine;
            str += "SendUP : " + SendUP + Environment.NewLine;
            str += "SendRIGHT : " + SendRIGHT + Environment.NewLine;
            str += "SendDOWN : " + SendDOWN + Environment.NewLine;
            str += "SendSELECT : " + SendSELECT + Environment.NewLine;
            str += "SendPRINT : " + SendPRINT + Environment.NewLine;
            str += "SendEXECUTE : " + SendEXECUTE + Environment.NewLine;
            str += "SendSNAPSHOT : " + SendSNAPSHOT + Environment.NewLine;
            str += "SendINSERT : " + SendINSERT + Environment.NewLine;
            str += "SendDELETE : " + SendDELETE + Environment.NewLine;
            str += "SendHELP : " + SendHELP + Environment.NewLine;
            str += "SendAPOSTROPHE : " + SendAPOSTROPHE + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form4.SetLabel1Text(str);
            }
            catch { }
            str = "Send0 : " + Send0 + Environment.NewLine;
            str += "Send1 : " + Send1 + Environment.NewLine;
            str += "Send2 : " + Send2 + Environment.NewLine;
            str += "Send3 : " + Send3 + Environment.NewLine;
            str += "Send4 : " + Send4 + Environment.NewLine;
            str += "Send5 : " + Send5 + Environment.NewLine;
            str += "Send6 : " + Send6 + Environment.NewLine;
            str += "Send7 : " + Send7 + Environment.NewLine;
            str += "Send8 : " + Send8 + Environment.NewLine;
            str += "Send9 : " + Send9 + Environment.NewLine;
            str += "SendA : " + SendA + Environment.NewLine;
            str += "SendB : " + SendB + Environment.NewLine;
            str += "SendC : " + SendC + Environment.NewLine;
            str += "SendD : " + SendD + Environment.NewLine;
            str += "SendE : " + SendE + Environment.NewLine;
            str += "SendF : " + SendF + Environment.NewLine;
            str += "SendG : " + SendG + Environment.NewLine;
            str += "SendH : " + SendH + Environment.NewLine;
            str += "SendI : " + SendI + Environment.NewLine;
            str += "SendJ : " + SendJ + Environment.NewLine;
            str += "SendK : " + SendK + Environment.NewLine;
            str += "SendL : " + SendL + Environment.NewLine;
            str += "SendM : " + SendM + Environment.NewLine;
            str += "SendN : " + SendN + Environment.NewLine;
            str += "SendO : " + SendO + Environment.NewLine;
            str += "SendP : " + SendP + Environment.NewLine;
            str += "SendQ : " + SendQ + Environment.NewLine;
            str += "SendR : " + SendR + Environment.NewLine;
            str += "SendS : " + SendS + Environment.NewLine;
            str += "SendT : " + SendT + Environment.NewLine;
            str += "SendU : " + SendU + Environment.NewLine;
            str += "SendV : " + SendV + Environment.NewLine;
            str += "SendW : " + SendW + Environment.NewLine;
            str += "SendX : " + SendX + Environment.NewLine;
            str += "SendY : " + SendY + Environment.NewLine;
            str += "SendZ : " + SendZ + Environment.NewLine;
            str += "SendLWIN : " + SendLWIN + Environment.NewLine;
            str += "SendRWIN : " + SendRWIN + Environment.NewLine;
            str += "SendAPPS : " + SendAPPS + Environment.NewLine;
            str += "SendSLEEP : " + SendSLEEP + Environment.NewLine;
            str += "SendNUMPAD0 : " + SendNUMPAD0 + Environment.NewLine;
            str += "SendNUMPAD1 : " + SendNUMPAD1 + Environment.NewLine;
            str += "SendNUMPAD2 : " + SendNUMPAD2 + Environment.NewLine;
            str += "SendNUMPAD3 : " + SendNUMPAD3 + Environment.NewLine;
            str += "SendNUMPAD4 : " + SendNUMPAD4 + Environment.NewLine;
            str += "SendNUMPAD5 : " + SendNUMPAD5 + Environment.NewLine;
            str += "SendNUMPAD6 : " + SendNUMPAD6 + Environment.NewLine;
            str += "SendNUMPAD7 : " + SendNUMPAD7 + Environment.NewLine;
            str += "SendNUMPAD8 : " + SendNUMPAD8 + Environment.NewLine;
            str += "SendNUMPAD9 : " + SendNUMPAD9 + Environment.NewLine;
            str += "SendMULTIPLY : " + SendMULTIPLY + Environment.NewLine;
            str += "SendADD : " + SendADD + Environment.NewLine;
            str += "SendSEPARATOR : " + SendSEPARATOR + Environment.NewLine;
            str += "SendSUBTRACT : " + SendSUBTRACT + Environment.NewLine;
            str += "SendDECIMAL : " + SendDECIMAL + Environment.NewLine;
            str += "SendDIVIDE : " + SendDIVIDE + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form4.SetLabel2Text(str);
            }
            catch { }
            str = "SendF1 : " + SendF1 + Environment.NewLine;
            str += "SendF2 : " + SendF2 + Environment.NewLine;
            str += "SendF3 : " + SendF3 + Environment.NewLine;
            str += "SendF4 : " + SendF4 + Environment.NewLine;
            str += "SendF5 : " + SendF5 + Environment.NewLine;
            str += "SendF6 : " + SendF6 + Environment.NewLine;
            str += "SendF7 : " + SendF7 + Environment.NewLine;
            str += "SendF8 : " + SendF8 + Environment.NewLine;
            str += "SendF9 : " + SendF9 + Environment.NewLine;
            str += "SendF10 : " + SendF10 + Environment.NewLine;
            str += "SendF11 : " + SendF11 + Environment.NewLine;
            str += "SendF12 : " + SendF12 + Environment.NewLine;
            str += "SendF13 : " + SendF13 + Environment.NewLine;
            str += "SendF14 : " + SendF14 + Environment.NewLine;
            str += "SendF15 : " + SendF15 + Environment.NewLine;
            str += "SendF16 : " + SendF16 + Environment.NewLine;
            str += "SendF17 : " + SendF17 + Environment.NewLine;
            str += "SendF18 : " + SendF18 + Environment.NewLine;
            str += "SendF19 : " + SendF19 + Environment.NewLine;
            str += "SendF20 : " + SendF20 + Environment.NewLine;
            str += "SendF21 : " + SendF21 + Environment.NewLine;
            str += "SendF22 : " + SendF22 + Environment.NewLine;
            str += "SendF23 : " + SendF23 + Environment.NewLine;
            str += "SendF24 : " + SendF24 + Environment.NewLine;
            str += "SendNUMLOCK : " + SendNUMLOCK + Environment.NewLine;
            str += "SendSCROLL : " + SendSCROLL + Environment.NewLine;
            str += "SendLeftShift : " + SendLeftShift + Environment.NewLine;
            str += "SendRightShift : " + SendRightShift + Environment.NewLine;
            str += "SendLeftControl : " + SendLeftControl + Environment.NewLine;
            str += "SendRightControl : " + SendRightControl + Environment.NewLine;
            str += "SendLMENU : " + SendLMENU + Environment.NewLine;
            str += "SendRMENU : " + SendRMENU + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form4.SetLabel3Text(str);
            }
            catch { }
        }
        private void SetXOutputs()
        {
            string str = "controller1_send_back : " + controller1_send_back + Environment.NewLine;
            str += "controller1_send_start : " + controller1_send_start + Environment.NewLine;
            str += "controller1_send_A : " + controller1_send_A + Environment.NewLine;
            str += "controller1_send_B : " + controller1_send_B + Environment.NewLine;
            str += "controller1_send_X : " + controller1_send_X + Environment.NewLine;
            str += "controller1_send_Y : " + controller1_send_Y + Environment.NewLine;
            str += "controller1_send_up : " + controller1_send_up + Environment.NewLine;
            str += "controller1_send_left : " + controller1_send_left + Environment.NewLine;
            str += "controller1_send_down : " + controller1_send_down + Environment.NewLine;
            str += "controller1_send_right : " + controller1_send_right + Environment.NewLine;
            str += "controller1_send_leftstick : " + controller1_send_leftstick + Environment.NewLine;
            str += "controller1_send_rightstick : " + controller1_send_rightstick + Environment.NewLine;
            str += "controller1_send_leftbumper : " + controller1_send_leftbumper + Environment.NewLine;
            str += "controller1_send_rightbumper : " + controller1_send_rightbumper + Environment.NewLine;
            str += "controller1_send_lefttrigger : " + controller1_send_lefttrigger + Environment.NewLine;
            str += "controller1_send_righttrigger : " + controller1_send_righttrigger + Environment.NewLine;
            str += "controller1_send_leftstickx : " + controller1_send_leftstickx + Environment.NewLine;
            str += "controller1_send_leftsticky : " + controller1_send_leftsticky + Environment.NewLine;
            str += "controller1_send_rightstickx : " + controller1_send_rightstickx + Environment.NewLine;
            str += "controller1_send_rightsticky : " + controller1_send_rightsticky + Environment.NewLine;
            str += "controller1_send_lefttriggerposition : " + controller1_send_lefttriggerposition + Environment.NewLine;
            str += "controller1_send_righttriggerposition : " + controller1_send_righttriggerposition + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form9.SetLabel1Text(str);
            }
            catch { }
            str = "controller2_send_back : " + controller2_send_back + Environment.NewLine;
            str += "controller2_send_start : " + controller2_send_start + Environment.NewLine;
            str += "controller2_send_A : " + controller2_send_A + Environment.NewLine;
            str += "controller2_send_B : " + controller2_send_B + Environment.NewLine;
            str += "controller2_send_X : " + controller2_send_X + Environment.NewLine;
            str += "controller2_send_Y : " + controller2_send_Y + Environment.NewLine;
            str += "controller2_send_up : " + controller2_send_up + Environment.NewLine;
            str += "controller2_send_left : " + controller2_send_left + Environment.NewLine;
            str += "controller2_send_down : " + controller2_send_down + Environment.NewLine;
            str += "controller2_send_right : " + controller2_send_right + Environment.NewLine;
            str += "controller2_send_leftstick : " + controller2_send_leftstick + Environment.NewLine;
            str += "controller2_send_rightstick : " + controller2_send_rightstick + Environment.NewLine;
            str += "controller2_send_leftbumper : " + controller2_send_leftbumper + Environment.NewLine;
            str += "controller2_send_rightbumper : " + controller2_send_rightbumper + Environment.NewLine;
            str += "controller2_send_lefttrigger : " + controller2_send_lefttrigger + Environment.NewLine;
            str += "controller2_send_righttrigger : " + controller2_send_righttrigger + Environment.NewLine;
            str += "controller2_send_leftstickx : " + controller2_send_leftstickx + Environment.NewLine;
            str += "controller2_send_leftsticky : " + controller2_send_leftsticky + Environment.NewLine;
            str += "controller2_send_rightstickx : " + controller2_send_rightstickx + Environment.NewLine;
            str += "controller2_send_rightsticky : " + controller2_send_rightsticky + Environment.NewLine;
            str += "controller2_send_lefttriggerposition : " + controller2_send_lefttriggerposition + Environment.NewLine;
            str += "controller2_send_righttriggerposition : " + controller2_send_righttriggerposition + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form9.SetLabel2Text(str);
            }
            catch { }
        }
        private void SetExtraOutputs()
        {
            string str = "sleeptime : " + sleeptime + Environment.NewLine;
            str += "centery : " + centery + Environment.NewLine;
            str += "irmode : " + irmode + Environment.NewLine;
            str += "gyromode : " + gyromode + Environment.NewLine;
            str += "pollcount : " + pollcount + Environment.NewLine;
            str += "keys12345 : " + keys12345 + Environment.NewLine;
            str += "keys54321 : " + keys54321 + Environment.NewLine;
            str += "getstate : " + getstate + Environment.NewLine;
            str += "mousexp : " + mousexp + Environment.NewLine;
            str += "mouseyp : " + mouseyp + Environment.NewLine;
            str += "testdouble : " + testdouble + Environment.NewLine;
            str += "testbool : " + testbool + Environment.NewLine;
            str += "JoyconLeftGyroCenter : " + JoyconLeftGyroCenter + Environment.NewLine;
            str += "JoyconRightGyroCenter : " + JoyconRightGyroCenter + Environment.NewLine;
            str += "ProControllerGyroCenter : " + ProControllerGyroCenter + Environment.NewLine;
            str += "PS5ControllerGyroCenter : " + PS5ControllerGyroCenter + Environment.NewLine;
            str += "Controller1GyroCenter : " + Controller1GyroCenter + Environment.NewLine;
            str += "Controller2GyroCenter : " + Controller2GyroCenter + Environment.NewLine;
            str += "JoyconLeftAccelCenter : " + JoyconLeftAccelCenter + Environment.NewLine;
            str += "JoyconRightAccelCenter : " + JoyconRightAccelCenter + Environment.NewLine;
            str += "ProControllerAccelCenter : " + ProControllerAccelCenter + Environment.NewLine;
            str += "PS5ControllerAccelCenter : " + PS5ControllerAccelCenter + Environment.NewLine;
            str += "JoyconLeftStickCenter : " + JoyconLeftStickCenter + Environment.NewLine;
            str += "JoyconRightStickCenter : " + JoyconRightStickCenter + Environment.NewLine;
            str += "ProControllerStickCenter : " + ProControllerStickCenter + Environment.NewLine;
            str += Environment.NewLine;
            try
            {
                form10.SetLabel1Text(str);
            }
            catch { }
        }
        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fastColoredTextBoxSaved != fastColoredTextBox1.Text)
                justSaved = false;
            ChangeScriptColors(sender);
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            keyboardHook.Hook += new KeyboardHook.KeyboardHookCallback(KeyboardHook_Hook);
            keyboardHook.Install();
            mouseHook.Hook += new MouseHook.MouseHookCallback(MouseHook_Hook);
            mouseHook.Install();
            if (System.IO.File.Exists("tempsave"))
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader("tempsave"))
                {
                    filename = file.ReadLine();
                    red = Convert.ToInt32(file.ReadLine());
                    green = Convert.ToInt32(file.ReadLine());
                    blue = Convert.ToInt32(file.ReadLine());
                    brightness = Convert.ToInt32(file.ReadLine());
                    radius = Convert.ToInt32(file.ReadLine());
                }
                if (filename != "" & System.IO.File.Exists(filename))
                {
                    if (!filename.EndsWith(".encrypted"))
                    {
                        string readText = File.ReadAllText(filename);
                        fastColoredTextBox1.Text = readText;
                    }
                    else
                    {
                        DecryptFile(filename, "tybtrybrtyertu50727885");
                        fastColoredTextBox1.Text = "Script Encrypted...";
                    }
                    openFilePath = filename;
                    this.Text = "PGM: " + System.IO.Path.GetFileName(filename);
                    fastColoredTextBoxSaved = fastColoredTextBox1.Text;
                    justSaved = true; 
                }
            }
            try
            {
                StartWebcamInputs();
            }
            catch { }
            username = Program.username;
            SetExtraInputs();
            SetClassicOutputs();
            SetClassicInputs();
            SetXInputs();
            SetDirectInputs();
            SetXOutputs();
            SetExtraOutputs();
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!runstopbool)
            {
                runToolStripMenuItem.Text = "Stop";
                Task.Run(() => ProcessScript());
                runstopbool = true;
            }
            else
            {
                StopScript();
            }
        }
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            Task.Run(() => TestScript());
        }
        public static void EncryptFile(string inputFile, string outputFile, string password)
        {
            byte[] salt = new byte[8];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);
            using (var pbkdf = new Rfc2898DeriveBytes(password, salt))
            using (var aes = new RijndaelManaged())
            using (var encryptor = aes.CreateEncryptor(pbkdf.GetBytes(aes.KeySize / 8), pbkdf.GetBytes(aes.BlockSize / 8)))
            using (var input = File.OpenRead(inputFile))
            using (var output = File.Create(outputFile))
            {
                output.Write(salt, 0, salt.Length);
                using (var cs = new CryptoStream(output, encryptor, CryptoStreamMode.Write))
                    input.CopyTo(cs);
            }
        }
        public static void DecryptFile(string inputFile, string password)
        {
            using (var input = File.OpenRead(inputFile))
            {
                byte[] salt = new byte[8];
                input.Read(salt, 0, salt.Length);
                using (var decryptedStream = new MemoryStream())
                using (var pbkdf = new Rfc2898DeriveBytes(password, salt))
                using (var aes = new RijndaelManaged())
                using (var decryptor = aes.CreateDecryptor(pbkdf.GetBytes(aes.KeySize / 8), pbkdf.GetBytes(aes.BlockSize / 8)))
                using (var cs = new CryptoStream(input, decryptor, CryptoStreamMode.Read))
                {
                    string contents;
                    int data;
                    while ((data = cs.ReadByte()) != -1)
                        decryptedStream.WriteByte((byte)data);
                    decryptedStream.Position = 0;
                    using (StreamReader sr = new StreamReader(decryptedStream))
                        contents = sr.ReadToEnd();
                    decryptedStream.Flush();
                    stringscript = contents;
                }
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopScript();
            Thread.Sleep(100);
            if (wiimoteconnected)
            {
                FormCloseWiimoteNunchuck();
            }
            if (joyconleftconnected)
            {
                FormCloseLeft();
            }
            if (joyconrightconnected)
            {
                FormCloseRight();
            }
            if (procontrollerconnected)
            {
                FormClosePro();
            }
            if (filename != "" | red != 0 | green != 205 | blue != 205 | brightness != -50 | radius != 175)
            {
                using (System.IO.StreamWriter createdfile = new System.IO.StreamWriter("tempsave"))
                {
                    createdfile.WriteLine(filename);
                    createdfile.WriteLine(red);
                    createdfile.WriteLine(green);
                    createdfile.WriteLine(blue);
                    createdfile.WriteLine(brightness);
                    createdfile.WriteLine(radius);
                }
            }
            keyboardHook.Hook -= new KeyboardHook.KeyboardHookCallback(KeyboardHook_Hook);
            keyboardHook.Uninstall();
            mouseHook.Hook -= new MouseHook.MouseHookCallback(MouseHook_Hook);
            mouseHook.Uninstall();
            try
            {
                FinalFrame.NewFrame -= FinalFrame_NewFrame;
                Thread.Sleep(100);
                if (FinalFrame.IsRunning)
                {
                    FinalFrame.Stop();
                }
            }
            catch { }
        }
        private void KeyboardHook_Hook(KeyboardHook.KBDLLHOOKSTRUCT keyboardStruct) { }
        private void MouseHook_Hook(MouseHook.MSLLHOOKSTRUCT mouseStruct) { }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (!justSaved)
            {
                result = MessageBox.Show("Content will be lost! Are you sure?", "New", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    fastColoredTextBox1.Clear();
                    this.Text = "ProGamingMapper";
                    openFilePath = "";
                    fastColoredTextBoxSaved = fastColoredTextBox1.Text;
                    justSaved = true;
                }
            }
            else
            {
                fastColoredTextBox1.Clear();
                this.Text = "ProGamingMapper";
                openFilePath = "";
                fastColoredTextBoxSaved = fastColoredTextBox1.Text;
                justSaved = true;
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (!justSaved)
            {
                result = MessageBox.Show("Content will be lost! Are you sure?", "Open", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    OpenFileDialog op = new OpenFileDialog();
                    op.Filter = "All Files(*.*)|*.*";
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        if (!op.FileName.EndsWith(".encrypted"))
                        {
                            string readText = File.ReadAllText(op.FileName);
                            fastColoredTextBox1.Text = readText;
                        }
                        else
                        {
                            DecryptFile(op.FileName, "tybtrybrtyertu50727885");
                            fastColoredTextBox1.Text = "Script Encrypted...";
                        }
                        filename = op.FileName;
                        openFilePath = op.FileName;
                        this.Text = "PGM: " + System.IO.Path.GetFileName(filename);
                        fastColoredTextBoxSaved = fastColoredTextBox1.Text;
                        justSaved = true;
                    }
                }
            }
            else
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "All Files(*.*)|*.*";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (!op.FileName.EndsWith(".encrypted"))
                    {
                        string readText = File.ReadAllText(op.FileName);
                        fastColoredTextBox1.Text = readText;
                    }
                    else
                    {
                        DecryptFile(op.FileName, "tybtrybrtyertu50727885");
                        fastColoredTextBox1.Text = "Script Encrypted...";
                    }
                    filename = op.FileName;
                    openFilePath = op.FileName;
                    this.Text = "PGM: " + System.IO.Path.GetFileName(filename);
                    fastColoredTextBoxSaved = fastColoredTextBox1.Text;
                    justSaved = true;
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilePath == "")
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                StopScript();
                if (fastColoredTextBox1.Text != "Script Encrypted...")
                {
                    stringscript = fastColoredTextBox1.Text;
                    File.WriteAllText(openFilePath, stringscript);
                    filename = System.IO.Path.GetFileName(openFilePath);
                    savepath = Path.GetDirectoryName(openFilePath);
                    if (!System.IO.Directory.Exists(savepath + "/encrypted"))
                    {
                        System.IO.Directory.CreateDirectory(savepath + "/encrypted");
                    }
                    EncryptFile(openFilePath, savepath + "/encrypted/" + filename + ".encrypted", "tybtrybrtyertu50727885");
                    filename = openFilePath;
                    fastColoredTextBoxSaved = fastColoredTextBox1.Text;
                    justSaved = true;
                }
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "All Files(*.*)|*.*";
            if (filename != "")
                sf.FileName = System.IO.Path.GetFileName(filename);
            if (sf.ShowDialog() == DialogResult.OK)
            {
                if (fastColoredTextBox1.Text != "Script Encrypted...")
                {
                    stringscript = fastColoredTextBox1.Text;
                    File.WriteAllText(sf.FileName, stringscript);
                    filename = System.IO.Path.GetFileName(sf.FileName);
                    this.Text = "PGM: " + filename;
                    savepath = Path.GetDirectoryName(sf.FileName);
                    if (!System.IO.Directory.Exists(savepath + "/encrypted"))
                    {
                        System.IO.Directory.CreateDirectory(savepath + "/encrypted");
                    }
                    EncryptFile(sf.FileName, savepath + "/encrypted/" + filename + ".encrypted", "tybtrybrtyertu50727885");
                    filename = sf.FileName;
                    openFilePath = sf.FileName;
                    fastColoredTextBoxSaved = fastColoredTextBox1.Text;
                    justSaved = true;
                }
            }
        }
        public void StopScript()
        {
            scriptrunning = false;
            runToolStripMenuItem.Text = "Run";
            runstopbool = false;
            fastColoredTextBox1.ReadOnly = false;
            fastColoredTextBox1.Enabled = true;
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Cut();
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Copy();
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Paste();
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Undo();
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Redo();
        }
        private void fastColoredTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                fastColoredTextBox1.ContextMenu = contextMenu;
            }
        }
        private void changeCursor(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void cutAction(object sender, EventArgs e)
        {
            fastColoredTextBox1.Cut();
        }
        private void copyAction(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.SelectedText != "")
                Clipboard.SetText(fastColoredTextBox1.SelectedText);
        }
        private void pasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                fastColoredTextBox1.SelectedText = Clipboard.GetText(TextDataFormat.Text).ToString();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!justSaved)
            {
                result = MessageBox.Show("Content will be lost! Are you sure?", "Exit", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            menuItem = new MenuItem("Cut");
            contextMenu.MenuItems.Add(menuItem);
            menuItem.Select += new EventHandler(changeCursor);
            menuItem.Click += new EventHandler(cutAction);
            menuItem = new MenuItem("Copy");
            contextMenu.MenuItems.Add(menuItem);
            menuItem.Select += new EventHandler(changeCursor);
            menuItem.Click += new EventHandler(copyAction);
            menuItem = new MenuItem("Paste");
            contextMenu.MenuItems.Add(menuItem);
            menuItem.Select += new EventHandler(changeCursor);
            menuItem.Click += new EventHandler(pasteAction);
            fastColoredTextBox1.ContextMenu = contextMenu;
        }
        private void webcamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!form2visible)
            {
                form2visible = true;
                try
                {
                    form2.Visible = true;
                }
                catch { }
            }
            else
            {
                if (form2visible)
                {
                    form2visible = false;
                    try
                    {
                        form2.Hide();
                    }
                    catch { }
                }
            }
        }
        private void inputsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!form3visible)
            {
                form3visible = true;
                try
                {
                    form3.Visible = true;
                }
                catch { }
            }
            else
            {
                if (form3visible)
                {
                    form3visible = false;
                    try
                    {
                        form3.Hide();
                    }
                    catch { }
                }
            }
        }
        private void outputsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!form4visible)
            {
                form4visible = true;
                try
                {
                    form4.Visible = true;
                }
                catch { }
            }
            else
            {
                if (form4visible)
                {
                    form4visible = false;
                    try
                    {
                        form4.Hide();
                    }
                    catch { }
                }
            }
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (!form5visible)
            {
                form5visible = true;
                try
                {
                    form5.Visible = true;
                }
                catch { }
            }
            else
            {
                if (form5visible)
                {
                    form5visible = false;
                    try
                    {
                        form5.Hide();
                    }
                    catch { }
                }
            }
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (!form7visible)
            {
                form7visible = true;
                try
                {
                    form7.Visible = true;
                }
                catch { }
            }
            else
            {
                if (form7visible)
                {
                    form7visible = false;
                    try
                    {
                        form7.Hide();
                    }
                    catch { }
                }
            }
        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (!form8visible)
            {
                form8visible = true;
                try
                {
                    form8.Visible = true;
                }
                catch { }
            }
            else
            {
                if (form8visible)
                {
                    form8visible = false;
                    try
                    {
                        form8.Hide();
                    }
                    catch { }
                }
            }
        }
        private void xboxOutputsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!form9visible)
            {
                form9visible = true;
                try
                {
                    form9.Visible = true;
                }
                catch { }
            }
            else
            {
                if (form9visible)
                {
                    form9visible = false;
                    try
                    {
                        form9.Hide();
                    }
                    catch { }
                }
            }
        }
        private void extraOutputsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!form10visible)
            {
                form10visible = true;
                try
                {
                    form10.Visible = true;
                }
                catch { }
            }
            else
            {
                if (form10visible)
                {
                    form10visible = false;
                    try
                    {
                        form10.Hide();
                    }
                    catch { }
                }
            }
        }
        private void connectWiimoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            Task.Run(() =>
            {
                do
                {
                    wiimoteconnected = wiimoteconnect();
                    Thread.Sleep(1);
                }
                while (!wiimoteconnected & !scriptrunning);
                if (wiimoteconnected)
                {
                    MessageBox.Show("Wiimote");
                }
            });
        }
        private void connectJoyconLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            Task.Run(() =>
            {
                do
                {
                    joyconleftconnected = joyconleftconnect();
                    Thread.Sleep(1);
                }
                while (!joyconleftconnected & !scriptrunning);
                if (joyconleftconnected)
                {
                    MessageBox.Show("Joycon left");
                }
            });
        }
        private void connectJoyconRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            Task.Run(() =>
            {
                do
                {
                    joyconrightconnected = joyconrightconnect();
                    Thread.Sleep(1);
                }
                while (!joyconrightconnected & !scriptrunning);
                if (joyconrightconnected)
                {
                    MessageBox.Show("Joycon right");
                }
            });
        }
        private void connectBothJoyconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            Task.Run(() =>
            {
                do
                {
                    joyconsconnected = joyconsconnect();
                    Thread.Sleep(1);
                }
                while (!joyconsconnected & !scriptrunning);
                if (joyconsconnected)
                {
                    joyconleftconnected = true;
                    joyconrightconnected = true;
                    MessageBox.Show("Joycons");
                }
            });
        }
        private void connectWiimoteJoyconLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            Task.Run(() =>
            {
                do
                {
                    wiimotejoyconleftconnected = wiimotejoyconleftconnect();
                    Thread.Sleep(1);
                }
                while (!wiimotejoyconleftconnected & !scriptrunning);
                if (wiimotejoyconleftconnected)
                {
                    wiimoteconnected = true;
                    joyconleftconnected = true;
                    MessageBox.Show("Wiimote Joycon left");
                }
            });
        }
        private void connectWiimoteJoyconRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            Task.Run(() =>
            {
                do
                {
                    wiimotejoyconrightconnected = wiimotejoyconrightconnect();
                    Thread.Sleep(1);
                }
                while (!wiimotejoyconrightconnected & !scriptrunning);
                if (wiimotejoyconrightconnected)
                {
                    wiimoteconnected = true;
                    joyconrightconnected = true;
                    MessageBox.Show("Wiimote Joycon right");
                }
            });
        }
        private void connectProControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            Task.Run(() =>
            {
                do
                {
                    procontrollerconnected = procontrollerconnect();
                    Thread.Sleep(1);
                }
                while (!procontrollerconnected & !scriptrunning);
                if (procontrollerconnected)
                {
                    MessageBox.Show("Pro Controller");
                }
            });
        }
        private void disconnectWiimoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (wiimoteconnected)
            {
                Task.Run(() => { FormCloseWiimoteNunchuck(); });
            }
        }
        private void disconnectJoyconLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (joyconleftconnected)
            {
                Task.Run(() => { FormCloseLeft(); });
            }
        }
        private void disconnectJoyconRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (joyconrightconnected)
            {
                Task.Run(() => { FormCloseRight(); });
            }
        }
        private void disconnectBothJoyconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (joyconleftconnected)
            {
                Task.Run(() => { FormCloseLeft(); });
            }
            if (joyconrightconnected)
            {
                Task.Run(() => { FormCloseRight(); });
            }
        }
        private void disconnectWiimoteJoyconLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (joyconleftconnected)
            {
                Task.Run(() => { FormCloseLeft(); });
            }
            if (wiimoteconnected)
            {
                Task.Run(() => { FormCloseWiimoteNunchuck(); });
            }
        }
        private void disconnectWiimoteJoyconRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (joyconrightconnected)
            {
                Task.Run(() => { FormCloseRight(); });
            }
            if (wiimoteconnected)
            {
                Task.Run(() => { FormCloseWiimoteNunchuck(); });
            }
        }
        private void disconnectProControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopScript();
            if (procontrollerconnected)
            {
                Task.Run(() => { FormClosePro(); });
            }
        }
        public void TestScript()
        {
            scriptrunning = true;
            try
            {
                if (fastColoredTextBox1.Text != "Script Encrypted...")
                {
                    stringscript = fastColoredTextBox1.Text;
                }
                string finalcode = code.Replace("funct_driver", stringscript);
                parameters = new System.CodeDom.Compiler.CompilerParameters();
                parameters.GenerateExecutable = false;
                parameters.GenerateInMemory = true;
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                provider = new Microsoft.CSharp.CSharpCodeProvider();
                results = provider.CompileAssemblyFromSource(parameters, finalcode);
                if (results.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}) : {1}", error.ErrorNumber, error.ErrorText));
                    }
                    MessageBox.Show("Script Error :\n\r" + sb.ToString());
                    return;
                }
                else
                {
                    MessageBox.Show("Script Ok.");
                }
                assembly = results.CompiledAssembly;
                program = assembly.GetType("StringToCode.FooClass");
                obj = Activator.CreateInstance(program);
                object[] val = (object[])program.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, new object[] { width, height, Key_LBUTTON, Key_RBUTTON, Key_CANCEL, Key_MBUTTON, Key_XBUTTON1, Key_XBUTTON2, Key_BACK, Key_Tab, Key_CLEAR, Key_Return, Key_SHIFT, Key_CONTROL, Key_MENU, Key_PAUSE, Key_CAPITAL, Key_KANA, Key_HANGEUL, Key_HANGUL, Key_JUNJA, Key_FINAL, Key_HANJA, Key_KANJI, Key_Escape, Key_CONVERT, Key_NONCONVERT, Key_ACCEPT, Key_MODECHANGE, Key_Space, Key_PRIOR, Key_NEXT, Key_END, Key_HOME, Key_LEFT, Key_UP, Key_RIGHT, Key_DOWN, Key_SELECT, Key_PRINT, Key_EXECUTE, Key_SNAPSHOT, Key_INSERT, Key_DELETE, Key_HELP, Key_APOSTROPHE, Key_0, Key_1, Key_2, Key_3, Key_4, Key_5, Key_6, Key_7, Key_8, Key_9, Key_A, Key_B, Key_C, Key_D, Key_E, Key_F, Key_G, Key_H, Key_I, Key_J, Key_K, Key_L, Key_M, Key_N, Key_O, Key_P, Key_Q, Key_R, Key_S, Key_T, Key_U, Key_V, Key_W, Key_X, Key_Y, Key_Z, Key_LWIN, Key_RWIN, Key_APPS, Key_SLEEP, Key_NUMPAD0, Key_NUMPAD1, Key_NUMPAD2, Key_NUMPAD3, Key_NUMPAD4, Key_NUMPAD5, Key_NUMPAD6, Key_NUMPAD7, Key_NUMPAD8, Key_NUMPAD9, Key_MULTIPLY, Key_ADD, Key_SEPARATOR, Key_SUBTRACT, Key_DECIMAL, Key_DIVIDE, Key_F1, Key_F2, Key_F3, Key_F4, Key_F5, Key_F6, Key_F7, Key_F8, Key_F9, Key_F10, Key_F11, Key_F12, Key_F13, Key_F14, Key_F15, Key_F16, Key_F17, Key_F18, Key_F19, Key_F20, Key_F21, Key_F22, Key_F23, Key_F24, Key_NUMLOCK, Key_SCROLL, Key_LeftShift, Key_RightShift, Key_LeftControl, Key_RightControl, Key_LMENU, Key_RMENU, Key_BROWSER_BACK, Key_BROWSER_FORWARD, Key_BROWSER_REFRESH, Key_BROWSER_STOP, Key_BROWSER_SEARCH, Key_BROWSER_FAVORITES, Key_BROWSER_HOME, Key_VOLUME_MUTE, Key_VOLUME_DOWN, Key_VOLUME_UP, Key_MEDIA_NEXT_TRACK, Key_MEDIA_PREV_TRACK, Key_MEDIA_STOP, Key_MEDIA_PLAY_PAUSE, Key_LAUNCH_MAIL, Key_LAUNCH_MEDIA_SELECT, Key_LAUNCH_APP1, Key_LAUNCH_APP2, Key_OEM_1, Key_OEM_PLUS, Key_OEM_COMMA, Key_OEM_MINUS, Key_OEM_PERIOD, Key_OEM_2, Key_OEM_3, Key_OEM_4, Key_OEM_5, Key_OEM_6, Key_OEM_7, Key_OEM_8, Key_OEM_102, Key_PROCESSKEY, Key_PACKET, Key_ATTN, Key_CRSEL, Key_EXSEL, Key_EREOF, Key_PLAY, Key_ZOOM, Key_NONAME, Key_PA1, Key_OEM_CLEAR, irx, iry, WiimoteButtonStateA, WiimoteButtonStateB, WiimoteButtonStateMinus, WiimoteButtonStateHome, WiimoteButtonStatePlus, WiimoteButtonStateOne, WiimoteButtonStateTwo, WiimoteButtonStateUp, WiimoteButtonStateDown, WiimoteButtonStateLeft, WiimoteButtonStateRight, WiimoteRawValuesX, WiimoteRawValuesY, WiimoteRawValuesZ, WiimoteNunchuckStateRawJoystickX, WiimoteNunchuckStateRawJoystickY, WiimoteNunchuckStateRawValuesX, WiimoteNunchuckStateRawValuesY, WiimoteNunchuckStateRawValuesZ, WiimoteNunchuckStateC, WiimoteNunchuckStateZ, JoyconRightStickX, JoyconRightStickY, JoyconRightButtonSHOULDER_1, JoyconRightButtonSHOULDER_2, JoyconRightButtonSR, JoyconRightButtonSL, JoyconRightButtonDPAD_DOWN, JoyconRightButtonDPAD_RIGHT, JoyconRightButtonDPAD_UP, JoyconRightButtonDPAD_LEFT, JoyconRightButtonPLUS, JoyconRightButtonHOME, JoyconRightButtonSTICK, JoyconRightButtonACC, JoyconRightButtonSPA, JoyconRightRollLeft, JoyconRightRollRight, JoyconRightAccelX, JoyconRightAccelY, JoyconRightGyroX, JoyconRightGyroY, JoyconLeftStickX, JoyconLeftStickY, JoyconLeftButtonSHOULDER_1, JoyconLeftButtonSHOULDER_2, JoyconLeftButtonSR, JoyconLeftButtonSL, JoyconLeftButtonDPAD_DOWN, JoyconLeftButtonDPAD_RIGHT, JoyconLeftButtonDPAD_UP, JoyconLeftButtonDPAD_LEFT, JoyconLeftButtonMINUS, JoyconLeftButtonCAPTURE, JoyconLeftButtonSTICK, JoyconLeftButtonACC, JoyconLeftButtonSMA, JoyconLeftRollLeft, JoyconLeftRollRight, JoyconLeftAccelX, JoyconLeftAccelY, JoyconLeftGyroX, JoyconLeftGyroY, ProControllerLeftStickX, ProControllerLeftStickY, ProControllerRightStickX, ProControllerRightStickY, ProControllerButtonSHOULDER_Left_1, ProControllerButtonSHOULDER_Left_2, ProControllerButtonDPAD_DOWN, ProControllerButtonDPAD_RIGHT, ProControllerButtonDPAD_UP, ProControllerButtonDPAD_LEFT, ProControllerButtonMINUS, ProControllerButtonCAPTURE, ProControllerButtonSTICK_Left, ProControllerButtonSHOULDER_Right_1, ProControllerButtonSHOULDER_Right_2, ProControllerButtonA, ProControllerButtonB, ProControllerButtonX, ProControllerButtonY, ProControllerButtonPLUS, ProControllerButtonHOME, ProControllerButtonSTICK_Right, ProControllerAccelX, ProControllerAccelY, ProControllerGyroX, ProControllerGyroY, camx, camy, Controller1ButtonAPressed, Controller2ButtonAPressed, Controller1ButtonBPressed, Controller2ButtonBPressed, Controller1ButtonXPressed, Controller2ButtonXPressed, Controller1ButtonYPressed, Controller2ButtonYPressed, Controller1ButtonStartPressed, Controller2ButtonStartPressed, Controller1ButtonBackPressed, Controller2ButtonBackPressed, Controller1ButtonDownPressed, Controller2ButtonDownPressed, Controller1ButtonUpPressed, Controller2ButtonUpPressed, Controller1ButtonLeftPressed, Controller2ButtonLeftPressed, Controller1ButtonRightPressed, Controller2ButtonRightPressed, Controller1ButtonShoulderLeftPressed, Controller2ButtonShoulderLeftPressed, Controller1ButtonShoulderRightPressed, Controller2ButtonShoulderRightPressed, Controller1ThumbpadLeftPressed, Controller2ThumbpadLeftPressed, Controller1ThumbpadRightPressed, Controller2ThumbpadRightPressed, Controller1TriggerLeftPosition, Controller2TriggerLeftPosition, Controller1TriggerRightPosition, Controller2TriggerRightPosition, Controller1ThumbLeftX, Controller2ThumbLeftX, Controller1ThumbLeftY, Controller2ThumbLeftY, Controller1ThumbRightX, Controller2ThumbRightX, Controller1ThumbRightY, Controller2ThumbRightY, Joystick1AxisX, Joystick1AxisY, Joystick1AxisZ, Joystick1RotationX, Joystick1RotationY, Joystick1RotationZ, Joystick1Sliders0, Joystick1Sliders1, Joystick1PointOfViewControllers0, Joystick1PointOfViewControllers1, Joystick1PointOfViewControllers2, Joystick1PointOfViewControllers3, Joystick1VelocityX, Joystick1VelocityY, Joystick1VelocityZ, Joystick1AngularVelocityX, Joystick1AngularVelocityY, Joystick1AngularVelocityZ, Joystick1VelocitySliders0, Joystick1VelocitySliders1, Joystick1AccelerationX, Joystick1AccelerationY, Joystick1AccelerationZ, Joystick1AngularAccelerationX, Joystick1AngularAccelerationY, Joystick1AngularAccelerationZ, Joystick1AccelerationSliders0, Joystick1AccelerationSliders1, Joystick1ForceX, Joystick1ForceY, Joystick1ForceZ, Joystick1TorqueX, Joystick1TorqueY, Joystick1TorqueZ, Joystick1ForceSliders0, Joystick1ForceSliders1, Joystick1Buttons0, Joystick1Buttons1, Joystick1Buttons2, Joystick1Buttons3, Joystick1Buttons4, Joystick1Buttons5, Joystick1Buttons6, Joystick1Buttons7, Joystick1Buttons8, Joystick1Buttons9, Joystick1Buttons10, Joystick1Buttons11, Joystick1Buttons12, Joystick1Buttons13, Joystick1Buttons14, Joystick1Buttons15, Joystick1Buttons16, Joystick1Buttons17, Joystick1Buttons18, Joystick1Buttons19, Joystick1Buttons20, Joystick1Buttons21, Joystick1Buttons22, Joystick1Buttons23, Joystick1Buttons24, Joystick1Buttons25, Joystick1Buttons26, Joystick1Buttons27, Joystick1Buttons28, Joystick1Buttons29, Joystick1Buttons30, Joystick1Buttons31, Joystick1Buttons32, Joystick1Buttons33, Joystick1Buttons34, Joystick1Buttons35, Joystick1Buttons36, Joystick1Buttons37, Joystick1Buttons38, Joystick1Buttons39, Joystick1Buttons40, Joystick1Buttons41, Joystick1Buttons42, Joystick1Buttons43, Joystick1Buttons44, Joystick1Buttons45, Joystick1Buttons46, Joystick1Buttons47, Joystick1Buttons48, Joystick1Buttons49, Joystick1Buttons50, Joystick1Buttons51, Joystick1Buttons52, Joystick1Buttons53, Joystick1Buttons54, Joystick1Buttons55, Joystick1Buttons56, Joystick1Buttons57, Joystick1Buttons58, Joystick1Buttons59, Joystick1Buttons60, Joystick1Buttons61, Joystick1Buttons62, Joystick1Buttons63, Joystick1Buttons64, Joystick1Buttons65, Joystick1Buttons66, Joystick1Buttons67, Joystick1Buttons68, Joystick1Buttons69, Joystick1Buttons70, Joystick1Buttons71, Joystick1Buttons72, Joystick1Buttons73, Joystick1Buttons74, Joystick1Buttons75, Joystick1Buttons76, Joystick1Buttons77, Joystick1Buttons78, Joystick1Buttons79, Joystick1Buttons80, Joystick1Buttons81, Joystick1Buttons82, Joystick1Buttons83, Joystick1Buttons84, Joystick1Buttons85, Joystick1Buttons86, Joystick1Buttons87, Joystick1Buttons88, Joystick1Buttons89, Joystick1Buttons90, Joystick1Buttons91, Joystick1Buttons92, Joystick1Buttons93, Joystick1Buttons94, Joystick1Buttons95, Joystick1Buttons96, Joystick1Buttons97, Joystick1Buttons98, Joystick1Buttons99, Joystick1Buttons100, Joystick1Buttons101, Joystick1Buttons102, Joystick1Buttons103, Joystick1Buttons104, Joystick1Buttons105, Joystick1Buttons106, Joystick1Buttons107, Joystick1Buttons108, Joystick1Buttons109, Joystick1Buttons110, Joystick1Buttons111, Joystick1Buttons112, Joystick1Buttons113, Joystick1Buttons114, Joystick1Buttons115, Joystick1Buttons116, Joystick1Buttons117, Joystick1Buttons118, Joystick1Buttons119, Joystick1Buttons120, Joystick1Buttons121, Joystick1Buttons122, Joystick1Buttons123, Joystick1Buttons124, Joystick1Buttons125, Joystick1Buttons126, Joystick1Buttons127, Joystick2AxisX, Joystick2AxisY, Joystick2AxisZ, Joystick2RotationX, Joystick2RotationY, Joystick2RotationZ, Joystick2Sliders0, Joystick2Sliders1, Joystick2PointOfViewControllers0, Joystick2PointOfViewControllers1, Joystick2PointOfViewControllers2, Joystick2PointOfViewControllers3, Joystick2VelocityX, Joystick2VelocityY, Joystick2VelocityZ, Joystick2AngularVelocityX, Joystick2AngularVelocityY, Joystick2AngularVelocityZ, Joystick2VelocitySliders0, Joystick2VelocitySliders1, Joystick2AccelerationX, Joystick2AccelerationY, Joystick2AccelerationZ, Joystick2AngularAccelerationX, Joystick2AngularAccelerationY, Joystick2AngularAccelerationZ, Joystick2AccelerationSliders0, Joystick2AccelerationSliders1, Joystick2ForceX, Joystick2ForceY, Joystick2ForceZ, Joystick2TorqueX, Joystick2TorqueY, Joystick2TorqueZ, Joystick2ForceSliders0, Joystick2ForceSliders1, Joystick2Buttons0, Joystick2Buttons1, Joystick2Buttons2, Joystick2Buttons3, Joystick2Buttons4, Joystick2Buttons5, Joystick2Buttons6, Joystick2Buttons7, Joystick2Buttons8, Joystick2Buttons9, Joystick2Buttons10, Joystick2Buttons11, Joystick2Buttons12, Joystick2Buttons13, Joystick2Buttons14, Joystick2Buttons15, Joystick2Buttons16, Joystick2Buttons17, Joystick2Buttons18, Joystick2Buttons19, Joystick2Buttons20, Joystick2Buttons21, Joystick2Buttons22, Joystick2Buttons23, Joystick2Buttons24, Joystick2Buttons25, Joystick2Buttons26, Joystick2Buttons27, Joystick2Buttons28, Joystick2Buttons29, Joystick2Buttons30, Joystick2Buttons31, Joystick2Buttons32, Joystick2Buttons33, Joystick2Buttons34, Joystick2Buttons35, Joystick2Buttons36, Joystick2Buttons37, Joystick2Buttons38, Joystick2Buttons39, Joystick2Buttons40, Joystick2Buttons41, Joystick2Buttons42, Joystick2Buttons43, Joystick2Buttons44, Joystick2Buttons45, Joystick2Buttons46, Joystick2Buttons47, Joystick2Buttons48, Joystick2Buttons49, Joystick2Buttons50, Joystick2Buttons51, Joystick2Buttons52, Joystick2Buttons53, Joystick2Buttons54, Joystick2Buttons55, Joystick2Buttons56, Joystick2Buttons57, Joystick2Buttons58, Joystick2Buttons59, Joystick2Buttons60, Joystick2Buttons61, Joystick2Buttons62, Joystick2Buttons63, Joystick2Buttons64, Joystick2Buttons65, Joystick2Buttons66, Joystick2Buttons67, Joystick2Buttons68, Joystick2Buttons69, Joystick2Buttons70, Joystick2Buttons71, Joystick2Buttons72, Joystick2Buttons73, Joystick2Buttons74, Joystick2Buttons75, Joystick2Buttons76, Joystick2Buttons77, Joystick2Buttons78, Joystick2Buttons79, Joystick2Buttons80, Joystick2Buttons81, Joystick2Buttons82, Joystick2Buttons83, Joystick2Buttons84, Joystick2Buttons85, Joystick2Buttons86, Joystick2Buttons87, Joystick2Buttons88, Joystick2Buttons89, Joystick2Buttons90, Joystick2Buttons91, Joystick2Buttons92, Joystick2Buttons93, Joystick2Buttons94, Joystick2Buttons95, Joystick2Buttons96, Joystick2Buttons97, Joystick2Buttons98, Joystick2Buttons99, Joystick2Buttons100, Joystick2Buttons101, Joystick2Buttons102, Joystick2Buttons103, Joystick2Buttons104, Joystick2Buttons105, Joystick2Buttons106, Joystick2Buttons107, Joystick2Buttons108, Joystick2Buttons109, Joystick2Buttons110, Joystick2Buttons111, Joystick2Buttons112, Joystick2Buttons113, Joystick2Buttons114, Joystick2Buttons115, Joystick2Buttons116, Joystick2Buttons117, Joystick2Buttons118, Joystick2Buttons119, Joystick2Buttons120, Joystick2Buttons121, Joystick2Buttons122, Joystick2Buttons123, Joystick2Buttons124, Joystick2Buttons125, Joystick2Buttons126, Joystick2Buttons127, Mouse1Buttons0, Mouse1Buttons1, Mouse1Buttons2, Mouse1Buttons3, Mouse1Buttons4, Mouse1Buttons5, Mouse1Buttons6, Mouse1Buttons7, MouseHookX, MouseHookY, Mouse1AxisX, Mouse1AxisY, Mouse1AxisZ, Mouse2Buttons0, Mouse2Buttons1, Mouse2Buttons2, Mouse2Buttons3, Mouse2Buttons4, Mouse2Buttons5, Mouse2Buttons6, Mouse2Buttons7, Mouse2AxisX, Mouse2AxisY, Mouse2AxisZ, Keyboard1KeyEscape, Keyboard1KeyD1, Keyboard1KeyD2, Keyboard1KeyD3, Keyboard1KeyD4, Keyboard1KeyD5, Keyboard1KeyD6, Keyboard1KeyD7, Keyboard1KeyD8, Keyboard1KeyD9, Keyboard1KeyD0, Keyboard1KeyMinus, Keyboard1KeyEquals, Keyboard1KeyBack, Keyboard1KeyTab, Keyboard1KeyQ, Keyboard1KeyW, Keyboard1KeyE, Keyboard1KeyR, Keyboard1KeyT, Keyboard1KeyY, Keyboard1KeyU, Keyboard1KeyI, Keyboard1KeyO, Keyboard1KeyP, Keyboard1KeyLeftBracket, Keyboard1KeyRightBracket, Keyboard1KeyReturn, Keyboard1KeyLeftControl, Keyboard1KeyA, Keyboard1KeyS, Keyboard1KeyD, Keyboard1KeyF, Keyboard1KeyG, Keyboard1KeyH, Keyboard1KeyJ, Keyboard1KeyK, Keyboard1KeyL, Keyboard1KeySemicolon, Keyboard1KeyApostrophe, Keyboard1KeyGrave, Keyboard1KeyLeftShift, Keyboard1KeyBackslash, Keyboard1KeyZ, Keyboard1KeyX, Keyboard1KeyC, Keyboard1KeyV, Keyboard1KeyB, Keyboard1KeyN, Keyboard1KeyM, Keyboard1KeyComma, Keyboard1KeyPeriod, Keyboard1KeySlash, Keyboard1KeyRightShift, Keyboard1KeyMultiply, Keyboard1KeyLeftAlt, Keyboard1KeySpace, Keyboard1KeyCapital, Keyboard1KeyF1, Keyboard1KeyF2, Keyboard1KeyF3, Keyboard1KeyF4, Keyboard1KeyF5, Keyboard1KeyF6, Keyboard1KeyF7, Keyboard1KeyF8, Keyboard1KeyF9, Keyboard1KeyF10, Keyboard1KeyNumberLock, Keyboard1KeyScrollLock, Keyboard1KeyNumberPad7, Keyboard1KeyNumberPad8, Keyboard1KeyNumberPad9, Keyboard1KeySubtract, Keyboard1KeyNumberPad4, Keyboard1KeyNumberPad5, Keyboard1KeyNumberPad6, Keyboard1KeyAdd, Keyboard1KeyNumberPad1, Keyboard1KeyNumberPad2, Keyboard1KeyNumberPad3, Keyboard1KeyNumberPad0, Keyboard1KeyDecimal, Keyboard1KeyOem102, Keyboard1KeyF11, Keyboard1KeyF12, Keyboard1KeyF13, Keyboard1KeyF14, Keyboard1KeyF15, Keyboard1KeyKana, Keyboard1KeyAbntC1, Keyboard1KeyConvert, Keyboard1KeyNoConvert, Keyboard1KeyYen, Keyboard1KeyAbntC2, Keyboard1KeyNumberPadEquals, Keyboard1KeyPreviousTrack, Keyboard1KeyAT, Keyboard1KeyColon, Keyboard1KeyUnderline, Keyboard1KeyKanji, Keyboard1KeyStop, Keyboard1KeyAX, Keyboard1KeyUnlabeled, Keyboard1KeyNextTrack, Keyboard1KeyNumberPadEnter, Keyboard1KeyRightControl, Keyboard1KeyMute, Keyboard1KeyCalculator, Keyboard1KeyPlayPause, Keyboard1KeyMediaStop, Keyboard1KeyVolumeDown, Keyboard1KeyVolumeUp, Keyboard1KeyWebHome, Keyboard1KeyNumberPadComma, Keyboard1KeyDivide, Keyboard1KeyPrintScreen, Keyboard1KeyRightAlt, Keyboard1KeyPause, Keyboard1KeyHome, Keyboard1KeyUp, Keyboard1KeyPageUp, Keyboard1KeyLeft, Keyboard1KeyRight, Keyboard1KeyEnd, Keyboard1KeyDown, Keyboard1KeyPageDown, Keyboard1KeyInsert, Keyboard1KeyDelete, Keyboard1KeyLeftWindowsKey, Keyboard1KeyRightWindowsKey, Keyboard1KeyApplications, Keyboard1KeyPower, Keyboard1KeySleep, Keyboard1KeyWake, Keyboard1KeyWebSearch, Keyboard1KeyWebFavorites, Keyboard1KeyWebRefresh, Keyboard1KeyWebStop, Keyboard1KeyWebForward, Keyboard1KeyWebBack, Keyboard1KeyMyComputer, Keyboard1KeyMail, Keyboard1KeyMediaSelect, Keyboard1KeyUnknown, Keyboard2KeyEscape, Keyboard2KeyD1, Keyboard2KeyD2, Keyboard2KeyD3, Keyboard2KeyD4, Keyboard2KeyD5, Keyboard2KeyD6, Keyboard2KeyD7, Keyboard2KeyD8, Keyboard2KeyD9, Keyboard2KeyD0, Keyboard2KeyMinus, Keyboard2KeyEquals, Keyboard2KeyBack, Keyboard2KeyTab, Keyboard2KeyQ, Keyboard2KeyW, Keyboard2KeyE, Keyboard2KeyR, Keyboard2KeyT, Keyboard2KeyY, Keyboard2KeyU, Keyboard2KeyI, Keyboard2KeyO, Keyboard2KeyP, Keyboard2KeyLeftBracket, Keyboard2KeyRightBracket, Keyboard2KeyReturn, Keyboard2KeyLeftControl, Keyboard2KeyA, Keyboard2KeyS, Keyboard2KeyD, Keyboard2KeyF, Keyboard2KeyG, Keyboard2KeyH, Keyboard2KeyJ, Keyboard2KeyK, Keyboard2KeyL, Keyboard2KeySemicolon, Keyboard2KeyApostrophe, Keyboard2KeyGrave, Keyboard2KeyLeftShift, Keyboard2KeyBackslash, Keyboard2KeyZ, Keyboard2KeyX, Keyboard2KeyC, Keyboard2KeyV, Keyboard2KeyB, Keyboard2KeyN, Keyboard2KeyM, Keyboard2KeyComma, Keyboard2KeyPeriod, Keyboard2KeySlash, Keyboard2KeyRightShift, Keyboard2KeyMultiply, Keyboard2KeyLeftAlt, Keyboard2KeySpace, Keyboard2KeyCapital, Keyboard2KeyF1, Keyboard2KeyF2, Keyboard2KeyF3, Keyboard2KeyF4, Keyboard2KeyF5, Keyboard2KeyF6, Keyboard2KeyF7, Keyboard2KeyF8, Keyboard2KeyF9, Keyboard2KeyF10, Keyboard2KeyNumberLock, Keyboard2KeyScrollLock, Keyboard2KeyNumberPad7, Keyboard2KeyNumberPad8, Keyboard2KeyNumberPad9, Keyboard2KeySubtract, Keyboard2KeyNumberPad4, Keyboard2KeyNumberPad5, Keyboard2KeyNumberPad6, Keyboard2KeyAdd, Keyboard2KeyNumberPad1, Keyboard2KeyNumberPad2, Keyboard2KeyNumberPad3, Keyboard2KeyNumberPad0, Keyboard2KeyDecimal, Keyboard2KeyOem102, Keyboard2KeyF11, Keyboard2KeyF12, Keyboard2KeyF13, Keyboard2KeyF14, Keyboard2KeyF15, Keyboard2KeyKana, Keyboard2KeyAbntC1, Keyboard2KeyConvert, Keyboard2KeyNoConvert, Keyboard2KeyYen, Keyboard2KeyAbntC2, Keyboard2KeyNumberPadEquals, Keyboard2KeyPreviousTrack, Keyboard2KeyAT, Keyboard2KeyColon, Keyboard2KeyUnderline, Keyboard2KeyKanji, Keyboard2KeyStop, Keyboard2KeyAX, Keyboard2KeyUnlabeled, Keyboard2KeyNextTrack, Keyboard2KeyNumberPadEnter, Keyboard2KeyRightControl, Keyboard2KeyMute, Keyboard2KeyCalculator, Keyboard2KeyPlayPause, Keyboard2KeyMediaStop, Keyboard2KeyVolumeDown, Keyboard2KeyVolumeUp, Keyboard2KeyWebHome, Keyboard2KeyNumberPadComma, Keyboard2KeyDivide, Keyboard2KeyPrintScreen, Keyboard2KeyRightAlt, Keyboard2KeyPause, Keyboard2KeyHome, Keyboard2KeyUp, Keyboard2KeyPageUp, Keyboard2KeyLeft, Keyboard2KeyRight, Keyboard2KeyEnd, Keyboard2KeyDown, Keyboard2KeyPageDown, Keyboard2KeyInsert, Keyboard2KeyDelete, Keyboard2KeyLeftWindowsKey, Keyboard2KeyRightWindowsKey, Keyboard2KeyApplications, Keyboard2KeyPower, Keyboard2KeySleep, Keyboard2KeyWake, Keyboard2KeyWebSearch, Keyboard2KeyWebFavorites, Keyboard2KeyWebRefresh, Keyboard2KeyWebStop, Keyboard2KeyWebForward, Keyboard2KeyWebBack, Keyboard2KeyMyComputer, Keyboard2KeyMail, Keyboard2KeyMediaSelect, Keyboard2KeyUnknown, TextFromSpeech, PS5ControllerLeftStickX, PS5ControllerLeftStickY, PS5ControllerRightStickX, PS5ControllerRightStickY, PS5ControllerLeftTriggerPosition, PS5ControllerRightTriggerPosition, PS5ControllerTouchX, PS5ControllerTouchY, PS5ControllerTouchOn, PS5ControllerGyroX, PS5ControllerGyroY, PS5ControllerAccelX, PS5ControllerAccelY, PS5ControllerButtonCrossPressed, PS5ControllerButtonCirclePressed, PS5ControllerButtonSquarePressed, PS5ControllerButtonTrianglePressed, PS5ControllerButtonDPadUpPressed, PS5ControllerButtonDPadRightPressed, PS5ControllerButtonDPadDownPressed, PS5ControllerButtonDPadLeftPressed, PS5ControllerButtonL1Pressed, PS5ControllerButtonR1Pressed, PS5ControllerButtonL2Pressed, PS5ControllerButtonR2Pressed, PS5ControllerButtonL3Pressed, PS5ControllerButtonR3Pressed, PS5ControllerButtonCreatePressed, PS5ControllerButtonMenuPressed, PS5ControllerButtonLogoPressed, PS5ControllerButtonTouchpadPressed, PS5ControllerButtonMicPressed, Controller1GyroX, Controller1GyroY, Controller2GyroX, Controller2GyroY });
                UsersAllowedList = (string[])val[0];
                checkingusername = false;
                if (UsersAllowedList.Length == 0)
                {
                    checkingusername = true;
                }
                else
                {
                    foreach (string userallowed in UsersAllowedList)
                    {
                        if (username == userallowed)
                        {
                            checkingusername = true;
                            break;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                if (!checkingusername)
                {
                    MessageBox.Show("You are not allowed to run this script.");
                }
            }
            catch { }
            scriptrunning = false;
        }
        private void ProcessScript()
        {
            try
            {
                fastColoredTextBox1.ReadOnly = true;
                fastColoredTextBox1.Enabled = false;
                if (fastColoredTextBox1.Text != "Script Encrypted...")
                {
                    stringscript = fastColoredTextBox1.Text;
                }
                scriptrunning = true;
                string finalcode = code.Replace("funct_driver", stringscript);
                parameters = new System.CodeDom.Compiler.CompilerParameters();
                parameters.GenerateExecutable = false;
                parameters.GenerateInMemory = true;
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                provider = new Microsoft.CSharp.CSharpCodeProvider();
                results = provider.CompileAssemblyFromSource(parameters, finalcode);
                if (results.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}) : {1}", error.ErrorNumber, error.ErrorText));
                    }
                    MessageBox.Show("Script Error :\n\r" + sb.ToString());
                    StopScript();
                    return;
                }
                assembly = results.CompiledAssembly;
                program = assembly.GetType("StringToCode.FooClass");
                obj = Activator.CreateInstance(program);
                object[] val = (object[])program.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, new object[] { width, height, Key_LBUTTON, Key_RBUTTON, Key_CANCEL, Key_MBUTTON, Key_XBUTTON1, Key_XBUTTON2, Key_BACK, Key_Tab, Key_CLEAR, Key_Return, Key_SHIFT, Key_CONTROL, Key_MENU, Key_PAUSE, Key_CAPITAL, Key_KANA, Key_HANGEUL, Key_HANGUL, Key_JUNJA, Key_FINAL, Key_HANJA, Key_KANJI, Key_Escape, Key_CONVERT, Key_NONCONVERT, Key_ACCEPT, Key_MODECHANGE, Key_Space, Key_PRIOR, Key_NEXT, Key_END, Key_HOME, Key_LEFT, Key_UP, Key_RIGHT, Key_DOWN, Key_SELECT, Key_PRINT, Key_EXECUTE, Key_SNAPSHOT, Key_INSERT, Key_DELETE, Key_HELP, Key_APOSTROPHE, Key_0, Key_1, Key_2, Key_3, Key_4, Key_5, Key_6, Key_7, Key_8, Key_9, Key_A, Key_B, Key_C, Key_D, Key_E, Key_F, Key_G, Key_H, Key_I, Key_J, Key_K, Key_L, Key_M, Key_N, Key_O, Key_P, Key_Q, Key_R, Key_S, Key_T, Key_U, Key_V, Key_W, Key_X, Key_Y, Key_Z, Key_LWIN, Key_RWIN, Key_APPS, Key_SLEEP, Key_NUMPAD0, Key_NUMPAD1, Key_NUMPAD2, Key_NUMPAD3, Key_NUMPAD4, Key_NUMPAD5, Key_NUMPAD6, Key_NUMPAD7, Key_NUMPAD8, Key_NUMPAD9, Key_MULTIPLY, Key_ADD, Key_SEPARATOR, Key_SUBTRACT, Key_DECIMAL, Key_DIVIDE, Key_F1, Key_F2, Key_F3, Key_F4, Key_F5, Key_F6, Key_F7, Key_F8, Key_F9, Key_F10, Key_F11, Key_F12, Key_F13, Key_F14, Key_F15, Key_F16, Key_F17, Key_F18, Key_F19, Key_F20, Key_F21, Key_F22, Key_F23, Key_F24, Key_NUMLOCK, Key_SCROLL, Key_LeftShift, Key_RightShift, Key_LeftControl, Key_RightControl, Key_LMENU, Key_RMENU, Key_BROWSER_BACK, Key_BROWSER_FORWARD, Key_BROWSER_REFRESH, Key_BROWSER_STOP, Key_BROWSER_SEARCH, Key_BROWSER_FAVORITES, Key_BROWSER_HOME, Key_VOLUME_MUTE, Key_VOLUME_DOWN, Key_VOLUME_UP, Key_MEDIA_NEXT_TRACK, Key_MEDIA_PREV_TRACK, Key_MEDIA_STOP, Key_MEDIA_PLAY_PAUSE, Key_LAUNCH_MAIL, Key_LAUNCH_MEDIA_SELECT, Key_LAUNCH_APP1, Key_LAUNCH_APP2, Key_OEM_1, Key_OEM_PLUS, Key_OEM_COMMA, Key_OEM_MINUS, Key_OEM_PERIOD, Key_OEM_2, Key_OEM_3, Key_OEM_4, Key_OEM_5, Key_OEM_6, Key_OEM_7, Key_OEM_8, Key_OEM_102, Key_PROCESSKEY, Key_PACKET, Key_ATTN, Key_CRSEL, Key_EXSEL, Key_EREOF, Key_PLAY, Key_ZOOM, Key_NONAME, Key_PA1, Key_OEM_CLEAR, irx, iry, WiimoteButtonStateA, WiimoteButtonStateB, WiimoteButtonStateMinus, WiimoteButtonStateHome, WiimoteButtonStatePlus, WiimoteButtonStateOne, WiimoteButtonStateTwo, WiimoteButtonStateUp, WiimoteButtonStateDown, WiimoteButtonStateLeft, WiimoteButtonStateRight, WiimoteRawValuesX, WiimoteRawValuesY, WiimoteRawValuesZ, WiimoteNunchuckStateRawJoystickX, WiimoteNunchuckStateRawJoystickY, WiimoteNunchuckStateRawValuesX, WiimoteNunchuckStateRawValuesY, WiimoteNunchuckStateRawValuesZ, WiimoteNunchuckStateC, WiimoteNunchuckStateZ, JoyconRightStickX, JoyconRightStickY, JoyconRightButtonSHOULDER_1, JoyconRightButtonSHOULDER_2, JoyconRightButtonSR, JoyconRightButtonSL, JoyconRightButtonDPAD_DOWN, JoyconRightButtonDPAD_RIGHT, JoyconRightButtonDPAD_UP, JoyconRightButtonDPAD_LEFT, JoyconRightButtonPLUS, JoyconRightButtonHOME, JoyconRightButtonSTICK, JoyconRightButtonACC, JoyconRightButtonSPA, JoyconRightRollLeft, JoyconRightRollRight, JoyconRightAccelX, JoyconRightAccelY, JoyconRightGyroX, JoyconRightGyroY, JoyconLeftStickX, JoyconLeftStickY, JoyconLeftButtonSHOULDER_1, JoyconLeftButtonSHOULDER_2, JoyconLeftButtonSR, JoyconLeftButtonSL, JoyconLeftButtonDPAD_DOWN, JoyconLeftButtonDPAD_RIGHT, JoyconLeftButtonDPAD_UP, JoyconLeftButtonDPAD_LEFT, JoyconLeftButtonMINUS, JoyconLeftButtonCAPTURE, JoyconLeftButtonSTICK, JoyconLeftButtonACC, JoyconLeftButtonSMA, JoyconLeftRollLeft, JoyconLeftRollRight, JoyconLeftAccelX, JoyconLeftAccelY, JoyconLeftGyroX, JoyconLeftGyroY, ProControllerLeftStickX, ProControllerLeftStickY, ProControllerRightStickX, ProControllerRightStickY, ProControllerButtonSHOULDER_Left_1, ProControllerButtonSHOULDER_Left_2, ProControllerButtonDPAD_DOWN, ProControllerButtonDPAD_RIGHT, ProControllerButtonDPAD_UP, ProControllerButtonDPAD_LEFT, ProControllerButtonMINUS, ProControllerButtonCAPTURE, ProControllerButtonSTICK_Left, ProControllerButtonSHOULDER_Right_1, ProControllerButtonSHOULDER_Right_2, ProControllerButtonA, ProControllerButtonB, ProControllerButtonX, ProControllerButtonY, ProControllerButtonPLUS, ProControllerButtonHOME, ProControllerButtonSTICK_Right, ProControllerAccelX, ProControllerAccelY, ProControllerGyroX, ProControllerGyroY, camx, camy, Controller1ButtonAPressed, Controller2ButtonAPressed, Controller1ButtonBPressed, Controller2ButtonBPressed, Controller1ButtonXPressed, Controller2ButtonXPressed, Controller1ButtonYPressed, Controller2ButtonYPressed, Controller1ButtonStartPressed, Controller2ButtonStartPressed, Controller1ButtonBackPressed, Controller2ButtonBackPressed, Controller1ButtonDownPressed, Controller2ButtonDownPressed, Controller1ButtonUpPressed, Controller2ButtonUpPressed, Controller1ButtonLeftPressed, Controller2ButtonLeftPressed, Controller1ButtonRightPressed, Controller2ButtonRightPressed, Controller1ButtonShoulderLeftPressed, Controller2ButtonShoulderLeftPressed, Controller1ButtonShoulderRightPressed, Controller2ButtonShoulderRightPressed, Controller1ThumbpadLeftPressed, Controller2ThumbpadLeftPressed, Controller1ThumbpadRightPressed, Controller2ThumbpadRightPressed, Controller1TriggerLeftPosition, Controller2TriggerLeftPosition, Controller1TriggerRightPosition, Controller2TriggerRightPosition, Controller1ThumbLeftX, Controller2ThumbLeftX, Controller1ThumbLeftY, Controller2ThumbLeftY, Controller1ThumbRightX, Controller2ThumbRightX, Controller1ThumbRightY, Controller2ThumbRightY, Joystick1AxisX, Joystick1AxisY, Joystick1AxisZ, Joystick1RotationX, Joystick1RotationY, Joystick1RotationZ, Joystick1Sliders0, Joystick1Sliders1, Joystick1PointOfViewControllers0, Joystick1PointOfViewControllers1, Joystick1PointOfViewControllers2, Joystick1PointOfViewControllers3, Joystick1VelocityX, Joystick1VelocityY, Joystick1VelocityZ, Joystick1AngularVelocityX, Joystick1AngularVelocityY, Joystick1AngularVelocityZ, Joystick1VelocitySliders0, Joystick1VelocitySliders1, Joystick1AccelerationX, Joystick1AccelerationY, Joystick1AccelerationZ, Joystick1AngularAccelerationX, Joystick1AngularAccelerationY, Joystick1AngularAccelerationZ, Joystick1AccelerationSliders0, Joystick1AccelerationSliders1, Joystick1ForceX, Joystick1ForceY, Joystick1ForceZ, Joystick1TorqueX, Joystick1TorqueY, Joystick1TorqueZ, Joystick1ForceSliders0, Joystick1ForceSliders1, Joystick1Buttons0, Joystick1Buttons1, Joystick1Buttons2, Joystick1Buttons3, Joystick1Buttons4, Joystick1Buttons5, Joystick1Buttons6, Joystick1Buttons7, Joystick1Buttons8, Joystick1Buttons9, Joystick1Buttons10, Joystick1Buttons11, Joystick1Buttons12, Joystick1Buttons13, Joystick1Buttons14, Joystick1Buttons15, Joystick1Buttons16, Joystick1Buttons17, Joystick1Buttons18, Joystick1Buttons19, Joystick1Buttons20, Joystick1Buttons21, Joystick1Buttons22, Joystick1Buttons23, Joystick1Buttons24, Joystick1Buttons25, Joystick1Buttons26, Joystick1Buttons27, Joystick1Buttons28, Joystick1Buttons29, Joystick1Buttons30, Joystick1Buttons31, Joystick1Buttons32, Joystick1Buttons33, Joystick1Buttons34, Joystick1Buttons35, Joystick1Buttons36, Joystick1Buttons37, Joystick1Buttons38, Joystick1Buttons39, Joystick1Buttons40, Joystick1Buttons41, Joystick1Buttons42, Joystick1Buttons43, Joystick1Buttons44, Joystick1Buttons45, Joystick1Buttons46, Joystick1Buttons47, Joystick1Buttons48, Joystick1Buttons49, Joystick1Buttons50, Joystick1Buttons51, Joystick1Buttons52, Joystick1Buttons53, Joystick1Buttons54, Joystick1Buttons55, Joystick1Buttons56, Joystick1Buttons57, Joystick1Buttons58, Joystick1Buttons59, Joystick1Buttons60, Joystick1Buttons61, Joystick1Buttons62, Joystick1Buttons63, Joystick1Buttons64, Joystick1Buttons65, Joystick1Buttons66, Joystick1Buttons67, Joystick1Buttons68, Joystick1Buttons69, Joystick1Buttons70, Joystick1Buttons71, Joystick1Buttons72, Joystick1Buttons73, Joystick1Buttons74, Joystick1Buttons75, Joystick1Buttons76, Joystick1Buttons77, Joystick1Buttons78, Joystick1Buttons79, Joystick1Buttons80, Joystick1Buttons81, Joystick1Buttons82, Joystick1Buttons83, Joystick1Buttons84, Joystick1Buttons85, Joystick1Buttons86, Joystick1Buttons87, Joystick1Buttons88, Joystick1Buttons89, Joystick1Buttons90, Joystick1Buttons91, Joystick1Buttons92, Joystick1Buttons93, Joystick1Buttons94, Joystick1Buttons95, Joystick1Buttons96, Joystick1Buttons97, Joystick1Buttons98, Joystick1Buttons99, Joystick1Buttons100, Joystick1Buttons101, Joystick1Buttons102, Joystick1Buttons103, Joystick1Buttons104, Joystick1Buttons105, Joystick1Buttons106, Joystick1Buttons107, Joystick1Buttons108, Joystick1Buttons109, Joystick1Buttons110, Joystick1Buttons111, Joystick1Buttons112, Joystick1Buttons113, Joystick1Buttons114, Joystick1Buttons115, Joystick1Buttons116, Joystick1Buttons117, Joystick1Buttons118, Joystick1Buttons119, Joystick1Buttons120, Joystick1Buttons121, Joystick1Buttons122, Joystick1Buttons123, Joystick1Buttons124, Joystick1Buttons125, Joystick1Buttons126, Joystick1Buttons127, Joystick2AxisX, Joystick2AxisY, Joystick2AxisZ, Joystick2RotationX, Joystick2RotationY, Joystick2RotationZ, Joystick2Sliders0, Joystick2Sliders1, Joystick2PointOfViewControllers0, Joystick2PointOfViewControllers1, Joystick2PointOfViewControllers2, Joystick2PointOfViewControllers3, Joystick2VelocityX, Joystick2VelocityY, Joystick2VelocityZ, Joystick2AngularVelocityX, Joystick2AngularVelocityY, Joystick2AngularVelocityZ, Joystick2VelocitySliders0, Joystick2VelocitySliders1, Joystick2AccelerationX, Joystick2AccelerationY, Joystick2AccelerationZ, Joystick2AngularAccelerationX, Joystick2AngularAccelerationY, Joystick2AngularAccelerationZ, Joystick2AccelerationSliders0, Joystick2AccelerationSliders1, Joystick2ForceX, Joystick2ForceY, Joystick2ForceZ, Joystick2TorqueX, Joystick2TorqueY, Joystick2TorqueZ, Joystick2ForceSliders0, Joystick2ForceSliders1, Joystick2Buttons0, Joystick2Buttons1, Joystick2Buttons2, Joystick2Buttons3, Joystick2Buttons4, Joystick2Buttons5, Joystick2Buttons6, Joystick2Buttons7, Joystick2Buttons8, Joystick2Buttons9, Joystick2Buttons10, Joystick2Buttons11, Joystick2Buttons12, Joystick2Buttons13, Joystick2Buttons14, Joystick2Buttons15, Joystick2Buttons16, Joystick2Buttons17, Joystick2Buttons18, Joystick2Buttons19, Joystick2Buttons20, Joystick2Buttons21, Joystick2Buttons22, Joystick2Buttons23, Joystick2Buttons24, Joystick2Buttons25, Joystick2Buttons26, Joystick2Buttons27, Joystick2Buttons28, Joystick2Buttons29, Joystick2Buttons30, Joystick2Buttons31, Joystick2Buttons32, Joystick2Buttons33, Joystick2Buttons34, Joystick2Buttons35, Joystick2Buttons36, Joystick2Buttons37, Joystick2Buttons38, Joystick2Buttons39, Joystick2Buttons40, Joystick2Buttons41, Joystick2Buttons42, Joystick2Buttons43, Joystick2Buttons44, Joystick2Buttons45, Joystick2Buttons46, Joystick2Buttons47, Joystick2Buttons48, Joystick2Buttons49, Joystick2Buttons50, Joystick2Buttons51, Joystick2Buttons52, Joystick2Buttons53, Joystick2Buttons54, Joystick2Buttons55, Joystick2Buttons56, Joystick2Buttons57, Joystick2Buttons58, Joystick2Buttons59, Joystick2Buttons60, Joystick2Buttons61, Joystick2Buttons62, Joystick2Buttons63, Joystick2Buttons64, Joystick2Buttons65, Joystick2Buttons66, Joystick2Buttons67, Joystick2Buttons68, Joystick2Buttons69, Joystick2Buttons70, Joystick2Buttons71, Joystick2Buttons72, Joystick2Buttons73, Joystick2Buttons74, Joystick2Buttons75, Joystick2Buttons76, Joystick2Buttons77, Joystick2Buttons78, Joystick2Buttons79, Joystick2Buttons80, Joystick2Buttons81, Joystick2Buttons82, Joystick2Buttons83, Joystick2Buttons84, Joystick2Buttons85, Joystick2Buttons86, Joystick2Buttons87, Joystick2Buttons88, Joystick2Buttons89, Joystick2Buttons90, Joystick2Buttons91, Joystick2Buttons92, Joystick2Buttons93, Joystick2Buttons94, Joystick2Buttons95, Joystick2Buttons96, Joystick2Buttons97, Joystick2Buttons98, Joystick2Buttons99, Joystick2Buttons100, Joystick2Buttons101, Joystick2Buttons102, Joystick2Buttons103, Joystick2Buttons104, Joystick2Buttons105, Joystick2Buttons106, Joystick2Buttons107, Joystick2Buttons108, Joystick2Buttons109, Joystick2Buttons110, Joystick2Buttons111, Joystick2Buttons112, Joystick2Buttons113, Joystick2Buttons114, Joystick2Buttons115, Joystick2Buttons116, Joystick2Buttons117, Joystick2Buttons118, Joystick2Buttons119, Joystick2Buttons120, Joystick2Buttons121, Joystick2Buttons122, Joystick2Buttons123, Joystick2Buttons124, Joystick2Buttons125, Joystick2Buttons126, Joystick2Buttons127, Mouse1Buttons0, Mouse1Buttons1, Mouse1Buttons2, Mouse1Buttons3, Mouse1Buttons4, Mouse1Buttons5, Mouse1Buttons6, Mouse1Buttons7, MouseHookX, MouseHookY, Mouse1AxisX, Mouse1AxisY, Mouse1AxisZ, Mouse2Buttons0, Mouse2Buttons1, Mouse2Buttons2, Mouse2Buttons3, Mouse2Buttons4, Mouse2Buttons5, Mouse2Buttons6, Mouse2Buttons7, Mouse2AxisX, Mouse2AxisY, Mouse2AxisZ, Keyboard1KeyEscape, Keyboard1KeyD1, Keyboard1KeyD2, Keyboard1KeyD3, Keyboard1KeyD4, Keyboard1KeyD5, Keyboard1KeyD6, Keyboard1KeyD7, Keyboard1KeyD8, Keyboard1KeyD9, Keyboard1KeyD0, Keyboard1KeyMinus, Keyboard1KeyEquals, Keyboard1KeyBack, Keyboard1KeyTab, Keyboard1KeyQ, Keyboard1KeyW, Keyboard1KeyE, Keyboard1KeyR, Keyboard1KeyT, Keyboard1KeyY, Keyboard1KeyU, Keyboard1KeyI, Keyboard1KeyO, Keyboard1KeyP, Keyboard1KeyLeftBracket, Keyboard1KeyRightBracket, Keyboard1KeyReturn, Keyboard1KeyLeftControl, Keyboard1KeyA, Keyboard1KeyS, Keyboard1KeyD, Keyboard1KeyF, Keyboard1KeyG, Keyboard1KeyH, Keyboard1KeyJ, Keyboard1KeyK, Keyboard1KeyL, Keyboard1KeySemicolon, Keyboard1KeyApostrophe, Keyboard1KeyGrave, Keyboard1KeyLeftShift, Keyboard1KeyBackslash, Keyboard1KeyZ, Keyboard1KeyX, Keyboard1KeyC, Keyboard1KeyV, Keyboard1KeyB, Keyboard1KeyN, Keyboard1KeyM, Keyboard1KeyComma, Keyboard1KeyPeriod, Keyboard1KeySlash, Keyboard1KeyRightShift, Keyboard1KeyMultiply, Keyboard1KeyLeftAlt, Keyboard1KeySpace, Keyboard1KeyCapital, Keyboard1KeyF1, Keyboard1KeyF2, Keyboard1KeyF3, Keyboard1KeyF4, Keyboard1KeyF5, Keyboard1KeyF6, Keyboard1KeyF7, Keyboard1KeyF8, Keyboard1KeyF9, Keyboard1KeyF10, Keyboard1KeyNumberLock, Keyboard1KeyScrollLock, Keyboard1KeyNumberPad7, Keyboard1KeyNumberPad8, Keyboard1KeyNumberPad9, Keyboard1KeySubtract, Keyboard1KeyNumberPad4, Keyboard1KeyNumberPad5, Keyboard1KeyNumberPad6, Keyboard1KeyAdd, Keyboard1KeyNumberPad1, Keyboard1KeyNumberPad2, Keyboard1KeyNumberPad3, Keyboard1KeyNumberPad0, Keyboard1KeyDecimal, Keyboard1KeyOem102, Keyboard1KeyF11, Keyboard1KeyF12, Keyboard1KeyF13, Keyboard1KeyF14, Keyboard1KeyF15, Keyboard1KeyKana, Keyboard1KeyAbntC1, Keyboard1KeyConvert, Keyboard1KeyNoConvert, Keyboard1KeyYen, Keyboard1KeyAbntC2, Keyboard1KeyNumberPadEquals, Keyboard1KeyPreviousTrack, Keyboard1KeyAT, Keyboard1KeyColon, Keyboard1KeyUnderline, Keyboard1KeyKanji, Keyboard1KeyStop, Keyboard1KeyAX, Keyboard1KeyUnlabeled, Keyboard1KeyNextTrack, Keyboard1KeyNumberPadEnter, Keyboard1KeyRightControl, Keyboard1KeyMute, Keyboard1KeyCalculator, Keyboard1KeyPlayPause, Keyboard1KeyMediaStop, Keyboard1KeyVolumeDown, Keyboard1KeyVolumeUp, Keyboard1KeyWebHome, Keyboard1KeyNumberPadComma, Keyboard1KeyDivide, Keyboard1KeyPrintScreen, Keyboard1KeyRightAlt, Keyboard1KeyPause, Keyboard1KeyHome, Keyboard1KeyUp, Keyboard1KeyPageUp, Keyboard1KeyLeft, Keyboard1KeyRight, Keyboard1KeyEnd, Keyboard1KeyDown, Keyboard1KeyPageDown, Keyboard1KeyInsert, Keyboard1KeyDelete, Keyboard1KeyLeftWindowsKey, Keyboard1KeyRightWindowsKey, Keyboard1KeyApplications, Keyboard1KeyPower, Keyboard1KeySleep, Keyboard1KeyWake, Keyboard1KeyWebSearch, Keyboard1KeyWebFavorites, Keyboard1KeyWebRefresh, Keyboard1KeyWebStop, Keyboard1KeyWebForward, Keyboard1KeyWebBack, Keyboard1KeyMyComputer, Keyboard1KeyMail, Keyboard1KeyMediaSelect, Keyboard1KeyUnknown, Keyboard2KeyEscape, Keyboard2KeyD1, Keyboard2KeyD2, Keyboard2KeyD3, Keyboard2KeyD4, Keyboard2KeyD5, Keyboard2KeyD6, Keyboard2KeyD7, Keyboard2KeyD8, Keyboard2KeyD9, Keyboard2KeyD0, Keyboard2KeyMinus, Keyboard2KeyEquals, Keyboard2KeyBack, Keyboard2KeyTab, Keyboard2KeyQ, Keyboard2KeyW, Keyboard2KeyE, Keyboard2KeyR, Keyboard2KeyT, Keyboard2KeyY, Keyboard2KeyU, Keyboard2KeyI, Keyboard2KeyO, Keyboard2KeyP, Keyboard2KeyLeftBracket, Keyboard2KeyRightBracket, Keyboard2KeyReturn, Keyboard2KeyLeftControl, Keyboard2KeyA, Keyboard2KeyS, Keyboard2KeyD, Keyboard2KeyF, Keyboard2KeyG, Keyboard2KeyH, Keyboard2KeyJ, Keyboard2KeyK, Keyboard2KeyL, Keyboard2KeySemicolon, Keyboard2KeyApostrophe, Keyboard2KeyGrave, Keyboard2KeyLeftShift, Keyboard2KeyBackslash, Keyboard2KeyZ, Keyboard2KeyX, Keyboard2KeyC, Keyboard2KeyV, Keyboard2KeyB, Keyboard2KeyN, Keyboard2KeyM, Keyboard2KeyComma, Keyboard2KeyPeriod, Keyboard2KeySlash, Keyboard2KeyRightShift, Keyboard2KeyMultiply, Keyboard2KeyLeftAlt, Keyboard2KeySpace, Keyboard2KeyCapital, Keyboard2KeyF1, Keyboard2KeyF2, Keyboard2KeyF3, Keyboard2KeyF4, Keyboard2KeyF5, Keyboard2KeyF6, Keyboard2KeyF7, Keyboard2KeyF8, Keyboard2KeyF9, Keyboard2KeyF10, Keyboard2KeyNumberLock, Keyboard2KeyScrollLock, Keyboard2KeyNumberPad7, Keyboard2KeyNumberPad8, Keyboard2KeyNumberPad9, Keyboard2KeySubtract, Keyboard2KeyNumberPad4, Keyboard2KeyNumberPad5, Keyboard2KeyNumberPad6, Keyboard2KeyAdd, Keyboard2KeyNumberPad1, Keyboard2KeyNumberPad2, Keyboard2KeyNumberPad3, Keyboard2KeyNumberPad0, Keyboard2KeyDecimal, Keyboard2KeyOem102, Keyboard2KeyF11, Keyboard2KeyF12, Keyboard2KeyF13, Keyboard2KeyF14, Keyboard2KeyF15, Keyboard2KeyKana, Keyboard2KeyAbntC1, Keyboard2KeyConvert, Keyboard2KeyNoConvert, Keyboard2KeyYen, Keyboard2KeyAbntC2, Keyboard2KeyNumberPadEquals, Keyboard2KeyPreviousTrack, Keyboard2KeyAT, Keyboard2KeyColon, Keyboard2KeyUnderline, Keyboard2KeyKanji, Keyboard2KeyStop, Keyboard2KeyAX, Keyboard2KeyUnlabeled, Keyboard2KeyNextTrack, Keyboard2KeyNumberPadEnter, Keyboard2KeyRightControl, Keyboard2KeyMute, Keyboard2KeyCalculator, Keyboard2KeyPlayPause, Keyboard2KeyMediaStop, Keyboard2KeyVolumeDown, Keyboard2KeyVolumeUp, Keyboard2KeyWebHome, Keyboard2KeyNumberPadComma, Keyboard2KeyDivide, Keyboard2KeyPrintScreen, Keyboard2KeyRightAlt, Keyboard2KeyPause, Keyboard2KeyHome, Keyboard2KeyUp, Keyboard2KeyPageUp, Keyboard2KeyLeft, Keyboard2KeyRight, Keyboard2KeyEnd, Keyboard2KeyDown, Keyboard2KeyPageDown, Keyboard2KeyInsert, Keyboard2KeyDelete, Keyboard2KeyLeftWindowsKey, Keyboard2KeyRightWindowsKey, Keyboard2KeyApplications, Keyboard2KeyPower, Keyboard2KeySleep, Keyboard2KeyWake, Keyboard2KeyWebSearch, Keyboard2KeyWebFavorites, Keyboard2KeyWebRefresh, Keyboard2KeyWebStop, Keyboard2KeyWebForward, Keyboard2KeyWebBack, Keyboard2KeyMyComputer, Keyboard2KeyMail, Keyboard2KeyMediaSelect, Keyboard2KeyUnknown, TextFromSpeech, PS5ControllerLeftStickX, PS5ControllerLeftStickY, PS5ControllerRightStickX, PS5ControllerRightStickY, PS5ControllerLeftTriggerPosition, PS5ControllerRightTriggerPosition, PS5ControllerTouchX, PS5ControllerTouchY, PS5ControllerTouchOn, PS5ControllerGyroX, PS5ControllerGyroY, PS5ControllerAccelX, PS5ControllerAccelY, PS5ControllerButtonCrossPressed, PS5ControllerButtonCirclePressed, PS5ControllerButtonSquarePressed, PS5ControllerButtonTrianglePressed, PS5ControllerButtonDPadUpPressed, PS5ControllerButtonDPadRightPressed, PS5ControllerButtonDPadDownPressed, PS5ControllerButtonDPadLeftPressed, PS5ControllerButtonL1Pressed, PS5ControllerButtonR1Pressed, PS5ControllerButtonL2Pressed, PS5ControllerButtonR2Pressed, PS5ControllerButtonL3Pressed, PS5ControllerButtonR3Pressed, PS5ControllerButtonCreatePressed, PS5ControllerButtonMenuPressed, PS5ControllerButtonLogoPressed, PS5ControllerButtonTouchpadPressed, PS5ControllerButtonMicPressed, Controller1GyroX, Controller1GyroY, Controller2GyroX, Controller2GyroY });
                UsersAllowedList = (string[])val[0];
                sleeptime = (int)val[1];
                SpeechToText = (string[])val[152];
                EnableKM = (bool)val[201]; EnableXC = (bool)val[202]; EnableRI = (bool)val[203]; EnableDI = (bool)val[204]; EnableXI = (bool)val[205]; EnableJI = (bool)val[206]; EnablePI = (bool)val[207];
                checkingusername = false;
                if (UsersAllowedList.Length == 0)
                {
                    checkingusername = true;
                }
                else
                {
                    foreach (string userallowed in UsersAllowedList)
                    {
                        if (username == userallowed)
                        {
                            checkingusername = true;
                            break;
                        }
                        Thread.Sleep(1);
                    }
                }
                if (checkingusername)
                {
                    if (SpeechToText.Length != 0)
                    {
                        microphone = Ozeki.Media.Microphone.GetDefaultDevice();
                        connector = new Ozeki.Media.MediaConnector();
                        SetupSpeechToText(SpeechToText);
                    }
                    if (wiimoteconnected)
                    {
                        do
                            Thread.Sleep(1);
                        while (!ScanWiimote());
                        Task.Run(() => WiimoteNunchuckData());
                    }
                    if (EnableJI)
                    {
                        if (joyconleftconnected)
                        {
                            do
                                Thread.Sleep(1);
                            while (!ScanLeft());
                        }
                        if (joyconrightconnected)
                        {
                            do
                                Thread.Sleep(1);
                            while (!ScanRight());
                        }
                        if (joyconleftconnected | joyconrightconnected)
                        {
                            Task.Run(() => JoyconsData());
                        }
                    }
                    if (EnablePI & procontrollerconnected)
                    {
                        do
                            Thread.Sleep(1);
                        while (!ScanPro());
                        Task.Run(() => ProControllerData());
                    }
                    if (wiimoteconnected | (EnableJI & (joyconleftconnected | joyconrightconnected)) | (EnablePI & procontrollerconnected))
                        Thread.Sleep(2000);
                    if (wiimoteconnected)
                    {
                        InitWiimoteNunchuck();
                    }
                    if (EnableJI)
                    {
                        if (joyconleftconnected)
                        {
                            InitLeftJoycon();
                        }
                        if (joyconrightconnected)
                        {
                            InitRightJoycon();
                        }
                    }
                    if (EnablePI & procontrollerconnected)
                    {
                        InitProController();
                    }
                    if (EnableXI)
                    {
                        controllerconnected = XInputHookConnect();
                    }
                    if (EnableDI)
                    {
                        gamepadconnected = DirectInputHookConnect();
                    }
                    if (EnableRI)
                    {
                        keyboardconnected = KeyboardInputHookConnect();
                    }
                    if (!EnableDI)
                    {
                        ds = ChooseController();
                        if (ds == null)
                        {
                            ps5controllerconneced = false;
                        }
                        else
                        {
                            ps5controllerconneced = true;
                            Task.Run(() => MainAsyncPolling());
                        }
                    }
                    mouseconnected = MouseInputHookConnect();
                    if (EnableXC)
                        Scp.LoadControllers();
                    while (scriptrunning)
                    {
                        if (wiimoteconnected)
                        {
                            ProcessButtonsWiimoteNunchuck();
                        }
                        if (EnableJI)
                        {
                            if (joyconleftconnected)
                            {
                                ProcessButtonsLeftJoycon();
                            }
                            if (joyconrightconnected)
                            {
                                ProcessButtonsRightJoycon();
                            }
                        }
                        if (EnablePI & procontrollerconnected)
                        {
                            ProcessButtonsAndSticksPro();
                        }
                        if (EnableXI & controllerconnected)
                        {
                            ControllerProcess();
                        }
                        if (EnableDI & gamepadconnected)
                        {
                            GamepadProcess();
                        }
                        if (EnableRI & keyboardconnected)
                        {
                            KeyboardInputProcess();
                        }
                        else
                        {
                            KeyboardHookProcessButtons();
                        }
                        if (!EnableDI & ps5controllerconneced)
                        {
                            wheelPos = ProcessStateLogic(ds.InputState, wheelPos);
                        }
                        if (mouseconnected)
                        {
                            MouseInputProcess();
                        }
                        val = (object[])program.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, new object[] { width, height, Key_LBUTTON, Key_RBUTTON, Key_CANCEL, Key_MBUTTON, Key_XBUTTON1, Key_XBUTTON2, Key_BACK, Key_Tab, Key_CLEAR, Key_Return, Key_SHIFT, Key_CONTROL, Key_MENU, Key_PAUSE, Key_CAPITAL, Key_KANA, Key_HANGEUL, Key_HANGUL, Key_JUNJA, Key_FINAL, Key_HANJA, Key_KANJI, Key_Escape, Key_CONVERT, Key_NONCONVERT, Key_ACCEPT, Key_MODECHANGE, Key_Space, Key_PRIOR, Key_NEXT, Key_END, Key_HOME, Key_LEFT, Key_UP, Key_RIGHT, Key_DOWN, Key_SELECT, Key_PRINT, Key_EXECUTE, Key_SNAPSHOT, Key_INSERT, Key_DELETE, Key_HELP, Key_APOSTROPHE, Key_0, Key_1, Key_2, Key_3, Key_4, Key_5, Key_6, Key_7, Key_8, Key_9, Key_A, Key_B, Key_C, Key_D, Key_E, Key_F, Key_G, Key_H, Key_I, Key_J, Key_K, Key_L, Key_M, Key_N, Key_O, Key_P, Key_Q, Key_R, Key_S, Key_T, Key_U, Key_V, Key_W, Key_X, Key_Y, Key_Z, Key_LWIN, Key_RWIN, Key_APPS, Key_SLEEP, Key_NUMPAD0, Key_NUMPAD1, Key_NUMPAD2, Key_NUMPAD3, Key_NUMPAD4, Key_NUMPAD5, Key_NUMPAD6, Key_NUMPAD7, Key_NUMPAD8, Key_NUMPAD9, Key_MULTIPLY, Key_ADD, Key_SEPARATOR, Key_SUBTRACT, Key_DECIMAL, Key_DIVIDE, Key_F1, Key_F2, Key_F3, Key_F4, Key_F5, Key_F6, Key_F7, Key_F8, Key_F9, Key_F10, Key_F11, Key_F12, Key_F13, Key_F14, Key_F15, Key_F16, Key_F17, Key_F18, Key_F19, Key_F20, Key_F21, Key_F22, Key_F23, Key_F24, Key_NUMLOCK, Key_SCROLL, Key_LeftShift, Key_RightShift, Key_LeftControl, Key_RightControl, Key_LMENU, Key_RMENU, Key_BROWSER_BACK, Key_BROWSER_FORWARD, Key_BROWSER_REFRESH, Key_BROWSER_STOP, Key_BROWSER_SEARCH, Key_BROWSER_FAVORITES, Key_BROWSER_HOME, Key_VOLUME_MUTE, Key_VOLUME_DOWN, Key_VOLUME_UP, Key_MEDIA_NEXT_TRACK, Key_MEDIA_PREV_TRACK, Key_MEDIA_STOP, Key_MEDIA_PLAY_PAUSE, Key_LAUNCH_MAIL, Key_LAUNCH_MEDIA_SELECT, Key_LAUNCH_APP1, Key_LAUNCH_APP2, Key_OEM_1, Key_OEM_PLUS, Key_OEM_COMMA, Key_OEM_MINUS, Key_OEM_PERIOD, Key_OEM_2, Key_OEM_3, Key_OEM_4, Key_OEM_5, Key_OEM_6, Key_OEM_7, Key_OEM_8, Key_OEM_102, Key_PROCESSKEY, Key_PACKET, Key_ATTN, Key_CRSEL, Key_EXSEL, Key_EREOF, Key_PLAY, Key_ZOOM, Key_NONAME, Key_PA1, Key_OEM_CLEAR, irx, iry, WiimoteButtonStateA, WiimoteButtonStateB, WiimoteButtonStateMinus, WiimoteButtonStateHome, WiimoteButtonStatePlus, WiimoteButtonStateOne, WiimoteButtonStateTwo, WiimoteButtonStateUp, WiimoteButtonStateDown, WiimoteButtonStateLeft, WiimoteButtonStateRight, WiimoteRawValuesX, WiimoteRawValuesY, WiimoteRawValuesZ, WiimoteNunchuckStateRawJoystickX, WiimoteNunchuckStateRawJoystickY, WiimoteNunchuckStateRawValuesX, WiimoteNunchuckStateRawValuesY, WiimoteNunchuckStateRawValuesZ, WiimoteNunchuckStateC, WiimoteNunchuckStateZ, JoyconRightStickX, JoyconRightStickY, JoyconRightButtonSHOULDER_1, JoyconRightButtonSHOULDER_2, JoyconRightButtonSR, JoyconRightButtonSL, JoyconRightButtonDPAD_DOWN, JoyconRightButtonDPAD_RIGHT, JoyconRightButtonDPAD_UP, JoyconRightButtonDPAD_LEFT, JoyconRightButtonPLUS, JoyconRightButtonHOME, JoyconRightButtonSTICK, JoyconRightButtonACC, JoyconRightButtonSPA, JoyconRightRollLeft, JoyconRightRollRight, JoyconRightAccelX, JoyconRightAccelY, JoyconRightGyroX, JoyconRightGyroY, JoyconLeftStickX, JoyconLeftStickY, JoyconLeftButtonSHOULDER_1, JoyconLeftButtonSHOULDER_2, JoyconLeftButtonSR, JoyconLeftButtonSL, JoyconLeftButtonDPAD_DOWN, JoyconLeftButtonDPAD_RIGHT, JoyconLeftButtonDPAD_UP, JoyconLeftButtonDPAD_LEFT, JoyconLeftButtonMINUS, JoyconLeftButtonCAPTURE, JoyconLeftButtonSTICK, JoyconLeftButtonACC, JoyconLeftButtonSMA, JoyconLeftRollLeft, JoyconLeftRollRight, JoyconLeftAccelX, JoyconLeftAccelY, JoyconLeftGyroX, JoyconLeftGyroY, ProControllerLeftStickX, ProControllerLeftStickY, ProControllerRightStickX, ProControllerRightStickY, ProControllerButtonSHOULDER_Left_1, ProControllerButtonSHOULDER_Left_2, ProControllerButtonDPAD_DOWN, ProControllerButtonDPAD_RIGHT, ProControllerButtonDPAD_UP, ProControllerButtonDPAD_LEFT, ProControllerButtonMINUS, ProControllerButtonCAPTURE, ProControllerButtonSTICK_Left, ProControllerButtonSHOULDER_Right_1, ProControllerButtonSHOULDER_Right_2, ProControllerButtonA, ProControllerButtonB, ProControllerButtonX, ProControllerButtonY, ProControllerButtonPLUS, ProControllerButtonHOME, ProControllerButtonSTICK_Right, ProControllerAccelX, ProControllerAccelY, ProControllerGyroX, ProControllerGyroY, camx, camy, Controller1ButtonAPressed, Controller2ButtonAPressed, Controller1ButtonBPressed, Controller2ButtonBPressed, Controller1ButtonXPressed, Controller2ButtonXPressed, Controller1ButtonYPressed, Controller2ButtonYPressed, Controller1ButtonStartPressed, Controller2ButtonStartPressed, Controller1ButtonBackPressed, Controller2ButtonBackPressed, Controller1ButtonDownPressed, Controller2ButtonDownPressed, Controller1ButtonUpPressed, Controller2ButtonUpPressed, Controller1ButtonLeftPressed, Controller2ButtonLeftPressed, Controller1ButtonRightPressed, Controller2ButtonRightPressed, Controller1ButtonShoulderLeftPressed, Controller2ButtonShoulderLeftPressed, Controller1ButtonShoulderRightPressed, Controller2ButtonShoulderRightPressed, Controller1ThumbpadLeftPressed, Controller2ThumbpadLeftPressed, Controller1ThumbpadRightPressed, Controller2ThumbpadRightPressed, Controller1TriggerLeftPosition, Controller2TriggerLeftPosition, Controller1TriggerRightPosition, Controller2TriggerRightPosition, Controller1ThumbLeftX, Controller2ThumbLeftX, Controller1ThumbLeftY, Controller2ThumbLeftY, Controller1ThumbRightX, Controller2ThumbRightX, Controller1ThumbRightY, Controller2ThumbRightY, Joystick1AxisX, Joystick1AxisY, Joystick1AxisZ, Joystick1RotationX, Joystick1RotationY, Joystick1RotationZ, Joystick1Sliders0, Joystick1Sliders1, Joystick1PointOfViewControllers0, Joystick1PointOfViewControllers1, Joystick1PointOfViewControllers2, Joystick1PointOfViewControllers3, Joystick1VelocityX, Joystick1VelocityY, Joystick1VelocityZ, Joystick1AngularVelocityX, Joystick1AngularVelocityY, Joystick1AngularVelocityZ, Joystick1VelocitySliders0, Joystick1VelocitySliders1, Joystick1AccelerationX, Joystick1AccelerationY, Joystick1AccelerationZ, Joystick1AngularAccelerationX, Joystick1AngularAccelerationY, Joystick1AngularAccelerationZ, Joystick1AccelerationSliders0, Joystick1AccelerationSliders1, Joystick1ForceX, Joystick1ForceY, Joystick1ForceZ, Joystick1TorqueX, Joystick1TorqueY, Joystick1TorqueZ, Joystick1ForceSliders0, Joystick1ForceSliders1, Joystick1Buttons0, Joystick1Buttons1, Joystick1Buttons2, Joystick1Buttons3, Joystick1Buttons4, Joystick1Buttons5, Joystick1Buttons6, Joystick1Buttons7, Joystick1Buttons8, Joystick1Buttons9, Joystick1Buttons10, Joystick1Buttons11, Joystick1Buttons12, Joystick1Buttons13, Joystick1Buttons14, Joystick1Buttons15, Joystick1Buttons16, Joystick1Buttons17, Joystick1Buttons18, Joystick1Buttons19, Joystick1Buttons20, Joystick1Buttons21, Joystick1Buttons22, Joystick1Buttons23, Joystick1Buttons24, Joystick1Buttons25, Joystick1Buttons26, Joystick1Buttons27, Joystick1Buttons28, Joystick1Buttons29, Joystick1Buttons30, Joystick1Buttons31, Joystick1Buttons32, Joystick1Buttons33, Joystick1Buttons34, Joystick1Buttons35, Joystick1Buttons36, Joystick1Buttons37, Joystick1Buttons38, Joystick1Buttons39, Joystick1Buttons40, Joystick1Buttons41, Joystick1Buttons42, Joystick1Buttons43, Joystick1Buttons44, Joystick1Buttons45, Joystick1Buttons46, Joystick1Buttons47, Joystick1Buttons48, Joystick1Buttons49, Joystick1Buttons50, Joystick1Buttons51, Joystick1Buttons52, Joystick1Buttons53, Joystick1Buttons54, Joystick1Buttons55, Joystick1Buttons56, Joystick1Buttons57, Joystick1Buttons58, Joystick1Buttons59, Joystick1Buttons60, Joystick1Buttons61, Joystick1Buttons62, Joystick1Buttons63, Joystick1Buttons64, Joystick1Buttons65, Joystick1Buttons66, Joystick1Buttons67, Joystick1Buttons68, Joystick1Buttons69, Joystick1Buttons70, Joystick1Buttons71, Joystick1Buttons72, Joystick1Buttons73, Joystick1Buttons74, Joystick1Buttons75, Joystick1Buttons76, Joystick1Buttons77, Joystick1Buttons78, Joystick1Buttons79, Joystick1Buttons80, Joystick1Buttons81, Joystick1Buttons82, Joystick1Buttons83, Joystick1Buttons84, Joystick1Buttons85, Joystick1Buttons86, Joystick1Buttons87, Joystick1Buttons88, Joystick1Buttons89, Joystick1Buttons90, Joystick1Buttons91, Joystick1Buttons92, Joystick1Buttons93, Joystick1Buttons94, Joystick1Buttons95, Joystick1Buttons96, Joystick1Buttons97, Joystick1Buttons98, Joystick1Buttons99, Joystick1Buttons100, Joystick1Buttons101, Joystick1Buttons102, Joystick1Buttons103, Joystick1Buttons104, Joystick1Buttons105, Joystick1Buttons106, Joystick1Buttons107, Joystick1Buttons108, Joystick1Buttons109, Joystick1Buttons110, Joystick1Buttons111, Joystick1Buttons112, Joystick1Buttons113, Joystick1Buttons114, Joystick1Buttons115, Joystick1Buttons116, Joystick1Buttons117, Joystick1Buttons118, Joystick1Buttons119, Joystick1Buttons120, Joystick1Buttons121, Joystick1Buttons122, Joystick1Buttons123, Joystick1Buttons124, Joystick1Buttons125, Joystick1Buttons126, Joystick1Buttons127, Joystick2AxisX, Joystick2AxisY, Joystick2AxisZ, Joystick2RotationX, Joystick2RotationY, Joystick2RotationZ, Joystick2Sliders0, Joystick2Sliders1, Joystick2PointOfViewControllers0, Joystick2PointOfViewControllers1, Joystick2PointOfViewControllers2, Joystick2PointOfViewControllers3, Joystick2VelocityX, Joystick2VelocityY, Joystick2VelocityZ, Joystick2AngularVelocityX, Joystick2AngularVelocityY, Joystick2AngularVelocityZ, Joystick2VelocitySliders0, Joystick2VelocitySliders1, Joystick2AccelerationX, Joystick2AccelerationY, Joystick2AccelerationZ, Joystick2AngularAccelerationX, Joystick2AngularAccelerationY, Joystick2AngularAccelerationZ, Joystick2AccelerationSliders0, Joystick2AccelerationSliders1, Joystick2ForceX, Joystick2ForceY, Joystick2ForceZ, Joystick2TorqueX, Joystick2TorqueY, Joystick2TorqueZ, Joystick2ForceSliders0, Joystick2ForceSliders1, Joystick2Buttons0, Joystick2Buttons1, Joystick2Buttons2, Joystick2Buttons3, Joystick2Buttons4, Joystick2Buttons5, Joystick2Buttons6, Joystick2Buttons7, Joystick2Buttons8, Joystick2Buttons9, Joystick2Buttons10, Joystick2Buttons11, Joystick2Buttons12, Joystick2Buttons13, Joystick2Buttons14, Joystick2Buttons15, Joystick2Buttons16, Joystick2Buttons17, Joystick2Buttons18, Joystick2Buttons19, Joystick2Buttons20, Joystick2Buttons21, Joystick2Buttons22, Joystick2Buttons23, Joystick2Buttons24, Joystick2Buttons25, Joystick2Buttons26, Joystick2Buttons27, Joystick2Buttons28, Joystick2Buttons29, Joystick2Buttons30, Joystick2Buttons31, Joystick2Buttons32, Joystick2Buttons33, Joystick2Buttons34, Joystick2Buttons35, Joystick2Buttons36, Joystick2Buttons37, Joystick2Buttons38, Joystick2Buttons39, Joystick2Buttons40, Joystick2Buttons41, Joystick2Buttons42, Joystick2Buttons43, Joystick2Buttons44, Joystick2Buttons45, Joystick2Buttons46, Joystick2Buttons47, Joystick2Buttons48, Joystick2Buttons49, Joystick2Buttons50, Joystick2Buttons51, Joystick2Buttons52, Joystick2Buttons53, Joystick2Buttons54, Joystick2Buttons55, Joystick2Buttons56, Joystick2Buttons57, Joystick2Buttons58, Joystick2Buttons59, Joystick2Buttons60, Joystick2Buttons61, Joystick2Buttons62, Joystick2Buttons63, Joystick2Buttons64, Joystick2Buttons65, Joystick2Buttons66, Joystick2Buttons67, Joystick2Buttons68, Joystick2Buttons69, Joystick2Buttons70, Joystick2Buttons71, Joystick2Buttons72, Joystick2Buttons73, Joystick2Buttons74, Joystick2Buttons75, Joystick2Buttons76, Joystick2Buttons77, Joystick2Buttons78, Joystick2Buttons79, Joystick2Buttons80, Joystick2Buttons81, Joystick2Buttons82, Joystick2Buttons83, Joystick2Buttons84, Joystick2Buttons85, Joystick2Buttons86, Joystick2Buttons87, Joystick2Buttons88, Joystick2Buttons89, Joystick2Buttons90, Joystick2Buttons91, Joystick2Buttons92, Joystick2Buttons93, Joystick2Buttons94, Joystick2Buttons95, Joystick2Buttons96, Joystick2Buttons97, Joystick2Buttons98, Joystick2Buttons99, Joystick2Buttons100, Joystick2Buttons101, Joystick2Buttons102, Joystick2Buttons103, Joystick2Buttons104, Joystick2Buttons105, Joystick2Buttons106, Joystick2Buttons107, Joystick2Buttons108, Joystick2Buttons109, Joystick2Buttons110, Joystick2Buttons111, Joystick2Buttons112, Joystick2Buttons113, Joystick2Buttons114, Joystick2Buttons115, Joystick2Buttons116, Joystick2Buttons117, Joystick2Buttons118, Joystick2Buttons119, Joystick2Buttons120, Joystick2Buttons121, Joystick2Buttons122, Joystick2Buttons123, Joystick2Buttons124, Joystick2Buttons125, Joystick2Buttons126, Joystick2Buttons127, Mouse1Buttons0, Mouse1Buttons1, Mouse1Buttons2, Mouse1Buttons3, Mouse1Buttons4, Mouse1Buttons5, Mouse1Buttons6, Mouse1Buttons7, MouseHookX, MouseHookY, Mouse1AxisX, Mouse1AxisY, Mouse1AxisZ, Mouse2Buttons0, Mouse2Buttons1, Mouse2Buttons2, Mouse2Buttons3, Mouse2Buttons4, Mouse2Buttons5, Mouse2Buttons6, Mouse2Buttons7, Mouse2AxisX, Mouse2AxisY, Mouse2AxisZ, Keyboard1KeyEscape, Keyboard1KeyD1, Keyboard1KeyD2, Keyboard1KeyD3, Keyboard1KeyD4, Keyboard1KeyD5, Keyboard1KeyD6, Keyboard1KeyD7, Keyboard1KeyD8, Keyboard1KeyD9, Keyboard1KeyD0, Keyboard1KeyMinus, Keyboard1KeyEquals, Keyboard1KeyBack, Keyboard1KeyTab, Keyboard1KeyQ, Keyboard1KeyW, Keyboard1KeyE, Keyboard1KeyR, Keyboard1KeyT, Keyboard1KeyY, Keyboard1KeyU, Keyboard1KeyI, Keyboard1KeyO, Keyboard1KeyP, Keyboard1KeyLeftBracket, Keyboard1KeyRightBracket, Keyboard1KeyReturn, Keyboard1KeyLeftControl, Keyboard1KeyA, Keyboard1KeyS, Keyboard1KeyD, Keyboard1KeyF, Keyboard1KeyG, Keyboard1KeyH, Keyboard1KeyJ, Keyboard1KeyK, Keyboard1KeyL, Keyboard1KeySemicolon, Keyboard1KeyApostrophe, Keyboard1KeyGrave, Keyboard1KeyLeftShift, Keyboard1KeyBackslash, Keyboard1KeyZ, Keyboard1KeyX, Keyboard1KeyC, Keyboard1KeyV, Keyboard1KeyB, Keyboard1KeyN, Keyboard1KeyM, Keyboard1KeyComma, Keyboard1KeyPeriod, Keyboard1KeySlash, Keyboard1KeyRightShift, Keyboard1KeyMultiply, Keyboard1KeyLeftAlt, Keyboard1KeySpace, Keyboard1KeyCapital, Keyboard1KeyF1, Keyboard1KeyF2, Keyboard1KeyF3, Keyboard1KeyF4, Keyboard1KeyF5, Keyboard1KeyF6, Keyboard1KeyF7, Keyboard1KeyF8, Keyboard1KeyF9, Keyboard1KeyF10, Keyboard1KeyNumberLock, Keyboard1KeyScrollLock, Keyboard1KeyNumberPad7, Keyboard1KeyNumberPad8, Keyboard1KeyNumberPad9, Keyboard1KeySubtract, Keyboard1KeyNumberPad4, Keyboard1KeyNumberPad5, Keyboard1KeyNumberPad6, Keyboard1KeyAdd, Keyboard1KeyNumberPad1, Keyboard1KeyNumberPad2, Keyboard1KeyNumberPad3, Keyboard1KeyNumberPad0, Keyboard1KeyDecimal, Keyboard1KeyOem102, Keyboard1KeyF11, Keyboard1KeyF12, Keyboard1KeyF13, Keyboard1KeyF14, Keyboard1KeyF15, Keyboard1KeyKana, Keyboard1KeyAbntC1, Keyboard1KeyConvert, Keyboard1KeyNoConvert, Keyboard1KeyYen, Keyboard1KeyAbntC2, Keyboard1KeyNumberPadEquals, Keyboard1KeyPreviousTrack, Keyboard1KeyAT, Keyboard1KeyColon, Keyboard1KeyUnderline, Keyboard1KeyKanji, Keyboard1KeyStop, Keyboard1KeyAX, Keyboard1KeyUnlabeled, Keyboard1KeyNextTrack, Keyboard1KeyNumberPadEnter, Keyboard1KeyRightControl, Keyboard1KeyMute, Keyboard1KeyCalculator, Keyboard1KeyPlayPause, Keyboard1KeyMediaStop, Keyboard1KeyVolumeDown, Keyboard1KeyVolumeUp, Keyboard1KeyWebHome, Keyboard1KeyNumberPadComma, Keyboard1KeyDivide, Keyboard1KeyPrintScreen, Keyboard1KeyRightAlt, Keyboard1KeyPause, Keyboard1KeyHome, Keyboard1KeyUp, Keyboard1KeyPageUp, Keyboard1KeyLeft, Keyboard1KeyRight, Keyboard1KeyEnd, Keyboard1KeyDown, Keyboard1KeyPageDown, Keyboard1KeyInsert, Keyboard1KeyDelete, Keyboard1KeyLeftWindowsKey, Keyboard1KeyRightWindowsKey, Keyboard1KeyApplications, Keyboard1KeyPower, Keyboard1KeySleep, Keyboard1KeyWake, Keyboard1KeyWebSearch, Keyboard1KeyWebFavorites, Keyboard1KeyWebRefresh, Keyboard1KeyWebStop, Keyboard1KeyWebForward, Keyboard1KeyWebBack, Keyboard1KeyMyComputer, Keyboard1KeyMail, Keyboard1KeyMediaSelect, Keyboard1KeyUnknown, Keyboard2KeyEscape, Keyboard2KeyD1, Keyboard2KeyD2, Keyboard2KeyD3, Keyboard2KeyD4, Keyboard2KeyD5, Keyboard2KeyD6, Keyboard2KeyD7, Keyboard2KeyD8, Keyboard2KeyD9, Keyboard2KeyD0, Keyboard2KeyMinus, Keyboard2KeyEquals, Keyboard2KeyBack, Keyboard2KeyTab, Keyboard2KeyQ, Keyboard2KeyW, Keyboard2KeyE, Keyboard2KeyR, Keyboard2KeyT, Keyboard2KeyY, Keyboard2KeyU, Keyboard2KeyI, Keyboard2KeyO, Keyboard2KeyP, Keyboard2KeyLeftBracket, Keyboard2KeyRightBracket, Keyboard2KeyReturn, Keyboard2KeyLeftControl, Keyboard2KeyA, Keyboard2KeyS, Keyboard2KeyD, Keyboard2KeyF, Keyboard2KeyG, Keyboard2KeyH, Keyboard2KeyJ, Keyboard2KeyK, Keyboard2KeyL, Keyboard2KeySemicolon, Keyboard2KeyApostrophe, Keyboard2KeyGrave, Keyboard2KeyLeftShift, Keyboard2KeyBackslash, Keyboard2KeyZ, Keyboard2KeyX, Keyboard2KeyC, Keyboard2KeyV, Keyboard2KeyB, Keyboard2KeyN, Keyboard2KeyM, Keyboard2KeyComma, Keyboard2KeyPeriod, Keyboard2KeySlash, Keyboard2KeyRightShift, Keyboard2KeyMultiply, Keyboard2KeyLeftAlt, Keyboard2KeySpace, Keyboard2KeyCapital, Keyboard2KeyF1, Keyboard2KeyF2, Keyboard2KeyF3, Keyboard2KeyF4, Keyboard2KeyF5, Keyboard2KeyF6, Keyboard2KeyF7, Keyboard2KeyF8, Keyboard2KeyF9, Keyboard2KeyF10, Keyboard2KeyNumberLock, Keyboard2KeyScrollLock, Keyboard2KeyNumberPad7, Keyboard2KeyNumberPad8, Keyboard2KeyNumberPad9, Keyboard2KeySubtract, Keyboard2KeyNumberPad4, Keyboard2KeyNumberPad5, Keyboard2KeyNumberPad6, Keyboard2KeyAdd, Keyboard2KeyNumberPad1, Keyboard2KeyNumberPad2, Keyboard2KeyNumberPad3, Keyboard2KeyNumberPad0, Keyboard2KeyDecimal, Keyboard2KeyOem102, Keyboard2KeyF11, Keyboard2KeyF12, Keyboard2KeyF13, Keyboard2KeyF14, Keyboard2KeyF15, Keyboard2KeyKana, Keyboard2KeyAbntC1, Keyboard2KeyConvert, Keyboard2KeyNoConvert, Keyboard2KeyYen, Keyboard2KeyAbntC2, Keyboard2KeyNumberPadEquals, Keyboard2KeyPreviousTrack, Keyboard2KeyAT, Keyboard2KeyColon, Keyboard2KeyUnderline, Keyboard2KeyKanji, Keyboard2KeyStop, Keyboard2KeyAX, Keyboard2KeyUnlabeled, Keyboard2KeyNextTrack, Keyboard2KeyNumberPadEnter, Keyboard2KeyRightControl, Keyboard2KeyMute, Keyboard2KeyCalculator, Keyboard2KeyPlayPause, Keyboard2KeyMediaStop, Keyboard2KeyVolumeDown, Keyboard2KeyVolumeUp, Keyboard2KeyWebHome, Keyboard2KeyNumberPadComma, Keyboard2KeyDivide, Keyboard2KeyPrintScreen, Keyboard2KeyRightAlt, Keyboard2KeyPause, Keyboard2KeyHome, Keyboard2KeyUp, Keyboard2KeyPageUp, Keyboard2KeyLeft, Keyboard2KeyRight, Keyboard2KeyEnd, Keyboard2KeyDown, Keyboard2KeyPageDown, Keyboard2KeyInsert, Keyboard2KeyDelete, Keyboard2KeyLeftWindowsKey, Keyboard2KeyRightWindowsKey, Keyboard2KeyApplications, Keyboard2KeyPower, Keyboard2KeySleep, Keyboard2KeyWake, Keyboard2KeyWebSearch, Keyboard2KeyWebFavorites, Keyboard2KeyWebRefresh, Keyboard2KeyWebStop, Keyboard2KeyWebForward, Keyboard2KeyWebBack, Keyboard2KeyMyComputer, Keyboard2KeyMail, Keyboard2KeyMediaSelect, Keyboard2KeyUnknown, TextFromSpeech, PS5ControllerLeftStickX, PS5ControllerLeftStickY, PS5ControllerRightStickX, PS5ControllerRightStickY, PS5ControllerLeftTriggerPosition, PS5ControllerRightTriggerPosition, PS5ControllerTouchX, PS5ControllerTouchY, PS5ControllerTouchOn, PS5ControllerGyroX, PS5ControllerGyroY, PS5ControllerAccelX, PS5ControllerAccelY, PS5ControllerButtonCrossPressed, PS5ControllerButtonCirclePressed, PS5ControllerButtonSquarePressed, PS5ControllerButtonTrianglePressed, PS5ControllerButtonDPadUpPressed, PS5ControllerButtonDPadRightPressed, PS5ControllerButtonDPadDownPressed, PS5ControllerButtonDPadLeftPressed, PS5ControllerButtonL1Pressed, PS5ControllerButtonR1Pressed, PS5ControllerButtonL2Pressed, PS5ControllerButtonR2Pressed, PS5ControllerButtonL3Pressed, PS5ControllerButtonR3Pressed, PS5ControllerButtonCreatePressed, PS5ControllerButtonMenuPressed, PS5ControllerButtonLogoPressed, PS5ControllerButtonTouchpadPressed, PS5ControllerButtonMicPressed, Controller1GyroX, Controller1GyroY, Controller2GyroX, Controller2GyroY });
                        KeyboardMouseDriverType = (string)val[2]; MouseMoveX = (double)val[3]; MouseMoveY = (double)val[4]; MouseAbsX = (double)val[5]; MouseAbsY = (double)val[6]; MouseDesktopX = (double)val[7]; MouseDesktopY = (double)val[8]; SendLeftClick = (bool)val[9]; SendRightClick = (bool)val[10]; SendMiddleClick = (bool)val[11]; SendWheelUp = (bool)val[12]; SendWheelDown = (bool)val[13]; SendLeft = (bool)val[14]; SendRight = (bool)val[15]; SendUp = (bool)val[16]; SendDown = (bool)val[17]; SendLButton = (bool)val[18]; SendRButton = (bool)val[19]; SendCancel = (bool)val[20]; SendMBUTTON = (bool)val[21]; SendXBUTTON1 = (bool)val[22]; SendXBUTTON2 = (bool)val[23]; SendBack = (bool)val[24]; SendTab = (bool)val[25]; SendClear = (bool)val[26]; SendReturn = (bool)val[27]; SendSHIFT = (bool)val[28]; SendCONTROL = (bool)val[29]; SendMENU = (bool)val[30]; SendPAUSE = (bool)val[31]; SendCAPITAL = (bool)val[32]; SendKANA = (bool)val[33]; SendHANGEUL = (bool)val[34]; SendHANGUL = (bool)val[35]; SendJUNJA = (bool)val[36]; SendFINAL = (bool)val[37]; SendHANJA = (bool)val[38]; SendKANJI = (bool)val[39]; SendEscape = (bool)val[40]; SendCONVERT = (bool)val[41]; SendNONCONVERT = (bool)val[42]; SendACCEPT = (bool)val[43]; SendMODECHANGE = (bool)val[44]; SendSpace = (bool)val[45]; SendPRIOR = (bool)val[46]; SendNEXT = (bool)val[47]; SendEND = (bool)val[48]; SendHOME = (bool)val[49]; SendLEFT = (bool)val[50]; SendUP = (bool)val[51]; SendRIGHT = (bool)val[52]; SendDOWN = (bool)val[53]; SendSELECT = (bool)val[54]; SendPRINT = (bool)val[55]; SendEXECUTE = (bool)val[56]; SendSNAPSHOT = (bool)val[57]; SendINSERT = (bool)val[58]; SendDELETE = (bool)val[59]; SendHELP = (bool)val[60]; SendAPOSTROPHE = (bool)val[61]; Send0 = (bool)val[62]; Send1 = (bool)val[63]; Send2 = (bool)val[64]; Send3 = (bool)val[65]; Send4 = (bool)val[66]; Send5 = (bool)val[67]; Send6 = (bool)val[68]; Send7 = (bool)val[69]; Send8 = (bool)val[70]; Send9 = (bool)val[71]; SendA = (bool)val[72]; SendB = (bool)val[73]; SendC = (bool)val[74]; SendD = (bool)val[75]; SendE = (bool)val[76]; SendF = (bool)val[77]; SendG = (bool)val[78]; SendH = (bool)val[79]; SendI = (bool)val[80]; SendJ = (bool)val[81]; SendK = (bool)val[82]; SendL = (bool)val[83]; SendM = (bool)val[84]; SendN = (bool)val[85]; SendO = (bool)val[86]; SendP = (bool)val[87]; SendQ = (bool)val[88]; SendR = (bool)val[89]; SendS = (bool)val[90]; SendT = (bool)val[91]; SendU = (bool)val[92]; SendV = (bool)val[93]; SendW = (bool)val[94]; SendX = (bool)val[95]; SendY = (bool)val[96]; SendZ = (bool)val[97]; SendLWIN = (bool)val[98]; SendRWIN = (bool)val[99]; SendAPPS = (bool)val[100]; SendSLEEP = (bool)val[101]; SendNUMPAD0 = (bool)val[102]; SendNUMPAD1 = (bool)val[103]; SendNUMPAD2 = (bool)val[104]; SendNUMPAD3 = (bool)val[105]; SendNUMPAD4 = (bool)val[106]; SendNUMPAD5 = (bool)val[107]; SendNUMPAD6 = (bool)val[108]; SendNUMPAD7 = (bool)val[109]; SendNUMPAD8 = (bool)val[110]; SendNUMPAD9 = (bool)val[111]; SendMULTIPLY = (bool)val[112]; SendADD = (bool)val[113]; SendSEPARATOR = (bool)val[114]; SendSUBTRACT = (bool)val[115]; SendDECIMAL = (bool)val[116]; SendDIVIDE = (bool)val[117]; SendF1 = (bool)val[118]; SendF2 = (bool)val[119]; SendF3 = (bool)val[120]; SendF4 = (bool)val[121]; SendF5 = (bool)val[122]; SendF6 = (bool)val[123]; SendF7 = (bool)val[124]; SendF8 = (bool)val[125]; SendF9 = (bool)val[126]; SendF10 = (bool)val[127]; SendF11 = (bool)val[128]; SendF12 = (bool)val[129]; SendF13 = (bool)val[130]; SendF14 = (bool)val[131]; SendF15 = (bool)val[132]; SendF16 = (bool)val[133]; SendF17 = (bool)val[134]; SendF18 = (bool)val[135]; SendF19 = (bool)val[136]; SendF20 = (bool)val[137]; SendF21 = (bool)val[138]; SendF22 = (bool)val[139]; SendF23 = (bool)val[140]; SendF24 = (bool)val[141]; SendNUMLOCK = (bool)val[142]; SendSCROLL = (bool)val[143]; SendLeftShift = (bool)val[144]; SendRightShift = (bool)val[145]; SendLeftControl = (bool)val[146]; SendRightControl = (bool)val[147]; SendLMENU = (bool)val[148]; SendRMENU = (bool)val[149];
                        centery = (double)val[150]; irmode = (int)val[151];
                        controller1_send_back = (bool)val[153]; controller1_send_start = (bool)val[154]; controller1_send_A = (bool)val[155]; controller1_send_B = (bool)val[156]; controller1_send_X = (bool)val[157]; controller1_send_Y = (bool)val[158]; controller1_send_up = (bool)val[159]; controller1_send_left = (bool)val[160]; controller1_send_down = (bool)val[161]; controller1_send_right = (bool)val[162]; controller1_send_leftstick = (bool)val[163]; controller1_send_rightstick = (bool)val[164]; controller1_send_leftbumper = (bool)val[165]; controller1_send_rightbumper = (bool)val[166]; controller1_send_lefttrigger = (bool)val[167]; controller1_send_righttrigger = (bool)val[168]; controller1_send_leftstickx = (double)val[169]; controller1_send_leftsticky = (double)val[170]; controller1_send_rightstickx = (double)val[171]; controller1_send_rightsticky = (double)val[172]; controller2_send_back = (bool)val[173]; controller2_send_start = (bool)val[174]; controller2_send_A = (bool)val[175]; controller2_send_B = (bool)val[176]; controller2_send_X = (bool)val[177]; controller2_send_Y = (bool)val[178]; controller2_send_up = (bool)val[179]; controller2_send_left = (bool)val[180]; controller2_send_down = (bool)val[181]; controller2_send_right = (bool)val[182]; controller2_send_leftstick = (bool)val[183]; controller2_send_rightstick = (bool)val[184]; controller2_send_leftbumper = (bool)val[185]; controller2_send_rightbumper = (bool)val[186]; controller2_send_lefttrigger = (bool)val[187]; controller2_send_righttrigger = (bool)val[188]; controller2_send_leftstickx = (double)val[189]; controller2_send_leftsticky = (double)val[190]; controller2_send_rightstickx = (double)val[191]; controller2_send_rightsticky = (double)val[192];
                        pollcount = ((int[])val[193])[0]; keys12345 = (int)val[194]; keys54321 = (int)val[195]; getstate = ((bool[])val[196])[0];
                        mousexp = (double)val[197]; mouseyp = (double)val[198]; testdouble = (double)val[199]; testbool = (bool)val[200]; 
                        JoyconLeftGyroCenter = (bool)val[208]; JoyconRightGyroCenter = (bool)val[209]; ProControllerGyroCenter = (bool)val[210];
                        JoyconLeftAccelCenter = (bool)val[211]; JoyconRightAccelCenter = (bool)val[212]; ProControllerAccelCenter = (bool)val[213];
                        gyromode = (int)val[214];
                        controller1_send_lefttriggerposition = (double)val[215]; controller1_send_righttriggerposition = (double)val[216]; controller2_send_lefttriggerposition = (double)val[217]; controller2_send_righttriggerposition = (double)val[218];
                        PS5ControllerGyroCenter = (bool)val[219]; PS5ControllerAccelCenter = (bool)val[220];
                        JoyconLeftStickCenter = (bool)val[221]; JoyconRightStickCenter = (bool)val[222]; ProControllerStickCenter = (bool)val[223];
                        Controller1GyroCenter = (bool)val[224]; Controller2GyroCenter = (bool)val[225];
                        if (EnableKM)
                        {
                            SendKeyboard.SetKM(KeyboardMouseDriverType, SendLeftClick, SendRightClick, SendMiddleClick, SendWheelUp, SendWheelDown, SendLeft, SendRight, SendUp, SendDown, SendLButton, SendRButton, SendCancel, SendMBUTTON, SendXBUTTON1, SendXBUTTON2, SendBack, SendTab, SendClear, SendReturn, SendSHIFT, SendCONTROL, SendMENU, SendPAUSE, SendCAPITAL, SendKANA, SendHANGEUL, SendHANGUL, SendJUNJA, SendFINAL, SendHANJA, SendKANJI, SendEscape, SendCONVERT, SendNONCONVERT, SendACCEPT, SendMODECHANGE, SendSpace, SendPRIOR, SendNEXT, SendEND, SendHOME, SendLEFT, SendUP, SendRIGHT, SendDOWN, SendSELECT, SendPRINT, SendEXECUTE, SendSNAPSHOT, SendINSERT, SendDELETE, SendHELP, SendAPOSTROPHE, Send0, Send1, Send2, Send3, Send4, Send5, Send6, Send7, Send8, Send9, SendA, SendB, SendC, SendD, SendE, SendF, SendG, SendH, SendI, SendJ, SendK, SendL, SendM, SendN, SendO, SendP, SendQ, SendR, SendS, SendT, SendU, SendV, SendW, SendX, SendY, SendZ, SendLWIN, SendRWIN, SendAPPS, SendSLEEP, SendNUMPAD0, SendNUMPAD1, SendNUMPAD2, SendNUMPAD3, SendNUMPAD4, SendNUMPAD5, SendNUMPAD6, SendNUMPAD7, SendNUMPAD8, SendNUMPAD9, SendMULTIPLY, SendADD, SendSEPARATOR, SendSUBTRACT, SendDECIMAL, SendDIVIDE, SendF1, SendF2, SendF3, SendF4, SendF5, SendF6, SendF7, SendF8, SendF9, SendF10, SendF11, SendF12, SendF13, SendF14, SendF15, SendF16, SendF17, SendF18, SendF19, SendF20, SendF21, SendF22, SendF23, SendF24, SendNUMLOCK, SendSCROLL, SendLeftShift, SendRightShift, SendLeftControl, SendRightControl, SendLMENU, SendRMENU);
                            SendMouse.SetKM(KeyboardMouseDriverType, MouseMoveX, MouseMoveY, MouseAbsX, MouseAbsY, MouseDesktopX, MouseDesktopY);
                        }
                        if (EnableXC)
                        {
                            Scp.SetControllers(controller1_send_back, controller1_send_start, controller1_send_A, controller1_send_B, controller1_send_X, controller1_send_Y, controller1_send_up, controller1_send_left, controller1_send_down, controller1_send_right, controller1_send_leftstick, controller1_send_rightstick, controller1_send_leftbumper, controller1_send_rightbumper, controller1_send_lefttrigger, controller1_send_righttrigger, controller1_send_leftstickx, controller1_send_leftsticky, controller1_send_rightstickx, controller1_send_rightsticky, controller2_send_back, controller2_send_start, controller2_send_A, controller2_send_B, controller2_send_X, controller2_send_Y, controller2_send_up, controller2_send_left, controller2_send_down, controller2_send_right, controller2_send_leftstick, controller2_send_rightstick, controller2_send_leftbumper, controller2_send_rightbumper, controller2_send_lefttrigger, controller2_send_righttrigger, controller2_send_leftstickx, controller2_send_leftsticky, controller2_send_rightstickx, controller2_send_rightsticky, controller1_send_lefttriggerposition, controller1_send_righttriggerposition, controller2_send_lefttriggerposition, controller2_send_righttriggerposition);
                        }
                        if (form3visible)
                        {
                            SetExtraInputs();
                        }
                        if (form4visible)
                        {
                            SetClassicOutputs();
                        }
                        if (form5visible)
                        {
                            SetClassicInputs();
                        }
                        if (form7visible)
                        {
                            SetXInputs();
                        }
                        if (form8visible)
                        {
                            SetDirectInputs();
                        }
                        if (form9visible)
                        {
                            SetXOutputs();
                        }
                        if (form10visible)
                        {
                            SetExtraOutputs();
                        }
                        Mouse1AxisZ = 0;
                        Thread.Sleep(sleeptime);
                    }
                    if (SpeechToText.Length != 0)
                    {
                        StopSpeechToText();
                    }
                    if (EnableKM)
                        SendKeyboard.UnLoadKM(); 
                    if (EnableXC)
                        Scp.UnLoadControllers();
                    keyboardconnected = false;
                    mouseconnected = false;
                    gamepadconnected = false;
                    controllerconnected = false;
                    ps5controllerconneced = false;
                    CloseHandles();
                }
                else
                {
                    StopScript();
                    MessageBox.Show("You are not allowed to run this script.");
                }
            }
            catch { }
        }
        public void StartWebcamInputs()
        {
            CaptureDevice = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
            FinalFrame = new AForge.Video.DirectShow.VideoCaptureDevice(CaptureDevice[CaptureDevice.Count - 1].MonikerString);
            FinalFrame.NewFrame += FinalFrame_NewFrame;
            FinalFrame.Start();
        }
        void FinalFrame_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            brightnessfilter = new AForge.Imaging.Filters.BrightnessCorrection(brightness);
            img = (Bitmap)eventArgs.Frame.Clone();
            brightnessfilter.ApplyInPlace(img);
            colorfilter.Red = new IntRange(red, 255);
            colorfilter.Green = new IntRange(green, 255);
            colorfilter.Blue = new IntRange(blue, 255);
            colorfilter.ApplyInPlace(img);
            brightnessfilter.ApplyInPlace(img);
            euclideanfilter.CenterColor = new RGB(255, 255, 255);
            euclideanfilter.Radius = (short)radius;
            euclideanfilter.ApplyInPlace(img);
            blobCounter.ProcessImage(img);
            blobs = blobCounter.GetObjectsInformation();
            for (int i = 0; i < blobs.Length; i++)
            {
                shapeChecker.RelativeDistortionLimit = 100f;
                shapeChecker.MinAcceptableDistortion = 20f;
                if (shapeChecker.IsCircle(blobCounter.GetBlobsEdgePoints(blobs[i])))
                {
                    backpointX = blobs[0].CenterOfGravity.X;
                    backpointY = blobs[0].CenterOfGravity.Y;
                }
            }
            EditableImg = new Bitmap(img);
            EditableImg.MakeTransparent();
            DrawLines(ref EditableImg, new System.Drawing.Point((int)backpointX, (int)backpointY));
            if (form2visible)
            {
                try
                {
                    form2.SetWebcamInputs(EditableImg);
                }
                catch { }
            }
            posRightX = backpointX - img.Width / 2f;
            posRightY = backpointY - img.Height / 2f;
            camx = posRightX / (img.Width / 2f) * 1024f;
            camy = posRightY / (img.Height / 2f) * 1024f;
        }
        public void DrawLines(ref Bitmap image, System.Drawing.Point p)
        {
            Graphics g = Graphics.FromImage(image);
            Pen p1 = new Pen(System.Drawing.Color.Red, 2);
            System.Drawing.Point ph = new System.Drawing.Point(image.Width, p.Y);
            System.Drawing.Point ph2 = new System.Drawing.Point(0, p.Y);
            g.DrawLine(p1, p, ph);
            g.DrawLine(p1, p, ph2);
            ph = new System.Drawing.Point(p.X, 0);
            ph2 = new System.Drawing.Point(p.X, image.Height);
            g.DrawLine(p1, p, ph);
            g.DrawLine(p1, p, ph2);
            g.Dispose();
        }
        static void SetupSpeechToText(string[] speechwords)
        {
            speechToText = Ozeki.Media.SpeechToText.CreateInstance(speechwords);
            speechToText.WordRecognized += SpeechToText_WordsRecognized;
            connector.Connect(microphone, speechToText);
            microphone.Start();
        }
        private static void StopSpeechToText()
        {
            try
            {
                speechToText.WordRecognized -= SpeechToText_WordsRecognized;
            }
            catch { }
            try
            {
                connector.Disconnect(microphone, speechToText);
            }
            catch { }
            try
            {
                speechToText.Dispose();
            }
            catch { }
            try
            {
                microphone.Stop();
            }
            catch { }
        }
        private static void SpeechToText_WordsRecognized(object sender, Ozeki.Media.SpeechDetectionEventArgs e)
        {
            TextFromSpeech = e.Word;
        }
        private static Controller[] controller = new Controller[] { null, null };
        private static State xistate;
        private static int xinum = 0;
        public static bool Controller1ButtonAPressed, Controller2ButtonAPressed;
        public static bool Controller1ButtonBPressed, Controller2ButtonBPressed;
        public static bool Controller1ButtonXPressed, Controller2ButtonXPressed;
        public static bool Controller1ButtonYPressed, Controller2ButtonYPressed;
        public static bool Controller1ButtonStartPressed, Controller2ButtonStartPressed;
        public static bool Controller1ButtonBackPressed, Controller2ButtonBackPressed;
        public static bool Controller1ButtonDownPressed, Controller2ButtonDownPressed;
        public static bool Controller1ButtonUpPressed, Controller2ButtonUpPressed;
        public static bool Controller1ButtonLeftPressed, Controller2ButtonLeftPressed;
        public static bool Controller1ButtonRightPressed, Controller2ButtonRightPressed;
        public static bool Controller1ButtonShoulderLeftPressed, Controller2ButtonShoulderLeftPressed;
        public static bool Controller1ButtonShoulderRightPressed, Controller2ButtonShoulderRightPressed;
        public static bool Controller1ThumbpadLeftPressed, Controller2ThumbpadLeftPressed;
        public static bool Controller1ThumbpadRightPressed, Controller2ThumbpadRightPressed;
        public static double Controller1TriggerLeftPosition, Controller2TriggerLeftPosition;
        public static double Controller1TriggerRightPosition, Controller2TriggerRightPosition;
        public static double Controller1ThumbLeftX, Controller2ThumbLeftX;
        public static double Controller1ThumbLeftY, Controller2ThumbLeftY;
        public static double Controller1ThumbRightX, Controller2ThumbRightX;
        public static double Controller1ThumbRightY, Controller2ThumbRightY;
        public bool XInputHookConnect()
        {
            try
            {
                controller = new Controller[] { null, null };
                xinum = 0;
                var controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two) };
                foreach (var selectControler in controllers)
                {
                    if (selectControler.IsConnected)
                    {
                        controller[xinum] = selectControler;
                        xinum++;
                        if (xinum >= 2)
                        {
                            break;
                        }
                    }
                }
            }
            catch { }
            if (controller[0] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void ControllerProcess()
        {
            for (int inc = 0; inc < xinum; inc++)
            {
                xistate = controller[inc].GetState();
                if (inc == 0)
                {
                    if (xistate.Gamepad.Buttons.ToString().Contains("A"))
                        Controller1ButtonAPressed = true;
                    else
                        Controller1ButtonAPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().EndsWith("B") | xistate.Gamepad.Buttons.ToString().Contains("B, "))
                        Controller1ButtonBPressed = true;
                    else
                        Controller1ButtonBPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("X"))
                        Controller1ButtonXPressed = true;
                    else
                        Controller1ButtonXPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("Y"))
                        Controller1ButtonYPressed = true;
                    else
                        Controller1ButtonYPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("Start"))
                        Controller1ButtonStartPressed = true;
                    else
                        Controller1ButtonStartPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("Back"))
                        Controller1ButtonBackPressed = true;
                    else
                        Controller1ButtonBackPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("DPadDown"))
                        Controller1ButtonDownPressed = true;
                    else
                        Controller1ButtonDownPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("DPadUp"))
                        Controller1ButtonUpPressed = true;
                    else
                        Controller1ButtonUpPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("DPadLeft"))
                        Controller1ButtonLeftPressed = true;
                    else
                        Controller1ButtonLeftPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("DPadRight"))
                        Controller1ButtonRightPressed = true;
                    else
                        Controller1ButtonRightPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("LeftShoulder"))
                        Controller1ButtonShoulderLeftPressed = true;
                    else
                        Controller1ButtonShoulderLeftPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("RightShoulder"))
                        Controller1ButtonShoulderRightPressed = true;
                    else
                        Controller1ButtonShoulderRightPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("LeftThumb"))
                        Controller1ThumbpadLeftPressed = true;
                    else
                        Controller1ThumbpadLeftPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("RightThumb"))
                        Controller1ThumbpadRightPressed = true;
                    else
                        Controller1ThumbpadRightPressed = false;
                    Controller1TriggerLeftPosition = xistate.Gamepad.LeftTrigger;
                    Controller1TriggerRightPosition = xistate.Gamepad.RightTrigger;
                    Controller1ThumbLeftX = xistate.Gamepad.LeftThumbX;
                    Controller1ThumbLeftY = xistate.Gamepad.LeftThumbY;
                    Controller1ThumbRightX = xistate.Gamepad.RightThumbX;
                    Controller1ThumbRightY = xistate.Gamepad.RightThumbY;
                    if (gyromode != 1 & gyromode != 2)
                    {
                        Controller1GyroX = -Controller1ThumbRightX;
                        Controller1GyroY = -Controller1ThumbRightY;
                    }
                }
                if (inc == 1)
                {
                    if (xistate.Gamepad.Buttons.ToString().Contains("A"))
                        Controller2ButtonAPressed = true;
                    else
                        Controller2ButtonAPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().EndsWith("B") | xistate.Gamepad.Buttons.ToString().Contains("B, "))
                        Controller2ButtonBPressed = true;
                    else
                        Controller2ButtonBPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("X"))
                        Controller2ButtonXPressed = true;
                    else
                        Controller2ButtonXPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("Y"))
                        Controller2ButtonYPressed = true;
                    else
                        Controller2ButtonYPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("Start"))
                        Controller2ButtonStartPressed = true;
                    else
                        Controller2ButtonStartPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("Back"))
                        Controller2ButtonBackPressed = true;
                    else
                        Controller2ButtonBackPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("DPadDown"))
                        Controller2ButtonDownPressed = true;
                    else
                        Controller2ButtonDownPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("DPadUp"))
                        Controller2ButtonUpPressed = true;
                    else
                        Controller2ButtonUpPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("DPadLeft"))
                        Controller2ButtonLeftPressed = true;
                    else
                        Controller2ButtonLeftPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("DPadRight"))
                        Controller2ButtonRightPressed = true;
                    else
                        Controller2ButtonRightPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("LeftShoulder"))
                        Controller2ButtonShoulderLeftPressed = true;
                    else
                        Controller2ButtonShoulderLeftPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("RightShoulder"))
                        Controller2ButtonShoulderRightPressed = true;
                    else
                        Controller2ButtonShoulderRightPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("LeftThumb"))
                        Controller2ThumbpadLeftPressed = true;
                    else
                        Controller2ThumbpadLeftPressed = false;
                    if (xistate.Gamepad.Buttons.ToString().Contains("RightThumb"))
                        Controller2ThumbpadRightPressed = true;
                    else
                        Controller2ThumbpadRightPressed = false;
                    Controller2TriggerLeftPosition = xistate.Gamepad.LeftTrigger;
                    Controller2TriggerRightPosition = xistate.Gamepad.RightTrigger;
                    Controller2ThumbLeftX = xistate.Gamepad.LeftThumbX;
                    Controller2ThumbLeftY = xistate.Gamepad.LeftThumbY;
                    Controller2ThumbRightX = xistate.Gamepad.RightThumbX;
                    Controller2ThumbRightY = xistate.Gamepad.RightThumbY;
                    if (gyromode != 1 & gyromode != 2)
                    {
                        Controller2GyroX = -Controller2ThumbRightX;
                        Controller2GyroY = -Controller2ThumbRightY;
                    }
                }
            }
        }
        public static bool Controller1GyroCenter;
        public static double Controller1GyroX, Controller1GyroY;
        public static Vector3 InitEulerAnglesaController1, EulerAnglesaController1, InitEulerAnglesbController1, EulerAnglesbController1;
        public static Vector3 gyr_gController1 = new Vector3();
        public static Vector3 i_aController1 = new Vector3(1, 0, 0);
        public static Vector3 j_aController1 = new Vector3(0, 1, 0);
        public static Vector3 k_aController1 = new Vector3(0, 0, 1);
        public static Vector3 k_aCrossController1 = new Vector3(0, 0, 1);
        public static Vector3 i_bController1 = new Vector3(1, 0, 0);
        public static Vector3 j_bController1 = new Vector3(0, 1, 0);
        public static Vector3 j_bCrossController1 = new Vector3(0, 1, 0);
        public static Vector3 k_bController1 = new Vector3(0, 0, 1);
        public static Quaternion GetVectorController1(Vector3 i_Controller1, Vector3 j_Controller1, Vector3 k_Controller1)
        {
            Vector3 v1 = new Vector3(j_Controller1.X, i_Controller1.X, k_Controller1.X);
            Vector3 v2 = -new Vector3(j_Controller1.Z, i_Controller1.Z, k_Controller1.Z);
            return QuaternionLookRotationController1(v1, v2);
        }
        private static Quaternion QuaternionLookRotationController1(Vector3 forward, Vector3 up)
        {
            Vector3 vector = forward;
            Vector3 vector2 = Vector3.Cross(up, vector);
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            Quaternion quaternion = new Quaternion();
            double m00 = vector2.X;
            double m01 = vector2.Y;
            double m02 = vector2.Z;
            double m10 = vector3.X;
            double m11 = vector3.Y;
            double m12 = vector3.Z;
            double m20 = vector.X;
            double m21 = vector.Y;
            double m22 = vector.Z;
            double num8 = m00 + m11 + m22;
            double num = Math.Sqrt(num8 + 1f);
            quaternion.W = (float)num * 0.5f;
            num = 0.5f / num;
            quaternion.X = (float)(m12 - m21) * (float)num;
            quaternion.Y = (float)(m20 - m02) * (float)num;
            quaternion.Z = (float)(m01 - m10) * (float)num;
            return quaternion;
        }
        private static Vector3 ToEulerAnglesController1(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;
            pitchYawRoll.X = (float)Math.Asin(2f * test / unit);
            pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);
            pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);
            return pitchYawRoll;
        }
        private void Controller1ProcessIMU()
        {
            if (gyromode == 1 | gyromode == 2)
            {
                gyr_gController1.X = 0;
                gyr_gController1.Y = -(float)Math.Round((Int16)Controller1ThumbRightY / 666666666f, 7);
                gyr_gController1.Z = -(float)Math.Round((Int16)Controller1ThumbRightX / 666666666f, 7);
                i_aController1 = new Vector3(1, 0, 0);
                j_aController1 = new Vector3(0, 1, 0);
                k_aController1.Y = 0f;
                k_aController1.Z = 1f;
                i_bController1 = new Vector3(1, 0, 0);
                j_bController1.Y = 1f;
                k_bController1 = new Vector3(0, 0, 1);
                if (gyromode == 1)
                {
                    k_aCrossController1 += Vector3.Cross(gyr_gController1, Vector3.Normalize(k_aController1));
                    j_bCrossController1 += Vector3.Cross(gyr_gController1, Vector3.Normalize(j_bController1));
                    EulerAnglesaController1 = ToEulerAnglesController1(GetVectorController1(Vector3.Normalize(i_aController1), Vector3.Normalize(j_aController1), Vector3.Normalize(k_aCrossController1))) - InitEulerAnglesaController1;
                    EulerAnglesbController1 = Vector3.Normalize(ToEulerAnglesController1(GetVectorController1(Vector3.Normalize(i_bController1), Vector3.Normalize(j_bCrossController1), Vector3.Normalize(k_bController1)))) - InitEulerAnglesbController1;
                    Controller1GyroX = (EulerAnglesbController1.X - EulerAnglesbController1.Y) * 22222222f;
                    Controller1GyroY = EulerAnglesaController1.Z * 22222222f;
                    if (Controller1GyroCenter | (int)Controller1GyroX == 0)
                    {
                        j_bCrossController1 = new Vector3(0, 1, 0);
                        j_bController1.X = 0f;
                        j_bController1.Z = 0f;
                        InitEulerAnglesbController1 = Vector3.Normalize(ToEulerAnglesController1(GetVectorController1(Vector3.Normalize(i_bController1), Vector3.Normalize(j_bCrossController1), Vector3.Normalize(k_bController1))));
                    }
                    if (Controller1GyroCenter | (int)Controller1GyroY == 0)
                    {
                        k_aCrossController1 = new Vector3(0, 0, 1);
                        k_aController1.X = 0f;
                        InitEulerAnglesaController1 = ToEulerAnglesController1(GetVectorController1(Vector3.Normalize(i_aController1), Vector3.Normalize(j_aController1), Vector3.Normalize(k_aCrossController1)));
                    }
                }
                else if (gyromode == 2)
                {
                    k_aCrossController1 = Vector3.Cross(gyr_gController1, (k_aController1)) * 10f;
                    j_bCrossController1 = Vector3.Cross(gyr_gController1, (j_bController1)) * 10f;
                    EulerAnglesaController1 = ToEulerAnglesController1(GetVectorController1((i_aController1), (j_aController1), (k_aCrossController1))) - InitEulerAnglesaController1;
                    EulerAnglesbController1 = (ToEulerAnglesController1(GetVectorController1((i_bController1), (j_bCrossController1), (k_bController1)))) - InitEulerAnglesbController1;
                    Controller1GyroX = (EulerAnglesbController1.X - EulerAnglesbController1.Y) * 22222222f;
                    Controller1GyroY = EulerAnglesaController1.Z * 22222222f;
                    if (Controller1GyroCenter | (int)Controller1GyroX == 0)
                    {
                        j_bCrossController1 = new Vector3(0, 1, 0);
                        j_bController1.X = 0f;
                        j_bController1.Z = 0f;
                        InitEulerAnglesbController1 = (ToEulerAnglesController1(GetVectorController1((i_bController1), (j_bCrossController1), (k_bController1))));
                    }
                    if (Controller1GyroCenter | (int)Controller1GyroY == 0)
                    {
                        k_aCrossController1 = new Vector3(0, 0, 1);
                        k_aController1.X = 0f;
                        InitEulerAnglesaController1 = ToEulerAnglesController1(GetVectorController1((i_aController1), (j_aController1), (k_aCrossController1)));
                    }
                }
            }
        }
        public static bool Controller2GyroCenter;
        public static double Controller2GyroX, Controller2GyroY;
        public static Vector3 InitEulerAnglesaController2, EulerAnglesaController2, InitEulerAnglesbController2, EulerAnglesbController2;
        public static Vector3 gyr_gController2 = new Vector3();
        public static Vector3 i_aController2 = new Vector3(1, 0, 0);
        public static Vector3 j_aController2 = new Vector3(0, 1, 0);
        public static Vector3 k_aController2 = new Vector3(0, 0, 1);
        public static Vector3 k_aCrossController2 = new Vector3(0, 0, 1);
        public static Vector3 i_bController2 = new Vector3(1, 0, 0);
        public static Vector3 j_bController2 = new Vector3(0, 1, 0);
        public static Vector3 j_bCrossController2 = new Vector3(0, 1, 0);
        public static Vector3 k_bController2 = new Vector3(0, 0, 1);
        public static Quaternion GetVectorController2(Vector3 i_Controller2, Vector3 j_Controller2, Vector3 k_Controller2)
        {
            Vector3 v1 = new Vector3(j_Controller2.X, i_Controller2.X, k_Controller2.X);
            Vector3 v2 = -new Vector3(j_Controller2.Z, i_Controller2.Z, k_Controller2.Z);
            return QuaternionLookRotationController2(v1, v2);
        }
        private static Quaternion QuaternionLookRotationController2(Vector3 forward, Vector3 up)
        {
            Vector3 vector = forward;
            Vector3 vector2 = Vector3.Cross(up, vector);
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            Quaternion quaternion = new Quaternion();
            double m00 = vector2.X;
            double m01 = vector2.Y;
            double m02 = vector2.Z;
            double m10 = vector3.X;
            double m11 = vector3.Y;
            double m12 = vector3.Z;
            double m20 = vector.X;
            double m21 = vector.Y;
            double m22 = vector.Z;
            double num8 = m00 + m11 + m22;
            double num = Math.Sqrt(num8 + 1f);
            quaternion.W = (float)num * 0.5f;
            num = 0.5f / num;
            quaternion.X = (float)(m12 - m21) * (float)num;
            quaternion.Y = (float)(m20 - m02) * (float)num;
            quaternion.Z = (float)(m01 - m10) * (float)num;
            return quaternion;
        }
        private static Vector3 ToEulerAnglesController2(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;
            pitchYawRoll.X = (float)Math.Asin(2f * test / unit);
            pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);
            pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);
            return pitchYawRoll;
        }
        private void Controller2ProcessIMU()
        {
            if (gyromode == 1 | gyromode == 2)
            {
                gyr_gController2.X = 0;
                gyr_gController2.Y = -(float)Math.Round((Int16)Controller2ThumbRightY / 666666666f, 7);
                gyr_gController2.Z = -(float)Math.Round((Int16)Controller2ThumbRightX / 666666666f, 7);
                i_aController2 = new Vector3(1, 0, 0);
                j_aController2 = new Vector3(0, 1, 0);
                k_aController2.Y = 0f;
                k_aController2.Z = 1f;
                i_bController2 = new Vector3(1, 0, 0);
                j_bController2.Y = 1f;
                k_bController2 = new Vector3(0, 0, 1);
                if (gyromode == 1)
                {
                    k_aCrossController2 += Vector3.Cross(gyr_gController2, Vector3.Normalize(k_aController2));
                    j_bCrossController2 += Vector3.Cross(gyr_gController2, Vector3.Normalize(j_bController2));
                    EulerAnglesaController2 = ToEulerAnglesController2(GetVectorController2(Vector3.Normalize(i_aController2), Vector3.Normalize(j_aController2), Vector3.Normalize(k_aCrossController2))) - InitEulerAnglesaController2;
                    EulerAnglesbController2 = Vector3.Normalize(ToEulerAnglesController2(GetVectorController2(Vector3.Normalize(i_bController2), Vector3.Normalize(j_bCrossController2), Vector3.Normalize(k_bController2)))) - InitEulerAnglesbController2;
                    Controller2GyroX = (EulerAnglesbController2.X - EulerAnglesbController2.Y) * 22222222f;
                    Controller2GyroY = EulerAnglesaController2.Z * 22222222f;
                    if (Controller2GyroCenter | (int)Controller2GyroX == 0)
                    {
                        j_bCrossController2 = new Vector3(0, 1, 0);
                        j_bController2.X = 0f;
                        j_bController2.Z = 0f;
                        InitEulerAnglesbController2 = Vector3.Normalize(ToEulerAnglesController2(GetVectorController2(Vector3.Normalize(i_bController2), Vector3.Normalize(j_bCrossController2), Vector3.Normalize(k_bController2))));
                    }
                    if (Controller2GyroCenter | (int)Controller2GyroY == 0)
                    {
                        k_aCrossController2 = new Vector3(0, 0, 1);
                        k_aController2.X = 0f;
                        InitEulerAnglesaController2 = ToEulerAnglesController2(GetVectorController2(Vector3.Normalize(i_aController2), Vector3.Normalize(j_aController2), Vector3.Normalize(k_aCrossController2)));
                    }
                }
                else if (gyromode == 2)
                {
                    k_aCrossController2 = Vector3.Cross(gyr_gController2, (k_aController2)) * 10f;
                    j_bCrossController2 = Vector3.Cross(gyr_gController2, (j_bController2)) * 10f;
                    EulerAnglesaController2 = ToEulerAnglesController2(GetVectorController2((i_aController2), (j_aController2), (k_aCrossController2))) - InitEulerAnglesaController2;
                    EulerAnglesbController2 = (ToEulerAnglesController2(GetVectorController2((i_bController2), (j_bCrossController2), (k_bController2)))) - InitEulerAnglesbController2;
                    Controller2GyroX = (EulerAnglesbController2.X - EulerAnglesbController2.Y) * 22222222f;
                    Controller2GyroY = EulerAnglesaController2.Z * 22222222f;
                    if (Controller2GyroCenter | (int)Controller2GyroX == 0)
                    {
                        j_bCrossController2 = new Vector3(0, 1, 0);
                        j_bController2.X = 0f;
                        j_bController2.Z = 0f;
                        InitEulerAnglesbController2 = (ToEulerAnglesController2(GetVectorController2((i_bController2), (j_bCrossController2), (k_bController2))));
                    }
                    if (Controller2GyroCenter | (int)Controller2GyroY == 0)
                    {
                        k_aCrossController2 = new Vector3(0, 0, 1);
                        k_aController2.X = 0f;
                        InitEulerAnglesaController2 = ToEulerAnglesController2(GetVectorController2((i_aController2), (j_aController2), (k_aCrossController2)));
                    }
                }
            }
        }
        private static Joystick[] joystick = new Joystick[] { null, null };
        private static Guid[] joystickGuid = new Guid[] { Guid.Empty, Guid.Empty };
        private static int dinum = 0;
        public static int Joystick1AxisX;
        public static int Joystick1AxisY;
        public static int Joystick1AxisZ;
        public static int Joystick1RotationX;
        public static int Joystick1RotationY;
        public static int Joystick1RotationZ;
        public static int Joystick1Sliders0;
        public static int Joystick1Sliders1;
        public static int Joystick1PointOfViewControllers0;
        public static int Joystick1PointOfViewControllers1;
        public static int Joystick1PointOfViewControllers2;
        public static int Joystick1PointOfViewControllers3;
        public static int Joystick1VelocityX;
        public static int Joystick1VelocityY;
        public static int Joystick1VelocityZ;
        public static int Joystick1AngularVelocityX;
        public static int Joystick1AngularVelocityY;
        public static int Joystick1AngularVelocityZ;
        public static int Joystick1VelocitySliders0;
        public static int Joystick1VelocitySliders1;
        public static int Joystick1AccelerationX;
        public static int Joystick1AccelerationY;
        public static int Joystick1AccelerationZ;
        public static int Joystick1AngularAccelerationX;
        public static int Joystick1AngularAccelerationY;
        public static int Joystick1AngularAccelerationZ;
        public static int Joystick1AccelerationSliders0;
        public static int Joystick1AccelerationSliders1;
        public static int Joystick1ForceX;
        public static int Joystick1ForceY;
        public static int Joystick1ForceZ;
        public static int Joystick1TorqueX;
        public static int Joystick1TorqueY;
        public static int Joystick1TorqueZ;
        public static int Joystick1ForceSliders0;
        public static int Joystick1ForceSliders1;
        public static bool Joystick1Buttons0, Joystick1Buttons1, Joystick1Buttons2, Joystick1Buttons3, Joystick1Buttons4, Joystick1Buttons5, Joystick1Buttons6, Joystick1Buttons7, Joystick1Buttons8, Joystick1Buttons9, Joystick1Buttons10, Joystick1Buttons11, Joystick1Buttons12, Joystick1Buttons13, Joystick1Buttons14, Joystick1Buttons15, Joystick1Buttons16, Joystick1Buttons17, Joystick1Buttons18, Joystick1Buttons19, Joystick1Buttons20, Joystick1Buttons21, Joystick1Buttons22, Joystick1Buttons23, Joystick1Buttons24, Joystick1Buttons25, Joystick1Buttons26, Joystick1Buttons27, Joystick1Buttons28, Joystick1Buttons29, Joystick1Buttons30, Joystick1Buttons31, Joystick1Buttons32, Joystick1Buttons33, Joystick1Buttons34, Joystick1Buttons35, Joystick1Buttons36, Joystick1Buttons37, Joystick1Buttons38, Joystick1Buttons39, Joystick1Buttons40, Joystick1Buttons41, Joystick1Buttons42, Joystick1Buttons43, Joystick1Buttons44, Joystick1Buttons45, Joystick1Buttons46, Joystick1Buttons47, Joystick1Buttons48, Joystick1Buttons49, Joystick1Buttons50, Joystick1Buttons51, Joystick1Buttons52, Joystick1Buttons53, Joystick1Buttons54, Joystick1Buttons55, Joystick1Buttons56, Joystick1Buttons57, Joystick1Buttons58, Joystick1Buttons59, Joystick1Buttons60, Joystick1Buttons61, Joystick1Buttons62, Joystick1Buttons63, Joystick1Buttons64, Joystick1Buttons65, Joystick1Buttons66, Joystick1Buttons67, Joystick1Buttons68, Joystick1Buttons69, Joystick1Buttons70, Joystick1Buttons71, Joystick1Buttons72, Joystick1Buttons73, Joystick1Buttons74, Joystick1Buttons75, Joystick1Buttons76, Joystick1Buttons77, Joystick1Buttons78, Joystick1Buttons79, Joystick1Buttons80, Joystick1Buttons81, Joystick1Buttons82, Joystick1Buttons83, Joystick1Buttons84, Joystick1Buttons85, Joystick1Buttons86, Joystick1Buttons87, Joystick1Buttons88, Joystick1Buttons89, Joystick1Buttons90, Joystick1Buttons91, Joystick1Buttons92, Joystick1Buttons93, Joystick1Buttons94, Joystick1Buttons95, Joystick1Buttons96, Joystick1Buttons97, Joystick1Buttons98, Joystick1Buttons99, Joystick1Buttons100, Joystick1Buttons101, Joystick1Buttons102, Joystick1Buttons103, Joystick1Buttons104, Joystick1Buttons105, Joystick1Buttons106, Joystick1Buttons107, Joystick1Buttons108, Joystick1Buttons109, Joystick1Buttons110, Joystick1Buttons111, Joystick1Buttons112, Joystick1Buttons113, Joystick1Buttons114, Joystick1Buttons115, Joystick1Buttons116, Joystick1Buttons117, Joystick1Buttons118, Joystick1Buttons119, Joystick1Buttons120, Joystick1Buttons121, Joystick1Buttons122, Joystick1Buttons123, Joystick1Buttons124, Joystick1Buttons125, Joystick1Buttons126, Joystick1Buttons127;
        public static int Joystick2AxisX;
        public static int Joystick2AxisY;
        public static int Joystick2AxisZ;
        public static int Joystick2RotationX;
        public static int Joystick2RotationY;
        public static int Joystick2RotationZ;
        public static int Joystick2Sliders0;
        public static int Joystick2Sliders1;
        public static int Joystick2PointOfViewControllers0;
        public static int Joystick2PointOfViewControllers1;
        public static int Joystick2PointOfViewControllers2;
        public static int Joystick2PointOfViewControllers3;
        public static int Joystick2VelocityX;
        public static int Joystick2VelocityY;
        public static int Joystick2VelocityZ;
        public static int Joystick2AngularVelocityX;
        public static int Joystick2AngularVelocityY;
        public static int Joystick2AngularVelocityZ;
        public static int Joystick2VelocitySliders0;
        public static int Joystick2VelocitySliders1;
        public static int Joystick2AccelerationX;
        public static int Joystick2AccelerationY;
        public static int Joystick2AccelerationZ;
        public static int Joystick2AngularAccelerationX;
        public static int Joystick2AngularAccelerationY;
        public static int Joystick2AngularAccelerationZ;
        public static int Joystick2AccelerationSliders0;
        public static int Joystick2AccelerationSliders1;
        public static int Joystick2ForceX;
        public static int Joystick2ForceY;
        public static int Joystick2ForceZ;
        public static int Joystick2TorqueX;
        public static int Joystick2TorqueY;
        public static int Joystick2TorqueZ;
        public static int Joystick2ForceSliders0;
        public static int Joystick2ForceSliders1;
        public static bool Joystick2Buttons0, Joystick2Buttons1, Joystick2Buttons2, Joystick2Buttons3, Joystick2Buttons4, Joystick2Buttons5, Joystick2Buttons6, Joystick2Buttons7, Joystick2Buttons8, Joystick2Buttons9, Joystick2Buttons10, Joystick2Buttons11, Joystick2Buttons12, Joystick2Buttons13, Joystick2Buttons14, Joystick2Buttons15, Joystick2Buttons16, Joystick2Buttons17, Joystick2Buttons18, Joystick2Buttons19, Joystick2Buttons20, Joystick2Buttons21, Joystick2Buttons22, Joystick2Buttons23, Joystick2Buttons24, Joystick2Buttons25, Joystick2Buttons26, Joystick2Buttons27, Joystick2Buttons28, Joystick2Buttons29, Joystick2Buttons30, Joystick2Buttons31, Joystick2Buttons32, Joystick2Buttons33, Joystick2Buttons34, Joystick2Buttons35, Joystick2Buttons36, Joystick2Buttons37, Joystick2Buttons38, Joystick2Buttons39, Joystick2Buttons40, Joystick2Buttons41, Joystick2Buttons42, Joystick2Buttons43, Joystick2Buttons44, Joystick2Buttons45, Joystick2Buttons46, Joystick2Buttons47, Joystick2Buttons48, Joystick2Buttons49, Joystick2Buttons50, Joystick2Buttons51, Joystick2Buttons52, Joystick2Buttons53, Joystick2Buttons54, Joystick2Buttons55, Joystick2Buttons56, Joystick2Buttons57, Joystick2Buttons58, Joystick2Buttons59, Joystick2Buttons60, Joystick2Buttons61, Joystick2Buttons62, Joystick2Buttons63, Joystick2Buttons64, Joystick2Buttons65, Joystick2Buttons66, Joystick2Buttons67, Joystick2Buttons68, Joystick2Buttons69, Joystick2Buttons70, Joystick2Buttons71, Joystick2Buttons72, Joystick2Buttons73, Joystick2Buttons74, Joystick2Buttons75, Joystick2Buttons76, Joystick2Buttons77, Joystick2Buttons78, Joystick2Buttons79, Joystick2Buttons80, Joystick2Buttons81, Joystick2Buttons82, Joystick2Buttons83, Joystick2Buttons84, Joystick2Buttons85, Joystick2Buttons86, Joystick2Buttons87, Joystick2Buttons88, Joystick2Buttons89, Joystick2Buttons90, Joystick2Buttons91, Joystick2Buttons92, Joystick2Buttons93, Joystick2Buttons94, Joystick2Buttons95, Joystick2Buttons96, Joystick2Buttons97, Joystick2Buttons98, Joystick2Buttons99, Joystick2Buttons100, Joystick2Buttons101, Joystick2Buttons102, Joystick2Buttons103, Joystick2Buttons104, Joystick2Buttons105, Joystick2Buttons106, Joystick2Buttons107, Joystick2Buttons108, Joystick2Buttons109, Joystick2Buttons110, Joystick2Buttons111, Joystick2Buttons112, Joystick2Buttons113, Joystick2Buttons114, Joystick2Buttons115, Joystick2Buttons116, Joystick2Buttons117, Joystick2Buttons118, Joystick2Buttons119, Joystick2Buttons120, Joystick2Buttons121, Joystick2Buttons122, Joystick2Buttons123, Joystick2Buttons124, Joystick2Buttons125, Joystick2Buttons126, Joystick2Buttons127;
        public bool DirectInputHookConnect()
        {
            try
            {
                directInput = new DirectInput();
                joystick = new Joystick[] { null, null };
                joystickGuid = new Guid[] { Guid.Empty, Guid.Empty };
                dinum = 0;
                foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                {
                    joystickGuid[dinum] = deviceInstance.InstanceGuid;
                    dinum++;
                    if (dinum >= 2)
                    {
                        break;
                    }
                }
                if (dinum < 2)
                {
                    foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid[dinum] = deviceInstance.InstanceGuid;
                        dinum++;
                        if (dinum >= 2)
                        {
                            break;
                        }
                    }
                }
                if (dinum < 2)
                {
                    foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Flight, DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid[dinum] = deviceInstance.InstanceGuid;
                        dinum++;
                        if (dinum >= 2)
                        {
                            break;
                        }
                    }
                }
                if (dinum < 2)
                {
                    foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.FirstPerson, DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid[dinum] = deviceInstance.InstanceGuid;
                        dinum++;
                        if (dinum >= 2)
                        {
                            break;
                        }
                    }
                }
                if (dinum < 2)
                {
                    foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Driving, DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid[dinum] = deviceInstance.InstanceGuid;
                        dinum++;
                        if (dinum >= 2)
                        {
                            break;
                        }
                    }
                }
            }
            catch { }
            if (joystickGuid[0] == Guid.Empty)
            {
                return false;
            }
            else
            {
                for (int inc = 0; inc < dinum; inc++)
                {
                    joystick[inc] = new Joystick(directInput, joystickGuid[inc]);
                    joystick[inc].Properties.BufferSize = 128;
                    joystick[inc].Acquire();
                }
                return true;
            }
        }
        private void GamepadProcess()
        {
            for (int inc = 0; inc < dinum; inc++)
            {
                joystick[inc].Poll();
                var datas = joystick[inc].GetBufferedData();
                foreach (var state in datas)
                {
                    if (inc == 0 & state.Offset == JoystickOffset.X)
                        Joystick1AxisX = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.Y)
                        Joystick1AxisY = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.Z)
                        Joystick1AxisZ = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.RotationX)
                        Joystick1RotationX = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.RotationY)
                        Joystick1RotationY = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.RotationZ)
                        Joystick1RotationZ = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.Sliders0)
                        Joystick1Sliders0 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.Sliders1)
                        Joystick1Sliders1 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.PointOfViewControllers0)
                        Joystick1PointOfViewControllers0 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.PointOfViewControllers1)
                        Joystick1PointOfViewControllers1 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.PointOfViewControllers2)
                        Joystick1PointOfViewControllers2 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.PointOfViewControllers3)
                        Joystick1PointOfViewControllers3 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.VelocityX)
                        Joystick1VelocityX = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.VelocityY)
                        Joystick1VelocityY = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.VelocityZ)
                        Joystick1VelocityZ = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AngularVelocityX)
                        Joystick1AngularVelocityX = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AngularVelocityY)
                        Joystick1AngularVelocityY = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AngularVelocityZ)
                        Joystick1AngularVelocityZ = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.VelocitySliders0)
                        Joystick1VelocitySliders0 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.VelocitySliders1)
                        Joystick1VelocitySliders1 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AccelerationX)
                        Joystick1AccelerationX = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AccelerationY)
                        Joystick1AccelerationY = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AccelerationZ)
                        Joystick1AccelerationZ = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AngularAccelerationX)
                        Joystick1AngularAccelerationX = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AngularAccelerationY)
                        Joystick1AngularAccelerationY = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AngularAccelerationZ)
                        Joystick1AngularAccelerationZ = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AccelerationSliders0)
                        Joystick1AccelerationSliders0 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.AccelerationSliders1)
                        Joystick1AccelerationSliders1 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.ForceX)
                        Joystick1ForceX = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.ForceY)
                        Joystick1ForceY = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.ForceZ)
                        Joystick1ForceZ = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.TorqueX)
                        Joystick1TorqueX = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.TorqueY)
                        Joystick1TorqueY = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.TorqueZ)
                        Joystick1TorqueZ = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.ForceSliders0)
                        Joystick1ForceSliders0 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.ForceSliders1)
                        Joystick1ForceSliders1 = state.Value;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons0 & state.Value == 128)
                        Joystick1Buttons0 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons0 & state.Value == 0)
                        Joystick1Buttons0 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons1 & state.Value == 128)
                        Joystick1Buttons1 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons1 & state.Value == 0)
                        Joystick1Buttons1 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons2 & state.Value == 128)
                        Joystick1Buttons2 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons2 & state.Value == 0)
                        Joystick1Buttons2 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons3 & state.Value == 128)
                        Joystick1Buttons3 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons3 & state.Value == 0)
                        Joystick1Buttons3 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons4 & state.Value == 128)
                        Joystick1Buttons4 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons4 & state.Value == 0)
                        Joystick1Buttons4 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons5 & state.Value == 128)
                        Joystick1Buttons5 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons5 & state.Value == 0)
                        Joystick1Buttons5 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons6 & state.Value == 128)
                        Joystick1Buttons6 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons6 & state.Value == 0)
                        Joystick1Buttons6 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons7 & state.Value == 128)
                        Joystick1Buttons7 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons7 & state.Value == 0)
                        Joystick1Buttons7 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons8 & state.Value == 128)
                        Joystick1Buttons8 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons8 & state.Value == 0)
                        Joystick1Buttons8 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons9 & state.Value == 128)
                        Joystick1Buttons9 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons9 & state.Value == 0)
                        Joystick1Buttons9 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons10 & state.Value == 128)
                        Joystick1Buttons10 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons10 & state.Value == 0)
                        Joystick1Buttons10 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons11 & state.Value == 128)
                        Joystick1Buttons11 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons11 & state.Value == 0)
                        Joystick1Buttons11 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons12 & state.Value == 128)
                        Joystick1Buttons12 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons12 & state.Value == 0)
                        Joystick1Buttons12 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons13 & state.Value == 128)
                        Joystick1Buttons13 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons13 & state.Value == 0)
                        Joystick1Buttons13 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons14 & state.Value == 128)
                        Joystick1Buttons14 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons14 & state.Value == 0)
                        Joystick1Buttons14 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons15 & state.Value == 128)
                        Joystick1Buttons15 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons15 & state.Value == 0)
                        Joystick1Buttons15 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons16 & state.Value == 128)
                        Joystick1Buttons16 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons16 & state.Value == 0)
                        Joystick1Buttons16 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons17 & state.Value == 128)
                        Joystick1Buttons17 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons17 & state.Value == 0)
                        Joystick1Buttons17 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons18 & state.Value == 128)
                        Joystick1Buttons18 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons18 & state.Value == 0)
                        Joystick1Buttons18 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons19 & state.Value == 128)
                        Joystick1Buttons19 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons19 & state.Value == 0)
                        Joystick1Buttons19 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons20 & state.Value == 128)
                        Joystick1Buttons20 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons20 & state.Value == 0)
                        Joystick1Buttons20 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons21 & state.Value == 128)
                        Joystick1Buttons21 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons21 & state.Value == 0)
                        Joystick1Buttons21 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons22 & state.Value == 128)
                        Joystick1Buttons22 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons22 & state.Value == 0)
                        Joystick1Buttons22 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons23 & state.Value == 128)
                        Joystick1Buttons23 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons23 & state.Value == 0)
                        Joystick1Buttons23 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons24 & state.Value == 128)
                        Joystick1Buttons24 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons24 & state.Value == 0)
                        Joystick1Buttons24 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons25 & state.Value == 128)
                        Joystick1Buttons25 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons25 & state.Value == 0)
                        Joystick1Buttons25 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons26 & state.Value == 128)
                        Joystick1Buttons26 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons26 & state.Value == 0)
                        Joystick1Buttons26 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons27 & state.Value == 128)
                        Joystick1Buttons27 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons27 & state.Value == 0)
                        Joystick1Buttons27 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons28 & state.Value == 128)
                        Joystick1Buttons28 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons28 & state.Value == 0)
                        Joystick1Buttons28 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons29 & state.Value == 128)
                        Joystick1Buttons29 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons29 & state.Value == 0)
                        Joystick1Buttons29 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons30 & state.Value == 128)
                        Joystick1Buttons30 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons30 & state.Value == 0)
                        Joystick1Buttons30 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons31 & state.Value == 128)
                        Joystick1Buttons31 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons31 & state.Value == 0)
                        Joystick1Buttons31 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons32 & state.Value == 128)
                        Joystick1Buttons32 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons32 & state.Value == 0)
                        Joystick1Buttons32 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons33 & state.Value == 128)
                        Joystick1Buttons33 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons33 & state.Value == 0)
                        Joystick1Buttons33 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons34 & state.Value == 128)
                        Joystick1Buttons34 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons34 & state.Value == 0)
                        Joystick1Buttons34 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons35 & state.Value == 128)
                        Joystick1Buttons35 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons35 & state.Value == 0)
                        Joystick1Buttons35 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons36 & state.Value == 128)
                        Joystick1Buttons36 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons36 & state.Value == 0)
                        Joystick1Buttons36 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons37 & state.Value == 128)
                        Joystick1Buttons37 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons37 & state.Value == 0)
                        Joystick1Buttons37 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons38 & state.Value == 128)
                        Joystick1Buttons38 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons38 & state.Value == 0)
                        Joystick1Buttons38 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons39 & state.Value == 128)
                        Joystick1Buttons39 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons39 & state.Value == 0)
                        Joystick1Buttons39 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons40 & state.Value == 128)
                        Joystick1Buttons40 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons40 & state.Value == 0)
                        Joystick1Buttons40 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons41 & state.Value == 128)
                        Joystick1Buttons41 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons41 & state.Value == 0)
                        Joystick1Buttons41 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons42 & state.Value == 128)
                        Joystick1Buttons42 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons42 & state.Value == 0)
                        Joystick1Buttons42 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons43 & state.Value == 128)
                        Joystick1Buttons43 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons43 & state.Value == 0)
                        Joystick1Buttons43 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons44 & state.Value == 128)
                        Joystick1Buttons44 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons44 & state.Value == 0)
                        Joystick1Buttons44 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons45 & state.Value == 128)
                        Joystick1Buttons45 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons45 & state.Value == 0)
                        Joystick1Buttons45 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons46 & state.Value == 128)
                        Joystick1Buttons46 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons46 & state.Value == 0)
                        Joystick1Buttons46 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons47 & state.Value == 128)
                        Joystick1Buttons47 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons47 & state.Value == 0)
                        Joystick1Buttons47 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons48 & state.Value == 128)
                        Joystick1Buttons48 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons48 & state.Value == 0)
                        Joystick1Buttons48 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons49 & state.Value == 128)
                        Joystick1Buttons49 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons49 & state.Value == 0)
                        Joystick1Buttons49 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons50 & state.Value == 128)
                        Joystick1Buttons50 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons50 & state.Value == 0)
                        Joystick1Buttons50 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons51 & state.Value == 128)
                        Joystick1Buttons51 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons51 & state.Value == 0)
                        Joystick1Buttons51 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons52 & state.Value == 128)
                        Joystick1Buttons52 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons52 & state.Value == 0)
                        Joystick1Buttons52 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons53 & state.Value == 128)
                        Joystick1Buttons53 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons53 & state.Value == 0)
                        Joystick1Buttons53 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons54 & state.Value == 128)
                        Joystick1Buttons54 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons54 & state.Value == 0)
                        Joystick1Buttons54 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons55 & state.Value == 128)
                        Joystick1Buttons55 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons55 & state.Value == 0)
                        Joystick1Buttons55 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons56 & state.Value == 128)
                        Joystick1Buttons56 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons56 & state.Value == 0)
                        Joystick1Buttons56 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons57 & state.Value == 128)
                        Joystick1Buttons57 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons57 & state.Value == 0)
                        Joystick1Buttons57 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons58 & state.Value == 128)
                        Joystick1Buttons58 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons58 & state.Value == 0)
                        Joystick1Buttons58 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons59 & state.Value == 128)
                        Joystick1Buttons59 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons59 & state.Value == 0)
                        Joystick1Buttons59 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons60 & state.Value == 128)
                        Joystick1Buttons60 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons60 & state.Value == 0)
                        Joystick1Buttons60 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons61 & state.Value == 128)
                        Joystick1Buttons61 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons61 & state.Value == 0)
                        Joystick1Buttons61 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons62 & state.Value == 128)
                        Joystick1Buttons62 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons62 & state.Value == 0)
                        Joystick1Buttons62 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons63 & state.Value == 128)
                        Joystick1Buttons63 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons63 & state.Value == 0)
                        Joystick1Buttons63 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons64 & state.Value == 128)
                        Joystick1Buttons64 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons64 & state.Value == 0)
                        Joystick1Buttons64 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons65 & state.Value == 128)
                        Joystick1Buttons65 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons65 & state.Value == 0)
                        Joystick1Buttons65 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons66 & state.Value == 128)
                        Joystick1Buttons66 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons66 & state.Value == 0)
                        Joystick1Buttons66 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons67 & state.Value == 128)
                        Joystick1Buttons67 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons67 & state.Value == 0)
                        Joystick1Buttons67 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons68 & state.Value == 128)
                        Joystick1Buttons68 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons68 & state.Value == 0)
                        Joystick1Buttons68 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons69 & state.Value == 128)
                        Joystick1Buttons69 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons69 & state.Value == 0)
                        Joystick1Buttons69 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons70 & state.Value == 128)
                        Joystick1Buttons70 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons70 & state.Value == 0)
                        Joystick1Buttons70 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons71 & state.Value == 128)
                        Joystick1Buttons71 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons71 & state.Value == 0)
                        Joystick1Buttons71 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons72 & state.Value == 128)
                        Joystick1Buttons72 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons72 & state.Value == 0)
                        Joystick1Buttons72 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons73 & state.Value == 128)
                        Joystick1Buttons73 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons73 & state.Value == 0)
                        Joystick1Buttons73 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons74 & state.Value == 128)
                        Joystick1Buttons74 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons74 & state.Value == 0)
                        Joystick1Buttons74 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons75 & state.Value == 128)
                        Joystick1Buttons75 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons75 & state.Value == 0)
                        Joystick1Buttons75 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons76 & state.Value == 128)
                        Joystick1Buttons76 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons76 & state.Value == 0)
                        Joystick1Buttons76 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons77 & state.Value == 128)
                        Joystick1Buttons77 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons77 & state.Value == 0)
                        Joystick1Buttons77 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons78 & state.Value == 128)
                        Joystick1Buttons78 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons78 & state.Value == 0)
                        Joystick1Buttons78 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons79 & state.Value == 128)
                        Joystick1Buttons79 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons79 & state.Value == 0)
                        Joystick1Buttons79 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons80 & state.Value == 128)
                        Joystick1Buttons80 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons80 & state.Value == 0)
                        Joystick1Buttons80 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons81 & state.Value == 128)
                        Joystick1Buttons81 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons81 & state.Value == 0)
                        Joystick1Buttons81 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons82 & state.Value == 128)
                        Joystick1Buttons82 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons82 & state.Value == 0)
                        Joystick1Buttons82 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons83 & state.Value == 128)
                        Joystick1Buttons83 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons83 & state.Value == 0)
                        Joystick1Buttons83 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons84 & state.Value == 128)
                        Joystick1Buttons84 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons84 & state.Value == 0)
                        Joystick1Buttons84 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons85 & state.Value == 128)
                        Joystick1Buttons85 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons85 & state.Value == 0)
                        Joystick1Buttons85 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons86 & state.Value == 128)
                        Joystick1Buttons86 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons86 & state.Value == 0)
                        Joystick1Buttons86 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons87 & state.Value == 128)
                        Joystick1Buttons87 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons87 & state.Value == 0)
                        Joystick1Buttons87 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons88 & state.Value == 128)
                        Joystick1Buttons88 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons88 & state.Value == 0)
                        Joystick1Buttons88 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons89 & state.Value == 128)
                        Joystick1Buttons89 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons89 & state.Value == 0)
                        Joystick1Buttons89 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons90 & state.Value == 128)
                        Joystick1Buttons90 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons90 & state.Value == 0)
                        Joystick1Buttons90 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons91 & state.Value == 128)
                        Joystick1Buttons91 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons91 & state.Value == 0)
                        Joystick1Buttons91 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons92 & state.Value == 128)
                        Joystick1Buttons92 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons92 & state.Value == 0)
                        Joystick1Buttons92 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons93 & state.Value == 128)
                        Joystick1Buttons93 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons93 & state.Value == 0)
                        Joystick1Buttons93 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons94 & state.Value == 128)
                        Joystick1Buttons94 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons94 & state.Value == 0)
                        Joystick1Buttons94 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons95 & state.Value == 128)
                        Joystick1Buttons95 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons95 & state.Value == 0)
                        Joystick1Buttons95 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons96 & state.Value == 128)
                        Joystick1Buttons96 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons96 & state.Value == 0)
                        Joystick1Buttons96 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons97 & state.Value == 128)
                        Joystick1Buttons97 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons97 & state.Value == 0)
                        Joystick1Buttons97 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons98 & state.Value == 128)
                        Joystick1Buttons98 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons98 & state.Value == 0)
                        Joystick1Buttons98 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons99 & state.Value == 128)
                        Joystick1Buttons99 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons99 & state.Value == 0)
                        Joystick1Buttons99 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons100 & state.Value == 128)
                        Joystick1Buttons100 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons100 & state.Value == 0)
                        Joystick1Buttons100 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons101 & state.Value == 128)
                        Joystick1Buttons101 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons101 & state.Value == 0)
                        Joystick1Buttons101 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons102 & state.Value == 128)
                        Joystick1Buttons102 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons102 & state.Value == 0)
                        Joystick1Buttons102 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons103 & state.Value == 128)
                        Joystick1Buttons103 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons103 & state.Value == 0)
                        Joystick1Buttons103 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons104 & state.Value == 128)
                        Joystick1Buttons104 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons104 & state.Value == 0)
                        Joystick1Buttons104 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons105 & state.Value == 128)
                        Joystick1Buttons105 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons105 & state.Value == 0)
                        Joystick1Buttons105 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons106 & state.Value == 128)
                        Joystick1Buttons106 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons106 & state.Value == 0)
                        Joystick1Buttons106 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons107 & state.Value == 128)
                        Joystick1Buttons107 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons107 & state.Value == 0)
                        Joystick1Buttons107 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons108 & state.Value == 128)
                        Joystick1Buttons108 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons108 & state.Value == 0)
                        Joystick1Buttons108 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons109 & state.Value == 128)
                        Joystick1Buttons109 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons109 & state.Value == 0)
                        Joystick1Buttons109 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons110 & state.Value == 128)
                        Joystick1Buttons110 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons110 & state.Value == 0)
                        Joystick1Buttons110 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons111 & state.Value == 128)
                        Joystick1Buttons111 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons111 & state.Value == 0)
                        Joystick1Buttons111 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons112 & state.Value == 128)
                        Joystick1Buttons112 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons112 & state.Value == 0)
                        Joystick1Buttons112 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons113 & state.Value == 128)
                        Joystick1Buttons113 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons113 & state.Value == 0)
                        Joystick1Buttons113 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons114 & state.Value == 128)
                        Joystick1Buttons114 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons114 & state.Value == 0)
                        Joystick1Buttons114 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons115 & state.Value == 128)
                        Joystick1Buttons115 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons115 & state.Value == 0)
                        Joystick1Buttons115 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons116 & state.Value == 128)
                        Joystick1Buttons116 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons116 & state.Value == 0)
                        Joystick1Buttons116 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons117 & state.Value == 128)
                        Joystick1Buttons117 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons117 & state.Value == 0)
                        Joystick1Buttons117 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons118 & state.Value == 128)
                        Joystick1Buttons118 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons118 & state.Value == 0)
                        Joystick1Buttons118 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons119 & state.Value == 128)
                        Joystick1Buttons119 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons119 & state.Value == 0)
                        Joystick1Buttons119 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons120 & state.Value == 128)
                        Joystick1Buttons120 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons120 & state.Value == 0)
                        Joystick1Buttons120 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons121 & state.Value == 128)
                        Joystick1Buttons121 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons121 & state.Value == 0)
                        Joystick1Buttons121 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons122 & state.Value == 128)
                        Joystick1Buttons122 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons122 & state.Value == 0)
                        Joystick1Buttons122 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons123 & state.Value == 128)
                        Joystick1Buttons123 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons123 & state.Value == 0)
                        Joystick1Buttons123 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons124 & state.Value == 128)
                        Joystick1Buttons124 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons124 & state.Value == 0)
                        Joystick1Buttons124 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons125 & state.Value == 128)
                        Joystick1Buttons125 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons125 & state.Value == 0)
                        Joystick1Buttons125 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons126 & state.Value == 128)
                        Joystick1Buttons126 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons126 & state.Value == 0)
                        Joystick1Buttons126 = false;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons127 & state.Value == 128)
                        Joystick1Buttons127 = true;
                    if (inc == 0 & state.Offset == JoystickOffset.Buttons127 & state.Value == 0)
                        Joystick1Buttons127 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.X)
                        Joystick2AxisX = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.Y)
                        Joystick2AxisY = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.Z)
                        Joystick2AxisZ = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.RotationX)
                        Joystick2RotationX = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.RotationY)
                        Joystick2RotationY = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.RotationZ)
                        Joystick2RotationZ = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.Sliders0)
                        Joystick2Sliders0 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.Sliders1)
                        Joystick2Sliders1 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.PointOfViewControllers0)
                        Joystick2PointOfViewControllers0 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.PointOfViewControllers1)
                        Joystick2PointOfViewControllers1 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.PointOfViewControllers2)
                        Joystick2PointOfViewControllers2 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.PointOfViewControllers3)
                        Joystick2PointOfViewControllers3 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.VelocityX)
                        Joystick2VelocityX = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.VelocityY)
                        Joystick2VelocityY = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.VelocityZ)
                        Joystick2VelocityZ = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AngularVelocityX)
                        Joystick2AngularVelocityX = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AngularVelocityY)
                        Joystick2AngularVelocityY = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AngularVelocityZ)
                        Joystick2AngularVelocityZ = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.VelocitySliders0)
                        Joystick2VelocitySliders0 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.VelocitySliders1)
                        Joystick2VelocitySliders1 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AccelerationX)
                        Joystick2AccelerationX = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AccelerationY)
                        Joystick2AccelerationY = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AccelerationZ)
                        Joystick2AccelerationZ = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AngularAccelerationX)
                        Joystick2AngularAccelerationX = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AngularAccelerationY)
                        Joystick2AngularAccelerationY = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AngularAccelerationZ)
                        Joystick2AngularAccelerationZ = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AccelerationSliders0)
                        Joystick2AccelerationSliders0 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.AccelerationSliders1)
                        Joystick2AccelerationSliders1 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.ForceX)
                        Joystick2ForceX = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.ForceY)
                        Joystick2ForceY = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.ForceZ)
                        Joystick2ForceZ = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.TorqueX)
                        Joystick2TorqueX = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.TorqueY)
                        Joystick2TorqueY = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.TorqueZ)
                        Joystick2TorqueZ = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.ForceSliders0)
                        Joystick2ForceSliders0 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.ForceSliders1)
                        Joystick2ForceSliders1 = state.Value;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons0 & state.Value == 128)
                        Joystick2Buttons0 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons0 & state.Value == 0)
                        Joystick2Buttons0 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons1 & state.Value == 128)
                        Joystick2Buttons1 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons1 & state.Value == 0)
                        Joystick2Buttons1 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons2 & state.Value == 128)
                        Joystick2Buttons2 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons2 & state.Value == 0)
                        Joystick2Buttons2 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons3 & state.Value == 128)
                        Joystick2Buttons3 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons3 & state.Value == 0)
                        Joystick2Buttons3 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons4 & state.Value == 128)
                        Joystick2Buttons4 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons4 & state.Value == 0)
                        Joystick2Buttons4 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons5 & state.Value == 128)
                        Joystick2Buttons5 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons5 & state.Value == 0)
                        Joystick2Buttons5 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons6 & state.Value == 128)
                        Joystick2Buttons6 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons6 & state.Value == 0)
                        Joystick2Buttons6 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons7 & state.Value == 128)
                        Joystick2Buttons7 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons7 & state.Value == 0)
                        Joystick2Buttons7 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons8 & state.Value == 128)
                        Joystick2Buttons8 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons8 & state.Value == 0)
                        Joystick2Buttons8 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons9 & state.Value == 128)
                        Joystick2Buttons9 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons9 & state.Value == 0)
                        Joystick2Buttons9 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons10 & state.Value == 128)
                        Joystick2Buttons10 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons10 & state.Value == 0)
                        Joystick2Buttons10 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons11 & state.Value == 128)
                        Joystick2Buttons11 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons11 & state.Value == 0)
                        Joystick2Buttons11 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons12 & state.Value == 128)
                        Joystick2Buttons12 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons12 & state.Value == 0)
                        Joystick2Buttons12 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons13 & state.Value == 128)
                        Joystick2Buttons13 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons13 & state.Value == 0)
                        Joystick2Buttons13 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons14 & state.Value == 128)
                        Joystick2Buttons14 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons14 & state.Value == 0)
                        Joystick2Buttons14 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons15 & state.Value == 128)
                        Joystick2Buttons15 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons15 & state.Value == 0)
                        Joystick2Buttons15 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons16 & state.Value == 128)
                        Joystick2Buttons16 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons16 & state.Value == 0)
                        Joystick2Buttons16 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons17 & state.Value == 128)
                        Joystick2Buttons17 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons17 & state.Value == 0)
                        Joystick2Buttons17 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons18 & state.Value == 128)
                        Joystick2Buttons18 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons18 & state.Value == 0)
                        Joystick2Buttons18 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons19 & state.Value == 128)
                        Joystick2Buttons19 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons19 & state.Value == 0)
                        Joystick2Buttons19 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons20 & state.Value == 128)
                        Joystick2Buttons20 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons20 & state.Value == 0)
                        Joystick2Buttons20 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons21 & state.Value == 128)
                        Joystick2Buttons21 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons21 & state.Value == 0)
                        Joystick2Buttons21 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons22 & state.Value == 128)
                        Joystick2Buttons22 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons22 & state.Value == 0)
                        Joystick2Buttons22 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons23 & state.Value == 128)
                        Joystick2Buttons23 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons23 & state.Value == 0)
                        Joystick2Buttons23 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons24 & state.Value == 128)
                        Joystick2Buttons24 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons24 & state.Value == 0)
                        Joystick2Buttons24 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons25 & state.Value == 128)
                        Joystick2Buttons25 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons25 & state.Value == 0)
                        Joystick2Buttons25 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons26 & state.Value == 128)
                        Joystick2Buttons26 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons26 & state.Value == 0)
                        Joystick2Buttons26 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons27 & state.Value == 128)
                        Joystick2Buttons27 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons27 & state.Value == 0)
                        Joystick2Buttons27 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons28 & state.Value == 128)
                        Joystick2Buttons28 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons28 & state.Value == 0)
                        Joystick2Buttons28 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons29 & state.Value == 128)
                        Joystick2Buttons29 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons29 & state.Value == 0)
                        Joystick2Buttons29 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons30 & state.Value == 128)
                        Joystick2Buttons30 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons30 & state.Value == 0)
                        Joystick2Buttons30 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons31 & state.Value == 128)
                        Joystick2Buttons31 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons31 & state.Value == 0)
                        Joystick2Buttons31 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons32 & state.Value == 128)
                        Joystick2Buttons32 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons32 & state.Value == 0)
                        Joystick2Buttons32 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons33 & state.Value == 128)
                        Joystick2Buttons33 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons33 & state.Value == 0)
                        Joystick2Buttons33 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons34 & state.Value == 128)
                        Joystick2Buttons34 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons34 & state.Value == 0)
                        Joystick2Buttons34 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons35 & state.Value == 128)
                        Joystick2Buttons35 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons35 & state.Value == 0)
                        Joystick2Buttons35 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons36 & state.Value == 128)
                        Joystick2Buttons36 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons36 & state.Value == 0)
                        Joystick2Buttons36 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons37 & state.Value == 128)
                        Joystick2Buttons37 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons37 & state.Value == 0)
                        Joystick2Buttons37 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons38 & state.Value == 128)
                        Joystick2Buttons38 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons38 & state.Value == 0)
                        Joystick2Buttons38 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons39 & state.Value == 128)
                        Joystick2Buttons39 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons39 & state.Value == 0)
                        Joystick2Buttons39 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons40 & state.Value == 128)
                        Joystick2Buttons40 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons40 & state.Value == 0)
                        Joystick2Buttons40 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons41 & state.Value == 128)
                        Joystick2Buttons41 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons41 & state.Value == 0)
                        Joystick2Buttons41 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons42 & state.Value == 128)
                        Joystick2Buttons42 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons42 & state.Value == 0)
                        Joystick2Buttons42 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons43 & state.Value == 128)
                        Joystick2Buttons43 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons43 & state.Value == 0)
                        Joystick2Buttons43 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons44 & state.Value == 128)
                        Joystick2Buttons44 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons44 & state.Value == 0)
                        Joystick2Buttons44 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons45 & state.Value == 128)
                        Joystick2Buttons45 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons45 & state.Value == 0)
                        Joystick2Buttons45 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons46 & state.Value == 128)
                        Joystick2Buttons46 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons46 & state.Value == 0)
                        Joystick2Buttons46 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons47 & state.Value == 128)
                        Joystick2Buttons47 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons47 & state.Value == 0)
                        Joystick2Buttons47 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons48 & state.Value == 128)
                        Joystick2Buttons48 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons48 & state.Value == 0)
                        Joystick2Buttons48 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons49 & state.Value == 128)
                        Joystick2Buttons49 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons49 & state.Value == 0)
                        Joystick2Buttons49 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons50 & state.Value == 128)
                        Joystick2Buttons50 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons50 & state.Value == 0)
                        Joystick2Buttons50 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons51 & state.Value == 128)
                        Joystick2Buttons51 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons51 & state.Value == 0)
                        Joystick2Buttons51 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons52 & state.Value == 128)
                        Joystick2Buttons52 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons52 & state.Value == 0)
                        Joystick2Buttons52 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons53 & state.Value == 128)
                        Joystick2Buttons53 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons53 & state.Value == 0)
                        Joystick2Buttons53 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons54 & state.Value == 128)
                        Joystick2Buttons54 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons54 & state.Value == 0)
                        Joystick2Buttons54 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons55 & state.Value == 128)
                        Joystick2Buttons55 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons55 & state.Value == 0)
                        Joystick2Buttons55 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons56 & state.Value == 128)
                        Joystick2Buttons56 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons56 & state.Value == 0)
                        Joystick2Buttons56 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons57 & state.Value == 128)
                        Joystick2Buttons57 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons57 & state.Value == 0)
                        Joystick2Buttons57 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons58 & state.Value == 128)
                        Joystick2Buttons58 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons58 & state.Value == 0)
                        Joystick2Buttons58 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons59 & state.Value == 128)
                        Joystick2Buttons59 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons59 & state.Value == 0)
                        Joystick2Buttons59 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons60 & state.Value == 128)
                        Joystick2Buttons60 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons60 & state.Value == 0)
                        Joystick2Buttons60 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons61 & state.Value == 128)
                        Joystick2Buttons61 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons61 & state.Value == 0)
                        Joystick2Buttons61 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons62 & state.Value == 128)
                        Joystick2Buttons62 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons62 & state.Value == 0)
                        Joystick2Buttons62 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons63 & state.Value == 128)
                        Joystick2Buttons63 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons63 & state.Value == 0)
                        Joystick2Buttons63 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons64 & state.Value == 128)
                        Joystick2Buttons64 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons64 & state.Value == 0)
                        Joystick2Buttons64 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons65 & state.Value == 128)
                        Joystick2Buttons65 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons65 & state.Value == 0)
                        Joystick2Buttons65 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons66 & state.Value == 128)
                        Joystick2Buttons66 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons66 & state.Value == 0)
                        Joystick2Buttons66 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons67 & state.Value == 128)
                        Joystick2Buttons67 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons67 & state.Value == 0)
                        Joystick2Buttons67 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons68 & state.Value == 128)
                        Joystick2Buttons68 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons68 & state.Value == 0)
                        Joystick2Buttons68 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons69 & state.Value == 128)
                        Joystick2Buttons69 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons69 & state.Value == 0)
                        Joystick2Buttons69 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons70 & state.Value == 128)
                        Joystick2Buttons70 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons70 & state.Value == 0)
                        Joystick2Buttons70 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons71 & state.Value == 128)
                        Joystick2Buttons71 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons71 & state.Value == 0)
                        Joystick2Buttons71 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons72 & state.Value == 128)
                        Joystick2Buttons72 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons72 & state.Value == 0)
                        Joystick2Buttons72 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons73 & state.Value == 128)
                        Joystick2Buttons73 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons73 & state.Value == 0)
                        Joystick2Buttons73 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons74 & state.Value == 128)
                        Joystick2Buttons74 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons74 & state.Value == 0)
                        Joystick2Buttons74 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons75 & state.Value == 128)
                        Joystick2Buttons75 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons75 & state.Value == 0)
                        Joystick2Buttons75 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons76 & state.Value == 128)
                        Joystick2Buttons76 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons76 & state.Value == 0)
                        Joystick2Buttons76 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons77 & state.Value == 128)
                        Joystick2Buttons77 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons77 & state.Value == 0)
                        Joystick2Buttons77 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons78 & state.Value == 128)
                        Joystick2Buttons78 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons78 & state.Value == 0)
                        Joystick2Buttons78 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons79 & state.Value == 128)
                        Joystick2Buttons79 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons79 & state.Value == 0)
                        Joystick2Buttons79 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons80 & state.Value == 128)
                        Joystick2Buttons80 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons80 & state.Value == 0)
                        Joystick2Buttons80 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons81 & state.Value == 128)
                        Joystick2Buttons81 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons81 & state.Value == 0)
                        Joystick2Buttons81 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons82 & state.Value == 128)
                        Joystick2Buttons82 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons82 & state.Value == 0)
                        Joystick2Buttons82 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons83 & state.Value == 128)
                        Joystick2Buttons83 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons83 & state.Value == 0)
                        Joystick2Buttons83 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons84 & state.Value == 128)
                        Joystick2Buttons84 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons84 & state.Value == 0)
                        Joystick2Buttons84 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons85 & state.Value == 128)
                        Joystick2Buttons85 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons85 & state.Value == 0)
                        Joystick2Buttons85 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons86 & state.Value == 128)
                        Joystick2Buttons86 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons86 & state.Value == 0)
                        Joystick2Buttons86 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons87 & state.Value == 128)
                        Joystick2Buttons87 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons87 & state.Value == 0)
                        Joystick2Buttons87 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons88 & state.Value == 128)
                        Joystick2Buttons88 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons88 & state.Value == 0)
                        Joystick2Buttons88 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons89 & state.Value == 128)
                        Joystick2Buttons89 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons89 & state.Value == 0)
                        Joystick2Buttons89 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons90 & state.Value == 128)
                        Joystick2Buttons90 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons90 & state.Value == 0)
                        Joystick2Buttons90 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons91 & state.Value == 128)
                        Joystick2Buttons91 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons91 & state.Value == 0)
                        Joystick2Buttons91 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons92 & state.Value == 128)
                        Joystick2Buttons92 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons92 & state.Value == 0)
                        Joystick2Buttons92 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons93 & state.Value == 128)
                        Joystick2Buttons93 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons93 & state.Value == 0)
                        Joystick2Buttons93 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons94 & state.Value == 128)
                        Joystick2Buttons94 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons94 & state.Value == 0)
                        Joystick2Buttons94 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons95 & state.Value == 128)
                        Joystick2Buttons95 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons95 & state.Value == 0)
                        Joystick2Buttons95 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons96 & state.Value == 128)
                        Joystick2Buttons96 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons96 & state.Value == 0)
                        Joystick2Buttons96 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons97 & state.Value == 128)
                        Joystick2Buttons97 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons97 & state.Value == 0)
                        Joystick2Buttons97 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons98 & state.Value == 128)
                        Joystick2Buttons98 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons98 & state.Value == 0)
                        Joystick2Buttons98 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons99 & state.Value == 128)
                        Joystick2Buttons99 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons99 & state.Value == 0)
                        Joystick2Buttons99 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons100 & state.Value == 128)
                        Joystick2Buttons100 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons100 & state.Value == 0)
                        Joystick2Buttons100 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons101 & state.Value == 128)
                        Joystick2Buttons101 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons101 & state.Value == 0)
                        Joystick2Buttons101 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons102 & state.Value == 128)
                        Joystick2Buttons102 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons102 & state.Value == 0)
                        Joystick2Buttons102 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons103 & state.Value == 128)
                        Joystick2Buttons103 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons103 & state.Value == 0)
                        Joystick2Buttons103 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons104 & state.Value == 128)
                        Joystick2Buttons104 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons104 & state.Value == 0)
                        Joystick2Buttons104 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons105 & state.Value == 128)
                        Joystick2Buttons105 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons105 & state.Value == 0)
                        Joystick2Buttons105 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons106 & state.Value == 128)
                        Joystick2Buttons106 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons106 & state.Value == 0)
                        Joystick2Buttons106 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons107 & state.Value == 128)
                        Joystick2Buttons107 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons107 & state.Value == 0)
                        Joystick2Buttons107 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons108 & state.Value == 128)
                        Joystick2Buttons108 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons108 & state.Value == 0)
                        Joystick2Buttons108 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons109 & state.Value == 128)
                        Joystick2Buttons109 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons109 & state.Value == 0)
                        Joystick2Buttons109 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons110 & state.Value == 128)
                        Joystick2Buttons110 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons110 & state.Value == 0)
                        Joystick2Buttons110 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons111 & state.Value == 128)
                        Joystick2Buttons111 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons111 & state.Value == 0)
                        Joystick2Buttons111 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons112 & state.Value == 128)
                        Joystick2Buttons112 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons112 & state.Value == 0)
                        Joystick2Buttons112 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons113 & state.Value == 128)
                        Joystick2Buttons113 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons113 & state.Value == 0)
                        Joystick2Buttons113 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons114 & state.Value == 128)
                        Joystick2Buttons114 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons114 & state.Value == 0)
                        Joystick2Buttons114 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons115 & state.Value == 128)
                        Joystick2Buttons115 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons115 & state.Value == 0)
                        Joystick2Buttons115 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons116 & state.Value == 128)
                        Joystick2Buttons116 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons116 & state.Value == 0)
                        Joystick2Buttons116 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons117 & state.Value == 128)
                        Joystick2Buttons117 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons117 & state.Value == 0)
                        Joystick2Buttons117 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons118 & state.Value == 128)
                        Joystick2Buttons118 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons118 & state.Value == 0)
                        Joystick2Buttons118 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons119 & state.Value == 128)
                        Joystick2Buttons119 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons119 & state.Value == 0)
                        Joystick2Buttons119 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons120 & state.Value == 128)
                        Joystick2Buttons120 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons120 & state.Value == 0)
                        Joystick2Buttons120 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons121 & state.Value == 128)
                        Joystick2Buttons121 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons121 & state.Value == 0)
                        Joystick2Buttons121 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons122 & state.Value == 128)
                        Joystick2Buttons122 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons122 & state.Value == 0)
                        Joystick2Buttons122 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons123 & state.Value == 128)
                        Joystick2Buttons123 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons123 & state.Value == 0)
                        Joystick2Buttons123 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons124 & state.Value == 128)
                        Joystick2Buttons124 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons124 & state.Value == 0)
                        Joystick2Buttons124 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons125 & state.Value == 128)
                        Joystick2Buttons125 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons125 & state.Value == 0)
                        Joystick2Buttons125 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons126 & state.Value == 128)
                        Joystick2Buttons126 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons126 & state.Value == 0)
                        Joystick2Buttons126 = false;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons127 & state.Value == 128)
                        Joystick2Buttons127 = true;
                    if (inc == 1 & state.Offset == JoystickOffset.Buttons127 & state.Value == 0)
                        Joystick2Buttons127 = false;
                }
            }
        }
        private static Mouse[] mouse = new Mouse[] { null, null };
        private static Guid[] mouseGuid = new Guid[] { Guid.Empty, Guid.Empty };
        private static int mnum = 0;
        public static bool Mouse1Buttons0;
        public static bool Mouse1Buttons1;
        public static bool Mouse1Buttons2;
        public static bool Mouse1Buttons3;
        public static bool Mouse1Buttons4;
        public static bool Mouse1Buttons5;
        public static bool Mouse1Buttons6;
        public static bool Mouse1Buttons7;
        public static int Mouse1AxisX;
        public static int Mouse1AxisY;
        public static int Mouse1AxisZ;
        public static bool Mouse2Buttons0;
        public static bool Mouse2Buttons1;
        public static bool Mouse2Buttons2;
        public static bool Mouse2Buttons3;
        public static bool Mouse2Buttons4;
        public static bool Mouse2Buttons5;
        public static bool Mouse2Buttons6;
        public static bool Mouse2Buttons7;
        public static int Mouse2AxisX;
        public static int Mouse2AxisY;
        public static int Mouse2AxisZ;
        public bool MouseInputHookConnect()
        {
            try
            {
                directInput = new DirectInput();
                mouse = new Mouse[] { null, null };
                mouseGuid = new Guid[] { Guid.Empty, Guid.Empty };
                mnum = 0;
                foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Mouse, DeviceEnumerationFlags.AllDevices))
                {
                    mouseGuid[mnum] = deviceInstance.InstanceGuid;
                    mnum++;
                    if (mnum >= 2)
                        break;
                }
            }
            catch { }
            if (mouseGuid[0] == Guid.Empty)
            {
                return false;
            }
            else
            {
                for (int inc = 0; inc < mnum; inc++)
                {
                    mouse[inc] = new Mouse(directInput);
                    mouse[inc].Properties.BufferSize = 128;
                    mouse[inc].Acquire();
                }
                return true;
            }
        }
        public void MouseInputProcess()
        {
            for (int inc = 0; inc < mnum; inc++)
            {
                mouse[inc].Poll();
                var datas = mouse[inc].GetBufferedData();
                foreach (var state in datas)
                {
                    if (inc == 0 & state.Offset == MouseOffset.X)
                        Mouse1AxisX = state.Value;
                    if (inc == 0 & state.Offset == MouseOffset.Y)
                        Mouse1AxisY = state.Value;
                    if (inc == 0 & state.Offset == MouseOffset.Z)
                        Mouse1AxisZ = state.Value;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons0 & state.Value == 128)
                        Mouse1Buttons0 = true;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons0 & state.Value == 0)
                        Mouse1Buttons0 = false;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons1 & state.Value == 128)
                        Mouse1Buttons1 = true;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons1 & state.Value == 0)
                        Mouse1Buttons1 = false;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons2 & state.Value == 128)
                        Mouse1Buttons2 = true;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons2 & state.Value == 0)
                        Mouse1Buttons2 = false;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons3 & state.Value == 128)
                        Mouse1Buttons3 = true;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons3 & state.Value == 0)
                        Mouse1Buttons3 = false;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons4 & state.Value == 128)
                        Mouse1Buttons4 = true;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons4 & state.Value == 0)
                        Mouse1Buttons4 = false;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons5 & state.Value == 128)
                        Mouse1Buttons5 = true;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons5 & state.Value == 0)
                        Mouse1Buttons5 = false;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons6 & state.Value == 128)
                        Mouse1Buttons6 = true;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons6 & state.Value == 0)
                        Mouse1Buttons6 = false;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons7 & state.Value == 128)
                        Mouse1Buttons7 = true;
                    if (inc == 0 & state.Offset == MouseOffset.Buttons7 & state.Value == 0)
                        Mouse1Buttons7 = false;
                    if (inc == 1 & state.Offset == MouseOffset.X)
                        Mouse2AxisX = state.Value;
                    if (inc == 1 & state.Offset == MouseOffset.Y)
                        Mouse2AxisY = state.Value;
                    if (inc == 1 & state.Offset == MouseOffset.Z)
                        Mouse2AxisZ = state.Value;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons0 & state.Value == 128)
                        Mouse2Buttons0 = true;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons0 & state.Value == 0)
                        Mouse2Buttons0 = false;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons1 & state.Value == 128)
                        Mouse2Buttons1 = true;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons1 & state.Value == 0)
                        Mouse2Buttons1 = false;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons2 & state.Value == 128)
                        Mouse2Buttons2 = true;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons2 & state.Value == 0)
                        Mouse2Buttons2 = false;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons3 & state.Value == 128)
                        Mouse2Buttons3 = true;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons3 & state.Value == 0)
                        Mouse2Buttons3 = false;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons4 & state.Value == 128)
                        Mouse2Buttons4 = true;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons4 & state.Value == 0)
                        Mouse2Buttons4 = false;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons5 & state.Value == 128)
                        Mouse2Buttons5 = true;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons5 & state.Value == 0)
                        Mouse2Buttons5 = false;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons6 & state.Value == 128)
                        Mouse2Buttons6 = true;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons6 & state.Value == 0)
                        Mouse2Buttons6 = false;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons7 & state.Value == 128)
                        Mouse2Buttons7 = true;
                    if (inc == 1 & state.Offset == MouseOffset.Buttons7 & state.Value == 0)
                        Mouse2Buttons7 = false;
                }
            }
        }
        private static Keyboard[] keyboard = new Keyboard[] { null, null };
        private static Guid[] keyboardGuid = new Guid[] { Guid.Empty, Guid.Empty };
        private static int knum = 0;
        public static bool Keyboard1KeyEscape;
        public static bool Keyboard1KeyD1;
        public static bool Keyboard1KeyD2;
        public static bool Keyboard1KeyD3;
        public static bool Keyboard1KeyD4;
        public static bool Keyboard1KeyD5;
        public static bool Keyboard1KeyD6;
        public static bool Keyboard1KeyD7;
        public static bool Keyboard1KeyD8;
        public static bool Keyboard1KeyD9;
        public static bool Keyboard1KeyD0;
        public static bool Keyboard1KeyMinus;
        public static bool Keyboard1KeyEquals;
        public static bool Keyboard1KeyBack;
        public static bool Keyboard1KeyTab;
        public static bool Keyboard1KeyQ;
        public static bool Keyboard1KeyW;
        public static bool Keyboard1KeyE;
        public static bool Keyboard1KeyR;
        public static bool Keyboard1KeyT;
        public static bool Keyboard1KeyY;
        public static bool Keyboard1KeyU;
        public static bool Keyboard1KeyI;
        public static bool Keyboard1KeyO;
        public static bool Keyboard1KeyP;
        public static bool Keyboard1KeyLeftBracket;
        public static bool Keyboard1KeyRightBracket;
        public static bool Keyboard1KeyReturn;
        public static bool Keyboard1KeyLeftControl;
        public static bool Keyboard1KeyA;
        public static bool Keyboard1KeyS;
        public static bool Keyboard1KeyD;
        public static bool Keyboard1KeyF;
        public static bool Keyboard1KeyG;
        public static bool Keyboard1KeyH;
        public static bool Keyboard1KeyJ;
        public static bool Keyboard1KeyK;
        public static bool Keyboard1KeyL;
        public static bool Keyboard1KeySemicolon;
        public static bool Keyboard1KeyApostrophe;
        public static bool Keyboard1KeyGrave;
        public static bool Keyboard1KeyLeftShift;
        public static bool Keyboard1KeyBackslash;
        public static bool Keyboard1KeyZ;
        public static bool Keyboard1KeyX;
        public static bool Keyboard1KeyC;
        public static bool Keyboard1KeyV;
        public static bool Keyboard1KeyB;
        public static bool Keyboard1KeyN;
        public static bool Keyboard1KeyM;
        public static bool Keyboard1KeyComma;
        public static bool Keyboard1KeyPeriod;
        public static bool Keyboard1KeySlash;
        public static bool Keyboard1KeyRightShift;
        public static bool Keyboard1KeyMultiply;
        public static bool Keyboard1KeyLeftAlt;
        public static bool Keyboard1KeySpace;
        public static bool Keyboard1KeyCapital;
        public static bool Keyboard1KeyF1;
        public static bool Keyboard1KeyF2;
        public static bool Keyboard1KeyF3;
        public static bool Keyboard1KeyF4;
        public static bool Keyboard1KeyF5;
        public static bool Keyboard1KeyF6;
        public static bool Keyboard1KeyF7;
        public static bool Keyboard1KeyF8;
        public static bool Keyboard1KeyF9;
        public static bool Keyboard1KeyF10;
        public static bool Keyboard1KeyNumberLock;
        public static bool Keyboard1KeyScrollLock;
        public static bool Keyboard1KeyNumberPad7;
        public static bool Keyboard1KeyNumberPad8;
        public static bool Keyboard1KeyNumberPad9;
        public static bool Keyboard1KeySubtract;
        public static bool Keyboard1KeyNumberPad4;
        public static bool Keyboard1KeyNumberPad5;
        public static bool Keyboard1KeyNumberPad6;
        public static bool Keyboard1KeyAdd;
        public static bool Keyboard1KeyNumberPad1;
        public static bool Keyboard1KeyNumberPad2;
        public static bool Keyboard1KeyNumberPad3;
        public static bool Keyboard1KeyNumberPad0;
        public static bool Keyboard1KeyDecimal;
        public static bool Keyboard1KeyOem102;
        public static bool Keyboard1KeyF11;
        public static bool Keyboard1KeyF12;
        public static bool Keyboard1KeyF13;
        public static bool Keyboard1KeyF14;
        public static bool Keyboard1KeyF15;
        public static bool Keyboard1KeyKana;
        public static bool Keyboard1KeyAbntC1;
        public static bool Keyboard1KeyConvert;
        public static bool Keyboard1KeyNoConvert;
        public static bool Keyboard1KeyYen;
        public static bool Keyboard1KeyAbntC2;
        public static bool Keyboard1KeyNumberPadEquals;
        public static bool Keyboard1KeyPreviousTrack;
        public static bool Keyboard1KeyAT;
        public static bool Keyboard1KeyColon;
        public static bool Keyboard1KeyUnderline;
        public static bool Keyboard1KeyKanji;
        public static bool Keyboard1KeyStop;
        public static bool Keyboard1KeyAX;
        public static bool Keyboard1KeyUnlabeled;
        public static bool Keyboard1KeyNextTrack;
        public static bool Keyboard1KeyNumberPadEnter;
        public static bool Keyboard1KeyRightControl;
        public static bool Keyboard1KeyMute;
        public static bool Keyboard1KeyCalculator;
        public static bool Keyboard1KeyPlayPause;
        public static bool Keyboard1KeyMediaStop;
        public static bool Keyboard1KeyVolumeDown;
        public static bool Keyboard1KeyVolumeUp;
        public static bool Keyboard1KeyWebHome;
        public static bool Keyboard1KeyNumberPadComma;
        public static bool Keyboard1KeyDivide;
        public static bool Keyboard1KeyPrintScreen;
        public static bool Keyboard1KeyRightAlt;
        public static bool Keyboard1KeyPause;
        public static bool Keyboard1KeyHome;
        public static bool Keyboard1KeyUp;
        public static bool Keyboard1KeyPageUp;
        public static bool Keyboard1KeyLeft;
        public static bool Keyboard1KeyRight;
        public static bool Keyboard1KeyEnd;
        public static bool Keyboard1KeyDown;
        public static bool Keyboard1KeyPageDown;
        public static bool Keyboard1KeyInsert;
        public static bool Keyboard1KeyDelete;
        public static bool Keyboard1KeyLeftWindowsKey;
        public static bool Keyboard1KeyRightWindowsKey;
        public static bool Keyboard1KeyApplications;
        public static bool Keyboard1KeyPower;
        public static bool Keyboard1KeySleep;
        public static bool Keyboard1KeyWake;
        public static bool Keyboard1KeyWebSearch;
        public static bool Keyboard1KeyWebFavorites;
        public static bool Keyboard1KeyWebRefresh;
        public static bool Keyboard1KeyWebStop;
        public static bool Keyboard1KeyWebForward;
        public static bool Keyboard1KeyWebBack;
        public static bool Keyboard1KeyMyComputer;
        public static bool Keyboard1KeyMail;
        public static bool Keyboard1KeyMediaSelect;
        public static bool Keyboard1KeyUnknown;
        public static bool Keyboard2KeyEscape;
        public static bool Keyboard2KeyD1;
        public static bool Keyboard2KeyD2;
        public static bool Keyboard2KeyD3;
        public static bool Keyboard2KeyD4;
        public static bool Keyboard2KeyD5;
        public static bool Keyboard2KeyD6;
        public static bool Keyboard2KeyD7;
        public static bool Keyboard2KeyD8;
        public static bool Keyboard2KeyD9;
        public static bool Keyboard2KeyD0;
        public static bool Keyboard2KeyMinus;
        public static bool Keyboard2KeyEquals;
        public static bool Keyboard2KeyBack;
        public static bool Keyboard2KeyTab;
        public static bool Keyboard2KeyQ;
        public static bool Keyboard2KeyW;
        public static bool Keyboard2KeyE;
        public static bool Keyboard2KeyR;
        public static bool Keyboard2KeyT;
        public static bool Keyboard2KeyY;
        public static bool Keyboard2KeyU;
        public static bool Keyboard2KeyI;
        public static bool Keyboard2KeyO;
        public static bool Keyboard2KeyP;
        public static bool Keyboard2KeyLeftBracket;
        public static bool Keyboard2KeyRightBracket;
        public static bool Keyboard2KeyReturn;
        public static bool Keyboard2KeyLeftControl;
        public static bool Keyboard2KeyA;
        public static bool Keyboard2KeyS;
        public static bool Keyboard2KeyD;
        public static bool Keyboard2KeyF;
        public static bool Keyboard2KeyG;
        public static bool Keyboard2KeyH;
        public static bool Keyboard2KeyJ;
        public static bool Keyboard2KeyK;
        public static bool Keyboard2KeyL;
        public static bool Keyboard2KeySemicolon;
        public static bool Keyboard2KeyApostrophe;
        public static bool Keyboard2KeyGrave;
        public static bool Keyboard2KeyLeftShift;
        public static bool Keyboard2KeyBackslash;
        public static bool Keyboard2KeyZ;
        public static bool Keyboard2KeyX;
        public static bool Keyboard2KeyC;
        public static bool Keyboard2KeyV;
        public static bool Keyboard2KeyB;
        public static bool Keyboard2KeyN;
        public static bool Keyboard2KeyM;
        public static bool Keyboard2KeyComma;
        public static bool Keyboard2KeyPeriod;
        public static bool Keyboard2KeySlash;
        public static bool Keyboard2KeyRightShift;
        public static bool Keyboard2KeyMultiply;
        public static bool Keyboard2KeyLeftAlt;
        public static bool Keyboard2KeySpace;
        public static bool Keyboard2KeyCapital;
        public static bool Keyboard2KeyF1;
        public static bool Keyboard2KeyF2;
        public static bool Keyboard2KeyF3;
        public static bool Keyboard2KeyF4;
        public static bool Keyboard2KeyF5;
        public static bool Keyboard2KeyF6;
        public static bool Keyboard2KeyF7;
        public static bool Keyboard2KeyF8;
        public static bool Keyboard2KeyF9;
        public static bool Keyboard2KeyF10;
        public static bool Keyboard2KeyNumberLock;
        public static bool Keyboard2KeyScrollLock;
        public static bool Keyboard2KeyNumberPad7;
        public static bool Keyboard2KeyNumberPad8;
        public static bool Keyboard2KeyNumberPad9;
        public static bool Keyboard2KeySubtract;
        public static bool Keyboard2KeyNumberPad4;
        public static bool Keyboard2KeyNumberPad5;
        public static bool Keyboard2KeyNumberPad6;
        public static bool Keyboard2KeyAdd;
        public static bool Keyboard2KeyNumberPad1;
        public static bool Keyboard2KeyNumberPad2;
        public static bool Keyboard2KeyNumberPad3;
        public static bool Keyboard2KeyNumberPad0;
        public static bool Keyboard2KeyDecimal;
        public static bool Keyboard2KeyOem102;
        public static bool Keyboard2KeyF11;
        public static bool Keyboard2KeyF12;
        public static bool Keyboard2KeyF13;
        public static bool Keyboard2KeyF14;
        public static bool Keyboard2KeyF15;
        public static bool Keyboard2KeyKana;
        public static bool Keyboard2KeyAbntC1;
        public static bool Keyboard2KeyConvert;
        public static bool Keyboard2KeyNoConvert;
        public static bool Keyboard2KeyYen;
        public static bool Keyboard2KeyAbntC2;
        public static bool Keyboard2KeyNumberPadEquals;
        public static bool Keyboard2KeyPreviousTrack;
        public static bool Keyboard2KeyAT;
        public static bool Keyboard2KeyColon;
        public static bool Keyboard2KeyUnderline;
        public static bool Keyboard2KeyKanji;
        public static bool Keyboard2KeyStop;
        public static bool Keyboard2KeyAX;
        public static bool Keyboard2KeyUnlabeled;
        public static bool Keyboard2KeyNextTrack;
        public static bool Keyboard2KeyNumberPadEnter;
        public static bool Keyboard2KeyRightControl;
        public static bool Keyboard2KeyMute;
        public static bool Keyboard2KeyCalculator;
        public static bool Keyboard2KeyPlayPause;
        public static bool Keyboard2KeyMediaStop;
        public static bool Keyboard2KeyVolumeDown;
        public static bool Keyboard2KeyVolumeUp;
        public static bool Keyboard2KeyWebHome;
        public static bool Keyboard2KeyNumberPadComma;
        public static bool Keyboard2KeyDivide;
        public static bool Keyboard2KeyPrintScreen;
        public static bool Keyboard2KeyRightAlt;
        public static bool Keyboard2KeyPause;
        public static bool Keyboard2KeyHome;
        public static bool Keyboard2KeyUp;
        public static bool Keyboard2KeyPageUp;
        public static bool Keyboard2KeyLeft;
        public static bool Keyboard2KeyRight;
        public static bool Keyboard2KeyEnd;
        public static bool Keyboard2KeyDown;
        public static bool Keyboard2KeyPageDown;
        public static bool Keyboard2KeyInsert;
        public static bool Keyboard2KeyDelete;
        public static bool Keyboard2KeyLeftWindowsKey;
        public static bool Keyboard2KeyRightWindowsKey;
        public static bool Keyboard2KeyApplications;
        public static bool Keyboard2KeyPower;
        public static bool Keyboard2KeySleep;
        public static bool Keyboard2KeyWake;
        public static bool Keyboard2KeyWebSearch;
        public static bool Keyboard2KeyWebFavorites;
        public static bool Keyboard2KeyWebRefresh;
        public static bool Keyboard2KeyWebStop;
        public static bool Keyboard2KeyWebForward;
        public static bool Keyboard2KeyWebBack;
        public static bool Keyboard2KeyMyComputer;
        public static bool Keyboard2KeyMail;
        public static bool Keyboard2KeyMediaSelect;
        public static bool Keyboard2KeyUnknown;
        public bool KeyboardInputHookConnect()
        {
            try
            {
                directInput = new DirectInput();
                keyboard = new Keyboard[] { null, null };
                keyboardGuid = new Guid[] { Guid.Empty, Guid.Empty };
                knum = 0;
                foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Keyboard, DeviceEnumerationFlags.AllDevices))
                {
                    keyboardGuid[knum] = deviceInstance.InstanceGuid;
                    knum++;
                    if (knum >= 2)
                        break;
                }
            }
            catch { }
            if (keyboardGuid[0] == Guid.Empty)
            {
                return false;
            }
            else
            {
                for (int inc = 0; inc < knum; inc++)
                {
                    keyboard[inc] = new Keyboard(directInput);
                    keyboard[inc].Properties.BufferSize = 128;
                    keyboard[inc].Acquire();
                }
                return true;
            }
        }
        public void KeyboardInputProcess()
        {
            for (int inc = 0; inc < knum; inc++)
            {
                keyboard[inc].Poll();
                var datas = keyboard[inc].GetBufferedData();
                foreach (var state in datas)
                {
                    if (inc == 0 & state.IsPressed & state.Key == Key.Escape)
                        Keyboard1KeyEscape = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Escape)
                        Keyboard1KeyEscape = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D1)
                        Keyboard1KeyD1 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D1)
                        Keyboard1KeyD1 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D2)
                        Keyboard1KeyD2 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D2)
                        Keyboard1KeyD2 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D3)
                        Keyboard1KeyD3 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D3)
                        Keyboard1KeyD3 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D4)
                        Keyboard1KeyD4 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D4)
                        Keyboard1KeyD4 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D5)
                        Keyboard1KeyD5 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D5)
                        Keyboard1KeyD5 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D6)
                        Keyboard1KeyD6 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D6)
                        Keyboard1KeyD6 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D7)
                        Keyboard1KeyD7 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D7)
                        Keyboard1KeyD7 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D8)
                        Keyboard1KeyD8 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D8)
                        Keyboard1KeyD8 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D9)
                        Keyboard1KeyD9 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D9)
                        Keyboard1KeyD9 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D0)
                        Keyboard1KeyD0 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D0)
                        Keyboard1KeyD0 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Minus)
                        Keyboard1KeyMinus = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Minus)
                        Keyboard1KeyMinus = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Equals)
                        Keyboard1KeyEquals = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Equals)
                        Keyboard1KeyEquals = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Back)
                        Keyboard1KeyBack = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Back)
                        Keyboard1KeyBack = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Tab)
                        Keyboard1KeyTab = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Tab)
                        Keyboard1KeyTab = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Q)
                        Keyboard1KeyQ = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Q)
                        Keyboard1KeyQ = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.W)
                        Keyboard1KeyW = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.W)
                        Keyboard1KeyW = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.E)
                        Keyboard1KeyE = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.E)
                        Keyboard1KeyE = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.R)
                        Keyboard1KeyR = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.R)
                        Keyboard1KeyR = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.T)
                        Keyboard1KeyT = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.T)
                        Keyboard1KeyT = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Y)
                        Keyboard1KeyY = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Y)
                        Keyboard1KeyY = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.U)
                        Keyboard1KeyU = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.U)
                        Keyboard1KeyU = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.I)
                        Keyboard1KeyI = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.I)
                        Keyboard1KeyI = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.O)
                        Keyboard1KeyO = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.O)
                        Keyboard1KeyO = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.P)
                        Keyboard1KeyP = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.P)
                        Keyboard1KeyP = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.LeftBracket)
                        Keyboard1KeyLeftBracket = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.LeftBracket)
                        Keyboard1KeyLeftBracket = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.RightBracket)
                        Keyboard1KeyRightBracket = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.RightBracket)
                        Keyboard1KeyRightBracket = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Return)
                        Keyboard1KeyReturn = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Return)
                        Keyboard1KeyReturn = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.LeftControl)
                        Keyboard1KeyLeftControl = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.LeftControl)
                        Keyboard1KeyLeftControl = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.A)
                        Keyboard1KeyA = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.A)
                        Keyboard1KeyA = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.S)
                        Keyboard1KeyS = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.S)
                        Keyboard1KeyS = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.D)
                        Keyboard1KeyD = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.D)
                        Keyboard1KeyD = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F)
                        Keyboard1KeyF = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F)
                        Keyboard1KeyF = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.G)
                        Keyboard1KeyG = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.G)
                        Keyboard1KeyG = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.H)
                        Keyboard1KeyH = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.H)
                        Keyboard1KeyH = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.J)
                        Keyboard1KeyJ = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.J)
                        Keyboard1KeyJ = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.K)
                        Keyboard1KeyK = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.K)
                        Keyboard1KeyK = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.L)
                        Keyboard1KeyL = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.L)
                        Keyboard1KeyL = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Semicolon)
                        Keyboard1KeySemicolon = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Semicolon)
                        Keyboard1KeySemicolon = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Apostrophe)
                        Keyboard1KeyApostrophe = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Apostrophe)
                        Keyboard1KeyApostrophe = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Grave)
                        Keyboard1KeyGrave = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Grave)
                        Keyboard1KeyGrave = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.LeftShift)
                        Keyboard1KeyLeftShift = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.LeftShift)
                        Keyboard1KeyLeftShift = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Backslash)
                        Keyboard1KeyBackslash = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Backslash)
                        Keyboard1KeyBackslash = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Z)
                        Keyboard1KeyZ = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Z)
                        Keyboard1KeyZ = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.X)
                        Keyboard1KeyX = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.X)
                        Keyboard1KeyX = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.C)
                        Keyboard1KeyC = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.C)
                        Keyboard1KeyC = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.V)
                        Keyboard1KeyV = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.V)
                        Keyboard1KeyV = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.B)
                        Keyboard1KeyB = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.B)
                        Keyboard1KeyB = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.N)
                        Keyboard1KeyN = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.N)
                        Keyboard1KeyN = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.M)
                        Keyboard1KeyM = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.M)
                        Keyboard1KeyM = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Comma)
                        Keyboard1KeyComma = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Comma)
                        Keyboard1KeyComma = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Period)
                        Keyboard1KeyPeriod = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Period)
                        Keyboard1KeyPeriod = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Slash)
                        Keyboard1KeySlash = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Slash)
                        Keyboard1KeySlash = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.RightShift)
                        Keyboard1KeyRightShift = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.RightShift)
                        Keyboard1KeyRightShift = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Multiply)
                        Keyboard1KeyMultiply = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Multiply)
                        Keyboard1KeyMultiply = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.LeftAlt)
                        Keyboard1KeyLeftAlt = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.LeftAlt)
                        Keyboard1KeyLeftAlt = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Space)
                        Keyboard1KeySpace = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Space)
                        Keyboard1KeySpace = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Capital)
                        Keyboard1KeyCapital = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Capital)
                        Keyboard1KeyCapital = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F1)
                        Keyboard1KeyF1 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F1)
                        Keyboard1KeyF1 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F2)
                        Keyboard1KeyF2 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F2)
                        Keyboard1KeyF2 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F3)
                        Keyboard1KeyF3 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F3)
                        Keyboard1KeyF3 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F4)
                        Keyboard1KeyF4 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F4)
                        Keyboard1KeyF4 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F5)
                        Keyboard1KeyF5 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F5)
                        Keyboard1KeyF5 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F6)
                        Keyboard1KeyF6 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F6)
                        Keyboard1KeyF6 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F7)
                        Keyboard1KeyF7 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F7)
                        Keyboard1KeyF7 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F8)
                        Keyboard1KeyF8 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F8)
                        Keyboard1KeyF8 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F9)
                        Keyboard1KeyF9 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F9)
                        Keyboard1KeyF9 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F10)
                        Keyboard1KeyF10 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F10)
                        Keyboard1KeyF10 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberLock)
                        Keyboard1KeyNumberLock = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberLock)
                        Keyboard1KeyNumberLock = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.ScrollLock)
                        Keyboard1KeyScrollLock = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.ScrollLock)
                        Keyboard1KeyScrollLock = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad7)
                        Keyboard1KeyNumberPad7 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad7)
                        Keyboard1KeyNumberPad7 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad8)
                        Keyboard1KeyNumberPad8 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad8)
                        Keyboard1KeyNumberPad8 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad9)
                        Keyboard1KeyNumberPad9 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad9)
                        Keyboard1KeyNumberPad9 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Subtract)
                        Keyboard1KeySubtract = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Subtract)
                        Keyboard1KeySubtract = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad4)
                        Keyboard1KeyNumberPad4 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad4)
                        Keyboard1KeyNumberPad4 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad5)
                        Keyboard1KeyNumberPad5 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad5)
                        Keyboard1KeyNumberPad5 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad6)
                        Keyboard1KeyNumberPad6 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad6)
                        Keyboard1KeyNumberPad6 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Add)
                        Keyboard1KeyAdd = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Add)
                        Keyboard1KeyAdd = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad1)
                        Keyboard1KeyNumberPad1 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad1)
                        Keyboard1KeyNumberPad1 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad2)
                        Keyboard1KeyNumberPad2 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad2)
                        Keyboard1KeyNumberPad2 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad3)
                        Keyboard1KeyNumberPad3 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad3)
                        Keyboard1KeyNumberPad3 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPad0)
                        Keyboard1KeyNumberPad0 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPad0)
                        Keyboard1KeyNumberPad0 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Decimal)
                        Keyboard1KeyDecimal = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Decimal)
                        Keyboard1KeyDecimal = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Oem102)
                        Keyboard1KeyOem102 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Oem102)
                        Keyboard1KeyOem102 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F11)
                        Keyboard1KeyF11 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F11)
                        Keyboard1KeyF11 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F12)
                        Keyboard1KeyF12 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F12)
                        Keyboard1KeyF12 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F13)
                        Keyboard1KeyF13 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F13)
                        Keyboard1KeyF13 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F14)
                        Keyboard1KeyF14 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F14)
                        Keyboard1KeyF14 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.F15)
                        Keyboard1KeyF15 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.F15)
                        Keyboard1KeyF15 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Kana)
                        Keyboard1KeyKana = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Kana)
                        Keyboard1KeyKana = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.AbntC1)
                        Keyboard1KeyAbntC1 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.AbntC1)
                        Keyboard1KeyAbntC1 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Convert)
                        Keyboard1KeyConvert = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Convert)
                        Keyboard1KeyConvert = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NoConvert)
                        Keyboard1KeyNoConvert = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NoConvert)
                        Keyboard1KeyNoConvert = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Yen)
                        Keyboard1KeyYen = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Yen)
                        Keyboard1KeyYen = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.AbntC2)
                        Keyboard1KeyAbntC2 = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.AbntC2)
                        Keyboard1KeyAbntC2 = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPadEquals)
                        Keyboard1KeyNumberPadEquals = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPadEquals)
                        Keyboard1KeyNumberPadEquals = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.PreviousTrack)
                        Keyboard1KeyPreviousTrack = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.PreviousTrack)
                        Keyboard1KeyPreviousTrack = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.AT)
                        Keyboard1KeyAT = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.AT)
                        Keyboard1KeyAT = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Colon)
                        Keyboard1KeyColon = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Colon)
                        Keyboard1KeyColon = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Underline)
                        Keyboard1KeyUnderline = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Underline)
                        Keyboard1KeyUnderline = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Kanji)
                        Keyboard1KeyKanji = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Kanji)
                        Keyboard1KeyKanji = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Stop)
                        Keyboard1KeyStop = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Stop)
                        Keyboard1KeyStop = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.AX)
                        Keyboard1KeyAX = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.AX)
                        Keyboard1KeyAX = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Unlabeled)
                        Keyboard1KeyUnlabeled = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Unlabeled)
                        Keyboard1KeyUnlabeled = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NextTrack)
                        Keyboard1KeyNextTrack = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NextTrack)
                        Keyboard1KeyNextTrack = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPadEnter)
                        Keyboard1KeyNumberPadEnter = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPadEnter)
                        Keyboard1KeyNumberPadEnter = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.RightControl)
                        Keyboard1KeyRightControl = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.RightControl)
                        Keyboard1KeyRightControl = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Mute)
                        Keyboard1KeyMute = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Mute)
                        Keyboard1KeyMute = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Calculator)
                        Keyboard1KeyCalculator = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Calculator)
                        Keyboard1KeyCalculator = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.PlayPause)
                        Keyboard1KeyPlayPause = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.PlayPause)
                        Keyboard1KeyPlayPause = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.MediaStop)
                        Keyboard1KeyMediaStop = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.MediaStop)
                        Keyboard1KeyMediaStop = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.VolumeDown)
                        Keyboard1KeyVolumeDown = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.VolumeDown)
                        Keyboard1KeyVolumeDown = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.VolumeUp)
                        Keyboard1KeyVolumeUp = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.VolumeUp)
                        Keyboard1KeyVolumeUp = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.WebHome)
                        Keyboard1KeyWebHome = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.WebHome)
                        Keyboard1KeyWebHome = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.NumberPadComma)
                        Keyboard1KeyNumberPadComma = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.NumberPadComma)
                        Keyboard1KeyNumberPadComma = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Divide)
                        Keyboard1KeyDivide = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Divide)
                        Keyboard1KeyDivide = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.PrintScreen)
                        Keyboard1KeyPrintScreen = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.PrintScreen)
                        Keyboard1KeyPrintScreen = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.RightAlt)
                        Keyboard1KeyRightAlt = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.RightAlt)
                        Keyboard1KeyRightAlt = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Pause)
                        Keyboard1KeyPause = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Pause)
                        Keyboard1KeyPause = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Home)
                        Keyboard1KeyHome = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Home)
                        Keyboard1KeyHome = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Up)
                        Keyboard1KeyUp = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Up)
                        Keyboard1KeyUp = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.PageUp)
                        Keyboard1KeyPageUp = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.PageUp)
                        Keyboard1KeyPageUp = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Left)
                        Keyboard1KeyLeft = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Left)
                        Keyboard1KeyLeft = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Right)
                        Keyboard1KeyRight = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Right)
                        Keyboard1KeyRight = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.End)
                        Keyboard1KeyEnd = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.End)
                        Keyboard1KeyEnd = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Down)
                        Keyboard1KeyDown = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Down)
                        Keyboard1KeyDown = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.PageDown)
                        Keyboard1KeyPageDown = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.PageDown)
                        Keyboard1KeyPageDown = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Insert)
                        Keyboard1KeyInsert = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Insert)
                        Keyboard1KeyInsert = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Delete)
                        Keyboard1KeyDelete = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Delete)
                        Keyboard1KeyDelete = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.LeftWindowsKey)
                        Keyboard1KeyLeftWindowsKey = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.LeftWindowsKey)
                        Keyboard1KeyLeftWindowsKey = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.RightWindowsKey)
                        Keyboard1KeyRightWindowsKey = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.RightWindowsKey)
                        Keyboard1KeyRightWindowsKey = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Applications)
                        Keyboard1KeyApplications = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Applications)
                        Keyboard1KeyApplications = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Power)
                        Keyboard1KeyPower = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Power)
                        Keyboard1KeyPower = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Sleep)
                        Keyboard1KeySleep = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Sleep)
                        Keyboard1KeySleep = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Wake)
                        Keyboard1KeyWake = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Wake)
                        Keyboard1KeyWake = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.WebSearch)
                        Keyboard1KeyWebSearch = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.WebSearch)
                        Keyboard1KeyWebSearch = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.WebFavorites)
                        Keyboard1KeyWebFavorites = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.WebFavorites)
                        Keyboard1KeyWebFavorites = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.WebRefresh)
                        Keyboard1KeyWebRefresh = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.WebRefresh)
                        Keyboard1KeyWebRefresh = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.WebStop)
                        Keyboard1KeyWebStop = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.WebStop)
                        Keyboard1KeyWebStop = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.WebForward)
                        Keyboard1KeyWebForward = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.WebForward)
                        Keyboard1KeyWebForward = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.WebBack)
                        Keyboard1KeyWebBack = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.WebBack)
                        Keyboard1KeyWebBack = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.MyComputer)
                        Keyboard1KeyMyComputer = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.MyComputer)
                        Keyboard1KeyMyComputer = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Mail)
                        Keyboard1KeyMail = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Mail)
                        Keyboard1KeyMail = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.MediaSelect)
                        Keyboard1KeyMediaSelect = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.MediaSelect)
                        Keyboard1KeyMediaSelect = false;
                    if (inc == 0 & state.IsPressed & state.Key == Key.Unknown)
                        Keyboard1KeyUnknown = true;
                    if (inc == 0 & state.IsReleased & state.Key == Key.Unknown)
                        Keyboard1KeyUnknown = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Escape)
                        Keyboard2KeyEscape = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Escape)
                        Keyboard2KeyEscape = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D1)
                        Keyboard2KeyD1 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D1)
                        Keyboard2KeyD1 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D2)
                        Keyboard2KeyD2 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D2)
                        Keyboard2KeyD2 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D3)
                        Keyboard2KeyD3 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D3)
                        Keyboard2KeyD3 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D4)
                        Keyboard2KeyD4 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D4)
                        Keyboard2KeyD4 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D5)
                        Keyboard2KeyD5 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D5)
                        Keyboard2KeyD5 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D6)
                        Keyboard2KeyD6 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D6)
                        Keyboard2KeyD6 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D7)
                        Keyboard2KeyD7 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D7)
                        Keyboard2KeyD7 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D8)
                        Keyboard2KeyD8 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D8)
                        Keyboard2KeyD8 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D9)
                        Keyboard2KeyD9 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D9)
                        Keyboard2KeyD9 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D0)
                        Keyboard2KeyD0 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D0)
                        Keyboard2KeyD0 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Minus)
                        Keyboard2KeyMinus = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Minus)
                        Keyboard2KeyMinus = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Equals)
                        Keyboard2KeyEquals = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Equals)
                        Keyboard2KeyEquals = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Back)
                        Keyboard2KeyBack = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Back)
                        Keyboard2KeyBack = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Tab)
                        Keyboard2KeyTab = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Tab)
                        Keyboard2KeyTab = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Q)
                        Keyboard2KeyQ = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Q)
                        Keyboard2KeyQ = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.W)
                        Keyboard2KeyW = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.W)
                        Keyboard2KeyW = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.E)
                        Keyboard2KeyE = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.E)
                        Keyboard2KeyE = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.R)
                        Keyboard2KeyR = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.R)
                        Keyboard2KeyR = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.T)
                        Keyboard2KeyT = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.T)
                        Keyboard2KeyT = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Y)
                        Keyboard2KeyY = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Y)
                        Keyboard2KeyY = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.U)
                        Keyboard2KeyU = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.U)
                        Keyboard2KeyU = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.I)
                        Keyboard2KeyI = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.I)
                        Keyboard2KeyI = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.O)
                        Keyboard2KeyO = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.O)
                        Keyboard2KeyO = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.P)
                        Keyboard2KeyP = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.P)
                        Keyboard2KeyP = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.LeftBracket)
                        Keyboard2KeyLeftBracket = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.LeftBracket)
                        Keyboard2KeyLeftBracket = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.RightBracket)
                        Keyboard2KeyRightBracket = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.RightBracket)
                        Keyboard2KeyRightBracket = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Return)
                        Keyboard2KeyReturn = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Return)
                        Keyboard2KeyReturn = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.LeftControl)
                        Keyboard2KeyLeftControl = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.LeftControl)
                        Keyboard2KeyLeftControl = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.A)
                        Keyboard2KeyA = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.A)
                        Keyboard2KeyA = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.S)
                        Keyboard2KeyS = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.S)
                        Keyboard2KeyS = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.D)
                        Keyboard2KeyD = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.D)
                        Keyboard2KeyD = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F)
                        Keyboard2KeyF = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F)
                        Keyboard2KeyF = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.G)
                        Keyboard2KeyG = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.G)
                        Keyboard2KeyG = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.H)
                        Keyboard2KeyH = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.H)
                        Keyboard2KeyH = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.J)
                        Keyboard2KeyJ = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.J)
                        Keyboard2KeyJ = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.K)
                        Keyboard2KeyK = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.K)
                        Keyboard2KeyK = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.L)
                        Keyboard2KeyL = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.L)
                        Keyboard2KeyL = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Semicolon)
                        Keyboard2KeySemicolon = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Semicolon)
                        Keyboard2KeySemicolon = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Apostrophe)
                        Keyboard2KeyApostrophe = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Apostrophe)
                        Keyboard2KeyApostrophe = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Grave)
                        Keyboard2KeyGrave = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Grave)
                        Keyboard2KeyGrave = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.LeftShift)
                        Keyboard2KeyLeftShift = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.LeftShift)
                        Keyboard2KeyLeftShift = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Backslash)
                        Keyboard2KeyBackslash = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Backslash)
                        Keyboard2KeyBackslash = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Z)
                        Keyboard2KeyZ = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Z)
                        Keyboard2KeyZ = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.X)
                        Keyboard2KeyX = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.X)
                        Keyboard2KeyX = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.C)
                        Keyboard2KeyC = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.C)
                        Keyboard2KeyC = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.V)
                        Keyboard2KeyV = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.V)
                        Keyboard2KeyV = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.B)
                        Keyboard2KeyB = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.B)
                        Keyboard2KeyB = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.N)
                        Keyboard2KeyN = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.N)
                        Keyboard2KeyN = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.M)
                        Keyboard2KeyM = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.M)
                        Keyboard2KeyM = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Comma)
                        Keyboard2KeyComma = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Comma)
                        Keyboard2KeyComma = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Period)
                        Keyboard2KeyPeriod = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Period)
                        Keyboard2KeyPeriod = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Slash)
                        Keyboard2KeySlash = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Slash)
                        Keyboard2KeySlash = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.RightShift)
                        Keyboard2KeyRightShift = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.RightShift)
                        Keyboard2KeyRightShift = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Multiply)
                        Keyboard2KeyMultiply = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Multiply)
                        Keyboard2KeyMultiply = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.LeftAlt)
                        Keyboard2KeyLeftAlt = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.LeftAlt)
                        Keyboard2KeyLeftAlt = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Space)
                        Keyboard2KeySpace = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Space)
                        Keyboard2KeySpace = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Capital)
                        Keyboard2KeyCapital = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Capital)
                        Keyboard2KeyCapital = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F1)
                        Keyboard2KeyF1 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F1)
                        Keyboard2KeyF1 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F2)
                        Keyboard2KeyF2 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F2)
                        Keyboard2KeyF2 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F3)
                        Keyboard2KeyF3 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F3)
                        Keyboard2KeyF3 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F4)
                        Keyboard2KeyF4 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F4)
                        Keyboard2KeyF4 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F5)
                        Keyboard2KeyF5 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F5)
                        Keyboard2KeyF5 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F6)
                        Keyboard2KeyF6 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F6)
                        Keyboard2KeyF6 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F7)
                        Keyboard2KeyF7 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F7)
                        Keyboard2KeyF7 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F8)
                        Keyboard2KeyF8 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F8)
                        Keyboard2KeyF8 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F9)
                        Keyboard2KeyF9 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F9)
                        Keyboard2KeyF9 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F10)
                        Keyboard2KeyF10 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F10)
                        Keyboard2KeyF10 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberLock)
                        Keyboard2KeyNumberLock = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberLock)
                        Keyboard2KeyNumberLock = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.ScrollLock)
                        Keyboard2KeyScrollLock = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.ScrollLock)
                        Keyboard2KeyScrollLock = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad7)
                        Keyboard2KeyNumberPad7 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad7)
                        Keyboard2KeyNumberPad7 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad8)
                        Keyboard2KeyNumberPad8 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad8)
                        Keyboard2KeyNumberPad8 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad9)
                        Keyboard2KeyNumberPad9 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad9)
                        Keyboard2KeyNumberPad9 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Subtract)
                        Keyboard2KeySubtract = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Subtract)
                        Keyboard2KeySubtract = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad4)
                        Keyboard2KeyNumberPad4 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad4)
                        Keyboard2KeyNumberPad4 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad5)
                        Keyboard2KeyNumberPad5 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad5)
                        Keyboard2KeyNumberPad5 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad6)
                        Keyboard2KeyNumberPad6 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad6)
                        Keyboard2KeyNumberPad6 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Add)
                        Keyboard2KeyAdd = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Add)
                        Keyboard2KeyAdd = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad1)
                        Keyboard2KeyNumberPad1 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad1)
                        Keyboard2KeyNumberPad1 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad2)
                        Keyboard2KeyNumberPad2 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad2)
                        Keyboard2KeyNumberPad2 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad3)
                        Keyboard2KeyNumberPad3 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad3)
                        Keyboard2KeyNumberPad3 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPad0)
                        Keyboard2KeyNumberPad0 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPad0)
                        Keyboard2KeyNumberPad0 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Decimal)
                        Keyboard2KeyDecimal = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Decimal)
                        Keyboard2KeyDecimal = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Oem102)
                        Keyboard2KeyOem102 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Oem102)
                        Keyboard2KeyOem102 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F11)
                        Keyboard2KeyF11 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F11)
                        Keyboard2KeyF11 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F12)
                        Keyboard2KeyF12 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F12)
                        Keyboard2KeyF12 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F13)
                        Keyboard2KeyF13 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F13)
                        Keyboard2KeyF13 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F14)
                        Keyboard2KeyF14 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F14)
                        Keyboard2KeyF14 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.F15)
                        Keyboard2KeyF15 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.F15)
                        Keyboard2KeyF15 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Kana)
                        Keyboard2KeyKana = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Kana)
                        Keyboard2KeyKana = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.AbntC1)
                        Keyboard2KeyAbntC1 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.AbntC1)
                        Keyboard2KeyAbntC1 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Convert)
                        Keyboard2KeyConvert = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Convert)
                        Keyboard2KeyConvert = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NoConvert)
                        Keyboard2KeyNoConvert = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NoConvert)
                        Keyboard2KeyNoConvert = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Yen)
                        Keyboard2KeyYen = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Yen)
                        Keyboard2KeyYen = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.AbntC2)
                        Keyboard2KeyAbntC2 = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.AbntC2)
                        Keyboard2KeyAbntC2 = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPadEquals)
                        Keyboard2KeyNumberPadEquals = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPadEquals)
                        Keyboard2KeyNumberPadEquals = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.PreviousTrack)
                        Keyboard2KeyPreviousTrack = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.PreviousTrack)
                        Keyboard2KeyPreviousTrack = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.AT)
                        Keyboard2KeyAT = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.AT)
                        Keyboard2KeyAT = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Colon)
                        Keyboard2KeyColon = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Colon)
                        Keyboard2KeyColon = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Underline)
                        Keyboard2KeyUnderline = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Underline)
                        Keyboard2KeyUnderline = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Kanji)
                        Keyboard2KeyKanji = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Kanji)
                        Keyboard2KeyKanji = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Stop)
                        Keyboard2KeyStop = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Stop)
                        Keyboard2KeyStop = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.AX)
                        Keyboard2KeyAX = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.AX)
                        Keyboard2KeyAX = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Unlabeled)
                        Keyboard2KeyUnlabeled = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Unlabeled)
                        Keyboard2KeyUnlabeled = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NextTrack)
                        Keyboard2KeyNextTrack = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NextTrack)
                        Keyboard2KeyNextTrack = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPadEnter)
                        Keyboard2KeyNumberPadEnter = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPadEnter)
                        Keyboard2KeyNumberPadEnter = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.RightControl)
                        Keyboard2KeyRightControl = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.RightControl)
                        Keyboard2KeyRightControl = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Mute)
                        Keyboard2KeyMute = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Mute)
                        Keyboard2KeyMute = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Calculator)
                        Keyboard2KeyCalculator = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Calculator)
                        Keyboard2KeyCalculator = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.PlayPause)
                        Keyboard2KeyPlayPause = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.PlayPause)
                        Keyboard2KeyPlayPause = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.MediaStop)
                        Keyboard2KeyMediaStop = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.MediaStop)
                        Keyboard2KeyMediaStop = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.VolumeDown)
                        Keyboard2KeyVolumeDown = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.VolumeDown)
                        Keyboard2KeyVolumeDown = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.VolumeUp)
                        Keyboard2KeyVolumeUp = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.VolumeUp)
                        Keyboard2KeyVolumeUp = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.WebHome)
                        Keyboard2KeyWebHome = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.WebHome)
                        Keyboard2KeyWebHome = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.NumberPadComma)
                        Keyboard2KeyNumberPadComma = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.NumberPadComma)
                        Keyboard2KeyNumberPadComma = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Divide)
                        Keyboard2KeyDivide = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Divide)
                        Keyboard2KeyDivide = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.PrintScreen)
                        Keyboard2KeyPrintScreen = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.PrintScreen)
                        Keyboard2KeyPrintScreen = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.RightAlt)
                        Keyboard2KeyRightAlt = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.RightAlt)
                        Keyboard2KeyRightAlt = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Pause)
                        Keyboard2KeyPause = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Pause)
                        Keyboard2KeyPause = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Home)
                        Keyboard2KeyHome = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Home)
                        Keyboard2KeyHome = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Up)
                        Keyboard2KeyUp = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Up)
                        Keyboard2KeyUp = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.PageUp)
                        Keyboard2KeyPageUp = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.PageUp)
                        Keyboard2KeyPageUp = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Left)
                        Keyboard2KeyLeft = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Left)
                        Keyboard2KeyLeft = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Right)
                        Keyboard2KeyRight = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Right)
                        Keyboard2KeyRight = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.End)
                        Keyboard2KeyEnd = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.End)
                        Keyboard2KeyEnd = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Down)
                        Keyboard2KeyDown = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Down)
                        Keyboard2KeyDown = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.PageDown)
                        Keyboard2KeyPageDown = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.PageDown)
                        Keyboard2KeyPageDown = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Insert)
                        Keyboard2KeyInsert = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Insert)
                        Keyboard2KeyInsert = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Delete)
                        Keyboard2KeyDelete = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Delete)
                        Keyboard2KeyDelete = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.LeftWindowsKey)
                        Keyboard2KeyLeftWindowsKey = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.LeftWindowsKey)
                        Keyboard2KeyLeftWindowsKey = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.RightWindowsKey)
                        Keyboard2KeyRightWindowsKey = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.RightWindowsKey)
                        Keyboard2KeyRightWindowsKey = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Applications)
                        Keyboard2KeyApplications = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Applications)
                        Keyboard2KeyApplications = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Power)
                        Keyboard2KeyPower = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Power)
                        Keyboard2KeyPower = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Sleep)
                        Keyboard2KeySleep = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Sleep)
                        Keyboard2KeySleep = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Wake)
                        Keyboard2KeyWake = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Wake)
                        Keyboard2KeyWake = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.WebSearch)
                        Keyboard2KeyWebSearch = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.WebSearch)
                        Keyboard2KeyWebSearch = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.WebFavorites)
                        Keyboard2KeyWebFavorites = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.WebFavorites)
                        Keyboard2KeyWebFavorites = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.WebRefresh)
                        Keyboard2KeyWebRefresh = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.WebRefresh)
                        Keyboard2KeyWebRefresh = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.WebStop)
                        Keyboard2KeyWebStop = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.WebStop)
                        Keyboard2KeyWebStop = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.WebForward)
                        Keyboard2KeyWebForward = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.WebForward)
                        Keyboard2KeyWebForward = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.WebBack)
                        Keyboard2KeyWebBack = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.WebBack)
                        Keyboard2KeyWebBack = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.MyComputer)
                        Keyboard2KeyMyComputer = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.MyComputer)
                        Keyboard2KeyMyComputer = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Mail)
                        Keyboard2KeyMail = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Mail)
                        Keyboard2KeyMail = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.MediaSelect)
                        Keyboard2KeyMediaSelect = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.MediaSelect)
                        Keyboard2KeyMediaSelect = false;
                    if (inc == 1 & state.IsPressed & state.Key == Key.Unknown)
                        Keyboard2KeyUnknown = true;
                    if (inc == 1 & state.IsReleased & state.Key == Key.Unknown)
                        Keyboard2KeyUnknown = false;
                }
            }
        }
        public const int S_LBUTTON = (int)0x0;
        public const int S_RBUTTON = 0;
        public const int S_CANCEL = 70;
        public const int S_MBUTTON = 0;
        public const int S_XBUTTON1 = 0;
        public const int S_XBUTTON2 = 0;
        public const int S_BACK = 14;
        public const int S_Tab = 15;
        public const int S_CLEAR = 76;
        public const int S_Return = 28;
        public const int S_SHIFT = 42;
        public const int S_CONTROL = 29;
        public const int S_MENU = 56;
        public const int S_PAUSE = 0;
        public const int S_CAPITAL = 58;
        public const int S_KANA = 0;
        public const int S_HANGEUL = 0;
        public const int S_HANGUL = 0;
        public const int S_JUNJA = 0;
        public const int S_FINAL = 0;
        public const int S_HANJA = 0;
        public const int S_KANJI = 0;
        public const int S_Escape = 1;
        public const int S_CONVERT = 0;
        public const int S_NONCONVERT = 0;
        public const int S_ACCEPT = 0;
        public const int S_MODECHANGE = 0;
        public const int S_Space = 57;
        public const int S_PRIOR = 73;
        public const int S_NEXT = 81;
        public const int S_END = 79;
        public const int S_HOME = 71;
        public const int S_LEFT = 75;
        public const int S_UP = 72;
        public const int S_RIGHT = 77;
        public const int S_DOWN = 80;
        public const int S_SELECT = 0;
        public const int S_PRINT = 0;
        public const int S_EXECUTE = 0;
        public const int S_SNAPSHOT = 84;
        public const int S_INSERT = 82;
        public const int S_DELETE = 83;
        public const int S_HELP = 99;
        public const int S_APOSTROPHE = 41;
        public const int S_0 = 11;
        public const int S_1 = 2;
        public const int S_2 = 3;
        public const int S_3 = 4;
        public const int S_4 = 5;
        public const int S_5 = 6;
        public const int S_6 = 7;
        public const int S_7 = 8;
        public const int S_8 = 9;
        public const int S_9 = 10;
        public const int S_A = 16;
        public const int S_B = 48;
        public const int S_C = 46;
        public const int S_D = 32;
        public const int S_E = 18;
        public const int S_F = 33;
        public const int S_G = 34;
        public const int S_H = 35;
        public const int S_I = 23;
        public const int S_J = 36;
        public const int S_K = 37;
        public const int S_L = 38;
        public const int S_M = 39;
        public const int S_N = 49;
        public const int S_O = 24;
        public const int S_P = 25;
        public const int S_Q = 30;
        public const int S_R = 19;
        public const int S_S = 31;
        public const int S_T = 20;
        public const int S_U = 22;
        public const int S_V = 47;
        public const int S_W = 44;
        public const int S_X = 45;
        public const int S_Y = 21;
        public const int S_Z = 17;
        public const int S_LWIN = 91;
        public const int S_RWIN = 92;
        public const int S_APPS = 93;
        public const int S_SLEEP = 95;
        public const int S_NUMPAD0 = 82;
        public const int S_NUMPAD1 = 79;
        public const int S_NUMPAD2 = 80;
        public const int S_NUMPAD3 = 81;
        public const int S_NUMPAD4 = 75;
        public const int S_NUMPAD5 = 76;
        public const int S_NUMPAD6 = 77;
        public const int S_NUMPAD7 = 71;
        public const int S_NUMPAD8 = 72;
        public const int S_NUMPAD9 = 73;
        public const int S_MULTIPLY = 55;
        public const int S_ADD = 78;
        public const int S_SEPARATOR = 0;
        public const int S_SUBTRACT = 74;
        public const int S_DECIMAL = 83;
        public const int S_DIVIDE = 53;
        public const int S_F1 = 59;
        public const int S_F2 = 60;
        public const int S_F3 = 61;
        public const int S_F4 = 62;
        public const int S_F5 = 63;
        public const int S_F6 = 64;
        public const int S_F7 = 65;
        public const int S_F8 = 66;
        public const int S_F9 = 67;
        public const int S_F10 = 68;
        public const int S_F11 = 87;
        public const int S_F12 = 88;
        public const int S_F13 = 100;
        public const int S_F14 = 101;
        public const int S_F15 = 102;
        public const int S_F16 = 103;
        public const int S_F17 = 104;
        public const int S_F18 = 105;
        public const int S_F19 = 106;
        public const int S_F20 = 107;
        public const int S_F21 = 108;
        public const int S_F22 = 109;
        public const int S_F23 = 110;
        public const int S_F24 = 118;
        public const int S_NUMLOCK = 69;
        public const int S_SCROLL = 70;
        public const int S_LeftShift = 42;
        public const int S_RightShift = 54;
        public const int S_LeftControl = 29;
        public const int S_RightControl = 29;
        public const int S_LMENU = 56;
        public const int S_RMENU = 56;
        public const int S_BROWSER_BACK = 106;
        public const int S_BROWSER_FORWARD = 105;
        public const int S_BROWSER_REFRESH = 103;
        public const int S_BROWSER_STOP = 104;
        public const int S_BROWSER_SEARCH = 101;
        public const int S_BROWSER_FAVORITES = 102;
        public const int S_BROWSER_HOME = 50;
        public const int S_VOLUME_MUTE = 32;
        public const int S_VOLUME_DOWN = 46;
        public const int S_VOLUME_UP = 48;
        public const int S_MEDIA_NEXT_TRACK = 25;
        public const int S_MEDIA_PREV_TRACK = 16;
        public const int S_MEDIA_STOP = 36;
        public const int S_MEDIA_PLAY_PAUSE = 34;
        public const int S_LAUNCH_MAIL = 108;
        public const int S_LAUNCH_MEDIA_SELECT = 109;
        public const int S_LAUNCH_APP1 = 107;
        public const int S_LAUNCH_APP2 = 33;
        public const int S_OEM_1 = 27;
        public const int S_OEM_PLUS = 13;
        public const int S_OEM_COMMA = 50;
        public const int S_OEM_MINUS = 0;
        public const int S_OEM_PERIOD = 51;
        public const int S_OEM_2 = 52;
        public const int S_OEM_3 = 40;
        public const int S_OEM_4 = 12;
        public const int S_OEM_5 = 43;
        public const int S_OEM_6 = 26;
        public const int S_OEM_7 = 41;
        public const int S_OEM_8 = 53;
        public const int S_OEM_102 = 86;
        public const int S_PROCESSKEY = 0;
        public const int S_PACKET = 0;
        public const int S_ATTN = 0;
        public const int S_CRSEL = 0;
        public const int S_EXSEL = 0;
        public const int S_EREOF = 93;
        public const int S_PLAY = 0;
        public const int S_ZOOM = 98;
        public const int S_NONAME = 0;
        public const int S_PA1 = 0;
        public const int S_OEM_CLEAR = 0;
        public static bool Key_LBUTTON;
        public static bool Key_RBUTTON;
        public static bool Key_CANCEL;
        public static bool Key_MBUTTON;
        public static bool Key_XBUTTON1;
        public static bool Key_XBUTTON2;
        public static bool Key_BACK;
        public static bool Key_Tab;
        public static bool Key_CLEAR;
        public static bool Key_Return;
        public static bool Key_SHIFT;
        public static bool Key_CONTROL;
        public static bool Key_MENU;
        public static bool Key_PAUSE;
        public static bool Key_CAPITAL;
        public static bool Key_KANA;
        public static bool Key_HANGEUL;
        public static bool Key_HANGUL;
        public static bool Key_JUNJA;
        public static bool Key_FINAL;
        public static bool Key_HANJA;
        public static bool Key_KANJI;
        public static bool Key_Escape;
        public static bool Key_CONVERT;
        public static bool Key_NONCONVERT;
        public static bool Key_ACCEPT;
        public static bool Key_MODECHANGE;
        public static bool Key_Space;
        public static bool Key_PRIOR;
        public static bool Key_NEXT;
        public static bool Key_END;
        public static bool Key_HOME;
        public static bool Key_LEFT;
        public static bool Key_UP;
        public static bool Key_RIGHT;
        public static bool Key_DOWN;
        public static bool Key_SELECT;
        public static bool Key_PRINT;
        public static bool Key_EXECUTE;
        public static bool Key_SNAPSHOT;
        public static bool Key_INSERT;
        public static bool Key_DELETE;
        public static bool Key_HELP;
        public static bool Key_APOSTROPHE;
        public static bool Key_0;
        public static bool Key_1;
        public static bool Key_2;
        public static bool Key_3;
        public static bool Key_4;
        public static bool Key_5;
        public static bool Key_6;
        public static bool Key_7;
        public static bool Key_8;
        public static bool Key_9;
        public static bool Key_A;
        public static bool Key_B;
        public static bool Key_C;
        public static bool Key_D;
        public static bool Key_E;
        public static bool Key_F;
        public static bool Key_G;
        public static bool Key_H;
        public static bool Key_I;
        public static bool Key_J;
        public static bool Key_K;
        public static bool Key_L;
        public static bool Key_M;
        public static bool Key_N;
        public static bool Key_O;
        public static bool Key_P;
        public static bool Key_Q;
        public static bool Key_R;
        public static bool Key_S;
        public static bool Key_T;
        public static bool Key_U;
        public static bool Key_V;
        public static bool Key_W;
        public static bool Key_X;
        public static bool Key_Y;
        public static bool Key_Z;
        public static bool Key_LWIN;
        public static bool Key_RWIN;
        public static bool Key_APPS;
        public static bool Key_SLEEP;
        public static bool Key_NUMPAD0;
        public static bool Key_NUMPAD1;
        public static bool Key_NUMPAD2;
        public static bool Key_NUMPAD3;
        public static bool Key_NUMPAD4;
        public static bool Key_NUMPAD5;
        public static bool Key_NUMPAD6;
        public static bool Key_NUMPAD7;
        public static bool Key_NUMPAD8;
        public static bool Key_NUMPAD9;
        public static bool Key_MULTIPLY;
        public static bool Key_ADD;
        public static bool Key_SEPARATOR;
        public static bool Key_SUBTRACT;
        public static bool Key_DECIMAL;
        public static bool Key_DIVIDE;
        public static bool Key_F1;
        public static bool Key_F2;
        public static bool Key_F3;
        public static bool Key_F4;
        public static bool Key_F5;
        public static bool Key_F6;
        public static bool Key_F7;
        public static bool Key_F8;
        public static bool Key_F9;
        public static bool Key_F10;
        public static bool Key_F11;
        public static bool Key_F12;
        public static bool Key_F13;
        public static bool Key_F14;
        public static bool Key_F15;
        public static bool Key_F16;
        public static bool Key_F17;
        public static bool Key_F18;
        public static bool Key_F19;
        public static bool Key_F20;
        public static bool Key_F21;
        public static bool Key_F22;
        public static bool Key_F23;
        public static bool Key_F24;
        public static bool Key_NUMLOCK;
        public static bool Key_SCROLL;
        public static bool Key_LeftShift;
        public static bool Key_RightShift;
        public static bool Key_LeftControl;
        public static bool Key_RightControl;
        public static bool Key_LMENU;
        public static bool Key_RMENU;
        public static bool Key_BROWSER_BACK;
        public static bool Key_BROWSER_FORWARD;
        public static bool Key_BROWSER_REFRESH;
        public static bool Key_BROWSER_STOP;
        public static bool Key_BROWSER_SEARCH;
        public static bool Key_BROWSER_FAVORITES;
        public static bool Key_BROWSER_HOME;
        public static bool Key_VOLUME_MUTE;
        public static bool Key_VOLUME_DOWN;
        public static bool Key_VOLUME_UP;
        public static bool Key_MEDIA_NEXT_TRACK;
        public static bool Key_MEDIA_PREV_TRACK;
        public static bool Key_MEDIA_STOP;
        public static bool Key_MEDIA_PLAY_PAUSE;
        public static bool Key_LAUNCH_MAIL;
        public static bool Key_LAUNCH_MEDIA_SELECT;
        public static bool Key_LAUNCH_APP1;
        public static bool Key_LAUNCH_APP2;
        public static bool Key_OEM_1;
        public static bool Key_OEM_PLUS;
        public static bool Key_OEM_COMMA;
        public static bool Key_OEM_MINUS;
        public static bool Key_OEM_PERIOD;
        public static bool Key_OEM_2;
        public static bool Key_OEM_3;
        public static bool Key_OEM_4;
        public static bool Key_OEM_5;
        public static bool Key_OEM_6;
        public static bool Key_OEM_7;
        public static bool Key_OEM_8;
        public static bool Key_OEM_102;
        public static bool Key_PROCESSKEY;
        public static bool Key_PACKET;
        public static bool Key_ATTN;
        public static bool Key_CRSEL;
        public static bool Key_EXSEL;
        public static bool Key_EREOF;
        public static bool Key_PLAY;
        public static bool Key_ZOOM;
        public static bool Key_NONAME;
        public static bool Key_PA1;
        public static bool Key_OEM_CLEAR;
        public static void KeyboardHookProcessButtons()
        {
            if (KeyboardHookButtonDown)
            {
                if (scanCode == S_LBUTTON)
                    Key_LBUTTON = true;
                if (scanCode == S_RBUTTON)
                    Key_RBUTTON = true;
                if (scanCode == S_CANCEL)
                    Key_CANCEL = true;
                if (scanCode == S_MBUTTON)
                    Key_MBUTTON = true;
                if (scanCode == S_XBUTTON1)
                    Key_XBUTTON1 = true;
                if (scanCode == S_XBUTTON2)
                    Key_XBUTTON2 = true;
                if (scanCode == S_BACK)
                    Key_BACK = true;
                if (scanCode == S_Tab)
                    Key_Tab = true;
                if (scanCode == S_CLEAR)
                    Key_CLEAR = true;
                if (scanCode == S_Return)
                    Key_Return = true;
                if (scanCode == S_SHIFT)
                    Key_SHIFT = true;
                if (scanCode == S_CONTROL)
                    Key_CONTROL = true;
                if (scanCode == S_MENU)
                    Key_MENU = true;
                if (scanCode == S_PAUSE)
                    Key_PAUSE = true;
                if (scanCode == S_CAPITAL)
                    Key_CAPITAL = true;
                if (scanCode == S_KANA)
                    Key_KANA = true;
                if (scanCode == S_HANGEUL)
                    Key_HANGEUL = true;
                if (scanCode == S_HANGUL)
                    Key_HANGUL = true;
                if (scanCode == S_JUNJA)
                    Key_JUNJA = true;
                if (scanCode == S_FINAL)
                    Key_FINAL = true;
                if (scanCode == S_HANJA)
                    Key_HANJA = true;
                if (scanCode == S_KANJI)
                    Key_KANJI = true;
                if (scanCode == S_Escape)
                    Key_Escape = true;
                if (scanCode == S_CONVERT)
                    Key_CONVERT = true;
                if (scanCode == S_NONCONVERT)
                    Key_NONCONVERT = true;
                if (scanCode == S_ACCEPT)
                    Key_ACCEPT = true;
                if (scanCode == S_MODECHANGE)
                    Key_MODECHANGE = true;
                if (scanCode == S_Space)
                    Key_Space = true;
                if (scanCode == S_PRIOR)
                    Key_PRIOR = true;
                if (scanCode == S_NEXT)
                    Key_NEXT = true;
                if (scanCode == S_END)
                    Key_END = true;
                if (scanCode == S_HOME)
                    Key_HOME = true;
                if (scanCode == S_LEFT)
                    Key_LEFT = true;
                if (scanCode == S_UP)
                    Key_UP = true;
                if (scanCode == S_RIGHT)
                    Key_RIGHT = true;
                if (scanCode == S_DOWN)
                    Key_DOWN = true;
                if (scanCode == S_SELECT)
                    Key_SELECT = true;
                if (scanCode == S_PRINT)
                    Key_PRINT = true;
                if (scanCode == S_EXECUTE)
                    Key_EXECUTE = true;
                if (scanCode == S_SNAPSHOT)
                    Key_SNAPSHOT = true;
                if (scanCode == S_INSERT)
                    Key_INSERT = true;
                if (scanCode == S_DELETE)
                    Key_DELETE = true;
                if (scanCode == S_HELP)
                    Key_HELP = true;
                if (scanCode == S_APOSTROPHE)
                    Key_APOSTROPHE = true;
                if (scanCode == S_0)
                    Key_0 = true;
                if (scanCode == S_1)
                    Key_1 = true;
                if (scanCode == S_2)
                    Key_2 = true;
                if (scanCode == S_3)
                    Key_3 = true;
                if (scanCode == S_4)
                    Key_4 = true;
                if (scanCode == S_5)
                    Key_5 = true;
                if (scanCode == S_6)
                    Key_6 = true;
                if (scanCode == S_7)
                    Key_7 = true;
                if (scanCode == S_8)
                    Key_8 = true;
                if (scanCode == S_9)
                    Key_9 = true;
                if (scanCode == S_A)
                    Key_A = true;
                if (scanCode == S_B)
                    Key_B = true;
                if (scanCode == S_C)
                    Key_C = true;
                if (scanCode == S_D)
                    Key_D = true;
                if (scanCode == S_E)
                    Key_E = true;
                if (scanCode == S_F)
                    Key_F = true;
                if (scanCode == S_G)
                    Key_G = true;
                if (scanCode == S_H)
                    Key_H = true;
                if (scanCode == S_I)
                    Key_I = true;
                if (scanCode == S_J)
                    Key_J = true;
                if (scanCode == S_K)
                    Key_K = true;
                if (scanCode == S_L)
                    Key_L = true;
                if (scanCode == S_M)
                    Key_M = true;
                if (scanCode == S_N)
                    Key_N = true;
                if (scanCode == S_O)
                    Key_O = true;
                if (scanCode == S_P)
                    Key_P = true;
                if (scanCode == S_Q)
                    Key_Q = true;
                if (scanCode == S_R)
                    Key_R = true;
                if (scanCode == S_S)
                    Key_S = true;
                if (scanCode == S_T)
                    Key_T = true;
                if (scanCode == S_U)
                    Key_U = true;
                if (scanCode == S_V)
                    Key_V = true;
                if (scanCode == S_W)
                    Key_W = true;
                if (scanCode == S_X)
                    Key_X = true;
                if (scanCode == S_Y)
                    Key_Y = true;
                if (scanCode == S_Z)
                    Key_Z = true;
                if (scanCode == S_LWIN)
                    Key_LWIN = true;
                if (scanCode == S_RWIN)
                    Key_RWIN = true;
                if (scanCode == S_APPS)
                    Key_APPS = true;
                if (scanCode == S_SLEEP)
                    Key_SLEEP = true;
                if (scanCode == S_NUMPAD0)
                    Key_NUMPAD0 = true;
                if (scanCode == S_NUMPAD1)
                    Key_NUMPAD1 = true;
                if (scanCode == S_NUMPAD2)
                    Key_NUMPAD2 = true;
                if (scanCode == S_NUMPAD3)
                    Key_NUMPAD3 = true;
                if (scanCode == S_NUMPAD4)
                    Key_NUMPAD4 = true;
                if (scanCode == S_NUMPAD5)
                    Key_NUMPAD5 = true;
                if (scanCode == S_NUMPAD6)
                    Key_NUMPAD6 = true;
                if (scanCode == S_NUMPAD7)
                    Key_NUMPAD7 = true;
                if (scanCode == S_NUMPAD8)
                    Key_NUMPAD8 = true;
                if (scanCode == S_NUMPAD9)
                    Key_NUMPAD9 = true;
                if (scanCode == S_MULTIPLY)
                    Key_MULTIPLY = true;
                if (scanCode == S_ADD)
                    Key_ADD = true;
                if (scanCode == S_SEPARATOR)
                    Key_SEPARATOR = true;
                if (scanCode == S_SUBTRACT)
                    Key_SUBTRACT = true;
                if (scanCode == S_DECIMAL)
                    Key_DECIMAL = true;
                if (scanCode == S_DIVIDE)
                    Key_DIVIDE = true;
                if (scanCode == S_F1)
                    Key_F1 = true;
                if (scanCode == S_F2)
                    Key_F2 = true;
                if (scanCode == S_F3)
                    Key_F3 = true;
                if (scanCode == S_F4)
                    Key_F4 = true;
                if (scanCode == S_F5)
                    Key_F5 = true;
                if (scanCode == S_F6)
                    Key_F6 = true;
                if (scanCode == S_F7)
                    Key_F7 = true;
                if (scanCode == S_F8)
                    Key_F8 = true;
                if (scanCode == S_F9)
                    Key_F9 = true;
                if (scanCode == S_F10)
                    Key_F10 = true;
                if (scanCode == S_F11)
                    Key_F11 = true;
                if (scanCode == S_F12)
                    Key_F12 = true;
                if (scanCode == S_F13)
                    Key_F13 = true;
                if (scanCode == S_F14)
                    Key_F14 = true;
                if (scanCode == S_F15)
                    Key_F15 = true;
                if (scanCode == S_F16)
                    Key_F16 = true;
                if (scanCode == S_F17)
                    Key_F17 = true;
                if (scanCode == S_F18)
                    Key_F18 = true;
                if (scanCode == S_F19)
                    Key_F19 = true;
                if (scanCode == S_F20)
                    Key_F20 = true;
                if (scanCode == S_F21)
                    Key_F21 = true;
                if (scanCode == S_F22)
                    Key_F22 = true;
                if (scanCode == S_F23)
                    Key_F23 = true;
                if (scanCode == S_F24)
                    Key_F24 = true;
                if (scanCode == S_NUMLOCK)
                    Key_NUMLOCK = true;
                if (scanCode == S_SCROLL)
                    Key_SCROLL = true;
                if (scanCode == S_LeftShift)
                    Key_LeftShift = true;
                if (scanCode == S_RightShift)
                    Key_RightShift = true;
                if (scanCode == S_LeftControl)
                    Key_LeftControl = true;
                if (scanCode == S_RightControl)
                    Key_RightControl = true;
                if (scanCode == S_LMENU)
                    Key_LMENU = true;
                if (scanCode == S_RMENU)
                    Key_RMENU = true;
                if (scanCode == S_BROWSER_BACK)
                    Key_BROWSER_BACK = true;
                if (scanCode == S_BROWSER_FORWARD)
                    Key_BROWSER_FORWARD = true;
                if (scanCode == S_BROWSER_REFRESH)
                    Key_BROWSER_REFRESH = true;
                if (scanCode == S_BROWSER_STOP)
                    Key_BROWSER_STOP = true;
                if (scanCode == S_BROWSER_SEARCH)
                    Key_BROWSER_SEARCH = true;
                if (scanCode == S_BROWSER_FAVORITES)
                    Key_BROWSER_FAVORITES = true;
                if (scanCode == S_BROWSER_HOME)
                    Key_BROWSER_HOME = true;
                if (scanCode == S_VOLUME_MUTE)
                    Key_VOLUME_MUTE = true;
                if (scanCode == S_VOLUME_DOWN)
                    Key_VOLUME_DOWN = true;
                if (scanCode == S_VOLUME_UP)
                    Key_VOLUME_UP = true;
                if (scanCode == S_MEDIA_NEXT_TRACK)
                    Key_MEDIA_NEXT_TRACK = true;
                if (scanCode == S_MEDIA_PREV_TRACK)
                    Key_MEDIA_PREV_TRACK = true;
                if (scanCode == S_MEDIA_STOP)
                    Key_MEDIA_STOP = true;
                if (scanCode == S_MEDIA_PLAY_PAUSE)
                    Key_MEDIA_PLAY_PAUSE = true;
                if (scanCode == S_LAUNCH_MAIL)
                    Key_LAUNCH_MAIL = true;
                if (scanCode == S_LAUNCH_MEDIA_SELECT)
                    Key_LAUNCH_MEDIA_SELECT = true;
                if (scanCode == S_LAUNCH_APP1)
                    Key_LAUNCH_APP1 = true;
                if (scanCode == S_LAUNCH_APP2)
                    Key_LAUNCH_APP2 = true;
                if (scanCode == S_OEM_1)
                    Key_OEM_1 = true;
                if (scanCode == S_OEM_PLUS)
                    Key_OEM_PLUS = true;
                if (scanCode == S_OEM_COMMA)
                    Key_OEM_COMMA = true;
                if (scanCode == S_OEM_MINUS)
                    Key_OEM_MINUS = true;
                if (scanCode == S_OEM_PERIOD)
                    Key_OEM_PERIOD = true;
                if (scanCode == S_OEM_2)
                    Key_OEM_2 = true;
                if (scanCode == S_OEM_3)
                    Key_OEM_3 = true;
                if (scanCode == S_OEM_4)
                    Key_OEM_4 = true;
                if (scanCode == S_OEM_5)
                    Key_OEM_5 = true;
                if (scanCode == S_OEM_6)
                    Key_OEM_6 = true;
                if (scanCode == S_OEM_7)
                    Key_OEM_7 = true;
                if (scanCode == S_OEM_8)
                    Key_OEM_8 = true;
                if (scanCode == S_OEM_102)
                    Key_OEM_102 = true;
                if (scanCode == S_PROCESSKEY)
                    Key_PROCESSKEY = true;
                if (scanCode == S_PACKET)
                    Key_PACKET = true;
                if (scanCode == S_ATTN)
                    Key_ATTN = true;
                if (scanCode == S_CRSEL)
                    Key_CRSEL = true;
                if (scanCode == S_EXSEL)
                    Key_EXSEL = true;
                if (scanCode == S_EREOF)
                    Key_EREOF = true;
                if (scanCode == S_PLAY)
                    Key_PLAY = true;
                if (scanCode == S_ZOOM)
                    Key_ZOOM = true;
                if (scanCode == S_NONAME)
                    Key_NONAME = true;
                if (scanCode == S_PA1)
                    Key_PA1 = true;
                if (scanCode == S_OEM_CLEAR)
                    Key_OEM_CLEAR = true;
            }
            if (KeyboardHookButtonUp)
            {
                if (scanCode == S_LBUTTON)
                    Key_LBUTTON = false;
                if (scanCode == S_RBUTTON)
                    Key_RBUTTON = false;
                if (scanCode == S_CANCEL)
                    Key_CANCEL = false;
                if (scanCode == S_MBUTTON)
                    Key_MBUTTON = false;
                if (scanCode == S_XBUTTON1)
                    Key_XBUTTON1 = false;
                if (scanCode == S_XBUTTON2)
                    Key_XBUTTON2 = false;
                if (scanCode == S_BACK)
                    Key_BACK = false;
                if (scanCode == S_Tab)
                    Key_Tab = false;
                if (scanCode == S_CLEAR)
                    Key_CLEAR = false;
                if (scanCode == S_Return)
                    Key_Return = false;
                if (scanCode == S_SHIFT)
                    Key_SHIFT = false;
                if (scanCode == S_CONTROL)
                    Key_CONTROL = false;
                if (scanCode == S_MENU)
                    Key_MENU = false;
                if (scanCode == S_PAUSE)
                    Key_PAUSE = false;
                if (scanCode == S_CAPITAL)
                    Key_CAPITAL = false;
                if (scanCode == S_KANA)
                    Key_KANA = false;
                if (scanCode == S_HANGEUL)
                    Key_HANGEUL = false;
                if (scanCode == S_HANGUL)
                    Key_HANGUL = false;
                if (scanCode == S_JUNJA)
                    Key_JUNJA = false;
                if (scanCode == S_FINAL)
                    Key_FINAL = false;
                if (scanCode == S_HANJA)
                    Key_HANJA = false;
                if (scanCode == S_KANJI)
                    Key_KANJI = false;
                if (scanCode == S_Escape)
                    Key_Escape = false;
                if (scanCode == S_CONVERT)
                    Key_CONVERT = false;
                if (scanCode == S_NONCONVERT)
                    Key_NONCONVERT = false;
                if (scanCode == S_ACCEPT)
                    Key_ACCEPT = false;
                if (scanCode == S_MODECHANGE)
                    Key_MODECHANGE = false;
                if (scanCode == S_Space)
                    Key_Space = false;
                if (scanCode == S_PRIOR)
                    Key_PRIOR = false;
                if (scanCode == S_NEXT)
                    Key_NEXT = false;
                if (scanCode == S_END)
                    Key_END = false;
                if (scanCode == S_HOME)
                    Key_HOME = false;
                if (scanCode == S_LEFT)
                    Key_LEFT = false;
                if (scanCode == S_UP)
                    Key_UP = false;
                if (scanCode == S_RIGHT)
                    Key_RIGHT = false;
                if (scanCode == S_DOWN)
                    Key_DOWN = false;
                if (scanCode == S_SELECT)
                    Key_SELECT = false;
                if (scanCode == S_PRINT)
                    Key_PRINT = false;
                if (scanCode == S_EXECUTE)
                    Key_EXECUTE = false;
                if (scanCode == S_SNAPSHOT)
                    Key_SNAPSHOT = false;
                if (scanCode == S_INSERT)
                    Key_INSERT = false;
                if (scanCode == S_DELETE)
                    Key_DELETE = false;
                if (scanCode == S_HELP)
                    Key_HELP = false;
                if (scanCode == S_APOSTROPHE)
                    Key_APOSTROPHE = false;
                if (scanCode == S_0)
                    Key_0 = false;
                if (scanCode == S_1)
                    Key_1 = false;
                if (scanCode == S_2)
                    Key_2 = false;
                if (scanCode == S_3)
                    Key_3 = false;
                if (scanCode == S_4)
                    Key_4 = false;
                if (scanCode == S_5)
                    Key_5 = false;
                if (scanCode == S_6)
                    Key_6 = false;
                if (scanCode == S_7)
                    Key_7 = false;
                if (scanCode == S_8)
                    Key_8 = false;
                if (scanCode == S_9)
                    Key_9 = false;
                if (scanCode == S_A)
                    Key_A = false;
                if (scanCode == S_B)
                    Key_B = false;
                if (scanCode == S_C)
                    Key_C = false;
                if (scanCode == S_D)
                    Key_D = false;
                if (scanCode == S_E)
                    Key_E = false;
                if (scanCode == S_F)
                    Key_F = false;
                if (scanCode == S_G)
                    Key_G = false;
                if (scanCode == S_H)
                    Key_H = false;
                if (scanCode == S_I)
                    Key_I = false;
                if (scanCode == S_J)
                    Key_J = false;
                if (scanCode == S_K)
                    Key_K = false;
                if (scanCode == S_L)
                    Key_L = false;
                if (scanCode == S_M)
                    Key_M = false;
                if (scanCode == S_N)
                    Key_N = false;
                if (scanCode == S_O)
                    Key_O = false;
                if (scanCode == S_P)
                    Key_P = false;
                if (scanCode == S_Q)
                    Key_Q = false;
                if (scanCode == S_R)
                    Key_R = false;
                if (scanCode == S_S)
                    Key_S = false;
                if (scanCode == S_T)
                    Key_T = false;
                if (scanCode == S_U)
                    Key_U = false;
                if (scanCode == S_V)
                    Key_V = false;
                if (scanCode == S_W)
                    Key_W = false;
                if (scanCode == S_X)
                    Key_X = false;
                if (scanCode == S_Y)
                    Key_Y = false;
                if (scanCode == S_Z)
                    Key_Z = false;
                if (scanCode == S_LWIN)
                    Key_LWIN = false;
                if (scanCode == S_RWIN)
                    Key_RWIN = false;
                if (scanCode == S_APPS)
                    Key_APPS = false;
                if (scanCode == S_SLEEP)
                    Key_SLEEP = false;
                if (scanCode == S_NUMPAD0)
                    Key_NUMPAD0 = false;
                if (scanCode == S_NUMPAD1)
                    Key_NUMPAD1 = false;
                if (scanCode == S_NUMPAD2)
                    Key_NUMPAD2 = false;
                if (scanCode == S_NUMPAD3)
                    Key_NUMPAD3 = false;
                if (scanCode == S_NUMPAD4)
                    Key_NUMPAD4 = false;
                if (scanCode == S_NUMPAD5)
                    Key_NUMPAD5 = false;
                if (scanCode == S_NUMPAD6)
                    Key_NUMPAD6 = false;
                if (scanCode == S_NUMPAD7)
                    Key_NUMPAD7 = false;
                if (scanCode == S_NUMPAD8)
                    Key_NUMPAD8 = false;
                if (scanCode == S_NUMPAD9)
                    Key_NUMPAD9 = false;
                if (scanCode == S_MULTIPLY)
                    Key_MULTIPLY = false;
                if (scanCode == S_ADD)
                    Key_ADD = false;
                if (scanCode == S_SEPARATOR)
                    Key_SEPARATOR = false;
                if (scanCode == S_SUBTRACT)
                    Key_SUBTRACT = false;
                if (scanCode == S_DECIMAL)
                    Key_DECIMAL = false;
                if (scanCode == S_DIVIDE)
                    Key_DIVIDE = false;
                if (scanCode == S_F1)
                    Key_F1 = false;
                if (scanCode == S_F2)
                    Key_F2 = false;
                if (scanCode == S_F3)
                    Key_F3 = false;
                if (scanCode == S_F4)
                    Key_F4 = false;
                if (scanCode == S_F5)
                    Key_F5 = false;
                if (scanCode == S_F6)
                    Key_F6 = false;
                if (scanCode == S_F7)
                    Key_F7 = false;
                if (scanCode == S_F8)
                    Key_F8 = false;
                if (scanCode == S_F9)
                    Key_F9 = false;
                if (scanCode == S_F10)
                    Key_F10 = false;
                if (scanCode == S_F11)
                    Key_F11 = false;
                if (scanCode == S_F12)
                    Key_F12 = false;
                if (scanCode == S_F13)
                    Key_F13 = false;
                if (scanCode == S_F14)
                    Key_F14 = false;
                if (scanCode == S_F15)
                    Key_F15 = false;
                if (scanCode == S_F16)
                    Key_F16 = false;
                if (scanCode == S_F17)
                    Key_F17 = false;
                if (scanCode == S_F18)
                    Key_F18 = false;
                if (scanCode == S_F19)
                    Key_F19 = false;
                if (scanCode == S_F20)
                    Key_F20 = false;
                if (scanCode == S_F21)
                    Key_F21 = false;
                if (scanCode == S_F22)
                    Key_F22 = false;
                if (scanCode == S_F23)
                    Key_F23 = false;
                if (scanCode == S_F24)
                    Key_F24 = false;
                if (scanCode == S_NUMLOCK)
                    Key_NUMLOCK = false;
                if (scanCode == S_SCROLL)
                    Key_SCROLL = false;
                if (scanCode == S_LeftShift)
                    Key_LeftShift = false;
                if (scanCode == S_RightShift)
                    Key_RightShift = false;
                if (scanCode == S_LeftControl)
                    Key_LeftControl = false;
                if (scanCode == S_RightControl)
                    Key_RightControl = false;
                if (scanCode == S_LMENU)
                    Key_LMENU = false;
                if (scanCode == S_RMENU)
                    Key_RMENU = false;
                if (scanCode == S_BROWSER_BACK)
                    Key_BROWSER_BACK = false;
                if (scanCode == S_BROWSER_FORWARD)
                    Key_BROWSER_FORWARD = false;
                if (scanCode == S_BROWSER_REFRESH)
                    Key_BROWSER_REFRESH = false;
                if (scanCode == S_BROWSER_STOP)
                    Key_BROWSER_STOP = false;
                if (scanCode == S_BROWSER_SEARCH)
                    Key_BROWSER_SEARCH = false;
                if (scanCode == S_BROWSER_FAVORITES)
                    Key_BROWSER_FAVORITES = false;
                if (scanCode == S_BROWSER_HOME)
                    Key_BROWSER_HOME = false;
                if (scanCode == S_VOLUME_MUTE)
                    Key_VOLUME_MUTE = false;
                if (scanCode == S_VOLUME_DOWN)
                    Key_VOLUME_DOWN = false;
                if (scanCode == S_VOLUME_UP)
                    Key_VOLUME_UP = false;
                if (scanCode == S_MEDIA_NEXT_TRACK)
                    Key_MEDIA_NEXT_TRACK = false;
                if (scanCode == S_MEDIA_PREV_TRACK)
                    Key_MEDIA_PREV_TRACK = false;
                if (scanCode == S_MEDIA_STOP)
                    Key_MEDIA_STOP = false;
                if (scanCode == S_MEDIA_PLAY_PAUSE)
                    Key_MEDIA_PLAY_PAUSE = false;
                if (scanCode == S_LAUNCH_MAIL)
                    Key_LAUNCH_MAIL = false;
                if (scanCode == S_LAUNCH_MEDIA_SELECT)
                    Key_LAUNCH_MEDIA_SELECT = false;
                if (scanCode == S_LAUNCH_APP1)
                    Key_LAUNCH_APP1 = false;
                if (scanCode == S_LAUNCH_APP2)
                    Key_LAUNCH_APP2 = false;
                if (scanCode == S_OEM_1)
                    Key_OEM_1 = false;
                if (scanCode == S_OEM_PLUS)
                    Key_OEM_PLUS = false;
                if (scanCode == S_OEM_COMMA)
                    Key_OEM_COMMA = false;
                if (scanCode == S_OEM_MINUS)
                    Key_OEM_MINUS = false;
                if (scanCode == S_OEM_PERIOD)
                    Key_OEM_PERIOD = false;
                if (scanCode == S_OEM_2)
                    Key_OEM_2 = false;
                if (scanCode == S_OEM_3)
                    Key_OEM_3 = false;
                if (scanCode == S_OEM_4)
                    Key_OEM_4 = false;
                if (scanCode == S_OEM_5)
                    Key_OEM_5 = false;
                if (scanCode == S_OEM_6)
                    Key_OEM_6 = false;
                if (scanCode == S_OEM_7)
                    Key_OEM_7 = false;
                if (scanCode == S_OEM_8)
                    Key_OEM_8 = false;
                if (scanCode == S_OEM_102)
                    Key_OEM_102 = false;
                if (scanCode == S_PROCESSKEY)
                    Key_PROCESSKEY = false;
                if (scanCode == S_PACKET)
                    Key_PACKET = false;
                if (scanCode == S_ATTN)
                    Key_ATTN = false;
                if (scanCode == S_CRSEL)
                    Key_CRSEL = false;
                if (scanCode == S_EXSEL)
                    Key_EXSEL = false;
                if (scanCode == S_EREOF)
                    Key_EREOF = false;
                if (scanCode == S_PLAY)
                    Key_PLAY = false;
                if (scanCode == S_ZOOM)
                    Key_ZOOM = false;
                if (scanCode == S_NONAME)
                    Key_NONAME = false;
                if (scanCode == S_PA1)
                    Key_PA1 = false;
                if (scanCode == S_OEM_CLEAR)
                    Key_OEM_CLEAR = false;
            }
        }
        public static void WiimoteNunchuckData()
        {
            while (scriptrunning & wiimoteconnected)
            {
                try
                {
                    mStream.Read(aBuffer, 0, 22);
                }
                catch { }
            }
        }
        public static void JoyconsData()
        {
            while (scriptrunning)
            {
                if (joyconleftconnected)
                {
                    try
                    {
                        Lhid_read_timeout(handleLeft, report_bufLeft, (UIntPtr)report_lenLeft);
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1);
                    }
                }
                if (joyconrightconnected)
                {
                    try
                    {
                        Rhid_read_timeout(handleRight, report_bufRight, (UIntPtr)report_lenRight);
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1);
                    }
                }
            }
        }
        public static void ProControllerData()
        {
            while (scriptrunning & procontrollerconnected)
            {
                try
                {
                    Prohid_read_timeout(handlePro, report_bufPro, (UIntPtr)report_lenPro);
                }
                catch
                {
                    System.Threading.Thread.Sleep(1);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            width = Screen.PrimaryScreen.Bounds.Width;
            height = Screen.PrimaryScreen.Bounds.Height;
            if (scriptrunning)
            {
                if (joyconleftconnected)
                {
                    try
                    {
                        ExtractIMUValuesLeft();
                    }
                    catch { }
                }
                if (joyconrightconnected)
                {
                    try
                    {
                        ExtractIMUValuesRight();
                    }
                    catch { }
                }
                if (procontrollerconnected)
                {
                    try
                    {
                        ExtractIMUValuesPro();
                    }
                    catch { }
                }
                if (ps5controllerconneced)
                {
                    try
                    {
                        ExtractIMUValuesPS5();
                    }
                    catch { }
                }
                if (controllerconnected)
                { 
                    try
                    { 
                        Controller1ProcessIMU();
                    }
                    catch { }
                    try
                    {
                        Controller2ProcessIMU();
                    }
                    catch { }
                }
            }
        }
        private void CloseHandles()
        {
            if (joyconleftconnected)
                try
                {
                    Lhid_close(handleLeft);
                    handleLeft.Close();
                }
                catch { }
            if (joyconrightconnected)
                try
                {
                    Rhid_close(handleRight);
                    handleRight.Close();
                }
                catch { }
            if (procontrollerconnected)
                try
                {
                    Prohid_close(handlePro);
                    handlePro.Close();
                }
                catch { }
            if (wiimoteconnected)
                try
                {
                    mStream.Close();
                    handle.Close();
                }
                catch { }
        }
        public const string vendor_id = "57e", vendor_id_ = "057e", product_r1 = "0330", product_r2 = "0306", product_l = "2006", product_r = "2007", product_pro = "2009";
        public enum EFileAttributes : uint
        {
            Overlapped = 0x40000000,
            Normal = 0x80
        };
        struct SP_DEVICE_INTERFACE_DATA
        {
            public int cbSize;
            public Guid InterfaceClassGuid;
            public int Flags;
            public IntPtr RESERVED;
        }
        struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public UInt32 cbSize;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }
        [DllImport("MotionInputPairing.dll", EntryPoint = "joyconleftconnect")]
        public static extern bool joyconleftconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "joyconrightconnect")]
        public static extern bool joyconrightconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "wiimoteconnect")]
        public static extern bool wiimoteconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "joyconsconnect")]
        public static extern bool joyconsconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "wiimotejoyconleftconnect")]
        public static extern bool wiimotejoyconleftconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "wiimotejoyconrightconnect")]
        public static extern bool wiimotejoyconrightconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "joyconleftdisconnect")]
        public static extern bool joyconleftdisconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "joyconrightdisconnect")]
        public static extern bool joyconrightdisconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "wiimotedisconnect")]
        public static extern bool wiimotedisconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "procontrollerconnect")]
        public static extern bool procontrollerconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "procontrollerdisconnect")]
        public static extern bool procontrollerdisconnect();
        [DllImport("hid.dll")]
        public static extern void HidD_GetHidGuid(out Guid gHid);
        [DllImport("hid.dll")]
        public extern static bool HidD_SetOutputReport(IntPtr HidDeviceObject, byte[] lpReportBuffer, uint ReportBufferLength);
        [DllImport("setupapi.dll")]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, string Enumerator, IntPtr hwndParent, UInt32 Flags);
        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, IntPtr devInvo, ref Guid interfaceClassGuid, Int32 memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);
        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiGetDeviceInterfaceDetail(IntPtr hDevInfo, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData, UInt32 deviceInterfaceDetailDataSize, out UInt32 requiredSize, IntPtr deviceInfoData);
        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiGetDeviceInterfaceDetail(IntPtr hDevInfo, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData, UInt32 deviceInterfaceDetailDataSize, out UInt32 requiredSize, IntPtr deviceInfoData);
        [DllImport("Kernel32.dll")]
        public static extern SafeFileHandle CreateFile(string fileName, [MarshalAs(UnmanagedType.U4)] FileAccess fileAccess, [MarshalAs(UnmanagedType.U4)] FileShare fileShare, IntPtr securityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr template);
        [DllImport("Kernel32.dll")]
        public static extern IntPtr CreateFile(string fileName, System.IO.FileAccess fileAccess, System.IO.FileShare fileShare, IntPtr securityAttributes, System.IO.FileMode creationDisposition, EFileAttributes flags, IntPtr template);
        public static void FormCloseLeft()
        {
            try
            {
                Lhid_close(handleLeft);
                handleLeft.Close();
            }
            catch { }
            try
            {
                joyconleftdisconnect();
                joyconleftconnected = false;
            }
            catch { }
        }
        public static void FormCloseRight()
        {
            try
            {
                Rhid_close(handleRight);
                handleRight.Close();
            }
            catch { }
            try
            {
                joyconrightdisconnect();
                joyconrightconnected = false;
            }
            catch { }
        }
        public static void FormClosePro()
        {
            try
            {
                Prohid_close(handlePro);
                handlePro.Close();
            }
            catch { }
            try
            {
                procontrollerdisconnect();
                procontrollerconnected = false;
            }
            catch { }
        }
        [DllImport("lhidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Lhid_read_timeout")]
        public static extern int Lhid_read_timeout(SafeFileHandle dev, byte[] data, UIntPtr length);
        [DllImport("lhidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Lhid_write")]
        public static extern int Lhid_write(SafeFileHandle device, byte[] data, UIntPtr length);
        [DllImport("lhidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Lhid_open_path")]
        public static extern SafeFileHandle Lhid_open_path(IntPtr handle);
        [DllImport("lhidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Lhid_close")]
        public static extern void Lhid_close(SafeFileHandle device);
        public static void InitLeftJoycon()
        {
            try
            {
                stick_rawLeft[0] = report_bufLeft[6 + (ISLEFT ? 0 : 3)];
                stick_rawLeft[1] = report_bufLeft[7 + (ISLEFT ? 0 : 3)];
                stick_rawLeft[2] = report_bufLeft[8 + (ISLEFT ? 0 : 3)];
                stickCenterLeft[0] = (UInt16)(stick_rawLeft[0] | ((stick_rawLeft[1] & 0xf) << 8));
                stickCenterLeft[1] = (UInt16)((stick_rawLeft[1] >> 4) | (stick_rawLeft[2] << 4));
                acc_gcalibrationLeftX = (Int16)(report_bufLeft[13 + 0 * 12] | ((report_bufLeft[14 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[13 + 1 * 12] | ((report_bufLeft[14 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[13 + 2 * 12] | ((report_bufLeft[14 + 2 * 12] << 8) & 0xff00));
                acc_gcalibrationLeftY = (Int16)(report_bufLeft[15 + 0 * 12] | ((report_bufLeft[16 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[15 + 1 * 12] | ((report_bufLeft[16 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[15 + 2 * 12] | ((report_bufLeft[16 + 2 * 12] << 8) & 0xff00));
                acc_gcalibrationLeftZ = (Int16)(report_bufLeft[17 + 0 * 12] | ((report_bufLeft[18 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[17 + 1 * 12] | ((report_bufLeft[18 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[17 + 2 * 12] | ((report_bufLeft[18 + 2 * 12] << 8) & 0xff00));
            }
            catch { }
        }
        public static void ProcessButtonsLeftJoycon()
        {
            try
            {
                if (JoyconLeftStickCenter)
                {
                    stick_rawLeft[0] = report_bufLeft[6 + (ISLEFT ? 0 : 3)];
                    stick_rawLeft[1] = report_bufLeft[7 + (ISLEFT ? 0 : 3)];
                    stick_rawLeft[2] = report_bufLeft[8 + (ISLEFT ? 0 : 3)];
                    stickCenterLeft[0] = (UInt16)(stick_rawLeft[0] | ((stick_rawLeft[1] & 0xf) << 8));
                    stickCenterLeft[1] = (UInt16)((stick_rawLeft[1] >> 4) | (stick_rawLeft[2] << 4));
                }
                stick_rawLeft[0] = report_bufLeft[6 + (ISLEFT ? 0 : 3)];
                stick_rawLeft[1] = report_bufLeft[7 + (ISLEFT ? 0 : 3)];
                stick_rawLeft[2] = report_bufLeft[8 + (ISLEFT ? 0 : 3)];
                stickLeft[0] = ((UInt16)(stick_rawLeft[0] | ((stick_rawLeft[1] & 0xf) << 8)) - stickCenterLeft[0]) / 1440f;
                stickLeft[1] = ((UInt16)((stick_rawLeft[1] >> 4) | (stick_rawLeft[2] << 4)) - stickCenterLeft[1]) / 1440f;
                JoyconLeftStickX = stickLeft[0];
                JoyconLeftStickY = stickLeft[1];
                acc_gLeft.X = ((Int16)(report_bufLeft[13 + 0 * 12] | ((report_bufLeft[14 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[13 + 1 * 12] | ((report_bufLeft[14 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[13 + 2 * 12] | ((report_bufLeft[14 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationLeftX) * (1.0f / 12000f);
                acc_gLeft.Y = -((Int16)(report_bufLeft[15 + 0 * 12] | ((report_bufLeft[16 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[15 + 1 * 12] | ((report_bufLeft[16 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[15 + 2 * 12] | ((report_bufLeft[16 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationLeftY) * (1.0f / 12000f);
                acc_gLeft.Z = -((Int16)(report_bufLeft[17 + 0 * 12] | ((report_bufLeft[18 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[17 + 1 * 12] | ((report_bufLeft[18 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufLeft[17 + 2 * 12] | ((report_bufLeft[18 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationLeftZ) * (1.0f / 12000f);
                JoyconLeftButtonSHOULDER_1 = (report_bufLeft[3 + (ISLEFT ? 2 : 0)] & 0x40) != 0;
                JoyconLeftButtonSHOULDER_2 = (report_bufLeft[3 + (ISLEFT ? 2 : 0)] & 0x80) != 0;
                JoyconLeftButtonSR = (report_bufLeft[3 + (ISLEFT ? 2 : 0)] & 0x10) != 0;
                JoyconLeftButtonSL = (report_bufLeft[3 + (ISLEFT ? 2 : 0)] & 0x20) != 0;
                JoyconLeftButtonDPAD_DOWN = (report_bufLeft[3 + (ISLEFT ? 2 : 0)] & (ISLEFT ? 0x01 : 0x04)) != 0;
                JoyconLeftButtonDPAD_RIGHT = (report_bufLeft[3 + (ISLEFT ? 2 : 0)] & (ISLEFT ? 0x04 : 0x08)) != 0;
                JoyconLeftButtonDPAD_UP = (report_bufLeft[3 + (ISLEFT ? 2 : 0)] & (ISLEFT ? 0x02 : 0x02)) != 0;
                JoyconLeftButtonDPAD_LEFT = (report_bufLeft[3 + (ISLEFT ? 2 : 0)] & (ISLEFT ? 0x08 : 0x01)) != 0;
                JoyconLeftButtonMINUS = (report_bufLeft[4] & 0x01) != 0;
                JoyconLeftButtonCAPTURE = (report_bufLeft[4] & 0x20) != 0;
                JoyconLeftButtonSTICK = (report_bufLeft[4] & (ISLEFT ? 0x08 : 0x04)) != 0;
                JoyconLeftButtonACC = acc_gLeft.X <= -1.13;
                JoyconLeftButtonSMA = JoyconLeftButtonSL | JoyconLeftButtonSR | JoyconLeftButtonMINUS | JoyconLeftButtonACC;
                if (LeftValListY.Count >= 50)
                {
                    LeftValListY.RemoveAt(0);
                    LeftValListY.Add(acc_gLeft.Y);
                }
                else
                    LeftValListY.Add(acc_gLeft.Y);
                JoyconLeftRollLeft = LeftValListY.Average() <= -0.75f;
                JoyconLeftRollRight = LeftValListY.Average() >= 0.75f;
                if (JoyconLeftAccelCenter)
                    InitDirectAnglesLeft = acc_gLeft;
                DirectAnglesLeft = acc_gLeft - InitDirectAnglesLeft;
                JoyconLeftAccelX = DirectAnglesLeft.X * 1350f;
                JoyconLeftAccelY = -DirectAnglesLeft.Y * 1350f;
                if (gyromode != 1 & gyromode != 2)
                {
                    gyr_gLeft.X = ((Int16)(report_bufLeft[19 + 0 * 12] | ((report_bufLeft[20 + 0 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufLeft[19 + 1 * 12] | ((report_bufLeft[20 + 1 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufLeft[19 + 2 * 12] | ((report_bufLeft[20 + 2 * 12] << 8) & 0xff00)) - 20);
                    gyr_gLeft.Y = ((Int16)(report_bufLeft[21 + 0 * 12] | ((report_bufLeft[22 + 0 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufLeft[21 + 1 * 12] | ((report_bufLeft[22 + 1 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufLeft[21 + 2 * 12] | ((report_bufLeft[22 + 2 * 12] << 8) & 0xff00)) - 4);
                    gyr_gLeft.Z = ((Int16)(report_bufLeft[23 + 0 * 12] | ((report_bufLeft[24 + 0 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufLeft[23 + 1 * 12] | ((report_bufLeft[24 + 1 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufLeft[23 + 2 * 12] | ((report_bufLeft[24 + 2 * 12] << 8) & 0xff00)) - 18);
                    JoyconLeftGyroX = gyr_gLeft.Z;
                    JoyconLeftGyroY = gyr_gLeft.Y;
                }
            }
            catch { }
        }
        public static Quaternion GetVectorLeft(Vector3 i_Left, Vector3 j_Left, Vector3 k_Left)
        {
            Vector3 v1 = new Vector3(j_Left.X, i_Left.X, k_Left.X);
            Vector3 v2 = -new Vector3(j_Left.Z, i_Left.Z, k_Left.Z);
            return QuaternionLookRotationLeft(v1, v2);
        }
        private static Quaternion QuaternionLookRotationLeft(Vector3 forward, Vector3 up)
        {
            Vector3 vector = forward;
            Vector3 vector2 = Vector3.Cross(up, vector);
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            Quaternion quaternion = new Quaternion();
            double m00 = vector2.X;
            double m01 = vector2.Y;
            double m02 = vector2.Z;
            double m10 = vector3.X;
            double m11 = vector3.Y;
            double m12 = vector3.Z;
            double m20 = vector.X;
            double m21 = vector.Y;
            double m22 = vector.Z;
            double num8 = m00 + m11 + m22;
            double num = Math.Sqrt(num8 + 1f);
            quaternion.W = (float)num * 0.5f;
            num = 0.5f / num;
            quaternion.X = (float)(m12 - m21) * (float)num;
            quaternion.Y = (float)(m20 - m02) * (float)num;
            quaternion.Z = (float)(m01 - m10) * (float)num;
            return quaternion;
        }
        private static Vector3 ToEulerAnglesLeft(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;
            pitchYawRoll.X = (float)Math.Asin(2f * test / unit);
            pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);
            pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);
            return pitchYawRoll;
        }
        private void ExtractIMUValuesLeft()
        {
            if (gyromode == 1 | gyromode == 2)
            {
                gyr_gLeft.X = (float)Math.Round(((Int16)(report_bufLeft[19 + 0 * 12] | ((report_bufLeft[20 + 0 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufLeft[19 + 1 * 12] | ((report_bufLeft[20 + 1 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufLeft[19 + 2 * 12] | ((report_bufLeft[20 + 2 * 12] << 8) & 0xff00)) - 20) / 666666666f, 7);
                gyr_gLeft.Y = (float)Math.Round(((Int16)(report_bufLeft[21 + 0 * 12] | ((report_bufLeft[22 + 0 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufLeft[21 + 1 * 12] | ((report_bufLeft[22 + 1 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufLeft[21 + 2 * 12] | ((report_bufLeft[22 + 2 * 12] << 8) & 0xff00)) - 4) / 666666666f, 7);
                gyr_gLeft.Z = (float)Math.Round(((Int16)(report_bufLeft[23 + 0 * 12] | ((report_bufLeft[24 + 0 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufLeft[23 + 1 * 12] | ((report_bufLeft[24 + 1 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufLeft[23 + 2 * 12] | ((report_bufLeft[24 + 2 * 12] << 8) & 0xff00)) - 18) / 666666666f, 7);
                i_aLeft = new Vector3(1, 0, 0);
                j_aLeft = new Vector3(0, 1, 0);
                k_aLeft.Y = 0f;
                k_aLeft.Z = 1f;
                i_bLeft = new Vector3(1, 0, 0);
                j_bLeft.Y = 1f;
                k_bLeft = new Vector3(0, 0, 1);
                if (gyromode == 1)
                {
                    k_aCrossLeft += Vector3.Cross(gyr_gLeft, Vector3.Normalize(k_aLeft));
                    j_bCrossLeft += Vector3.Cross(gyr_gLeft, Vector3.Normalize(j_bLeft));
                    EulerAnglesaLeft = ToEulerAnglesLeft(GetVectorLeft(Vector3.Normalize(i_aLeft), Vector3.Normalize(j_aLeft), Vector3.Normalize(k_aCrossLeft))) - InitEulerAnglesaLeft;
                    EulerAnglesbLeft = Vector3.Normalize(ToEulerAnglesLeft(GetVectorLeft(Vector3.Normalize(i_bLeft), Vector3.Normalize(j_bCrossLeft), Vector3.Normalize(k_bLeft)))) - InitEulerAnglesbLeft;
                    JoyconLeftGyroX = (EulerAnglesbLeft.X - EulerAnglesbLeft.Y) * 22222222f;
                    JoyconLeftGyroY = EulerAnglesaLeft.Z * 22222222f;
                    if (JoyconLeftGyroCenter | (int)JoyconLeftGyroX == 0)
                    {
                        j_bCrossLeft = new Vector3(0, 1, 0);
                        j_bLeft.X = 0f;
                        j_bLeft.Z = 0f;
                        InitEulerAnglesbLeft = Vector3.Normalize(ToEulerAnglesLeft(GetVectorLeft(Vector3.Normalize(i_bLeft), Vector3.Normalize(j_bCrossLeft), Vector3.Normalize(k_bLeft))));
                    }
                    if (JoyconLeftGyroCenter | (int)JoyconLeftGyroY == 0)
                    {
                        k_aCrossLeft = new Vector3(0, 0, 1);
                        k_aLeft.X = 0f;
                        InitEulerAnglesaLeft = ToEulerAnglesLeft(GetVectorLeft(Vector3.Normalize(i_aLeft), Vector3.Normalize(j_aLeft), Vector3.Normalize(k_aCrossLeft)));
                    }
                }
                else if (gyromode == 2)
                {
                    k_aCrossLeft = Vector3.Cross(gyr_gLeft, (k_aLeft)) * 10f;
                    j_bCrossLeft = Vector3.Cross(gyr_gLeft, (j_bLeft)) * 10f;
                    EulerAnglesaLeft = ToEulerAnglesLeft(GetVectorLeft((i_aLeft), (j_aLeft), (k_aCrossLeft))) - InitEulerAnglesaLeft;
                    EulerAnglesbLeft = (ToEulerAnglesLeft(GetVectorLeft((i_bLeft), (j_bCrossLeft), (k_bLeft)))) - InitEulerAnglesbLeft;
                    JoyconLeftGyroX = (EulerAnglesbLeft.X - EulerAnglesbLeft.Y) * 22222222f;
                    JoyconLeftGyroY = EulerAnglesaLeft.Z * 22222222f;
                    if (JoyconLeftGyroCenter | (int)JoyconLeftGyroX == 0)
                    {
                        j_bCrossLeft = new Vector3(0, 1, 0);
                        j_bLeft.X = 0f;
                        j_bLeft.Z = 0f;
                        InitEulerAnglesbLeft = (ToEulerAnglesLeft(GetVectorLeft((i_bLeft), (j_bCrossLeft), (k_bLeft))));
                    }
                    if (JoyconLeftGyroCenter | (int)JoyconLeftGyroY == 0)
                    {
                        k_aCrossLeft = new Vector3(0, 0, 1);
                        k_aLeft.X = 0f;
                        InitEulerAnglesaLeft = ToEulerAnglesLeft(GetVectorLeft((i_aLeft), (j_aLeft), (k_aCrossLeft)));
                    }
                }
            }
        }
        public static bool JoyconLeftButtonSMA, JoyconLeftButtonACC, JoyconLeftRollLeft, JoyconLeftRollRight;
        private static double JoyconLeftStickX, JoyconLeftStickY;
        public static System.Collections.Generic.List<double> LeftValListX = new System.Collections.Generic.List<double>(), LeftValListY = new System.Collections.Generic.List<double>();
        public static bool JoyconLeftGyroCenter, JoyconLeftAccelCenter, JoyconLeftStickCenter;
        public static double JoyconLeftAccelX, JoyconLeftAccelY, JoyconLeftGyroX, JoyconLeftGyroY;
        public static Vector3 InitEulerAnglesaLeft, EulerAnglesaLeft, InitEulerAnglesbLeft, EulerAnglesbLeft;
        public static Vector3 gyr_gLeft = new Vector3();
        public static Vector3 i_aLeft = new Vector3(1, 0, 0);
        public static Vector3 j_aLeft = new Vector3(0, 1, 0);
        public static Vector3 k_aLeft = new Vector3(0, 0, 1);
        public static Vector3 k_aCrossLeft = new Vector3(0, 0, 1);
        public static Vector3 i_bLeft = new Vector3(1, 0, 0);
        public static Vector3 j_bLeft = new Vector3(0, 1, 0);
        public static Vector3 j_bCrossLeft = new Vector3(0, 1, 0);
        public static Vector3 k_bLeft = new Vector3(0, 0, 1);
        private static double[] stickLeft = { 0, 0 };
        private static double[] stickCenterLeft = { 0, 0 };
        private static byte[] stick_rawLeft = { 0, 0, 0 };
        private static UInt16[] stick_calLeft = { 0, 0, 0, 0, 0, 0 };
        public static SafeFileHandle handleLeft;
        public static Vector3 acc_gLeft = new Vector3();
        public const uint report_lenLeft = 49;
        public static Vector3 InitDirectAnglesLeft, DirectAnglesLeft;
        public static bool JoyconLeftButtonSHOULDER_1, JoyconLeftButtonSHOULDER_2, JoyconLeftButtonSR, JoyconLeftButtonSL, JoyconLeftButtonDPAD_DOWN, JoyconLeftButtonDPAD_RIGHT, JoyconLeftButtonDPAD_UP, JoyconLeftButtonDPAD_LEFT, JoyconLeftButtonMINUS, JoyconLeftButtonSTICK, JoyconLeftButtonCAPTURE, ISLEFT;
        public static byte[] report_bufLeft = new byte[report_lenLeft];
        public static float acc_gcalibrationLeftX, acc_gcalibrationLeftY, acc_gcalibrationLeftZ;
        public static bool ScanLeft()
        {
            int index = 0;
            System.Guid guid;
            HidD_GetHidGuid(out guid);
            System.IntPtr hDevInfo = SetupDiGetClassDevs(ref guid, null, new System.IntPtr(), 0x00000010);
            SP_DEVICE_INTERFACE_DATA diData = new SP_DEVICE_INTERFACE_DATA();
            diData.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(diData);
            while (SetupDiEnumDeviceInterfaces(hDevInfo, new System.IntPtr(), ref guid, index, ref diData))
            {
                System.UInt32 size;
                SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, new System.IntPtr(), 0, out size, new System.IntPtr());
                SP_DEVICE_INTERFACE_DETAIL_DATA diDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                diDetail.cbSize = 5;
                if (SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, ref diDetail, size, out size, new System.IntPtr()))
                {
                    if ((diDetail.DevicePath.Contains(vendor_id) | diDetail.DevicePath.Contains(vendor_id_)) & diDetail.DevicePath.Contains(product_l))
                    {
                        ISLEFT = true;
                        AttachJoyLeft(diDetail.DevicePath);
                        return true;
                    }
                }
                index++;
            }
            return false;
        }
        public static void AttachJoyLeft(string path)
        {
            do
            {
                IntPtr handle = CreateFile(path, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite, new System.IntPtr(), System.IO.FileMode.Open, EFileAttributes.Normal, new System.IntPtr());
                handleLeft = Lhid_open_path(handle);
                SubcommandLeft(0x40, new byte[] { 0x1 }, 1);
                SubcommandLeft(0x3, new byte[] { 0x30 }, 1);
            }
            while (handleLeft.IsInvalid);
        }
        public static void SubcommandLeft(byte sc, byte[] buf, uint len)
        {
            byte[] buf_Left = new byte[report_lenLeft];
            System.Array.Copy(buf, 0, buf_Left, 11, len);
            buf_Left[10] = sc;
            buf_Left[1] = 0;
            buf_Left[0] = 0x1;
            Lhid_write(handleLeft, buf_Left, (UIntPtr)(len + 11));
            Lhid_read_timeout(handleLeft, buf_Left, (UIntPtr)report_lenLeft);
        }
        [DllImport("rhidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Rhid_read_timeout")]
        public static extern int Rhid_read_timeout(SafeFileHandle dev, byte[] data, UIntPtr length);
        [DllImport("rhidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Rhid_write")]
        public static extern int Rhid_write(SafeFileHandle device, byte[] data, UIntPtr length);
        [DllImport("rhidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Rhid_open_path")]
        public static extern SafeFileHandle Rhid_open_path(IntPtr handle);
        [DllImport("rhidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Rhid_close")]
        public static extern void Rhid_close(SafeFileHandle device);
        public static void InitRightJoycon()
        {
            try
            {
                stick_rawRight[0] = report_bufRight[6 + (!ISRIGHT ? 0 : 3)];
                stick_rawRight[1] = report_bufRight[7 + (!ISRIGHT ? 0 : 3)];
                stick_rawRight[2] = report_bufRight[8 + (!ISRIGHT ? 0 : 3)];
                stickCenterRight[0] = (UInt16)(stick_rawRight[0] | ((stick_rawRight[1] & 0xf) << 8));
                stickCenterRight[1] = (UInt16)((stick_rawRight[1] >> 4) | (stick_rawRight[2] << 4));
                acc_gcalibrationRightX = (Int16)(report_bufRight[13 + 0 * 12] | ((report_bufRight[14 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[13 + 1 * 12] | ((report_bufRight[14 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[13 + 2 * 12] | ((report_bufRight[14 + 2 * 12] << 8) & 0xff00));
                acc_gcalibrationRightY = (Int16)(report_bufRight[15 + 0 * 12] | ((report_bufRight[16 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[15 + 1 * 12] | ((report_bufRight[16 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[15 + 2 * 12] | ((report_bufRight[16 + 2 * 12] << 8) & 0xff00));
                acc_gcalibrationRightZ = (Int16)(report_bufRight[17 + 0 * 12] | ((report_bufRight[18 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[17 + 1 * 12] | ((report_bufRight[18 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[17 + 2 * 12] | ((report_bufRight[18 + 2 * 12] << 8) & 0xff00));
            }
            catch { }
        }
        public static void ProcessButtonsRightJoycon()
        {
            try
            {
                if (JoyconRightStickCenter)
                {
                    stick_rawRight[0] = report_bufRight[6 + (!ISRIGHT ? 0 : 3)];
                    stick_rawRight[1] = report_bufRight[7 + (!ISRIGHT ? 0 : 3)];
                    stick_rawRight[2] = report_bufRight[8 + (!ISRIGHT ? 0 : 3)];
                    stickCenterRight[0] = (UInt16)(stick_rawRight[0] | ((stick_rawRight[1] & 0xf) << 8));
                    stickCenterRight[1] = (UInt16)((stick_rawRight[1] >> 4) | (stick_rawRight[2] << 4));
                }
                stick_rawRight[0] = report_bufRight[6 + (!ISRIGHT ? 0 : 3)];
                stick_rawRight[1] = report_bufRight[7 + (!ISRIGHT ? 0 : 3)];
                stick_rawRight[2] = report_bufRight[8 + (!ISRIGHT ? 0 : 3)];
                stickRight[0] = ((UInt16)(stick_rawRight[0] | ((stick_rawRight[1] & 0xf) << 8)) - stickCenterRight[0]) / 1440f;
                stickRight[1] = ((UInt16)((stick_rawRight[1] >> 4) | (stick_rawRight[2] << 4)) - stickCenterRight[1]) / 1440f;
                JoyconRightStickX = -stickRight[0];
                JoyconRightStickY = -stickRight[1];
                acc_gRight.X = ((Int16)(report_bufRight[13 + 0 * 12] | ((report_bufRight[14 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[13 + 1 * 12] | ((report_bufRight[14 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[13 + 2 * 12] | ((report_bufRight[14 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationRightX) * (1.0f / 12000f);
                acc_gRight.Y = -((Int16)(report_bufRight[15 + 0 * 12] | ((report_bufRight[16 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[15 + 1 * 12] | ((report_bufRight[16 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[15 + 2 * 12] | ((report_bufRight[16 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationRightY) * (1.0f / 12000f);
                acc_gRight.Z = -((Int16)(report_bufRight[17 + 0 * 12] | ((report_bufRight[18 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[17 + 1 * 12] | ((report_bufRight[18 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufRight[17 + 2 * 12] | ((report_bufRight[18 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationRightZ) * (1.0f / 12000f);
                JoyconRightButtonSHOULDER_1 = (report_bufRight[3 + (!ISRIGHT ? 2 : 0)] & 0x40) != 0;
                JoyconRightButtonSHOULDER_2 = (report_bufRight[3 + (!ISRIGHT ? 2 : 0)] & 0x80) != 0;
                JoyconRightButtonSR = (report_bufRight[3 + (!ISRIGHT ? 2 : 0)] & 0x10) != 0;
                JoyconRightButtonSL = (report_bufRight[3 + (!ISRIGHT ? 2 : 0)] & 0x20) != 0;
                JoyconRightButtonDPAD_DOWN = (report_bufRight[3 + (!ISRIGHT ? 2 : 0)] & (!ISRIGHT ? 0x01 : 0x04)) != 0;
                JoyconRightButtonDPAD_RIGHT = (report_bufRight[3 + (!ISRIGHT ? 2 : 0)] & (!ISRIGHT ? 0x04 : 0x08)) != 0;
                JoyconRightButtonDPAD_UP = (report_bufRight[3 + (!ISRIGHT ? 2 : 0)] & (!ISRIGHT ? 0x02 : 0x02)) != 0;
                JoyconRightButtonDPAD_LEFT = (report_bufRight[3 + (!ISRIGHT ? 2 : 0)] & (!ISRIGHT ? 0x08 : 0x01)) != 0;
                JoyconRightButtonPLUS = ((report_bufRight[4] & 0x02) != 0);
                JoyconRightButtonHOME = ((report_bufRight[4] & 0x10) != 0);
                JoyconRightButtonSTICK = ((report_bufRight[4] & (!ISRIGHT ? 0x08 : 0x04)) != 0);
                JoyconRightButtonACC = acc_gRight.X <= -1.13;
                JoyconRightButtonSPA = JoyconRightButtonSL | JoyconRightButtonSR | JoyconRightButtonPLUS | JoyconRightButtonACC;
                if (RightValListY.Count >= 50)
                {
                    RightValListY.RemoveAt(0);
                    RightValListY.Add(acc_gRight.Y);
                }
                else
                    RightValListY.Add(acc_gRight.Y);
                JoyconRightRollLeft = RightValListY.Average() <= -0.75f;
                JoyconRightRollRight = RightValListY.Average() >= 0.75f;
                if (JoyconRightAccelCenter)
                    InitDirectAnglesRight = acc_gRight;
                DirectAnglesRight = acc_gRight - InitDirectAnglesRight;
                JoyconRightAccelX = DirectAnglesRight.X * 1350f;
                JoyconRightAccelY = -DirectAnglesRight.Y * 1350f;
                if (gyromode != 1 & gyromode != 2)
                {
                    gyr_gRight.X = ((Int16)(report_bufRight[19 + 0 * 12] | ((report_bufRight[20 + 0 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufRight[19 + 1 * 12] | ((report_bufRight[20 + 1 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufRight[19 + 2 * 12] | ((report_bufRight[20 + 2 * 12] << 8) & 0xff00)) - 20);
                    gyr_gRight.Y = ((Int16)(report_bufRight[21 + 0 * 12] | ((report_bufRight[22 + 0 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufRight[21 + 1 * 12] | ((report_bufRight[22 + 1 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufRight[21 + 2 * 12] | ((report_bufRight[22 + 2 * 12] << 8) & 0xff00)) - 4);
                    gyr_gRight.Z = ((Int16)(report_bufRight[23 + 0 * 12] | ((report_bufRight[24 + 0 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufRight[23 + 1 * 12] | ((report_bufRight[24 + 1 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufRight[23 + 2 * 12] | ((report_bufRight[24 + 2 * 12] << 8) & 0xff00)) - 18);
                    JoyconRightGyroX = gyr_gRight.Z;
                    JoyconRightGyroY = gyr_gRight.Y;
                }
            }
            catch { }
        }
        public static Quaternion GetVectorRight(Vector3 i_Right, Vector3 j_Right, Vector3 k_Right)
        {
            Vector3 v1 = new Vector3(j_Right.X, i_Right.X, k_Right.X);
            Vector3 v2 = -new Vector3(j_Right.Z, i_Right.Z, k_Right.Z);
            return QuaternionLookRotationRight(v1, v2);
        }
        private static Quaternion QuaternionLookRotationRight(Vector3 forward, Vector3 up)
        {
            Vector3 vector = forward;
            Vector3 vector2 = Vector3.Cross(up, vector);
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            Quaternion quaternion = new Quaternion();
            double m00 = vector2.X;
            double m01 = vector2.Y;
            double m02 = vector2.Z;
            double m10 = vector3.X;
            double m11 = vector3.Y;
            double m12 = vector3.Z;
            double m20 = vector.X;
            double m21 = vector.Y;
            double m22 = vector.Z;
            double num8 = m00 + m11 + m22;
            double num = Math.Sqrt(num8 + 1f);
            quaternion.W = (float)num * 0.5f;
            num = 0.5f / num;
            quaternion.X = (float)(m12 - m21) * (float)num;
            quaternion.Y = (float)(m20 - m02) * (float)num;
            quaternion.Z = (float)(m01 - m10) * (float)num;
            return quaternion;
        }
        private static Vector3 ToEulerAnglesRight(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;
            pitchYawRoll.X = (float)Math.Asin(2f * test / unit);
            pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);
            pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);
            return pitchYawRoll;
        }
        private void ExtractIMUValuesRight()
        {
            if (gyromode == 1 | gyromode == 2)
            {
                gyr_gRight.X = (float)Math.Round(((Int16)(report_bufRight[19 + 0 * 12] | ((report_bufRight[20 + 0 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufRight[19 + 1 * 12] | ((report_bufRight[20 + 1 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufRight[19 + 2 * 12] | ((report_bufRight[20 + 2 * 12] << 8) & 0xff00)) - 20) / 666666666f, 7);
                gyr_gRight.Y = (float)Math.Round(((Int16)(report_bufRight[21 + 0 * 12] | ((report_bufRight[22 + 0 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufRight[21 + 1 * 12] | ((report_bufRight[22 + 1 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufRight[21 + 2 * 12] | ((report_bufRight[22 + 2 * 12] << 8) & 0xff00)) - 4) / 666666666f, 7);
                gyr_gRight.Z = (float)Math.Round(((Int16)(report_bufRight[23 + 0 * 12] | ((report_bufRight[24 + 0 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufRight[23 + 1 * 12] | ((report_bufRight[24 + 1 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufRight[23 + 2 * 12] | ((report_bufRight[24 + 2 * 12] << 8) & 0xff00)) - 18) / 666666666f, 7);
                i_aRight = new Vector3(1, 0, 0);
                j_aRight = new Vector3(0, 1, 0);
                k_aRight.Y = 0f;
                k_aRight.Z = 1f;
                i_bRight = new Vector3(1, 0, 0);
                j_bRight.Y = 1f;
                k_bRight = new Vector3(0, 0, 1);
                if (gyromode == 1)
                {
                    k_aCrossRight += Vector3.Cross(gyr_gRight, Vector3.Normalize(k_aRight));
                    j_bCrossRight += Vector3.Cross(gyr_gRight, Vector3.Normalize(j_bRight));
                    EulerAnglesaRight = ToEulerAnglesRight(GetVectorRight(Vector3.Normalize(i_aRight), Vector3.Normalize(j_aRight), Vector3.Normalize(k_aCrossRight))) - InitEulerAnglesaRight;
                    EulerAnglesbRight = Vector3.Normalize(ToEulerAnglesRight(GetVectorRight(Vector3.Normalize(i_bRight), Vector3.Normalize(j_bCrossRight), Vector3.Normalize(k_bRight)))) - InitEulerAnglesbRight;
                    JoyconRightGyroX = (EulerAnglesbRight.X - EulerAnglesbRight.Y) * 22222222f;
                    JoyconRightGyroY = EulerAnglesaRight.Z * 22222222f;
                    if (JoyconRightGyroCenter | (int)JoyconRightGyroX == 0)
                    {
                        j_bCrossRight = new Vector3(0, 1, 0);
                        j_bRight.X = 0f;
                        j_bRight.Z = 0f;
                        InitEulerAnglesbRight = Vector3.Normalize(ToEulerAnglesRight(GetVectorRight(Vector3.Normalize(i_bRight), Vector3.Normalize(j_bCrossRight), Vector3.Normalize(k_bRight))));
                    }
                    if (JoyconRightGyroCenter | (int)JoyconRightGyroY == 0)
                    {
                        k_aCrossRight = new Vector3(0, 0, 1);
                        k_aRight.X = 0f;
                        InitEulerAnglesaRight = ToEulerAnglesRight(GetVectorRight(Vector3.Normalize(i_aRight), Vector3.Normalize(j_aRight), Vector3.Normalize(k_aCrossRight)));
                    }
                }
                else if (gyromode == 2)
                {
                    k_aCrossRight = Vector3.Cross(gyr_gRight, (k_aRight)) * 10f;
                    j_bCrossRight = Vector3.Cross(gyr_gRight, (j_bRight)) * 10f;
                    EulerAnglesaRight = ToEulerAnglesRight(GetVectorRight((i_aRight), (j_aRight), (k_aCrossRight))) - InitEulerAnglesaRight;
                    EulerAnglesbRight = (ToEulerAnglesRight(GetVectorRight((i_bRight), (j_bCrossRight), (k_bRight)))) - InitEulerAnglesbRight;
                    JoyconRightGyroX = (EulerAnglesbRight.X - EulerAnglesbRight.Y) * 22222222f;
                    JoyconRightGyroY = EulerAnglesaRight.Z * 22222222f;
                    if (JoyconRightGyroCenter | (int)JoyconRightGyroX == 0)
                    {
                        j_bCrossRight = new Vector3(0, 1, 0);
                        j_bRight.X = 0f;
                        j_bRight.Z = 0f;
                        InitEulerAnglesbRight = (ToEulerAnglesRight(GetVectorRight((i_bRight), (j_bCrossRight), (k_bRight))));
                    }
                    if (JoyconRightGyroCenter | (int)JoyconRightGyroY == 0)
                    {
                        k_aCrossRight = new Vector3(0, 0, 1);
                        k_aRight.X = 0f;
                        InitEulerAnglesaRight = ToEulerAnglesRight(GetVectorRight((i_aRight), (j_aRight), (k_aCrossRight)));
                    }
                }
            }
        }
        public static bool JoyconRightButtonSPA, JoyconRightButtonACC, JoyconRightRollLeft, JoyconRightRollRight;
        private static double JoyconRightStickX, JoyconRightStickY;
        public static System.Collections.Generic.List<double> RightValListX = new System.Collections.Generic.List<double>(), RightValListY = new System.Collections.Generic.List<double>();
        public static bool JoyconRightGyroCenter, JoyconRightAccelCenter, JoyconRightStickCenter;
        public static double JoyconRightAccelX, JoyconRightAccelY, JoyconRightGyroX, JoyconRightGyroY;
        public static Vector3 InitEulerAnglesaRight, EulerAnglesaRight, InitEulerAnglesbRight, EulerAnglesbRight;
        public static Vector3 gyr_gRight = new Vector3();
        public static Vector3 i_aRight = new Vector3(1, 0, 0);
        public static Vector3 j_aRight = new Vector3(0, 1, 0);
        public static Vector3 k_aRight = new Vector3(0, 0, 1);
        public static Vector3 k_aCrossRight = new Vector3(0, 0, 1);
        public static Vector3 i_bRight = new Vector3(1, 0, 0);
        public static Vector3 j_bRight = new Vector3(0, 1, 0);
        public static Vector3 j_bCrossRight = new Vector3(0, 1, 0);
        public static Vector3 k_bRight = new Vector3(0, 0, 1);
        private static double[] stickRight = { 0, 0 };
        private static double[] stickCenterRight = { 0, 0 };
        private static byte[] stick_rawRight = { 0, 0, 0 };
        private static UInt16[] stick_calRight = { 0, 0, 0, 0, 0, 0 };
        public static SafeFileHandle handleRight;
        public static Vector3 acc_gRight = new Vector3();
        public const uint report_lenRight = 49;
        public static Vector3 InitDirectAnglesRight, DirectAnglesRight;
        public static bool JoyconRightButtonSHOULDER_1, JoyconRightButtonSHOULDER_2, JoyconRightButtonSR, JoyconRightButtonSL, JoyconRightButtonDPAD_DOWN, JoyconRightButtonDPAD_RIGHT, JoyconRightButtonDPAD_UP, JoyconRightButtonDPAD_LEFT, JoyconRightButtonPLUS, JoyconRightButtonSTICK, JoyconRightButtonHOME, ISRIGHT;
        public static byte[] report_bufRight = new byte[report_lenRight];
        public static float acc_gcalibrationRightX, acc_gcalibrationRightY, acc_gcalibrationRightZ;
        public static bool ScanRight()
        {
            int index = 0;
            System.Guid guid;
            HidD_GetHidGuid(out guid);
            System.IntPtr hDevInfo = SetupDiGetClassDevs(ref guid, null, new System.IntPtr(), 0x00000010);
            SP_DEVICE_INTERFACE_DATA diData = new SP_DEVICE_INTERFACE_DATA();
            diData.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(diData);
            while (SetupDiEnumDeviceInterfaces(hDevInfo, new System.IntPtr(), ref guid, index, ref diData))
            {
                System.UInt32 size;
                SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, new System.IntPtr(), 0, out size, new System.IntPtr());
                SP_DEVICE_INTERFACE_DETAIL_DATA diDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                diDetail.cbSize = 5;
                if (SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, ref diDetail, size, out size, new System.IntPtr()))
                {
                    if ((diDetail.DevicePath.Contains(vendor_id) | diDetail.DevicePath.Contains(vendor_id_)) & diDetail.DevicePath.Contains(product_r))
                    {
                        ISRIGHT = true;
                        AttachJoyRight(diDetail.DevicePath);
                        return true;
                    }
                }
                index++;
            }
            return false;
        }
        public static void AttachJoyRight(string path)
        {
            do
            {
                IntPtr handle = CreateFile(path, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite, new System.IntPtr(), System.IO.FileMode.Open, EFileAttributes.Normal, new System.IntPtr());
                handleRight = Rhid_open_path(handle);
                SubcommandRight(0x40, new byte[] { 0x1 }, 1);
                SubcommandRight(0x3, new byte[] { 0x30 }, 1);
            }
            while (handleRight.IsInvalid);
        }
        public static void SubcommandRight(byte sc, byte[] buf, uint len)
        {
            byte[] buf_Right = new byte[report_lenRight];
            System.Array.Copy(buf, 0, buf_Right, 11, len);
            buf_Right[10] = sc;
            buf_Right[1] = 0;
            buf_Right[0] = 0x1;
            Rhid_write(handleRight, buf_Right, (UIntPtr)(len + 11));
            Rhid_read_timeout(handleRight, buf_Right, (UIntPtr)report_lenRight);
        }
        [DllImport("prohidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Prohid_read_timeout")]
        private static extern int Prohid_read_timeout(SafeFileHandle dev, byte[] data, UIntPtr length);
        [DllImport("prohidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Prohid_write")]
        private static extern int Prohid_write(SafeFileHandle device, byte[] data, UIntPtr length);
        [DllImport("prohidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Prohid_open_path")]
        private static extern SafeFileHandle Prohid_open_path(IntPtr handle);
        [DllImport("prohidread.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Prohid_close")]
        private static extern void Prohid_close(SafeFileHandle device);
        public static void InitProController()
        {
            try
            {
                stick_rawleftPro[0] = report_bufPro[6 + (ISPRO ? 0 : 3)];
                stick_rawleftPro[1] = report_bufPro[7 + (ISPRO ? 0 : 3)];
                stick_rawleftPro[2] = report_bufPro[8 + (ISPRO ? 0 : 3)];
                stickCenterleftPro[0] = (UInt16)(stick_rawleftPro[0] | ((stick_rawleftPro[1] & 0xf) << 8));
                stickCenterleftPro[1] = (UInt16)((stick_rawleftPro[1] >> 4) | (stick_rawleftPro[2] << 4));
                stick_rawrightPro[0] = report_bufPro[6 + (!ISPRO ? 0 : 3)];
                stick_rawrightPro[1] = report_bufPro[7 + (!ISPRO ? 0 : 3)];
                stick_rawrightPro[2] = report_bufPro[8 + (!ISPRO ? 0 : 3)];
                stickCenterrightPro[0] = (UInt16)(stick_rawrightPro[0] | ((stick_rawrightPro[1] & 0xf) << 8));
                stickCenterrightPro[1] = (UInt16)((stick_rawrightPro[1] >> 4) | (stick_rawrightPro[2] << 4));
                acc_gcalibrationProX = (Int16)(report_bufPro[13 + 0 * 12] | ((report_bufPro[14 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[13 + 1 * 12] | ((report_bufPro[14 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[13 + 2 * 12] | ((report_bufPro[14 + 2 * 12] << 8) & 0xff00));
                acc_gcalibrationProY = (Int16)(report_bufPro[15 + 0 * 12] | ((report_bufPro[16 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[15 + 1 * 12] | ((report_bufPro[16 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[15 + 2 * 12] | ((report_bufPro[16 + 2 * 12] << 8) & 0xff00));
                acc_gcalibrationProZ = (Int16)(report_bufPro[17 + 0 * 12] | ((report_bufPro[18 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[17 + 1 * 12] | ((report_bufPro[18 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[17 + 2 * 12] | ((report_bufPro[18 + 2 * 12] << 8) & 0xff00));
            }
            catch { }
        }
        public static void ProcessButtonsAndSticksPro()
        {
            try
            {
                if (ProControllerStickCenter)
                {
                    stick_rawleftPro[0] = report_bufPro[6 + (ISPRO ? 0 : 3)];
                    stick_rawleftPro[1] = report_bufPro[7 + (ISPRO ? 0 : 3)];
                    stick_rawleftPro[2] = report_bufPro[8 + (ISPRO ? 0 : 3)];
                    stickCenterleftPro[0] = (UInt16)(stick_rawleftPro[0] | ((stick_rawleftPro[1] & 0xf) << 8));
                    stickCenterleftPro[1] = (UInt16)((stick_rawleftPro[1] >> 4) | (stick_rawleftPro[2] << 4));
                    stick_rawrightPro[0] = report_bufPro[6 + (!ISPRO ? 0 : 3)];
                    stick_rawrightPro[1] = report_bufPro[7 + (!ISPRO ? 0 : 3)];
                    stick_rawrightPro[2] = report_bufPro[8 + (!ISPRO ? 0 : 3)];
                    stickCenterrightPro[0] = (UInt16)(stick_rawrightPro[0] | ((stick_rawrightPro[1] & 0xf) << 8));
                    stickCenterrightPro[1] = (UInt16)((stick_rawrightPro[1] >> 4) | (stick_rawrightPro[2] << 4));
                }
                stick_rawleftPro[0] = report_bufPro[6 + (ISPRO ? 0 : 3)];
                stick_rawleftPro[1] = report_bufPro[7 + (ISPRO ? 0 : 3)];
                stick_rawleftPro[2] = report_bufPro[8 + (ISPRO ? 0 : 3)];
                stickleftPro[0] = ((UInt16)(stick_rawleftPro[0] | ((stick_rawleftPro[1] & 0xf) << 8)) - stickCenterleftPro[0]) / 1440f;
                stickleftPro[1] = ((UInt16)((stick_rawleftPro[1] >> 4) | (stick_rawleftPro[2] << 4)) - stickCenterleftPro[1]) / 1440f;
                ProControllerLeftStickX = stickleftPro[0];
                ProControllerLeftStickY = stickleftPro[1];
                stick_rawrightPro[0] = report_bufPro[6 + (!ISPRO ? 0 : 3)];
                stick_rawrightPro[1] = report_bufPro[7 + (!ISPRO ? 0 : 3)];
                stick_rawrightPro[2] = report_bufPro[8 + (!ISPRO ? 0 : 3)];
                stickrightPro[0] = ((UInt16)(stick_rawrightPro[0] | ((stick_rawrightPro[1] & 0xf) << 8)) - stickCenterrightPro[0]) / 1440f;
                stickrightPro[1] = ((UInt16)((stick_rawrightPro[1] >> 4) | (stick_rawrightPro[2] << 4)) - stickCenterrightPro[1]) / 1440f;
                ProControllerRightStickX = -stickrightPro[0];
                ProControllerRightStickY = -stickrightPro[1];
                acc_gPro.X = ((Int16)(report_bufPro[13 + 0 * 12] | ((report_bufPro[14 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[13 + 1 * 12] | ((report_bufPro[14 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[13 + 2 * 12] | ((report_bufPro[14 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationProX) * (1.0f / 12000f);
                acc_gPro.Y = -((Int16)(report_bufPro[15 + 0 * 12] | ((report_bufPro[16 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[15 + 1 * 12] | ((report_bufPro[16 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[15 + 2 * 12] | ((report_bufPro[16 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationProY) * (1.0f / 12000f);
                acc_gPro.Z = -((Int16)(report_bufPro[17 + 0 * 12] | ((report_bufPro[18 + 0 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[17 + 1 * 12] | ((report_bufPro[18 + 1 * 12] << 8) & 0xff00)) + (Int16)(report_bufPro[17 + 2 * 12] | ((report_bufPro[18 + 2 * 12] << 8) & 0xff00)) - acc_gcalibrationProZ) * (1.0f / 12000f);
                ProControllerButtonSHOULDER_Left_1 = (report_bufPro[3 + (ISPRO ? 2 : 0)] & 0x40) != 0;
                ProControllerButtonSHOULDER_Left_2 = (report_bufPro[3 + (ISPRO ? 2 : 0)] & 0x80) != 0;
                ProControllerButtonDPAD_DOWN = (report_bufPro[3 + (ISPRO ? 2 : 0)] & (ISPRO ? 0x01 : 0x04)) != 0;
                ProControllerButtonDPAD_RIGHT = (report_bufPro[3 + (ISPRO ? 2 : 0)] & (ISPRO ? 0x04 : 0x08)) != 0;
                ProControllerButtonDPAD_UP = (report_bufPro[3 + (ISPRO ? 2 : 0)] & (ISPRO ? 0x02 : 0x02)) != 0;
                ProControllerButtonDPAD_LEFT = (report_bufPro[3 + (ISPRO ? 2 : 0)] & (ISPRO ? 0x08 : 0x01)) != 0;
                ProControllerButtonMINUS = (report_bufPro[4] & 0x01) != 0;
                ProControllerButtonCAPTURE = (report_bufPro[4] & 0x20) != 0;
                ProControllerButtonSTICK_Left = (report_bufPro[4] & (ISPRO ? 0x08 : 0x04)) != 0;
                ProControllerButtonACC = acc_gPro.X <= -1.13;
                ProControllerButtonSHOULDER_Right_1 = (report_bufPro[3 + (!ISPRO ? 2 : 0)] & 0x40) != 0;
                ProControllerButtonSHOULDER_Right_2 = (report_bufPro[3 + (!ISPRO ? 2 : 0)] & 0x80) != 0;
                ProControllerButtonA = (report_bufPro[3 + (!ISPRO ? 2 : 0)] & (!ISPRO ? 0x04 : 0x08)) != 0;
                ProControllerButtonB = (report_bufPro[3 + (!ISPRO ? 2 : 0)] & (!ISPRO ? 0x01 : 0x04)) != 0;
                ProControllerButtonX = (report_bufPro[3 + (!ISPRO ? 2 : 0)] & (!ISPRO ? 0x02 : 0x02)) != 0;
                ProControllerButtonY = (report_bufPro[3 + (!ISPRO ? 2 : 0)] & (!ISPRO ? 0x08 : 0x01)) != 0;
                ProControllerButtonPLUS = (report_bufPro[4] & 0x02) != 0;
                ProControllerButtonHOME = (report_bufPro[4] & 0x10) != 0;
                ProControllerButtonSTICK_Right = ((report_bufPro[4] & (!ISPRO ? 0x08 : 0x04)) != 0);
                if (ProValListY.Count >= 50)
                {
                    ProValListY.RemoveAt(0);
                    ProValListY.Add(acc_gPro.Y);
                }
                else
                    ProValListY.Add(acc_gPro.Y);
                ProControllerRollLeft = ProValListY.Average() <= -0.75f;
                ProControllerRollRight = ProValListY.Average() >= 0.75f;
                if (ProControllerAccelCenter)
                    InitDirectAnglesPro = acc_gPro;
                DirectAnglesPro = acc_gPro - InitDirectAnglesPro;
                ProControllerAccelX = DirectAnglesPro.X * 1350f;
                ProControllerAccelY = -DirectAnglesPro.Y * 1350f;
                if (gyromode != 1 & gyromode != 2)
                {
                    gyr_gPro.X = ((Int16)(report_bufPro[19 + 0 * 12] | ((report_bufPro[20 + 0 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufPro[19 + 1 * 12] | ((report_bufPro[20 + 1 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufPro[19 + 2 * 12] | ((report_bufPro[20 + 2 * 12] << 8) & 0xff00)) - 20);
                    gyr_gPro.Y = ((Int16)(report_bufPro[21 + 0 * 12] | ((report_bufPro[22 + 0 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufPro[21 + 1 * 12] | ((report_bufPro[22 + 1 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufPro[21 + 2 * 12] | ((report_bufPro[22 + 2 * 12] << 8) & 0xff00)) - 4);
                    gyr_gPro.Z = ((Int16)(report_bufPro[23 + 0 * 12] | ((report_bufPro[24 + 0 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufPro[23 + 1 * 12] | ((report_bufPro[24 + 1 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufPro[23 + 2 * 12] | ((report_bufPro[24 + 2 * 12] << 8) & 0xff00)) - 18);
                    ProControllerGyroX = gyr_gPro.Z;
                    ProControllerGyroY = gyr_gPro.Y;
                }
            }
            catch { }
        }
        public static Quaternion GetVectorPro(Vector3 i_Pro, Vector3 j_Pro, Vector3 k_Pro)
        {
            Vector3 v1 = new Vector3(j_Pro.X, i_Pro.X, k_Pro.X);
            Vector3 v2 = -new Vector3(j_Pro.Z, i_Pro.Z, k_Pro.Z);
            return QuaternionLookRotationPro(v1, v2);
        }
        private static Quaternion QuaternionLookRotationPro(Vector3 forward, Vector3 up)
        {
            Vector3 vector = forward;
            Vector3 vector2 = Vector3.Cross(up, vector);
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            Quaternion quaternion = new Quaternion();
            double m00 = vector2.X;
            double m01 = vector2.Y;
            double m02 = vector2.Z;
            double m10 = vector3.X;
            double m11 = vector3.Y;
            double m12 = vector3.Z;
            double m20 = vector.X;
            double m21 = vector.Y;
            double m22 = vector.Z;
            double num8 = m00 + m11 + m22;
            double num = Math.Sqrt(num8 + 1f);
            quaternion.W = (float)num * 0.5f;
            num = 0.5f / num;
            quaternion.X = (float)(m12 - m21) * (float)num;
            quaternion.Y = (float)(m20 - m02) * (float)num;
            quaternion.Z = (float)(m01 - m10) * (float)num;
            return quaternion;
        }
        private static Vector3 ToEulerAnglesPro(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;
            pitchYawRoll.X = (float)Math.Asin(2f * test / unit);
            pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);
            pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);
            return pitchYawRoll;
        }
        private void ExtractIMUValuesPro()
        {
            if (gyromode == 1 | gyromode == 2)
            {
                gyr_gPro.X = (float)Math.Round(((Int16)(report_bufPro[19 + 0 * 12] | ((report_bufPro[20 + 0 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufPro[19 + 1 * 12] | ((report_bufPro[20 + 1 * 12] << 8) & 0xff00)) - 20 + (Int16)(report_bufPro[19 + 2 * 12] | ((report_bufPro[20 + 2 * 12] << 8) & 0xff00)) - 20) / 666666666f, 7);
                gyr_gPro.Y = (float)Math.Round(((Int16)(report_bufPro[21 + 0 * 12] | ((report_bufPro[22 + 0 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufPro[21 + 1 * 12] | ((report_bufPro[22 + 1 * 12] << 8) & 0xff00)) - 4 + (Int16)(report_bufPro[21 + 2 * 12] | ((report_bufPro[22 + 2 * 12] << 8) & 0xff00)) - 4) / 666666666f, 7);
                gyr_gPro.Z = (float)Math.Round(((Int16)(report_bufPro[23 + 0 * 12] | ((report_bufPro[24 + 0 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufPro[23 + 1 * 12] | ((report_bufPro[24 + 1 * 12] << 8) & 0xff00)) - 18 + (Int16)(report_bufPro[23 + 2 * 12] | ((report_bufPro[24 + 2 * 12] << 8) & 0xff00)) - 18) / 666666666f, 7);
                i_aPro = new Vector3(1, 0, 0);
                j_aPro = new Vector3(0, 1, 0);
                k_aPro.Y = 0f;
                k_aPro.Z = 1f;
                i_bPro = new Vector3(1, 0, 0);
                j_bPro.Y = 1f;
                k_bPro = new Vector3(0, 0, 1);
                if (gyromode == 1)
                {
                    k_aCrossPro += Vector3.Cross(gyr_gPro, Vector3.Normalize(k_aPro));
                    j_bCrossPro += Vector3.Cross(gyr_gPro, Vector3.Normalize(j_bPro));
                    EulerAnglesaPro = ToEulerAnglesPro(GetVectorPro(Vector3.Normalize(i_aPro), Vector3.Normalize(j_aPro), Vector3.Normalize(k_aCrossPro))) - InitEulerAnglesaPro;
                    EulerAnglesbPro = Vector3.Normalize(ToEulerAnglesPro(GetVectorPro(Vector3.Normalize(i_bPro), Vector3.Normalize(j_bCrossPro), Vector3.Normalize(k_bPro)))) - InitEulerAnglesbPro;
                    ProControllerGyroX = (EulerAnglesbPro.X - EulerAnglesbPro.Y) * 22222222f;
                    ProControllerGyroY = EulerAnglesaPro.Z * 22222222f;
                    if (ProControllerGyroCenter | (int)ProControllerGyroX == 0)
                    {
                        j_bCrossPro = new Vector3(0, 1, 0);
                        j_bPro.X = 0f;
                        j_bPro.Z = 0f;
                        InitEulerAnglesbPro = Vector3.Normalize(ToEulerAnglesPro(GetVectorPro(Vector3.Normalize(i_bPro), Vector3.Normalize(j_bCrossPro), Vector3.Normalize(k_bPro))));
                    }
                    if (ProControllerGyroCenter | (int)ProControllerGyroY == 0)
                    {
                        k_aCrossPro = new Vector3(0, 0, 1);
                        k_aPro.X = 0f;
                        InitEulerAnglesaPro = ToEulerAnglesPro(GetVectorPro(Vector3.Normalize(i_aPro), Vector3.Normalize(j_aPro), Vector3.Normalize(k_aCrossPro)));
                    }
                }
                else if (gyromode == 2)
                {
                    k_aCrossPro = Vector3.Cross(gyr_gPro, (k_aPro)) * 10f;
                    j_bCrossPro = Vector3.Cross(gyr_gPro, (j_bPro)) * 10f;
                    EulerAnglesaPro = ToEulerAnglesPro(GetVectorPro((i_aPro), (j_aPro), (k_aCrossPro))) - InitEulerAnglesaPro;
                    EulerAnglesbPro = (ToEulerAnglesPro(GetVectorPro((i_bPro), (j_bCrossPro), (k_bPro)))) - InitEulerAnglesbPro;
                    ProControllerGyroX = (EulerAnglesbPro.X - EulerAnglesbPro.Y) * 22222222f;
                    ProControllerGyroY = EulerAnglesaPro.Z * 22222222f;
                    if (ProControllerGyroCenter | (int)ProControllerGyroX == 0)
                    {
                        j_bCrossPro = new Vector3(0, 1, 0);
                        j_bPro.X = 0f;
                        j_bPro.Z = 0f;
                        InitEulerAnglesbPro = (ToEulerAnglesPro(GetVectorPro((i_bPro), (j_bCrossPro), (k_bPro))));
                    }
                    if (ProControllerGyroCenter | (int)ProControllerGyroY == 0)
                    {
                        k_aCrossPro = new Vector3(0, 0, 1);
                        k_aPro.X = 0f;
                        InitEulerAnglesaPro = ToEulerAnglesPro(GetVectorPro((i_aPro), (j_aPro), (k_aCrossPro)));
                    }
                }
            }
        }
        public static bool ProControllerButtonACC, ProControllerRollLeft, ProControllerRollRight;
        private static double ProControllerLeftStickX, ProControllerLeftStickY, ProControllerRightStickX, ProControllerRightStickY;
        public static System.Collections.Generic.List<double> ProValListX = new System.Collections.Generic.List<double>(), ProValListY = new System.Collections.Generic.List<double>();
        public static bool ProControllerGyroCenter, ProControllerAccelCenter, ProControllerStickCenter;
        public static double ProControllerAccelX, ProControllerAccelY, ProControllerGyroX, ProControllerGyroY;
        public static Vector3 InitEulerAnglesaPro, EulerAnglesaPro, InitEulerAnglesbPro, EulerAnglesbPro;
        public static Vector3 gyr_gPro = new Vector3();
        public static Vector3 i_aPro = new Vector3(1, 0, 0);
        public static Vector3 j_aPro = new Vector3(0, 1, 0);
        public static Vector3 k_aPro = new Vector3(0, 0, 1);
        public static Vector3 k_aCrossPro = new Vector3(0, 0, 1);
        public static Vector3 i_bPro = new Vector3(1, 0, 0);
        public static Vector3 j_bPro = new Vector3(0, 1, 0);
        public static Vector3 j_bCrossPro = new Vector3(0, 1, 0);
        public static Vector3 k_bPro = new Vector3(0, 0, 1);
        private static double[] stickleftPro = { 0, 0 };
        private static double[] stickCenterleftPro = { 0, 0 };
        private static byte[] stick_rawleftPro = { 0, 0, 0 };
        private static double[] stickrightPro = { 0, 0 };
        private static double[] stickCenterrightPro = { 0, 0 };
        private static byte[] stick_rawrightPro = { 0, 0, 0 };
        public static SafeFileHandle handlePro;
        public static Vector3 acc_gPro = new Vector3();
        public const uint report_lenPro = 49;
        public static Vector3 InitDirectAnglesPro, DirectAnglesPro;
        public static bool ProControllerButtonSHOULDER_Left_1, ProControllerButtonSHOULDER_Left_2, ProControllerButtonSHOULDER_Right_1, ProControllerButtonSHOULDER_Right_2, ProControllerButtonDPAD_DOWN, ProControllerButtonDPAD_RIGHT, ProControllerButtonDPAD_UP, ProControllerButtonDPAD_LEFT, ProControllerButtonA, ProControllerButtonB, ProControllerButtonX, ProControllerButtonY, ProControllerButtonMINUS, ProControllerButtonPLUS, ProControllerButtonSTICK_Left, ProControllerButtonSTICK_Right, ProControllerButtonCAPTURE, ProControllerButtonHOME, ISPRO;
        public static byte[] report_bufPro = new byte[report_lenPro];
        public static float acc_gcalibrationProX, acc_gcalibrationProY, acc_gcalibrationProZ;
        private static bool ScanPro()
        {
            ISPRO = false;
            int index = 0;
            System.Guid guid;
            HidD_GetHidGuid(out guid);
            System.IntPtr hDevInfo = SetupDiGetClassDevs(ref guid, null, new System.IntPtr(), 0x00000010);
            SP_DEVICE_INTERFACE_DATA diData = new SP_DEVICE_INTERFACE_DATA();
            diData.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(diData);
            while (SetupDiEnumDeviceInterfaces(hDevInfo, new System.IntPtr(), ref guid, index, ref diData))
            {
                System.UInt32 size;
                SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, new System.IntPtr(), 0, out size, new System.IntPtr());
                SP_DEVICE_INTERFACE_DETAIL_DATA diDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                diDetail.cbSize = 5;
                if (SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, ref diDetail, size, out size, new System.IntPtr()))
                {
                    if ((diDetail.DevicePath.Contains(vendor_id) | diDetail.DevicePath.Contains(vendor_id_)) & diDetail.DevicePath.Contains(product_pro))
                    {
                        ISPRO = true;
                        AttachProController(diDetail.DevicePath);
                    }
                    if (ISPRO)
                        return true;
                }
                index++;
            }
            return false;
        }
        private static void AttachProController(string path)
        {
            do
            {
                IntPtr handle = CreateFile(path, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite, new System.IntPtr(), System.IO.FileMode.Open, EFileAttributes.Normal, new System.IntPtr());
                handlePro = Prohid_open_path(handle);
                SubcommandProController(0x40, new byte[] { 0x1 }, 1);
                SubcommandProController(0x3, new byte[] { 0x30 }, 1);
                SubcommandProController(0x40, new byte[] { 0x1 }, 1);
                SubcommandProController(0x3, new byte[] { 0x30 }, 1);
            }
            while (handlePro.IsInvalid);
        }
        private static void SubcommandProController(byte sc, byte[] buf, uint len)
        {
            byte[] buf_Pro = new byte[report_lenPro];
            System.Array.Copy(buf, 0, buf_Pro, 11, len);
            buf_Pro[10] = sc;
            buf_Pro[1] = 0;
            buf_Pro[0] = 0x1;
            Prohid_write(handlePro, buf_Pro, (UIntPtr)(len + 11));
            Prohid_read_timeout(handlePro, buf_Pro, (UIntPtr)report_lenPro);
        }
        public const double REGISTER_IR = 0x04b00030, REGISTER_EXTENSION_INIT_1 = 0x04a400f0, REGISTER_EXTENSION_INIT_2 = 0x04a400fb, REGISTER_EXTENSION_TYPE = 0x04a400fa, REGISTER_EXTENSION_CALIBRATION = 0x04a40020, REGISTER_MOTIONPLUS_INIT = 0x04a600fe;
        public static double irx0, iry0, irx1, iry1, irx, iry, irxc, iryc, WiimoteIRSensors0X, WiimoteIRSensors0Y, WiimoteIRSensors1X, WiimoteIRSensors1Y, WiimoteRawValuesX, WiimoteRawValuesY, WiimoteRawValuesZ, calibrationxinit, calibrationyinit, calibrationzinit, stickviewxinit, stickviewyinit, WiimoteNunchuckStateRawValuesX, WiimoteNunchuckStateRawValuesY, WiimoteNunchuckStateRawValuesZ, WiimoteNunchuckStateRawJoystickX, WiimoteNunchuckStateRawJoystickY, mousex, mousey, WiimoteIRSensors0Xcam, WiimoteIRSensors0Ycam, WiimoteIRSensors1Xcam, WiimoteIRSensors1Ycam, WiimoteIRSensorsXcam, WiimoteIRSensorsYcam, centery = 160f, WiimoteIR0notfound = 0, irx2, iry2, irx3, iry3;
        public static int irmode = 1, gyromode = 1;
        public static bool WiimoteIR1foundcam, WiimoteIR0foundcam, WiimoteIR1found, WiimoteIR0found, WiimoteIRswitch, WiimoteButtonStateA, WiimoteButtonStateB, WiimoteButtonStateMinus, WiimoteButtonStateHome, WiimoteButtonStatePlus, WiimoteButtonStateOne, WiimoteButtonStateTwo, WiimoteButtonStateUp, WiimoteButtonStateDown, WiimoteButtonStateLeft, WiimoteButtonStateRight, WiimoteNunchuckStateC, WiimoteNunchuckStateZ, ISWIIMOTE;
        public static byte[] buff = new byte[] { 0x55 }, mBuff = new byte[22], aBuffer = new byte[22];
        public const byte Type = 0x12, IR = 0x13, WriteMemory = 0x16, ReadMemory = 0x16, IRExtensionAccel = 0x37;
        public static FileStream mStream;
        public static SafeFileHandle handle = null;
        public static void InitWiimoteNunchuck()
        {
            try
            {
                calibrationxinit = -aBuffer[3] + 135f;
                calibrationyinit = -aBuffer[4] + 135f;
                calibrationzinit = -aBuffer[5] + 135f;
                stickviewxinit = -aBuffer[16] + 125f;
                stickviewyinit = -aBuffer[17] + 125f;
            }
            catch { }
        }
        public static void ProcessButtonsWiimoteNunchuck()
        {
            try
            {
                if (irmode == 1)
                {
                    WiimoteIRSensors0X = aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8;
                    WiimoteIRSensors0Y = aBuffer[7] | ((aBuffer[8] >> 6) & 0x03) << 8;
                    WiimoteIRSensors1X = aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8;
                    WiimoteIRSensors1Y = aBuffer[10] | ((aBuffer[8] >> 2) & 0x03) << 8;
                    WiimoteIR0found = WiimoteIRSensors0X > 0f & WiimoteIRSensors0X <= 1024f & WiimoteIRSensors0Y > 0f & WiimoteIRSensors0Y <= 768f;
                    WiimoteIR1found = WiimoteIRSensors1X > 0f & WiimoteIRSensors1X <= 1024f & WiimoteIRSensors1Y > 0f & WiimoteIRSensors1Y <= 768f;
                    if (WiimoteIR0found)
                    {
                        WiimoteIRSensors0Xcam = WiimoteIRSensors0X - 512f;
                        WiimoteIRSensors0Ycam = WiimoteIRSensors0Y - 384f;
                    }
                    if (WiimoteIR1found)
                    {
                        WiimoteIRSensors1Xcam = WiimoteIRSensors1X - 512f;
                        WiimoteIRSensors1Ycam = WiimoteIRSensors1Y - 384f;
                    }
                    if (WiimoteIR0found & WiimoteIR1found)
                    {
                        WiimoteIRSensorsXcam = (WiimoteIRSensors0Xcam + WiimoteIRSensors1Xcam) / 2f;
                        WiimoteIRSensorsYcam = (WiimoteIRSensors0Ycam + WiimoteIRSensors1Ycam) / 2f;
                    }
                    if (WiimoteIR0found)
                    {
                        irx0 = 2 * WiimoteIRSensors0Xcam - WiimoteIRSensorsXcam;
                        iry0 = 2 * WiimoteIRSensors0Ycam - WiimoteIRSensorsYcam;
                    }
                    if (WiimoteIR1found)
                    {
                        irx1 = 2 * WiimoteIRSensors1Xcam - WiimoteIRSensorsXcam;
                        iry1 = 2 * WiimoteIRSensors1Ycam - WiimoteIRSensorsYcam;
                    }
                    irxc = irx0 + irx1;
                    iryc = iry0 + iry1;
                }
                else if (irmode == 2)
                {
                    WiimoteIR0found = (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8) > 1 & (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8) < 1023;
                    WiimoteIR1found = (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8) > 1 & (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8) < 1023;
                    if (WiimoteIR0notfound == 0 & WiimoteIR1found)
                        WiimoteIR0notfound = 1;
                    if (WiimoteIR0notfound == 1 & !WiimoteIR0found & !WiimoteIR1found)
                        WiimoteIR0notfound = 2;
                    if (WiimoteIR0notfound == 2 & WiimoteIR0found)
                    {
                        WiimoteIR0notfound = 0;
                        if (!WiimoteIRswitch)
                            WiimoteIRswitch = true;
                        else
                            WiimoteIRswitch = false;
                    }
                    if (WiimoteIR0notfound == 0 & WiimoteIR0found)
                        WiimoteIR0notfound = 0;
                    if (WiimoteIR0notfound == 0 & !WiimoteIR0found & !WiimoteIR1found)
                        WiimoteIR0notfound = 0;
                    if (WiimoteIR0notfound == 1 & WiimoteIR0found)
                        WiimoteIR0notfound = 0;
                    if (WiimoteIR0found)
                    {
                        WiimoteIRSensors0X = (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8);
                        WiimoteIRSensors0Y = (aBuffer[7] | ((aBuffer[8] >> 6) & 0x03) << 8);
                    }
                    if (WiimoteIR1found)
                    {
                        WiimoteIRSensors1X = (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8);
                        WiimoteIRSensors1Y = (aBuffer[10] | ((aBuffer[8] >> 2) & 0x03) << 8);
                    }
                    if (WiimoteIRswitch)
                    {
                        WiimoteIR0foundcam = WiimoteIR0found;
                        WiimoteIR1foundcam = WiimoteIR1found;
                        WiimoteIRSensors0Xcam = WiimoteIRSensors0X - 512f;
                        WiimoteIRSensors0Ycam = WiimoteIRSensors0Y - 384f;
                        WiimoteIRSensors1Xcam = WiimoteIRSensors1X - 512f;
                        WiimoteIRSensors1Ycam = WiimoteIRSensors1Y - 384f;
                    }
                    else
                    {
                        WiimoteIR1foundcam = WiimoteIR0found;
                        WiimoteIR0foundcam = WiimoteIR1found;
                        WiimoteIRSensors1Xcam = WiimoteIRSensors0X - 512f;
                        WiimoteIRSensors1Ycam = WiimoteIRSensors0Y - 384f;
                        WiimoteIRSensors0Xcam = WiimoteIRSensors1X - 512f;
                        WiimoteIRSensors0Ycam = WiimoteIRSensors1Y - 384f;
                    }
                    if (WiimoteIR0foundcam & WiimoteIR1foundcam)
                    {
                        irx2 = WiimoteIRSensors0Xcam;
                        iry2 = WiimoteIRSensors0Ycam;
                        irx3 = WiimoteIRSensors1Xcam;
                        iry3 = WiimoteIRSensors1Ycam;
                        WiimoteIRSensorsXcam = WiimoteIRSensors0Xcam - WiimoteIRSensors1Xcam;
                        WiimoteIRSensorsYcam = WiimoteIRSensors0Ycam - WiimoteIRSensors1Ycam;
                    }
                    if (WiimoteIR0foundcam & !WiimoteIR1foundcam)
                    {
                        irx2 = WiimoteIRSensors0Xcam;
                        iry2 = WiimoteIRSensors0Ycam;
                        irx3 = WiimoteIRSensors0Xcam - WiimoteIRSensorsXcam;
                        iry3 = WiimoteIRSensors0Ycam - WiimoteIRSensorsYcam;
                    }
                    if (WiimoteIR1foundcam & !WiimoteIR0foundcam)
                    {
                        irx3 = WiimoteIRSensors1Xcam;
                        iry3 = WiimoteIRSensors1Ycam;
                        irx2 = WiimoteIRSensors1Xcam + WiimoteIRSensorsXcam;
                        iry2 = WiimoteIRSensors1Ycam + WiimoteIRSensorsYcam;
                    }
                    irxc = irx2 + irx3;
                    iryc = iry2 + iry3;
                }
                else
                {
                    WiimoteIR0found = (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8) > 1 & (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8) < 1023;
                    WiimoteIR1found = (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8) > 1 & (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8) < 1023;
                    if (WiimoteIR0found & WiimoteIR1found)
                    {
                        WiimoteIRSensors0X = (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8);
                        WiimoteIRSensors0Y = (aBuffer[7] | ((aBuffer[8] >> 6) & 0x03) << 8);
                        irx2 = WiimoteIRSensors0X - 512f;
                        iry2 = WiimoteIRSensors0Y - 384f;
                        WiimoteIRSensors1X = (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8);
                        WiimoteIRSensors1Y = (aBuffer[10] | ((aBuffer[8] >> 2) & 0x03) << 8);
                        irx3 = WiimoteIRSensors1X - 512f;
                        iry3 = WiimoteIRSensors1Y - 384f;
                    }
                    irxc = (irx2 + irx3) / 512f * 1346f;
                    iryc = (iry2 + iry3) / 768f * 782f;
                }
                irx = irxc * (1024f / 1346f);
                iry = iryc + centery >= 0 ? Scale(iryc + centery, 0f, 782f + centery, 0f, 1024f) : Scale(iryc + centery, -782f + centery, 0f, -1024f, 0f);
                WiimoteButtonStateA = (aBuffer[2] & 0x08) != 0;
                WiimoteButtonStateB = (aBuffer[2] & 0x04) != 0;
                WiimoteButtonStateMinus = (aBuffer[2] & 0x10) != 0;
                WiimoteButtonStateHome = (aBuffer[2] & 0x80) != 0;
                WiimoteButtonStatePlus = (aBuffer[1] & 0x10) != 0;
                WiimoteButtonStateOne = (aBuffer[2] & 0x02) != 0;
                WiimoteButtonStateTwo = (aBuffer[2] & 0x01) != 0;
                WiimoteButtonStateUp = (aBuffer[1] & 0x08) != 0;
                WiimoteButtonStateDown = (aBuffer[1] & 0x04) != 0;
                WiimoteButtonStateLeft = (aBuffer[1] & 0x01) != 0;
                WiimoteButtonStateRight = (aBuffer[1] & 0x02) != 0;
                WiimoteNunchuckStateRawJoystickX = aBuffer[16] - 125f + stickviewxinit;
                WiimoteNunchuckStateRawJoystickY = aBuffer[17] - 125f + stickviewyinit;
                WiimoteNunchuckStateC = (aBuffer[21] & 0x02) == 0;
                WiimoteNunchuckStateZ = (aBuffer[21] & 0x01) == 0;
                WiimoteRawValuesX = aBuffer[3] - 135f + calibrationxinit;
                WiimoteRawValuesY = aBuffer[4] - 135f + calibrationyinit;
                WiimoteRawValuesZ = aBuffer[5] - 135f + calibrationzinit;
                WiimoteNunchuckStateRawValuesX = aBuffer[18] - 125f;
                WiimoteNunchuckStateRawValuesY = aBuffer[19] - 125f;
                WiimoteNunchuckStateRawValuesZ = aBuffer[20] - 125f;
            }
            catch { }
        }
        public static double Scale(double value, double min, double max, double minScale, double maxScale)
        {
            double scaled = minScale + (double)(value - min) / (max - min) * (maxScale - minScale);
            return scaled;
        }
        public static void DataWiimoteNunchuck()
        {
            try
            {
                mStream.Read(aBuffer, 0, 22);
            }
            catch
            {
                System.Threading.Thread.Sleep(1);
            }
        }
        public static void FormCloseWiimoteNunchuck()
        {
            try
            {
                mStream.Close();
                handle.Close();
            }
            catch { }
            try
            {
                wiimotedisconnect();
                wiimoteconnected = false;
            }
            catch { }
        }
        public static bool ScanWiimote()
        {
            int index = 0;
            Guid guid;
            HidD_GetHidGuid(out guid);
            IntPtr hDevInfo = SetupDiGetClassDevs(ref guid, null, new IntPtr(), 0x00000010);
            SP_DEVICE_INTERFACE_DATA diData = new SP_DEVICE_INTERFACE_DATA();
            diData.cbSize = Marshal.SizeOf(diData);
            while (SetupDiEnumDeviceInterfaces(hDevInfo, new IntPtr(), ref guid, index, ref diData))
            {
                UInt32 size;
                SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, new IntPtr(), 0, out size, new IntPtr());
                SP_DEVICE_INTERFACE_DETAIL_DATA diDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                diDetail.cbSize = 5;
                if (SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, ref diDetail, size, out size, new IntPtr()))
                {
                    if ((diDetail.DevicePath.Contains(vendor_id) | diDetail.DevicePath.Contains(vendor_id_)) & (diDetail.DevicePath.Contains(product_r1) | diDetail.DevicePath.Contains(product_r2)))
                    {
                        WiimoteFound(diDetail.DevicePath);
                        return true;
                    }
                }
                index++;
            }
            return false;
        }
        public static void WiimoteFound(string path)
        {
            do
            {
                handle = CreateFile(path, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, (uint)EFileAttributes.Overlapped, IntPtr.Zero);
                WriteData(handle, IR, (int)REGISTER_IR, new byte[] { 0x08 }, 1);
                WriteData(handle, Type, (int)REGISTER_EXTENSION_INIT_1, new byte[] { 0x55 }, 1);
                WriteData(handle, Type, (int)REGISTER_EXTENSION_INIT_2, new byte[] { 0x00 }, 1);
                WriteData(handle, Type, (int)REGISTER_MOTIONPLUS_INIT, new byte[] { 0x04 }, 1);
                ReadData(handle, 0x0016, 7);
                ReadData(handle, (int)REGISTER_EXTENSION_TYPE, 6);
                ReadData(handle, (int)REGISTER_EXTENSION_CALIBRATION, 16);
                ReadData(handle, (int)REGISTER_EXTENSION_CALIBRATION, 32);
            }
            while (handle.IsInvalid);
            mStream = new FileStream(handle, FileAccess.ReadWrite, 22, true);
        }
        public static void ReadData(SafeFileHandle _hFile, int address, short size)
        {
            mBuff[0] = (byte)ReadMemory;
            mBuff[1] = (byte)((address & 0xff000000) >> 24);
            mBuff[2] = (byte)((address & 0x00ff0000) >> 16);
            mBuff[3] = (byte)((address & 0x0000ff00) >> 8);
            mBuff[4] = (byte)(address & 0x000000ff);
            mBuff[5] = (byte)((size & 0xff00) >> 8);
            mBuff[6] = (byte)(size & 0xff);
            HidD_SetOutputReport(_hFile.DangerousGetHandle(), mBuff, 22);
        }
        public static void WriteData(SafeFileHandle _hFile, byte mbuff, int address, byte[] buff, short size)
        {
            mBuff[0] = (byte)mbuff;
            mBuff[1] = (byte)(0x04);
            mBuff[2] = (byte)IRExtensionAccel;
            System.Array.Copy(buff, 0, mBuff, 3, 1);
            HidD_SetOutputReport(_hFile.DangerousGetHandle(), mBuff, 22);
            mBuff[0] = (byte)WriteMemory;
            mBuff[1] = (byte)(((address & 0xff000000) >> 24));
            mBuff[2] = (byte)((address & 0x00ff0000) >> 16);
            mBuff[3] = (byte)((address & 0x0000ff00) >> 8);
            mBuff[4] = (byte)((address & 0x000000ff) >> 0);
            mBuff[5] = (byte)size;
            System.Array.Copy(buff, 0, mBuff, 6, 1);
            HidD_SetOutputReport(_hFile.DangerousGetHandle(), mBuff, 22);
        }
        public static bool PS5ControllerButtonCrossPressed;
        public static bool PS5ControllerButtonCirclePressed;
        public static bool PS5ControllerButtonSquarePressed;
        public static bool PS5ControllerButtonTrianglePressed;
        public static bool PS5ControllerButtonDPadUpPressed;
        public static bool PS5ControllerButtonDPadRightPressed;
        public static bool PS5ControllerButtonDPadDownPressed;
        public static bool PS5ControllerButtonDPadLeftPressed;
        public static bool PS5ControllerButtonL1Pressed;
        public static bool PS5ControllerButtonR1Pressed;
        public static bool PS5ControllerButtonL2Pressed;
        public static bool PS5ControllerButtonR2Pressed;
        public static bool PS5ControllerButtonL3Pressed;
        public static bool PS5ControllerButtonR3Pressed;
        public static bool PS5ControllerButtonCreatePressed;
        public static bool PS5ControllerButtonMenuPressed;
        public static bool PS5ControllerButtonLogoPressed;
        public static bool PS5ControllerButtonTouchpadPressed;
        public static bool PS5ControllerButtonMicPressed;
        public static bool PS5ControllerTouchOn;
        public static bool PS5ControllerButtonACC, PS5ControllerRollLeft, PS5ControllerRollRight;
        private static double PS5ControllerLeftStickX, PS5ControllerLeftStickY, PS5ControllerRightStickX, PS5ControllerRightStickY, PS5ControllerRightTriggerPosition, PS5ControllerLeftTriggerPosition, PS5ControllerTouchX, PS5ControllerTouchY;
        public static System.Collections.Generic.List<double> PS5ValListX = new System.Collections.Generic.List<double>(), PS5ValListY = new System.Collections.Generic.List<double>();
        public static bool PS5ControllerGyroCenter, PS5ControllerAccelCenter;
        public static double PS5ControllerAccelX, PS5ControllerAccelY, PS5ControllerGyroX, PS5ControllerGyroY;
        public static Vector3 InitEulerAnglesaPS5, EulerAnglesaPS5, InitEulerAnglesbPS5, EulerAnglesbPS5;
        public static Vector3 gyr_gPS5 = new Vector3();
        public static Vector3 i_aPS5 = new Vector3(1, 0, 0);
        public static Vector3 j_aPS5 = new Vector3(0, 1, 0);
        public static Vector3 k_aPS5 = new Vector3(0, 0, 1);
        public static Vector3 k_aCrossPS5 = new Vector3(0, 0, 1);
        public static Vector3 i_bPS5 = new Vector3(1, 0, 0);
        public static Vector3 j_bPS5 = new Vector3(0, 1, 0);
        public static Vector3 j_bCrossPS5 = new Vector3(0, 1, 0);
        public static Vector3 k_bPS5 = new Vector3(0, 0, 1);
        public static Vector3 acc_gPS5 = new Vector3();
        public static Vector3 InitDirectAnglesPS5, DirectAnglesPS5;
        public static float acc_gcalibrationPS5X, acc_gcalibrationPS5Y, acc_gcalibrationPS5Z;
        private static DualSense ds;
        private static int wheelPos = 0;
        public static Quaternion GetVectorPS5(Vector3 i_PS5, Vector3 j_PS5, Vector3 k_PS5)
        {
            Vector3 v1 = new Vector3(j_PS5.X, i_PS5.X, k_PS5.X);
            Vector3 v2 = -new Vector3(j_PS5.Z, i_PS5.Z, k_PS5.Z);
            return QuaternionLookRotationPS5(v1, v2);
        }
        private static Quaternion QuaternionLookRotationPS5(Vector3 forward, Vector3 up)
        {
            Vector3 vector = forward;
            Vector3 vector2 = Vector3.Cross(up, vector);
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            Quaternion quaternion = new Quaternion();
            double m00 = vector2.X;
            double m01 = vector2.Y;
            double m02 = vector2.Z;
            double m10 = vector3.X;
            double m11 = vector3.Y;
            double m12 = vector3.Z;
            double m20 = vector.X;
            double m21 = vector.Y;
            double m22 = vector.Z;
            double num8 = m00 + m11 + m22;
            double num = Math.Sqrt(num8 + 1f);
            quaternion.W = (float)num * 0.5f;
            num = 0.5f / num;
            quaternion.X = (float)(m12 - m21) * (float)num;
            quaternion.Y = (float)(m20 - m02) * (float)num;
            quaternion.Z = (float)(m01 - m10) * (float)num;
            return quaternion;
        }
        private static Vector3 ToEulerAnglesPS5(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;
            pitchYawRoll.X = (float)Math.Asin(2f * test / unit);
            pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);
            pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);
            return pitchYawRoll;
        }
        private void ExtractIMUValuesPS5()
        {
            if (gyromode == 1 | gyromode == 2)
            {
                i_aPS5 = new Vector3(1, 0, 0);
                j_aPS5 = new Vector3(0, 1, 0);
                k_aPS5.Y = 0f;
                k_aPS5.Z = 1f;
                i_bPS5 = new Vector3(1, 0, 0);
                j_bPS5.Y = 1f;
                k_bPS5 = new Vector3(0, 0, 1);
                if (gyromode == 1)
                {
                    k_aCrossPS5 += Vector3.Cross(gyr_gPS5, Vector3.Normalize(k_aPS5));
                    j_bCrossPS5 += Vector3.Cross(gyr_gPS5, Vector3.Normalize(j_bPS5));
                    EulerAnglesaPS5 = ToEulerAnglesPS5(GetVectorPS5(Vector3.Normalize(i_aPS5), Vector3.Normalize(j_aPS5), Vector3.Normalize(k_aCrossPS5))) - InitEulerAnglesaPS5;
                    EulerAnglesbPS5 = Vector3.Normalize(ToEulerAnglesPS5(GetVectorPS5(Vector3.Normalize(i_bPS5), Vector3.Normalize(j_bCrossPS5), Vector3.Normalize(k_bPS5)))) - InitEulerAnglesbPS5;
                    PS5ControllerGyroX = (EulerAnglesbPS5.X + EulerAnglesbPS5.Y) * 22222222f;
                    PS5ControllerGyroY = EulerAnglesaPS5.Z * 22222222f;
                    if (PS5ControllerGyroCenter | (int)PS5ControllerGyroX == 0)
                    {
                        j_bCrossPS5 = new Vector3(0, 1, 0);
                        j_bPS5.X = 0f;
                        j_bPS5.Z = 0f;
                        InitEulerAnglesbPS5 = Vector3.Normalize(ToEulerAnglesPS5(GetVectorPS5(Vector3.Normalize(i_bPS5), Vector3.Normalize(j_bCrossPS5), Vector3.Normalize(k_bPS5))));
                    }
                    if (PS5ControllerGyroCenter | (int)PS5ControllerGyroY == 0)
                    {
                        k_aCrossPS5 = new Vector3(0, 0, 1);
                        k_aPS5.X = 0f;
                        InitEulerAnglesaPS5 = ToEulerAnglesPS5(GetVectorPS5(Vector3.Normalize(i_aPS5), Vector3.Normalize(j_aPS5), Vector3.Normalize(k_aCrossPS5)));
                    }
                }
                else if (gyromode == 2)
                {
                    k_aCrossPS5 = Vector3.Cross(gyr_gPS5, (k_aPS5)) * 10f;
                    j_bCrossPS5 = Vector3.Cross(gyr_gPS5, (j_bPS5)) * 10f;
                    EulerAnglesaPS5 = ToEulerAnglesPS5(GetVectorPS5((i_aPS5), (j_aPS5), (k_aCrossPS5))) - InitEulerAnglesaPS5;
                    EulerAnglesbPS5 = (ToEulerAnglesPS5(GetVectorPS5((i_bPS5), (j_bCrossPS5), (k_bPS5)))) - InitEulerAnglesbPS5;
                    PS5ControllerGyroX = (EulerAnglesbPS5.X + EulerAnglesbPS5.Y) * 22222222f;
                    PS5ControllerGyroY = EulerAnglesaPS5.Z * 22222222f;
                    if (PS5ControllerGyroCenter | (int)PS5ControllerGyroX == 0)
                    {
                        j_bCrossPS5 = new Vector3(0, 1, 0);
                        j_bPS5.X = 0f;
                        j_bPS5.Z = 0f;
                        InitEulerAnglesbPS5 = (ToEulerAnglesPS5(GetVectorPS5((i_bPS5), (j_bCrossPS5), (k_bPS5))));
                    }
                    if (PS5ControllerGyroCenter | (int)PS5ControllerGyroY == 0)
                    {
                        k_aCrossPS5 = new Vector3(0, 0, 1);
                        k_aPS5.X = 0f;
                        InitEulerAnglesaPS5 = ToEulerAnglesPS5(GetVectorPS5((i_aPS5), (j_aPS5), (k_aCrossPS5)));
                    }
                }
            }
        }
        static int ProcessStateLogic(DualSenseInputState dss, int wheelPos)
        {
            PS5ControllerLeftStickX = dss.LeftAnalogStick.X;
            PS5ControllerLeftStickY = dss.LeftAnalogStick.Y;
            PS5ControllerRightStickX = -dss.RightAnalogStick.X;
            PS5ControllerRightStickY = -dss.RightAnalogStick.Y;
            PS5ControllerLeftTriggerPosition = dss.L2;
            PS5ControllerRightTriggerPosition = dss.R2;
            PS5ControllerTouchX = dss.Touchpad1.X;
            PS5ControllerTouchY = dss.Touchpad1.Y;
            PS5ControllerTouchOn = dss.Touchpad1.IsDown;
            if (gyromode == 1 | gyromode == 2)
            {
                gyr_gPS5.X = (float)Math.Round((dss.Gyro.Z - 1) / 333333333f, 7);
                gyr_gPS5.Y = -(float)Math.Round((dss.Gyro.X - 6) / 333333333f, 7);
                gyr_gPS5.Z = -(float)Math.Round((dss.Gyro.Y - 5) / 333333333f, 7);
            }
            else
            {
                gyr_gPS5.X = dss.Gyro.Z - 1;
                gyr_gPS5.Y = -(dss.Gyro.X - 6);
                gyr_gPS5.Z = -(dss.Gyro.Y - 5);
                PS5ControllerGyroX = gyr_gPS5.Z;
                PS5ControllerGyroY = gyr_gPS5.Y;
            }
            acc_gPS5 = new Vector3(dss.Accelerometer.X, dss.Accelerometer.Z, dss.Accelerometer.Y);
            if (PS5ControllerAccelCenter)
                InitDirectAnglesPS5 = acc_gPS5;
            DirectAnglesPS5 = acc_gPS5 - InitDirectAnglesPS5;
            PS5ControllerAccelX = -(DirectAnglesPS5.Y + DirectAnglesPS5.Z) / 6f;
            PS5ControllerAccelY = DirectAnglesPS5.X / 6f;
            IEnumerable<string> pressedButtons = dss.GetType().GetProperties()
                .Where(p => p.Name.EndsWith("Button") && p.PropertyType == typeof(bool))
                .Where(p => (bool)p.GetValue(dss))
                .Select(p => p.Name.Replace("Button", ""));
            string joined = string.Join(", ", pressedButtons);
            if (joined.Contains("Cross"))
                PS5ControllerButtonCrossPressed = true;
            else
                PS5ControllerButtonCrossPressed = false;
            if (joined.Contains("Circle"))
                PS5ControllerButtonCirclePressed = true;
            else
                PS5ControllerButtonCirclePressed = false;
            if (joined.Contains("Square"))
                PS5ControllerButtonSquarePressed = true;
            else
                PS5ControllerButtonSquarePressed = false;
            if (joined.Contains("Triangle"))
                PS5ControllerButtonTrianglePressed = true;
            else
                PS5ControllerButtonTrianglePressed = false;
            if (joined.Contains("DPadUp"))
                PS5ControllerButtonDPadUpPressed = true;
            else
                PS5ControllerButtonDPadUpPressed = false;
            if (joined.Contains("DPadRight"))
                PS5ControllerButtonDPadRightPressed = true;
            else
                PS5ControllerButtonDPadRightPressed = false;
            if (joined.Contains("DPadDown"))
                PS5ControllerButtonDPadDownPressed = true;
            else
                PS5ControllerButtonDPadDownPressed = false;
            if (joined.Contains("DPadLeft"))
                PS5ControllerButtonDPadLeftPressed = true;
            else
                PS5ControllerButtonDPadLeftPressed = false;
            if (joined.Contains("L1"))
                PS5ControllerButtonL1Pressed = true;
            else
                PS5ControllerButtonL1Pressed = false;
            if (joined.Contains("R1"))
                PS5ControllerButtonR1Pressed = true;
            else
                PS5ControllerButtonR1Pressed = false;
            if (joined.Contains("L2"))
                PS5ControllerButtonL2Pressed = true;
            else
                PS5ControllerButtonL2Pressed = false;
            if (joined.Contains("R2"))
                PS5ControllerButtonR2Pressed = true;
            else
                PS5ControllerButtonR2Pressed = false;
            if (joined.Contains("L3"))
                PS5ControllerButtonL3Pressed = true;
            else
                PS5ControllerButtonL3Pressed = false;
            if (joined.Contains("R3"))
                PS5ControllerButtonR3Pressed = true;
            else
                PS5ControllerButtonR3Pressed = false;
            if (joined.Contains("Create"))
                PS5ControllerButtonCreatePressed = true;
            else
                PS5ControllerButtonCreatePressed = false;
            if (joined.Contains("Menu"))
                PS5ControllerButtonMenuPressed = true;
            else
                PS5ControllerButtonMenuPressed = false;
            if (joined.Contains("Logo"))
                PS5ControllerButtonLogoPressed = true;
            else
                PS5ControllerButtonLogoPressed = false;
            if (joined.Contains("Touchpad"))
                PS5ControllerButtonTouchpadPressed = true;
            else
                PS5ControllerButtonTouchpadPressed = false;
            if (joined.Contains("Mic"))
                PS5ControllerButtonMicPressed = true;
            else
                PS5ControllerButtonMicPressed = false;
            return (wheelPos + 5) % 384;
        }
        static T Choose<T>(T[] ts)
        {
            return ts[0];
        }
        static DualSense ChooseController()
        {
            DualSense[] available = DualSense.EnumerateControllers().ToArray();
            if (available.Length == 0)
            {
                return null;
            }
            return Choose(available);
        }
        static void MainAsyncPolling()
        {
            ds.Acquire();
            while (scriptrunning)
            {
                ds.BeginPolling();
                Thread.Sleep(1);
            }
            ds.EndPolling();
            ds.Release();
        }
    }
    /// <summary>
    /// Interaction logic for DualSense controllers.
    /// </summary>
    public class DualSense
    {
        // IO parameters
        private readonly IDevice underlyingDevice;
        private readonly int? readBufferSize;
        private readonly int? writeBufferSize;

        // async polling
        private IDisposable pollerSubscription;

        /// <summary>
        /// State event handler for asynchronous polling.
        /// </summary>
        /// <seealso cref="BeginPolling(uint)"/>
        /// <seealso cref="EndPolling"/>
        public event StatePolledHandler OnStatePolled;

        /// <summary>
        /// Button state changed event handler for asynchronous polling.
        /// </summary>
        /// <seealso cref="BeginPolling(uint)"/>
        /// <seealso cref="EndPolling"/>
        public event ButtonStateChangedHandler OnButtonStateChanged;

        /// <summary>
        /// This controller's most recently polled input state.
        /// </summary>
        public DualSenseInputState InputState { get; private set; } = new DualSenseInputState();

        /// <summary>
        /// Private constructor for <see cref="EnumerateControllers"/>.
        /// </summary>
        /// <param name="underlyingDevice">The underlying low-level device.</param>
        /// <param name="readBufferSize">The device's declared read buffer size.</param>
        /// <param name="writeBufferSize">The device's declared write buffer size.</param>
        private DualSense(IDevice underlyingDevice, int? readBufferSize, int? writeBufferSize)
        {
            this.underlyingDevice = underlyingDevice;
            this.readBufferSize = readBufferSize;
            this.writeBufferSize = writeBufferSize;
        }

        /// <summary>
        /// Acquires the controller.
        /// </summary>
        public void Acquire()
        {
            if (!underlyingDevice.IsInitialized)
            {
                underlyingDevice.InitializeAsync().Wait();
            }
        }

        /// <summary>
        /// Releases the controller.
        /// </summary>
        public void Release()
        {
            if (underlyingDevice.IsInitialized)
            {
                underlyingDevice.Close();
            }
        }

        private async Task<DualSenseInputState> ReadWriteOnceAsync()
        {
            TransferResult result = await underlyingDevice.WriteAndReadAsync(GetOutputDataBytes());
            if (result.BytesTransferred == readBufferSize)
            {
                return new DualSenseInputState(result.Data.Skip(1).ToArray(), 0f);
            }
            else
            {
                throw new IOException("Failed to read data - buffer size mismatch");
            }
        }

        /// <summary>
        /// Updates the input and output states once. This operation is blocking.
        /// </summary>
        /// <returns>The polled state, for convenience. This is also updated on the controller instance.</returns>
        public DualSenseInputState ReadWriteOnce()
        {
            Task<DualSenseInputState> stateTask = ReadWriteOnceAsync();
            stateTask.Wait();
            InputState = stateTask.Result;
            return InputState;
        }

        /// <summary>
        /// Begins asynchously updating the output state and polling the input state at the specified interval.
        /// </summary>
        /// <param name="pollingIntervalMs">How long to wait between each I/O loop, in milliseconds</param>
        /// <remarks>
        /// Instance state is not thread safe. In other words, when using polling, updating instance state 
        /// (such as <see cref="OutputState"/>) both inside and outside of <see cref="OnStatePolled"/>
        /// may create unexpected results. When using polling, it is generally expected you will only make
        /// modifications to state inside the <see cref="OnStatePolled"/> handler in response to input, or
        /// outside of the handler in response to external events (for example, game logic). It's also
        /// expected that you will only use the <see cref="DualSense"/> instance passed as an argument to 
        /// the sender, rather than external references to instance.
        /// </remarks>
        public void BeginPolling()
        {
            DualSenseInputState nextState = ReadWriteOnce();
            DualSenseInputState prevState = InputState;
            InputState = nextState;
            // don't take up the burden to diff the changes unless someone cares
            if (OnButtonStateChanged != null)
            {
                DualSenseInputStateButtonDelta delta = new DualSenseInputStateButtonDelta(prevState, nextState);
                if (delta.HasChanges)
                {
                    OnButtonStateChanged.Invoke(this, delta);
                }
            }
        }

        /// <summary>
        /// Stop asynchronously updating the output state and polling for new inputs.
        /// </summary>
        public void EndPolling()
        {
            if (pollerSubscription == null)
            {
                throw new InvalidOperationException("Can't end polling without starting polling first");
            }
            pollerSubscription.Dispose();
            pollerSubscription = null;
        }

        /// <summary>
        /// Builds the output byte array that will be sent to the controller.
        /// </summary>
        /// <returns>An array of bytes to send to the controller</returns>
        private byte[] GetOutputDataBytes()
        {
            byte[] bytes = new byte[writeBufferSize ?? 0];
            bytes[0] = 0x02;
            return bytes;
        }

        /// <summary>
        /// Enumerates available controllers.
        /// </summary>
        /// <returns>Enumerable of available controllers.</returns>
        public static IEnumerable<DualSense> EnumerateControllers()
        {
            foreach (ConnectedDeviceDefinition deviceDefinition in HidScanner.Instance.ListDevices())
            {
                IDevice device = HidScanner.Instance.GetConnectedDevice(deviceDefinition);
                yield return new DualSense(device, deviceDefinition.ReadBufferSize, deviceDefinition.WriteBufferSize);
            }
        }

        /// <summary>
        /// A handler for a state polling IO event. The sender has the <see cref="DualSenseInputState"/>
        /// from the most recent poll, and can be used to update the next
        /// <see cref="DualSenseOutputState"/>.
        /// </summary>
        /// <param name="sender">The <see cref="DualSense"/> instance that was just polled.</param>
        public delegate void StatePolledHandler(DualSense sender);

        /// <summary>
        /// A handler for a button state changed IO event. The sender has the <see cref="DualSenseInputState"/>
        /// from the most recent poll, and can be used to update the next <see cref="DualSenseInputState"/>.
        /// </summary>
        /// <param name="sender">The <see cref="DualSense"/> instance that was just polled.</param>
        /// <param name="changes">The change status of each button.</param>
        public delegate void ButtonStateChangedHandler(DualSense sender, DualSenseInputStateButtonDelta changes);
    }
    public class SendKeyboard
    {
        [DllImport("keyboard.dll", EntryPoint = "SendKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendKey(UInt16 bVk, UInt16 bScan);
        [DllImport("keyboard.dll", EntryPoint = "SendKeyF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendKeyF(UInt16 bVk, UInt16 bScan);
        [DllImport("keyboard.dll", EntryPoint = "SendKeyArrows", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendKeyArrows(UInt16 bVk, UInt16 bScan);
        [DllImport("keyboard.dll", EntryPoint = "SendKeyArrowsF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendKeyArrowsF(UInt16 bVk, UInt16 bScan);
        [DllImport("keyboard.dll", EntryPoint = "SendMouseEventButtonLeft", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMouseEventButtonLeft();
        [DllImport("keyboard.dll", EntryPoint = "SendMouseEventButtonLeftF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMouseEventButtonLeftF();
        [DllImport("keyboard.dll", EntryPoint = "SendMouseEventButtonRight", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMouseEventButtonRight();
        [DllImport("keyboard.dll", EntryPoint = "SendMouseEventButtonRightF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMouseEventButtonRightF();
        [DllImport("keyboard.dll", EntryPoint = "SendMouseEventButtonMiddle", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMouseEventButtonMiddle();
        [DllImport("keyboard.dll", EntryPoint = "SendMouseEventButtonMiddleF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMouseEventButtonMiddleF();
        [DllImport("keyboard.dll", EntryPoint = "SendMouseEventButtonWheelUp", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMouseEventButtonWheelUp();
        [DllImport("keyboard.dll", EntryPoint = "SendMouseEventButtonWheelDown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMouseEventButtonWheelDown();
        [DllImport("keyboard.dll", EntryPoint = "SimulateKeyDown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SimulateKeyDown(UInt16 keyCode, UInt16 bScan);
        [DllImport("keyboard.dll", EntryPoint = "SimulateKeyUp", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SimulateKeyUp(UInt16 keyCode, UInt16 bScan);
        [DllImport("keyboard.dll", EntryPoint = "SimulateKeyDownArrows", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SimulateKeyDownArrows(UInt16 keyCode, UInt16 bScan);
        [DllImport("keyboard.dll", EntryPoint = "SimulateKeyUpArrows", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SimulateKeyUpArrows(UInt16 keyCode, UInt16 bScan);
        [DllImport("keyboard.dll", EntryPoint = "LeftClick", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LeftClick();
        [DllImport("keyboard.dll", EntryPoint = "LeftClickF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LeftClickF();
        [DllImport("keyboard.dll", EntryPoint = "RightClick", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RightClick();
        [DllImport("keyboard.dll", EntryPoint = "RightClickF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RightClickF();
        [DllImport("keyboard.dll", EntryPoint = "MiddleClick", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MiddleClick();
        [DllImport("keyboard.dll", EntryPoint = "MiddleClickF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MiddleClickF();
        [DllImport("keyboard.dll", EntryPoint = "WheelDownF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WheelDownF();
        [DllImport("keyboard.dll", EntryPoint = "WheelUpF", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WheelUpF();
        public const ushort VK_LBUTTON = (ushort)0x01;
        public const ushort VK_RBUTTON = (ushort)0x02;
        public const ushort VK_CANCEL = (ushort)0x03;
        public const ushort VK_MBUTTON = (ushort)0x04;
        public const ushort VK_XBUTTON1 = (ushort)0x05;
        public const ushort VK_XBUTTON2 = (ushort)0x06;
        public const ushort VK_BACK = (ushort)0x08;
        public const ushort VK_Tab = (ushort)0x09;
        public const ushort VK_CLEAR = (ushort)0x0C;
        public const ushort VK_Return = (ushort)0x0D;
        public const ushort VK_SHIFT = (ushort)0x10;
        public const ushort VK_CONTROL = (ushort)0x11;
        public const ushort VK_MENU = (ushort)0x12;
        public const ushort VK_PAUSE = (ushort)0x13;
        public const ushort VK_CAPITAL = (ushort)0x14;
        public const ushort VK_KANA = (ushort)0x15;
        public const ushort VK_HANGEUL = (ushort)0x15;
        public const ushort VK_HANGUL = (ushort)0x15;
        public const ushort VK_JUNJA = (ushort)0x17;
        public const ushort VK_FINAL = (ushort)0x18;
        public const ushort VK_HANJA = (ushort)0x19;
        public const ushort VK_KANJI = (ushort)0x19;
        public const ushort VK_Escape = (ushort)0x1B;
        public const ushort VK_CONVERT = (ushort)0x1C;
        public const ushort VK_NONCONVERT = (ushort)0x1D;
        public const ushort VK_ACCEPT = (ushort)0x1E;
        public const ushort VK_MODECHANGE = (ushort)0x1F;
        public const ushort VK_Space = (ushort)0x20;
        public const ushort VK_PRIOR = (ushort)0x21;
        public const ushort VK_NEXT = (ushort)0x22;
        public const ushort VK_END = (ushort)0x23;
        public const ushort VK_HOME = (ushort)0x24;
        public const ushort VK_LEFT = (ushort)0x25;
        public const ushort VK_UP = (ushort)0x26;
        public const ushort VK_RIGHT = (ushort)0x27;
        public const ushort VK_DOWN = (ushort)0x28;
        public const ushort VK_SELECT = (ushort)0x29;
        public const ushort VK_PRINT = (ushort)0x2A;
        public const ushort VK_EXECUTE = (ushort)0x2B;
        public const ushort VK_SNAPSHOT = (ushort)0x2C;
        public const ushort VK_INSERT = (ushort)0x2D;
        public const ushort VK_DELETE = (ushort)0x2E;
        public const ushort VK_HELP = (ushort)0x2F;
        public const ushort VK_APOSTROPHE = (ushort)0xDE;
        public const ushort VK_0 = (ushort)0x30;
        public const ushort VK_1 = (ushort)0x31;
        public const ushort VK_2 = (ushort)0x32;
        public const ushort VK_3 = (ushort)0x33;
        public const ushort VK_4 = (ushort)0x34;
        public const ushort VK_5 = (ushort)0x35;
        public const ushort VK_6 = (ushort)0x36;
        public const ushort VK_7 = (ushort)0x37;
        public const ushort VK_8 = (ushort)0x38;
        public const ushort VK_9 = (ushort)0x39;
        public const ushort VK_A = (ushort)0x41;
        public const ushort VK_B = (ushort)0x42;
        public const ushort VK_C = (ushort)0x43;
        public const ushort VK_D = (ushort)0x44;
        public const ushort VK_E = (ushort)0x45;
        public const ushort VK_F = (ushort)0x46;
        public const ushort VK_G = (ushort)0x47;
        public const ushort VK_H = (ushort)0x48;
        public const ushort VK_I = (ushort)0x49;
        public const ushort VK_J = (ushort)0x4A;
        public const ushort VK_K = (ushort)0x4B;
        public const ushort VK_L = (ushort)0x4C;
        public const ushort VK_M = (ushort)0x4D;
        public const ushort VK_N = (ushort)0x4E;
        public const ushort VK_O = (ushort)0x4F;
        public const ushort VK_P = (ushort)0x50;
        public const ushort VK_Q = (ushort)0x51;
        public const ushort VK_R = (ushort)0x52;
        public const ushort VK_S = (ushort)0x53;
        public const ushort VK_T = (ushort)0x54;
        public const ushort VK_U = (ushort)0x55;
        public const ushort VK_V = (ushort)0x56;
        public const ushort VK_W = (ushort)0x57;
        public const ushort VK_X = (ushort)0x58;
        public const ushort VK_Y = (ushort)0x59;
        public const ushort VK_Z = (ushort)0x5A;
        public const ushort VK_LWIN = (ushort)0x5B;
        public const ushort VK_RWIN = (ushort)0x5C;
        public const ushort VK_APPS = (ushort)0x5D;
        public const ushort VK_SLEEP = (ushort)0x5F;
        public const ushort VK_NUMPAD0 = (ushort)0x60;
        public const ushort VK_NUMPAD1 = (ushort)0x61;
        public const ushort VK_NUMPAD2 = (ushort)0x62;
        public const ushort VK_NUMPAD3 = (ushort)0x63;
        public const ushort VK_NUMPAD4 = (ushort)0x64;
        public const ushort VK_NUMPAD5 = (ushort)0x65;
        public const ushort VK_NUMPAD6 = (ushort)0x66;
        public const ushort VK_NUMPAD7 = (ushort)0x67;
        public const ushort VK_NUMPAD8 = (ushort)0x68;
        public const ushort VK_NUMPAD9 = (ushort)0x69;
        public const ushort VK_MULTIPLY = (ushort)0x6A;
        public const ushort VK_ADD = (ushort)0x6B;
        public const ushort VK_SEPARATOR = (ushort)0x6C;
        public const ushort VK_SUBTRACT = (ushort)0x6D;
        public const ushort VK_DECIMAL = (ushort)0x6E;
        public const ushort VK_DIVIDE = (ushort)0x6F;
        public const ushort VK_F1 = (ushort)0x70;
        public const ushort VK_F2 = (ushort)0x71;
        public const ushort VK_F3 = (ushort)0x72;
        public const ushort VK_F4 = (ushort)0x73;
        public const ushort VK_F5 = (ushort)0x74;
        public const ushort VK_F6 = (ushort)0x75;
        public const ushort VK_F7 = (ushort)0x76;
        public const ushort VK_F8 = (ushort)0x77;
        public const ushort VK_F9 = (ushort)0x78;
        public const ushort VK_F10 = (ushort)0x79;
        public const ushort VK_F11 = (ushort)0x7A;
        public const ushort VK_F12 = (ushort)0x7B;
        public const ushort VK_F13 = (ushort)0x7C;
        public const ushort VK_F14 = (ushort)0x7D;
        public const ushort VK_F15 = (ushort)0x7E;
        public const ushort VK_F16 = (ushort)0x7F;
        public const ushort VK_F17 = (ushort)0x80;
        public const ushort VK_F18 = (ushort)0x81;
        public const ushort VK_F19 = (ushort)0x82;
        public const ushort VK_F20 = (ushort)0x83;
        public const ushort VK_F21 = (ushort)0x84;
        public const ushort VK_F22 = (ushort)0x85;
        public const ushort VK_F23 = (ushort)0x86;
        public const ushort VK_F24 = (ushort)0x87;
        public const ushort VK_NUMLOCK = (ushort)0x90;
        public const ushort VK_SCROLL = (ushort)0x91;
        public const ushort VK_LeftShift = (ushort)0xA0;
        public const ushort VK_RightShift = (ushort)0xA1;
        public const ushort VK_LeftControl = (ushort)0xA2;
        public const ushort VK_RightControl = (ushort)0xA3;
        public const ushort VK_LMENU = (ushort)0xA4;
        public const ushort VK_RMENU = (ushort)0xA5;
        public const ushort VK_BROWSER_BACK = (ushort)0xA6;
        public const ushort VK_BROWSER_FORWARD = (ushort)0xA7;
        public const ushort VK_BROWSER_REFRESH = (ushort)0xA8;
        public const ushort VK_BROWSER_STOP = (ushort)0xA9;
        public const ushort VK_BROWSER_SEARCH = (ushort)0xAA;
        public const ushort VK_BROWSER_FAVORITES = (ushort)0xAB;
        public const ushort VK_BROWSER_HOME = (ushort)0xAC;
        public const ushort VK_VOLUME_MUTE = (ushort)0xAD;
        public const ushort VK_VOLUME_DOWN = (ushort)0xAE;
        public const ushort VK_VOLUME_UP = (ushort)0xAF;
        public const ushort VK_MEDIA_NEXT_TRACK = (ushort)0xB0;
        public const ushort VK_MEDIA_PREV_TRACK = (ushort)0xB1;
        public const ushort VK_MEDIA_STOP = (ushort)0xB2;
        public const ushort VK_MEDIA_PLAY_PAUSE = (ushort)0xB3;
        public const ushort VK_LAUNCH_MAIL = (ushort)0xB4;
        public const ushort VK_LAUNCH_MEDIA_SELECT = (ushort)0xB5;
        public const ushort VK_LAUNCH_APP1 = (ushort)0xB6;
        public const ushort VK_LAUNCH_APP2 = (ushort)0xB7;
        public const ushort VK_OEM_1 = (ushort)0xBA;
        public const ushort VK_OEM_PLUS = (ushort)0xBB;
        public const ushort VK_OEM_COMMA = (ushort)0xBC;
        public const ushort VK_OEM_MINUS = (ushort)0xBD;
        public const ushort VK_OEM_PERIOD = (ushort)0xBE;
        public const ushort VK_OEM_2 = (ushort)0xBF;
        public const ushort VK_OEM_3 = (ushort)0xC0;
        public const ushort VK_OEM_4 = (ushort)0xDB;
        public const ushort VK_OEM_5 = (ushort)0xDC;
        public const ushort VK_OEM_6 = (ushort)0xDD;
        public const ushort VK_OEM_7 = (ushort)0xDE;
        public const ushort VK_OEM_8 = (ushort)0xDF;
        public const ushort VK_OEM_102 = (ushort)0xE2;
        public const ushort VK_PROCESSKEY = (ushort)0xE5;
        public const ushort VK_PACKET = (ushort)0xE7;
        public const ushort VK_ATTN = (ushort)0xF6;
        public const ushort VK_CRSEL = (ushort)0xF7;
        public const ushort VK_EXSEL = (ushort)0xF8;
        public const ushort VK_EREOF = (ushort)0xF9;
        public const ushort VK_PLAY = (ushort)0xFA;
        public const ushort VK_ZOOM = (ushort)0xFB;
        public const ushort VK_NONAME = (ushort)0xFC;
        public const ushort VK_PA1 = (ushort)0xFD;
        public const ushort VK_OEM_CLEAR = (ushort)0xFE;
        public const ushort S_LBUTTON = (ushort)0x0;
        public const ushort S_RBUTTON = 0;
        public const ushort S_CANCEL = 70;
        public const ushort S_MBUTTON = 0;
        public const ushort S_XBUTTON1 = 0;
        public const ushort S_XBUTTON2 = 0;
        public const ushort S_BACK = 14;
        public const ushort S_Tab = 15;
        public const ushort S_CLEAR = 76;
        public const ushort S_Return = 28;
        public const ushort S_SHIFT = 42;
        public const ushort S_CONTROL = 29;
        public const ushort S_MENU = 56;
        public const ushort S_PAUSE = 0;
        public const ushort S_CAPITAL = 58;
        public const ushort S_KANA = 0;
        public const ushort S_HANGEUL = 0;
        public const ushort S_HANGUL = 0;
        public const ushort S_JUNJA = 0;
        public const ushort S_FINAL = 0;
        public const ushort S_HANJA = 0;
        public const ushort S_KANJI = 0;
        public const ushort S_Escape = 1;
        public const ushort S_CONVERT = 0;
        public const ushort S_NONCONVERT = 0;
        public const ushort S_ACCEPT = 0;
        public const ushort S_MODECHANGE = 0;
        public const ushort S_Space = 57;
        public const ushort S_PRIOR = 73;
        public const ushort S_NEXT = 81;
        public const ushort S_END = 79;
        public const ushort S_HOME = 71;
        public const ushort S_LEFT = 75;
        public const ushort S_UP = 72;
        public const ushort S_RIGHT = 77;
        public const ushort S_DOWN = 80;
        public const ushort S_SELECT = 0;
        public const ushort S_PRINT = 0;
        public const ushort S_EXECUTE = 0;
        public const ushort S_SNAPSHOT = 84;
        public const ushort S_INSERT = 82;
        public const ushort S_DELETE = 83;
        public const ushort S_HELP = 99;
        public const ushort S_APOSTROPHE = 41;
        public const ushort S_0 = 11;
        public const ushort S_1 = 2;
        public const ushort S_2 = 3;
        public const ushort S_3 = 4;
        public const ushort S_4 = 5;
        public const ushort S_5 = 6;
        public const ushort S_6 = 7;
        public const ushort S_7 = 8;
        public const ushort S_8 = 9;
        public const ushort S_9 = 10;
        public const ushort S_A = 16;
        public const ushort S_B = 48;
        public const ushort S_C = 46;
        public const ushort S_D = 32;
        public const ushort S_E = 18;
        public const ushort S_F = 33;
        public const ushort S_G = 34;
        public const ushort S_H = 35;
        public const ushort S_I = 23;
        public const ushort S_J = 36;
        public const ushort S_K = 37;
        public const ushort S_L = 38;
        public const ushort S_M = 39;
        public const ushort S_N = 49;
        public const ushort S_O = 24;
        public const ushort S_P = 25;
        public const ushort S_Q = 30;
        public const ushort S_R = 19;
        public const ushort S_S = 31;
        public const ushort S_T = 20;
        public const ushort S_U = 22;
        public const ushort S_V = 47;
        public const ushort S_W = 44;
        public const ushort S_X = 45;
        public const ushort S_Y = 21;
        public const ushort S_Z = 17;
        public const ushort S_LWIN = 91;
        public const ushort S_RWIN = 92;
        public const ushort S_APPS = 93;
        public const ushort S_SLEEP = 95;
        public const ushort S_NUMPAD0 = 82;
        public const ushort S_NUMPAD1 = 79;
        public const ushort S_NUMPAD2 = 80;
        public const ushort S_NUMPAD3 = 81;
        public const ushort S_NUMPAD4 = 75;
        public const ushort S_NUMPAD5 = 76;
        public const ushort S_NUMPAD6 = 77;
        public const ushort S_NUMPAD7 = 71;
        public const ushort S_NUMPAD8 = 72;
        public const ushort S_NUMPAD9 = 73;
        public const ushort S_MULTIPLY = 55;
        public const ushort S_ADD = 78;
        public const ushort S_SEPARATOR = 0;
        public const ushort S_SUBTRACT = 74;
        public const ushort S_DECIMAL = 83;
        public const ushort S_DIVIDE = 53;
        public const ushort S_F1 = 59;
        public const ushort S_F2 = 60;
        public const ushort S_F3 = 61;
        public const ushort S_F4 = 62;
        public const ushort S_F5 = 63;
        public const ushort S_F6 = 64;
        public const ushort S_F7 = 65;
        public const ushort S_F8 = 66;
        public const ushort S_F9 = 67;
        public const ushort S_F10 = 68;
        public const ushort S_F11 = 87;
        public const ushort S_F12 = 88;
        public const ushort S_F13 = 100;
        public const ushort S_F14 = 101;
        public const ushort S_F15 = 102;
        public const ushort S_F16 = 103;
        public const ushort S_F17 = 104;
        public const ushort S_F18 = 105;
        public const ushort S_F19 = 106;
        public const ushort S_F20 = 107;
        public const ushort S_F21 = 108;
        public const ushort S_F22 = 109;
        public const ushort S_F23 = 110;
        public const ushort S_F24 = 118;
        public const ushort S_NUMLOCK = 69;
        public const ushort S_SCROLL = 70;
        public const ushort S_LeftShift = 42;
        public const ushort S_RightShift = 54;
        public const ushort S_LeftControl = 29;
        public const ushort S_RightControl = 29;
        public const ushort S_LMENU = 56;
        public const ushort S_RMENU = 56;
        public const ushort S_BROWSER_BACK = 106;
        public const ushort S_BROWSER_FORWARD = 105;
        public const ushort S_BROWSER_REFRESH = 103;
        public const ushort S_BROWSER_STOP = 104;
        public const ushort S_BROWSER_SEARCH = 101;
        public const ushort S_BROWSER_FAVORITES = 102;
        public const ushort S_BROWSER_HOME = 50;
        public const ushort S_VOLUME_MUTE = 32;
        public const ushort S_VOLUME_DOWN = 46;
        public const ushort S_VOLUME_UP = 48;
        public const ushort S_MEDIA_NEXT_TRACK = 25;
        public const ushort S_MEDIA_PREV_TRACK = 16;
        public const ushort S_MEDIA_STOP = 36;
        public const ushort S_MEDIA_PLAY_PAUSE = 34;
        public const ushort S_LAUNCH_MAIL = 108;
        public const ushort S_LAUNCH_MEDIA_SELECT = 109;
        public const ushort S_LAUNCH_APP1 = 107;
        public const ushort S_LAUNCH_APP2 = 33;
        public const ushort S_OEM_1 = 27;
        public const ushort S_OEM_PLUS = 13;
        public const ushort S_OEM_COMMA = 50;
        public const ushort S_OEM_MINUS = 0;
        public const ushort S_OEM_PERIOD = 51;
        public const ushort S_OEM_2 = 52;
        public const ushort S_OEM_3 = 40;
        public const ushort S_OEM_4 = 12;
        public const ushort S_OEM_5 = 43;
        public const ushort S_OEM_6 = 26;
        public const ushort S_OEM_7 = 41;
        public const ushort S_OEM_8 = 53;
        public const ushort S_OEM_102 = 86;
        public const ushort S_PROCESSKEY = 0;
        public const ushort S_PACKET = 0;
        public const ushort S_ATTN = 0;
        public const ushort S_CRSEL = 0;
        public const ushort S_EXSEL = 0;
        public const ushort S_EREOF = 93;
        public const ushort S_PLAY = 0;
        public const ushort S_ZOOM = 98;
        public const ushort S_NONAME = 0;
        public const ushort S_PA1 = 0;
        public const ushort S_OEM_CLEAR = 0;
        public static string drivertype;
        public static int[] wd = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static int[] wu = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static void valchanged(int n, bool val)
        {
            if (val)
            {
                if (wd[n] <= 1)
                {
                    wd[n] = wd[n] + 1;
                }
                wu[n] = 0;
            }
            else
            {
                if (wu[n] <= 1)
                {
                    wu[n] = wu[n] + 1;
                }
                wd[n] = 0;
            }
        }
        public static void UnLoadKM()
        {
            SetKM("kmevent", false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            SetKM("sendinput", false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
        }
        public static void SetKM(string KeyboardMouseDriverType, bool SendLeftClick, bool SendRightClick, bool SendMiddleClick, bool SendWheelUp, bool SendWheelDown, bool SendLeft, bool SendRight, bool SendUp, bool SendDown, bool SendLButton, bool SendRButton, bool SendCancel, bool SendMBUTTON, bool SendXBUTTON1, bool SendXBUTTON2, bool SendBack, bool SendTab, bool SendClear, bool SendReturn, bool SendSHIFT, bool SendCONTROL, bool SendMENU, bool SendPAUSE, bool SendCAPITAL, bool SendKANA, bool SendHANGEUL, bool SendHANGUL, bool SendJUNJA, bool SendFINAL, bool SendHANJA, bool SendKANJI, bool SendEscape, bool SendCONVERT, bool SendNONCONVERT, bool SendACCEPT, bool SendMODECHANGE, bool SendSpace, bool SendPRIOR, bool SendNEXT, bool SendEND, bool SendHOME, bool SendLEFT, bool SendUP, bool SendRIGHT, bool SendDOWN, bool SendSELECT, bool SendPRINT, bool SendEXECUTE, bool SendSNAPSHOT, bool SendINSERT, bool SendDELETE, bool SendHELP, bool SendAPOSTROPHE, bool Send0, bool Send1, bool Send2, bool Send3, bool Send4, bool Send5, bool Send6, bool Send7, bool Send8, bool Send9, bool SendA, bool SendB, bool SendC, bool SendD, bool SendE, bool SendF, bool SendG, bool SendH, bool SendI, bool SendJ, bool SendK, bool SendL, bool SendM, bool SendN, bool SendO, bool SendP, bool SendQ, bool SendR, bool SendS, bool SendT, bool SendU, bool SendV, bool SendW, bool SendX, bool SendY, bool SendZ, bool SendLWIN, bool SendRWIN, bool SendAPPS, bool SendSLEEP, bool SendNUMPAD0, bool SendNUMPAD1, bool SendNUMPAD2, bool SendNUMPAD3, bool SendNUMPAD4, bool SendNUMPAD5, bool SendNUMPAD6, bool SendNUMPAD7, bool SendNUMPAD8, bool SendNUMPAD9, bool SendMULTIPLY, bool SendADD, bool SendSEPARATOR, bool SendSUBTRACT, bool SendDECIMAL, bool SendDIVIDE, bool SendF1, bool SendF2, bool SendF3, bool SendF4, bool SendF5, bool SendF6, bool SendF7, bool SendF8, bool SendF9, bool SendF10, bool SendF11, bool SendF12, bool SendF13, bool SendF14, bool SendF15, bool SendF16, bool SendF17, bool SendF18, bool SendF19, bool SendF20, bool SendF21, bool SendF22, bool SendF23, bool SendF24, bool SendNUMLOCK, bool SendSCROLL, bool SendLeftShift, bool SendRightShift, bool SendLeftControl, bool SendRightControl, bool SendLMENU, bool SendRMENU)
        {
            drivertype = KeyboardMouseDriverType;
            valchanged(1, SendLeftClick);
            if (wd[1] == 1)
                mouseclickleft();
            if (wu[1] == 1)
                mouseclickleftF();
            valchanged(2, SendRightClick);
            if (wd[2] == 1)
                mouseclickright();
            if (wu[2] == 1)
                mouseclickrightF();
            valchanged(3, SendMiddleClick);
            if (wd[3] == 1)
                mouseclickmiddle();
            if (wu[3] == 1)
                mouseclickmiddleF();
            valchanged(4, SendWheelUp);
            if (wd[4] == 1)
                mousewheelup();
            valchanged(5, SendWheelDown);
            if (wd[5] == 1)
                mousewheeldown();
            valchanged(6, SendLeft);
            if (wd[6] == 1)
                keyboardArrows(VK_LEFT, S_LEFT);
            if (wu[6] == 1)
                keyboardArrowsF(VK_LEFT, S_LEFT);
            valchanged(7, SendRight);
            if (wd[7] == 1)
                keyboardArrows(VK_RIGHT, S_RIGHT);
            if (wu[7] == 1)
                keyboardArrowsF(VK_RIGHT, S_RIGHT);
            valchanged(8, SendUp);
            if (wd[8] == 1)
                keyboardArrows(VK_UP, S_UP);
            if (wu[8] == 1)
                keyboardArrowsF(VK_UP, S_UP);
            valchanged(9, SendDown);
            if (wd[9] == 1)
                keyboardArrows(VK_DOWN, S_DOWN);
            if (wu[9] == 1)
                keyboardArrowsF(VK_DOWN, S_DOWN);
            valchanged(10, SendLButton);
            if (wd[10] == 1)
                keyboard(VK_LBUTTON, S_LBUTTON);
            if (wu[10] == 1)
                keyboardF(VK_LBUTTON, S_LBUTTON);
            valchanged(11, SendRButton);
            if (wd[11] == 1)
                keyboard(VK_RBUTTON, S_RBUTTON);
            if (wu[11] == 1)
                keyboardF(VK_RBUTTON, S_RBUTTON);
            valchanged(12, SendCancel);
            if (wd[12] == 1)
                keyboard(VK_CANCEL, S_CANCEL);
            if (wu[12] == 1)
                keyboardF(VK_CANCEL, S_CANCEL);
            valchanged(13, SendMBUTTON);
            if (wd[13] == 1)
                keyboard(VK_MBUTTON, S_MBUTTON);
            if (wu[13] == 1)
                keyboardF(VK_MBUTTON, S_MBUTTON);
            valchanged(14, SendXBUTTON1);
            if (wd[14] == 1)
                keyboard(VK_XBUTTON1, S_XBUTTON1);
            if (wu[14] == 1)
                keyboardF(VK_XBUTTON1, S_XBUTTON1);
            valchanged(15, SendXBUTTON2);
            if (wd[15] == 1)
                keyboard(VK_XBUTTON2, S_XBUTTON2);
            if (wu[15] == 1)
                keyboardF(VK_XBUTTON2, S_XBUTTON2);
            valchanged(16, SendBack);
            if (wd[16] == 1)
                keyboard(VK_BACK, S_BACK);
            if (wu[16] == 1)
                keyboardF(VK_BACK, S_BACK);
            valchanged(17, SendTab);
            if (wd[17] == 1)
                keyboard(VK_Tab, S_Tab);
            if (wu[17] == 1)
                keyboardF(VK_Tab, S_Tab);
            valchanged(18, SendClear);
            if (wd[18] == 1)
                keyboard(VK_CLEAR, S_CLEAR);
            if (wu[18] == 1)
                keyboardF(VK_CLEAR, S_CLEAR);
            valchanged(19, SendReturn);
            if (wd[19] == 1)
                keyboard(VK_Return, S_Return);
            if (wu[19] == 1)
                keyboardF(VK_Return, S_Return);
            valchanged(20, SendSHIFT);
            if (wd[20] == 1)
                keyboard(VK_SHIFT, S_SHIFT);
            if (wu[20] == 1)
                keyboardF(VK_SHIFT, S_SHIFT);
            valchanged(21, SendCONTROL);
            if (wd[21] == 1)
                keyboard(VK_CONTROL, S_CONTROL);
            if (wu[21] == 1)
                keyboardF(VK_CONTROL, S_CONTROL);
            valchanged(22, SendMENU);
            if (wd[22] == 1)
                keyboard(VK_MENU, S_MENU);
            if (wu[22] == 1)
                keyboardF(VK_MENU, S_MENU);
            valchanged(23, SendPAUSE);
            if (wd[23] == 1)
                keyboard(VK_PAUSE, S_PAUSE);
            if (wu[23] == 1)
                keyboardF(VK_PAUSE, S_PAUSE);
            valchanged(24, SendCAPITAL);
            if (wd[24] == 1)
                keyboard(VK_CAPITAL, S_CAPITAL);
            if (wu[24] == 1)
                keyboardF(VK_CAPITAL, S_CAPITAL);
            valchanged(25, SendKANA);
            if (wd[25] == 1)
                keyboard(VK_KANA, S_KANA);
            if (wu[25] == 1)
                keyboardF(VK_KANA, S_KANA);
            valchanged(26, SendHANGEUL);
            if (wd[26] == 1)
                keyboard(VK_HANGEUL, S_HANGEUL);
            if (wu[26] == 1)
                keyboardF(VK_HANGEUL, S_HANGEUL);
            valchanged(27, SendHANGUL);
            if (wd[27] == 1)
                keyboard(VK_HANGUL, S_HANGUL);
            if (wu[27] == 1)
                keyboardF(VK_HANGUL, S_HANGUL);
            valchanged(28, SendJUNJA);
            if (wd[28] == 1)
                keyboard(VK_JUNJA, S_JUNJA);
            if (wu[28] == 1)
                keyboardF(VK_JUNJA, S_JUNJA);
            valchanged(29, SendFINAL);
            if (wd[29] == 1)
                keyboard(VK_FINAL, S_FINAL);
            if (wu[29] == 1)
                keyboardF(VK_FINAL, S_FINAL);
            valchanged(30, SendHANJA);
            if (wd[30] == 1)
                keyboard(VK_HANJA, S_HANJA);
            if (wu[30] == 1)
                keyboardF(VK_HANJA, S_HANJA);
            valchanged(31, SendKANJI);
            if (wd[31] == 1)
                keyboard(VK_KANJI, S_KANJI);
            if (wu[31] == 1)
                keyboardF(VK_KANJI, S_KANJI);
            valchanged(32, SendEscape);
            if (wd[32] == 1)
                keyboard(VK_Escape, S_Escape);
            if (wu[32] == 1)
                keyboardF(VK_Escape, S_Escape);
            valchanged(33, SendCONVERT);
            if (wd[33] == 1)
                keyboard(VK_CONVERT, S_CONVERT);
            if (wu[33] == 1)
                keyboardF(VK_CONVERT, S_CONVERT);
            valchanged(34, SendNONCONVERT);
            if (wd[34] == 1)
                keyboard(VK_NONCONVERT, S_NONCONVERT);
            if (wu[34] == 1)
                keyboardF(VK_NONCONVERT, S_NONCONVERT);
            valchanged(35, SendACCEPT);
            if (wd[35] == 1)
                keyboard(VK_ACCEPT, S_ACCEPT);
            if (wu[35] == 1)
                keyboardF(VK_ACCEPT, S_ACCEPT);
            valchanged(36, SendMODECHANGE);
            if (wd[36] == 1)
                keyboard(VK_MODECHANGE, S_MODECHANGE);
            if (wu[36] == 1)
                keyboardF(VK_MODECHANGE, S_MODECHANGE);
            valchanged(37, SendSpace);
            if (wd[37] == 1)
                keyboard(VK_Space, S_Space);
            if (wu[37] == 1)
                keyboardF(VK_Space, S_Space);
            valchanged(38, SendPRIOR);
            if (wd[38] == 1)
                keyboard(VK_PRIOR, S_PRIOR);
            if (wu[38] == 1)
                keyboardF(VK_PRIOR, S_PRIOR);
            valchanged(39, SendNEXT);
            if (wd[39] == 1)
                keyboard(VK_NEXT, S_NEXT);
            if (wu[39] == 1)
                keyboardF(VK_NEXT, S_NEXT);
            valchanged(40, SendEND);
            if (wd[40] == 1)
                keyboard(VK_END, S_END);
            if (wu[40] == 1)
                keyboardF(VK_END, S_END);
            valchanged(41, SendHOME);
            if (wd[41] == 1)
                keyboard(VK_HOME, S_HOME);
            if (wu[41] == 1)
                keyboardF(VK_HOME, S_HOME);
            valchanged(42, SendLEFT);
            if (wd[42] == 1)
                keyboard(VK_LEFT, S_LEFT);
            if (wu[42] == 1)
                keyboardF(VK_LEFT, S_LEFT);
            valchanged(43, SendUP);
            if (wd[43] == 1)
                keyboard(VK_UP, S_UP);
            if (wu[43] == 1)
                keyboardF(VK_UP, S_UP);
            valchanged(44, SendRIGHT);
            if (wd[44] == 1)
                keyboard(VK_RIGHT, S_RIGHT);
            if (wu[44] == 1)
                keyboardF(VK_RIGHT, S_RIGHT);
            valchanged(45, SendDOWN);
            if (wd[45] == 1)
                keyboard(VK_DOWN, S_DOWN);
            if (wu[45] == 1)
                keyboardF(VK_DOWN, S_DOWN);
            valchanged(46, SendSELECT);
            if (wd[46] == 1)
                keyboard(VK_SELECT, S_SELECT);
            if (wu[46] == 1)
                keyboardF(VK_SELECT, S_SELECT);
            valchanged(47, SendPRINT);
            if (wd[47] == 1)
                keyboard(VK_PRINT, S_PRINT);
            if (wu[47] == 1)
                keyboardF(VK_PRINT, S_PRINT);
            valchanged(48, SendEXECUTE);
            if (wd[48] == 1)
                keyboard(VK_EXECUTE, S_EXECUTE);
            if (wu[48] == 1)
                keyboardF(VK_EXECUTE, S_EXECUTE);
            valchanged(49, SendSNAPSHOT);
            if (wd[49] == 1)
                keyboard(VK_SNAPSHOT, S_SNAPSHOT);
            if (wu[49] == 1)
                keyboardF(VK_SNAPSHOT, S_SNAPSHOT);
            valchanged(50, SendINSERT);
            if (wd[50] == 1)
                keyboard(VK_INSERT, S_INSERT);
            if (wu[50] == 1)
                keyboardF(VK_INSERT, S_INSERT);
            valchanged(51, SendDELETE);
            if (wd[51] == 1)
                keyboard(VK_DELETE, S_DELETE);
            if (wu[51] == 1)
                keyboardF(VK_DELETE, S_DELETE);
            valchanged(52, SendHELP);
            if (wd[52] == 1)
                keyboard(VK_HELP, S_HELP);
            if (wu[52] == 1)
                keyboardF(VK_HELP, S_HELP);
            valchanged(53, SendAPOSTROPHE);
            if (wd[53] == 1)
                keyboard(VK_APOSTROPHE, S_APOSTROPHE);
            if (wu[53] == 1)
                keyboardF(VK_APOSTROPHE, S_APOSTROPHE);
            valchanged(54, Send0);
            if (wd[54] == 1)
                keyboard(VK_0, S_0);
            if (wu[54] == 1)
                keyboardF(VK_0, S_0);
            valchanged(55, Send1);
            if (wd[55] == 1)
                keyboard(VK_1, S_1);
            if (wu[55] == 1)
                keyboardF(VK_1, S_1);
            valchanged(56, Send2);
            if (wd[56] == 1)
                keyboard(VK_2, S_2);
            if (wu[56] == 1)
                keyboardF(VK_2, S_2);
            valchanged(57, Send3);
            if (wd[57] == 1)
                keyboard(VK_3, S_3);
            if (wu[57] == 1)
                keyboardF(VK_3, S_3);
            valchanged(58, Send4);
            if (wd[58] == 1)
                keyboard(VK_4, S_4);
            if (wu[58] == 1)
                keyboardF(VK_4, S_4);
            valchanged(59, Send5);
            if (wd[59] == 1)
                keyboard(VK_5, S_5);
            if (wu[59] == 1)
                keyboardF(VK_5, S_5);
            valchanged(60, Send6);
            if (wd[60] == 1)
                keyboard(VK_6, S_6);
            if (wu[60] == 1)
                keyboardF(VK_6, S_6);
            valchanged(61, Send7);
            if (wd[61] == 1)
                keyboard(VK_7, S_7);
            if (wu[61] == 1)
                keyboardF(VK_7, S_7);
            valchanged(62, Send8);
            if (wd[62] == 1)
                keyboard(VK_8, S_8);
            if (wu[62] == 1)
                keyboardF(VK_8, S_8);
            valchanged(63, Send9);
            if (wd[63] == 1)
                keyboard(VK_9, S_9);
            if (wu[63] == 1)
                keyboardF(VK_9, S_9);
            valchanged(64, SendA);
            if (wd[64] == 1)
                keyboard(VK_A, S_A);
            if (wu[64] == 1)
                keyboardF(VK_A, S_A);
            valchanged(65, SendB);
            if (wd[65] == 1)
                keyboard(VK_B, S_B);
            if (wu[65] == 1)
                keyboardF(VK_B, S_B);
            valchanged(66, SendC);
            if (wd[66] == 1)
                keyboard(VK_C, S_C);
            if (wu[66] == 1)
                keyboardF(VK_C, S_C);
            valchanged(67, SendD);
            if (wd[67] == 1)
                keyboard(VK_D, S_D);
            if (wu[67] == 1)
                keyboardF(VK_D, S_D);
            valchanged(68, SendE);
            if (wd[68] == 1)
                keyboard(VK_E, S_E);
            if (wu[68] == 1)
                keyboardF(VK_E, S_E);
            valchanged(69, SendF);
            if (wd[69] == 1)
                keyboard(VK_F, S_F);
            if (wu[69] == 1)
                keyboardF(VK_F, S_F);
            valchanged(70, SendG);
            if (wd[70] == 1)
                keyboard(VK_G, S_G);
            if (wu[70] == 1)
                keyboardF(VK_G, S_G);
            valchanged(71, SendH);
            if (wd[71] == 1)
                keyboard(VK_H, S_H);
            if (wu[71] == 1)
                keyboardF(VK_H, S_H);
            valchanged(72, SendI);
            if (wd[72] == 1)
                keyboard(VK_I, S_I);
            if (wu[72] == 1)
                keyboardF(VK_I, S_I);
            valchanged(73, SendJ);
            if (wd[73] == 1)
                keyboard(VK_J, S_J);
            if (wu[73] == 1)
                keyboardF(VK_J, S_J);
            valchanged(74, SendK);
            if (wd[74] == 1)
                keyboard(VK_K, S_K);
            if (wu[74] == 1)
                keyboardF(VK_K, S_K);
            valchanged(75, SendL);
            if (wd[75] == 1)
                keyboard(VK_L, S_L);
            if (wu[75] == 1)
                keyboardF(VK_L, S_L);
            valchanged(76, SendM);
            if (wd[76] == 1)
                keyboard(VK_M, S_M);
            if (wu[76] == 1)
                keyboardF(VK_M, S_M);
            valchanged(77, SendN);
            if (wd[77] == 1)
                keyboard(VK_N, S_N);
            if (wu[77] == 1)
                keyboardF(VK_N, S_N);
            valchanged(78, SendO);
            if (wd[78] == 1)
                keyboard(VK_O, S_O);
            if (wu[78] == 1)
                keyboardF(VK_O, S_O);
            valchanged(79, SendP);
            if (wd[79] == 1)
                keyboard(VK_P, S_P);
            if (wu[79] == 1)
                keyboardF(VK_P, S_P);
            valchanged(80, SendQ);
            if (wd[80] == 1)
                keyboard(VK_Q, S_Q);
            if (wu[80] == 1)
                keyboardF(VK_Q, S_Q);
            valchanged(81, SendR);
            if (wd[81] == 1)
                keyboard(VK_R, S_R);
            if (wu[81] == 1)
                keyboardF(VK_R, S_R);
            valchanged(82, SendS);
            if (wd[82] == 1)
                keyboard(VK_S, S_S);
            if (wu[82] == 1)
                keyboardF(VK_S, S_S);
            valchanged(83, SendT);
            if (wd[83] == 1)
                keyboard(VK_T, S_T);
            if (wu[83] == 1)
                keyboardF(VK_T, S_T);
            valchanged(84, SendU);
            if (wd[84] == 1)
                keyboard(VK_U, S_U);
            if (wu[84] == 1)
                keyboardF(VK_U, S_U);
            valchanged(85, SendV);
            if (wd[85] == 1)
                keyboard(VK_V, S_V);
            if (wu[85] == 1)
                keyboardF(VK_V, S_V);
            valchanged(86, SendW);
            if (wd[86] == 1)
                keyboard(VK_W, S_W);
            if (wu[86] == 1)
                keyboardF(VK_W, S_W);
            valchanged(87, SendX);
            if (wd[87] == 1)
                keyboard(VK_X, S_X);
            if (wu[87] == 1)
                keyboardF(VK_X, S_X);
            valchanged(88, SendY);
            if (wd[88] == 1)
                keyboard(VK_Y, S_Y);
            if (wu[88] == 1)
                keyboardF(VK_Y, S_Y);
            valchanged(89, SendZ);
            if (wd[89] == 1)
                keyboard(VK_Z, S_Z);
            if (wu[89] == 1)
                keyboardF(VK_Z, S_Z);
            valchanged(90, SendLWIN);
            if (wd[90] == 1)
                keyboard(VK_LWIN, S_LWIN);
            if (wu[90] == 1)
                keyboardF(VK_LWIN, S_LWIN);
            valchanged(91, SendRWIN);
            if (wd[91] == 1)
                keyboard(VK_RWIN, S_RWIN);
            if (wu[91] == 1)
                keyboardF(VK_RWIN, S_RWIN);
            valchanged(92, SendAPPS);
            if (wd[92] == 1)
                keyboard(VK_APPS, S_APPS);
            if (wu[92] == 1)
                keyboardF(VK_APPS, S_APPS);
            valchanged(93, SendSLEEP);
            if (wd[93] == 1)
                keyboard(VK_SLEEP, S_SLEEP);
            if (wu[93] == 1)
                keyboardF(VK_SLEEP, S_SLEEP);
            valchanged(94, SendNUMPAD0);
            if (wd[94] == 1)
                keyboard(VK_NUMPAD0, S_NUMPAD0);
            if (wu[94] == 1)
                keyboardF(VK_NUMPAD0, S_NUMPAD0);
            valchanged(95, SendNUMPAD1);
            if (wd[95] == 1)
                keyboard(VK_NUMPAD1, S_NUMPAD1);
            if (wu[95] == 1)
                keyboardF(VK_NUMPAD1, S_NUMPAD1);
            valchanged(96, SendNUMPAD2);
            if (wd[96] == 1)
                keyboard(VK_NUMPAD2, S_NUMPAD2);
            if (wu[96] == 1)
                keyboardF(VK_NUMPAD2, S_NUMPAD2);
            valchanged(97, SendNUMPAD3);
            if (wd[97] == 1)
                keyboard(VK_NUMPAD3, S_NUMPAD3);
            if (wu[97] == 1)
                keyboardF(VK_NUMPAD3, S_NUMPAD3);
            valchanged(98, SendNUMPAD4);
            if (wd[98] == 1)
                keyboard(VK_NUMPAD4, S_NUMPAD4);
            if (wu[98] == 1)
                keyboardF(VK_NUMPAD4, S_NUMPAD4);
            valchanged(99, SendNUMPAD5);
            if (wd[99] == 1)
                keyboard(VK_NUMPAD5, S_NUMPAD5);
            if (wu[99] == 1)
                keyboardF(VK_NUMPAD5, S_NUMPAD5);
            valchanged(100, SendNUMPAD6);
            if (wd[100] == 1)
                keyboard(VK_NUMPAD6, S_NUMPAD6);
            if (wu[100] == 1)
                keyboardF(VK_NUMPAD6, S_NUMPAD6);
            valchanged(101, SendNUMPAD7);
            if (wd[101] == 1)
                keyboard(VK_NUMPAD7, S_NUMPAD7);
            if (wu[101] == 1)
                keyboardF(VK_NUMPAD7, S_NUMPAD7);
            valchanged(102, SendNUMPAD8);
            if (wd[102] == 1)
                keyboard(VK_NUMPAD8, S_NUMPAD8);
            if (wu[102] == 1)
                keyboardF(VK_NUMPAD8, S_NUMPAD8);
            valchanged(103, SendNUMPAD9);
            if (wd[103] == 1)
                keyboard(VK_NUMPAD9, S_NUMPAD9);
            if (wu[103] == 1)
                keyboardF(VK_NUMPAD9, S_NUMPAD9);
            valchanged(104, SendMULTIPLY);
            if (wd[104] == 1)
                keyboard(VK_MULTIPLY, S_MULTIPLY);
            if (wu[104] == 1)
                keyboardF(VK_MULTIPLY, S_MULTIPLY);
            valchanged(105, SendADD);
            if (wd[105] == 1)
                keyboard(VK_ADD, S_ADD);
            if (wu[105] == 1)
                keyboardF(VK_ADD, S_ADD);
            valchanged(106, SendSEPARATOR);
            if (wd[106] == 1)
                keyboard(VK_SEPARATOR, S_SEPARATOR);
            if (wu[106] == 1)
                keyboardF(VK_SEPARATOR, S_SEPARATOR);
            valchanged(107, SendSUBTRACT);
            if (wd[107] == 1)
                keyboard(VK_SUBTRACT, S_SUBTRACT);
            if (wu[107] == 1)
                keyboardF(VK_SUBTRACT, S_SUBTRACT);
            valchanged(108, SendDECIMAL);
            if (wd[108] == 1)
                keyboard(VK_DECIMAL, S_DECIMAL);
            if (wu[108] == 1)
                keyboardF(VK_DECIMAL, S_DECIMAL);
            valchanged(109, SendDIVIDE);
            if (wd[109] == 1)
                keyboard(VK_DIVIDE, S_DIVIDE);
            if (wu[109] == 1)
                keyboardF(VK_DIVIDE, S_DIVIDE);
            valchanged(110, SendF1);
            if (wd[110] == 1)
                keyboard(VK_F1, S_F1);
            if (wu[110] == 1)
                keyboardF(VK_F1, S_F1);
            valchanged(111, SendF2);
            if (wd[111] == 1)
                keyboard(VK_F2, S_F2);
            if (wu[111] == 1)
                keyboardF(VK_F2, S_F2);
            valchanged(112, SendF3);
            if (wd[112] == 1)
                keyboard(VK_F3, S_F3);
            if (wu[112] == 1)
                keyboardF(VK_F3, S_F3);
            valchanged(113, SendF4);
            if (wd[113] == 1)
                keyboard(VK_F4, S_F4);
            if (wu[113] == 1)
                keyboardF(VK_F4, S_F4);
            valchanged(114, SendF5);
            if (wd[114] == 1)
                keyboard(VK_F5, S_F5);
            if (wu[114] == 1)
                keyboardF(VK_F5, S_F5);
            valchanged(115, SendF6);
            if (wd[115] == 1)
                keyboard(VK_F6, S_F6);
            if (wu[115] == 1)
                keyboardF(VK_F6, S_F6);
            valchanged(116, SendF7);
            if (wd[116] == 1)
                keyboard(VK_F7, S_F7);
            if (wu[116] == 1)
                keyboardF(VK_F7, S_F7);
            valchanged(117, SendF8);
            if (wd[117] == 1)
                keyboard(VK_F8, S_F8);
            if (wu[117] == 1)
                keyboardF(VK_F8, S_F8);
            valchanged(118, SendF9);
            if (wd[118] == 1)
                keyboard(VK_F9, S_F9);
            if (wu[118] == 1)
                keyboardF(VK_F9, S_F9);
            valchanged(119, SendF10);
            if (wd[119] == 1)
                keyboard(VK_F10, S_F10);
            if (wu[119] == 1)
                keyboardF(VK_F10, S_F10);
            valchanged(120, SendF11);
            if (wd[120] == 1)
                keyboard(VK_F11, S_F11);
            if (wu[120] == 1)
                keyboardF(VK_F11, S_F11);
            valchanged(121, SendF12);
            if (wd[121] == 1)
                keyboard(VK_F12, S_F12);
            if (wu[121] == 1)
                keyboardF(VK_F12, S_F12);
            valchanged(122, SendF13);
            if (wd[122] == 1)
                keyboard(VK_F13, S_F13);
            if (wu[122] == 1)
                keyboardF(VK_F13, S_F13);
            valchanged(123, SendF14);
            if (wd[123] == 1)
                keyboard(VK_F14, S_F14);
            if (wu[123] == 1)
                keyboardF(VK_F14, S_F14);
            valchanged(124, SendF15);
            if (wd[124] == 1)
                keyboard(VK_F15, S_F15);
            if (wu[124] == 1)
                keyboardF(VK_F15, S_F15);
            valchanged(125, SendF16);
            if (wd[125] == 1)
                keyboard(VK_F16, S_F16);
            if (wu[125] == 1)
                keyboardF(VK_F16, S_F16);
            valchanged(126, SendF17);
            if (wd[126] == 1)
                keyboard(VK_F17, S_F17);
            if (wu[126] == 1)
                keyboardF(VK_F17, S_F17);
            valchanged(127, SendF18);
            if (wd[127] == 1)
                keyboard(VK_F18, S_F18);
            if (wu[127] == 1)
                keyboardF(VK_F18, S_F18);
            valchanged(128, SendF19);
            if (wd[128] == 1)
                keyboard(VK_F19, S_F19);
            if (wu[128] == 1)
                keyboardF(VK_F19, S_F19);
            valchanged(129, SendF20);
            if (wd[129] == 1)
                keyboard(VK_F20, S_F20);
            if (wu[129] == 1)
                keyboardF(VK_F20, S_F20);
            valchanged(130, SendF21);
            if (wd[130] == 1)
                keyboard(VK_F21, S_F21);
            if (wu[130] == 1)
                keyboardF(VK_F21, S_F21);
            valchanged(131, SendF22);
            if (wd[131] == 1)
                keyboard(VK_F22, S_F22);
            if (wu[131] == 1)
                keyboardF(VK_F22, S_F22);
            valchanged(132, SendF23);
            if (wd[132] == 1)
                keyboard(VK_F23, S_F23);
            if (wu[132] == 1)
                keyboardF(VK_F23, S_F23);
            valchanged(133, SendF24);
            if (wd[133] == 1)
                keyboard(VK_F24, S_F24);
            if (wu[133] == 1)
                keyboardF(VK_F24, S_F24);
            valchanged(134, SendNUMLOCK);
            if (wd[134] == 1)
                keyboard(VK_NUMLOCK, S_NUMLOCK);
            if (wu[134] == 1)
                keyboardF(VK_NUMLOCK, S_NUMLOCK);
            valchanged(135, SendSCROLL);
            if (wd[135] == 1)
                keyboard(VK_SCROLL, S_SCROLL);
            if (wu[135] == 1)
                keyboardF(VK_SCROLL, S_SCROLL);
            valchanged(136, SendLeftShift);
            if (wd[136] == 1)
                keyboard(VK_LeftShift, S_LeftShift);
            if (wu[136] == 1)
                keyboardF(VK_LeftShift, S_LeftShift);
            valchanged(137, SendRightShift);
            if (wd[137] == 1)
                keyboard(VK_RightShift, S_RightShift);
            if (wu[137] == 1)
                keyboardF(VK_RightShift, S_RightShift);
            valchanged(138, SendLeftControl);
            if (wd[138] == 1)
                keyboard(VK_LeftControl, S_LeftControl);
            if (wu[138] == 1)
                keyboardF(VK_LeftControl, S_LeftControl);
            valchanged(139, SendRightControl);
            if (wd[139] == 1)
                keyboard(VK_RightControl, S_RightControl);
            if (wu[139] == 1)
                keyboardF(VK_RightControl, S_RightControl);
            valchanged(140, SendLMENU);
            if (wd[140] == 1)
                keyboard(VK_LMENU, S_LMENU);
            if (wu[140] == 1)
                keyboardF(VK_LMENU, S_LMENU);
            valchanged(141, SendRMENU);
            if (wd[141] == 1)
                keyboard(VK_RMENU, S_RMENU);
            if (wu[141] == 1)
                keyboardF(VK_RMENU, S_RMENU);
        }
        public static void mouseclickleft()
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendMouseEventButtonLeft());
            else
                Task.Run(() => LeftClick());
        }
        public static void mouseclickleftF()
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendMouseEventButtonLeftF());
            else
                Task.Run(() => LeftClickF());
        }
        public static void mouseclickright()
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendMouseEventButtonRight());
            else
                Task.Run(() => RightClick());
        }
        public static void mouseclickrightF()
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendMouseEventButtonRightF());
            else
                Task.Run(() => RightClickF());
        }
        public static void mouseclickmiddle()
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendMouseEventButtonMiddle());
            else
                Task.Run(() => MiddleClick());
        }
        public static void mouseclickmiddleF()
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendMouseEventButtonMiddleF());
            else
                Task.Run(() => MiddleClickF());
        }
        public static void mousewheelup()
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendMouseEventButtonWheelUp());
            else
                Task.Run(() => WheelUpF());
        }
        public static void mousewheeldown()
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendMouseEventButtonWheelDown());
            else
                Task.Run(() => WheelDownF());
        }
        public static void keyboard(UInt16 bVk, UInt16 bScan)
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendKey(bVk, bScan));
            else
                Task.Run(() => SimulateKeyDown(bVk, bScan));
        }
        public static void keyboardF(UInt16 bVk, UInt16 bScan)
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendKeyF(bVk, bScan));
            else
                Task.Run(() => SimulateKeyUp(bVk, bScan));
        }
        public static void keyboardArrows(UInt16 bVk, UInt16 bScan)
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendKeyArrows(bVk, bScan));
            else
                Task.Run(() => SimulateKeyDownArrows(bVk, bScan));
        }
        public static void keyboardArrowsF(UInt16 bVk, UInt16 bScan)
        {
            if (drivertype == "sendinput")
                Task.Run(() => SendKeyArrowsF(bVk, bScan));
            else
                Task.Run(() => SimulateKeyUpArrows(bVk, bScan));
        }
    }
    public class SendMouse
    {
        [DllImport("mouse.dll", EntryPoint = "MoveMouseTo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MoveMouseTo(int x, int y);
        [DllImport("mouse.dll", EntryPoint = "MoveMouseBy", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MoveMouseBy(int x, int y);
        [DllImport("mouse.dll", EntryPoint = "MouseMW3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MouseMW3(int x, int y);
        [DllImport("mouse.dll", EntryPoint = "MouseBrink", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MouseBrink(int x, int y);
        [DllImport("user32.dll")]
        public static extern void SetPhysicalCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern void SetCaretPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int X, int Y);
        public static string drivertype;
        public static void SetKM(string KeyboardMouseDriverType, double MouseMoveX, double MouseMoveY, double MouseAbsX, double MouseAbsY, double MouseDesktopX, double MouseDesktopY)
        {
            drivertype = KeyboardMouseDriverType;
            if (MouseMoveX != 0f | MouseMoveY != 0f)
                mousebrink((int)(MouseMoveX), (int)(MouseMoveY));
            if (MouseAbsX != 0f | MouseAbsY != 0f)
                mousemw3((int)(MouseAbsX), (int)(MouseAbsY));
            if (MouseDesktopX != 0f | MouseDesktopY != 0f)
            {
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)(MouseDesktopX), (int)(MouseDesktopY));
                SetPhysicalCursorPos((int)(MouseDesktopX), (int)(MouseDesktopY));
                SetCaretPos((int)(MouseDesktopX), (int)(MouseDesktopY));
                SetCursorPos((int)(MouseDesktopX), (int)(MouseDesktopY));
            }
        }
        public static void mousebrink(int x, int y)
        {
            if (drivertype == "sendinput")
                MoveMouseBy(x, y);
            else
                MouseBrink(x, y);
        }
        public static void mousemw3(int x, int y)
        {
            if (drivertype == "sendinput")
                MoveMouseTo(x, y);
            else
                MouseMW3(x, y);
        }
    }
    public class MouseHook
    {
        public delegate IntPtr MouseHookHandler(int nCode, IntPtr wParam, IntPtr lParam);
        public MouseHookHandler hookHandler;
        public MSLLHOOKSTRUCT mouseStruct;
        public delegate void MouseHookCallback(MSLLHOOKSTRUCT mouseStruct);
        public event MouseHookCallback Hook;
        public IntPtr hookID = IntPtr.Zero;
        public void Install()
        {
            hookHandler = HookFunc;
            hookID = SetHook(hookHandler);
        }
        public void Uninstall()
        {
            if (hookID == IntPtr.Zero)
                return;
            UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;
        }
        ~MouseHook()
        {
            Uninstall();
        }
        public IntPtr SetHook(MouseHookHandler proc)
        {
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(module.ModuleName), 0);
        }
        public IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            mouseStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            Form1.MouseHookX = mouseStruct.pt.x;
            Form1.MouseHookY = mouseStruct.pt.y;
            Hook((MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT)));
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }
        public const int WH_MOUSE_LL = 14;
        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_XBUTTONDOWN = 0x020B,
            WM_XBUTTONUP = 0x020C
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, MouseHookHandler lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
    public class KeyboardHook
    {
        public static bool KeyboardHookButtonDown, KeyboardHookButtonUp;
        public delegate IntPtr KeyboardHookHandler(int nCode, IntPtr wParam, IntPtr lParam);
        public KeyboardHookHandler hookHandler;
        public KBDLLHOOKSTRUCT keyboardStruct;
        public delegate void KeyboardHookCallback(KBDLLHOOKSTRUCT keyboardStruct);
        public event KeyboardHookCallback Hook;
        public IntPtr hookID = IntPtr.Zero;
        public void Install()
        {
            hookHandler = HookFunc;
            hookID = SetHook(hookHandler);
        }
        public void Uninstall()
        {
            if (hookID == IntPtr.Zero)
                return;
            UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;
        }
        ~KeyboardHook()
        {
            Uninstall();
        }
        public IntPtr SetHook(KeyboardHookHandler proc)
        {
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(module.ModuleName), 0);
        }
        public IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            keyboardStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            if (KeyboardHook.KeyboardMessages.WM_KEYDOWN == (KeyboardHook.KeyboardMessages)wParam)
                KeyboardHookButtonDown = true;
            else
                KeyboardHookButtonDown = false;
            if (KeyboardHook.KeyboardMessages.WM_KEYUP == (KeyboardHook.KeyboardMessages)wParam)
                KeyboardHookButtonUp = true;
            else
                KeyboardHookButtonUp = false;
            Form1.KeyboardHookButtonDown = KeyboardHookButtonDown;
            Form1.KeyboardHookButtonUp = KeyboardHookButtonUp;
            Form1.vkCode = (int)keyboardStruct.vkCode;
            Form1.scanCode = (int)keyboardStruct.scanCode;
            Hook((KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT)));
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        public const int WH_KEYBOARD_LL = 13;
        public enum KeyboardMessages
        {
            WM_ACTIVATE = 0x0006,
            WM_APPCOMMAND = 0x0319,
            WM_CHAR = 0x0102,
            WM_DEADCHAR = 0x010,
            WM_HOTKEY = 0x0312,
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_KILLFOCUS = 0x0008,
            WM_SETFOCUS = 0x0007,
            WM_SYSDEADCHAR = 0x0107,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105,
            WM_UNICHAR = 0x0109
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookHandler lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
    public class Scp
    {
        public static int[] wd = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static int[] wu = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static void valchanged(int n, bool val)
        {
            if (val)
            {
                if (wd[n] <= 1)
                {
                    wd[n] = wd[n] + 1;
                }
                wu[n] = 0;
            }
            else
            {
                if (wu[n] <= 1)
                {
                    wu[n] = wu[n] + 1;
                }
                wd[n] = 0;
            }
        }
        private static ScpBus scpBus;
        private static X360Controller controller1, controller2;
        public static void LoadControllers()
        {
            scpBus = new ScpBus();
            scpBus.PlugIn(1);
            scpBus.PlugIn(2);
            controller1 = new X360Controller();
            controller2 = new X360Controller();
        }
        public static void UnLoadControllers()
        {
            SetControllers(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, 0, 0, 0, 0, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, 0, 0, 0, 0, 0, 0, 0, 0);
            Thread.Sleep(100);
            scpBus.Unplug(1);
            Thread.Sleep(100);
            scpBus.Unplug(2);
        }
        public static void SetControllers(bool controller1_send_back, bool controller1_send_start, bool controller1_send_A, bool controller1_send_B, bool controller1_send_X, bool controller1_send_Y, bool controller1_send_up, bool controller1_send_left, bool controller1_send_down, bool controller1_send_right, bool controller1_send_leftstick, bool controller1_send_rightstick, bool controller1_send_leftbumper, bool controller1_send_rightbumper, bool controller1_send_lefttrigger, bool controller1_send_righttrigger, double controller1_send_leftstickx, double controller1_send_leftsticky, double controller1_send_rightstickx, double controller1_send_rightsticky, bool controller2_send_back, bool controller2_send_start, bool controller2_send_A, bool controller2_send_B, bool controller2_send_X, bool controller2_send_Y, bool controller2_send_up, bool controller2_send_left, bool controller2_send_down, bool controller2_send_right, bool controller2_send_leftstick, bool controller2_send_rightstick, bool controller2_send_leftbumper, bool controller2_send_rightbumper, bool controller2_send_lefttrigger, bool controller2_send_righttrigger, double controller2_send_leftstickx, double controller2_send_leftsticky, double controller2_send_rightstickx, double controller2_send_rightsticky, double controller1_send_lefttriggerposition, double controller1_send_righttriggerposition, double controller2_send_lefttriggerposition, double controller2_send_righttriggerposition)
        {
            valchanged(1, controller1_send_back);
            if (wd[1] == 1)
                controller1.Buttons ^= X360Buttons.Back;
            if (wu[1] == 1)
                controller1.Buttons &= ~X360Buttons.Back;
            valchanged(2, controller1_send_start);
            if (wd[2] == 1)
                controller1.Buttons ^= X360Buttons.Start;
            if (wu[2] == 1)
                controller1.Buttons &= ~X360Buttons.Start;
            valchanged(3, controller1_send_A);
            if (wd[3] == 1)
                controller1.Buttons ^= X360Buttons.A;
            if (wu[3] == 1)
                controller1.Buttons &= ~X360Buttons.A;
            valchanged(4, controller1_send_B);
            if (wd[4] == 1)
                controller1.Buttons ^= X360Buttons.B;
            if (wu[4] == 1)
                controller1.Buttons &= ~X360Buttons.B;
            valchanged(5, controller1_send_X);
            if (wd[5] == 1)
                controller1.Buttons ^= X360Buttons.X;
            if (wu[5] == 1)
                controller1.Buttons &= ~X360Buttons.X;
            valchanged(6, controller1_send_Y);
            if (wd[6] == 1)
                controller1.Buttons ^= X360Buttons.Y;
            if (wu[6] == 1)
                controller1.Buttons &= ~X360Buttons.Y;
            valchanged(7, controller1_send_up);
            if (wd[7] == 1)
                controller1.Buttons ^= X360Buttons.Up;
            if (wu[7] == 1)
                controller1.Buttons &= ~X360Buttons.Up;
            valchanged(8, controller1_send_left);
            if (wd[8] == 1)
                controller1.Buttons ^= X360Buttons.Left;
            if (wu[8] == 1)
                controller1.Buttons &= ~X360Buttons.Left;
            valchanged(9, controller1_send_down);
            if (wd[9] == 1)
                controller1.Buttons ^= X360Buttons.Down;
            if (wu[9] == 1)
                controller1.Buttons &= ~X360Buttons.Down;
            valchanged(10, controller1_send_right);
            if (wd[10] == 1)
                controller1.Buttons ^= X360Buttons.Right;
            if (wu[10] == 1)
                controller1.Buttons &= ~X360Buttons.Right;
            valchanged(11, controller1_send_leftstick);
            if (wd[11] == 1)
                controller1.Buttons ^= X360Buttons.LeftStick;
            if (wu[11] == 1)
                controller1.Buttons &= ~X360Buttons.LeftStick;
            valchanged(12, controller1_send_rightstick);
            if (wd[12] == 1)
                controller1.Buttons ^= X360Buttons.RightStick;
            if (wu[12] == 1)
                controller1.Buttons &= ~X360Buttons.RightStick;
            valchanged(13, controller1_send_leftbumper);
            if (wd[13] == 1)
                controller1.Buttons ^= X360Buttons.LeftBumper;
            if (wu[13] == 1)
                controller1.Buttons &= ~X360Buttons.LeftBumper;
            valchanged(14, controller1_send_rightbumper);
            if (wd[14] == 1)
                controller1.Buttons ^= X360Buttons.RightBumper;
            if (wu[14] == 1)
                controller1.Buttons &= ~X360Buttons.RightBumper;
            controller1.LeftStickX = (short)controller1_send_leftstickx;
            controller1.LeftStickY = (short)controller1_send_leftsticky;
            controller1.RightStickX = (short)controller1_send_rightstickx;
            controller1.RightStickY = (short)controller1_send_rightsticky;
            valchanged(15, controller2_send_back);
            if (wd[15] == 1)
                controller2.Buttons ^= X360Buttons.Back;
            if (wu[15] == 1)
                controller2.Buttons &= ~X360Buttons.Back;
            valchanged(16, controller2_send_start);
            if (wd[16] == 1)
                controller2.Buttons ^= X360Buttons.Start;
            if (wu[16] == 1)
                controller2.Buttons &= ~X360Buttons.Start;
            valchanged(17, controller2_send_A);
            if (wd[17] == 1)
                controller2.Buttons ^= X360Buttons.A;
            if (wu[17] == 1)
                controller2.Buttons &= ~X360Buttons.A;
            valchanged(18, controller2_send_B);
            if (wd[18] == 1)
                controller2.Buttons ^= X360Buttons.B;
            if (wu[18] == 1)
                controller2.Buttons &= ~X360Buttons.B;
            valchanged(19, controller2_send_X);
            if (wd[19] == 1)
                controller2.Buttons ^= X360Buttons.X;
            if (wu[19] == 1)
                controller2.Buttons &= ~X360Buttons.X;
            valchanged(20, controller2_send_Y);
            if (wd[20] == 1)
                controller2.Buttons ^= X360Buttons.Y;
            if (wu[20] == 1)
                controller2.Buttons &= ~X360Buttons.Y;
            valchanged(21, controller2_send_up);
            if (wd[21] == 1)
                controller2.Buttons ^= X360Buttons.Up;
            if (wu[21] == 1)
                controller2.Buttons &= ~X360Buttons.Up;
            valchanged(22, controller2_send_left);
            if (wd[22] == 1)
                controller2.Buttons ^= X360Buttons.Left;
            if (wu[22] == 1)
                controller2.Buttons &= ~X360Buttons.Left;
            valchanged(23, controller2_send_down);
            if (wd[23] == 1)
                controller2.Buttons ^= X360Buttons.Down;
            if (wu[23] == 1)
                controller2.Buttons &= ~X360Buttons.Down;
            valchanged(24, controller2_send_right);
            if (wd[24] == 1)
                controller2.Buttons ^= X360Buttons.Right;
            if (wu[24] == 1)
                controller2.Buttons &= ~X360Buttons.Right;
            valchanged(25, controller2_send_leftstick);
            if (wd[25] == 1)
                controller2.Buttons ^= X360Buttons.LeftStick;
            if (wu[25] == 1)
                controller2.Buttons &= ~X360Buttons.LeftStick;
            valchanged(26, controller2_send_rightstick);
            if (wd[26] == 1)
                controller2.Buttons ^= X360Buttons.RightStick;
            if (wu[26] == 1)
                controller2.Buttons &= ~X360Buttons.RightStick;
            valchanged(27, controller2_send_leftbumper);
            if (wd[27] == 1)
                controller2.Buttons ^= X360Buttons.LeftBumper;
            if (wu[27] == 1)
                controller2.Buttons &= ~X360Buttons.LeftBumper;
            valchanged(28, controller2_send_rightbumper);
            if (wd[28] == 1)
                controller2.Buttons ^= X360Buttons.RightBumper;
            if (wu[28] == 1)
                controller2.Buttons &= ~X360Buttons.RightBumper;
            controller2.LeftStickX = (short)controller2_send_leftstickx;
            controller2.LeftStickY = (short)controller2_send_leftsticky;
            controller2.RightStickX = (short)controller2_send_rightstickx;
            controller2.RightStickY = (short)controller2_send_rightsticky;
            controller1.LeftTrigger = (byte)controller1_send_lefttriggerposition;
            controller1.RightTrigger = (byte)controller1_send_righttriggerposition;
            controller2.LeftTrigger = (byte)controller2_send_lefttriggerposition;
            controller2.RightTrigger = (byte)controller2_send_righttriggerposition;
            valchanged(29, controller1_send_lefttrigger);
            if (controller1_send_lefttrigger)
                controller1.LeftTrigger = 255;
            if (wu[29] == 1)
                controller1.LeftTrigger = 0;
            valchanged(30, controller1_send_righttrigger);
            if (controller1_send_righttrigger)
                controller1.RightTrigger = 255;
            if (wu[30] == 1)
                controller1.RightTrigger = 0;
            valchanged(31, controller2_send_lefttrigger);
            if (controller2_send_lefttrigger)
                controller2.LeftTrigger = 255;
            if (wu[31] == 1)
                controller2.LeftTrigger = 0;
            valchanged(32, controller2_send_righttrigger);
            if (controller2_send_righttrigger)
                controller2.RightTrigger = 255;
            if (wu[32] == 1)
                controller2.RightTrigger = 0;
            scpBus.Report(1, controller1.GetReport());
            scpBus.Report(2, controller2.GetReport());
        }
    }
}