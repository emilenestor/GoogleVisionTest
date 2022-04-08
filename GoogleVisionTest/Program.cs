using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoogleVisionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentialsPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"credentials.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();

            Image stockImage = Image.FetchFromUri("https://media.istockphoto.com/photos/portrait-of-a-man-picture-id155360935");
            Image profileImage = Image.FromFile(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "portrait.jpg"));

            //IReadOnlyList<FaceAnnotation> result = client.DetectFaces(stockImage);
            //foreach (FaceAnnotation face in result)
            //{
            //    string poly = string.Join(" - ", face.BoundingPoly.Vertices.Select(v => $"({v.X}, {v.Y})"));
            //    Console.WriteLine($"Confidence: {(int)(face.DetectionConfidence * 100)}%; BoundingPoly: {poly}");
            //}

            //WebDetection webDetection = client.DetectWebInformation(stockImage);

            //Console.WriteLine($"Full image Count: {webDetection.FullMatchingImages.Count}");
            //foreach (WebDetection.Types.WebImage webImage in webDetection.FullMatchingImages)
            //{
            //    Console.WriteLine($"Full image: {webImage.Url} ({webImage.Score})");
            //}

            //Console.WriteLine($"Partial image Count: {webDetection.PartialMatchingImages.Count}");
            //foreach (WebDetection.Types.WebImage webImage in webDetection.PartialMatchingImages)
            //{
            //    Console.WriteLine($"Partial image: {webImage.Url} ({webImage.Score})");
            //}

            //Console.WriteLine($"Page with matching image Count: {webDetection.PagesWithMatchingImages.Count}");
            //foreach (WebDetection.Types.WebPage webPage in webDetection.PagesWithMatchingImages)
            //{
            //    Console.WriteLine($"Page with matching image: {webPage.Url} ({webPage.Score})");
            //}

            //Console.WriteLine($"Web entity Count: {webDetection.WebEntities.Count}");
            //foreach (WebDetection.Types.WebEntity entity in webDetection.WebEntities)
            //{
            //    Console.WriteLine($"Web entity: {entity.EntityId} / {entity.Description} ({entity.Score})");
            //}

            //webDetection = client.DetectWebInformation(profileImage);

            //Console.WriteLine($"Full image Count: {webDetection.FullMatchingImages.Count}");
            //foreach (WebDetection.Types.WebImage webImage in webDetection.FullMatchingImages)
            //{
            //    Console.WriteLine($"Full image: {webImage.Url} ({webImage.Score})");
            //}

            //Console.WriteLine($"Partial image Count: {webDetection.PartialMatchingImages.Count}");
            //foreach (WebDetection.Types.WebImage webImage in webDetection.PartialMatchingImages)
            //{
            //    Console.WriteLine($"Partial image: {webImage.Url} ({webImage.Score})");
            //}

            //Console.WriteLine($"Page with matching image Count: {webDetection.PagesWithMatchingImages.Count}");
            //foreach (WebDetection.Types.WebPage webPage in webDetection.PagesWithMatchingImages)
            //{
            //    Console.WriteLine($"Page with matching image: {webPage.Url} ({webPage.Score})");
            //}

            //Console.WriteLine($"Web entity Count: {webDetection.WebEntities.Count}");
            //foreach (WebDetection.Types.WebEntity entity in webDetection.WebEntities)
            //{
            //    Console.WriteLine($"Web entity: {entity.EntityId} / {entity.Description} ({entity.Score})");
            //}

            AnnotateImageRequest[] requests = new AnnotateImageRequest[] {
                new AnnotateImageRequest
                {
                    Image = stockImage,
                    Features = { new Feature { Type = Feature.Types.Type.WebDetection, MaxResults = 30 } }
                },
                new AnnotateImageRequest
                {
                    Image = profileImage,
                    Features = { new Feature { Type = Feature.Types.Type.WebDetection, MaxResults = 30 } }
                }
            };

            BatchAnnotateImagesResponse response = client.BatchAnnotateImages(requests);

            foreach (var resp in response.Responses)
            {
                Console.WriteLine($"Full image Count: {resp.WebDetection.FullMatchingImages.Count}");
                foreach (WebDetection.Types.WebImage webImage in resp.WebDetection.FullMatchingImages)
                {
                    Console.WriteLine($"Full image: {webImage.Url} ({webImage.Score})");
                }

                Console.WriteLine($"Partial image Count: {resp.WebDetection.PartialMatchingImages.Count}");
                foreach (WebDetection.Types.WebImage webImage in resp.WebDetection.PartialMatchingImages)
                {
                    Console.WriteLine($"Partial image: {webImage.Url} ({webImage.Score})");
                }

                Console.WriteLine($"Page with matching image Count: {resp.WebDetection.PagesWithMatchingImages.Count}");
                foreach (WebDetection.Types.WebPage webPage in resp.WebDetection.PagesWithMatchingImages)
                {
                    Console.WriteLine($"Page with matching image: {webPage.Url} ({webPage.Score})");
                }

                Console.WriteLine($"Web entity Count: {resp.WebDetection.WebEntities.Count}");
                foreach (WebDetection.Types.WebEntity entity in resp.WebDetection.WebEntities)
                {
                    Console.WriteLine($"Web entity: {entity.EntityId} / {entity.Description} ({entity.Score})");
                }
            }

            Console.ReadKey();
        }
    }
}
