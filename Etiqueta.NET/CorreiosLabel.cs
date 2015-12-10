using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using BarcodeLib;

namespace Etiqueta.NET.Core
{
    /// <summary>
    /// Represent a label to be created
    /// </summary>
    public class CorreiosLabel
    {
        #region Properties

        private IList<String> PathsList { get; set; }
        private String company { get; set; }
        private String postCard { get; set; }

        private String agf { get; set; }

        private String admCode { get; set; }

        private String contractNumber { get; set; }

        /// <summary>
        /// Send type
        /// </summary>
        public enum LabelType
        {
            /// <summary>
            /// Letter
            /// </summary>
            CARTA,
            /// <summary>
            /// PAC
            /// </summary>
            PAC,
            /// <summary>
            /// SEDEX
            /// </summary>
            SEDEX
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Initialize class
        /// </summary>
        /// <param name="company">Company Name to be placed inside seal</param>
        /// <param name="postCard">post cad of correios account</param>
        /// <param name="agf">correios agency</param>
        /// <param name="admCode">administrative code of correios account</param>
        public CorreiosLabel(String company, String postCard, String agf, String admCode)
        {
            this.company = company;
            this.postCard = postCard;
            this.agf = agf;
            this.admCode = admCode;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method to generate labels
        /// </summary>
        /// <param name="registryCode">Object posted number</param>
        /// <param name="sender">Sender of package</param>
        /// <param name="receiver">Receiver of package</param>
        /// <param name="type">Delivery type</param>

        public void Generate(String registryCode, Sender sender, Receiver receiver, LabelType type)
        {
            Bitmap bmp = GetLabelTemplate(type);
            RectangleF cartaoPostagemPoint = new RectangleF(130, 25, 150, 22);
            RectangleF agfPoint = new RectangleF(130, 56, 200, 22);
            RectangleF admCodePoint = new RectangleF(385, 52, 200, 22);
            RectangleF companyCodePoint = new RectangleF(390, 62, 200, 22);
            RectangleF receiverPoint = new RectangleF(20, 260, 380, 80);
            RectangleF senderPoint = new RectangleF(20, 460, 380, 80);
            RectangleF codRegistroPoint = new RectangleF(130, 88, 150, 22);
            RectangleF codBarrasRegistroPoint = new RectangleF(80, 111, 360, 81);
            RectangleF codBarrasCepPoint = new RectangleF(65, 330, 160, 103);


            Graphics g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            String format = "{0}\n{1}\n{2}, {3}\n{4}  {5} - {6}";

            var imgBarCodeRegistry = GerarCodBarrasRegistro(registryCode);
            g.DrawImage(imgBarCodeRegistry, codBarrasRegistroPoint);

            var imgCodigoBarrasZip = GerarCodBarrasCep(receiver.ZipCode);
            g.DrawImage(imgCodigoBarrasZip, codBarrasCepPoint);

            if (type != LabelType.CARTA)
            {
                g.DrawString(admCode, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, admCodePoint);
                g.DrawString(company, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, companyCodePoint);
                g.DrawString(postCard, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, cartaoPostagemPoint);
                g.DrawString(agf, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, agfPoint);
            }      
          
            g.DrawString(registryCode, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, codRegistroPoint);            
            g.DrawString(String.Format(format, receiver.Name.ToUpper(), receiver.Address.ToUpper(), receiver.Complement.ToUpper(), receiver.District.ToUpper(), receiver.ZipCode.ToUpper(), receiver.City.ToUpper(), receiver.State.ToUpper()), new Font("Arial", 10), Brushes.Black, receiverPoint);
            g.DrawString(String.Format(format, sender.Name.ToUpper(), sender.Address.ToUpper(), sender.Complement.ToUpper(), sender.District.ToUpper(), sender.ZipCode.ToUpper(), sender.City.ToUpper(), sender.State.ToUpper()), new Font("Arial", 10), Brushes.Black, senderPoint);
            String fileName = "label-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff") + ".jpg";
            g.Flush();
            String path = fileName;
            bmp.Save(path);
            bmp.Dispose();

            if (PathsList == null)
            {
                PathsList = new List<String>();
            }
            PathsList.Add(path);
        }

