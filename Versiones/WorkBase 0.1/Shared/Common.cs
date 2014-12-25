using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Diagnostics;

namespace WorkBase.Shared
{
    public sealed class Common
    {
        #region Metodos
        public static string GetSetting(string seccion, string llave, string sDefault)
        {
            string app = Application.ProductName.ToString();
            return GetSetting(app, seccion, llave, sDefault);
        }

        public static string GetSetting(string nombreApp, string seccion, string llave, string sDefault)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\" +
                                                               nombreApp + "\\" + seccion);
            string s = sDefault;

            if (rk != null)
                s = (string)rk.GetValue(llave);

            return s;
        }

        public static void SaveSetting(string seccion, string llave, string config)
        {
            string app = Application.ProductName.ToString();
            SaveSetting(app, seccion, llave, config);
        }

        public static void SaveSetting(string nombreApp, string seccion, string llave, string config)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\VB and VBA Program Settings\" +
                                                                nombreApp + "\\" + seccion);
            rk.SetValue(llave, config);
        }

        public static bool IniciarConWindows(bool local)
        {
            return IniciarConWindows(local, true);
        }

        public static bool IniciarConWindows(bool local, bool activar)
        {
            const string clave = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            string subclave = Application.ProductName.ToString();

            try
            {
                // Abre la clave del usuario actual (CurrentUser) para poder establecer el dato
                // Si la clave CurrentVersion\Run no existe la crea
                RegistryKey rk;

                if (local)
                    rk = Registry.LocalMachine.CreateSubKey(clave, RegistryKeyPermissionCheck.ReadWriteSubTree);
                else
                    rk = Registry.CurrentUser.CreateSubKey(clave, RegistryKeyPermissionCheck.ReadWriteSubTree);

                rk.OpenSubKey(clave, true);

                switch (activar)
                {
                    case true:
                        rk.SetValue(subclave, Application.ExecutablePath.ToString());
                        return true;
                    case false:
                        if (rk.GetValue(subclave, "").ToString() != "")
                        {
                            rk.DeleteValue(subclave);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                /*ErrorManager em = new ErrorManager(ex);
                return false;*/
            }
        }

        public static bool AplicacionEjecutandose()
        {
            bool enEjecucion = false;

            enEjecucion = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1;

            enEjecucion = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).GetUpperBound(0) > 0;

            return enEjecucion;
        }
        #endregion
    }
}
