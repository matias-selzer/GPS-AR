using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

    /// <summary>
    /// Extension methods to compress a string or bojects to zipped array of bytes,
    /// and decompress a byte array containing zipped data to a string or object
    /// again
    /// </summary>
    public static class StringZipExtensions
    {
        #region Compress
        /// <summary>
        /// Compresses the specified string to a byte array
        /// using the specified encoding.
        /// </summary>
        /// <param name="stringToCompress">The string to compress.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>bytes array with compressed string</returns>
        public static byte[] Compress(
            this string stringToCompress, 
             Encoding encoding )
         {
             var stringAsBytes = encoding.GetBytes(stringToCompress);
             using (var memoryStream = new MemoryStream())
             {
                 using (var zipStream = new GZipStream(memoryStream,
                     CompressionMode.Compress))
                 {
                     zipStream.Write(stringAsBytes, 0, stringAsBytes.Length);
                     zipStream.Close();
                     return (memoryStream.ToArray());
                 }
             }
         }

         /// <summary>
         /// Compresses the specified string a byte array using default
         /// UTF8 encoding.
         /// </summary>
         /// <param name="stringToCompress">The string to compress.</param>
         /// <returns>bytes array with compressed string</returns>
        public static byte[] Compress( this string stringToCompress )
        {
            return Compress(stringToCompress, new UTF8Encoding());
        }


        /// <summary>
        /// XmlSerializes the object to a compressed byte array
        /// using the specified encoding.
        /// </summary>
        /// <param name="objectToCompress">The object to compress.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>bytes array with compressed serialized object</returns>
        public static byte[] Compress(this object objectToCompress,
            Encoding encoding)
        {
            var xmlSerializer = new XmlSerializer(objectToCompress.GetType());
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, objectToCompress);
                return stringWriter.ToString().Compress(encoding);
            }
        }

        /// <summary>
        /// XmlSerializes the object to a compressed byte array using default
        /// UTF8 encoding.
        /// </summary>
        /// <param name="objectToCompress">The object to compress.</param>
        /// <returns>bytes array with compressed serialized object</returns>
        public static byte[] Compress(this object objectToCompress)
        {
            return Compress(objectToCompress, new UTF8Encoding());
        }
        #endregion

        #region Decompress
        /// <summary>
        /// Decompress an array of bytes to a string 
        /// using the specified encoding
        /// </summary>
        /// <param name="compressedString">The compressed string.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>Decompressed string</returns>
        public static string DecompressToString(
            this byte[] compressedString, 
            Encoding encoding)
        {
            const int bufferSize = 1024;
            using (var memoryStream = new MemoryStream(compressedString))
            {
                using (var zipStream = new GZipStream(memoryStream,
                    CompressionMode.Decompress))
                {
                    // Memory stream for storing the decompressed bytes
                    using (var outStream = new MemoryStream())
                    {
                        var buffer = new byte[bufferSize];
                        var totalBytes = 0;
                        int readBytes;
                        while ((readBytes = zipStream.Read(buffer,0, bufferSize)) > 0)
                        {
                            outStream.Write(buffer, 0, readBytes);
                            totalBytes += readBytes;
                        }
                        return encoding.GetString(
                            outStream.GetBuffer(),0, totalBytes);                   
                    }
                }
            }
        }

        /// <summary>
        /// Decompress an array of bytes to a string using default
        /// UTF8 encoding.
        /// </summary>
        /// <param name="compressedString">The compressed string.</param>
        /// <returns>Decompressed string</returns>
        public static string DecompressToString(this byte[] compressedString )
        {
            return DecompressToString(compressedString, new UTF8Encoding());
        }


        /// <summary>
        /// Decompress an array of bytes into an object via Xml Deserialization
        /// using the specified encoding
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compressedObject">The compressed string.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>Decompressed object</returns>
        public static T DecompressToObject<T>(this byte[] compressedObject,
            Encoding encoding)
        {
            var xmlSer = new XmlSerializer(typeof(T));
            return (T)xmlSer.Deserialize(new StringReader(
                compressedObject.DecompressToString(encoding)));
        }

        /// <summary>
        /// Decompress an array of bytes into an object via Xml Deserialization
        /// using the specified encoding
        /// </summary>
        /// <param name="compressedObject">The compressed string.</param>
        /// <returns>Decompressed object</returns>
        public static T DecompressToObject<T>(this byte[] compressedObject )
        {
            return DecompressToObject<T>(compressedObject, new UTF8Encoding());
        }

	public static string CompressString(string value)
	{
		//Transform string into byte[] 
		byte[] byteArray = new byte[value.Length];
		int indexBA = 0;
		foreach (char item in value.ToCharArray())
		{
			byteArray[indexBA++] = (byte)item;
		}

		//Prepare for compress
		System.IO.MemoryStream ms = new System.IO.MemoryStream();
		System.IO.Compression.GZipStream sw = new System.IO.Compression.GZipStream(ms,
			System.IO.Compression.CompressionMode.Compress);

		//Compress
		sw.Write(byteArray, 0, byteArray.Length);
		//Close, DO NOT FLUSH cause bytes will go missing...
		sw.Close();

		//Transform byte[] zip data to string
		byteArray = ms.ToArray();
		System.Text.StringBuilder sB = new System.Text.StringBuilder(byteArray.Length);
		foreach (byte item in byteArray)
		{
			sB.Append((char)item);
		}
		ms.Close();
		sw.Dispose();
		ms.Dispose();

		return sB.ToString();
	}



        #endregion
    }

