using System;
using System.IO;

namespace MAF.Collection
{
    public static class StreamFunc
    {
        public static void StreamToFile(Stream pData, string file)
        {
            FileStream outfile = null;
            try
            {
                outfile = new FileStream(file, FileMode.Create);
                const int bufferSize = 65536; // 64K

                Byte[] buffer = new Byte[bufferSize];
                int bytesRead = pData.Read(buffer, 0, bufferSize);

                while (bytesRead > 0)
                {
                    outfile.Write(buffer, 0, bytesRead);
                    bytesRead = pData.Read(buffer, 0, bufferSize);
                }
            }
            finally
            {
                if (outfile != null)
                    outfile.Close();
            }
        }

    }
}
