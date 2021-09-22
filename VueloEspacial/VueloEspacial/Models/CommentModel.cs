using Realms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VueloEspacial.Models
{
    public class CommentModel : RealmObject
    {
        [PrimaryKey]
        public int IdComment { get; set; }
        public int IdArticle { get; set; }
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string Comment { get; set; }
        public string Icon { get; set; }
        public string ArticleName { get; set; }
        public string ArticleImage { get; set; }

        public CommentModel()
        {
        }
    }
}
