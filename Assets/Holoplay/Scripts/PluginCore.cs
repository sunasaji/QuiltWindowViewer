﻿//Copyright 2017-2021 Looking Glass Factory Inc.
//All rights reserved.
//Unauthorized copying or distribution of this file, and the source code contained herein, is strictly prohibited.

using System.Runtime.InteropServices;
using UnityEngine;
using System.Text;
using UnityEngine.Events;

namespace LookingGlass {

    public static class PluginCore
    {
        public enum hpc_client_error
        {
            hpc_CLIERR_NOERROR,
            hpc_CLIERR_NOSERVICE,
            hpc_CLIERR_VERSIONERR,
            hpc_CLIERR_SERIALIZEERR,
            hpc_CLIERR_DESERIALIZEERR,
            hpc_CLIERR_MSGTOOBIG,
            hpc_CLIERR_SENDTIMEOUT,
            hpc_CLIERR_RECVTIMEOUT,
            hpc_CLIERR_PIPEERROR,
        };

        public enum hpc_license_type
        {
            hpc_LICENSE_NONCOMMERCIAL,
            hpc_LICENSE_COMMERCIAL
        }
        [DllImport("HoloPlayCore")] private static extern hpc_client_error hpc_InitializeApp(string app_name, hpc_license_type app_type);
        [DllImport("HoloPlayCore")] private static extern int hpc_CloseApp();
        [DllImport("HoloPlayCore")] private static extern hpc_client_error hpc_RefreshState();

        [DllImport("HoloPlayCore")] private static extern int hpc_GetStateAsJSON(StringBuilder out_buf, int out_buf_sz);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetNumDevices();
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyInt(int dev_index, string query_string);
        [DllImport("HoloPlayCore")] private static extern float hpc_GetDevicePropertyFloat(int dev_index, string query_string);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyString(int dev_index, string query_string, StringBuilder out_buf, int out_buf_sz);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetHoloPlayCoreVersion(StringBuilder out_buf, int out_buf_sz);
        /*
            Return the current version of HoloPlay Service
        */
        [DllImport("HoloPlayCore")] private static extern int hpc_GetHoloPlayServiceVersion(StringBuilder out_buf, int out_buf_sz);
        /*
            Query per-device string values.
        */
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDeviceHDMIName(int dev_index, StringBuilder out_buf, int out_buf_sz);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDeviceSerial(int dev_index, StringBuilder out_buf, int out_buf_sz);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDeviceType(int dev_index, StringBuilder out_buf, int out_buf_sz);
        /*
            Query per-device calibration and window parameters.
            These will return 0 if the device or calibration isn't found.
        */
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyScreenW(int dev_index);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyScreenH(int dev_index);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyWinX(int dev_index);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyWinY(int dev_index);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyInvView(int dev_index);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyRi(int dev_index);
        [DllImport("HoloPlayCore")] private static extern int hpc_GetDevicePropertyBi(int dev_index);
        [DllImport("HoloPlayCore")] private static extern float hpc_GetDevicePropertyPitch(int dev_index);
        [DllImport("HoloPlayCore")] private static extern float hpc_GetDevicePropertyCenter(int dev_index);
        [DllImport("HoloPlayCore")] private static extern float hpc_GetDevicePropertyTilt(int dev_index);
        [DllImport("HoloPlayCore")] private static extern float hpc_GetDevicePropertyDisplayAspect(int dev_index);
        [DllImport("HoloPlayCore")] private static extern float hpc_GetDevicePropertyFringe(int dev_index);
        [DllImport("HoloPlayCore")] private static extern float hpc_GetDevicePropertySubp(int dev_index);
 
        private static int hpc_GetViewCone(int dev_index) { return (int)hpc_GetDevicePropertyFloat(dev_index, "/calibration/viewCone/value");}

        private static string GetHoloPlayCoreVersion(){
            StringBuilder str = new StringBuilder(100);
            hpc_GetHoloPlayCoreVersion(str, 100);
            return str.ToString();
        }

        private static string GetHoloPlayServiceVersion(){
            StringBuilder str = new StringBuilder(100);
            hpc_GetHoloPlayServiceVersion(str, 100);
            return str.ToString();
        }

