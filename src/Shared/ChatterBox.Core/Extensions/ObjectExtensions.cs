﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChatterBox.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T source) 
            where T : class 
        {
            if (source == null)
                throw new ArgumentNullException("source");
            
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }
}