        /// <summary>
        /// Method to generate labels
        /// </summary>
        /// <param name="registryCode">Object posted number</param>
        /// <param name="sender">Sender of package</param>
        /// <param name="receiver">Receiver of package</param>
        /// <param name="type">Delivery type</param>
        /// <param name="logo">Logo size 100x50</param>
        public void Generate(String registryCode, Sender sender, Receiver receiver, LabelType type, Image logo)
        {
            Bitmap bmp = GetLabelTemplate(type);
            RectangleF logoPoint = new RectangleF(25, 25, 150, 50);
            RectangleF cartaoPostagemPoint = new RectangleF(130, 25, 150, 22);
            RectangleF agfPoint = new RectangleF(130, 56, 200, 22);
            RectangleF admCodePoint = new RectangleF(385, 52, 200, 22);
            RectangleF companyCodePoint = new RectangleF(390, 62, 200, 22);
            RectangleF receiverPoint = new RectangleF(20, 260, 380, 80);
            RectangleF senderPoint = new RectangleF(20, 460, 380, 80);
            RectangleF codRegistroPoint = new RectangleF(130, 88, 150, 22);
            RectangleF codBarrasRegistroPoint = new RectangleF(80, 111, 360, 81);
            RectangleF codBarrasCepPoint = new RectangleF(65, 330, 160, 103);

    
            Graphics g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            String format = "{0}\n{1}\n{2}, {3}\n{4}  {5} - {6}";

            var imgBarCodeRegistry = GerarCodBarrasRegistro(registryCode);
            g.DrawImage(imgBarCodeRegistry, codBarrasRegistroPoint);

            var imgCodigoBarrasZip = GerarCodBarrasCep(receiver.ZipCode);
            g.DrawImage(imgCodigoBarrasZip, codBarrasCepPoint);

            if (type != LabelType.CARTA)
            {
                g.DrawString(admCode, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, admCodePoint);
                g.DrawString(company, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, companyCodePoint);
                g.DrawString(postCard, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, cartaoPostagemPoint);
                g.DrawString(agf, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, agfPoint);
            }      

            g.DrawImage(logo, logoPoint);
            g.DrawString(registryCode, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, codRegistroPoint);            
            g.DrawString(String.Format(format, receiver.Name.ToUpper(), receiver.Address.ToUpper(), receiver.Complement.ToUpper(), receiver.District.ToUpper(), receiver.ZipCode.ToUpper(), receiver.City.ToUpper(), receiver.State.ToUpper()), new Font("Arial", 10), Brushes.Black, receiverPoint);
            g.DrawString(String.Format(format, sender.Name.ToUpper(), sender.Address.ToUpper(), sender.Complement.ToUpper(), sender.District.ToUpper(), sender.ZipCode.ToUpper(), sender.City.ToUpper(), sender.State.ToUpper()), new Font("Arial", 10), Brushes.Black, senderPoint);
            String fileName = "label-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff") + ".jpg";
            g.Flush();
            String path = fileName;
            bmp.Save(path);
            bmp.Dispose();

            if (PathsList == null)
            {
                PathsList = new List<String>();
            }
            PathsList.Add(path);
        }