        private static string GetDeviceStatus(int dev_index){
            StringBuilder str = new StringBuilder(100);
            hpc_GetDevicePropertyString(dev_index, "/state", str, 100);
            return str.ToString();
        }

        private static string GetSerial(int dev_index)
        {
            StringBuilder str = new StringBuilder(100);
            PluginCore.hpc_GetDeviceSerial(dev_index, str, 100);
            return str.ToString();
        }
        private static string GetLKGName(int dev_index)
        {
            StringBuilder str = new StringBuilder(100);
            PluginCore.hpc_GetDeviceHDMIName(dev_index, str, 100);
            // Debug.Log(" Device name: " + str);
            return str.ToString();
        }

        const string InitKey = "isHoloPlayCoreInit";
        const int DEFAULT = -1;
        // const string JsonKey = "CalibrationJson";

        public static hpc_client_error InitHoloPlayCore(){
            hpc_client_error error;
            string unity_status = Application.isEditor? "Editor" : "Player";
            error = hpc_InitializeApp("Unity_" + unity_status + "_" + Application.productName, hpc_license_type.hpc_LICENSE_NONCOMMERCIAL);
            if(error == hpc_client_error.hpc_CLIERR_NOERROR)
            {
                // isInit = true;
                Debug.Log("[HoloPlay] HoloPlay Core Initialization: (HoloPlay Core version: " 
                    + GetHoloPlayCoreVersion() +", HoloPlay Service version: " 
                    + GetHoloPlayServiceVersion() + ")");
            }
            return error;
        }
        public static LoadResults GetLoadResults() {
            // Debug.Log(PlayerPrefs.GetInt(InitKey));
            bool isInit = PlayerPrefs.GetInt(InitKey, DEFAULT) > DEFAULT;
            LoadResults results = new LoadResults(false, false, false);
            hpc_client_error error;

            if (!isInit)
            {
                error = InitHoloPlayCore();
            }else{
                error = hpc_RefreshState();    
            }
              
            results.attempted = error == hpc_client_error.hpc_CLIERR_NOERROR;
            if(results.attempted)
            {
                int num_displays = hpc_GetNumDevices();
                // TODO: compare json
                bool isChanged = !isInit || PlayerPrefs.GetInt(InitKey, DEFAULT) != num_displays;
                
                PlayerPrefs.SetInt(InitKey, num_displays);

                if(isChanged) Debug.Log("[HoloPlay] Found: " + num_displays + " Looking Glass" + (num_displays <= 1 ? "" : "es"));

                results.lkgDisplayFound = num_displays > 0;
                if(results.lkgDisplayFound)
                {
                    results.calibrationFound = true;
                    for (int i = 0; i < num_displays; i++)
                    {             
                        string str = GetDeviceStatus(i);
                        if(str != "nocalibration") {
                            continue;
                        }
                        Debug.Log("[HoloPlay] No calibration found for Looking Glass:" + GetLKGName(i));
                        results.calibrationFound = false;
                    }
                    // if (results.calibrationFound)
                }
                           
            }else{
                PrintError(error);
            }

            CalibrationManager.Refresh();
			
			return results;
		}
 
        public static int GetLKGunityIndex(int dev_index)
        {
#if UNITY_STANDALONE_OSX            
            return hpc_GetDevicePropertyInt(dev_index, "/unityIndex");
#else
            return hpc_GetDevicePropertyInt(dev_index, "/unityIndex") - 1;
#endif

        }

        public static void Reset(){
            if(PlayerPrefs.GetInt(InitKey, DEFAULT) > DEFAULT){
                hpc_CloseApp();
                // Debug.Log("[HoloPlay] HoloPlay Core deinited.");
            }
            PlayerPrefs.SetInt(InitKey, DEFAULT);
            //Debug.Log(PlayerPrefs.GetInt(InitKey));
        }

