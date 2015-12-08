using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etiqueta.NET.Core;

namespace Etiqueta.NET.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var Etiqueta = new Etiqueta.NET.Core.CorreiosLabel("ME", "0001", "005", "123456");

            var sender = new Sender("Luar Faria", "QMS 17 casa 2 Cond. Mini chacaras", "sobradinho", "Setor de mansões", "73062708", "Brasilia", "DF");
            var receiver = new Receiver("Luar Faria", "QMS 17 casa 2 Cond. Mini chacaras", "sobradinho", "Setor de mansões", "73062708", "Brasilia", "DF");


            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.SEDEX);
            var caminho = Etiqueta.ExportPDF();

            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.PAC);
            caminho = Etiqueta.ExportPDF();

            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.PAC);
            caminho = Etiqueta.ExportPDF();

            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.CARTA);
            caminho = Etiqueta.ExportPDF();

            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.CARTA);
            caminho = Etiqueta.ExportPDF();
            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.PAC);
            caminho = Etiqueta.ExportPDF();

            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.PAC);
            caminho = Etiqueta.ExportPDF();

            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.PAC);

            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.PAC);
            caminho = Etiqueta.ExportPDF();

            Assert.IsTrue(true);
        }
    }
}
