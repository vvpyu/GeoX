#region WSCF
//------------------------------------------------------------------------------
// <autogenerated code>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated code>
//------------------------------------------------------------------------------
// File time 03-06-10 04:12 PM
//
// This source code was auto-generated by WsContractFirst, Version=0.7.6319.1
#endregion


namespace GeoSupportService
{
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    public class Borough5DigitStreetCodeCollection : System.Collections.CollectionBase
    {
        
        public Borough5DigitStreetCodeCollection()
        {
        }
        
        public Borough5DigitStreetCode this[int idx]
        {
            get
            {
                return ((Borough5DigitStreetCode)(base.InnerList[idx]));
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("value");
                }
                base.InnerList[idx] = value;
            }
        }
        
        public int Add(Borough5DigitStreetCode value)
        {
            if ((value == null))
            {
                throw new System.ArgumentNullException("value");
            }
            return base.InnerList.Add(value);
        }
        
        public int IndexOf(Borough5DigitStreetCode value)
        {
            if ((value == null))
            {
                throw new System.ArgumentNullException("value");
            }
            return base.InnerList.IndexOf(value);
        }
        
        public void Insert(int index, Borough5DigitStreetCode value)
        {
            if ((value == null))
            {
                throw new System.ArgumentNullException("value");
            }
            base.InnerList.Insert(index, value);
        }
        
        public void Remove(Borough5DigitStreetCode value)
        {
            if ((value == null))
            {
                throw new System.ArgumentNullException("value");
            }
            base.InnerList.Remove(value);
        }
        
        public bool Contains(Borough5DigitStreetCode value)
        {
            if ((value == null))
            {
                throw new System.ArgumentNullException("value");
            }
            return base.InnerList.Contains(value);
        }
    }
}
