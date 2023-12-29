using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MethodicalSupportDisciplines.MVC.TagHelpers;

[HtmlTargetElement(Attributes = "is-active-header-route")]
public class IsActiveHeaderRoute : AnchorTagHelper
{
    public IsActiveHeaderRoute(IHtmlGenerator generator) : base(generator)
    {
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        RouteValueDictionary routeData = ViewContext.RouteData.Values;
        string currentController = routeData["controller"] as string ?? string.Empty;
        string currentAction = routeData["action"] as string ?? string.Empty;
        bool result = false;

        if (!string.IsNullOrWhiteSpace(Controller) && !string.IsNullOrWhiteSpace(Action))
        {
            result = string.Equals(Action, currentAction, StringComparison.OrdinalIgnoreCase) &&
                     string.Equals(Controller, currentController, StringComparison.OrdinalIgnoreCase);
        }
        else if (!string.IsNullOrWhiteSpace(Action))
        {
            result = string.Equals(Action, currentAction, StringComparison.OrdinalIgnoreCase);
        }
        else if (!string.IsNullOrWhiteSpace(Controller))
        {
            result = string.Equals(Controller, currentController, StringComparison.OrdinalIgnoreCase);
        }

        if (result)
        {
            string existingClasses = output.Attributes["class"].Value.ToString() ?? string.Empty;

            if (output.Attributes["class"] is not null)
                output.Attributes.Remove(output.Attributes["class"]);

            output.Attributes.Add("class", $"{existingClasses} active");
        }
    }
}