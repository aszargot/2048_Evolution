using System.Windows.Forms;

namespace Game2048.Controls
{
    public class TileButton : Button
    {
        public TileButton()
        {
            Text = "";
            Width = 100;
            Height = 100;
            Enabled = false;
            Font = new System.Drawing.Font("Tahoma", 8,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Millimeter, 0);
            BackgroundImageLayout = ImageLayout.Zoom;
        }
    }
}
