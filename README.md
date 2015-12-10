# Etiqueta.NET
Etiqueta.NET is a library to generate labels homologated by correios, you can easily export and print massive quantity of labels to send packages or letters to correios.

Supported send types: Letter, PAC, Sedex.

To use this library to generate labels is necessary to have a contract with correios to use send-type PAC or SEDEX. Is necessary send post card, origin agency and administrative code. To use letter send-type is not necessary have a correios contract.

To know how to do a contract with correios access the address http://www.correios.com.br/sobre-correios/fale-com-os-correios/contatos-comerciais

# Quick Start
The basic steps to getting labels are:

1. Instantiate the CorreiosLabel class, the constructor need a few parameters that will be used to print header label.
2. Generate label using Generate Method.
3. Export label using ExportPDF Method.

<a href="nuget.org/packages/Etiqueta.NET/" target="_blank">Click here to download it from nuget package</a>
