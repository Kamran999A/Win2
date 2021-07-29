using System.Drawing;
using System.Windows.Forms;
namespace T2
{
    public static class FormHelper
    {
        public static void ShowErrorLabel(Label lbl, string text)
        {
            lbl.Text = text;
            lbl.Visible = true;
        }

        public static void HideErrorLabel(Label lbl)
        {
            lbl.Text = string.Empty;
            lbl.Visible = false;
        }

        public static void RedRequiredLabel(Label lbl)
        {
            lbl.ForeColor = Color.Red;
        }

        public static void BlackRequiredLabel(Label lbl)
        {
            lbl.ForeColor = Color.Black;
        }
    }
}
