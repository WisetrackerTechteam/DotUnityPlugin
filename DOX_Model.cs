using System;
using System.Collections.Generic;
using UnityEngine;

namespace DOX_Model
{
    public struct XIdentify
    {
        private Dictionary<string, object> set;
        private Dictionary<string, object> setOnce;
        private Dictionary<string, string> unset;
        private Dictionary<string, int> add;
        private Dictionary<string, object> append;
        private Dictionary<string, object> prepend;

        public Dictionary<string, object> getSet()
        {
            return set;
        }

        public Dictionary<string, object> getSetOnce()
        {
            return setOnce;
        }

        public Dictionary<string, string> getUnset()
        {
            return unset;
        }

        public Dictionary<string, int> getAdd()
        {
            return add;
        }

        public Dictionary<string, object> getAppend()
        {
            return append;
        }

        public Dictionary<string, object> getPrepend()
        {
            return prepend;
        }

        public XIdentify(Builder builder)
        {
            set = builder.setDic;
            setOnce = builder.setOnceDic;
            unset = builder.unsetDic;
            add = builder.addDic;
            append = builder.appendDic;
            prepend = builder.prependDic;
        }

        public struct Builder
        {

            public Dictionary<string, object> setDic;
            public Dictionary<string, object> setOnceDic;
            public Dictionary<string, string> unsetDic;
            public Dictionary<string, int> addDic;
            public Dictionary<string, object> appendDic;
            public Dictionary<string, object> prependDic;

            public Builder set(string key, object data)
            {
                if (this.setDic == null)
                {
                    this.setDic = new Dictionary<string, object>();
                }
                this.setDic.Add(key, data);
                return this;
            }

            public Builder setOnce(string key, object data)
            {
                if (this.setDic == null)
                {
                    this.setDic = new Dictionary<string, object>();
                }
                this.setDic.Add(key, data);
                return this;
            }

            public Builder unset(string key)
            {
                if (this.unsetDic == null)
                {
                    this.unsetDic = new Dictionary<string, string>();
                }
                this.unsetDic.Add(key, "");
                return this;
            }

            public Builder add(string key, int increment)
            {
                if (this.addDic == null)
                {
                    this.addDic = new Dictionary<string, int>();
                }
                this.addDic.Add(key, increment);
                return this;
            }

            public Builder append(string key, object data)
            {
                if (this.appendDic == null)
                {
                    this.appendDic = new Dictionary<string, object>();
                }
                this.appendDic.Add(key, data);
                return this;
            }

            public Builder prepend(string key, object data)
            {
                if (this.prependDic == null)
                {
                    this.prependDic = new Dictionary<string, object>();
                }
                this.prependDic.Add(key, data);
                return this;
            }

            public XIdentify build()
            {
                return new XIdentify(this);
            }

        }

    }

    public struct XEvent
    {

        private string eventName;
        private XProperties xProperties;

        public String getEventName()
        {
            return eventName;
        }

        public XProperties getXProperties()
        {
            return xProperties;
        }

        public XEvent(Builder builder)
        {
            eventName = builder.eventName;
            xProperties = builder.xProperties;
        }

        public struct Builder
        {

            public string eventName;
            public XProperties xProperties;

            public Builder setEventName(string eventName)
            {
                this.eventName = eventName;
                return this;
            }

            public Builder setXProperties(XProperties xProperties)
            {
                this.xProperties = xProperties;
                return this;
            }

            public XEvent build()
            {
                return new XEvent(this);
            }

        }

    }

    public struct XConversion
    {

        private string eventName;
        private XProperties xProperties;

        public String getEventName()
        {
            return eventName;
        }

        public XProperties getXProperties()
        {
            return xProperties;
        }

        public XConversion(Builder builder)
        {
            eventName = builder.eventName;
            xProperties = builder.xProperties;
        }

        public struct Builder
        {

            public string eventName;
            public XProperties xProperties;

            public Builder setEventName(string eventName)
            {
                this.eventName = eventName;
                return this;
            }

            public Builder setXProperties(XProperties xProperties)
            {
                this.xProperties = xProperties;
                return this;
            }

            public XConversion build()
            {
                return new XConversion(this);
            }

        }

    }

    public struct XPurchase
    {

        private string orderNo;
        private string revenueType;
        private string currency;
        private List<XProduct> productList;
        private XProperties xProperties;

        public string getOrderNo()
        {
            return orderNo;
        }

        public string getRevenueType()
        {
            return revenueType;
        }

