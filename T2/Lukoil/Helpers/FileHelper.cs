using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using iTextSharp.text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using iTextSharp.text.pdf;
using Paragraph = iTextSharp.text.Paragraph;

namespace T2.Helpers
{
    public static class FileHelper
    {
        private static JsonSerializer Serializer;

        static FileHelper()
        {
            Serializer = new JsonSerializer();
        }

        public static void WriteToJsonFile(Entities.Bill bill, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    using (var jw = new JsonTextWriter(sw))
                    {
                        jw.Formatting = Formatting.Indented;

                        Serializer.Serialize(jw, bill);
                    }
                }
            }
        }

        public static void WriteToPdf(Entities.Bill bill, string filePath)
        {
            var pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 40f, 40f, 60f, 60f);

            PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));

            pdfDoc.Open();


            var cellTextSpace = "   ";

            var spacer = new Paragraph("")
            {
                SpacingBefore = 10f,
                SpacingAfter = 10f,
            };
            pdfDoc.Add(spacer);

            var headerTable = new PdfPTable(new[] { .75f, 2f })
            {
                HorizontalAlignment = 0,
                WidthPercentage = 75,
                DefaultCell = { MinimumHeight = 22f }
            };

            headerTable.AddCell("Company Name");
            headerTable.AddCell("Lukoil");
            headerTable.AddCell("Date");
            headerTable.AddCell($"{DateTime.Now}");

            pdfDoc.Add(headerTable);
            pdfDoc.Add(spacer);


            if (bill.FuelItem != null)
            {
                var refuellingTable = new PdfPTable(3)
                {
                    HorizontalAlignment = 0,
                };


                var cell = new PdfPCell(new Phrase("Refuelling"))
                {
                    HorizontalAlignment = 1,
                    MinimumHeight = 20f,
                };

                cell.Colspan = 3;
                refuellingTable.AddCell(cell);

                var cell1 = new PdfPCell(new Phrase("Name"))
                {
                    HorizontalAlignment = 1,
                    MinimumHeight = 20f,
                };
                var cell2 = new PdfPCell(new Phrase("Liter"))
                {
                    HorizontalAlignment = 1,
                    MinimumHeight = 20f,
                };
                var cell3 = new PdfPCell(new Phrase("Price (usd)"))
                {
                    HorizontalAlignment = 1,
                    MinimumHeight = 20f,
                };

                refuellingTable.AddCell(cell1);
                refuellingTable.AddCell(cell2);
                refuellingTable.AddCell(cell3);

                refuellingTable.AddCell($"{cellTextSpace}{bill.FuelItem.Fuel.Name}");
                refuellingTable.AddCell($"{cellTextSpace}{bill.FuelItem.Liter.ToString()}");
                refuellingTable.AddCell($"{cellTextSpace}{bill.FuelItem.Fuel.Price.ToString()}");



                var cell4 = new PdfPCell(new Phrase("Refuelling cost (usd)"))
                {
                    HorizontalAlignment = 0,
                    MinimumHeight = 20f,
                };

                cell4.Colspan = 2;

                refuellingTable.AddCell(cell4);

                var fuelCost = new PdfPCell(new Phrase(bill.FuelItem.Cost.ToString()))
                {
                    HorizontalAlignment = 1,
                };


                refuellingTable.AddCell(fuelCost);
                pdfDoc.Add(refuellingTable);
                pdfDoc.Add(spacer);

            }

            if (bill.FoodItems.Count > 0)
            {
                var miniCafeTable = new PdfPTable(3)
                {
                    HorizontalAlignment = 0,
                };

                var cell = new PdfPCell(new Phrase("Mini-Cafe"))
                {
                    HorizontalAlignment = 1,
                    MinimumHeight = 20f,
                };

                cell.Colspan = 3;
                miniCafeTable.AddCell(cell);

                var cell1 = new PdfPCell(new Phrase("Name"))
                {
                    HorizontalAlignment = 1,
                    MinimumHeight = 20f,
                };
                var cell2 = new PdfPCell(new Phrase("Amount"))
                {
                    HorizontalAlignment = 1,
                    MinimumHeight = 20f,
                };
                var cell3 = new PdfPCell(new Phrase("Price (usd)"))
                {
                    HorizontalAlignment = 1,
                    MinimumHeight = 20f,
                };

                miniCafeTable.AddCell(cell1);
                miniCafeTable.AddCell(cell2);
                miniCafeTable.AddCell(cell3);

                foreach (var foodItem in bill.FoodItems)
                {
                    miniCafeTable.AddCell($"{cellTextSpace}{foodItem.Food.Name}");
                    miniCafeTable.AddCell($"{cellTextSpace}{foodItem.Count}");
                    miniCafeTable.AddCell($"{cellTextSpace}{foodItem.Food.Price}");
                }

                var cell4 = new PdfPCell(new Phrase("Mini-Cafe cost (usd)"))
                {
                    HorizontalAlignment = 0,
                    MinimumHeight = 20f,
                };

                cell4.Colspan = 2;

                miniCafeTable.AddCell(cell4);

                var miniCafeCostCell = new PdfPCell(new Phrase(bill.FoodItems.Sum(f => f.Cost).ToString()))
                {
                    HorizontalAlignment = 1,
                };

                miniCafeTable.AddCell(miniCafeCostCell);

                pdfDoc.Add(miniCafeTable);
                pdfDoc.Add(spacer);
            }

            pdfDoc.Add(spacer);

            var totalCostTable = new PdfPTable(2)
            {
                HorizontalAlignment = 0,
            };

            var cell5 = new PdfPCell(new Phrase("Total cost (usd)"))
            {
                HorizontalAlignment = 0,
                MinimumHeight = 20f,
            };

            cell5.Colspan = 1;
            totalCostTable.AddCell(cell5);

            var totalCostCell = new PdfPCell(new Phrase($"{bill.TotalCost}"))
            {
                HorizontalAlignment = 1,
            };

            totalCostTable.AddCell(totalCostCell);

            pdfDoc.Add(totalCostTable);
            pdfDoc.Close();
        }

        public static string CreateNewFileName(Guid billId)
        {
            var now = DateTime.Now;

            return $"{now.Day}-{now.Month}-{now.Year}-{now.Hour}-{now.Minute}-{billId.ToString().Substring(0, 8)}";
        }
    }
}
