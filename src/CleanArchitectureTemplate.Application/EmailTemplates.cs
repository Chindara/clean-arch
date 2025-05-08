using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application;

public static class EmailTemplates
{
    private static string ReplaceTemplatePlaceholders(string template, Dictionary<string, string> replacements)
    {
        foreach (var placeholder in replacements)
        {
            template = template.Replace(placeholder.Key, placeholder.Value);
        }
        return template;
    }

    public static string NewUser(string wwwroot, string Name, string Username, string Password)
    {
        // Construct the path to the HTML template
        string templatePath = Path.Combine(wwwroot, "EmailTemplates", "NewUser.html");

        // Read the template
        string htmlTemplate = System.IO.File.ReadAllText(templatePath);

        // Define the replacements
        var replacements = new Dictionary<string, string>
        {
            { "{{user_name}}", Name },
            { "{{user_username}}", Username },
            { "{{user_password}}", Password },
        };

        return ReplaceTemplatePlaceholders(htmlTemplate, replacements);
    }
}
