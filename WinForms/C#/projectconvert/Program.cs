using System;
using TatukGIS.NDK;
using System.Drawing;
using TatukGIS.RTL;
using TatukGIS.NDK.WinForms;
using System.IO;

namespace projectconvert
{
    class Program
    {
        public static TGIS_ViewerBmp vwr;
        public static String path;

        static void Main(string[] args)
        {
            Console.WriteLine("TatukGIS Samples - TTKGP->TTKPROJECT converter");
            if (args.Length < 1)
            {
                Console.WriteLine("Usage : ");
                Console.WriteLine("Enter path of the TTKGP project. TTKPROJECT output will be placed in the same directory.");
                Console.WriteLine("TTKGP file will be kept in its place after conversion.");
                Console.WriteLine("Put directories with filenames and .TTKGP extension into parameters.");
                return;
            };
            vwr = new TGIS_ViewerBmp();
            path = args[0];
            vwr.Open(path);
            path = Path.ChangeExtension(path, ".ttkproject");
            vwr.SaveProjectAs(path);
        }
    }
}
