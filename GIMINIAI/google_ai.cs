using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GIMINIAI
{
    public class google_ai
    {

        public static async Task<string> Main(string questionTXT)
        {
            var api_key = Environment.GetEnvironmentVariable("OPEN_AI_KEY");

            // 1. החליפי פה למפתח שלך (בלי רווחים ובלי סוגריים מסולסלים)
            //string apiKey = "הטוקן_הארוך_שלך_מגוגל";
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={api_key}";

            using var httpClient = new HttpClient();

            // 2. בניית גוף הבקשה המדויק שגוגל דורשת (חובה בפורמט הזה)
            //var requestBody = "{ \"contents\": [ { \"parts\": [ { \"text\": "+ questionTXT + " } ] } ] }";
            // 2. בניית גוף הבקשה באמצעות אובייקט אנונימי (הדרך הבטוחה ביותר)
            var jsonObject = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = questionTXT } // נסי קודם באנגלית בשביל הבדיקה
                    }
                }
            }
            };

            // המרה אוטומטית ל-JSON תקני
            string requestBody = System.Text.Json.JsonSerializer.Serialize(jsonObject);

            // הגדרת ה-Content עם Content-Type מדויק שגוגל דורשת
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            try
            {
                // 3. שליחת בקשת POST (ולא GET כמו שהדפדפן מנסה לעשות)
                var response = await httpClient.PostAsync(url, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    string answer = ExtractAnswerFromJson(responseString);
                    return answer;
                }
                else
                {
                    if(response.StatusCode.ToString() == "TooManyRequests")
                    {
                        return "הגעת למגבלת הקריאות שלך";
                    }
                    if (int.Parse(response.StatusCode.ToString()) == 401)
                    {

                    Task<string> getModels = GetAiModels();
                    return "יש לך שגיאה ב URL,ייתכן אחת מהאפשרויות הבאות: \n"+
                        "1. המפתח שלך לא תקין או פג תוקף. \n"+
                        "2. ייתכן שזוהי חסימת אינטרנט (נטפרי, אתרוג, חסימת מוסד הלימודים וכו'). \n" +
                        "3. מודל לא קיים, להלן רשימת המודלים הקיימים: \n" +
                        getModels.Result;


                   }
                    return "שגיאה";
                }
            }
            catch (Exception ex)
            {
                return "error";
            }
        }
        private static async Task<string> GetAiModels()
        {
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={api_key}";

            using var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.GetAsync(url);
                string responseString = await response.Content.ReadAsStringAsync();

                //Console.WriteLine("רשימת המודלים הזמינים עבור המפתח שלך:");
                return "רשימת המודלים הזמינים עבור המפתח שלך: \n" + responseString;
            }
            catch (Exception ex)
            {
                return "לא נמצאו מודלי AI עבור בקשתך";
            }
        }

        private static string ExtractAnswerFromJson(string jsonResponse)
        {
            try
            {
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;

                // נתיב: candidates[0].content.parts[0].text
                JsonElement candidates = root.GetProperty("candidates");
                JsonElement firstCandidate = candidates[0];
                JsonElement content = firstCandidate.GetProperty("content");
                JsonElement parts = content.GetProperty("parts");
                JsonElement firstPart = parts[0];
                string text = firstPart.GetProperty("text").GetString();

                return text ?? "No text found";
            }
            catch (Exception ex)
            {
                return $"Error extracting answer: {ex.Message}";
            }
        }
    }


    public class ChatHistory
    {
        private readonly List<Message> _messages = new();

        public void AddSystemMessage(string content)
        {
            _messages.Add(new Message { Role = "system", Content = content });
        }

        public void AddUserMessage(string content)
        {
            _messages.Add(new Message { Role = "user", Content = content });
        }

        public void AddAssistantMessage(string content)
        {
            _messages.Add(new Message { Role = "assistant", Content = content });
        }

        public void Clear()
        {
            _messages.Clear();
        }

        public IReadOnlyList<Message> GetMessages()
        {
            return _messages.AsReadOnly();
        }

        public object GetMessagesForApi()
        {
            var apiMessages = new List<object>();
            foreach (var m in _messages)
            {
                apiMessages.Add(new { role = m.Role, content = m.Content });
            }
            return apiMessages;
        }

        public class Message
        {
            public string Role { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
        }
    }
}
