using System;
using System.Collections;
using Cognex.Designer.Core;
using Cognex.Designer.Sequences;
using System.Drawing;
using Cognex.VisionPro;
using System.Drawing.Imaging;
using System.Windows.Forms;
 //Designer plugin to screenshot.
namespace VPDLSupplement
{
    [Cognex.Designer.Core.DisplayName("ScreenShot")]
    [Description("Screenshot returns a bitmap, CogImage8Grey, and CogImage24PlanarColor.")]
    [Category("ScreenShot")]

    public class ScreenShotBlock : ConfigurableBlock
    {
        private readonly GenericPin<System.Drawing.Bitmap> _output1;
        private readonly GenericPin<CogImage8Grey> _output2;
        private readonly GenericPin<CogImage24PlanarColor> _output3;
        private Rectangle screenRect;
 
        public ScreenShotBlock()
        {
            _output1 = CreateOutputPin<System.Drawing.Bitmap>("BMP");
            _output2 = CreateOutputPin<CogImage8Grey>("CogImage8Grey");
            _output3 = CreateOutputPin<CogImage24PlanarColor>("CogImage24PlanarColor");
        }

        protected override void ExecuteSequence()
        {
            screenRect = Screen.PrimaryScreen.Bounds;

            using (Bitmap screenShotBMP = new Bitmap(screenRect.Size.Width, screenRect.Size.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics myGraphics = Graphics.FromImage(screenShotBMP))
                {
                    myGraphics.CopyFromScreen(screenRect.Left, screenRect.Top, 0, 0, screenRect.Size);

                    Cognex.VisionPro.CogImage24PlanarColor myCogImage24PlanarColor = new Cognex.VisionPro.CogImage24PlanarColor(screenShotBMP);

                    Cognex.VisionPro.CogImage8Grey myCogImage8Grey = new Cognex.VisionPro.CogImage8Grey(screenShotBMP);

                    _output1.Value = screenShotBMP;
                    _output2.Value = myCogImage8Grey;
                    _output3.Value = myCogImage24PlanarColor;
                }
            }
        }
    }
}