using System;
using System.Drawing;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace any2any
{
    /// <summary>
    /// Summary description for Class.
    /// </summary>
    class Class
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            TGIS_LayerVector lm;
            TGIS_LayerVector ll;
            TGIS_ShapeType shape_type;

            Console.WriteLine("TatukGIS Samples - ANY->ANY converter ( Vector files only )");
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: ANY2SQL source_file destination shape_type");
                Console.WriteLine("Where shape_type:");
                Console.WriteLine(" A - Arc");
                Console.WriteLine(" G - polyGon");
                Console.WriteLine(" P - Point");
                Console.WriteLine(" M - Multipoint");
                return;
            }

            try
            {
                lm = (TGIS_LayerVector)TGIS_Utils.GisCreateLayer("", args[0]);
                if (lm == null)
                {
                    Console.WriteLine(String.Format("### ERROR: File {0} not found", args[0]));
                    return;
                }
                lm.Open();

                switch (args[2][0])
                {
                    case 'A':
                        shape_type = TGIS_ShapeType.Arc;
                        break;
                    case 'G':
                        shape_type = TGIS_ShapeType.Polygon;
                        break;
                    case 'P':
                        shape_type = TGIS_ShapeType.Point;
                        break;
                    case 'M':
                        shape_type = TGIS_ShapeType.MultiPoint;
                        break;
                    default:
                        shape_type = TGIS_ShapeType.Unknown;
                        break;
                }

                ll = (TGIS_LayerVector)TGIS_Utils.GisCreateLayer("", args[1]);

                if (ll == null)
                {
                    Console.WriteLine(String.Format("### ERROR: File {0} not found", args[1]));
                    return;
                };
                ll.ImportLayer(lm, lm.Extent, shape_type, "", false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