        /// <summary>
        /// Method to generate labels
        /// </summary>
        /// <param name="registryCode">Object posted number</param>
        /// <param name="sender">Sender of package</param>
        /// <param name="receiver">Receiver of package</param>
        /// <param name="type">Delivery type</param>
        /// <param name="logo">Logo size 100x50</param>
        public void Generate(String registryCode, Sender sender, Receiver receiver, LabelType type, String logoPath)
        {
            Bitmap bmp = GetLabelTemplate(type);
            RectangleF logoPoint = new RectangleF(25, 25, 100, 50);
            RectangleF cartaoPostagemPoint = new RectangleF(130, 25, 150, 22);
            RectangleF agfPoint = new RectangleF(130, 56, 200, 22);
            RectangleF admCodePoint = new RectangleF(385, 52, 200, 22);
            RectangleF companyCodePoint = new RectangleF(390, 62, 200, 22);
            RectangleF receiverPoint = new RectangleF(20, 260, 380, 80);
            RectangleF senderPoint = new RectangleF(20, 460, 380, 80);
            RectangleF codRegistroPoint = new RectangleF(130, 88, 150, 22);
            RectangleF codBarrasRegistroPoint = new RectangleF(80, 111, 360, 81);
            RectangleF codBarrasCepPoint = new RectangleF(65, 330, 160, 103);           

            Graphics g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            String format = "{0}\n{1}\n{2}, {3}\n{4}  {5} - {6}";

            var imgBarCodeRegistry = GerarCodBarrasRegistro(registryCode);
            g.DrawImage(imgBarCodeRegistry, codBarrasRegistroPoint);

            var imgCodigoBarrasZip = GerarCodBarrasCep(receiver.ZipCode);
            g.DrawImage(imgCodigoBarrasZip, codBarrasCepPoint);

            if(type != LabelType.CARTA)
            {
                g.DrawString(admCode, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, admCodePoint);
                g.DrawString(company, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, companyCodePoint);
                g.DrawString(postCard, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, cartaoPostagemPoint);
                g.DrawString(agf, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, agfPoint);
            }
      

            var logo = Image.FromFile(logoPath);                                  
            g.DrawImage(logo, logoPoint);
            g.DrawString(registryCode, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, codRegistroPoint);
            
            g.DrawString(String.Format(format, receiver.Name.ToUpper(), receiver.Address.ToUpper(), receiver.Complement.ToUpper(), receiver.District.ToUpper(), receiver.ZipCode.ToUpper(), receiver.City.ToUpper(), receiver.State.ToUpper()), new Font("Arial", 10), Brushes.Black, receiverPoint);
            g.DrawString(String.Format(format, sender.Name.ToUpper(), sender.Address.ToUpper(), sender.Complement.ToUpper(), sender.District.ToUpper(), sender.ZipCode.ToUpper(), sender.City.ToUpper(), sender.State.ToUpper()), new Font("Arial", 10), Brushes.Black, senderPoint);
            String fileName = "label-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff") + ".jpg";
            g.Flush();
            String path = fileName;
            bmp.Save(path);
            bmp.Dispose();

            if (PathsList == null)
            {
                PathsList = new List<String>();
            }
            PathsList.Add(path);
        }

        
        /// <summary>
        /// Export labels to PDF format, return phisical path
        /// </summary>
        /// <returns>Path of generated pdf</returns>
        public Uri ExportPDF()
        {
            String nomeUnicoArquivo = "label-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff") + ".pdf";
            var urlPdf = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + nomeUnicoArquivo;
            PdfDocument outputDocument = new PdfDocument();
            var i = 0;
            PdfPage page = null;
            XGraphics gfx = null;
            String path = String.Empty;
            foreach (var url in PathsList)
            {                
                XImage image = XImage.FromFile(url);
          
                if (i == 0)
                {
                    page = outputDocument.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    page.Size = PdfSharp.PageSize.Letter;
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    i++;
                    gfx.DrawImage(image, 0, 0);
                }

                else if (i == 1)
                {
                    i = 0;
                    gfx.DrawImage(image, 400, 0);
                }
            }
            outputDocument.Dispose();
            outputDocument.Save(urlPdf);

            return new Uri(urlPdf);
        }
        #endregion

        #region Private Methods

        private Bitmap GetLabelTemplate(LabelType type)
        {
            Bitmap bmp = null;
            switch (type)
            {
                case LabelType.CARTA: bmp = Properties.Resources.ModeloEtiquetaCarta; break;
                case LabelType.PAC: bmp = Properties.Resources.ModeloEtiquetaPAC; break;
                case LabelType.SEDEX: bmp = Properties.Resources.ModeloEtiquetaSedex; break;
            }
            return bmp;
        }

        private Image GerarCodBarrasRegistro(String registro)
        {
            Barcode barcode = new Barcode()
            {
                Alignment = AlignmentPositions.CENTER,
                Width = 360,
                Height = 70,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
            };

            Image img = barcode.Encode(TYPE.CODE128A, registro);
            return img;
        }

        private Image GerarCodBarrasCep(String cep)
        {
            Barcode barcode = new Barcode()
            {
                Alignment = AlignmentPositions.LEFT,
                Width = 160,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
                LabelPosition = LabelPositions.TOPLEFT,
                LabelFont = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold),
                IncludeLabel = true
            };


            Image img = barcode.Encode(TYPE.CODE128, cep);
            return img;
        }

        #endregion
             
    }
}
