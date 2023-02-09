using System;
using System.Diagnostics;

namespace PharmacyAdmin.Data
{
    public abstract class DataObject
    {
        public DataObject()
        {
        }

        private ObjectState _State = null;

        public ObjectState State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }


        public void HandleException(Exception ex, string DisplayMessage)
        {
            _State.ErrorOccurred = true;
            _State.DisplayMessage = DisplayMessage;

            _State.ErrorMessage = ex.Message;

            if (_State.MaskException == true)
            {
            }
            else
            {
            }
        }

        public void HandleException(Exception ex, string DisplayMessage, string DatabaseQuery)
        {
            _State.ErrorOccurred = true;
            _State.DisplayMessage = DisplayMessage;
            _State.Query = DatabaseQuery;

            _State.ErrorMessage = ex.Message;

            if (_State.MaskException == true)
            {
            }
            else
            {
            }
        }
    }
}
