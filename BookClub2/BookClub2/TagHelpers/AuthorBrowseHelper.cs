namespace BookClub2.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text;

    [HtmlTargetElement("browse-author")]
    public class AuthorBrowseHelper : TagHelper
    {
        [HtmlAttributeName("forename")]
        public ModelExpression Forename { get; set; }
        [HtmlAttributeName("lastname")]
        public ModelExpression Lastname { get; set; }

        [HtmlAttributeName("yob")]
        public ModelExpression Yob { get; set; }

        [HtmlAttributeName("cob")]
        public ModelExpression CountryOfBirth { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "browseAuthor";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();

            sb.AppendFormat("<li class=\"list-group-item\">");
        
            sb.AppendFormat("<a class=\"btn btn-primary\" target=\"_blank\" role=\"button\" href=\"https://www.google.com/search?q= {0} + {1} + {2} + {3}\" >Wyszukaj</a>", this.Forename.Model, this.Lastname.Model, this.Yob.Model, this.CountryOfBirth.Model);
            sb.AppendFormat("<b> Autor: </b>");
            sb.AppendFormat("<a> {0} {1} </a>", this.Forename.Model, this.Lastname.Model);
            sb.AppendFormat("</li>");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
