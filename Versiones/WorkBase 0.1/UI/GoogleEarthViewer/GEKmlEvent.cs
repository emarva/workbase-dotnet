using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.UI
{
    public class GEKmlEvent
    {
        #region Campos
        private int _button;       
        private int _clientX;        
        private int _clientY;
        private int _screenX;        
        private int _screenY;
        private double _latitude;        
        private double _longitude;        
        private double _altitude;
        private bool _didHitGlobe;        
        private bool _altKey;        
        private bool _ctrlKey;        
        private bool _shiftKey;       
        private int _timeStamp;
        #endregion

        #region Constructores
        public GEKmlEvent(int button, int clientX, int clientY, int screenX, int screenY, double latitude,
                          double longitude, double altitude, bool didHitGlobe, bool altKey, bool ctrlKey,
                          bool shiftKey, int timeStamp)
        {
            _button = button;
            _clientX = clientX;
            _clientY = clientY;
            _screenX = screenX;
            _screenY = screenY;
            _latitude = latitude;
            _longitude = longitude;
            _altitude = altitude;
            _didHitGlobe = didHitGlobe;
            _altKey = altKey;
            _ctrlKey = ctrlKey;
            _shiftKey = shiftKey;
            _timeStamp = timeStamp;
        }

        public GEKmlEvent() { }
        #endregion

        #region Propiedades
        public int Button
        {
            get { return _button; }
            set { _button = value; }
        }

        public int ClientX
        {
            get { return _clientX; }
            set { _clientX = value; }
        }

        public int ClientY
        {
            get { return _clientY; }
            set { _clientY = value; }
        }

        public int ScreenX
        {
            get { return _screenX; }
            set { _screenX = value; }
        }

        public int ScreenY
        {
            get { return _screenY; }
            set { _screenY = value; }
        }

        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        public double Altitude
        {
            get { return _altitude; }
            set { _altitude = value; }
        }

        public bool DidHitGlobe
        {
            get { return _didHitGlobe; }
            set { _didHitGlobe = value; }
        }

        public bool AltKey
        {
            get { return _altKey; }
            set { _altKey = value; }
        }

        public bool CtrlKey
        {
            get { return _ctrlKey; }
            set { _ctrlKey = value; }
        }

        public bool ShiftKey
        {
            get { return _shiftKey; }
            set { _shiftKey = value; }
        }

        public int TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }
        #endregion
    }
}
