using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using UnityEngine;
using System.Linq;
using DOX_Model;

public class DOX {

#if UNITY_ANDROID && !UNITY_EDITOR // for android 

    private static AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidJavaObject context = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
    private static AndroidJavaClass unityHelperClass = new AndroidJavaClass("com.sdk.wisetracker.base.UnityHelper");
    private static AndroidJavaObject unityHelperInstance = unityHelperClass.CallStatic<AndroidJavaObject>("getInstance");

    public static void groupIdentify(string key, string value, XIdentify xIdentify) 
    {
        Debug.Log("set group identify");
        if (unityHelperInstance != null)
        {
            string xIdentifyString = xIdentifyObjectToDictionary(xIdentify);
            unityHelperInstance.Call("groupIdentify", key, value, xIdentifyString);
        }
    }

    public static void userIdentify(XIdentify xIdentify) 
    {
        Debug.Log("set user identify");
        if (unityHelperInstance != null)
        {
            string xIdentifyString = xIdentifyObjectToDictionary(xIdentify);
            unityHelperInstance.Call("userIdentify", xIdentifyString);
        }
    }

    public static string xIdentifyObjectToDictionary(XIdentify xIdentify)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("$set", xIdentify.getSet());
        dictionary.Add("$setOnce", xIdentify.getSetOnce());
        dictionary.Add("$unset", xIdentify.getUnset());
        dictionary.Add("$add", xIdentify.getAdd());
        dictionary.Add("$append", xIdentify.getAppend());
        dictionary.Add("$prepend", xIdentify.getPrepend());
        string json = Serializer.Serialize(dictionary);
        Debug.Log("xIdentify json string : " + json);
        return json;
    }

    public static void logEvent(XEvent xEvent)
    {
        Debug.Log("set log event data");
        if (unityHelperInstance != null)
        {
            string xEventString = xEventObjectToDictionary(xEvent);
            unityHelperInstance.Call("logEvent", xEventString);
        }
    }

    public static string xEventObjectToDictionary(XEvent xEvent)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("evtname", xEvent.getEventName());
        dictionary.Add("properties", getXProperties(xEvent.getXProperties()));
        string json = Serializer.Serialize(dictionary);
        Debug.Log("xEvent json string : " + json);
        return json;
    }

    public static void logConversion(XConversion xConversion)
    {
        Debug.Log("set log conversion data");
        if (unityHelperInstance != null)
        {
            string xConversionString = xConversionObjectToDictionary(xConversion);
            unityHelperInstance.Call("logConversion", xConversionString);
        }
    }

    public static string xConversionObjectToDictionary(XConversion xConversion)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("cvrname", xConversion.getEventName());
        dictionary.Add("properties", getXProperties(xConversion.getXProperties()));
        string json = Serializer.Serialize(dictionary);
        Debug.Log("xConversion json string : " + json);
        return json;
    }

    public static void logPurchase(XPurchase xPurchase)
    {
        Debug.Log("set log purchase");
        if (unityHelperInstance != null)
        {
            string xPurchaseString = xPurchaseObjectToDictionary(xPurchase);
            unityHelperInstance.Call("logPurchase", xPurchaseString);
        }
    }

    public static string xPurchaseObjectToDictionary(XPurchase xPurchase)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("ordNo", xPurchase.getOrderNo());
        dictionary.Add("eventType", xPurchase.getRevenueType());
        dictionary.Add("curcy", xPurchase.getCurrency());
        dictionary.Add("product", getProductList(xPurchase.getProductList()));
        dictionary.Add("properties", getXProperties(xPurchase.getXProperties()));
        string json = Serializer.Serialize(dictionary);
        Debug.Log("xPurchase json string : " + json);
        return json;
    }

    public static string getXProperties(XProperties xProperties)
    {
        if(xProperties.identifyMap == null)
        {
            return "";
        }
        Debug.Log("xproperties json string : " + Serializer.Serialize(xProperties.identifyMap));
        return Serializer.Serialize(xProperties.identifyMap);
        }

    public static string getProductList(List<XProduct> productList)
    {

        if (productList == null || productList.Count < 1)
        {
            return "[{}]";
        }

        string jsonArray = "[";

        for (int i = 0; i < productList.Count; i++)
        {

            XProduct product = productList[i];
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string jsonString = "null";

            dictionary.Add("pg1", product.getFirstCategory());
            dictionary.Add("pg2", product.getSecondCategory());
            dictionary.Add("pg3", product.getThirdCategory());
            dictionary.Add("pg4", product.getDetailCategory());
            dictionary.Add("pnc", product.getProductCode());
            dictionary.Add("amt", product.getOrderAmount());
            dictionary.Add("ea", product.getOrderQuantity());
            dictionary.Add("ordPno", product.getProductOrderNo());
            dictionary.Add("properties", getXProperties(product.getXProperties()));

            jsonString = Serializer.Serialize(dictionary);

            if (i == (productList.Count - 1))
            {
                jsonArray = jsonArray + jsonString + "]";
            }
            else
            {
                jsonArray = jsonArray + jsonString + ",";
            }

        }

        Debug.Log("json array : " + jsonArray);
        return jsonArray;

    }

    sealed class Serializer
    {
        StringBuilder builder;

        Serializer()
        {
            builder = new StringBuilder();
        }

        public static string Serialize(object obj)
        {
            var instance = new Serializer();

            instance.SerializeValue(obj);

            return instance.builder.ToString();
        }

        void SerializeValue(object value)
        {
            IList asList;
            IDictionary asDict;
            string asStr;

            if (value == null)
            {
                builder.Append("null");
            }
            else if ((asStr = value as string) != null)
            {
                SerializeString(asStr);
            }
            else if (value is bool)
            {
                builder.Append((bool)value ? "true" : "false");
            }
            else if ((asList = value as IList) != null)
            {
                SerializeArray(asList);
            }
            else if ((asDict = value as IDictionary) != null)
            {
                SerializeObject(asDict);
            }
            else if (value is char)
            {
                SerializeString(new string((char)value, 1));
            }
            else
            {
                SerializeOther(value);
            }
        }

        void SerializeObject(IDictionary obj)
        {
            bool first = true;

            builder.Append('{');

            foreach (object e in obj.Keys)
            {
                if (!first)
                {
                    builder.Append(',');
                }

                SerializeString(e.ToString());
                builder.Append(':');

                SerializeValue(obj[e]);

                first = false;
            }

            builder.Append('}');
        }

        void SerializeArray(IList anArray)
        {
            builder.Append('[');

            bool first = true;

            foreach (object obj in anArray)
            {
                if (!first)
                {
                    builder.Append(',');
                }

                SerializeValue(obj);

                first = false;
            }

            builder.Append(']');
        }

        void SerializeString(string str)
        {
            builder.Append('\"');

            char[] charArray = str.ToCharArray();
            foreach (var c in charArray)
            {
                switch (c)
                {
                    case '"':
                        builder.Append("\\\"");
                        break;
                    case '\\':
                        builder.Append("\\\\");
                        break;
                    case '\b':
                        builder.Append("\\b");
                        break;
                    case '\f':
                        builder.Append("\\f");
                        break;
                    case '\n':
                        builder.Append("\\n");
                        break;
                    case '\r':
                        builder.Append("\\r");
                        break;
                    case '\t':
                        builder.Append("\\t");
                        break;
                    default:
                        int codepoint = Convert.ToInt32(c);
                        if ((codepoint >= 32) && (codepoint <= 126))
                        {
                            builder.Append(c);
                        }
                        else
                        {
                            builder.Append("\\u");
                            builder.Append(codepoint.ToString("x4"));
                        }
                        break;
                }
            }

            builder.Append('\"');
        }

        void SerializeOther(object value)
        {
            // NOTE: decimals lose precision during serialization.
            // They always have, I'm just letting you know.
            // Previously floats and doubles lost precision too.
            if (value is float)
            {
                builder.Append(((float)value).ToString("R"));
            }
            else if (value is int
              || value is uint
              || value is long
              || value is sbyte
              || value is byte
              || value is short
              || value is ushort
              || value is ulong)
            {
                builder.Append(value);
            }
            else if (value is double
              || value is decimal)
            {
                builder.Append(Convert.ToDouble(value).ToString("R"));
            }
            else
            {
                SerializeString(value.ToString());
            }
        }
    }
