﻿using System;
using System.Collections.Generic;

namespace IGK.ICore.DB
{
    /// <summary>
    /// represent the data row
    /// </summary>
    public interface ICoreDataRow
    {
        string this[string name]
        {
            get;
            set;
        }
        T GetValue<T>(string name, T defaultValue = default(T));


        int FieldCount { get; }

        string GetName(int index);
        /// <summary>
        /// get if contains key
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool Contains(string key);
        /// <summary>
        /// get the primary key of the row
        /// </summary>
        string Primary { get; set; }

        Dictionary<string, object> ToDictionary();
        /// <summary>
        /// update the value with the object
        /// </summary>
        /// <param name="tItem">item</param>
        /// <param name="dataAdapter">data adapter to get data value</param>
        void UpdateValue(ICoreDataTable tItem, CoreDataAdapterBase dataAdapter);
        /// <summary>
        /// get the source table name
        /// </summary>
        Type SourceTableInterface { get; }
    }
}