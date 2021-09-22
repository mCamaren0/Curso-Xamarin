using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VueloEspacial.Models
{
    public class ArticleModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public string newsSite { get; set; }
        public string summary { get; set; }
        public string publishedAt { get; set; }
        public string updatedAt { get; set; }
        public bool featured { get; set; }
    
        public bool primero { get; set; }
        public ArticleModel()
        {
        }

        public async static Task<ObservableCollection<ArticleModel>> GetAllArticles(int id)
        {
            String url = "https://api.spaceflightnewsapi.net/v3/";
            switch (id)
            {
                case 1: //Noticias
                    url += "articles";
                    break;
                case 2: //Blogs
                    url += "blogs";
                    break;
                case 3: //Informes
                    url += "reports";
                    break;
                default:
                    url += "articles?limit=0";
                    break;
            }

            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(url); 

                //client.DefaultRequestHeaders.Add("app-id", "611c4fcfb585ebe870ba6568");

                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

                string ans = await response.Content.ReadAsStringAsync();

                ObservableCollection<ArticleModel> responseObject = JsonConvert.DeserializeObject<ObservableCollection<ArticleModel>>(ans);
             
                if (responseObject != null)
                {
                    responseObject[0].primero = true;
                    foreach (ArticleModel a in responseObject)
                    {
                       string date;
                       try
                        {
                            date = a.publishedAt.Substring(8, 2) + "/" + a.publishedAt.Substring(5, 2) + "/" + a.publishedAt.Substring(0, 4) + " - " + a.publishedAt.Substring(11, 5);
                        }
                        catch (Exception ex)
                        {
                            date = a.publishedAt;
                        }

                        a.publishedAt = date;
                    }
                }

                return responseObject;

            }
        }

    }
}
