using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPratice
{
    public partial class findDialog : Form
    {
        Minute_Editor minuteEditor;

        public findDialog( Minute_Editor mE)
        {
            InitializeComponent();
            minuteEditor = mE;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                int startPos;
                StringComparison searchType;

                if (chkMatchCase.Checked== true)
                {
                    searchType = StringComparison.Ordinal;
                }

                else
                {
                    searchType = StringComparison.OrdinalIgnoreCase;
                }

                startPos = minuteEditor.richTextBoxPrintCtrl1.Text.IndexOf(txtSearchTerm.Text, searchType);
                if (startPos <= 0)
                {
                    MessageBox.Show("String: " + txtSearchTerm.Text + " not found", "No Matches",
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                minuteEditor.richTextBoxPrintCtrl1.Select(startPos, txtSearchTerm.Text.Length);
                minuteEditor.richTextBoxPrintCtrl1.ScrollToCaret();
                this.FindForm();
                minuteEditor.Activate();
                btnFindNext.Enabled = true;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