        static void PrintError(hpc_client_error errco){
            if (errco != hpc_client_error.hpc_CLIERR_NOERROR)
            {
                string errstr;
                switch (errco)
                {
                    case hpc_client_error.hpc_CLIERR_NOSERVICE:
                        errstr = "HoloPlay Service not running";
                        break;
                    case hpc_client_error.hpc_CLIERR_SERIALIZEERR:
                        errstr = "Client message could not be serialized";
                        break;
                    case hpc_client_error.hpc_CLIERR_VERSIONERR:
                        errstr = "Incompatible version of HoloPlay Service";
                        break;
                    case hpc_client_error.hpc_CLIERR_PIPEERROR:
                        errstr = "Interprocess pipe broken. Check if HoloPlay Service is still running";
                        break;
                    case hpc_client_error.hpc_CLIERR_SENDTIMEOUT:
                        errstr = "Interprocess pipe send timeout";
                        break;
                    case hpc_client_error.hpc_CLIERR_RECVTIMEOUT:
                        errstr = "Interprocess pipe receive timeout";
                        break;
                    default:
                        errstr = "Unknown error";
                        break;
                }
                Debug.Log(string.Format("[Error] Client access error (code = {0}): {1}!", errco, errstr));
            }
        }

        public static Calibration[] GetCalibrationArray(){
            int num_displays = PlayerPrefs.GetInt(InitKey, DEFAULT);
            if(num_displays < 1)
            {
                // Debug.Log("Nothing is inited. Set up stops.");
                return null;
            }

            Calibration[] calibrations = new Calibration[num_displays];
            // get the info of each display
            for (int i = 0; i < num_displays; ++i)
            {
                //Debug.Log("Window parameters for display " + i);
                //Debug.Log(" Position: " + hpc_GetDevicePropertyWinX(i) + " , " + hpc_GetDevicePropertyWinY(i));
                //Debug.Log(" Size: " + hpc_GetDevicePropertyScreenW(i) + " , " + hpc_GetDevicePropertyScreenH(i));
                //Debug.Log(" Aspect ratio: " + hpc_GetDevicePropertyDisplayAspect(i));
                //Debug.Log("Shader uniforms for display " + i);
                //Debug.Log(" pitch: " + hpc_GetDevicePropertyPitch(i));
                //Debug.Log(" tilt: " + hpc_GetDevicePropertyTilt(i));
                //Debug.Log(" center: " + hpc_GetDevicePropertyCenter(i));
                //Debug.Log(" subp: " + hpc_GetDevicePropertySubp(i));
                //Debug.Log(" fringe: " + hpc_GetDevicePropertyFringe(i));
                //Debug.Log(" viewcone: " + hpc_GetViewCone(i));
                //Debug.Log(string.Format(" RI: {0}\n BI: {1}\n invView: {2}", hpc_GetDevicePropertyRi(i), hpc_GetDevicePropertyBi(i), hpc_GetDevicePropertyInvView(i)));
                int screenWidth = hpc_GetDevicePropertyScreenW(i);
                int screenHeight = hpc_GetDevicePropertyScreenH(i);
                float subp = hpc_GetDevicePropertySubp(i);
                float viewCone = hpc_GetViewCone(i);
                float aspect = hpc_GetDevicePropertyDisplayAspect(i);
                float pitch = hpc_GetDevicePropertyPitch(i);
                float slope = hpc_GetDevicePropertyTilt(i);
                float center = hpc_GetDevicePropertyCenter(i); 
                float fringe = hpc_GetDevicePropertyFringe(i);
                string serial = GetSerial(i);
                string LKGname = GetLKGName(i);
                int xpos = hpc_GetDevicePropertyWinX(i);
                int ypos = hpc_GetDevicePropertyWinY(i);
                Calibration newCal = new Calibration(   i, GetLKGunityIndex(i),
                                                        screenWidth, screenHeight, 
                                                        subp, viewCone, aspect, 
                                                        pitch, slope, center, 
                                                        fringe, 
                                                        serial, LKGname,
                                                        xpos, ypos
                                                    );
                calibrations[i] = newCal;
            }
            return calibrations;
        }
    }

    public struct LoadResults {
        public bool attempted;
        public bool calibrationFound;
        public bool lkgDisplayFound;
        public LoadResults(bool attempted, bool calibrationFound, bool lkgDisplayFound) {
            this.attempted = attempted;
            this.calibrationFound = calibrationFound;
            this.lkgDisplayFound = lkgDisplayFound;
        }
	}

	[System.Serializable] 
	public class LoadEvent : UnityEvent<LoadResults> {};
}