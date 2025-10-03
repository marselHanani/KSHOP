using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KASHOP.DAL.Repositories.@interface;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace KASHOP.BLL.Service.classes
{
    public class ReportService
    {
        private readonly IProductRepository _productRepo;

        public ReportService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
            QuestPDF.Settings.License = LicenseType.Community;

        }
        public async Task<byte[]> GenerateProductReport()
        {
            var products = await _productRepo.GetAllProductsWithImages();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("Product Report")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            foreach (var item in products)
                            {
                                x.Item().Text($"ID: {item.Id} ---- Name: {item.Name}");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });

            // Generate PDF as byte array instead of saving to file
            return document.GeneratePdf();
        }


        // Method to generate and save to file (if you still need it)
        public async Task<string> GenerateProductReportToFile(string filePath = "product.pdf")
        {
            var products = await _productRepo.GetAllProductsWithImages();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("Product Report")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            foreach (var item in products)
                            {
                                x.Item().Text($"ID: {item.Id} ---- Name: {item.Name}");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });

            document.GeneratePdf(filePath);
            return filePath;
        }
    }
}