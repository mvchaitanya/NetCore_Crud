namespace BookClub2.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text;

    [HtmlTargetElement("image-card")]
    public class ImageCardHelper : TagHelper
    {
        [HtmlAttributeName("url")]
        public string ImageUrl { get; set; }

        [HtmlAttributeName("alt")]
        public string Alternative { get; set; }

        [HtmlAttributeName("title")]
        public string Title { get; set; }

        [HtmlAttributeName("description")]
        public string Description { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "imageHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();

            sb.AppendFormat("<div class=\"card\">");
            sb.AppendFormat("<img class=\"card-img-top\" src=\"{0}\" alt=\"{1}\">", ImageUrl, Alternative);
            sb.AppendFormat("<div class=\"card-body\">");
            sb.AppendFormat("<h5 class=\"card-title\"> {0} </h5>", Title);
            sb.AppendFormat("<p class=\"card-text\"> {0} </p>", Description);
            sb.AppendFormat("</div>");
            sb.AppendFormat("</div>");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
