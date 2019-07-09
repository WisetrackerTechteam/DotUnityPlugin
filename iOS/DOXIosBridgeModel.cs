using System;
using DOX_Model;


namespace DOXIosBridgeModel
{
    public struct EventIos
    {
        public string evtName;

        public EventIos(XEvent xEvent)
        {
            this.evtName = xEvent.getEventName();
        }
    }

    public struct ConversionIos
    {
        public string cvrName;

        public ConversionIos(XConversion xConversion)
        {
            this.cvrName = xConversion.getEventName();
        }
    }
}
