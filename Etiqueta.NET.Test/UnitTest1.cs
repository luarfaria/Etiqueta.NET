using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etiqueta.NET.Core;
using System.Drawing;

namespace Etiqueta.NET.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var Etiqueta = new CorreiosLabel("ME", "0001", "005", "123456");

            var sender = new Sender("Luar Faria", "QMS 17 casa 2 Cond. Mini chacaras", "sobradinho", "Setor de mansões", "73062708", "Brasilia", "DF");
            var receiver = new Receiver("Luar Faria", "QMS 17 casa 2 Cond. Mini chacaras", "sobradinho", "Setor de mansões", "73062708", "Brasilia", "DF");


            Etiqueta.Generate("JH980121092BR", sender, receiver, CorreiosLabel.LabelType.CARTA, @"C:\Users\luar.faria\Documents\logo.png");
            Etiqueta.Generate("JH980121092BR", sender, receiver, CorreiosLabel.LabelType.SEDEX, @"C:\Users\luar.faria\Documents\logo.png");
            Etiqueta.Generate("JH980121092BR", sender, receiver, CorreiosLabel.LabelType.PAC, @"C:\Users\luar.faria\Documents\logo.png");


            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.PAC);
            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.SEDEX);
            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.CARTA);

            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.CARTA, Image.FromFile(@"C:\Users\luar.faria\Documents\logo.png"));
            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.PAC, Image.FromFile(@"C:\Users\luar.faria\Documents\logo.png"));
            Etiqueta.Generate("SP0014454045BR", sender, receiver, CorreiosLabel.LabelType.SEDEX, Image.FromFile(@"C:\Users\luar.faria\Documents\logo.png"));
            var caminho = Etiqueta.ExportPDF();

       

            Assert.IsTrue(true);
        }
    }
}