#elif UNITY_IOS && !UNITY_EDITOR // for ios
    DOX()
        {

        }

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallInit();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallSetUserId(string userId);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallSetDeepLink(string deepLink);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallSetPushId(string pushId);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallUserIdentify(string xIdentifyString);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallGroupIdentify(string groupString, string xIdentifyString);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallLogEvent(string eventName, string xPropetiesString);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallLogConversion(string conversionName, string xPropetiesString);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static public void CallLogRevenue(string orderNo, string revenueType, string currency, string productString, string xPropetiesString);

   
    public static void initialization()
    {
        Debug.Log("initialization");
        CallInit();
    }

    public static void setUserId(string userId)
    {
        CallSetUserId(userId);
    }

    public static void setPushId(string pushId)
    {
        CallSetPushId(pushId);
    }

    public static void setDeepLink(string deepLink)
    {
        CallSetDeepLink(deepLink);
    }
    public static void userIdentify(XIdentify xIdentify) {
        string xIdentifyString =  xIdentifyObjectToDictionary(xIdentify);
        CallUserIdentify(xIdentifyString);
    }  

    public static void groupIdentify(string key, string value, XIdentify xIdentify)
    {
        string groupString = groupObjectToDictionary(key, value);
        string xIdentifyString = xIdentifyObjectToDictionary(xIdentify);

        CallGroupIdentify(groupString, xIdentifyString);
    }
    
    public static void logEvent(XEvent xEvent)
    {
        Debug.Log(xEvent.getXProperties().identifyMap.Count);
        Debug.Log("logEvent");

        string xProperties = getXProperties(xEvent.getXProperties());
        CallLogEvent(xEvent.getEventName(), xProperties);
    }

    public static void logConversion(XConversion xConversion) {
        Debug.Log("logConversion");
   
        string xProperties = getXProperties(xConversion.getXProperties());
        CallLogConversion(xConversion.getEventName(), xProperties);
    }

    public static void logPurchase(XPurchase xPurchae)
    {
        Debug.Log("logPurchase");
        string productsString = getProductList(xPurchae.getProductList());
        string xPropertiesString = getXProperties(xPurchae.getXProperties());

        CallLogRevenue(xPurchae.getOrderNo(), xPurchae.getRevenueType(), xPurchae.getCurrency(), productsString, xPropertiesString);

    }



    public static string groupObjectToDictionary(string key, string value)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add(key, value);
  
        string json = Serializer.Serialize(dictionary);
        Debug.Log("group json string : " + json);
        return json;
    }

    public static string xIdentifyObjectToDictionary(XIdentify xIdentify)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        if(xIdentify.getSet() != null)
        {
            dictionary.Add("$set", xIdentify.getSet());
        }
        if(xIdentify.getSetOnce() != null)
        {
            dictionary.Add("$setOnce", xIdentify.getSetOnce());
        }
        if(xIdentify.getUnset() != null) { 
            dictionary.Add("$unset", xIdentify.getUnset());
        }
        if(xIdentify.getAdd() != null)
        {
            dictionary.Add("$add", xIdentify.getAdd());
        }
        if(xIdentify.getAppend() != null) 
        {
            dictionary.Add("$append", xIdentify.getAppend());
        }
        if(xIdentify.getPrepend() != null) 
        {
            dictionary.Add("$prepend", xIdentify.getPrepend());
        }

        string json = Serializer.Serialize(dictionary);
        Debug.Log("xIdentify json string : " + json);
        return json;
    }


    public static string getXProperties(XProperties xProperties)
    {
        if (xProperties.identifyMap == null)
        {
            return "";
        }
        Debug.Log("xproperties json string : " + Serializer.Serialize(xProperties.identifyMap));
        return Serializer.Serialize(xProperties.identifyMap);
    }

    public static string getProductList(List<XProduct> productList)
    {

        if (productList == null || productList.Count < 1)
        {
            return "[{}]";
        }

        string jsonArray = "[";

        for (int i = 0; i < productList.Count; i++)
        {

            XProduct product = productList[i];
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string jsonString = "null";

            if( product.getFirstCategory() != null)
            {
                dictionary.Add("pg1", product.getFirstCategory());
            }

            if( product.getSecondCategory() != null)
            {
                dictionary.Add("pg2", product.getSecondCategory());
            }

            if(product.getThirdCategory() != null)
            {
                dictionary.Add("pg3", product.getThirdCategory());
            }

            if(product.getDetailCategory() != null)
            {
                dictionary.Add("pg4", product.getDetailCategory());
            }

            if(product.getProductCode() != null)
            {
                dictionary.Add("pnc", product.getProductCode());
            }

            if(product.getOrderAmount() != 0.0)
            {
                dictionary.Add("amt", product.getOrderAmount());
            }

            if(product.getOrderQuantity() != 0)
            {
                dictionary.Add("ea", product.getOrderQuantity());
            }

            if(product.getProductOrderNo() != null)
            {
                dictionary.Add("productOrderNo", product.getProductOrderNo());
            }
           
            dictionary.Add("properties", getXProperties(product.getXProperties()));


            jsonString = Serializer.Serialize(dictionary);

            if (i == (productList.Count - 1))
            {
                jsonArray = jsonArray + jsonString + "]";
            }
            else
            {
                jsonArray = jsonArray + jsonString + ",";
            }

        }

        Debug.Log("json array : " + jsonArray);
        return jsonArray;

    }

    sealed class Serializer
    {
        StringBuilder builder;

        Serializer()
        {
            builder = new StringBuilder();
        }

        public static string Serialize(object obj)
        {
            var instance = new Serializer();

            instance.SerializeValue(obj);

            return instance.builder.ToString();
        }

        void SerializeValue(object value)
        {
            IList asList;
            IDictionary asDict;
            string asStr;

            if (value == null)
            {
                builder.Append("null");
            }
            else if ((asStr = value as string) != null)
            {
                SerializeString(asStr);
            }
            else if (value is bool)
            {
                builder.Append((bool)value ? "true" : "false");
            }
            else if ((asList = value as IList) != null)
            {
                SerializeArray(asList);
            }
            else if ((asDict = value as IDictionary) != null)
            {
                SerializeObject(asDict);
            }
            else if (value is char)
            {
                SerializeString(new string((char)value, 1));
            }
            else
            {
                SerializeOther(value);
            }
        }

        void SerializeObject(IDictionary obj)
        {
            bool first = true;

            builder.Append('{');

            foreach (object e in obj.Keys)
            {
                if (!first)
                {
                    builder.Append(',');
                }

                SerializeString(e.ToString());
                builder.Append(':');

                SerializeValue(obj[e]);

                first = false;
            }

            builder.Append('}');
        }

        void SerializeArray(IList anArray)
        {
            builder.Append('[');

            bool first = true;

            foreach (object obj in anArray)
            {
                if (!first)
                {
                    builder.Append(',');
                }

                SerializeValue(obj);

                first = false;
            }

            builder.Append(']');
        }

        void SerializeString(string str)
        {
            builder.Append('\"');

            char[] charArray = str.ToCharArray();
            foreach (var c in charArray)
            {
                switch (c)
                {
                    case '"':
                        builder.Append("\\\"");
                        break;
                    case '\\':
                        builder.Append("\\\\");
                        break;
                    case '\b':
                        builder.Append("\\b");
                        break;
                    case '\f':
                        builder.Append("\\f");
                        break;
                    case '\n':
                        builder.Append("\\n");
                        break;
                    case '\r':
                        builder.Append("\\r");
                        break;
                    case '\t':
                        builder.Append("\\t");
                        break;
                    default:
                        int codepoint = Convert.ToInt32(c);
                        if ((codepoint >= 32) && (codepoint <= 126))
                        {
                            builder.Append(c);
                        }
                        else
                        {
                            builder.Append("\\u");
                            builder.Append(codepoint.ToString("x4"));
                        }
                        break;
                }
            }

            builder.Append('\"');
        }

        void SerializeOther(object value)
        {
            // NOTE: decimals lose precision during serialization.
            // They always have, I'm just letting you know.
            // Previously floats and doubles lost precision too.
            if (value is float)
            {
                builder.Append(((float)value).ToString("R"));
            }
            else if (value is int
              || value is uint
              || value is long
              || value is sbyte
              || value is byte
              || value is short
              || value is ushort
              || value is ulong)
            {
                builder.Append(value);
            }
            else if (value is double
              || value is decimal)
            {
                builder.Append(Convert.ToDouble(value).ToString("R"));
            }
            else
            {
                SerializeString(value.ToString());
            }
        }
    }
#else
    public static void initialization() { }
    public static void groupIdentify(string key, string value, XIdentify identify) { }
    public static void userIdentify(XIdentify identify) { }
    public static void logEvent(XEvent eventData) { }
    public static void logConversion(XConversion conversionData) { }
    public static void logPurchase(XPurchase purchaseData) { }

#endif

}