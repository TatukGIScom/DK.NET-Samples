using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TigerGeocoding
{
    /// <summary>
    /// Summary description for WinForm1.
    /// </summary>
    public class HelpForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.TextBox textBox1;

        public HelpForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(451, 308);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "      \r\n    Below follow all possible forms of the address string" +
                ":\r\n      \r\n        06066                                      (zi" +
                "p code)\r\n        06066-3481\r\n      \r\n        CT 06076            " +
                "                    (state code & zip code)\r\n        CT 06238-204" +
                "0\r\n        Storrs                                       (city)\r\n " +
                "       Storrs, 06268\r\n        Storrs, 06268-2022\r\n        Storrs," +
                " CT\r\n        Storrs, CT 06268\r\n        Storrs, CT 06268-2022\r\n   " +
                "     E                                              (street suffi" +
                "x)\r\n        E, 06076\r\n        E, 06076-3137\r\n        E, CT\r\n     " +
                "   E, CT 06076\r\n        E, CT 06076-3138\r\n        E, Storrs\r\n    " +
                "    E, Storrs, 06268\r\n        E, Storrs, 06268-2022\r\n        E, S" +
                "torrs, CT\r\n        E, Storrs, CT 06268\r\n        E, Storrs, CT 062" +
                "68-2022\r\n      \r\n        Rd E                                    " +
                "   (street type & street suffix)\r\n        Rd E, 06268\r\n        Rd" +
                " E, 06268-2022\r\n        Rd E, CT\r\n        Rd E, CT 06268\r\n       " +
                " Rd E, CT 06268-2022\r\n        Dunham Pond                        " +
                "(street name)\r\n        Dunham Pond, 06076\r\n        Dunham Pond, 0" +
                "6076-2022\r\n        Dunham Pond, CT\r\n        Dunham Pond, CT 06268" +
                "\r\n        Dunham Pond, CT 06268-2022\r\n        Dunham Pond, Storrs" +
                "\r\n        Dunham Pond, Storrs, 06268\r\n        Dunham Pond, Storrs" +
                ", 06268-2022\r\n        Dunham Pond, Storrs, CT\r\n        Dunham Pon" +
                "d, Storrs, CT 06268\r\n        Dunham Pond, Storrs, CT 06268-2022\r\n" +
                "        Dunham Pond E\r\n        Dunham Pond E, 06268\r\n        Dunh" +
                "am Pond E, 06268-2022\r\n        Dunham Pond E, CT\r\n        Dunham " +
                "Pond E, CT 06268\r\n        Dunham Pond E, CT 06268-2022\r\n        D" +
                "unham Pond E, Storrs\r\n        Dunham Pond E, Storrs, 06268\r\n     " +
                "   Dunham Pond E, Storrs, 06268-2022\r\n        Dunham Pond E, Stor" +
                "rs, CT\r\n        Dunham Pond E, Storrs, CT 06268\r\n        Dunham P" +
                "ond E, Storrs, CT 06268-2022\r\n        Dunham Pond Rd\r\n        Dun" +
                "ham Pond Rd, 06268\r\n        Dunham Pond Rd, 06268-2022\r\n        D" +
                "unham Pond Rd, CT\r\n        Dunham Pond Rd, CT, 06268\r\n        Dun" +
                "ham Pond Rd, CT, 06268-2022\r\n        Dunham Pond Rd, Storrs\r\n    " +
                "    Dunham Pond Rd, Storrs, 06268\r\n        Dunham Pond Rd, Storrs" +
                ", 06268-2022\r\n        Dunham Pond Rd, Storrs, CT\r\n        Dunham " +
                "Pond Rd, Storrs, CT, 06268\r\n        Dunham Pond Rd, Storrs, CT, 0" +
                "6268-2022\r\n        Dunham Pond Rd E\r\n        Dunham Pond Rd E, 06" +
                "268\r\n        Dunham Pond Rd E, 06268-2022\r\n        Dunham Pond Rd" +
                " E, CT\r\n        Dunham Pond Rd E, CT, 06268\r\n        Dunham Pond " +
                "Rd E, CT, 06268-2022\r\n        Dunham Pond Rd E, Storrs\r\n        D" +
                "unham Pond Rd E, Storrs, 06268\r\n        Dunham Pond Rd E, Storrs," +
                " 06268-2022\r\n        Dunham Pond Rd E, Storrs, CT\r\n        Dunham" +
                " Pond Rd E, Storrs, CT, 06268\r\n        Dunham Pond Rd E, Storrs, " +
                "CT, 06268-2022\r\n        W                                        " +
                "   (street prefix)\r\n        W, 06066\r\n        W, 06066-3481\r\n    " +
                "    W, CT\r\n        W, CT 06066\r\n        W, CT 06066-3481\r\n       " +
                " W, Rockville\r\n        W, Rockville, 06066\r\n        W, Rockville," +
                " 06066-3481\r\n        W, Rockville, CT\r\n        W, Rockville, CT 0" +
                "6066\r\n        W, Rockville, CT 06066-3481\r\n        W St\r\n        " +
                "W St, 06066\r\n        W St, 06066-3481\r\n        W St, CT\r\n        " +
                "W St, CT 06066\r\n        W St, CT 06066-3481\r\n        W St, Rockvi" +
                "lle\r\n        W St, Rockville, 06066\r\n        W St, Rockville, 060" +
                "66-3481\r\n        W St, Rockville, CT\r\n        W St, Rockville, CT" +
                " 06066\r\n        W St, Rockville, CT 06066-3481\r\n        W Main\r\n " +
                "       W Main, 06066\r\n        W Main, 06066-3481\r\n        W Main," +
                " CT\r\n        W Main, CT 06066\r\n        W Main, CT 06066-3481\r\n   " +
                "     W Main, Rockville\r\n        W Main, Rockville, 06066\r\n       " +
                " W Main, Rockville, 06066-3481\r\n        W Main, Rockville, CT\r\n  " +
                "      W Main, Rockville, CT 06066\r\n        W Main, Rockville, CT " +
                "06066-3481\r\n        W Main St\r\n        W Main St, 06066\r\n        " +
                "W Main St, 06066-3481\r\n        W Main St, CT\r\n        W Main St, " +
                "CT 06066\r\n        W Main St, CT 06066-3481\r\n        W Main St, Ro" +
                "ckville\r\n        W Main St, Rockville, 06066\r\n        W Main St, " +
                "Rockville, 06066-3481\r\n        W Main St, Rockville, CT\r\n        " +
                "W Main St, Rockville, CT 06066\r\n        W Main St, Rockville, CT " +
                "06066-3481\r\n      \r\n      \r\n    All address patterns with explici" +
                "t street names can contain also \r\n    the house number at the beg" +
                "inning.\r\n    Some examples:\r\n      \r\n        74 Dunham Pond\r\n    " +
                "    74 Dunham Pond, Storrs\r\n        74 Dunham Pond Rd, CT\r\n      " +
                "  237 Main St\r\n        237 W Main\r\n        237 W Main St, Rockvil" +
                "le, 06066-3481\r\n      \r\n            \r\n    The option \'Exact stree" +
                "t- and city names\' makes the street names are searched \r\n    exac" +
                "tly as they were entered, otherwise they are searched with the li" +
                "ke-operator. \r\n    It means that \'Park\' can give also \'Park\' or \'" +
                "Parker Bridge\' as results.";
            // 
            // HelpForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(451, 308);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(299, 145);
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.Text = "Help";
            this.ResumeLayout(false);
        }
        #endregion
    }
}