        public string getCurrency()
        {
            return currency;
        }

        public List<XProduct> getProductList()
        {
            return productList;
        }

        public XProperties getXProperties()
        {
            return xProperties;
        }

        public XPurchase(Builder builder)
        {
            orderNo = builder.orderNo;
            revenueType = builder.revenueType;
            currency = builder.currency;
            productList = builder.productList;
            xProperties = builder.xProperties;
        }

        public struct Builder
        {

            public string orderNo;
            public string revenueType;
            public string currency;
            public List<XProduct> productList;
            public XProperties xProperties;

            public Builder setOrderNo(string orderNo)
            {
                this.orderNo = orderNo;
                return this;
            }

            public Builder setRevenueType(string revenueType)
            {
                this.revenueType = revenueType;
                return this;
            }

            public Builder setCurrency(string currency)
            {
                this.currency = currency;
                return this;
            }

            public Builder setProductList(List<XProduct> productList)
            {
                this.productList = productList;
                return this;
            }

            public Builder setXProperties(XProperties xProperties)
            {
                this.xProperties = xProperties;
                return this;
            }

            public XPurchase build()
            {
                return new XPurchase(this);
            }

        }

    }

    public struct XProduct
    {

        private string firstCategory;
        private string secondCategory;
        private string thirdCategory;
        private string detailCategory;
        private string productCode;
        private double orderAmount;
        private long orderQuantity;
        private string productOrderNo;
        private XProperties xProperties;

        public string getFirstCategory()
        {
            return firstCategory;
        }

        public string getSecondCategory()
        {
            return secondCategory;
        }

        public string getThirdCategory()
        {
            return thirdCategory;
        }

        public string getDetailCategory()
        {
            return detailCategory;
        }

        public string getProductCode()
        {
            return productCode;
        }

        public double getOrderAmount()
        {
            return orderAmount;
        }

        public long getOrderQuantity()
        {
            return orderQuantity;
        }

        public string getProductOrderNo()
        {
            return productOrderNo;
        }

        public XProperties getXProperties()
        {
            return xProperties;
        }

        public XProduct(Builder builder)
        {
            firstCategory = builder.firstCategory;
            secondCategory = builder.secondCategory;
            thirdCategory = builder.thirdCategory;
            detailCategory = builder.detailCategory;
            productCode = builder.productCode;
            orderAmount = builder.orderAmount;
            orderQuantity = builder.orderQuantity;
            productOrderNo = builder.productOrderNo;
            xProperties = builder.xProperties;
        }

        public struct Builder
        {

            public string firstCategory;
            public string secondCategory;
            public string thirdCategory;
            public string detailCategory;
            public string productCode;
            public double orderAmount;
            public long orderQuantity;
            public string productOrderNo;
            public XProperties xProperties;

            public Builder setFirstCategory(string firstCategory)
            {
                this.firstCategory = firstCategory;
                return this;
            }

            public Builder setSecondCategory(string secondCategory)
            {
                this.secondCategory = secondCategory;
                return this;
            }

            public Builder setThirdCategory(string thirdCategory)
            {
                this.thirdCategory = thirdCategory;
                return this;
            }

            public Builder setDetailCategory(string detailCategory)
            {
                this.detailCategory = detailCategory;
                return this;
            }

            public Builder seProductCode(string productCode)
            {
                this.productCode = productCode;
                return this;
            }

            public Builder setOrderAmount(double orderAmount)
            {
                this.orderAmount = orderAmount;
                return this;
            }

            public Builder setOrderQuantity(long orderQuantity)
            {
                this.orderQuantity = orderQuantity;
                return this;
            }

            public Builder setProductOrderNo(string productOrderNo)
            {
                this.productOrderNo = productOrderNo;
                return this;
            }

            public Builder setXProperties(XProperties xProperties)
            {
                this.xProperties = xProperties;
                return this;
            }

            public XProduct build()
            {
                return new XProduct(this);
            }

        }

    }

    public struct XProperties
    {

        public Dictionary<string, object> identifyMap;

        public XProperties(Builder builder)
        {
            identifyMap = builder.identifyMap;
        }

        public struct Builder
        {

            public Dictionary<string, object> identifyMap;

            public Builder set(string key, object data)
            {
                if (this.identifyMap == null)
                {
                    this.identifyMap = new Dictionary<string, object>();
                }
                this.identifyMap.Add(key, data);
                return this;
            }

            public XProperties build()
            {
                return new XProperties(this);
            }
        }
    }

}