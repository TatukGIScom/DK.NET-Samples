using System;
using TatukGIS.NDK;
using System.Drawing;
using TatukGIS.RTL;
using TatukGIS.NDK.WinForms;

namespace project2sqlite
{
    class Program
    {
        public const string TTKLS = @"[TatukGIS Layer]\nStorage=Native\nDialect=SQLITE\n
              Layer=%s\nSqlite=%s\nENGINEOPTIONS=16\n.ttkls";

        public static Bitmap bmp;
        public static TGIS_ViewerBmp vwr;
        public static string sprj;
        public static string path;
        public static string dbf;
        public static TGIS_Config prj;
        public static TStringList lst, lsts;
        public static bool embed;
        public static TGIS_LayerPixel lp;
        public static TGIS_LayerVector lv;
        public static TGIS_LayerVectorSqlAbstract lsv;
        public static TGIS_Config conf;

        static void Main(string[] args)
        {
            Console.WriteLine("TatukGIS Samples - Project->Sqlite converter.");
            if (args.Length < 2)
            {
                Console.WriteLine("Converts vector layers of a project into sqlite database.");
                Console.WriteLine("Usage : ");
                Console.WriteLine("  project2sqlite InputProject OutputProject [db embedded|ttkls] ");
                Console.WriteLine("Parameters:");
                Console.WriteLine("  InputProject OutputProject - paths to project files (must have the same extension)");
                Console.WriteLine("Optional parameters:");
                Console.WriteLine("  db - path to sqlite database");
                Console.WriteLine("  embedded|ttkls - use embedded path to database in project or create ttkls");
                return;
            };
            bmp = new Bitmap(128, 128);
            vwr = new TGIS_ViewerBmp(bmp);

            vwr.Open(args[0]);
            Console.WriteLine(" Opening project file: " + args[0] + " (" + vwr.Items.Count.ToString() + " layers)");
            sprj = args[1];
            path = System.IO.Path.GetDirectoryName(sprj);
            if (!System.IO.Directory.Exists(path))
            {
                Console.WriteLine(String.Format("### ERROR: Directory %s not found", path));
                return;
            };

            prj = TGIS_ConfigFactory.CreateConfig(null, sprj);

            lst = new TStringList();
            if (vwr.ProjectFile != null)
                conf = (TGIS_Config)(vwr.ProjectFile);
            conf.GetStrings(lst);
            if (prj.ConfigFormat == TGIS_ConfigFormat.Ini)
            {
                ((TGIS_ConfigProjectIni)prj).IniObj.SetStrings(lst);
            }
            else
            {
                ((TGIS_ConfigProjectXml)prj).ClearActiveSection();
                ((TGIS_ConfigProjectXml)prj).IniObj.SetStrings(lst);
            };


            lst.Clear();
            lsts = new TStringList();

            if (args.Length > 2)
                dbf = args[2];
            if (dbf == null)
                dbf = "Layers.sqlite";

            if (args.Length > 3)
                embed = args[3] != "ttkls";

            System.IO.Directory.SetCurrentDirectory(path);

            Console.WriteLine("  Importing layers :");

            for (int i = 0; i < vwr.Items.Count; i++)
            {
                if (vwr.Items[i] is TGIS_LayerVector)
                {
                    lv = (TGIS_LayerVector)(vwr.Items[i]);
                    Console.Write("  -> " + lv.Name + "...");

                    int p = lst.IndexOf(lv.Path);
                    if (p >= 0)
                    {
                        prj.SetLayer(lv);
                        if (prj.ConfigFormat == TGIS_ConfigFormat.Ini)
                        {
                            prj.WriteString("Path", lsts[p], "");
                        }
                        else
                        {
                            ((TGIS_ConfigProjectXml)prj).IniObj.SetLayer(lv.Name);
                            ((TGIS_ConfigProjectXml)prj).IniObj.WriteAttribute("Path", lsts[p]);
                        }
                    }
                    else
                    {
                        lsv = new TGIS_LayerSqlSqlite();

                        lsv.Name = lv.Name;
                        lsv.CS = lv.CS;
                        if (embed)
                            lsv.Path = String.Format(
                                          TTKLS,
                                          lv.Name,
                                          System.IO.Path.GetFileNameWithoutExtension(path) + dbf
                                         );
                        else
                            lsv.Path = System.IO.Path.GetFullPath(path) + "\\" +
                                       System.IO.Path.GetFileNameWithoutExtension(lv.Path) + ".ttkls";

                        lsv.set_SQLParameter("PRAGMA synchronous", "OFF");
                        lsv.set_SQLParameter("PRAGMA journal_mode", "OFF");
                        lsv.ImportLayer(lv, lv.Extent, TGIS_ShapeType.Unknown, "", false);

                        prj.SetLayer(lv);
                        if (prj.ConfigFormat == TGIS_ConfigFormat.Ini)
                        {
                            prj.WriteString("Path", lsv.Path, "");
                        }
                        else
                        {
                            ((TGIS_ConfigProjectXml)prj).IniObj.SetLayer(lv.Name);
                            ((TGIS_ConfigProjectXml)prj).IniObj.WriteAttribute("Path", lsv.Path);
                        }

                        lst.Add(lv.Path);
                        lsts.Add(lsv.Path);

                    };
                    Console.WriteLine("ok!");

                }
                else if (vwr.Items[i] is TGIS_LayerPixel)
                {
                    lp = (TGIS_LayerPixel)(vwr.Items[i]);
                    Console.Write("  -> " + lp.Name + "...");
                    // TODO - make ImportLayer for pixelstore
                    Console.WriteLine("skipped!");
                };

            };

            Console.WriteLine(" Saving new project: " + sprj);
            prj.Save();

        }
    }
}
