using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Utility.Extentions
{
    public static class EditorManyForExtensions
    {
        public static MvcHtmlString EditorManyFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, IEnumerable<TValue>>> expression,
            string htmlFieldName = null)
            where TModel : class
        {
            var items = expression.Compile()(html.ViewData.Model);
            var sb = new StringBuilder();
            var hasPrefix = false;

            if (string.IsNullOrEmpty(htmlFieldName))
            {
                var prefix = html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;
                hasPrefix = !string.IsNullOrEmpty(prefix);
                htmlFieldName = (prefix.Length > 0 ? (prefix + ".") : string.Empty) + ExpressionHelper.GetExpressionText(expression);
            }

            if (items != null)
            {
                foreach (var item in items)
                {
                    var dummy = new { Item = item };
                    var guid = Guid.NewGuid().ToString();

                    var memberExp = Expression.MakeMemberAccess(Expression.Constant(dummy), dummy.GetType().GetProperty("Item"));
                    var singleItemExp = Expression.Lambda<Func<TModel, TValue>>(memberExp, expression.Parameters);

                    sb.Append(string.Format(@"<input type=""hidden"" name=""{0}.Index"" value=""{1}"" />", htmlFieldName, guid));
                    sb.Append(html.EditorFor(singleItemExp, null, string.Format("{0}[{1}]", hasPrefix ? ExpressionHelper.GetExpressionText(expression) : htmlFieldName, guid)));
                }
            }
            return new MvcHtmlString(sb.ToString());
        }
    }
}