
using System.Text;
using System.Text.Json;
using Coupons.Models;

namespace Coupons.Utils
{
    public class MailersendUtils
    {
        public async Task EnviarCorreo(string marketingUserName, string emailUser, string Username, string CouponName, string CouponDescription, DateTime UseDate, string ProductName,decimal ProductPrice, decimal Discount, decimal Total)
        {
            string url = "https://api.mailersend.com/v1/email";
            string tokenEmail = "mlsn.615e5bfb39cbde0a574fca52d21fcd3c2a28b53d02bfb57e5e31b26a50dae228";

            string htmlContentPath = @"C:\Users\galle\OneDrive\Escritorio\Coupons\Template\template.html";
            string htmlContent;

            // Verifica que el archivo existe antes de intentar leerlo
            if (File.Exists(htmlContentPath))
            {
                htmlContent = File.ReadAllText(htmlContentPath, Encoding.UTF8);
            }
            else
            {
                Console.WriteLine($"El archivo {htmlContentPath} no existe.");
                return;
            }

            // Reemplazar marcadores en el HTML
            htmlContent = htmlContent.Replace("##Username##", Username)
                                     .Replace("##CouponName##", CouponName)
                                     .Replace("##CouponDescription##", CouponDescription)
                                     .Replace("##UseDate##", UseDate.ToString("yyyy-MM-dd"))
                                     .Replace("##ProductName##", ProductName)
                                     .Replace("##Discount##", Discount.ToString("F2"))
                                     .Replace("##ProductPrice##", ProductPrice.ToString("F2"))
                                     .Replace("##marketingUserName##", marketingUserName)
                                     .Replace("##Total##", Total.ToString("F2"));


            var emailMessage = new Emails
            {
                from = new From { email = "cuopnesFM@trial-3z0vklozo0v47qrx.mlsender.net" },
                to = new List<To> // Usar List en lugar de array estático
                {
                    new To { email = emailUser }
                },
                subject = "¡Felicidades! Tu cupón ha sido redimido correctamente",
                text = "Felicitaciones, has redimido tu cupón correctamente. Por favor, revisa los detalles en el correo.",
                html = htmlContent
            };

            // Serializar el objeto email en formato JSON:
            string jsonBody = JsonSerializer.Serialize(emailMessage);

            using (HttpClient client = new HttpClient())
            {
                // Configurar el encabezado de Authorization para indicar el token de autorización
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenEmail);

                // Crear el contenido de la solicitud POST como StringContent
                StringContent stringContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST a la URL indicada
                HttpResponseMessage response = await client.PostAsync(url, stringContent);

                // Verificar si la solicitud fue exitosa (código de estado: 200 - 209)
                if (response.IsSuccessStatusCode)
                {
                    // Mostrar el estado de la solicitud
                    Console.WriteLine($"Estado de la solicitud: {response.StatusCode}");
                }
                else
                {
                    // Si la solicitud no fue exitosa, mostrar el estado de la solicitud
                    Console.WriteLine($"Correo no enviado: {response.StatusCode}");
                }
            }
        }

        public async Task EnviarCorreoUser(string toEmail, string Subject, string body)
        {
            string url = "https://api.mailersend.com/v1/email";
            string tokenEmail = "mlsn.615e5bfb39cbde0a574fca52d21fcd3c2a28b53d02bfb57e5e31b26a50dae228";

             var emailMessage = new Emails
            {
                from = new From { email = "cuopnesFM@trial-3z0vklozo0v47qrx.mlsender.net" },
                to = new List<To> // Usar List en lugar de array estático
                {
                    new To { email = toEmail}
                },
                subject = Subject,
                text = "Bienvenido",
                html = body
            };

             // Serializar el objeto email en formato JSON:
            string jsonBody = JsonSerializer.Serialize(emailMessage);

            using (HttpClient client = new HttpClient())
            {
                // Configurar el encabezado de Authorization para indicar el token de autorización
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenEmail);

                // Crear el contenido de la solicitud POST como StringContent
                StringContent stringContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST a la URL indicada
                HttpResponseMessage response = await client.PostAsync(url, stringContent);

                // Verificar si la solicitud fue exitosa (código de estado: 200 - 209)
                if (response.IsSuccessStatusCode)
                {
                    // Mostrar el estado de la solicitud
                    Console.WriteLine($"Estado de la solicitud: {response.StatusCode}");
                }
                else
                {
                    // Si la solicitud no fue exitosa, mostrar el estado de la solicitud
                    Console.WriteLine($"Correo no enviado: {response.StatusCode}");
                }
            }
        }
    }
}
