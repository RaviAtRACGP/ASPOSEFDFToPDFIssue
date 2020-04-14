using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using GeneratePDF;

namespace GeneratePDF.Tests
{
    public class FunctionTest
    {


        [Fact]
        public void TestASPOSEFDFtoPDF()
        {


            //var pdfTempaltePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            //    @"PDFTemplates\ExamEnrolmentsTemplate.pdf");

            //var fdfTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            // @"PDFTemplates\623989.fdf");


            //Generate first test
            //-----
            //When creating this template for 4th textbox i have assigned ACTIVITY No 1 FDF field
            // as you can see generated pdf doesn't have ACTIVITY No 2 even though its in the template
            GeneratePDF("Test1.pdf", "Test1-RM.pdf", "2020-22_CPD_Accredited_Activity_Completion-TEST.fdf");

            //Generate second Test 
            //When creating this template for 4th textbox i have assigned ACTIVITY No 2 FDF field
            // as you can see generated pdf doesn't have ACTIVITY No 1 even though its in the template
            GeneratePDF("Test2.pdf", "Test2-RM.pdf", "2020-22_CPD_Accredited_Activity_Completion-TEST.fdf");

        }

        private void GeneratePDF(string outputFile, string pdfFile, string fdfFile)
        {
            var pdfTempaltePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                $@"PDFTemplates\{pdfFile}");


            var fdfTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                $@"PDFTemplates\{fdfFile}");


            var outPutFileLoc =
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), outputFile);

            Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfTempaltePath);

            using (var fdfStream = new FileStream(fdfTemplatePath, FileMode.Open))
            {
                form.ImportFdf(fdfStream);
                form.Save(outPutFileLoc);
            }
        }
    }
}
