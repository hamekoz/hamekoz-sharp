using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Hamekoz.UI.WinForm
{
	/// <summary>
	/// Barcode control.
	/// </summary>
	/// <see href="http://www.codeforge.com/read/40167/BarCodeCtrl.cs__html"/>
	public class BarCodeCtrl : UserControl
	{
		Panel panel;
		PrintDocument printDocument;

		public enum AlignType
		{
			Left,
			Center,
			Right
		}

		public enum BarCodeWeight
		{
			Small = 1,
			Medium,
			Large
		}

		AlignType align = AlignType.Center;
		String code = "1234567890";
		int leftMargin = 10;
		int topMargin = 10;
		int height = 50;
		bool showHeader;
		bool showFooter;
		String headerText = "BarCode Demo";
		BarCodeWeight weight = BarCodeWeight.Small;
		Font headerFont = new Font ("Courier", 18);
		Font footerFont = new Font ("Courier", 8);

		/// <summary>
		/// This controls the vertical alignment of the control, it can be either Left, Center, or Right.
		/// </summary>
		public AlignType VertAlign {
			get { return align; }
			set {
				align = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// This is the text to be displayed as a barcode.
		/// </summary>
		public String BarCode {
			get { return code; }
			set {
				code = value.ToUpper ();
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// This is the height in pixels of the barcode.
		/// </summary>
		public int BarCodeHeight {
			get { return height; }
			set {
				height = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// The size of the left margin.
		/// </summary>
		public int LeftMargin {
			get { return leftMargin; }
			set {
				leftMargin = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// The size of the top margin.
		/// </summary>
		public int TopMargin {
			get { return topMargin; }
			set {
				topMargin = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// Shows the header text specified by the HeaderText property.
		/// </summary>
		public bool ShowHeader {
			get { return showHeader; }
			set {
				showHeader = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// Shows the footer, which is the text representation of the barcode.
		/// </summary>
		public bool ShowFooter {
			get { return showFooter; }
			set {
				showFooter = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// The text to be displayed in the header.
		/// </summary>
		public String HeaderText {
			get { return headerText; }
			set {
				headerText = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// This is the weight of the barcode, it will affect how wide it is displayed. The values are Small, Medium and Large.
		/// </summary>
		public BarCodeWeight Weight {
			get { return weight; }
			set {
				weight = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// The font of the header text.
		/// </summary>
		public Font HeaderFont {
			get { return headerFont; }
			set {
				headerFont = value;
				panel.Invalidate ();
			}
		}

		/// <summary>
		/// The font of the footer text.
		/// </summary>
		public Font FooterFont {
			get { return footerFont; }
			set {
				footerFont = value;
				panel.Invalidate ();
			}
		}

		public BarCodeCtrl ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// This function will display a print dialog and then print the contents of the control to the selected printer.
		/// </summary>
		public void Print ()
		{
			var pd = new PrintDialog ();
			//HACK esto es para que funcione con Windows 7 64bits
			pd.UseEXDialog = true;
			pd.Document = printDocument;

			if (pd.ShowDialog () == DialogResult.OK) {
				pd.Document.Print ();
			}
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent ()
		{
			panel = new Panel ();
			printDocument = new PrintDocument ();
			SuspendLayout ();
			//
			// panel
			//
			panel.BackColor = SystemColors.Window;
			panel.Dock = DockStyle.Fill;
			panel.Location = new Point (0, 0);
			panel.Name = "panel1";
			panel.Size = new Size (424, 240);
			panel.TabIndex = 0;
			panel.Paint += panel_Paint;
			//
			// printDocument
			//
			printDocument.PrintPage += printDocument_PrintPage;
			//
			// BarCodeCtrl
			//
			Controls.Add (panel);
			Name = "BarCodeCtrl";
			Size = new Size (424, 240);
			Resize += BarCodeCtrl_Resize;
			ResumeLayout (false);

		}

		#endregion

		const String alphabet39 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";

		String[] coded39Char = {
			/* 0 */ "000110100",
			/* 1 */ "100100001",
			/* 2 */ "001100001",
			/* 3 */ "101100000",
			/* 4 */ "000110001",
			/* 5 */ "100110000",
			/* 6 */ "001110000",
			/* 7 */ "000100101",
			/* 8 */ "100100100",
			/* 9 */ "001100100",
			/* A */ "100001001",
			/* B */ "001001001",
			/* C */ "101001000",
			/* D */ "000011001",
			/* E */ "100011000",
			/* F */ "001011000",
			/* G */ "000001101",
			/* H */ "100001100",
			/* I */ "001001100",
			/* J */ "000011100",
			/* K */ "100000011",
			/* L */ "001000011",
			/* M */ "101000010",
			/* N */ "000010011",
			/* O */ "100010010",
			/* P */ "001010010",
			/* Q */ "000000111",
			/* R */ "100000110",
			/* S */ "001000110",
			/* T */ "000010110",
			/* U */ "110000001",
			/* V */ "011000001",
			/* W */ "111000000",
			/* X */ "010010001",
			/* Y */ "110010000",
			/* Z */ "011010000",
			/* - */ "010000101",
			/* . */ "110000100",
			/*' '*/ "011000100",
			/* $ */ "010101000",
			/* / */ "010100010",
			/* + */ "010001010",
			/* % */ "000101010",
			/* * */ "010010100"
		};

		void panel_Paint (object sender, PaintEventArgs e)
		{
			const String intercharacterGap = "0";
			String str = '*' + code.ToUpper () + '*';
			int strLength = str.Length;

			for (int i = 0; i < code.Length; i++) {
				if (alphabet39.IndexOf (code [i]) == -1 || code [i] == '*') {
					e.Graphics.DrawString ("INVALID BAR CODE TEXT", Font, Brushes.Red, 10, 10);
					return;
				}
			}

			String encodedString = "";

			for (int i = 0; i < strLength; i++) {
				if (i > 0)
					encodedString += intercharacterGap;

				encodedString += coded39Char [alphabet39.IndexOf (str [i])];
			}

			int encodedStringLength = encodedString.Length;
			int widthOfBarCodeString = 0;
			const double wideToNarrowRatio = 3;

			if (align != AlignType.Left) {
				for (int i = 0; i < encodedStringLength; i++) {
					widthOfBarCodeString += encodedString [i] == '1' ? (int)(wideToNarrowRatio * (int)weight) : (int)weight;
				}
			}

			int x = 0;
			int wid = 0;
			int yTop = 0;
			SizeF hSize = e.Graphics.MeasureString (headerText, headerFont);
			SizeF fSize = e.Graphics.MeasureString (code, footerFont);

			int headerX = 0;
			int footerX = 0;

			if (align == AlignType.Left) {
				x = leftMargin;
				headerX = leftMargin;
				footerX = leftMargin;
			} else if (align == AlignType.Center) {
				x = (Width - widthOfBarCodeString) / 2;
				headerX = (Width - (int)hSize.Width) / 2;
				footerX = (Width - (int)fSize.Width) / 2;
			} else {
				x = Width - widthOfBarCodeString - leftMargin;
				headerX = Width - (int)hSize.Width - leftMargin;
				footerX = Width - (int)fSize.Width - leftMargin;
			}

			if (showHeader) {
				yTop = (int)hSize.Height + topMargin;
				e.Graphics.DrawString (headerText, headerFont, Brushes.Black, headerX, topMargin);
			} else {
				yTop = topMargin;
			}

			for (int i = 0; i < encodedStringLength; i++) {
				wid = encodedString [i] == '1' ? (int)(wideToNarrowRatio * (int)weight) : (int)weight;

				e.Graphics.FillRectangle (i % 2 == 0 ? Brushes.Black : Brushes.White, x, yTop, wid, height);

				x += wid;
			}

			yTop += height;

			if (showFooter)
				e.Graphics.DrawString (code, footerFont, Brushes.Black, footerX, yTop);

		}

		void printDocument_PrintPage (object sender, PrintPageEventArgs e)
		{
			panel_Paint (sender, new PaintEventArgs (e.Graphics, ClientRectangle));
		}

		/// <summary>
		/// This function will save the contents of the control to a bitmap image specified by filename.
		/// </summary>
		/// <param name="file"></param>
		public void SaveImage (String file)
		{
			var bmp = new Bitmap (Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			Graphics g = Graphics.FromImage (bmp);
			g.FillRectangle (Brushes.White, 0, 0, Width, Height);

			panel_Paint (null, new PaintEventArgs (g, ClientRectangle));

			bmp.Save (file);
		}

		void BarCodeCtrl_Resize (object sender, EventArgs e)
		{
			panel.Invalidate ();
		}
	}
}
